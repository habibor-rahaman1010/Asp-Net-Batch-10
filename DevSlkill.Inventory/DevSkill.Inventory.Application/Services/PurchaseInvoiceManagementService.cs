using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Domain.UnitOfWorkContracts;

namespace DevSkill.Inventory.Application.Services
{
    public class PurchaseInvoiceManagementService : IPurchaseInvoiceManagementService
    {
        private readonly IInventoryUnitOfWork _purchaseInvoiceUnitOfWork;

        public PurchaseInvoiceManagementService(IInventoryUnitOfWork unitOfWork)
        {
            _purchaseInvoiceUnitOfWork = unitOfWork;
        }

        public async Task<Guid> CreatePurchaseInvoiceAsync(PurchaseInvoice purchaseInvoice)
        {
            await _purchaseInvoiceUnitOfWork.BeginTransactionAsync();

            try
            {
                var context = await ValidateAsync(purchaseInvoice, null);
                var invoiceId = Guid.NewGuid();

                var priced = PriceLines(context, invoiceId);

                var header = new PurchaseInvoice
                {
                    Id = invoiceId,
                    InvoiceNo = await GenerateInvoiceNoAsync(),
                    SupplierInvoiceNo = purchaseInvoice.SupplierInvoiceNo ?? string.Empty,
                    SupplierId = context.PurchaseOrder.SupplierId,
                    PurchaseOrderId = context.PurchaseOrder.Id,
                    GoodsReceiptId = context.GoodsReceiptId,
                    InvoiceDate = context.InvoiceDate,
                    DueDate = context.DueDate,
                    PaymentTermId = context.PaymentTermId,
                    Notes = purchaseInvoice.Notes ?? string.Empty,
                    Status = PurchaseInvoiceStatus.Draft,
                    SubTotal = priced.SubTotal,
                    ItemDiscountTotal = priced.ItemDiscountTotal,
                    TotalTax = priced.TotalTax,
                    OtherCharges = context.OtherCharges,
                    GrandTotal = priced.GrandTotal + context.OtherCharges,
                    PaidAmount = 0m,
                    DueAmount = priced.GrandTotal + context.OtherCharges,
                    CreatedBy = purchaseInvoice.CreatedBy ?? string.Empty,
                    Created = DateTime.Now,
                    Updated = DateTime.Now,
                    PurchaseInvoiceItems = priced.Lines
                };

                await _purchaseInvoiceUnitOfWork.PurchaseInvoiceRepository.AddAsync(header);

                // A draft owes nothing. Asking for it to be posted straight away runs
                // the posting inside this very transaction.
                if (purchaseInvoice.Status == PurchaseInvoiceStatus.Posted)
                {
                    await ApplyPostingAsync(header, context.PurchaseOrder, priced.Lines);
                }

                await _purchaseInvoiceUnitOfWork.CommitTransactionAsync();

                return invoiceId;
            }
            catch (InvalidOperationException)
            {
                await _purchaseInvoiceUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _purchaseInvoiceUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<bool> UpdatePurchaseInvoiceAsync(PurchaseInvoice purchaseInvoice)
        {
            await _purchaseInvoiceUnitOfWork.BeginTransactionAsync();

            try
            {
                var existing = await _purchaseInvoiceUnitOfWork
                    .PurchaseInvoiceRepository
                    .GetPurchaseInvoiceByIdAsync(purchaseInvoice.Id)
                    ?? throw new InvalidOperationException("Purchase invoice not found.");

                // A posted invoice is on the supplier ledger, so its amounts are
                // history. Cancel it and raise a new one instead.
                if (existing.Status != PurchaseInvoiceStatus.Draft)
                {
                    throw new InvalidOperationException(
                        $"A {existing.Status.ToString().ToLower()} purchase invoice cannot be edited.");
                }

                // The purchase order an invoice belongs to is what the invoice is.
                purchaseInvoice.PurchaseOrderId = existing.PurchaseOrderId;

                var context = await ValidateAsync(purchaseInvoice, existing);

                await _purchaseInvoiceUnitOfWork
                    .PurchaseInvoiceItemRepository
                    .RemoveRangeAsync(existing.PurchaseInvoiceItems?.ToList() ?? new List<PurchaseInvoiceItem>());

                var priced = PriceLines(context, existing.Id);

                foreach (var line in priced.Lines)
                {
                    await _purchaseInvoiceUnitOfWork.PurchaseInvoiceItemRepository.AddAsync(line);
                }

                // The invoice number is issued once and never changes.
                existing.SupplierInvoiceNo = purchaseInvoice.SupplierInvoiceNo ?? string.Empty;
                existing.GoodsReceiptId = context.GoodsReceiptId;
                existing.InvoiceDate = context.InvoiceDate;
                existing.DueDate = context.DueDate;
                existing.PaymentTermId = context.PaymentTermId;
                existing.Notes = purchaseInvoice.Notes ?? string.Empty;
                existing.SubTotal = priced.SubTotal;
                existing.ItemDiscountTotal = priced.ItemDiscountTotal;
                existing.TotalTax = priced.TotalTax;
                existing.OtherCharges = context.OtherCharges;
                existing.GrandTotal = priced.GrandTotal + context.OtherCharges;
                existing.DueAmount = existing.GrandTotal - existing.PaidAmount;
                existing.Updated = DateTime.Now;

                await _purchaseInvoiceUnitOfWork.PurchaseInvoiceRepository.EditAsync(existing);

                if (purchaseInvoice.Status == PurchaseInvoiceStatus.Posted)
                {
                    await ApplyPostingAsync(existing, context.PurchaseOrder, priced.Lines);
                }

                await _purchaseInvoiceUnitOfWork.CommitTransactionAsync();

                return true;
            }
            catch (InvalidOperationException)
            {
                await _purchaseInvoiceUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _purchaseInvoiceUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<bool> DeletePurchaseInvoiceAsync(Guid id)
        {
            await _purchaseInvoiceUnitOfWork.BeginTransactionAsync();

            try
            {
                var invoice = await _purchaseInvoiceUnitOfWork
                    .PurchaseInvoiceRepository
                    .GetPurchaseInvoiceByIdAsync(id)
                    ?? throw new InvalidOperationException("Purchase invoice not found.");

                if (invoice.Status is not (PurchaseInvoiceStatus.Draft or PurchaseInvoiceStatus.Cancelled))
                {
                    throw new InvalidOperationException(
                        "A posted purchase invoice cannot be deleted. Cancel it instead, which reverses the supplier balance.");
                }

                await _purchaseInvoiceUnitOfWork
                    .PurchaseInvoiceItemRepository
                    .RemoveRangeAsync(invoice.PurchaseInvoiceItems?.ToList() ?? new List<PurchaseInvoiceItem>());

                await _purchaseInvoiceUnitOfWork.PurchaseInvoiceRepository.RemoveAsync(invoice);

                await _purchaseInvoiceUnitOfWork.CommitTransactionAsync();

                return true;
            }
            catch (InvalidOperationException)
            {
                await _purchaseInvoiceUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _purchaseInvoiceUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<PurchaseInvoice?> GetPurchaseInvoiceByIdAsync(Guid id)
        {
            return await _purchaseInvoiceUnitOfWork.PurchaseInvoiceRepository.GetPurchaseInvoiceByIdAsync(id);
        }

        public async Task<(IList<PurchaseInvoice> data, int total, int totalDisplay)> GetPurchaseInvoicesAsync(
            int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            return await _purchaseInvoiceUnitOfWork
                .PurchaseInvoiceRepository
                .GetPagedPurchaseInvoicesAsync(pageIndex, pageSize, search, order);
        }

        public async Task<IList<PurchaseInvoice>> GetPurchaseInvoicesByStatusAsync(params PurchaseInvoiceStatus[] statuses)
        {
            return await _purchaseInvoiceUnitOfWork.PurchaseInvoiceRepository.GetPurchaseInvoicesByStatusAsync(statuses);
        }

        public async Task<string> GenerateInvoiceNoAsync()
        {
            var count = await _purchaseInvoiceUnitOfWork.PurchaseInvoiceRepository.GetCountAsync();

            string invoiceNo;
            do
            {
                count++;
                invoiceNo = $"PINV-{DateTime.Now:yyyyMM}-{count:D4}";
            }
            while (await _purchaseInvoiceUnitOfWork.PurchaseInvoiceRepository.IsInvoiceNoDuplicateAsync(invoiceNo));

            return invoiceNo;
        }

        public async Task PostPurchaseInvoiceAsync(Guid id)
        {
            await _purchaseInvoiceUnitOfWork.BeginTransactionAsync();

            try
            {
                var invoice = await _purchaseInvoiceUnitOfWork
                    .PurchaseInvoiceRepository
                    .GetPurchaseInvoiceByIdAsync(id)
                    ?? throw new InvalidOperationException("Purchase invoice not found.");

                if (invoice.Status != PurchaseInvoiceStatus.Draft)
                {
                    throw new InvalidOperationException(
                        $"Only a draft purchase invoice can be posted. This one is already {invoice.Status.ToString().ToLower()}.");
                }

                var purchaseOrder = await _purchaseInvoiceUnitOfWork
                    .PurchaseOrderRepository
                    .GetPurchaseOrderByIdAsync(invoice.PurchaseOrderId)
                    ?? throw new InvalidOperationException("Purchase order not found.");

                var lines = invoice.PurchaseInvoiceItems?.ToList() ?? new List<PurchaseInvoiceItem>();

                // The quantities are checked again at posting time, because another
                // invoice may have taken the remaining amount since this draft was saved.
                await GuardAgainstOverBillingAsync(purchaseOrder, lines, invoice.Id);

                await ApplyPostingAsync(invoice, purchaseOrder, lines);

                await _purchaseInvoiceUnitOfWork.CommitTransactionAsync();
            }
            catch (InvalidOperationException)
            {
                await _purchaseInvoiceUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _purchaseInvoiceUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task CancelPurchaseInvoiceAsync(Guid id)
        {
            await _purchaseInvoiceUnitOfWork.BeginTransactionAsync();

            try
            {
                var invoice = await _purchaseInvoiceUnitOfWork
                    .PurchaseInvoiceRepository
                    .GetPurchaseInvoiceByIdAsync(id)
                    ?? throw new InvalidOperationException("Purchase invoice not found.");

                if (invoice.Status == PurchaseInvoiceStatus.Cancelled)
                {
                    throw new InvalidOperationException("This purchase invoice is already cancelled.");
                }

                if (invoice.PaidAmount > 0)
                {
                    throw new InvalidOperationException(
                        "This invoice has already been paid against. Reverse the supplier payment first.");
                }

                var lines = invoice.PurchaseInvoiceItems?.ToList() ?? new List<PurchaseInvoiceItem>();

                if (invoice.Status != PurchaseInvoiceStatus.Draft)
                {
                    var purchaseOrder = await _purchaseInvoiceUnitOfWork
                        .PurchaseOrderRepository
                        .GetPurchaseOrderByIdAsync(invoice.PurchaseOrderId)
                        ?? throw new InvalidOperationException("Purchase order not found.");

                    await ApplyOrderConsumptionAsync(purchaseOrder, lines, add: false);
                    await AdjustSupplierBalanceAsync(invoice.SupplierId, -invoice.GrandTotal);
                }

                invoice.Status = PurchaseInvoiceStatus.Cancelled;
                invoice.DueAmount = 0m;
                invoice.Updated = DateTime.Now;

                await _purchaseInvoiceUnitOfWork.PurchaseInvoiceRepository.EditAsync(invoice);
                await _purchaseInvoiceUnitOfWork.CommitTransactionAsync();
            }
            catch (InvalidOperationException)
            {
                await _purchaseInvoiceUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _purchaseInvoiceUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<IList<BillableLineDto>> GetBillableLinesAsync(Guid purchaseOrderId, Guid? excludeInvoiceId = null)
        {
            var purchaseOrder = await _purchaseInvoiceUnitOfWork
                .PurchaseOrderRepository
                .GetPurchaseOrderByIdAsync(purchaseOrderId);

            if (purchaseOrder == null)
            {
                return new List<BillableLineDto>();
            }

            var pending = await GetPendingInvoicedQuantitiesAsync(purchaseOrderId, excludeInvoiceId);

            var lines = new List<BillableLineDto>();

            foreach (var item in purchaseOrder.PurchaseOrderItems ?? new List<PurchaseOrderItem>())
            {
                // Only goods that have actually arrived may be billed.
                var remaining = item.ReceivedQuantity - item.InvoicedQuantity - pending.GetValueOrDefault(item.ProductId);

                lines.Add(new BillableLineDto
                {
                    ProductId = item.ProductId,
                    ProductName = item.Product?.ProductName ?? string.Empty,
                    UnitName = item.Product?.Unit?.UnitName ?? string.Empty,
                    OrderedQuantity = item.Quantity,
                    ReceivedQuantity = item.ReceivedQuantity,
                    InvoicedQuantity = item.InvoicedQuantity,
                    RemainingQuantity = remaining < 0 ? 0 : remaining,
                    UnitPrice = item.UnitPrice,
                    DiscountAmount = ProRateDiscount(item, remaining < 0 ? 0 : remaining),
                    TaxRate = item.TaxRate
                });
            }

            return lines;
        }

        public async Task<ThreeWayMatchDto?> GetThreeWayMatchAsync(Guid purchaseOrderId)
        {
            var purchaseOrder = await _purchaseInvoiceUnitOfWork
                .PurchaseOrderRepository
                .GetPurchaseOrderByIdAsync(purchaseOrderId);

            if (purchaseOrder == null)
            {
                return null;
            }

            // Cancelled documents on either side never took effect, so they are left
            // out of the comparison entirely.
            var receipts = (await _purchaseInvoiceUnitOfWork
                .GoodsReceiptRepository
                .GetGoodsReceiptsByPurchaseOrderAsync(purchaseOrderId))
                .Where(x => x.Status == GoodsReceiptStatus.Received)
                .ToList();

            var invoices = (await _purchaseInvoiceUnitOfWork
                .PurchaseInvoiceRepository
                .GetPurchaseInvoicesByPurchaseOrderAsync(purchaseOrderId))
                .Where(x => x.Status != PurchaseInvoiceStatus.Cancelled && x.Status != PurchaseInvoiceStatus.Draft)
                .ToList();

            var receivedByProduct = receipts
                .SelectMany(x => x.GoodsReceiptItems ?? new List<GoodsReceiptItem>())
                .GroupBy(x => x.ProductId)
                .ToDictionary(g => g.Key, g => (
                    Quantity: g.Sum(x => x.ReceivedQuantity),
                    Amount: Round(g.Sum(x => x.ReceivedQuantity * x.UnitPrice))));

            var invoicedByProduct = invoices
                .SelectMany(x => x.PurchaseInvoiceItems ?? new List<PurchaseInvoiceItem>())
                .GroupBy(x => x.ProductId)
                .ToDictionary(g => g.Key, g => (
                    Quantity: g.Sum(x => x.Quantity),
                    Amount: Round(g.Sum(x => x.LineTotal))));

            var result = new ThreeWayMatchDto
            {
                PurchaseOrderId = purchaseOrder.Id,
                PurchaseOrderNo = purchaseOrder.PurchaseOrderNo,
                SupplierName = purchaseOrder.Supplier?.SupplierName ?? string.Empty,
                WarehouseName = purchaseOrder.BusinessLocation?.LocationName ?? string.Empty,
                OrderDate = purchaseOrder.OrderDate,
                Status = purchaseOrder.Status.ToString()
            };

            foreach (var item in purchaseOrder.PurchaseOrderItems ?? new List<PurchaseOrderItem>())
            {
                var received = receivedByProduct.GetValueOrDefault(item.ProductId);
                var invoiced = invoicedByProduct.GetValueOrDefault(item.ProductId);

                var productName = item.Product?.ProductName ?? string.Empty;

                var line = new ThreeWayMatchLineDto
                {
                    ProductName = productName,
                    UnitName = item.Product?.Unit?.UnitName ?? string.Empty,
                    OrderedQuantity = item.Quantity,
                    ReceivedQuantity = received.Quantity,
                    InvoicedQuantity = invoiced.Quantity,
                    OrderedAmount = item.LineTotal,
                    ReceivedAmount = received.Amount,
                    InvoicedAmount = invoiced.Amount,
                    QuantityVariance = received.Quantity - invoiced.Quantity,
                    AmountVariance = Round(received.Amount - invoiced.Amount)
                };

                line.IsMatched = line.QuantityVariance == 0 && line.AmountVariance == 0;

                if (invoiced.Quantity > received.Quantity)
                {
                    result.Mismatches.Add(
                        $"'{productName}' is billed for {invoiced.Quantity:N2} but only {received.Quantity:N2} arrived.");
                }
                else if (line.QuantityVariance != 0)
                {
                    result.Mismatches.Add(
                        $"'{productName}' has {line.QuantityVariance:N2} received but not yet billed.");
                }

                if (line.QuantityVariance == 0 && line.AmountVariance != 0)
                {
                    result.Mismatches.Add(
                        $"'{productName}' matches on quantity but the billed amount differs by {line.AmountVariance:N2}.");
                }

                if (received.Quantity > item.Quantity)
                {
                    result.Mismatches.Add(
                        $"'{productName}' received {received.Quantity:N2} against an order of {item.Quantity:N2}.");
                }

                result.Lines.Add(line);
            }

            result.OrderedAmount = purchaseOrder.GrandTotal;
            result.ReceivedAmount = Round(result.Lines.Sum(x => x.ReceivedAmount));
            result.InvoicedAmount = Round(invoices.Sum(x => x.GrandTotal));
            result.IsMatched = result.Mismatches.Count == 0 && result.Lines.All(x => x.IsMatched);

            return result;
        }

        /// <summary>
        /// Everything create and update both need before any line can be priced.
        /// </summary>
        private async Task<InvoiceContext> ValidateAsync(PurchaseInvoice purchaseInvoice, PurchaseInvoice? existing)
        {
            var purchaseOrder = await _purchaseInvoiceUnitOfWork
                .PurchaseOrderRepository
                .GetPurchaseOrderByIdAsync(purchaseInvoice.PurchaseOrderId)
                ?? throw new InvalidOperationException("Purchase order not found.");

            if (purchaseOrder.Status is not (PurchaseOrderStatus.PartiallyReceived
                or PurchaseOrderStatus.Received
                or PurchaseOrderStatus.Closed))
            {
                throw new InvalidOperationException(
                    "Only goods that have actually been received can be invoiced, so the purchase order " +
                    "needs at least one confirmed goods receipt first.");
            }

            if (!string.IsNullOrWhiteSpace(purchaseInvoice.SupplierInvoiceNo)
                && await _purchaseInvoiceUnitOfWork.PurchaseInvoiceRepository.IsSupplierInvoiceNoDuplicateAsync(
                    purchaseOrder.SupplierId, purchaseInvoice.SupplierInvoiceNo, existing?.Id))
            {
                throw new InvalidOperationException(
                    $"This supplier has already billed invoice number '{purchaseInvoice.SupplierInvoiceNo}'.");
            }

            var goodsReceiptId = await ResolveGoodsReceiptIdAsync(purchaseInvoice.GoodsReceiptId, purchaseOrder.Id);

            var invoiceDate = purchaseInvoice.InvoiceDate == default ? DateTime.Now : purchaseInvoice.InvoiceDate;
            var paymentTermId = await ResolvePaymentTermIdAsync(purchaseInvoice.PaymentTermId);

            var dueDate = purchaseInvoice.DueDate == default
                ? await CalculateDueDateAsync(invoiceDate, paymentTermId)
                : purchaseInvoice.DueDate;

            if (dueDate.Date < invoiceDate.Date)
            {
                throw new InvalidOperationException("The due date cannot be earlier than the invoice date.");
            }

            if (purchaseInvoice.OtherCharges < 0)
            {
                throw new InvalidOperationException("Other charges cannot be negative.");
            }

            var grouped = purchaseInvoice.PurchaseInvoiceItems?
                .Where(x => x.ProductId != Guid.Empty && x.Quantity > 0)
                .GroupBy(x => x.ProductId)
                .Select(g => new RequestedLine(
                    g.Key,
                    g.Sum(x => x.Quantity),
                    g.Sum(x => x.DiscountAmount)))
                .ToList();

            if (grouped == null || grouped.Count == 0)
            {
                throw new InvalidOperationException("At least one line with a positive quantity is required.");
            }

            var validated = new List<ValidatedLine>();

            foreach (var requested in grouped)
            {
                var orderLine = purchaseOrder.PurchaseOrderItems?
                    .FirstOrDefault(x => x.ProductId == requested.ProductId)
                    ?? throw new InvalidOperationException(
                        "A product that is not part of this purchase order cannot be invoiced.");

                var productName = orderLine.Product?.ProductName ?? "This product";
                var lineAmount = Round(orderLine.UnitPrice * requested.Quantity);

                if (requested.DiscountAmount < 0 || requested.DiscountAmount > lineAmount)
                {
                    throw new InvalidOperationException(
                        $"The discount on '{productName}' must be between 0 and {lineAmount:N2}.");
                }

                // The agreed unit price and tax rate come from the order, never from
                // the browser, so a supplier cannot be billed at a price nobody agreed.
                validated.Add(new ValidatedLine(
                    orderLine.ProductId,
                    requested.Quantity,
                    orderLine.UnitPrice,
                    requested.DiscountAmount,
                    orderLine.TaxRate));
            }

            await GuardAgainstOverBillingAsync(purchaseOrder,
                validated.Select(x => new PurchaseInvoiceItem
                {
                    ProductId = x.ProductId,
                    Quantity = x.Quantity
                }).ToList(),
                existing?.Id);

            return new InvoiceContext(purchaseOrder, goodsReceiptId, validated, invoiceDate, dueDate,
                paymentTermId, Round(purchaseInvoice.OtherCharges));
        }

        /// <summary>
        /// The received quantity is the ceiling, not the ordered one. Draft invoices
        /// count towards it too, so the same goods cannot be billed twice while a
        /// draft is open.
        /// </summary>
        private async Task GuardAgainstOverBillingAsync(PurchaseOrder purchaseOrder,
            IList<PurchaseInvoiceItem> lines, Guid? excludeInvoiceId)
        {
            var pending = await GetPendingInvoicedQuantitiesAsync(purchaseOrder.Id, excludeInvoiceId);

            foreach (var line in lines)
            {
                var orderLine = purchaseOrder.PurchaseOrderItems?
                    .FirstOrDefault(x => x.ProductId == line.ProductId)
                    ?? throw new InvalidOperationException(
                        "A product that is not part of this purchase order cannot be invoiced.");

                var productName = orderLine.Product?.ProductName ?? "This product";
                var remaining = orderLine.ReceivedQuantity - orderLine.InvoicedQuantity
                                - pending.GetValueOrDefault(line.ProductId);

                if (line.Quantity > remaining)
                {
                    throw new InvalidOperationException(
                        $"'{productName}' has only {(remaining < 0 ? 0 : remaining):N2} received but unbilled, " +
                        $"but {line.Quantity:N2} is being invoiced.");
                }
            }
        }

        /// <summary>
        /// Draft invoices of the order, product by product. One invoice can be left
        /// out so an edit is measured against the right figure.
        /// </summary>
        private async Task<Dictionary<Guid, decimal>> GetPendingInvoicedQuantitiesAsync(Guid purchaseOrderId,
            Guid? excludeInvoiceId)
        {
            var invoices = await _purchaseInvoiceUnitOfWork
                .PurchaseInvoiceRepository
                .GetPurchaseInvoicesByPurchaseOrderAsync(purchaseOrderId);

            return invoices
                .Where(x => x.Status == PurchaseInvoiceStatus.Draft
                            && (!excludeInvoiceId.HasValue || x.Id != excludeInvoiceId.Value))
                .SelectMany(x => x.PurchaseInvoiceItems ?? new List<PurchaseInvoiceItem>())
                .GroupBy(x => x.ProductId)
                .ToDictionary(g => g.Key, g => g.Sum(x => x.Quantity));
        }

        /// <summary>
        /// Marks the invoice as posted, moves the invoiced quantity onto the order and
        /// raises the supplier's outstanding balance. Every caller already runs inside
        /// a transaction.
        /// </summary>
        private async Task ApplyPostingAsync(PurchaseInvoice invoice, PurchaseOrder purchaseOrder,
            IList<PurchaseInvoiceItem> lines)
        {
            await ApplyOrderConsumptionAsync(purchaseOrder, lines, add: true);
            await AdjustSupplierBalanceAsync(invoice.SupplierId, invoice.GrandTotal);

            invoice.Status = PurchaseInvoiceStatus.Posted;
            invoice.DueAmount = invoice.GrandTotal - invoice.PaidAmount;
            invoice.Updated = DateTime.Now;

            await _purchaseInvoiceUnitOfWork.PurchaseInvoiceRepository.EditAsync(invoice);
        }

        private async Task ApplyOrderConsumptionAsync(PurchaseOrder purchaseOrder,
            IEnumerable<PurchaseInvoiceItem> lines, bool add)
        {
            foreach (var line in lines)
            {
                var orderLine = purchaseOrder.PurchaseOrderItems?
                    .FirstOrDefault(x => x.ProductId == line.ProductId);

                if (orderLine == null)
                {
                    continue;
                }

                orderLine.InvoicedQuantity += add ? line.Quantity : -line.Quantity;

                if (orderLine.InvoicedQuantity < 0)
                {
                    orderLine.InvoicedQuantity = 0;
                }

                await _purchaseInvoiceUnitOfWork.PurchaseOrderItemRepository.EditAsync(orderLine);
            }

            purchaseOrder.Updated = DateTime.Now;

            await _purchaseInvoiceUnitOfWork.PurchaseOrderRepository.EditAsync(purchaseOrder);
        }

        /// <summary>
        /// The single place the purchase module moves what is owed to a supplier.
        /// </summary>
        private async Task AdjustSupplierBalanceAsync(Guid supplierId, decimal amount)
        {
            var supplier = await _purchaseInvoiceUnitOfWork
                .SupplierRepository
                .GetByIdAsync(supplierId)
                ?? throw new InvalidOperationException("Supplier not found.");

            supplier.CurrentOutstanding += amount;
            supplier.Updated = DateTime.Now;

            await _purchaseInvoiceUnitOfWork.SupplierRepository.EditAsync(supplier);
        }

        private async Task<Guid?> ResolveGoodsReceiptIdAsync(Guid? goodsReceiptId, Guid purchaseOrderId)
        {
            if (!goodsReceiptId.HasValue || goodsReceiptId.Value == Guid.Empty)
            {
                return null;
            }

            var goodsReceipt = await _purchaseInvoiceUnitOfWork
                .GoodsReceiptRepository
                .GetByIdAsync(goodsReceiptId.Value)
                ?? throw new InvalidOperationException("Goods receipt not found.");

            if (goodsReceipt.PurchaseOrderId != purchaseOrderId)
            {
                throw new InvalidOperationException(
                    "The chosen goods receipt belongs to a different purchase order.");
            }

            if (goodsReceipt.Status != GoodsReceiptStatus.Received)
            {
                throw new InvalidOperationException(
                    "Only a received goods receipt can be invoiced against.");
            }

            return goodsReceipt.Id;
        }

        private async Task<Guid?> ResolvePaymentTermIdAsync(Guid? paymentTermId)
        {
            if (!paymentTermId.HasValue || paymentTermId.Value == Guid.Empty)
            {
                return null;
            }

            var paymentTerm = await _purchaseInvoiceUnitOfWork
                .PaymentTermRepository
                .GetByIdAsync(paymentTermId.Value)
                ?? throw new InvalidOperationException("Payment term not found.");

            if (!paymentTerm.IsActive)
            {
                throw new InvalidOperationException($"'{paymentTerm.TermName}' is inactive and cannot be used.");
            }

            return paymentTerm.Id;
        }

        private async Task<DateTime> CalculateDueDateAsync(DateTime invoiceDate, Guid? paymentTermId)
        {
            if (!paymentTermId.HasValue)
            {
                return invoiceDate;
            }

            var paymentTerm = await _purchaseInvoiceUnitOfWork.PaymentTermRepository.GetByIdAsync(paymentTermId.Value);

            return invoiceDate.AddDays(paymentTerm?.DueDays ?? 0);
        }

        /// <summary>
        /// Turns the validated lines into stored lines. This is the single place where
        /// the money on a purchase invoice is decided.
        /// </summary>
        private static PricedResult PriceLines(InvoiceContext context, Guid invoiceId)
        {
            decimal subTotal = 0m;
            decimal itemDiscountTotal = 0m;
            decimal totalTax = 0m;

            var lines = new List<PurchaseInvoiceItem>();

            foreach (var validated in context.Lines)
            {
                var lineAmount = Round(validated.UnitPrice * validated.Quantity);
                var discount = Round(Math.Clamp(validated.DiscountAmount, 0m, lineAmount));
                var taxableAmount = lineAmount - discount;
                var taxAmount = Round(taxableAmount * validated.TaxRate / 100m);

                lines.Add(new PurchaseInvoiceItem
                {
                    Id = Guid.NewGuid(),
                    PurchaseInvoiceId = invoiceId,
                    ProductId = validated.ProductId,
                    Quantity = validated.Quantity,
                    UnitPrice = validated.UnitPrice,
                    DiscountAmount = discount,
                    TaxRate = validated.TaxRate,
                    TaxAmount = taxAmount,
                    LineTotal = taxableAmount + taxAmount
                });

                subTotal += lineAmount;
                itemDiscountTotal += discount;
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
        /// Offers the share of the order line's discount that belongs to the quantity
        /// being billed, so part invoicing carries part of the discount.
        /// </summary>
        private static decimal ProRateDiscount(PurchaseOrderItem orderLine, decimal quantity)
        {
            if (orderLine.Quantity <= 0 || orderLine.DiscountAmount <= 0 || quantity <= 0)
            {
                return 0m;
            }

            return Round(orderLine.DiscountAmount * quantity / orderLine.Quantity);
        }

        private static decimal Round(decimal value)
        {
            return Math.Round(value, 2, MidpointRounding.AwayFromZero);
        }

        private sealed record RequestedLine(Guid ProductId, decimal Quantity, decimal DiscountAmount);

        private sealed record ValidatedLine(Guid ProductId, decimal Quantity, decimal UnitPrice,
            decimal DiscountAmount, decimal TaxRate);

        private sealed record InvoiceContext(
            PurchaseOrder PurchaseOrder,
            Guid? GoodsReceiptId,
            IList<ValidatedLine> Lines,
            DateTime InvoiceDate,
            DateTime DueDate,
            Guid? PaymentTermId,
            decimal OtherCharges);

        private sealed record PricedResult(
            List<PurchaseInvoiceItem> Lines,
            decimal SubTotal,
            decimal ItemDiscountTotal,
            decimal TotalTax,
            decimal GrandTotal);
    }
}
