using DevSkill.Inventory.Domain.Entities.StockAdjustmentEntites;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface IStockAdjustmentRepository : IRepositoryBase<StockAdjustment, Guid>
    {
        Task<(IList<StockAdjustment> data, int total, int totalDisplay)> GetPagedStockAdjustmentsAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order);
        //Task<bool> HasStockAdjustmentsAsync(Guid productId);
        Task<StockAdjustment> GetStockAdjustmentyByIdAsync(Guid id);
    }
}
