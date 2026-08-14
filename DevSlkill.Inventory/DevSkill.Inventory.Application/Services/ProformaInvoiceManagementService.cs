using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Domain.UnitOfWorkContracts;

namespace DevSkill.Inventory.Application.Services
{
    public class ProformaInvoiceManagementService : IProformaInvoiceManagementService
    {
        private readonly IInventoryUnitOfWork _proformaInvoiceUnitOfWork;
        private readonly IPriceListManagementService _priceListManagementService;
        private readonly IDiscountRuleManagementService _discountRuleManagementService;

        public ProformaInvoiceManagementService(IInventoryUnitOfWork unitOfWork,
            IPriceListManagementService priceListManagementService,
            IDiscountRuleManagementService discountRuleManagementService)
        {
            _proformaInvoiceUnitOfWork = unitOfWork;
            _priceListManagementService = priceListManagementService;
            _discountRuleManagementService = discountRuleManagementService;
        }

        public async Task<Guid> CreateProformaInvoiceAsync(ProformaInvoice proformaInvoice)
        {
            await _proformaInvoiceUnitOfWork.BeginTransactionAsync();

            try
            {
                var context = await ValidateAsync(proformaInvoice);
                var proformaInvoiceId = Guid.NewGuid();

                var priced = await PriceLinesAsync(context, proformaInvoiceId);

                var header = new ProformaInvoice
                {
                    Id = proformaInvoiceId,
                    ProformaNo = await GenerateProformaNoAsync(),
                    CustomerId = context.Customer.Id,
                    ProformaDate = context.PricingDate,
                    ValidUntil = proformaInvoice.ValidUntil,
                    BusinessLocationId = context.Warehouse.Id,
                    SalespersonId = proformaInvoice.SalespersonId,
                    SalespersonName = proformaInvoice.SalespersonName ?? string.Empty,
                    PaymentTermId = context.PaymentTermId,
                    Notes = proformaInvoice.Notes ?? string.Empty,
                    Status = RestrictCreateStatus(proformaInvoice.Status),
                    SubTotal = priced.SubTotal,
                    ItemDiscountTotal = priced.ItemDiscountTotal,
                    OrderDiscount = priced.OrderDiscount,
                    TotalTax = priced.TotalTax,
                    GrandTotal = priced.GrandTotal,
                    CreatedBy = proformaInvoice.CreatedBy ?? string.Empty,
                    Created = DateTime.Now,
                    Updated = DateTime.Now,
                    ProformaInvoiceItems = priced.Lines
                };

                await _proformaInvoiceUnitOfWork.ProformaInvoiceRepository.AddAsync(header);
                await _proformaInvoiceUnitOfWork.CommitTransactionAsync();

                return proformaInvoiceId;
            }
            catch (InvalidOperationException)
            {
                await _proformaInvoiceUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _proformaInvoiceUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<bool> UpdateProformaInvoiceAsync(ProformaInvoice proformaInvoice)
        {
            await _proformaInvoiceUnitOfWork.BeginTransactionAsync();

            try
            {
                var existing = await _proformaInvoiceUnitOfWork
                    .ProformaInvoiceRepository
                    .GetProformaInvoiceByIdAsync(proformaInvoice.Id)
                    ?? throw new InvalidOperationException("Proforma invoice not found.");

                GuardAgainstLockedStatus(existing.Status, "edited");
                await GuardAgainstDeliveriesAsync(existing.Id, "edited");

                var context = await ValidateAsync(proformaInvoice);

                // The old lines go away and the new set is priced from scratch, so a
                // stale price can never survive an edit.
                await _proformaInvoiceUnitOfWork
                    .ProformaInvoiceItemRepository
                    .RemoveRangeAsync(existing.ProformaInvoiceItems?.ToList() ?? new List<ProformaInvoiceItem>());

                var priced = await PriceLinesAsync(context, existing.Id);

                foreach (var line in priced.Lines)
                {
                    await _proformaInvoiceUnitOfWork.ProformaInvoiceItemRepository.AddAsync(line);
                }

                // The proforma number is issued once and never changes.
                existing.CustomerId = context.Customer.Id;
                existing.ProformaDate = context.PricingDate;
                existing.ValidUntil = proformaInvoice.ValidUntil;
                existing.BusinessLocationId = context.Warehouse.Id;
                existing.SalespersonId = proformaInvoice.SalespersonId;
                existing.SalespersonName = proformaInvoice.SalespersonName ?? string.Empty;
                existing.PaymentTermId = context.PaymentTermId;
                existing.Notes = proformaInvoice.Notes ?? string.Empty;
                existing.Status = RestrictUpdateStatus(proformaInvoice.Status);
                existing.SubTotal = priced.SubTotal;
                existing.ItemDiscountTotal = priced.ItemDiscountTotal;
                existing.OrderDiscount = priced.OrderDiscount;
                existing.TotalTax = priced.TotalTax;
                existing.GrandTotal = priced.GrandTotal;
                existing.Updated = DateTime.Now;

                await _proformaInvoiceUnitOfWork.ProformaInvoiceRepository.EditAsync(existing);
                await _proformaInvoiceUnitOfWork.CommitTransactionAsync();

                return true;
            }
            catch (InvalidOperationException)
            {
                await _proformaInvoiceUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _proformaInvoiceUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<bool> DeleteProformaInvoiceAsync(Guid id)
        {
            await _proformaInvoiceUnitOfWork.BeginTransactionAsync();

            try
            {
                var proformaInvoice = await _proformaInvoiceUnitOfWork
                    .ProformaInvoiceRepository
                    .GetProformaInvoiceByIdAsync(id)
                    ?? throw new InvalidOperationException("Proforma invoice not found.");

                GuardAgainstLockedStatus(proformaInvoice.Status, "deleted");
                await GuardAgainstDeliveriesAsync(proformaInvoice.Id, "deleted");

                await _proformaInvoiceUnitOfWork
                    .ProformaInvoiceItemRepository
                    .RemoveRangeAsync(proformaInvoice.ProformaInvoiceItems?.ToList() ?? new List<ProformaInvoiceItem>());

                await _proformaInvoiceUnitOfWork
                    .ProformaInvoiceRepository
                    .RemoveAsync(proformaInvoice);

                await _proformaInvoiceUnitOfWork.CommitTransactionAsync();

                return true;
            }
            catch (InvalidOperationException)
            {
                await _proformaInvoiceUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _proformaInvoiceUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<ProformaInvoice?> GetProformaInvoiceByIdAsync(Guid id)
        {
            return await _proformaInvoiceUnitOfWork.ProformaInvoiceRepository.GetProformaInvoiceByIdAsync(id);
        }

        public async Task<(IList<ProformaInvoice> data, int total, int totalDisplay)> GetProformaInvoicesAsync(int pageIndex,
            int pageSize, DataTablesSearch search, string? order)
        {
            return await _proformaInvoiceUnitOfWork
                .ProformaInvoiceRepository
                .GetPagedProformaInvoicesAsync(pageIndex, pageSize, search, order);
        }

        public async Task<string> GenerateProformaNoAsync()
        {
            var count = await _proformaInvoiceUnitOfWork.ProformaInvoiceRepository.GetCountAsync();

            string proformaNo;
            do
            {
                count++;
                proformaNo = $"PI-{DateTime.Now:yyyyMM}-{count:D4}";
            }
            while (await _proformaInvoiceUnitOfWork.ProformaInvoiceRepository.IsProformaNoDuplicateAsync(proformaNo));

            return proformaNo;
        }

        /// <summary>
        /// Everything create and update both need before any pricing can happen.
        /// </summary>
        private async Task<PricingContext> ValidateAsync(ProformaInvoice proformaInvoice)
        {
            var customer = await _proformaInvoiceUnitOfWork
                .CustomerRepository
                .GetByIdAsync(proformaInvoice.CustomerId)
                ?? throw new InvalidOperationException("Customer not found.");

            if (customer.Status != CustomerStatus.Active)
            {
                throw new InvalidOperationException($"'{customer.CustomerName}' is inactive, so a proforma invoice cannot be issued.");
            }

            var warehouse = await _proformaInvoiceUnitOfWork
                .BusinessLocationRepository
                .GetByIdAsync(proformaInvoice.BusinessLocationId)
                ?? throw new InvalidOperationException("Warehouse not found.");

            if (!warehouse.IsActive)
            {
                throw new InvalidOperationException($"'{warehouse.LocationName}' is inactive, so a proforma invoice cannot be issued from it.");
            }

            if (proformaInvoice.ValidUntil.Date < proformaInvoice.ProformaDate.Date)
            {
                throw new InvalidOperationException("Valid Until cannot be earlier than the proforma date.");
            }

            // Payment terms stay optional, but a chosen one has to be a live row
            // in the lookup table.
            var paymentTermId = await ResolvePaymentTermIdAsync(proformaInvoice.PaymentTermId);

            var requestedItems = proformaInvoice.ProformaInvoiceItems?
                .Where(x => x.ProductId != Guid.Empty && x.Quantity > 0)
                .GroupBy(x => x.ProductId)
                .Select(g => new RequestedLine(g.Key, g.Sum(x => x.Quantity)))
                .ToList();

            if (requestedItems == null || requestedItems.Count == 0)
            {
                throw new InvalidOperationException("At least one product line with a positive quantity is required.");
            }

            var pricingDate = proformaInvoice.ProformaDate == default
                ? DateTime.Now
                : proformaInvoice.ProformaDate;

            return new PricingContext(customer, warehouse, requestedItems, pricingDate, paymentTermId);
        }

        private async Task<Guid?> ResolvePaymentTermIdAsync(Guid? paymentTermId)
        {
            if (!paymentTermId.HasValue || paymentTermId.Value == Guid.Empty)
            {
                return null;
            }

            var paymentTerm = await _proformaInvoiceUnitOfWork
                .PaymentTermRepository
                .GetByIdAsync(paymentTermId.Value)
                ?? throw new InvalidOperationException("Payment term not found.");

            if (!paymentTerm.IsActive)
            {
                throw new InvalidOperationException($"'{paymentTerm.TermName}' is inactive and cannot be used.");
            }

            return paymentTerm.Id;
        }

        /// <summary>
        /// Turns the requested product/quantity pairs into fully priced lines.
        /// This is the single place where money is decided.
        /// </summary>
        private async Task<PricedResult> PriceLinesAsync(PricingContext context, Guid proformaInvoiceId)
        {
            decimal subTotal = 0m;
            decimal itemDiscountTotal = 0m;
            decimal totalTax = 0m;

            var lines = new List<ProformaInvoiceItem>();

            foreach (var requested in context.RequestedItems)
            {
                var product = await _proformaInvoiceUnitOfWork
                    .ProductRepository
                    .GetByIdAsync(requested.ProductId)
                    ?? throw new InvalidOperationException("Product not found.");

                if (product.Status != ProductStatus.Active)
                {
                    throw new InvalidOperationException($"'{product.ProductName}' is inactive and cannot be billed.");
                }

                var unitPrice = await _priceListManagementService.GetEffectiveUnitPriceAsync(
                    product.Id, context.Customer.CustomerType, requested.Quantity, context.PricingDate);

                var lineAmount = Round(unitPrice * requested.Quantity);

                var discountAmount = await _discountRuleManagementService.CalculateLineDiscountAsync(
                    product.Id, context.Customer.Id, context.Customer.CustomerType,
                    requested.Quantity, lineAmount, context.PricingDate);

                var taxableAmount = lineAmount - discountAmount;
                var taxRate = await GetTaxRateAsync(product.ApplicableTaxId);
                var taxAmount = Round(taxableAmount * taxRate / 100m);

                lines.Add(new ProformaInvoiceItem
                {
                    Id = Guid.NewGuid(),
                    ProformaInvoiceId = proformaInvoiceId,
                    ProductId = product.Id,
                    Quantity = requested.Quantity,
                    UnitPrice = unitPrice,
                    DiscountAmount = discountAmount,
                    TaxRate = taxRate,
                    TaxAmount = taxAmount,
                    LineTotal = taxableAmount + taxAmount
                });

                subTotal += lineAmount;
                itemDiscountTotal += discountAmount;
                totalTax += taxAmount;
            }

            // Order level discount applies on what is left after the line discounts.
            var orderDiscount = await _discountRuleManagementService.CalculateOrderDiscountAsync(
                context.Customer.Id, context.Customer.CustomerType,
                subTotal - itemDiscountTotal, context.PricingDate);

            return new PricedResult(
                lines,
                Round(subTotal),
                Round(itemDiscountTotal),
                Round(orderDiscount),
                Round(totalTax),
                Round(subTotal - itemDiscountTotal - orderDiscount + totalTax));
        }

        /// <summary>
        /// A converted proforma already produced a sales order, a cancelled one is
        /// closed and a delivered one has already left the warehouse. None of them may
        /// be changed or removed.
        /// </summary>
        private static void GuardAgainstLockedStatus(ProformaInvoiceStatus status, string action)
        {
            if (status == ProformaInvoiceStatus.Converted)
            {
                throw new InvalidOperationException($"A converted proforma invoice cannot be {action}.");
            }

            if (status == ProformaInvoiceStatus.Cancelled)
            {
                throw new InvalidOperationException($"A cancelled proforma invoice cannot be {action}.");
            }

            if (status == ProformaInvoiceStatus.PartiallyDelivered || status == ProformaInvoiceStatus.Delivered)
            {
                throw new InvalidOperationException($"A delivered proforma invoice cannot be {action}.");
            }
        }

        /// <summary>
        /// Even a draft delivery states what the customer has been promised, so the
        /// document behind it stays as it was until that delivery is cancelled.
        /// </summary>
        private async Task GuardAgainstDeliveriesAsync(Guid proformaInvoiceId, string action)
        {
            var deliveries = await _proformaInvoiceUnitOfWork
                .DeliveryRepository
                .GetDeliveriesByProformaInvoiceAsync(proformaInvoiceId);

            if (deliveries.Any(x => x.Status != DeliveryStatus.Cancelled))
            {
                throw new InvalidOperationException(
                    $"A proforma invoice that already has a delivery cannot be {action}.");
            }
        }

        private static ProformaInvoiceStatus RestrictCreateStatus(ProformaInvoiceStatus status)
        {
            // A brand new proforma is only ever an offer, never an outcome.
            return status == ProformaInvoiceStatus.Sent
                ? ProformaInvoiceStatus.Sent
                : ProformaInvoiceStatus.Draft;
        }

        private static ProformaInvoiceStatus RestrictUpdateStatus(ProformaInvoiceStatus status)
        {
            // Converted belongs to the conversion flow and the delivered states belong
            // to the delivery flow. An edit may never reach for any of them.
            return status is ProformaInvoiceStatus.Converted
                or ProformaInvoiceStatus.PartiallyDelivered
                or ProformaInvoiceStatus.Delivered
                ? ProformaInvoiceStatus.Accepted
                : status;
        }

        private async Task<decimal> GetTaxRateAsync(Guid? applicableTaxId)
        {
            if (!applicableTaxId.HasValue || applicableTaxId.Value == Guid.Empty)
            {
                return 0m;
            }

            var applicableTax = await _proformaInvoiceUnitOfWork
                .ApplicableTaxRepository
                .GetByIdAsync(applicableTaxId.Value);

            return applicableTax?.TaxRate ?? 0m;
        }

        private static decimal Round(decimal value)
        {
            return Math.Round(value, 2, MidpointRounding.AwayFromZero);
        }

        private sealed record RequestedLine(Guid ProductId, decimal Quantity);

        private sealed record PricingContext(
            Domain.Entities.SalesEntities.Customer Customer,
            Domain.Entities.BusinessLocation Warehouse,
            IList<RequestedLine> RequestedItems,
            DateTime PricingDate,
            Guid? PaymentTermId);

        private sealed record PricedResult(
            List<ProformaInvoiceItem> Lines,
            decimal SubTotal,
            decimal ItemDiscountTotal,
            decimal OrderDiscount,
            decimal TotalTax,
            decimal GrandTotal);
    }
}
