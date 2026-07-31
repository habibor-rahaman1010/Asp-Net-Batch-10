using DevSkill.Inventory.Domain.Entities.StockTransferEntities;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface IStockTransferRepository : IRepositoryBase<StockTransfer, Guid>
    {
        public Task<StockTransfer> GetStockTransferByIdAsync(Guid id);
        public Task<(IList<StockTransfer> data, int total, int totalDisplay)> GetStockTransferListAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order);
    }
}
