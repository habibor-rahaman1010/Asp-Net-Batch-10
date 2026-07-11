using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities.StockTransferEntities;

namespace DevSkill.Inventory.Application.ServicesContract
{
    public interface IStockTransferManagementService
    {
        public Task<bool> CreateStockTransferAsync(StockTransfer model);
        public Task<(IList<StockTransferItem> data, int total, int totalDisplay)> GetStockTransferListAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order);
    }
}
