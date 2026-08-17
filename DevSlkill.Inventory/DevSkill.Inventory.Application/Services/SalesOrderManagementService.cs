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
    /// The firm commitment to sell. Raising or confirming an order never changes
    /// stock; only a reservation holds it and only a delivery takes it out.
    /// </summary>
    public class SalesOrderManagementService : ISalesOrderManagementService
    {
        private readonly IInventoryUnitOfWork _salesOrderUnitOfWork;
        private readonly IPriceListManagementService _priceListManagementService;
        private readonly IDiscountRuleManagementService _discountRuleManagementService;

        public SalesOrderManagementService(IInventoryUnitOfWork unitOfWork,
            IPriceListManagementService priceListManagementService,
            IDiscountRuleManagementService discountRuleManagementService)
        {
            _salesOrderUnitOfWork = unitOfWork;
            _priceListManagementService = priceListManagementService;
            _discountRuleManagementService = discountRuleManagementService;
        }

        public async Task<Guid> CreateSalesOrderAsync(SalesOrder salesOrder)
        {
            await _salesOrderUnitOfWork.BeginTransactionAsync();

            try
            {
                var context = await ValidateAsync(salesOrder, null);
                var salesOrderId = Guid.NewGuid();

                var priced = await PriceLinesAsync(context, salesOrderId, null);

                var header = new SalesOrder
                {
                    Id = salesOrderId,
                    SalesOrderNo = await GenerateSalesOrderNoAsync(),
                    CustomerId = context.Customer.Id,
                    OrderDate = context.PricingDate,
                    ExpectedDeliveryDate = salesOrder.ExpectedDeliveryDate,
                    BusinessLocationId = context.Warehouse.Id,
                    SalespersonId = context.SalespersonId,
                    PaymentTermId = context.PaymentTermId,
                    SalesQuotationId = context.Quotation?.Id,
                    CustomerReference = salesOrder.CustomerReference ?? string.Empty,
                    Notes = salesOrder.Notes ?? string.Empty,
                    Status = SalesOrderStatus.Draft,
                    SubTotal = priced.SubTotal,
                    ItemDiscountTotal = priced.ItemDiscountTotal,
                    TotalTax = priced.TotalTax,
                    OtherCharges = context.OtherCharges,
                    GrandTotal = priced.GrandTotal + context.OtherCharges,
                    CreatedBy = salesOrder.CreatedBy ?? string.Empty,
                    Created = DateTime.Now,
                    Updated = DateTime.Now,
                    SalesOrderItems = priced.Lines
                };

                await _salesOrderUnitOfWork.SalesOrderRepository.AddAsync(header);

                // The quotation only learns about the order once the order exists, and
                // both moves live in the same transaction.
                if (context.Quotation != null)
                {
                    await ApplyQuotationConsumptionAsync(context.Quotation, priced.Lines, add: true);
                }

                await _salesOrderUnitOfWork.CommitTransactionAsync();

                return salesOrderId;
            }
            catch (InvalidOperationException)
            {
                await _salesOrderUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _salesOrderUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<bool> UpdateSalesOrderAsync(SalesOrder salesOrder)
        {
            await _salesOrderUnitOfWork.BeginTransactionAsync();

            try
            {
                var existing = await _salesOrderUnitOfWork
                    .SalesOrderRepository
                    .GetSalesOrderByIdAsync(salesOrder.Id)
                    ?? throw new InvalidOperationException("Sales order not found.");

                GuardAgainstLockedStatus(existing.Status, "edited");
                await GuardAgainstDeliveriesAsync(existing.Id, "edited");
                await GuardAgainstReservationsAsync(existing.Id, "edited");

                // Customer, warehouse and where the order came from are what the order
                // is, so an edit may change the lines but never any of those.
                salesOrder.CustomerId = existing.CustomerId;
                salesOrder.BusinessLocationId = existing.BusinessLocationId;
                salesOrder.SalesQuotationId = existing.SalesQuotationId;

                var context = await ValidateAsync(salesOrder, existing);
                var oldLines = existing.SalesOrderItems?.ToList() ?? new List<SalesOrderItem>();

                // Whatever this order was holding on the quotation is handed back before
                // the new lines claim their share.
                if (context.Quotation != null)
                {
                    await ApplyQuotationConsumptionAsync(context.Quotation, oldLines, add: false);
                }

                await _salesOrderUnitOfWork.SalesOrderItemRepository.RemoveRangeAsync(oldLines);

                var priced = await PriceLinesAsync(context, existing.Id, oldLines);

                foreach (var line in priced.Lines)
                {
                    await _salesOrderUnitOfWork.SalesOrderItemRepository.AddAsync(line);
                }

                if (context.Quotation != null)
                {
                    await ApplyQuotationConsumptionAsync(context.Quotation, priced.Lines, add: true);
                }

                // The sales order number is issued once and never changes.
                existing.OrderDate = context.PricingDate;
                existing.ExpectedDeliveryDate = salesOrder.ExpectedDeliveryDate;
                existing.SalespersonId = context.SalespersonId;
                existing.PaymentTermId = context.PaymentTermId;
                existing.CustomerReference = salesOrder.CustomerReference ?? string.Empty;
                existing.Notes = salesOrder.Notes ?? string.Empty;
                existing.SubTotal = priced.SubTotal;
                existing.ItemDiscountTotal = priced.ItemDiscountTotal;
                existing.TotalTax = priced.TotalTax;
                existing.OtherCharges = context.OtherCharges;
                existing.GrandTotal = priced.GrandTotal + context.OtherCharges;
                existing.Updated = DateTime.Now;

                await _salesOrderUnitOfWork.SalesOrderRepository.EditAsync(existing);
                await _salesOrderUnitOfWork.CommitTransactionAsync();

                return true;
            }
            catch (InvalidOperationException)
            {
                await _salesOrderUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _salesOrderUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<bool> DeleteSalesOrderAsync(Guid id)
        {
            await _salesOrderUnitOfWork.BeginTransactionAsync();

            try
            {
                var salesOrder = await _salesOrderUnitOfWork
                    .SalesOrderRepository
                    .GetSalesOrderByIdAsync(id)
                    ?? throw new InvalidOperationException("Sales order not found.");

                GuardAgainstLockedStatus(salesOrder.Status, "deleted");
                await GuardAgainstDeliveriesAsync(salesOrder.Id, "deleted");
                await GuardAgainstReservationsAsync(salesOrder.Id, "deleted");
                await GuardAgainstInvoicesAsync(salesOrder.Id, "deleted");

                var lines = salesOrder.SalesOrderItems?.ToList() ?? new List<SalesOrderItem>();

                // The quotation gets its quantity back, otherwise it would stay blocked
                // by an order that no longer exists.
                if (salesOrder.SalesQuotationId.HasValue)
                {
                    var quotation = await _salesOrderUnitOfWork
                        .SalesQuotationRepository
                        .GetSalesQuotationByIdAsync(salesOrder.SalesQuotationId.Value);

                    if (quotation != null)
                    {
                        await ApplyQuotationConsumptionAsync(quotation, lines, add: false);
                    }
                }

                await _salesOrderUnitOfWork.SalesOrderItemRepository.RemoveRangeAsync(lines);
                await _salesOrderUnitOfWork.SalesOrderRepository.RemoveAsync(salesOrder);

                await _salesOrderUnitOfWork.CommitTransactionAsync();

                return true;
            }
            catch (InvalidOperationException)
            {
                await _salesOrderUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _salesOrderUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<SalesOrder?> GetSalesOrderByIdAsync(Guid id)
        {
            return await _salesOrderUnitOfWork.SalesOrderRepository.GetSalesOrderByIdAsync(id);
        }

        public async Task<(IList<SalesOrder> data, int total, int totalDisplay)> GetSalesOrdersAsync(int pageIndex,
            int pageSize, DataTablesSearch search, string? order)
        {
            return await _salesOrderUnitOfWork
                .SalesOrderRepository
                .GetPagedSalesOrdersAsync(pageIndex, pageSize, search, order);
        }

        public async Task<IList<SalesOrder>> GetSalesOrdersByStatusAsync(params SalesOrderStatus[] statuses)
        {
            return await _salesOrderUnitOfWork.SalesOrderRepository.GetSalesOrdersByStatusAsync(statuses);
        }

        public async Task<string> GenerateSalesOrderNoAsync()
        {
            var count = await _salesOrderUnitOfWork.SalesOrderRepository.GetCountAsync();

            string salesOrderNo;
            do
            {
                count++;
                salesOrderNo = $"SO-{DateTime.Now:yyyyMM}-{count:D4}";
            }
            while (await _salesOrderUnitOfWork.SalesOrderRepository.IsSalesOrderNoDuplicateAsync(salesOrderNo));

            return salesOrderNo;
        }

        public async Task ConfirmSalesOrderAsync(Guid id)
        {
            await _salesOrderUnitOfWork.BeginTransactionAsync();

            try
            {
                var salesOrder = await GetOrderAsync(id);

                if (salesOrder.Status != SalesOrderStatus.Draft)
                {
                    throw new InvalidOperationException(
                        $"Only a draft sales order can be confirmed, this one is {salesOrder.Status.ToString().ToLower()}.");
                }

                if (salesOrder.SalesOrderItems == null || salesOrder.SalesOrderItems.Count == 0)
                {
                    throw new InvalidOperationException("A sales order without any product line cannot be confirmed.");
                }

                await GuardCreditLimitAsync(salesOrder);

                salesOrder.Status = SalesOrderStatus.Confirmed;
                salesOrder.Updated = DateTime.Now;

                await _salesOrderUnitOfWork.SalesOrderRepository.EditAsync(salesOrder);

                // Taking the offer up closes it, so the same quotation cannot be worked
                // twice once every line has been ordered.
                if (salesOrder.SalesQuotationId.HasValue)
                {
                    await MarkQuotationConvertedAsync(salesOrder.SalesQuotationId.Value);
                }

                await _salesOrderUnitOfWork.CommitTransactionAsync();
            }
            catch (InvalidOperationException)
            {
                await _salesOrderUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _salesOrderUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task CancelSalesOrderAsync(Guid id)
        {
            await _salesOrderUnitOfWork.BeginTransactionAsync();

            try
            {
                var salesOrder = await GetOrderAsync(id);

                if (salesOrder.Status is SalesOrderStatus.Cancelled or SalesOrderStatus.Closed)
                {
                    throw new InvalidOperationException("This sales order is already closed.");
                }

                await GuardAgainstDeliveriesAsync(salesOrder.Id, "cancelled");
                await GuardAgainstInvoicesAsync(salesOrder.Id, "cancelled");

                // Nothing is being sold any more, so every hold it still has goes back
                // to free stock in the same transaction.
                await ReleaseReservationsAsync(salesOrder.Id);

                salesOrder.Status = SalesOrderStatus.Cancelled;
                salesOrder.Updated = DateTime.Now;

                await _salesOrderUnitOfWork.SalesOrderRepository.EditAsync(salesOrder);
                await _salesOrderUnitOfWork.CommitTransactionAsync();
            }
            catch (InvalidOperationException)
            {
                await _salesOrderUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _salesOrderUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task CloseSalesOrderAsync(Guid id)
        {
            await _salesOrderUnitOfWork.BeginTransactionAsync();

            try
            {
                var salesOrder = await GetOrderAsync(id);

                if (salesOrder.Status is not (SalesOrderStatus.Confirmed or SalesOrderStatus.PartiallyDelivered))
                {
                    throw new InvalidOperationException(
                        "Only a confirmed or partly delivered sales order can be closed short.");
                }

                // The rest is never going out, so its hold is given back.
                await ReleaseReservationsAsync(salesOrder.Id);

                salesOrder.Status = SalesOrderStatus.Closed;
                salesOrder.Updated = DateTime.Now;

                await _salesOrderUnitOfWork.SalesOrderRepository.EditAsync(salesOrder);
                await _salesOrderUnitOfWork.CommitTransactionAsync();
            }
            catch (InvalidOperationException)
            {
                await _salesOrderUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _salesOrderUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<SalesLinePreviewDto?> GetLinePreviewAsync(Guid productId, decimal quantity,
            decimal unitPrice, decimal discountAmount)
        {
            if (productId == Guid.Empty || quantity <= 0)
            {
                return null;
            }

            var product = await _salesOrderUnitOfWork.ProductRepository.GetByIdAsync(productId);

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
        private async Task<OrderContext> ValidateAsync(SalesOrder salesOrder, SalesOrder? existing)
        {
            var customer = await _salesOrderUnitOfWork
                .CustomerRepository
                .GetByIdAsync(salesOrder.CustomerId)
                ?? throw new InvalidOperationException("Customer not found.");

            // An existing order keeps the customer it was raised for, so a customer that
            // went inactive later does not block an edit of that order.
            if (existing == null && customer.Status != CustomerStatus.Active)
            {
                throw new InvalidOperationException(
                    $"'{customer.CustomerName}' is inactive, so no sales order can be raised for them.");
            }

            var warehouse = await _salesOrderUnitOfWork
                .BusinessLocationRepository
                .GetByIdAsync(salesOrder.BusinessLocationId)
                ?? throw new InvalidOperationException("Warehouse not found.");

            if (existing == null && !warehouse.IsActive)
            {
                throw new InvalidOperationException(
                    $"'{warehouse.LocationName}' is inactive, so nothing can be sold out of it.");
            }

            var orderDate = salesOrder.OrderDate == default ? DateTime.Now : salesOrder.OrderDate;

            if (salesOrder.ExpectedDeliveryDate.Date < orderDate.Date)
            {
                throw new InvalidOperationException("Expected Delivery Date cannot be earlier than the order date.");
            }

            if (salesOrder.OtherCharges < 0)
            {
                throw new InvalidOperationException("Other charges cannot be negative.");
            }

            var paymentTermId = await ResolvePaymentTermIdAsync(salesOrder.PaymentTermId);
            var salespersonId = await ResolveSalespersonIdAsync(salesOrder.SalespersonId, existing == null);
            var quotation = await ResolveQuotationAsync(salesOrder.SalesQuotationId, customer, warehouse);

            var grouped = salesOrder.SalesOrderItems?
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

            return new OrderContext(customer, warehouse, quotation, grouped, orderDate, paymentTermId,
                salespersonId, Round(salesOrder.OtherCharges), existing);
        }

        /// <summary>
        /// Turns the requested lines into stored lines. This is the single place where
        /// the money on a sales order is decided.
        /// </summary>
        private async Task<PricedResult> PriceLinesAsync(OrderContext context, Guid salesOrderId,
            IList<SalesOrderItem>? oldLines)
        {
            decimal subTotal = 0m;
            decimal itemDiscountTotal = 0m;
            decimal totalTax = 0m;

            var lines = new List<SalesOrderItem>();

            foreach (var requested in context.RequestedItems)
            {
                var product = await _salesOrderUnitOfWork
                    .ProductRepository
                    .GetByIdAsync(requested.ProductId)
                    ?? throw new InvalidOperationException("Product not found.");

                if (product.Status != ProductStatus.Active)
                {
                    throw new InvalidOperationException($"'{product.ProductName}' is inactive and cannot be sold.");
                }

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

                GuardAgainstQuotationOverrun(context.Quotation, context.Existing, product, requested.Quantity);

                // Nothing typed in means the customer's own price list decides.
                var unitPrice = requested.UnitPrice > 0
                    ? requested.UnitPrice
                    : await _priceListManagementService.GetEffectiveUnitPriceAsync(
                        product.Id, context.Customer.CustomerType, requested.Quantity, context.PricingDate);

                var lineAmount = Round(unitPrice * requested.Quantity);

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

                // What has already shipped or been billed is a fact about the past and
                // survives a re-price.
                var old = oldLines?.FirstOrDefault(x => x.ProductId == product.Id);

                if (old != null && requested.Quantity < old.DeliveredQuantity)
                {
                    throw new InvalidOperationException(
                        $"'{product.ProductName}' already has {old.DeliveredQuantity:N2} delivered, " +
                        $"so the ordered quantity cannot be cut to {requested.Quantity:N2}.");
                }

                lines.Add(new SalesOrderItem
                {
                    Id = Guid.NewGuid(),
                    SalesOrderId = salesOrderId,
                    ProductId = product.Id,
                    Quantity = requested.Quantity,
                    UnitPrice = unitPrice,
                    DiscountAmount = discountAmount,
                    TaxRate = taxRate,
                    TaxAmount = taxAmount,
                    LineTotal = taxableAmount + taxAmount,
                    DeliveredQuantity = old?.DeliveredQuantity ?? 0m,
                    InvoicedQuantity = old?.InvoicedQuantity ?? 0m
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

        /// <summary>
        /// An order may only name a quotation the customer has accepted, and only that
        /// customer's own, so the price on the order stays tied to the offer behind it.
        /// </summary>
        private async Task<SalesQuotation?> ResolveQuotationAsync(Guid? salesQuotationId, Customer customer,
            BusinessLocation warehouse)
        {
            if (!salesQuotationId.HasValue || salesQuotationId.Value == Guid.Empty)
            {
                return null;
            }

            var quotation = await _salesOrderUnitOfWork
                .SalesQuotationRepository
                .GetSalesQuotationByIdAsync(salesQuotationId.Value)
                ?? throw new InvalidOperationException("Sales quotation not found.");

            if (quotation.Status is not (SalesQuotationStatus.Accepted or SalesQuotationStatus.Converted))
            {
                throw new InvalidOperationException(
                    "Only a quotation the customer has accepted can be turned into a sales order.");
            }

            if (quotation.CustomerId != customer.Id)
            {
                throw new InvalidOperationException(
                    $"Quotation '{quotation.QuotationNo}' was issued to another customer, " +
                    $"so '{customer.CustomerName}' cannot order on it.");
            }

            if (quotation.BusinessLocationId != warehouse.Id)
            {
                throw new InvalidOperationException(
                    "The sales order must be served from the same warehouse the quotation was issued from.");
            }

            return quotation;
        }

        /// <summary>
        /// A sales order built from a quotation may never order more of a product than
        /// the quotation still has open. What this very order already holds is added
        /// back first, so an edit is measured against the right figure.
        /// </summary>
        private static void GuardAgainstQuotationOverrun(SalesQuotation? quotation, SalesOrder? existing,
            Product product, decimal quantity)
        {
            if (quotation == null)
            {
                return;
            }

            var quotationLine = quotation.SalesQuotationItems?
                .FirstOrDefault(x => x.ProductId == product.Id)
                ?? throw new InvalidOperationException(
                    $"'{product.ProductName}' is not part of quotation '{quotation.QuotationNo}'.");

            var heldByThisOrder = existing?.SalesOrderItems?
                .Where(x => x.ProductId == product.Id)
                .Sum(x => x.Quantity) ?? 0m;

            var available = quotationLine.Quantity - quotationLine.OrderedQuantity + heldByThisOrder;

            if (quantity > available)
            {
                throw new InvalidOperationException(
                    $"'{product.ProductName}' has only {available:N2} left on quotation " +
                    $"'{quotation.QuotationNo}', but {quantity:N2} is being ordered.");
            }
        }

        /// <summary>
        /// Moves the ordered quantity on the source quotation. The quotation's own
        /// status is only closed on confirmation, because a draft order is not yet a
        /// sale.
        /// </summary>
        private async Task ApplyQuotationConsumptionAsync(SalesQuotation quotation, IEnumerable<SalesOrderItem> lines,
            bool add)
        {
            foreach (var line in lines)
            {
                var quotationLine = quotation.SalesQuotationItems?
                    .FirstOrDefault(x => x.ProductId == line.ProductId);

                if (quotationLine == null)
                {
                    continue;
                }

                quotationLine.OrderedQuantity += add ? line.Quantity : -line.Quantity;

                if (quotationLine.OrderedQuantity < 0)
                {
                    quotationLine.OrderedQuantity = 0;
                }

                await _salesOrderUnitOfWork.SalesQuotationItemRepository.EditAsync(quotationLine);
            }

            quotation.Updated = DateTime.Now;

            await _salesOrderUnitOfWork.SalesQuotationRepository.EditAsync(quotation);
        }

        /// <summary>
        /// Re-reads the quotation from its lines once an order on it is confirmed. It
        /// only closes when every line has been fully taken up.
        /// </summary>
        private async Task MarkQuotationConvertedAsync(Guid salesQuotationId)
        {
            var quotation = await _salesOrderUnitOfWork
                .SalesQuotationRepository
                .GetSalesQuotationByIdAsync(salesQuotationId);

            if (quotation == null)
            {
                return;
            }

            var items = quotation.SalesQuotationItems ?? new List<SalesQuotationItem>();

            if (items.Count > 0 && items.All(x => x.OrderedQuantity >= x.Quantity))
            {
                quotation.Status = SalesQuotationStatus.Converted;
                quotation.Updated = DateTime.Now;

                await _salesOrderUnitOfWork.SalesQuotationRepository.EditAsync(quotation);
            }
        }

        /// <summary>
        /// The order is only committed to if it keeps the customer inside their credit
        /// limit. A limit of zero means no limit was set, which is not the same as a
        /// limit of nothing.
        /// </summary>
        private async Task GuardCreditLimitAsync(SalesOrder salesOrder)
        {
            var customer = await _salesOrderUnitOfWork
                .CustomerRepository
                .GetByIdAsync(salesOrder.CustomerId)
                ?? throw new InvalidOperationException("Customer not found.");

            if (customer.Status != CustomerStatus.Active)
            {
                throw new InvalidOperationException(
                    $"'{customer.CustomerName}' is inactive, so this order cannot be confirmed.");
            }

            if (customer.CreditLimit <= 0)
            {
                return;
            }

            var exposure = customer.CurrentOutstanding + salesOrder.GrandTotal;

            if (exposure > customer.CreditLimit)
            {
                throw new InvalidOperationException(
                    $"'{customer.CustomerName}' would be at {exposure:N2} against a credit limit of " +
                    $"{customer.CreditLimit:N2}. Collect against the open invoices first, or raise the limit.");
            }
        }

        /// <summary>
        /// Hands back every hold the order still has. Only the part that has not gone
        /// out yet is given back, because what shipped was already released with the
        /// goods.
        /// </summary>
        private async Task ReleaseReservationsAsync(Guid salesOrderId)
        {
            var reservations = (await _salesOrderUnitOfWork
                .StockReservationRepository
                .GetStockReservationsBySalesOrderAsync(salesOrderId))
                .Where(x => x.Status is StockReservationStatus.Reserved or StockReservationStatus.Draft)
                .ToList();

            foreach (var reservation in reservations)
            {
                if (reservation.Status == StockReservationStatus.Reserved)
                {
                    foreach (var item in reservation.StockReservationItems ?? new List<StockReservationItem>())
                    {
                        var outstanding = item.ReservedQuantity - item.ConsumedQuantity;

                        if (outstanding <= 0)
                        {
                            continue;
                        }

                        var product = await _salesOrderUnitOfWork.ProductRepository.GetByIdAsync(item.ProductId);

                        if (product == null)
                        {
                            continue;
                        }

                        var quantity = (int)outstanding;

                        product.ReservedStock -= Math.Min(product.ReservedStock, quantity);

                        await _salesOrderUnitOfWork.ProductRepository.EditAsync(product);
                    }
                }

                reservation.Status = StockReservationStatus.Released;
                reservation.Updated = DateTime.Now;

                await _salesOrderUnitOfWork.StockReservationRepository.EditAsync(reservation);
            }
        }

        private async Task<Guid?> ResolvePaymentTermIdAsync(Guid? paymentTermId)
        {
            if (!paymentTermId.HasValue || paymentTermId.Value == Guid.Empty)
            {
                return null;
            }

            var paymentTerm = await _salesOrderUnitOfWork
                .PaymentTermRepository
                .GetByIdAsync(paymentTermId.Value)
                ?? throw new InvalidOperationException("Payment term not found.");

            if (!paymentTerm.IsActive)
            {
                throw new InvalidOperationException($"'{paymentTerm.TermName}' is inactive and cannot be used.");
            }

            return paymentTerm.Id;
        }

        private async Task<Guid?> ResolveSalespersonIdAsync(Guid? salespersonId, bool mustBeActive)
        {
            if (!salespersonId.HasValue || salespersonId.Value == Guid.Empty)
            {
                return null;
            }

            var salesperson = await _salesOrderUnitOfWork
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
        /// A confirmed order has been promised to the customer, and everything past it
        /// has already moved goods or money.
        /// </summary>
        private static void GuardAgainstLockedStatus(SalesOrderStatus status, string action)
        {
            if (status is SalesOrderStatus.PartiallyDelivered or SalesOrderStatus.Delivered)
            {
                throw new InvalidOperationException($"A delivered sales order cannot be {action}.");
            }

            if (status == SalesOrderStatus.Confirmed)
            {
                throw new InvalidOperationException(
                    $"A confirmed sales order cannot be {action}. Cancel it and raise a new one instead.");
            }

            if (status == SalesOrderStatus.Cancelled)
            {
                throw new InvalidOperationException($"A cancelled sales order cannot be {action}.");
            }

            if (status == SalesOrderStatus.Closed)
            {
                throw new InvalidOperationException($"A closed sales order cannot be {action}.");
            }
        }

        /// <summary>
        /// Even a draft delivery states what the customer has been promised, so the
        /// order behind it stays as it is until that delivery is cancelled.
        /// </summary>
        private async Task GuardAgainstDeliveriesAsync(Guid salesOrderId, string action)
        {
            var deliveries = await _salesOrderUnitOfWork
                .DeliveryRepository
                .GetDeliveriesBySalesOrderAsync(salesOrderId);

            if (deliveries.Any(x => x.Status != DeliveryStatus.Cancelled))
            {
                throw new InvalidOperationException(
                    $"A sales order that already has a delivery cannot be {action}.");
            }
        }

        private async Task GuardAgainstReservationsAsync(Guid salesOrderId, string action)
        {
            var reservations = await _salesOrderUnitOfWork
                .StockReservationRepository
                .GetStockReservationsBySalesOrderAsync(salesOrderId);

            if (reservations.Any(x => x.Status is StockReservationStatus.Draft or StockReservationStatus.Reserved))
            {
                throw new InvalidOperationException(
                    $"A sales order with stock still reserved against it cannot be {action}. Release the reservation first.");
            }
        }

        private async Task GuardAgainstInvoicesAsync(Guid salesOrderId, string action)
        {
            var invoices = await _salesOrderUnitOfWork
                .SalesInvoiceRepository
                .GetSalesInvoicesBySalesOrderAsync(salesOrderId);

            if (invoices.Count > 0)
            {
                throw new InvalidOperationException(
                    $"A sales order that has already been invoiced cannot be {action}.");
            }
        }

        private async Task<decimal> GetTaxRateAsync(Guid? applicableTaxId)
        {
            if (!applicableTaxId.HasValue || applicableTaxId.Value == Guid.Empty)
            {
                return 0m;
            }

            var applicableTax = await _salesOrderUnitOfWork
                .ApplicableTaxRepository
                .GetByIdAsync(applicableTaxId.Value);

            return applicableTax?.TaxRate ?? 0m;
        }

        private async Task<SalesOrder> GetOrderAsync(Guid id)
        {
            return await _salesOrderUnitOfWork
                .SalesOrderRepository
                .GetSalesOrderByIdAsync(id)
                ?? throw new InvalidOperationException("Sales order not found.");
        }

        private static decimal Round(decimal value)
        {
            return Math.Round(value, 2, MidpointRounding.AwayFromZero);
        }

        private sealed record RequestedLine(Guid ProductId, decimal Quantity, decimal UnitPrice, decimal DiscountAmount);

        private sealed record OrderContext(
            Customer Customer,
            BusinessLocation Warehouse,
            SalesQuotation? Quotation,
            IList<RequestedLine> RequestedItems,
            DateTime PricingDate,
            Guid? PaymentTermId,
            Guid? SalespersonId,
            decimal OtherCharges,
            SalesOrder? Existing);

        private sealed record PricedResult(
            List<SalesOrderItem> Lines,
            decimal SubTotal,
            decimal ItemDiscountTotal,
            decimal TotalTax,
            decimal GrandTotal);
    }
}
