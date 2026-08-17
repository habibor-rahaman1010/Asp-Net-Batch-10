using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Domain.UnitOfWorkContracts;

namespace DevSkill.Inventory.Application.Services
{
    public class GoodsReceiptManagementService : IGoodsReceiptManagementService
    {
        private readonly IInventoryUnitOfWork _goodsReceiptUnitOfWork;

        public GoodsReceiptManagementService(IInventoryUnitOfWork unitOfWork)
        {
            _goodsReceiptUnitOfWork = unitOfWork;
        }

        public async Task<Guid> CreateGoodsReceiptAsync(GoodsReceipt goodsReceipt)
        {
            await _goodsReceiptUnitOfWork.BeginTransactionAsync();

            try
            {
                var context = await ValidateAsync(goodsReceipt, null);
                var goodsReceiptId = Guid.NewGuid();

                var lines = BuildLines(context.Lines, goodsReceiptId);

                var header = new GoodsReceipt
                {
                    Id = goodsReceiptId,
                    GoodsReceiptNo = await GenerateGoodsReceiptNoAsync(),
                    PurchaseOrderId = context.PurchaseOrder.Id,
                    SupplierId = context.PurchaseOrder.SupplierId,
                    BusinessLocationId = context.PurchaseOrder.BusinessLocationId,
                    ReceiptDate = context.ReceiptDate,
                    SupplierChallanNo = goodsReceipt.SupplierChallanNo ?? string.Empty,
                    ReceivedBy = goodsReceipt.ReceivedBy ?? string.Empty,
                    Notes = goodsReceipt.Notes ?? string.Empty,
                    Status = GoodsReceiptStatus.Draft,
                    CreatedBy = goodsReceipt.CreatedBy ?? string.Empty,
                    Created = DateTime.Now,
                    Updated = DateTime.Now,
                    GoodsReceiptItems = lines
                };

                await _goodsReceiptUnitOfWork.GoodsReceiptRepository.AddAsync(header);

                // A draft holds no stock. Asking for it to be received straight away
                // runs the confirmation inside this very transaction.
                if (goodsReceipt.Status == GoodsReceiptStatus.Received)
                {
                    await ApplyConfirmationAsync(header, context.PurchaseOrder, lines);
                }

                await _goodsReceiptUnitOfWork.CommitTransactionAsync();

                return goodsReceiptId;
            }
            catch (InvalidOperationException)
            {
                await _goodsReceiptUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _goodsReceiptUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<bool> UpdateGoodsReceiptAsync(GoodsReceipt goodsReceipt)
        {
            await _goodsReceiptUnitOfWork.BeginTransactionAsync();

            try
            {
                var existing = await _goodsReceiptUnitOfWork
                    .GoodsReceiptRepository
                    .GetGoodsReceiptByIdAsync(goodsReceipt.Id)
                    ?? throw new InvalidOperationException("Goods receipt not found.");

                // A confirmed receipt has already moved stock, so its quantities are
                // history. Cancel it and raise a new one instead.
                if (existing.Status != GoodsReceiptStatus.Draft)
                {
                    throw new InvalidOperationException(
                        $"A {existing.Status.ToString().ToLower()} goods receipt cannot be edited.");
                }

                // The purchase order a receipt belongs to is what the receipt is.
                goodsReceipt.PurchaseOrderId = existing.PurchaseOrderId;

                var context = await ValidateAsync(goodsReceipt, existing);

                await _goodsReceiptUnitOfWork
                    .GoodsReceiptItemRepository
                    .RemoveRangeAsync(existing.GoodsReceiptItems?.ToList() ?? new List<GoodsReceiptItem>());

                var lines = BuildLines(context.Lines, existing.Id);

                foreach (var line in lines)
                {
                    await _goodsReceiptUnitOfWork.GoodsReceiptItemRepository.AddAsync(line);
                }

                // The receipt number is issued once and never changes.
                existing.ReceiptDate = context.ReceiptDate;
                existing.SupplierChallanNo = goodsReceipt.SupplierChallanNo ?? string.Empty;
                existing.ReceivedBy = goodsReceipt.ReceivedBy ?? string.Empty;
                existing.Notes = goodsReceipt.Notes ?? string.Empty;
                existing.Updated = DateTime.Now;

                await _goodsReceiptUnitOfWork.GoodsReceiptRepository.EditAsync(existing);

                if (goodsReceipt.Status == GoodsReceiptStatus.Received)
                {
                    await ApplyConfirmationAsync(existing, context.PurchaseOrder, lines);
                }

                await _goodsReceiptUnitOfWork.CommitTransactionAsync();

                return true;
            }
            catch (InvalidOperationException)
            {
                await _goodsReceiptUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _goodsReceiptUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<bool> DeleteGoodsReceiptAsync(Guid id)
        {
            await _goodsReceiptUnitOfWork.BeginTransactionAsync();

            try
            {
                var goodsReceipt = await _goodsReceiptUnitOfWork
                    .GoodsReceiptRepository
                    .GetGoodsReceiptByIdAsync(id)
                    ?? throw new InvalidOperationException("Goods receipt not found.");

                // Deleting a confirmed receipt would erase a stock movement that really
                // happened. Cancelling it puts the stock back and keeps the record.
                if (goodsReceipt.Status == GoodsReceiptStatus.Received)
                {
                    throw new InvalidOperationException(
                        "A received goods receipt cannot be deleted. Cancel it instead, which returns the stock.");
                }

                await _goodsReceiptUnitOfWork
                    .GoodsReceiptItemRepository
                    .RemoveRangeAsync(goodsReceipt.GoodsReceiptItems?.ToList() ?? new List<GoodsReceiptItem>());

                await _goodsReceiptUnitOfWork.GoodsReceiptRepository.RemoveAsync(goodsReceipt);

                await _goodsReceiptUnitOfWork.CommitTransactionAsync();

                return true;
            }
            catch (InvalidOperationException)
            {
                await _goodsReceiptUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _goodsReceiptUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<GoodsReceipt?> GetGoodsReceiptByIdAsync(Guid id)
        {
            return await _goodsReceiptUnitOfWork.GoodsReceiptRepository.GetGoodsReceiptByIdAsync(id);
        }

        public async Task<(IList<GoodsReceipt> data, int total, int totalDisplay)> GetGoodsReceiptsAsync(
            int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            return await _goodsReceiptUnitOfWork
                .GoodsReceiptRepository
                .GetPagedGoodsReceiptsAsync(pageIndex, pageSize, search, order);
        }

        public async Task<IList<GoodsReceipt>> GetGoodsReceiptsByPurchaseOrderAsync(Guid purchaseOrderId)
        {
            return await _goodsReceiptUnitOfWork
                .GoodsReceiptRepository
                .GetGoodsReceiptsByPurchaseOrderAsync(purchaseOrderId);
        }

        public async Task<string> GenerateGoodsReceiptNoAsync()
        {
            var count = await _goodsReceiptUnitOfWork.GoodsReceiptRepository.GetCountAsync();

            string goodsReceiptNo;
            do
            {
                count++;
                goodsReceiptNo = $"GRN-{DateTime.Now:yyyyMM}-{count:D4}";
            }
            while (await _goodsReceiptUnitOfWork.GoodsReceiptRepository.IsGoodsReceiptNoDuplicateAsync(goodsReceiptNo));

            return goodsReceiptNo;
        }

        public async Task ConfirmGoodsReceiptAsync(Guid id)
        {
            await _goodsReceiptUnitOfWork.BeginTransactionAsync();

            try
            {
                var goodsReceipt = await _goodsReceiptUnitOfWork
                    .GoodsReceiptRepository
                    .GetGoodsReceiptByIdAsync(id)
                    ?? throw new InvalidOperationException("Goods receipt not found.");

                if (goodsReceipt.Status != GoodsReceiptStatus.Draft)
                {
                    throw new InvalidOperationException(
                        $"Only a draft goods receipt can be received. This one is already {goodsReceipt.Status.ToString().ToLower()}.");
                }

                var purchaseOrder = await _goodsReceiptUnitOfWork
                    .PurchaseOrderRepository
                    .GetPurchaseOrderByIdAsync(goodsReceipt.PurchaseOrderId)
                    ?? throw new InvalidOperationException("Purchase order not found.");

                GuardOrderCanReceive(purchaseOrder);

                var lines = goodsReceipt.GoodsReceiptItems?.ToList() ?? new List<GoodsReceiptItem>();

                // The quantities are checked again at confirmation time, because another
                // receipt may have taken the remaining amount since this draft was saved.
                await GuardAgainstOverReceivingAsync(purchaseOrder, lines, goodsReceipt.Id);

                await ApplyConfirmationAsync(goodsReceipt, purchaseOrder, lines);

                await _goodsReceiptUnitOfWork.CommitTransactionAsync();
            }
            catch (InvalidOperationException)
            {
                await _goodsReceiptUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _goodsReceiptUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task CancelGoodsReceiptAsync(Guid id)
        {
            await _goodsReceiptUnitOfWork.BeginTransactionAsync();

            try
            {
                var goodsReceipt = await _goodsReceiptUnitOfWork
                    .GoodsReceiptRepository
                    .GetGoodsReceiptByIdAsync(id)
                    ?? throw new InvalidOperationException("Goods receipt not found.");

                if (goodsReceipt.Status == GoodsReceiptStatus.Cancelled)
                {
                    throw new InvalidOperationException("This goods receipt is already cancelled.");
                }

                var lines = goodsReceipt.GoodsReceiptItems?.ToList() ?? new List<GoodsReceiptItem>();

                if (goodsReceipt.Status == GoodsReceiptStatus.Received)
                {
                    // Anything already sent back to the supplier or billed rests on this
                    // receipt, so it cannot be unwound underneath them.
                    if (lines.Any(x => x.ReturnedQuantity > 0))
                    {
                        throw new InvalidOperationException(
                            "This receipt already has a purchase return against it and cannot be cancelled.");
                    }

                    var purchaseOrder = await _goodsReceiptUnitOfWork
                        .PurchaseOrderRepository
                        .GetPurchaseOrderByIdAsync(goodsReceipt.PurchaseOrderId)
                        ?? throw new InvalidOperationException("Purchase order not found.");

                    await GuardAgainstInvoicedQuantitiesAsync(purchaseOrder, lines);

                    // What came in on this receipt goes straight back out.
                    await MoveStockAsync(lines, increase: false);
                    await ApplyOrderConsumptionAsync(purchaseOrder, lines, add: false);
                }

                goodsReceipt.Status = GoodsReceiptStatus.Cancelled;
                goodsReceipt.Updated = DateTime.Now;

                await _goodsReceiptUnitOfWork.GoodsReceiptRepository.EditAsync(goodsReceipt);
                await _goodsReceiptUnitOfWork.CommitTransactionAsync();
            }
            catch (InvalidOperationException)
            {
                await _goodsReceiptUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _goodsReceiptUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<IList<ReceivableLineDto>> GetReceivableLinesAsync(Guid purchaseOrderId,
            Guid? excludeGoodsReceiptId = null)
        {
            var purchaseOrder = await _goodsReceiptUnitOfWork
                .PurchaseOrderRepository
                .GetPurchaseOrderByIdAsync(purchaseOrderId);

            if (purchaseOrder == null)
            {
                return new List<ReceivableLineDto>();
            }

            var pending = await GetPendingQuantitiesAsync(purchaseOrderId, excludeGoodsReceiptId);

            var lines = new List<ReceivableLineDto>();

            foreach (var item in purchaseOrder.PurchaseOrderItems ?? new List<PurchaseOrderItem>())
            {
                var pendingQuantity = pending.GetValueOrDefault(item.ProductId);
                var remaining = item.Quantity - item.ReceivedQuantity - pendingQuantity;

                var product = item.Product ?? await _goodsReceiptUnitOfWork.ProductRepository.GetByIdAsync(item.ProductId);

                lines.Add(new ReceivableLineDto
                {
                    ProductId = item.ProductId,
                    ProductName = product?.ProductName ?? string.Empty,
                    UnitName = product?.Unit?.UnitName ?? string.Empty,
                    OrderedQuantity = item.Quantity,
                    ReceivedQuantity = item.ReceivedQuantity,
                    PendingQuantity = pendingQuantity,
                    RemainingQuantity = remaining < 0 ? 0 : remaining,
                    UnitPrice = item.UnitPrice,
                    CurrentStock = product?.CurrentStock ?? 0
                });
            }

            return lines;
        }

        /// <summary>
        /// Everything create and update both need before any line can be built.
        /// </summary>
        private async Task<ReceiptContext> ValidateAsync(GoodsReceipt goodsReceipt, GoodsReceipt? existing)
        {
            var purchaseOrder = await _goodsReceiptUnitOfWork
                .PurchaseOrderRepository
                .GetPurchaseOrderByIdAsync(goodsReceipt.PurchaseOrderId)
                ?? throw new InvalidOperationException("Purchase order not found.");

            GuardOrderCanReceive(purchaseOrder);

            var receiptDate = goodsReceipt.ReceiptDate == default ? DateTime.Now : goodsReceipt.ReceiptDate;

            if (receiptDate.Date < purchaseOrder.OrderDate.Date)
            {
                throw new InvalidOperationException("The receipt date cannot be earlier than the purchase order date.");
            }

            var grouped = goodsReceipt.GoodsReceiptItems?
                .Where(x => x.ProductId != Guid.Empty && (x.ReceivedQuantity > 0 || x.RejectedQuantity > 0))
                .GroupBy(x => x.ProductId)
                .Select(g => new RequestedLine(
                    g.Key,
                    g.Sum(x => x.ReceivedQuantity),
                    g.Sum(x => x.RejectedQuantity),
                    g.Select(x => x.Remarks).FirstOrDefault(x => !string.IsNullOrWhiteSpace(x)) ?? string.Empty))
                .ToList();

            if (grouped == null || grouped.Count == 0)
            {
                throw new InvalidOperationException("At least one line with a received or rejected quantity is required.");
            }

            var validated = new List<ValidatedLine>();

            foreach (var requested in grouped)
            {
                var orderLine = purchaseOrder.PurchaseOrderItems?
                    .FirstOrDefault(x => x.ProductId == requested.ProductId)
                    ?? throw new InvalidOperationException(
                        "A product that is not part of this purchase order cannot be received.");

                var product = orderLine.Product
                    ?? await _goodsReceiptUnitOfWork.ProductRepository.GetByIdAsync(requested.ProductId)
                    ?? throw new InvalidOperationException("Product not found.");

                if (requested.ReceivedQuantity < 0 || requested.RejectedQuantity < 0)
                {
                    throw new InvalidOperationException(
                        $"The quantities of '{product.ProductName}' cannot be negative.");
                }

                // Stock is kept as whole units, so a receipt may not book a fraction.
                GuardWholeQuantity(product.ProductName, requested.ReceivedQuantity);

                validated.Add(new ValidatedLine(
                    product.Id,
                    orderLine.Quantity,
                    requested.ReceivedQuantity,
                    requested.RejectedQuantity,
                    orderLine.UnitPrice,
                    requested.Remarks));
            }

            await GuardAgainstOverReceivingAsync(purchaseOrder,
                validated.Select(x => new GoodsReceiptItem
                {
                    ProductId = x.ProductId,
                    ReceivedQuantity = x.ReceivedQuantity
                }).ToList(),
                existing?.Id);

            return new ReceiptContext(purchaseOrder, validated, receiptDate);
        }

        /// <summary>
        /// Nothing may be received against an order that is not approved, and nothing
        /// more against one that is finished.
        /// </summary>
        private static void GuardOrderCanReceive(PurchaseOrder purchaseOrder)
        {
            if (purchaseOrder.Status is PurchaseOrderStatus.Approved
                or PurchaseOrderStatus.PartiallyReceived
                or PurchaseOrderStatus.Received)
            {
                return;
            }

            throw new InvalidOperationException(
                $"Goods cannot be received against a {purchaseOrder.Status.ToString().ToLower()} purchase order. " +
                "It has to be approved first.");
        }

        /// <summary>
        /// The ordered quantity is the ceiling. Draft receipts count towards it too,
        /// so the same goods cannot be booked twice while a draft is open.
        /// </summary>
        private async Task GuardAgainstOverReceivingAsync(PurchaseOrder purchaseOrder,
            IList<GoodsReceiptItem> lines, Guid? excludeGoodsReceiptId)
        {
            var pending = await GetPendingQuantitiesAsync(purchaseOrder.Id, excludeGoodsReceiptId);

            foreach (var line in lines.Where(x => x.ReceivedQuantity > 0))
            {
                var orderLine = purchaseOrder.PurchaseOrderItems?
                    .FirstOrDefault(x => x.ProductId == line.ProductId)
                    ?? throw new InvalidOperationException(
                        "A product that is not part of this purchase order cannot be received.");

                var productName = orderLine.Product?.ProductName ?? "This product";
                var remaining = orderLine.Quantity - orderLine.ReceivedQuantity - pending.GetValueOrDefault(line.ProductId);

                if (line.ReceivedQuantity > remaining)
                {
                    throw new InvalidOperationException(
                        $"'{productName}' has only {(remaining < 0 ? 0 : remaining):N2} left to receive on this order, " +
                        $"but {line.ReceivedQuantity:N2} is being received.");
                }
            }
        }

        /// <summary>
        /// Draft receipts of the order, product by product. One receipt can be left
        /// out so an edit is measured against the right figure.
        /// </summary>
        private async Task<Dictionary<Guid, decimal>> GetPendingQuantitiesAsync(Guid purchaseOrderId,
            Guid? excludeGoodsReceiptId)
        {
            var receipts = await _goodsReceiptUnitOfWork
                .GoodsReceiptRepository
                .GetGoodsReceiptsByPurchaseOrderAsync(purchaseOrderId);

            return receipts
                .Where(x => x.Status == GoodsReceiptStatus.Draft
                            && (!excludeGoodsReceiptId.HasValue || x.Id != excludeGoodsReceiptId.Value))
                .SelectMany(x => x.GoodsReceiptItems ?? new List<GoodsReceiptItem>())
                .GroupBy(x => x.ProductId)
                .ToDictionary(g => g.Key, g => g.Sum(x => x.ReceivedQuantity));
        }

        /// <summary>
        /// Marks the receipt as received, moves the stock and tells the order about
        /// it. Every caller already runs inside a transaction.
        /// </summary>
        private async Task ApplyConfirmationAsync(GoodsReceipt goodsReceipt, PurchaseOrder purchaseOrder,
            IList<GoodsReceiptItem> lines)
        {
            await MoveStockAsync(lines, increase: true);
            await ApplyOrderConsumptionAsync(purchaseOrder, lines, add: true);

            goodsReceipt.Status = GoodsReceiptStatus.Received;
            goodsReceipt.Updated = DateTime.Now;

            await _goodsReceiptUnitOfWork.GoodsReceiptRepository.EditAsync(goodsReceipt);
        }

        /// <summary>
        /// Puts the goods into the warehouse, or takes them back out when a receipt is
        /// cancelled. This is the only place the purchase module touches the product
        /// table.
        /// </summary>
        private async Task MoveStockAsync(IEnumerable<GoodsReceiptItem> lines, bool increase)
        {
            foreach (var line in lines.Where(x => x.ReceivedQuantity > 0))
            {
                var product = await _goodsReceiptUnitOfWork
                    .ProductRepository
                    .GetByIdAsync(line.ProductId)
                    ?? throw new InvalidOperationException("Product not found.");

                GuardWholeQuantity(product.ProductName, line.ReceivedQuantity);

                var quantity = (int)line.ReceivedQuantity;

                if (increase)
                {
                    product.CurrentStock += quantity;
                }
                else
                {
                    if (product.CurrentStock < quantity)
                    {
                        throw new InvalidOperationException(
                            $"'{product.ProductName}' has only {product.CurrentStock} in stock, so {quantity} " +
                            "cannot be taken back out. The goods have probably already been sold.");
                    }

                    product.CurrentStock -= quantity;
                }

                product.Updated = DateTime.Now;

                await _goodsReceiptUnitOfWork.ProductRepository.EditAsync(product);
            }
        }

        /// <summary>
        /// Moves the received quantity on the order lines and re-reads the order
        /// status from them, so it says exactly how much is still outstanding.
        /// </summary>
        private async Task ApplyOrderConsumptionAsync(PurchaseOrder purchaseOrder,
            IEnumerable<GoodsReceiptItem> lines, bool add)
        {
            foreach (var line in lines.Where(x => x.ReceivedQuantity > 0))
            {
                var orderLine = purchaseOrder.PurchaseOrderItems?
                    .FirstOrDefault(x => x.ProductId == line.ProductId);

                if (orderLine == null)
                {
                    continue;
                }

                orderLine.ReceivedQuantity += add ? line.ReceivedQuantity : -line.ReceivedQuantity;

                if (orderLine.ReceivedQuantity < 0)
                {
                    orderLine.ReceivedQuantity = 0;
                }

                await _goodsReceiptUnitOfWork.PurchaseOrderItemRepository.EditAsync(orderLine);
            }

            var items = purchaseOrder.PurchaseOrderItems ?? new List<PurchaseOrderItem>();

            // A closed order was deliberately finished short, so receiving does not
            // reopen it.
            if (purchaseOrder.Status != PurchaseOrderStatus.Closed)
            {
                purchaseOrder.Status = items.All(x => x.ReceivedQuantity >= x.Quantity)
                    ? PurchaseOrderStatus.Received
                    : items.Any(x => x.ReceivedQuantity > 0)
                        ? PurchaseOrderStatus.PartiallyReceived
                        : PurchaseOrderStatus.Approved;
            }

            purchaseOrder.Updated = DateTime.Now;

            await _goodsReceiptUnitOfWork.PurchaseOrderRepository.EditAsync(purchaseOrder);
        }

        /// <summary>
        /// Cancelling a receipt would pull the received quantity below what the
        /// supplier has already billed, which the invoice flow relies on.
        /// </summary>
        private static Task GuardAgainstInvoicedQuantitiesAsync(PurchaseOrder purchaseOrder,
            IEnumerable<GoodsReceiptItem> lines)
        {
            foreach (var line in lines.Where(x => x.ReceivedQuantity > 0))
            {
                var orderLine = purchaseOrder.PurchaseOrderItems?
                    .FirstOrDefault(x => x.ProductId == line.ProductId);

                if (orderLine == null)
                {
                    continue;
                }

                if (orderLine.ReceivedQuantity - line.ReceivedQuantity < orderLine.InvoicedQuantity)
                {
                    throw new InvalidOperationException(
                        $"'{orderLine.Product?.ProductName ?? "A product"}' on this receipt has already been " +
                        "invoiced. Cancel the purchase invoice first.");
                }
            }

            return Task.CompletedTask;
        }

        private static void GuardWholeQuantity(string productName, decimal quantity)
        {
            if (quantity != Math.Floor(quantity))
            {
                throw new InvalidOperationException(
                    $"Stock is kept in whole units, so '{productName}' cannot be received in fractions.");
            }
        }

        private static List<GoodsReceiptItem> BuildLines(IList<ValidatedLine> lines, Guid goodsReceiptId)
        {
            return lines.Select(x => new GoodsReceiptItem
            {
                Id = Guid.NewGuid(),
                GoodsReceiptId = goodsReceiptId,
                ProductId = x.ProductId,
                OrderedQuantity = x.OrderedQuantity,
                ReceivedQuantity = x.ReceivedQuantity,
                RejectedQuantity = x.RejectedQuantity,
                ReturnedQuantity = 0,
                UnitPrice = x.UnitPrice,
                Remarks = x.Remarks
            }).ToList();
        }

        private sealed record RequestedLine(Guid ProductId, decimal ReceivedQuantity,
            decimal RejectedQuantity, string Remarks);

        private sealed record ValidatedLine(Guid ProductId, decimal OrderedQuantity, decimal ReceivedQuantity,
            decimal RejectedQuantity, decimal UnitPrice, string Remarks);

        private sealed record ReceiptContext(
            PurchaseOrder PurchaseOrder,
            IList<ValidatedLine> Lines,
            DateTime ReceiptDate);
    }
}
