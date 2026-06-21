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

        public async Task AddStockAdjustmentAsync(StockAdjustment stockAdjustment)
        {
            await _stockAdjustmentUnitOfWork.StockAdjustmentRepository.AddAsync(stockAdjustment);
            await _stockAdjustmentUnitOfWork.SaveAsync();
        }

        public async Task DeleteStockAdjustmentAsync(Guid id)
        {
            await _stockAdjustmentUnitOfWork.StockAdjustmentRepository.RemoveAsync(id);
            await _stockAdjustmentUnitOfWork.SaveAsync();
        }

        public async Task<(IList<StockAdjustment> data, int total, int totalDisplay)> GetAllStockAdjustmentAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            return await _stockAdjustmentUnitOfWork.StockAdjustmentRepository.GetPagedStockAdjustmentsAsync(pageIndex, pageSize, search, order);
        }

        public async Task<StockAdjustment> GetStockAdjustmentyByIdAsync(Guid id)
        {
            return await _stockAdjustmentUnitOfWork.StockAdjustmentRepository.GetStockAdjustmentyByIdAsync(id);
        }
    }
}
