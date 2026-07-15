using DevSkill.Inventory.Domain.Entities.StockTransferEntities;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface IStockAdjustmentItemRepository
    {
        public Task<(IList<StockTransferItem> data, int total, int totalDisplay)> GetStockAdjustmentListAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order);
    }
}
