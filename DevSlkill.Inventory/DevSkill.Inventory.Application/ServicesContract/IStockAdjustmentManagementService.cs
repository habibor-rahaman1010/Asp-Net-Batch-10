using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities.StockAdjustmentEntites;

namespace DevSkill.Inventory.Application.ServicesContract
{
    public interface IStockAdjustmentManagementService
    {
        Task<(IList<StockAdjustment> data, int total, int totalDisplay)> GetAllStockAdjustmentAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order);
        Task AddStockAdjustmentAsync(StockAdjustment stockAdjustment);
        Task DeleteStockAdjustmentAsync(Guid id);
        Task<StockAdjustment> GetStockAdjustmentyByIdAsync(Guid id); 
    }
}
