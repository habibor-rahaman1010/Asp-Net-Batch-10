using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities.StockTransferEntities;
using DevSkill.Inventory.Domain.RepositoryContracts;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public interface IStockTransferItemRepository : IRepositoryBase<StockTransferItem, Guid>
    {
        public Task<(IList<StockTransferItem> data, int total, int totalDisplay)> GetStockTransferListAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order);
    }
}
