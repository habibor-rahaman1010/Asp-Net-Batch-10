using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities.StockTransferEntities;
using DevSkill.Inventory.Domain.Enums;
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
