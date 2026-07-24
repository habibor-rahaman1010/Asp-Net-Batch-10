using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities.StockAdjustmentEntites;
using DevSkill.Inventory.Domain.UnitOfWorkContracts;

namespace DevSkill.Inventory.Application.Services
{
    public class StockAdjustmentManagementService : IStockAdjustmentManagementService
    {
        private readonly IInventoryUnitOfWork _stockAdjustmentUnitOfWork;
        public StockAdjustmentManagementService(IInventoryUnitOfWork stockAdjustmentUnitOfWork)
        {
            _stockAdjustmentUnitOfWork = stockAdjustmentUnitOfWork;
        }

        public async Task<bool> AddStockAdjustmentAsync(StockAdjustment stockAdjustment)
        {
            await _stockAdjustmentUnitOfWork.BeginTransactionAsync();

            try
            {
                var adjustmentType = await _stockAdjustmentUnitOfWork
                .AdjustmentTypeRepository
                .GetByIdAsync(stockAdjustment.AdjustmentTypeId);

                foreach (var item in stockAdjustment.StockAdjustmentItems!)
                {
                    var product = await _stockAdjustmentUnitOfWork
                        .ProductRepository
                        .GetByIdAsync(item.ProductId);

                    product.CurrentStock += item.AdjustmentQuantity * adjustmentType.Sign;

                    await _stockAdjustmentUnitOfWork.ProductRepository.EditAsync(product);
                }

                stockAdjustment.TotalAmount = stockAdjustment.StockAdjustmentItems.Sum(x => x.TotalAmount);

                await _stockAdjustmentUnitOfWork.StockAdjustmentRepository.AddAsync(stockAdjustment);
                await _stockAdjustmentUnitOfWork.CommitTransactionAsync();
                return true;

            }
            catch (Exception ex)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<bool> UpdateStockAdjustmentAsync(StockAdjustment stockAdjustment)
        {
            await _stockAdjustmentUnitOfWork.BeginTransactionAsync();

            try
            {
                var existingStockAdjustment = await _stockAdjustmentUnitOfWork
                    .StockAdjustmentRepository
                    .GetStockAdjustmentByIdAsync(stockAdjustment.Id);

                if (existingStockAdjustment == null)
                {
                    throw new Exception("Stock Adjustment not found.");
                }

                // Old Adjustment Type
                var oldAdjustmentType = await _stockAdjustmentUnitOfWork
                    .AdjustmentTypeRepository
                    .GetByIdAsync(existingStockAdjustment.AdjustmentTypeId);

                // Rollback previous stock
                foreach (var item in existingStockAdjustment.StockAdjustmentItems.ToList())
                {
                    var product = await _stockAdjustmentUnitOfWork
                        .ProductRepository
                        .GetByIdAsync(item.ProductId);

                    product.CurrentStock -= item.AdjustmentQuantity * oldAdjustmentType.Sign;

                    await _stockAdjustmentUnitOfWork.ProductRepository.EditAsync(product);
                }

                // Delete old details
                await _stockAdjustmentUnitOfWork
                    .StockAdjustmentItemRepository
                    .RemoveRangeAsync(existingStockAdjustment.StockAdjustmentItems.ToList());

                // Update master
                existingStockAdjustment.BusinessLocationId = stockAdjustment.BusinessLocationId;
                existingStockAdjustment.AdjustmentTypeId = stockAdjustment.AdjustmentTypeId;
                existingStockAdjustment.TotalAmountRecover = stockAdjustment.TotalAmountRecover;
                existingStockAdjustment.Reason = stockAdjustment.Reason;
                existingStockAdjustment.AddedBy = stockAdjustment.AddedBy;

                var newAdjustmentType = await _stockAdjustmentUnitOfWork
                    .AdjustmentTypeRepository
                    .GetByIdAsync(stockAdjustment.AdjustmentTypeId);

                decimal totalAmount = 0;

                // Insert new details
                foreach (var item in stockAdjustment.StockAdjustmentItems.ToList())
                {
                    var product = await _stockAdjustmentUnitOfWork
                        .ProductRepository
                        .GetByIdAsync(item.ProductId);

                    product.CurrentStock += item.AdjustmentQuantity * newAdjustmentType.Sign;

                    await _stockAdjustmentUnitOfWork.ProductRepository.EditAsync(product);

                    var detail = new StockAdjustmentItem
                    {
                        Id = Guid.NewGuid(),
                        StockAdjustmentId = existingStockAdjustment.Id,
                        ProductId = item.ProductId,
                        AdjustmentQuantity = item.AdjustmentQuantity,
                        UnitPrice = item.UnitPrice,
                        TotalAmount = item.TotalAmount
                    };

                    totalAmount += detail.TotalAmount;

                    await _stockAdjustmentUnitOfWork
                        .StockAdjustmentItemRepository
                        .AddAsync(detail);
                }

                existingStockAdjustment.TotalAmount = totalAmount;

                await _stockAdjustmentUnitOfWork
                    .StockAdjustmentRepository
                    .EditAsync(existingStockAdjustment);

                await _stockAdjustmentUnitOfWork.CommitTransactionAsync();

                return true;
            }
            catch (Exception ex)
            {
                await _stockAdjustmentUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<bool> DeleteStockAdjustmentAsync(Guid id)
        {
            await _stockAdjustmentUnitOfWork.BeginTransactionAsync();

            try
            {
                var stockAdjustment = await _stockAdjustmentUnitOfWork
                    .StockAdjustmentRepository
                    .GetStockAdjustmentByIdAsync(id);

                if (stockAdjustment == null)
                {
                    throw new InvalidOperationException("Stock Adjustment not found.");
                }

                var adjustmentType = await _stockAdjustmentUnitOfWork
                    .AdjustmentTypeRepository
                    .GetByIdAsync(stockAdjustment.AdjustmentTypeId);

                if (adjustmentType == null)
                {
                    throw new InvalidOperationException("Adjustment Type not found.");
                }

                // Rollback stock
                foreach (var item in stockAdjustment.StockAdjustmentItems.ToList())
                {
                    var product = await _stockAdjustmentUnitOfWork
                        .ProductRepository
                        .GetByIdAsync(item.ProductId);

                    if (product == null)
                        throw new InvalidOperationException("Product not found.");

                    product.CurrentStock -= item.AdjustmentQuantity * adjustmentType.Sign;

                    await _stockAdjustmentUnitOfWork
                        .ProductRepository
                        .EditAsync(product);
                }

                // Delete Details
                await _stockAdjustmentUnitOfWork
                    .StockAdjustmentItemRepository
                    .RemoveRangeAsync(stockAdjustment.StockAdjustmentItems.ToList());

                // Delete Master
                await _stockAdjustmentUnitOfWork
                    .StockAdjustmentRepository
                    .RemoveAsync(stockAdjustment);

                await _stockAdjustmentUnitOfWork.CommitTransactionAsync();

                return true;
            }
            catch(Exception ex)
            {
                await _stockAdjustmentUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<(IList<StockAdjustmentItem> data, int total, int totalDisplay)> GetAllStockAdjustmentAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            return await _stockAdjustmentUnitOfWork.StockAdjustmentItemRepository.GetStockAdjustmentListAsync(pageIndex, pageSize, search, order);
        }

        public async Task<StockAdjustment> GetStockAdjustmentByIdAsync(Guid id)
        {
            return await _stockAdjustmentUnitOfWork.StockAdjustmentRepository.GetStockAdjustmentByIdAsync(id);
        }
    }
}