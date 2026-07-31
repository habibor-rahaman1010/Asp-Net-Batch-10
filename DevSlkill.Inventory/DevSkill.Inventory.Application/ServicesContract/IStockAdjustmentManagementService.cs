using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities.StockAdjustmentEntites;

namespace DevSkill.Inventory.Application.ServicesContract
{
    public interface IStockAdjustmentManagementService
    {
        public Task<(IList<StockAdjustmentItem> data, int total, int totalDisplay)> GetAllStockAdjustmentAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order);
        public Task<bool> AddStockAdjustmentAsync(StockAdjustment stockAdjustment);
        public Task<bool> DeleteStockAdjustmentAsync(Guid id);
        public Task<bool> UpdateStockAdjustmentAsync(StockAdjustment stockAdjustment);
        public Task<StockAdjustment> GetStockAdjustmentByIdAsync(Guid id); 
    }
}