using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Domain.UnitOfWorkContracts;

namespace DevSkill.Inventory.Application.Services
{
    /// <summary>
    /// The priced offer made to a customer. It never touches stock and never posts to
    /// the customer ledger; it only states what the sale would cost while the offer is
    /// being held.
    /// </summary>
    /// <remarks>
    /// A price left at zero falls back to the customer's own price list, and a
    /// discount left at zero falls back to the discount rules. Anything typed in wins,
    /// because that is the whole point of negotiating.
    /// </remarks>
    public class SalesQuotationManagementService : ISalesQuotationManagementService
    {
        private readonly IInventoryUnitOfWork _salesQuotationUnitOfWork;
        private readonly IPriceListManagementService _priceListManagementService;
        private readonly IDiscountRuleManagementService _discountRuleManagementService;

        public SalesQuotationManagementService(IInventoryUnitOfWork unitOfWork,
            IPriceListManagementService priceListManagementService,
            IDiscountRuleManagementService discountRuleManagementService)
        {
            _salesQuotationUnitOfWork = unitOfWork;
            _priceListManagementService = priceListManagementService;
            _discountRuleManagementService = discountRuleManagementService;
        }

        public async Task<Guid> CreateSalesQuotationAsync(SalesQuotation salesQuotation)
        {
            await _salesQuotationUnitOfWork.BeginTransactionAsync();

            try
            {
                var context = await ValidateAsync(salesQuotation, null);
                var salesQuotationId = Guid.NewGuid();

                var priced = await PriceLinesAsync(context, salesQuotationId, null);

                var header = new SalesQuotation
                {
                    Id = salesQuotationId,
                    QuotationNo = await GenerateQuotationNoAsync(),
                    CustomerId = context.Customer.Id,
                    QuotationDate = context.PricingDate,
                    ValidUntil = salesQuotation.ValidUntil,
                    BusinessLocationId = context.Warehouse.Id,
                    SalespersonId = context.SalespersonId,
                    PaymentTermId = context.PaymentTermId,
                    CustomerReference = salesQuotation.CustomerReference ?? string.Empty,
                    Notes = salesQuotation.Notes ?? string.Empty,
                    Status = RestrictCreateStatus(salesQuotation.Status),
                    SubTotal = priced.SubTotal,
                    ItemDiscountTotal = priced.ItemDiscountTotal,
                    TotalTax = priced.TotalTax,
                    OtherCharges = context.OtherCharges,
                    GrandTotal = priced.GrandTotal + context.OtherCharges,
                    CreatedBy = salesQuotation.CreatedBy ?? string.Empty,
                    Created = DateTime.Now,
                    Updated = DateTime.Now,
                    SalesQuotationItems = priced.Lines
                };

                await _salesQuotationUnitOfWork.SalesQuotationRepository.AddAsync(header);
                await _salesQuotationUnitOfWork.CommitTransactionAsync();

                return salesQuotationId;
            }
            catch (InvalidOperationException)
            {
                await _salesQuotationUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _salesQuotationUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<bool> UpdateSalesQuotationAsync(SalesQuotation salesQuotation)
        {
            await _salesQuotationUnitOfWork.BeginTransactionAsync();

            try
            {
                var existing = await _salesQuotationUnitOfWork
                    .SalesQuotationRepository
                    .GetSalesQuotationByIdAsync(salesQuotation.Id)
                    ?? throw new InvalidOperationException("Sales quotation not found.");

                GuardAgainstLockedStatus(existing.Status, "edited");
                await GuardAgainstSalesOrdersAsync(existing.Id, "edited");

                // The warehouse a quotation was issued from decides which products its
                // lines may hold, so an edit may change the lines but never that.
                salesQuotation.BusinessLocationId = existing.BusinessLocationId;

                var context = await ValidateAsync(salesQuotation, existing);
                var oldLines = existing.SalesQuotationItems?.ToList() ?? new List<SalesQuotationItem>();

                // The old lines go away and the new set is priced from scratch, so a
                // stale price can never survive an edit. What has already been ordered
                // is carried across, because that is a fact about the past.
                await _salesQuotationUnitOfWork.SalesQuotationItemRepository.RemoveRangeAsync(oldLines);

                var priced = await PriceLinesAsync(context, existing.Id, oldLines);

                foreach (var line in priced.Lines)
                {
                    await _salesQuotationUnitOfWork.SalesQuotationItemRepository.AddAsync(line);
                }

                // The quotation number is issued once and never changes.
                existing.CustomerId = context.Customer.Id;
                existing.QuotationDate = context.PricingDate;
                existing.ValidUntil = salesQuotation.ValidUntil;
                existing.SalespersonId = context.SalespersonId;
                existing.PaymentTermId = context.PaymentTermId;
                existing.CustomerReference = salesQuotation.CustomerReference ?? string.Empty;
                existing.Notes = salesQuotation.Notes ?? string.Empty;
                existing.Status = RestrictUpdateStatus(salesQuotation.Status, existing.Status);
                existing.SubTotal = priced.SubTotal;
                existing.ItemDiscountTotal = priced.ItemDiscountTotal;
                existing.TotalTax = priced.TotalTax;
                existing.OtherCharges = context.OtherCharges;
                existing.GrandTotal = priced.GrandTotal + context.OtherCharges;
                existing.Updated = DateTime.Now;

                await _salesQuotationUnitOfWork.SalesQuotationRepository.EditAsync(existing);
                await _salesQuotationUnitOfWork.CommitTransactionAsync();

                return true;
            }
            catch (InvalidOperationException)
            {
                await _salesQuotationUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _salesQuotationUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<bool> DeleteSalesQuotationAsync(Guid id)
        {
            await _salesQuotationUnitOfWork.BeginTransactionAsync();

            try
            {
                var salesQuotation = await _salesQuotationUnitOfWork
                    .SalesQuotationRepository
                    .GetSalesQuotationByIdAsync(id)
                    ?? throw new InvalidOperationException("Sales quotation not found.");

                GuardAgainstLockedStatus(salesQuotation.Status, "deleted");
                await GuardAgainstSalesOrdersAsync(salesQuotation.Id, "deleted");

                await _salesQuotationUnitOfWork
                    .SalesQuotationItemRepository
                    .RemoveRangeAsync(salesQuotation.SalesQuotationItems?.ToList() ?? new List<SalesQuotationItem>());

                await _salesQuotationUnitOfWork.SalesQuotationRepository.RemoveAsync(salesQuotation);
                await _salesQuotationUnitOfWork.CommitTransactionAsync();

                return true;
            }
            catch (InvalidOperationException)
            {
                await _salesQuotationUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _salesQuotationUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<SalesQuotation?> GetSalesQuotationByIdAsync(Guid id)
        {
            return await _salesQuotationUnitOfWork.SalesQuotationRepository.GetSalesQuotationByIdAsync(id);
        }

        public async Task<(IList<SalesQuotation> data, int total, int totalDisplay)> GetSalesQuotationsAsync(
            int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            return await _salesQuotationUnitOfWork
                .SalesQuotationRepository
                .GetPagedSalesQuotationsAsync(pageIndex, pageSize, search, order);
        }

        public async Task<IList<SalesQuotation>> GetSalesQuotationsByStatusAsync(params SalesQuotationStatus[] statuses)
        {
            return await _salesQuotationUnitOfWork.SalesQuotationRepository.GetSalesQuotationsByStatusAsync(statuses);
        }

        public async Task<string> GenerateQuotationNoAsync()
        {
            var count = await _salesQuotationUnitOfWork.SalesQuotationRepository.GetCountAsync();

            string quotationNo;
            do
            {
                count++;
                quotationNo = $"SQ-{DateTime.Now:yyyyMM}-{count:D4}";
            }
            while (await _salesQuotationUnitOfWork.SalesQuotationRepository.IsQuotationNoDuplicateAsync(quotationNo));

            return quotationNo;
        }

        public async Task SendSalesQuotationAsync(Guid id)
        {
            var salesQuotation = await GetQuotationAsync(id);

            if (salesQuotation.Status != SalesQuotationStatus.Draft)
            {
                throw new InvalidOperationException("Only a draft quotation can be sent to the customer.");
            }

            if (salesQuotation.SalesQuotationItems == null || salesQuotation.SalesQuotationItems.Count == 0)
            {
                throw new InvalidOperationException("A quotation without any product line cannot be sent.");
            }

            if (salesQuotation.ValidUntil.Date < DateTime.Today)
            {
                throw new InvalidOperationException(
                    "This quotation is already past its validity date, so it cannot be sent.");
            }

            salesQuotation.Status = SalesQuotationStatus.Sent;
            salesQuotation.Updated = DateTime.Now;

            await SaveHeaderAsync(salesQuotation);
        }

        public async Task AcceptSalesQuotationAsync(Guid id)
        {
            var salesQuotation = await GetQuotationAsync(id);

            if (salesQuotation.Status != SalesQuotationStatus.Sent)
            {
                throw new InvalidOperationException(
                    "Only a quotation that has been sent to the customer can be accepted.");
            }

            salesQuotation.Status = SalesQuotationStatus.Accepted;
            salesQuotation.Updated = DateTime.Now;

            await SaveHeaderAsync(salesQuotation);
        }

        public async Task RejectSalesQuotationAsync(Guid id, string reason)
        {
            if (string.IsNullOrWhiteSpace(reason))
            {
                throw new InvalidOperationException("A reason is required when the customer turns an offer down.");
            }

            var salesQuotation = await GetQuotationAsync(id);

            if (salesQuotation.Status != SalesQuotationStatus.Sent)
            {
                throw new InvalidOperationException(
                    "Only a quotation that has been sent to the customer can be rejected.");
            }

            salesQuotation.Status = SalesQuotationStatus.Rejected;
            salesQuotation.Notes = string.IsNullOrWhiteSpace(salesQuotation.Notes)
                ? $"Rejected: {reason.Trim()}"
                : $"{salesQuotation.Notes}{Environment.NewLine}Rejected: {reason.Trim()}";
            salesQuotation.Updated = DateTime.Now;

            await SaveHeaderAsync(salesQuotation);
        }

        public async Task ExpireSalesQuotationAsync(Guid id)
        {
            var salesQuotation = await GetQuotationAsync(id);

            if (salesQuotation.Status != SalesQuotationStatus.Sent)
            {
                throw new InvalidOperationException(
                    "Only an offer that is still out with the customer can expire.");
            }

            if (salesQuotation.ValidUntil.Date >= DateTime.Today)
            {
                throw new InvalidOperationException(
                    $"'{salesQuotation.QuotationNo}' is still valid until {salesQuotation.ValidUntil:dd-MM-yyyy}.");
            }

            salesQuotation.Status = SalesQuotationStatus.Expired;
            salesQuotation.Updated = DateTime.Now;

            await SaveHeaderAsync(salesQuotation);
        }

        public async Task CancelSalesQuotationAsync(Guid id)
        {
            var salesQuotation = await GetQuotationAsync(id);

            if (salesQuotation.Status is SalesQuotationStatus.Converted or SalesQuotationStatus.Cancelled)
            {
                throw new InvalidOperationException("This quotation is already closed.");
            }

            await GuardAgainstSalesOrdersAsync(salesQuotation.Id, "cancelled");

            salesQuotation.Status = SalesQuotationStatus.Cancelled;
            salesQuotation.Updated = DateTime.Now;

            await SaveHeaderAsync(salesQuotation);
        }

        public async Task<IList<ConvertibleQuotationLineDto>> GetConvertibleQuotationLinesAsync(Guid salesQuotationId)
        {
            var salesQuotation = await _salesQuotationUnitOfWork
                .SalesQuotationRepository
                .GetSalesQuotationByIdAsync(salesQuotationId);

            if (salesQuotation == null)
            {
                return new List<ConvertibleQuotationLineDto>();
            }

            if (salesQuotation.Status is not (SalesQuotationStatus.Accepted or SalesQuotationStatus.Converted))
            {
                throw new InvalidOperationException(
                    "Only a quotation the customer has accepted can be turned into a sales order.");
            }

            var lines = new List<ConvertibleQuotationLineDto>();

            foreach (var item in salesQuotation.SalesQuotationItems ?? new List<SalesQuotationItem>())
            {
                var remaining = item.Quantity - item.OrderedQuantity;

                lines.Add(new ConvertibleQuotationLineDto
                {
                    ProductId = item.ProductId,
                    ProductName = item.Product?.ProductName ?? string.Empty,
                    UnitName = item.Product?.Unit?.UnitName ?? string.Empty,
                    QuotedQuantity = item.Quantity,
                    OrderedQuantity = item.OrderedQuantity,
                    RemainingQuantity = remaining < 0 ? 0 : remaining,
                    UnitPrice = item.UnitPrice,
                    DiscountAmount = item.DiscountAmount
                });
            }

            return lines;
        }

        public async Task<SalesLinePreviewDto?> GetLinePreviewAsync(Guid productId, decimal quantity,
            decimal unitPrice, decimal discountAmount)
        {
            if (productId == Guid.Empty || quantity <= 0)
            {
                return null;
            }

            var product = await _salesQuotationUnitOfWork.ProductRepository.GetByIdAsync(productId);

            if (product == null)
            {
                return null;
            }

            var price = unitPrice > 0 ? unitPrice : (decimal)product.SellingPrice;
            var lineAmount = Round(price * quantity);
            var discount = Round(Math.Clamp(discountAmount, 0m, lineAmount));
            var taxableAmount = lineAmount - discount;
            var taxRate = await GetTaxRateAsync(product.ApplicableTaxId);
            var taxAmount = Round(taxableAmount * taxRate / 100m);

            return new SalesLinePreviewDto
            {
                UnitName = product.Unit?.UnitName ?? string.Empty,
                CurrentStock = product.CurrentStock,
                AvailableStock = product.CurrentStock - product.ReservedStock,
                SellingPrice = (decimal)product.SellingPrice,
                LineAmount = lineAmount,
                DiscountAmount = discount,
                TaxRate = taxRate,
                TaxAmount = taxAmount,
                LineTotal = taxableAmount + taxAmount
            };
        }

        /// <summary>
        /// Everything create and update both need before any line can be priced.
        /// </summary>
        private async Task<QuotationContext> ValidateAsync(SalesQuotation salesQuotation, SalesQuotation? existing)
        {
            var customer = await _salesQuotationUnitOfWork
                .CustomerRepository
                .GetByIdAsync(salesQuotation.CustomerId)
                ?? throw new InvalidOperationException("Customer not found.");

            // An existing quotation keeps the customer it was issued to, so a customer
            // that went inactive later does not block an edit of that quotation.
            if (existing == null && customer.Status != CustomerStatus.Active)
            {
                throw new InvalidOperationException(
                    $"'{customer.CustomerName}' is inactive, so no quotation can be issued to them.");
            }

            var warehouse = await _salesQuotationUnitOfWork
                .BusinessLocationRepository
                .GetByIdAsync(salesQuotation.BusinessLocationId)
                ?? throw new InvalidOperationException("Warehouse not found.");

            if (existing == null && !warehouse.IsActive)
            {
                throw new InvalidOperationException(
                    $"'{warehouse.LocationName}' is inactive, so nothing can be quoted from it.");
            }

            var quotationDate = salesQuotation.QuotationDate == default
                ? DateTime.Now
                : salesQuotation.QuotationDate;

            if (salesQuotation.ValidUntil.Date < quotationDate.Date)
            {
                throw new InvalidOperationException("Valid Until cannot be earlier than the quotation date.");
            }

            if (salesQuotation.OtherCharges < 0)
            {
                throw new InvalidOperationException("Other charges cannot be negative.");
            }

            var paymentTermId = await ResolvePaymentTermIdAsync(salesQuotation.PaymentTermId);
            var salespersonId = await ResolveSalespersonIdAsync(salesQuotation.SalespersonId, existing == null);

            var grouped = salesQuotation.SalesQuotationItems?
                .Where(x => x.ProductId != Guid.Empty && x.Quantity > 0)
                .GroupBy(x => x.ProductId)
                .Select(g => new RequestedLine(
                    g.Key,
                    g.Sum(x => x.Quantity),
                    g.Max(x => x.UnitPrice),
                    g.Sum(x => x.DiscountAmount)))
                .ToList();

            if (grouped == null || grouped.Count == 0)
            {
                throw new InvalidOperationException("At least one product line with a positive quantity is required.");
            }

            return new QuotationContext(customer, warehouse, grouped, quotationDate, paymentTermId,
                salespersonId, Round(salesQuotation.OtherCharges));
        }

        /// <summary>
        /// Turns the requested lines into stored lines. This is the single place where
        /// the money on a quotation is decided.
        /// </summary>
        private async Task<PricedResult> PriceLinesAsync(QuotationContext context, Guid salesQuotationId,
            IList<SalesQuotationItem>? oldLines)
        {
            decimal subTotal = 0m;
            decimal itemDiscountTotal = 0m;
            decimal totalTax = 0m;

            var lines = new List<SalesQuotationItem>();

            foreach (var requested in context.RequestedItems)
            {
                var product = await _salesQuotationUnitOfWork
                    .ProductRepository
                    .GetByIdAsync(requested.ProductId)
                    ?? throw new InvalidOperationException("Product not found.");

                if (product.Status != ProductStatus.Active)
                {
                    throw new InvalidOperationException($"'{product.ProductName}' is inactive and cannot be quoted.");
                }

                // The dropdown only offers the warehouse's own products; this makes the
                // same rule hold for anything that reaches the server another way.
                if (product.BusinessLocationId != context.Warehouse.Id)
                {
                    throw new InvalidOperationException(
                        $"'{product.ProductName}' is not stocked in '{context.Warehouse.LocationName}'.");
                }

                if (requested.UnitPrice < 0)
                {
                    throw new InvalidOperationException(
                        $"The unit price of '{product.ProductName}' cannot be negative.");
                }

                // Nothing typed in means the customer's own price list decides.
                var unitPrice = requested.UnitPrice > 0
                    ? requested.UnitPrice
                    : await _priceListManagementService.GetEffectiveUnitPriceAsync(
                        product.Id, context.Customer.CustomerType, requested.Quantity, context.PricingDate);

                var lineAmount = Round(unitPrice * requested.Quantity);

                // Same for the discount: nothing typed in means the rules decide.
                var discountAmount = requested.DiscountAmount > 0
                    ? requested.DiscountAmount
                    : await _discountRuleManagementService.CalculateLineDiscountAsync(
                        product.Id, context.Customer.Id, context.Customer.CustomerType,
                        requested.Quantity, lineAmount, context.PricingDate);

                if (discountAmount < 0 || discountAmount > lineAmount)
                {
                    throw new InvalidOperationException(
                        $"The discount on '{product.ProductName}' must be between 0 and {lineAmount:N2}.");
                }

                discountAmount = Round(discountAmount);

                var taxableAmount = lineAmount - discountAmount;
                var taxRate = await GetTaxRateAsync(product.ApplicableTaxId);
                var taxAmount = Round(taxableAmount * taxRate / 100m);

                // What has already been ordered off this line is a fact about the past
                // and survives a re-price.
                var alreadyOrdered = oldLines?
                    .FirstOrDefault(x => x.ProductId == product.Id)?.OrderedQuantity ?? 0m;

                if (alreadyOrdered > requested.Quantity)
                {
                    throw new InvalidOperationException(
                        $"'{product.ProductName}' already has {alreadyOrdered:N2} on a sales order, " +
                        $"so the quoted quantity cannot be cut to {requested.Quantity:N2}.");
                }

                lines.Add(new SalesQuotationItem
                {
                    Id = Guid.NewGuid(),
                    SalesQuotationId = salesQuotationId,
                    ProductId = product.Id,
                    Quantity = requested.Quantity,
                    UnitPrice = unitPrice,
                    DiscountAmount = discountAmount,
                    TaxRate = taxRate,
                    TaxAmount = taxAmount,
                    LineTotal = taxableAmount + taxAmount,
                    OrderedQuantity = alreadyOrdered,
                    Remarks = string.Empty
                });

                subTotal += lineAmount;
                itemDiscountTotal += discountAmount;
                totalTax += taxAmount;
            }

            return new PricedResult(
                lines,
                Round(subTotal),
                Round(itemDiscountTotal),
                Round(totalTax),
                Round(subTotal - itemDiscountTotal + totalTax));
        }

        private async Task<Guid?> ResolvePaymentTermIdAsync(Guid? paymentTermId)
        {
            if (!paymentTermId.HasValue || paymentTermId.Value == Guid.Empty)
            {
                return null;
            }

            var paymentTerm = await _salesQuotationUnitOfWork
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
        /// A salesperson who has since left stays on the documents they already own,
        /// so only a brand new document insists on an active one.
        /// </summary>
        private async Task<Guid?> ResolveSalespersonIdAsync(Guid? salespersonId, bool mustBeActive)
        {
            if (!salespersonId.HasValue || salespersonId.Value == Guid.Empty)
            {
                return null;
            }

            var salesperson = await _salesQuotationUnitOfWork
                .SalespersonRepository
                .GetSalespersonByIdAsync(salespersonId.Value)
                ?? throw new InvalidOperationException("Salesperson not found.");

            if (mustBeActive && salesperson.Status != SalespersonStatus.Active)
            {
                throw new InvalidOperationException(
                    $"'{salesperson.SalespersonName}' is inactive and cannot be named on a new document.");
            }

            return salesperson.Id;
        }

        /// <summary>
        /// A converted quotation has already produced a sales order and a cancelled one
        /// is closed. Neither may be changed or removed.
        /// </summary>
        private static void GuardAgainstLockedStatus(SalesQuotationStatus status, string action)
        {
            if (status == SalesQuotationStatus.Converted)
            {
                throw new InvalidOperationException($"A converted quotation cannot be {action}.");
            }

            if (status == SalesQuotationStatus.Cancelled)
            {
                throw new InvalidOperationException($"A cancelled quotation cannot be {action}.");
            }
        }

        /// <summary>
        /// Even a draft sales order stakes a claim on the offer, so the quotation
        /// behind it stays as it is until that order is cancelled.
        /// </summary>
        private async Task GuardAgainstSalesOrdersAsync(Guid salesQuotationId, string action)
        {
            var salesOrders = await _salesQuotationUnitOfWork
                .SalesOrderRepository
                .GetSalesOrdersByQuotationAsync(salesQuotationId);

            if (salesOrders.Any(x => x.Status != SalesOrderStatus.Cancelled))
            {
                throw new InvalidOperationException(
                    $"A quotation that already has a sales order cannot be {action}.");
            }
        }

        private static SalesQuotationStatus RestrictCreateStatus(SalesQuotationStatus status)
        {
            // A brand new quotation is either kept as a draft or sent straight out.
            return status == SalesQuotationStatus.Sent
                ? SalesQuotationStatus.Sent
                : SalesQuotationStatus.Draft;
        }

        private static SalesQuotationStatus RestrictUpdateStatus(SalesQuotationStatus status,
            SalesQuotationStatus currentStatus)
        {
            // Accepting, rejecting, expiring and converting all belong to their own
            // flows. An edit may only move between draft and sent, and an already
            // accepted quotation keeps that state.
            if (currentStatus == SalesQuotationStatus.Accepted)
            {
                return SalesQuotationStatus.Accepted;
            }

            return status is SalesQuotationStatus.Draft or SalesQuotationStatus.Sent
                ? status
                : SalesQuotationStatus.Draft;
        }

        private async Task<decimal> GetTaxRateAsync(Guid? applicableTaxId)
        {
            if (!applicableTaxId.HasValue || applicableTaxId.Value == Guid.Empty)
            {
                return 0m;
            }

            var applicableTax = await _salesQuotationUnitOfWork
                .ApplicableTaxRepository
                .GetByIdAsync(applicableTaxId.Value);

            return applicableTax?.TaxRate ?? 0m;
        }

        private async Task<SalesQuotation> GetQuotationAsync(Guid id)
        {
            return await _salesQuotationUnitOfWork
                .SalesQuotationRepository
                .GetSalesQuotationByIdAsync(id)
                ?? throw new InvalidOperationException("Sales quotation not found.");
        }

        private async Task SaveHeaderAsync(SalesQuotation salesQuotation)
        {
            await _salesQuotationUnitOfWork.SalesQuotationRepository.EditAsync(salesQuotation);
            await _salesQuotationUnitOfWork.SaveAsync();
        }

        private static decimal Round(decimal value)
        {
            return Math.Round(value, 2, MidpointRounding.AwayFromZero);
        }

        private sealed record RequestedLine(Guid ProductId, decimal Quantity, decimal UnitPrice, decimal DiscountAmount);

        private sealed record QuotationContext(
            Customer Customer,
            BusinessLocation Warehouse,
            IList<RequestedLine> RequestedItems,
            DateTime PricingDate,
            Guid? PaymentTermId,
            Guid? SalespersonId,
            decimal OtherCharges);

        private sealed record PricedResult(
            List<SalesQuotationItem> Lines,
            decimal SubTotal,
            decimal ItemDiscountTotal,
            decimal TotalTax,
            decimal GrandTotal);
    }
}
