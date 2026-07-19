using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities.StockTransferEntities;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public interface IStockTransferItemRepository
    {
        public Task<(IList<StockTransferItem> data, int total, int totalDisplay)> GetStockTransferListAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order);
    }
}
