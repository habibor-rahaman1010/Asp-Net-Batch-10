using DevSkill.Inventory.Domain.Entities.StockAdjustmentEntites;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface IStockAdjustmentItemRepository
    {
        public Task<(IList<StockAdjustmentItem> data, int total, int totalDisplay)> GetStockAdjustmentListAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order);
    }
}
