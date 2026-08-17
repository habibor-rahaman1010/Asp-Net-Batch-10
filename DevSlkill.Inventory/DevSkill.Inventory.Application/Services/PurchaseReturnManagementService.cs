using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Domain.UnitOfWorkContracts;

namespace DevSkill.Inventory.Application.Services
{
    public class PurchaseReturnManagementService : IPurchaseReturnManagementService
    {
        private readonly IInventoryUnitOfWork _purchaseReturnUnitOfWork;

        public PurchaseReturnManagementService(IInventoryUnitOfWork unitOfWork)
        {
            _purchaseReturnUnitOfWork = unitOfWork;
        }

        public async Task<Guid> CreatePurchaseReturnAsync(PurchaseReturn purchaseReturn)
        {
            await _purchaseReturnUnitOfWork.BeginTransactionAsync();

            try
            {
                var context = await ValidateAsync(purchaseReturn, null);
                var purchaseReturnId = Guid.NewGuid();

                var lines = BuildLines(context.Lines, purchaseReturnId);

                var header = new PurchaseReturn
                {
                    Id = purchaseReturnId,
                    ReturnNo = await GenerateReturnNoAsync(),
                    GoodsReceiptId = context.GoodsReceipt.Id,
                    SupplierId = context.GoodsReceipt.SupplierId,
                    BusinessLocationId = context.GoodsReceipt.BusinessLocationId,
                    ReturnDate = context.ReturnDate,
                    Reason = purchaseReturn.Reason ?? string.Empty,
                    Notes = purchaseReturn.Notes ?? string.Empty,
                    Status = PurchaseReturnStatus.Draft,
                    TotalAmount = lines.Sum(x => x.LineTotal),
                    CreatedBy = purchaseReturn.CreatedBy ?? string.Empty,
                    Created = DateTime.Now,
                    Updated = DateTime.Now,
                    PurchaseReturnItems = lines
                };

                await _purchaseReturnUnitOfWork.PurchaseReturnRepository.AddAsync(header);

                // A draft holds no stock. Asking for it to go back straight away runs
                // the confirmation inside this very transaction.
                if (purchaseReturn.Status == PurchaseReturnStatus.Returned)
                {
                    await ApplyConfirmationAsync(header, context.GoodsReceipt, lines);
                }

                await _purchaseReturnUnitOfWork.CommitTransactionAsync();

                return purchaseReturnId;
            }
            catch (InvalidOperationException)
            {
                await _purchaseReturnUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _purchaseReturnUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<bool> UpdatePurchaseReturnAsync(PurchaseReturn purchaseReturn)
        {
            await _purchaseReturnUnitOfWork.BeginTransactionAsync();

            try
            {
                var existing = await _purchaseReturnUnitOfWork
                    .PurchaseReturnRepository
                    .GetPurchaseReturnByIdAsync(purchaseReturn.Id)
                    ?? throw new InvalidOperationException("Purchase return not found.");

                // A confirmed return has already moved stock, so its quantities are
                // history. Cancel it and raise a new one instead.
                if (existing.Status != PurchaseReturnStatus.Draft)
                {
                    throw new InvalidOperationException(
                        $"A {existing.Status.ToString().ToLower()} purchase return cannot be edited.");
                }

                // The goods receipt a return belongs to is what the return is.
                purchaseReturn.GoodsReceiptId = existing.GoodsReceiptId;

                var context = await ValidateAsync(purchaseReturn, existing);

                await _purchaseReturnUnitOfWork
                    .PurchaseReturnItemRepository
                    .RemoveRangeAsync(existing.PurchaseReturnItems?.ToList() ?? new List<PurchaseReturnItem>());

                var lines = BuildLines(context.Lines, existing.Id);

                foreach (var line in lines)
                {
                    await _purchaseReturnUnitOfWork.PurchaseReturnItemRepository.AddAsync(line);
                }

                // The return number is issued once and never changes.
                existing.ReturnDate = context.ReturnDate;
                existing.Reason = purchaseReturn.Reason ?? string.Empty;
                existing.Notes = purchaseReturn.Notes ?? string.Empty;
                existing.TotalAmount = lines.Sum(x => x.LineTotal);
                existing.Updated = DateTime.Now;

                await _purchaseReturnUnitOfWork.PurchaseReturnRepository.EditAsync(existing);

                if (purchaseReturn.Status == PurchaseReturnStatus.Returned)
                {
                    await ApplyConfirmationAsync(existing, context.GoodsReceipt, lines);
                }

                await _purchaseReturnUnitOfWork.CommitTransactionAsync();

                return true;
            }
            catch (InvalidOperationException)
            {
                await _purchaseReturnUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _purchaseReturnUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<bool> DeletePurchaseReturnAsync(Guid id)
        {
            await _purchaseReturnUnitOfWork.BeginTransactionAsync();

            try
            {
                var purchaseReturn = await _purchaseReturnUnitOfWork
                    .PurchaseReturnRepository
                    .GetPurchaseReturnByIdAsync(id)
                    ?? throw new InvalidOperationException("Purchase return not found.");

                // Deleting a confirmed return would erase a stock movement that really
                // happened. Cancelling it puts the stock back and keeps the record.
                if (purchaseReturn.Status == PurchaseReturnStatus.Returned)
                {
                    throw new InvalidOperationException(
                        "A confirmed purchase return cannot be deleted. Cancel it instead, which brings the stock back.");
                }

                await _purchaseReturnUnitOfWork
                    .PurchaseReturnItemRepository
                    .RemoveRangeAsync(purchaseReturn.PurchaseReturnItems?.ToList() ?? new List<PurchaseReturnItem>());

                await _purchaseReturnUnitOfWork.PurchaseReturnRepository.RemoveAsync(purchaseReturn);

                await _purchaseReturnUnitOfWork.CommitTransactionAsync();

                return true;
            }
            catch (InvalidOperationException)
            {
                await _purchaseReturnUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _purchaseReturnUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<PurchaseReturn?> GetPurchaseReturnByIdAsync(Guid id)
        {
            return await _purchaseReturnUnitOfWork.PurchaseReturnRepository.GetPurchaseReturnByIdAsync(id);
        }

        public async Task<(IList<PurchaseReturn> data, int total, int totalDisplay)> GetPurchaseReturnsAsync(
            int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            return await _purchaseReturnUnitOfWork
                .PurchaseReturnRepository
                .GetPagedPurchaseReturnsAsync(pageIndex, pageSize, search, order);
        }

        public async Task<IList<PurchaseReturn>> GetPurchaseReturnsByGoodsReceiptAsync(Guid goodsReceiptId)
        {
            return await _purchaseReturnUnitOfWork
                .PurchaseReturnRepository
                .GetPurchaseReturnsByGoodsReceiptAsync(goodsReceiptId);
        }

        public async Task<IList<GoodsReceipt>> GetReturnableGoodsReceiptsAsync(params GoodsReceiptStatus[] statuses)
        {
            return await _purchaseReturnUnitOfWork
                .GoodsReceiptRepository
                .GetGoodsReceiptsByStatusAsync(statuses.Length == 0
                    ? new[] { GoodsReceiptStatus.Received }
                    : statuses);
        }

        public async Task<string> GenerateReturnNoAsync()
        {
            var count = await _purchaseReturnUnitOfWork.PurchaseReturnRepository.GetCountAsync();

            string returnNo;
            do
            {
                count++;
                returnNo = $"PRT-{DateTime.Now:yyyyMM}-{count:D4}";
            }
            while (await _purchaseReturnUnitOfWork.PurchaseReturnRepository.IsReturnNoDuplicateAsync(returnNo));

            return returnNo;
        }

        public async Task ConfirmPurchaseReturnAsync(Guid id)
        {
            await _purchaseReturnUnitOfWork.BeginTransactionAsync();

            try
            {
                var purchaseReturn = await _purchaseReturnUnitOfWork
                    .PurchaseReturnRepository
                    .GetPurchaseReturnByIdAsync(id)
                    ?? throw new InvalidOperationException("Purchase return not found.");

                if (purchaseReturn.Status != PurchaseReturnStatus.Draft)
                {
                    throw new InvalidOperationException(
                        $"Only a draft purchase return can be confirmed. This one is already {purchaseReturn.Status.ToString().ToLower()}.");
                }

                var goodsReceipt = await _purchaseReturnUnitOfWork
                    .GoodsReceiptRepository
                    .GetGoodsReceiptByIdAsync(purchaseReturn.GoodsReceiptId)
                    ?? throw new InvalidOperationException("Goods receipt not found.");

                GuardReceiptCanReturn(goodsReceipt);

                var lines = purchaseReturn.PurchaseReturnItems?.ToList() ?? new List<PurchaseReturnItem>();

                // The quantities are checked again at confirmation time, because another
                // return may have taken the remaining amount since this draft was saved.
                await GuardAgainstOverReturningAsync(goodsReceipt, lines, purchaseReturn.Id);

                await ApplyConfirmationAsync(purchaseReturn, goodsReceipt, lines);

                await _purchaseReturnUnitOfWork.CommitTransactionAsync();
            }
            catch (InvalidOperationException)
            {
                await _purchaseReturnUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _purchaseReturnUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task CancelPurchaseReturnAsync(Guid id)
        {
            await _purchaseReturnUnitOfWork.BeginTransactionAsync();

            try
            {
                var purchaseReturn = await _purchaseReturnUnitOfWork
                    .PurchaseReturnRepository
                    .GetPurchaseReturnByIdAsync(id)
                    ?? throw new InvalidOperationException("Purchase return not found.");

                if (purchaseReturn.Status == PurchaseReturnStatus.Cancelled)
                {
                    throw new InvalidOperationException("This purchase return is already cancelled.");
                }

                var lines = purchaseReturn.PurchaseReturnItems?.ToList() ?? new List<PurchaseReturnItem>();

                if (purchaseReturn.Status == PurchaseReturnStatus.Returned)
                {
                    var goodsReceipt = await _purchaseReturnUnitOfWork
                        .GoodsReceiptRepository
                        .GetGoodsReceiptByIdAsync(purchaseReturn.GoodsReceiptId)
                        ?? throw new InvalidOperationException("Goods receipt not found.");

                    // What went back to the supplier on this return comes straight back in.
                    await MoveStockAsync(lines, increase: true);
                    await ApplyReceiptConsumptionAsync(goodsReceipt, lines, add: false);

                    // The credit the return gave the supplier is taken away again.
                    await AdjustSupplierBalanceAsync(purchaseReturn.SupplierId, purchaseReturn.TotalAmount);
                }

                purchaseReturn.Status = PurchaseReturnStatus.Cancelled;
                purchaseReturn.Updated = DateTime.Now;

                await _purchaseReturnUnitOfWork.PurchaseReturnRepository.EditAsync(purchaseReturn);
                await _purchaseReturnUnitOfWork.CommitTransactionAsync();
            }
            catch (InvalidOperationException)
            {
                await _purchaseReturnUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _purchaseReturnUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<IList<ReturnableLineDto>> GetReturnableLinesAsync(Guid goodsReceiptId,
            Guid? excludePurchaseReturnId = null)
        {
            var goodsReceipt = await _purchaseReturnUnitOfWork
                .GoodsReceiptRepository
                .GetGoodsReceiptByIdAsync(goodsReceiptId);

            if (goodsReceipt == null)
            {
                return new List<ReturnableLineDto>();
            }

            var pending = await GetPendingQuantitiesAsync(goodsReceiptId, excludePurchaseReturnId);

            var lines = new List<ReturnableLineDto>();

            foreach (var item in goodsReceipt.GoodsReceiptItems ?? new List<GoodsReceiptItem>())
            {
                // A line that only recorded a rejection never entered stock, so there is
                // nothing on it to send back.
                if (item.ReceivedQuantity <= 0)
                {
                    continue;
                }

                var pendingQuantity = pending.GetValueOrDefault(item.ProductId);
                var remaining = item.ReceivedQuantity - item.ReturnedQuantity - pendingQuantity;

                var product = item.Product
                    ?? await _purchaseReturnUnitOfWork.ProductRepository.GetByIdAsync(item.ProductId);

                lines.Add(new ReturnableLineDto
                {
                    ProductId = item.ProductId,
                    ProductName = product?.ProductName ?? string.Empty,
                    UnitName = product?.Unit?.UnitName ?? string.Empty,
                    ReceivedQuantity = item.ReceivedQuantity,
                    ReturnedQuantity = item.ReturnedQuantity,
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
        private async Task<ReturnContext> ValidateAsync(PurchaseReturn purchaseReturn, PurchaseReturn? existing)
        {
            var goodsReceipt = await _purchaseReturnUnitOfWork
                .GoodsReceiptRepository
                .GetGoodsReceiptByIdAsync(purchaseReturn.GoodsReceiptId)
                ?? throw new InvalidOperationException("Goods receipt not found.");

            GuardReceiptCanReturn(goodsReceipt);

            var returnDate = purchaseReturn.ReturnDate == default ? DateTime.Now : purchaseReturn.ReturnDate;

            if (returnDate.Date < goodsReceipt.ReceiptDate.Date)
            {
                throw new InvalidOperationException("The return date cannot be earlier than the goods receipt date.");
            }

            var grouped = purchaseReturn.PurchaseReturnItems?
                .Where(x => x.ProductId != Guid.Empty && x.ReturnQuantity > 0)
                .GroupBy(x => x.ProductId)
                .Select(g => new RequestedLine(
                    g.Key,
                    g.Sum(x => x.ReturnQuantity),
                    g.Select(x => x.Remarks).FirstOrDefault(x => !string.IsNullOrWhiteSpace(x)) ?? string.Empty))
                .ToList();

            if (grouped == null || grouped.Count == 0)
            {
                throw new InvalidOperationException("At least one line with a return quantity is required.");
            }

            var validated = new List<ValidatedLine>();

            foreach (var requested in grouped)
            {
                var receiptLine = goodsReceipt.GoodsReceiptItems?
                    .FirstOrDefault(x => x.ProductId == requested.ProductId)
                    ?? throw new InvalidOperationException(
                        "A product that is not part of this goods receipt cannot be returned.");

                var product = receiptLine.Product
                    ?? await _purchaseReturnUnitOfWork.ProductRepository.GetByIdAsync(requested.ProductId)
                    ?? throw new InvalidOperationException("Product not found.");

                if (requested.ReturnQuantity <= 0)
                {
                    throw new InvalidOperationException(
                        $"The return quantity of '{product.ProductName}' has to be greater than zero.");
                }

                // Stock is kept as whole units, so a return may not book a fraction.
                GuardWholeQuantity(product.ProductName, requested.ReturnQuantity);

                validated.Add(new ValidatedLine(
                    product.Id,
                    receiptLine.ReceivedQuantity,
                    requested.ReturnQuantity,
                    receiptLine.UnitPrice,
                    requested.Remarks));
            }

            await GuardAgainstOverReturningAsync(goodsReceipt,
                validated.Select(x => new PurchaseReturnItem
                {
                    ProductId = x.ProductId,
                    ReturnQuantity = x.ReturnQuantity
                }).ToList(),
                existing?.Id);

            return new ReturnContext(goodsReceipt, validated, returnDate);
        }

        /// <summary>
        /// Only goods that actually came in can go back, so a draft or cancelled
        /// receipt has nothing to return.
        /// </summary>
        private static void GuardReceiptCanReturn(GoodsReceipt goodsReceipt)
        {
            if (goodsReceipt.Status == GoodsReceiptStatus.Received)
            {
                return;
            }

            throw new InvalidOperationException(
                $"Goods cannot be returned against a {goodsReceipt.Status.ToString().ToLower()} goods receipt. " +
                "Only a received one holds stock that can go back.");
        }

        /// <summary>
        /// The received quantity is the ceiling. Draft returns count towards it too,
        /// so the same goods cannot be sent back twice while a draft is open.
        /// </summary>
        private async Task GuardAgainstOverReturningAsync(GoodsReceipt goodsReceipt,
            IList<PurchaseReturnItem> lines, Guid? excludePurchaseReturnId)
        {
            var pending = await GetPendingQuantitiesAsync(goodsReceipt.Id, excludePurchaseReturnId);

            foreach (var line in lines.Where(x => x.ReturnQuantity > 0))
            {
                var receiptLine = goodsReceipt.GoodsReceiptItems?
                    .FirstOrDefault(x => x.ProductId == line.ProductId)
                    ?? throw new InvalidOperationException(
                        "A product that is not part of this goods receipt cannot be returned.");

                var productName = receiptLine.Product?.ProductName ?? "This product";
                var remaining = receiptLine.ReceivedQuantity - receiptLine.ReturnedQuantity
                    - pending.GetValueOrDefault(line.ProductId);

                if (line.ReturnQuantity > remaining)
                {
                    throw new InvalidOperationException(
                        $"'{productName}' has only {(remaining < 0 ? 0 : remaining):N2} left to return on this receipt, " +
                        $"but {line.ReturnQuantity:N2} is being returned.");
                }
            }
        }

        /// <summary>
        /// Draft returns of the receipt, product by product. One return can be left
        /// out so an edit is measured against the right figure.
        /// </summary>
        private async Task<Dictionary<Guid, decimal>> GetPendingQuantitiesAsync(Guid goodsReceiptId,
            Guid? excludePurchaseReturnId)
        {
            var returns = await _purchaseReturnUnitOfWork
                .PurchaseReturnRepository
                .GetPurchaseReturnsByGoodsReceiptAsync(goodsReceiptId);

            return returns
                .Where(x => x.Status == PurchaseReturnStatus.Draft
                            && (!excludePurchaseReturnId.HasValue || x.Id != excludePurchaseReturnId.Value))
                .SelectMany(x => x.PurchaseReturnItems ?? new List<PurchaseReturnItem>())
                .GroupBy(x => x.ProductId)
                .ToDictionary(g => g.Key, g => g.Sum(x => x.ReturnQuantity));
        }

        /// <summary>
        /// Marks the return as returned, moves the stock and tells the receipt about
        /// it. Every caller already runs inside a transaction.
        /// </summary>
        private async Task ApplyConfirmationAsync(PurchaseReturn purchaseReturn, GoodsReceipt goodsReceipt,
            IList<PurchaseReturnItem> lines)
        {
            await MoveStockAsync(lines, increase: false);
            await ApplyReceiptConsumptionAsync(goodsReceipt, lines, add: true);

            // Goods going back are worth money, so the supplier is owed that much less.
            await AdjustSupplierBalanceAsync(purchaseReturn.SupplierId, -purchaseReturn.TotalAmount);

            purchaseReturn.Status = PurchaseReturnStatus.Returned;
            purchaseReturn.Updated = DateTime.Now;

            await _purchaseReturnUnitOfWork.PurchaseReturnRepository.EditAsync(purchaseReturn);
        }

        /// <summary>
        /// The single place a purchase return moves what is owed to a supplier, kept
        /// in step with the way the purchase invoice does it.
        /// </summary>
        private async Task AdjustSupplierBalanceAsync(Guid supplierId, decimal amount)
        {
            var supplier = await _purchaseReturnUnitOfWork
                .SupplierRepository
                .GetByIdAsync(supplierId)
                ?? throw new InvalidOperationException("Supplier not found.");

            supplier.CurrentOutstanding += amount;
            supplier.Updated = DateTime.Now;

            await _purchaseReturnUnitOfWork.SupplierRepository.EditAsync(supplier);
        }

        /// <summary>
        /// Takes the goods out of the warehouse, or puts them back when a return is
        /// cancelled. This is the only place a purchase return touches the product
        /// table.
        /// </summary>
        private async Task MoveStockAsync(IEnumerable<PurchaseReturnItem> lines, bool increase)
        {
            foreach (var line in lines.Where(x => x.ReturnQuantity > 0))
            {
                var product = await _purchaseReturnUnitOfWork
                    .ProductRepository
                    .GetByIdAsync(line.ProductId)
                    ?? throw new InvalidOperationException("Product not found.");

                GuardWholeQuantity(product.ProductName, line.ReturnQuantity);

                var quantity = (int)line.ReturnQuantity;

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
                            "cannot be sent back. The goods have probably already been sold.");
                    }

                    product.CurrentStock -= quantity;
                }

                product.Updated = DateTime.Now;

                await _purchaseReturnUnitOfWork.ProductRepository.EditAsync(product);
            }
        }

        /// <summary>
        /// Moves the returned quantity on the receipt lines, which is what caps every
        /// later return of the same receipt.
        /// </summary>
        private async Task ApplyReceiptConsumptionAsync(GoodsReceipt goodsReceipt,
            IEnumerable<PurchaseReturnItem> lines, bool add)
        {
            foreach (var line in lines.Where(x => x.ReturnQuantity > 0))
            {
                var receiptLine = goodsReceipt.GoodsReceiptItems?
                    .FirstOrDefault(x => x.ProductId == line.ProductId);

                if (receiptLine == null)
                {
                    continue;
                }

                receiptLine.ReturnedQuantity += add ? line.ReturnQuantity : -line.ReturnQuantity;

                if (receiptLine.ReturnedQuantity < 0)
                {
                    receiptLine.ReturnedQuantity = 0;
                }

                await _purchaseReturnUnitOfWork.GoodsReceiptItemRepository.EditAsync(receiptLine);
            }

            goodsReceipt.Updated = DateTime.Now;

            await _purchaseReturnUnitOfWork.GoodsReceiptRepository.EditAsync(goodsReceipt);
        }

        private static void GuardWholeQuantity(string productName, decimal quantity)
        {
            if (quantity != Math.Floor(quantity))
            {
                throw new InvalidOperationException(
                    $"Stock is kept in whole units, so '{productName}' cannot be returned in fractions.");
            }
        }

        private static List<PurchaseReturnItem> BuildLines(IList<ValidatedLine> lines, Guid purchaseReturnId)
        {
            return lines.Select(x => new PurchaseReturnItem
            {
                Id = Guid.NewGuid(),
                PurchaseReturnId = purchaseReturnId,
                ProductId = x.ProductId,
                ReceivedQuantity = x.ReceivedQuantity,
                ReturnQuantity = x.ReturnQuantity,
                UnitPrice = x.UnitPrice,
                LineTotal = Math.Round(x.ReturnQuantity * x.UnitPrice, 2),
                Remarks = x.Remarks
            }).ToList();
        }

        private sealed record RequestedLine(Guid ProductId, decimal ReturnQuantity, string Remarks);

        private sealed record ValidatedLine(Guid ProductId, decimal ReceivedQuantity, decimal ReturnQuantity,
            decimal UnitPrice, string Remarks);

        private sealed record ReturnContext(
            GoodsReceipt GoodsReceipt,
            IList<ValidatedLine> Lines,
            DateTime ReturnDate);
    }
}
