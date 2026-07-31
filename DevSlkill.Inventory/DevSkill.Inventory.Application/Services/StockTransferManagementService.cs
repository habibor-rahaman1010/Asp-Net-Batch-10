using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities.StockTransferEntities;
using DevSkill.Inventory.Domain.UnitOfWorkContracts;

namespace DevSkill.Inventory.Application.Services
{
    public class StockTransferManagementService : IStockTransferManagementService
    {
        private readonly IInventoryUnitOfWork _stockTransferUnitOfWork;

        public StockTransferManagementService(IInventoryUnitOfWork unitOfWork)
        {
            _stockTransferUnitOfWork = unitOfWork;
        }

        public async Task<bool> CreateStockTransferAsync(StockTransfer model)
        {
            await _stockTransferUnitOfWork.StockTransferRepository.AddAsync(model);
            await _stockTransferUnitOfWork.SaveAsync();

            return true;
        }

        public async Task<bool> UpdateStockTransferAsync(StockTransfer stockTransfer)
        {
            await _stockTransferUnitOfWork.BeginTransactionAsync();

            try
            {
                // 1. Fetch existing master record with items from DB
                var existingStockTransfer = await _stockTransferUnitOfWork
                    .StockTransferRepository
                    .GetStockTransferByIdAsync(stockTransfer.Id);

                if (existingStockTransfer == null)
                {
                    throw new Exception("Stock Transfer record not found.");
                }

                // 2. Rollback stock for old items
                foreach (var item in existingStockTransfer.StockTransferItems.ToList())
                {
                    var product = await _stockTransferUnitOfWork
                        .ProductRepository
                        .GetByIdAsync(item.ProductId);

                    if (product != null)
                    {
                        // পুরানো FromWarehouse-এ স্টক যোগ এবং ToWarehouse-এ স্টক কমানোর লজিক
                        // (যদি প্রোডাক্ট লেভেলে Stock হ্যান্ডেল করেন):
                        product.CurrentStock += (int)item.Quantity;
                        await _stockTransferUnitOfWork.ProductRepository.EditAsync(product);
                    }
                }

                // 3. Remove old detail records from DB
                await _stockTransferUnitOfWork
                    .StockTransferItemRepository
                    .RemoveRangeAsync(existingStockTransfer.StockTransferItems.ToList());

                // 4. Update master record properties (Entity Instance পরিবর্তন না করে DB-র টাতে Assign করা)
                existingStockTransfer.FromWarehouseId = stockTransfer.FromWarehouseId;
                existingStockTransfer.ToWarehouseId = stockTransfer.ToWarehouseId;
                existingStockTransfer.Status = stockTransfer.Status;
                existingStockTransfer.Remarks = stockTransfer.Remarks;

                // 5. Add new details and update stock for new items
                foreach (var item in stockTransfer.StockTransferItems.ToList())
                {
                    var product = await _stockTransferUnitOfWork
                        .ProductRepository
                        .GetByIdAsync(item.ProductId);

                    if (product != null)
                    {
                        // নতুন Stock Transfer অনুযায়ী স্টক অ্যাডজাস্ট করা
                        product.CurrentStock -= (int)item.Quantity;
                        await _stockTransferUnitOfWork.ProductRepository.EditAsync(product);
                    }

                    var detail = new StockTransferItem
                    {
                        Id = Guid.NewGuid(),
                        StockTransferId = existingStockTransfer.Id,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity
                    };

                    await _stockTransferUnitOfWork
                        .StockTransferItemRepository
                        .AddAsync(detail);
                }

                // 6. Save updated Master
                await _stockTransferUnitOfWork
                    .StockTransferRepository
                    .EditAsync(existingStockTransfer);

                // 7. Commit Transaction
                await _stockTransferUnitOfWork.CommitTransactionAsync();

                return true;
            }
            catch (Exception ex)
            {
                await _stockTransferUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occurred: ", ex);
            }
        }

        public async Task<StockTransfer> GetStockTransferByIdAsync(Guid id)
        {
            try
            {
                return await _stockTransferUnitOfWork.StockTransferRepository.GetStockTransferByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<(IList<StockTransferItem> data, int total, int totalDisplay)> GetStockTransferListAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            try
            {
                return await _stockTransferUnitOfWork.StockTransferItemRepository.GetStockTransferListAsync(pageIndex, pageSize, search, order);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }
    }
}
