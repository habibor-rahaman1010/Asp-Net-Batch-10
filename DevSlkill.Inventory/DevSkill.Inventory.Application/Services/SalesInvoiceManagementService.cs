using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Domain.UnitOfWorkContracts;

namespace DevSkill.Inventory.Application.Services
{
    /// <summary>
    /// Billing the customer for goods that have already left the warehouse. Posting
    /// an invoice is the only thing that raises what the customer owes; it never
    /// touches stock, because the delivery has already done that.
    /// </summary>
    public class SalesInvoiceManagementService : ISalesInvoiceManagementService
    {
        private readonly IInventoryUnitOfWork _salesInvoiceUnitOfWork;

        /// <summary>An order may only be billed while goods are actually going out against it.</summary>
        private static readonly SalesOrderStatus[] BillableOrderStatuses =
        {
            SalesOrderStatus.PartiallyDelivered,
            SalesOrderStatus.Delivered,
            SalesOrderStatus.Closed
        };

        public SalesInvoiceManagementService(IInventoryUnitOfWork unitOfWork)
        {
            _salesInvoiceUnitOfWork = unitOfWork;
        }

        public async Task<Guid> CreateSalesInvoiceAsync(SalesInvoice salesInvoice, bool postImmediately)
        {
            await _salesInvoiceUnitOfWork.BeginTransactionAsync();

            try
            {
                var context = await ValidateAsync(salesInvoice, null);
                var invoiceId = Guid.NewGuid();

                var lines = BuildLines(context, invoiceId);
                var totals = Summarise(lines, context.OtherCharges);

                var header = new SalesInvoice
                {
                    Id = invoiceId,
                    InvoiceNo = await GenerateInvoiceNoAsync(),
                    CustomerId = context.SalesOrder.CustomerId,
                    SalesOrderId = context.SalesOrder.Id,
                    DeliveryId = context.Delivery?.Id,
                    BusinessLocationId = context.SalesOrder.BusinessLocationId,
                    SalespersonId = context.SalesOrder.SalespersonId,
                    InvoiceDate = context.InvoiceDate,
                    DueDate = context.DueDate,
                    PaymentTermId = context.PaymentTermId,
                    Notes = salesInvoice.Notes ?? string.Empty,
                    Status = SalesInvoiceStatus.Draft,
                    SubTotal = totals.SubTotal,
                    ItemDiscountTotal = totals.ItemDiscountTotal,
                    TotalTax = totals.TotalTax,
                    OtherCharges = context.OtherCharges,
                    GrandTotal = totals.GrandTotal,
                    PaidAmount = 0m,
                    DueAmount = totals.GrandTotal,
                    CreatedBy = salesInvoice.CreatedBy ?? string.Empty,
                    Created = DateTime.Now,
                    Updated = DateTime.Now,
                    SalesInvoiceItems = lines
                };

                await _salesInvoiceUnitOfWork.SalesInvoiceRepository.AddAsync(header);

                if (postImmediately)
                {
                    // The header is still an unwritten insert here, so posting only sets
                    // the status on it. Marking it modified would turn that insert into an
                    // update of a row that does not exist yet, and the lines and the
                    // commission would then be written against a missing invoice.
                    header.Status = SalesInvoiceStatus.Posted;

                    await ApplyPostingAsync(header, context.SalesOrder);
                }

                await _salesInvoiceUnitOfWork.CommitTransactionAsync();

                return invoiceId;
            }
            catch (InvalidOperationException)
            {
                await _salesInvoiceUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _salesInvoiceUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<bool> UpdateSalesInvoiceAsync(SalesInvoice salesInvoice)
        {
            await _salesInvoiceUnitOfWork.BeginTransactionAsync();

            try
            {
                var existing = await GetInvoiceAsync(salesInvoice.Id);

                // A posted invoice is a claim already made on the customer. Correcting it
                // is a credit note, never a quiet edit.
                if (existing.Status != SalesInvoiceStatus.Draft)
                {
                    throw new InvalidOperationException(
                        $"Only a draft invoice can be edited, this one is {existing.Status.ToString().ToLower()}.");
                }

                // The order and the shipment behind an invoice are what the invoice is.
                salesInvoice.SalesOrderId = existing.SalesOrderId;
                salesInvoice.DeliveryId = existing.DeliveryId;

                var context = await ValidateAsync(salesInvoice, existing);
                var oldLines = existing.SalesInvoiceItems?.ToList() ?? new List<SalesInvoiceItem>();

                await _salesInvoiceUnitOfWork.SalesInvoiceItemRepository.RemoveRangeAsync(oldLines);

                var lines = BuildLines(context, existing.Id);

                foreach (var line in lines)
                {
                    await _salesInvoiceUnitOfWork.SalesInvoiceItemRepository.AddAsync(line);
                }

                var totals = Summarise(lines, context.OtherCharges);

                // The invoice number is issued once and never changes.
                existing.InvoiceDate = context.InvoiceDate;
                existing.DueDate = context.DueDate;
                existing.PaymentTermId = context.PaymentTermId;
                existing.Notes = salesInvoice.Notes ?? string.Empty;
                existing.SubTotal = totals.SubTotal;
                existing.ItemDiscountTotal = totals.ItemDiscountTotal;
                existing.TotalTax = totals.TotalTax;
                existing.OtherCharges = context.OtherCharges;
                existing.GrandTotal = totals.GrandTotal;
                existing.DueAmount = Round(totals.GrandTotal - existing.PaidAmount);
                existing.Updated = DateTime.Now;

                await _salesInvoiceUnitOfWork.SalesInvoiceRepository.EditAsync(existing);
                await _salesInvoiceUnitOfWork.CommitTransactionAsync();

                return true;
            }
            catch (InvalidOperationException)
            {
                await _salesInvoiceUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _salesInvoiceUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<bool> DeleteSalesInvoiceAsync(Guid id)
        {
            await _salesInvoiceUnitOfWork.BeginTransactionAsync();

            try
            {
                var invoice = await GetInvoiceAsync(id);

                if (invoice.Status != SalesInvoiceStatus.Draft)
                {
                    throw new InvalidOperationException(
                        "Only a draft invoice can be deleted. Cancel the posted one instead, so the customer account is put right.");
                }

                var lines = invoice.SalesInvoiceItems?.ToList() ?? new List<SalesInvoiceItem>();

                await _salesInvoiceUnitOfWork.SalesInvoiceItemRepository.RemoveRangeAsync(lines);
                await _salesInvoiceUnitOfWork.SalesInvoiceRepository.RemoveAsync(invoice);
                await _salesInvoiceUnitOfWork.CommitTransactionAsync();

                return true;
            }
            catch (InvalidOperationException)
            {
                await _salesInvoiceUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _salesInvoiceUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<bool> PostSalesInvoiceAsync(Guid id)
        {
            await _salesInvoiceUnitOfWork.BeginTransactionAsync();

            try
            {
                var invoice = await GetInvoiceAsync(id);

                if (invoice.Status != SalesInvoiceStatus.Draft)
                {
                    throw new InvalidOperationException(
                        $"Only a draft invoice can be posted, this one is {invoice.Status.ToString().ToLower()}.");
                }

                if (invoice.SalesInvoiceItems == null || invoice.SalesInvoiceItems.Count == 0)
                {
                    throw new InvalidOperationException("An invoice without any product line cannot be posted.");
                }

                var salesOrder = await _salesInvoiceUnitOfWork
                    .SalesOrderRepository
                    .GetSalesOrderByIdAsync(invoice.SalesOrderId)
                    ?? throw new InvalidOperationException("Sales order not found.");

                await ApplyPostingAsync(invoice, salesOrder);

                invoice.Status = SalesInvoiceStatus.Posted;
                invoice.Updated = DateTime.Now;

                await _salesInvoiceUnitOfWork.SalesInvoiceRepository.EditAsync(invoice);
                await _salesInvoiceUnitOfWork.CommitTransactionAsync();

                return true;
            }
            catch (InvalidOperationException)
            {
                await _salesInvoiceUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _salesInvoiceUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<bool> CancelSalesInvoiceAsync(Guid id)
        {
            await _salesInvoiceUnitOfWork.BeginTransactionAsync();

            try
            {
                var invoice = await GetInvoiceAsync(id);

                if (invoice.Status == SalesInvoiceStatus.Cancelled)
                {
                    throw new InvalidOperationException("This invoice is already cancelled.");
                }

                await GuardAgainstCollectionsAsync(invoice.Id);
                await GuardAgainstReturnsAsync(invoice.Id);

                // A draft never reached the customer account, so there is nothing to undo.
                if (invoice.Status != SalesInvoiceStatus.Draft)
                {
                    var salesOrder = await _salesInvoiceUnitOfWork
                        .SalesOrderRepository
                        .GetSalesOrderByIdAsync(invoice.SalesOrderId);

                    await ReversePostingAsync(invoice, salesOrder);
                }

                invoice.Status = SalesInvoiceStatus.Cancelled;
                invoice.DueAmount = 0m;
                invoice.Updated = DateTime.Now;

                await _salesInvoiceUnitOfWork.SalesInvoiceRepository.EditAsync(invoice);
                await _salesInvoiceUnitOfWork.CommitTransactionAsync();

                return true;
            }
            catch (InvalidOperationException)
            {
                await _salesInvoiceUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _salesInvoiceUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<SalesInvoice?> GetSalesInvoiceByIdAsync(Guid id)
        {
            return await _salesInvoiceUnitOfWork.SalesInvoiceRepository.GetSalesInvoiceByIdAsync(id);
        }

        public async Task<(IList<SalesInvoice> data, int total, int totalDisplay)> GetSalesInvoicesAsync(int pageIndex,
            int pageSize, DataTablesSearch search, string? order)
        {
            return await _salesInvoiceUnitOfWork
                .SalesInvoiceRepository
                .GetPagedSalesInvoicesAsync(pageIndex, pageSize, search, order);
        }

        public async Task<IList<SalesInvoice>> GetSalesInvoicesByStatusAsync(params SalesInvoiceStatus[] statuses)
        {
            return await _salesInvoiceUnitOfWork.SalesInvoiceRepository.GetSalesInvoicesByStatusAsync(statuses);
        }

        public async Task<string> GenerateInvoiceNoAsync()
        {
            var count = await _salesInvoiceUnitOfWork.SalesInvoiceRepository.GetCountAsync();

            string invoiceNo;
            do
            {
                count++;
                invoiceNo = $"SI-{DateTime.Now:yyyyMM}-{count:D4}";
            }
            while (await _salesInvoiceUnitOfWork.SalesInvoiceRepository.IsInvoiceNoDuplicateAsync(invoiceNo));

            return invoiceNo;
        }

        public async Task<IList<BillableSalesLineDto>> GetBillableLinesAsync(Guid salesOrderId,
            Guid? excludeInvoiceId = null)
        {
            var salesOrder = await _salesInvoiceUnitOfWork
                .SalesOrderRepository
                .GetSalesOrderByIdAsync(salesOrderId);

            if (salesOrder?.SalesOrderItems == null)
            {
                return new List<BillableSalesLineDto>();
            }

            var delivered = await GetDeliveredQuantitiesAsync(salesOrderId);
            var billed = await GetBilledQuantitiesAsync(salesOrderId, excludeInvoiceId);
            var lines = new List<BillableSalesLineDto>();

            foreach (var item in salesOrder.SalesOrderItems)
            {
                delivered.TryGetValue(item.ProductId, out var deliveredQuantity);
                billed.TryGetValue(item.ProductId, out var billedQuantity);

                lines.Add(new BillableSalesLineDto
                {
                    ProductId = item.ProductId,
                    ProductName = item.Product?.ProductName ?? string.Empty,
                    UnitName = item.Product?.Unit?.UnitName ?? string.Empty,
                    OrderedQuantity = item.Quantity,
                    DeliveredQuantity = deliveredQuantity,
                    InvoicedQuantity = billedQuantity,
                    RemainingQuantity = Math.Max(0m, Round(deliveredQuantity - billedQuantity)),
                    UnitPrice = item.UnitPrice,
                    DiscountAmount = ProportionalDiscount(item, deliveredQuantity - billedQuantity),
                    TaxRate = item.TaxRate
                });
            }

            return lines;
        }

        public async Task<IList<SalesOrder>> GetBillableSalesOrdersAsync()
        {
            return await _salesInvoiceUnitOfWork
                .SalesOrderRepository
                .GetSalesOrdersByStatusAsync(BillableOrderStatuses);
        }

        public async Task<IList<Delivery>> GetInvoiceableDeliveriesAsync(Guid salesOrderId)
        {
            var deliveries = await _salesInvoiceUnitOfWork
                .DeliveryRepository
                .GetDeliveriesBySalesOrderAsync(salesOrderId);

            return deliveries
                .Where(x => x.Status == DeliveryStatus.Confirmed)
                .OrderBy(x => x.DeliveryDate)
                .ToList();
        }

        /// <summary>
        /// Reads the request and checks it against the order and the shipments behind
        /// it. Prices are never taken from the browser; they come off the sales order.
        /// </summary>
        private async Task<InvoiceContext> ValidateAsync(SalesInvoice salesInvoice, SalesInvoice? existing)
        {
            if (salesInvoice.SalesOrderId == Guid.Empty)
            {
                throw new InvalidOperationException("An invoice has to be raised against a sales order.");
            }

            var salesOrder = await _salesInvoiceUnitOfWork
                .SalesOrderRepository
                .GetSalesOrderByIdAsync(salesInvoice.SalesOrderId)
                ?? throw new InvalidOperationException("Sales order not found.");

            if (salesOrder.Status == SalesOrderStatus.Cancelled)
            {
                throw new InvalidOperationException("A cancelled sales order cannot be invoiced.");
            }

            Delivery? delivery = null;

            if (salesInvoice.DeliveryId.HasValue && salesInvoice.DeliveryId.Value != Guid.Empty)
            {
                delivery = await _salesInvoiceUnitOfWork
                    .DeliveryRepository
                    .GetDeliveryByIdAsync(salesInvoice.DeliveryId.Value)
                    ?? throw new InvalidOperationException("Delivery not found.");

                if (delivery.SalesOrderId != salesOrder.Id)
                {
                    throw new InvalidOperationException("That delivery does not belong to this sales order.");
                }

                if (delivery.Status != DeliveryStatus.Confirmed)
                {
                    throw new InvalidOperationException(
                        $"'{delivery.DeliveryNo}' has not been confirmed yet, so nothing has left the warehouse to invoice.");
                }
            }

            var invoiceDate = salesInvoice.InvoiceDate == default
                ? DateTime.Now.Date
                : salesInvoice.InvoiceDate.Date;

            var paymentTermId = await ResolvePaymentTermIdAsync(salesInvoice.PaymentTermId ?? salesOrder.PaymentTermId);
            var dueDate = await ResolveDueDateAsync(salesInvoice.DueDate, invoiceDate, paymentTermId);

            var requested = (salesInvoice.SalesInvoiceItems ?? new List<SalesInvoiceItem>())
                .Where(x => x.ProductId != Guid.Empty && x.Quantity > 0)
                .GroupBy(x => x.ProductId)
                .Select(x => new RequestedLine(x.Key, Round(x.Sum(l => l.Quantity))))
                .ToList();

            if (requested.Count == 0)
            {
                throw new InvalidOperationException("An invoice needs at least one product line.");
            }

            var orderLines = (salesOrder.SalesOrderItems ?? new List<SalesOrderItem>())
                .GroupBy(x => x.ProductId)
                .ToDictionary(x => x.Key, x => x.First());

            // Only goods that have actually gone out may be billed, and only once. When
            // the invoice names one shipment, that shipment is the ceiling instead.
            var deliverable = delivery == null
                ? await GetDeliveredQuantitiesAsync(salesOrder.Id)
                : GetShipmentQuantities(delivery);

            var billed = await GetBilledQuantitiesAsync(salesOrder.Id, existing?.Id);
            var validated = new List<ValidatedLine>();

            foreach (var line in requested)
            {
                if (!orderLines.TryGetValue(line.ProductId, out var orderLine))
                {
                    throw new InvalidOperationException(
                        "A product that is not on the sales order cannot be invoiced against it.");
                }

                var product = orderLine.Product ?? await _salesInvoiceUnitOfWork
                    .ProductRepository
                    .GetProductByIdAsync(line.ProductId)
                    ?? throw new InvalidOperationException("Product not found.");

                deliverable.TryGetValue(line.ProductId, out var deliveredQuantity);
                billed.TryGetValue(line.ProductId, out var billedQuantity);

                var remaining = Round(deliveredQuantity - billedQuantity);

                if (remaining <= 0)
                {
                    throw new InvalidOperationException(
                        $"'{product.ProductName}' has nothing delivered left to invoice.");
                }

                if (line.Quantity > remaining)
                {
                    throw new InvalidOperationException(
                        $"'{product.ProductName}' has only {remaining:0.##} delivered and not yet invoiced.");
                }

                validated.Add(new ValidatedLine(
                    line.ProductId,
                    line.Quantity,
                    orderLine.UnitPrice,
                    ProportionalDiscount(orderLine, line.Quantity),
                    orderLine.TaxRate));
            }

            var otherCharges = Round(Math.Max(0m, salesInvoice.OtherCharges));

            return new InvoiceContext(salesOrder, delivery, invoiceDate, dueDate, paymentTermId, otherCharges,
                validated);
        }

        private static IList<SalesInvoiceItem> BuildLines(InvoiceContext context, Guid invoiceId)
        {
            return context.Lines
                .Select(line =>
                {
                    var lineAmount = Round(line.Quantity * line.UnitPrice);
                    var netAmount = Round(lineAmount - line.DiscountAmount);
                    var taxAmount = Round(netAmount * line.TaxRate / 100m);

                    return new SalesInvoiceItem
                    {
                        Id = Guid.NewGuid(),
                        SalesInvoiceId = invoiceId,
                        ProductId = line.ProductId,
                        Quantity = line.Quantity,
                        UnitPrice = line.UnitPrice,
                        DiscountAmount = line.DiscountAmount,
                        TaxRate = line.TaxRate,
                        TaxAmount = taxAmount,
                        LineTotal = Round(netAmount + taxAmount),
                        ReturnedQuantity = 0m
                    };
                })
                .ToList();
        }

        private static Totals Summarise(IList<SalesInvoiceItem> lines, decimal otherCharges)
        {
            var subTotal = Round(lines.Sum(x => x.Quantity * x.UnitPrice));
            var discountTotal = Round(lines.Sum(x => x.DiscountAmount));
            var taxTotal = Round(lines.Sum(x => x.TaxAmount));
            var grandTotal = Round(subTotal - discountTotal + taxTotal + otherCharges);

            return new Totals(subTotal, discountTotal, taxTotal, grandTotal);
        }

        /// <summary>
        /// Everything posting an invoice does: the customer owes more, the order knows
        /// what has been billed and the salesperson has earned something.
        /// </summary>
        private async Task ApplyPostingAsync(SalesInvoice invoice, SalesOrder salesOrder)
        {
            await MoveInvoicedQuantitiesAsync(invoice, salesOrder, add: true);
            await MoveOutstandingAsync(invoice.CustomerId, invoice.GrandTotal);
            await EarnCommissionAsync(invoice);
        }

        private async Task ReversePostingAsync(SalesInvoice invoice, SalesOrder? salesOrder)
        {
            if (salesOrder != null)
            {
                await MoveInvoicedQuantitiesAsync(invoice, salesOrder, add: false);
            }

            await MoveOutstandingAsync(invoice.CustomerId, -invoice.GrandTotal);
            await CancelCommissionAsync(invoice.Id);
        }

        private async Task MoveInvoicedQuantitiesAsync(SalesInvoice invoice, SalesOrder salesOrder, bool add)
        {
            var orderLines = (salesOrder.SalesOrderItems ?? new List<SalesOrderItem>())
                .GroupBy(x => x.ProductId)
                .ToDictionary(x => x.Key, x => x.First());

            foreach (var item in invoice.SalesInvoiceItems ?? new List<SalesInvoiceItem>())
            {
                if (!orderLines.TryGetValue(item.ProductId, out var orderLine))
                {
                    continue;
                }

                var moved = add ? item.Quantity : -item.Quantity;

                orderLine.InvoicedQuantity = Math.Max(0m, Round(orderLine.InvoicedQuantity + moved));

                await _salesInvoiceUnitOfWork.SalesOrderItemRepository.EditAsync(orderLine);
            }
        }

        private async Task MoveOutstandingAsync(Guid customerId, decimal amount)
        {
            var customer = await _salesInvoiceUnitOfWork.CustomerRepository.GetByIdAsync(customerId);

            if (customer == null)
            {
                return;
            }

            customer.CurrentOutstanding = Round(customer.CurrentOutstanding + amount);
            customer.Updated = DateTime.Now;

            await _salesInvoiceUnitOfWork.CustomerRepository.EditAsync(customer);
        }

        /// <summary>
        /// Writes the commission the invoice earned. Nothing is written when no
        /// salesperson was named, and an already earned commission is never doubled.
        /// </summary>
        private async Task EarnCommissionAsync(SalesInvoice invoice)
        {
            if (!invoice.SalespersonId.HasValue || invoice.SalespersonId.Value == Guid.Empty)
            {
                return;
            }

            var existing = await _salesInvoiceUnitOfWork
                .SalesCommissionRepository
                .GetSalesCommissionBySalesInvoiceAsync(invoice.Id);

            if (existing != null && existing.Status != CommissionStatus.Cancelled)
            {
                return;
            }

            var salesperson = await _salesInvoiceUnitOfWork
                .SalespersonRepository
                .GetSalespersonByIdAsync(invoice.SalespersonId.Value);

            if (salesperson == null)
            {
                return;
            }

            // The goods are what was sold; freight and the like are not commissionable.
            var basisAmount = Round(invoice.SubTotal - invoice.ItemDiscountTotal);

            var commissionAmount = salesperson.CommissionBasis == CommissionBasis.Percentage
                ? Round(basisAmount * salesperson.CommissionRate / 100m)
                : Round(salesperson.CommissionRate);

            if (commissionAmount <= 0)
            {
                return;
            }

            // A cancelled commission is written again rather than revived, so the
            // history of what was cancelled stays readable.
            if (existing != null)
            {
                await _salesInvoiceUnitOfWork.SalesCommissionRepository.RemoveAsync(existing);
            }

            await _salesInvoiceUnitOfWork.SalesCommissionRepository.AddAsync(new SalesCommission
            {
                Id = Guid.NewGuid(),
                SalespersonId = salesperson.Id,
                SalesInvoiceId = invoice.Id,
                EarnedDate = invoice.InvoiceDate,
                BasisAmount = basisAmount,
                CommissionBasis = salesperson.CommissionBasis,
                CommissionRate = salesperson.CommissionRate,
                CommissionAmount = commissionAmount,
                Status = CommissionStatus.Pending,
                Notes = $"Earned on {invoice.InvoiceNo}.",
                Created = DateTime.Now,
                Updated = DateTime.Now
            });
        }

        private async Task CancelCommissionAsync(Guid salesInvoiceId)
        {
            var commission = await _salesInvoiceUnitOfWork
                .SalesCommissionRepository
                .GetSalesCommissionBySalesInvoiceAsync(salesInvoiceId);

            if (commission == null || commission.Status == CommissionStatus.Cancelled)
            {
                return;
            }

            if (commission.Status == CommissionStatus.Paid)
            {
                throw new InvalidOperationException(
                    "The commission on this invoice has already been paid out, so the invoice cannot be cancelled.");
            }

            commission.Status = CommissionStatus.Cancelled;
            commission.Notes = "Cancelled with the invoice it was earned on.";
            commission.Updated = DateTime.Now;

            await _salesInvoiceUnitOfWork.SalesCommissionRepository.EditAsync(commission);
        }

        /// <summary>Per product, what every confirmed shipment of one order has handed over.</summary>
        private async Task<IDictionary<Guid, decimal>> GetDeliveredQuantitiesAsync(Guid salesOrderId)
        {
            var deliveries = await _salesInvoiceUnitOfWork
                .DeliveryRepository
                .GetDeliveriesBySalesOrderAsync(salesOrderId);

            var totals = new Dictionary<Guid, decimal>();

            foreach (var delivery in deliveries.Where(x => x.Status == DeliveryStatus.Confirmed))
            {
                foreach (var item in delivery.DeliveryItems ?? new List<DeliveryItem>())
                {
                    totals.TryGetValue(item.ProductId, out var current);
                    totals[item.ProductId] = current + item.DeliveredQuantity;
                }
            }

            return totals;
        }

        private static IDictionary<Guid, decimal> GetShipmentQuantities(Delivery delivery)
        {
            var totals = new Dictionary<Guid, decimal>();

            foreach (var item in delivery.DeliveryItems ?? new List<DeliveryItem>())
            {
                totals.TryGetValue(item.ProductId, out var current);
                totals[item.ProductId] = current + item.DeliveredQuantity;
            }

            return totals;
        }

        /// <summary>
        /// Per product, what the other invoices on the same order have already billed.
        /// A cancelled invoice claims nothing, so it does not count.
        /// </summary>
        private async Task<IDictionary<Guid, decimal>> GetBilledQuantitiesAsync(Guid salesOrderId,
            Guid? excludeInvoiceId)
        {
            var invoices = await _salesInvoiceUnitOfWork
                .SalesInvoiceRepository
                .GetSalesInvoicesBySalesOrderAsync(salesOrderId);

            var totals = new Dictionary<Guid, decimal>();

            foreach (var invoice in invoices)
            {
                if (excludeInvoiceId.HasValue && invoice.Id == excludeInvoiceId.Value)
                {
                    continue;
                }

                if (invoice.Status == SalesInvoiceStatus.Cancelled)
                {
                    continue;
                }

                foreach (var item in invoice.SalesInvoiceItems ?? new List<SalesInvoiceItem>())
                {
                    totals.TryGetValue(item.ProductId, out var current);
                    totals[item.ProductId] = current + item.Quantity;
                }
            }

            return totals;
        }

        /// <summary>
        /// The share of a sales order line's discount that belongs to the quantity being
        /// billed, so partial invoices add back up to the order.
        /// </summary>
        private static decimal ProportionalDiscount(SalesOrderItem orderLine, decimal quantity)
        {
            if (orderLine.Quantity <= 0 || quantity <= 0 || orderLine.DiscountAmount <= 0)
            {
                return 0m;
            }

            var share = Math.Min(quantity, orderLine.Quantity) / orderLine.Quantity;

            return Round(orderLine.DiscountAmount * share);
        }

        private async Task GuardAgainstCollectionsAsync(Guid salesInvoiceId)
        {
            var allocations = await _salesInvoiceUnitOfWork
                .CustomerPaymentAllocationRepository
                .GetAllocationsBySalesInvoiceAsync(salesInvoiceId);

            if (allocations.Any(x => x.CustomerPayment == null
                || x.CustomerPayment.Status != CustomerPaymentStatus.Cancelled))
            {
                throw new InvalidOperationException(
                    "Money has already been collected against this invoice, so it cannot be cancelled. Cancel the collection first.");
            }
        }

        private async Task GuardAgainstReturnsAsync(Guid salesInvoiceId)
        {
            var returns = await _salesInvoiceUnitOfWork
                .SalesReturnRepository
                .GetSalesReturnsBySalesInvoiceAsync(salesInvoiceId);

            if (returns.Any(x => x.Status != SalesReturnStatus.Cancelled))
            {
                throw new InvalidOperationException(
                    "Goods have already come back against this invoice, so it cannot be cancelled.");
            }
        }

        private async Task<Guid?> ResolvePaymentTermIdAsync(Guid? paymentTermId)
        {
            if (!paymentTermId.HasValue || paymentTermId.Value == Guid.Empty)
            {
                return null;
            }

            var paymentTerm = await _salesInvoiceUnitOfWork
                .PaymentTermRepository
                .GetByIdAsync(paymentTermId.Value);

            return paymentTerm?.Id;
        }

        /// <summary>
        /// A due date typed in wins; left empty the payment term decides, and with no
        /// term at all the invoice is due the day it is raised.
        /// </summary>
        private async Task<DateTime> ResolveDueDateAsync(DateTime dueDate, DateTime invoiceDate, Guid? paymentTermId)
        {
            if (dueDate != default && dueDate.Date >= invoiceDate)
            {
                return dueDate.Date;
            }

            if (!paymentTermId.HasValue)
            {
                return invoiceDate;
            }

            var paymentTerm = await _salesInvoiceUnitOfWork
                .PaymentTermRepository
                .GetByIdAsync(paymentTermId.Value);

            return invoiceDate.AddDays(paymentTerm?.DueDays ?? 0);
        }

        private async Task<SalesInvoice> GetInvoiceAsync(Guid id)
        {
            return await _salesInvoiceUnitOfWork
                .SalesInvoiceRepository
                .GetSalesInvoiceByIdAsync(id)
                ?? throw new InvalidOperationException("Sales invoice not found.");
        }

        private static decimal Round(decimal value)
        {
            return Math.Round(value, 2, MidpointRounding.AwayFromZero);
        }

        private sealed record RequestedLine(Guid ProductId, decimal Quantity);

        private sealed record ValidatedLine(Guid ProductId, decimal Quantity, decimal UnitPrice,
            decimal DiscountAmount, decimal TaxRate);

        private sealed record Totals(decimal SubTotal, decimal ItemDiscountTotal, decimal TotalTax,
            decimal GrandTotal);

        private sealed record InvoiceContext(SalesOrder SalesOrder, Delivery? Delivery, DateTime InvoiceDate,
            DateTime DueDate, Guid? PaymentTermId, decimal OtherCharges, IList<ValidatedLine> Lines);
    }
}
