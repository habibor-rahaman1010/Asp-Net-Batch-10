
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Entities.StockTransferEntities;

namespace DevSkill.Inventory.Application.ServicesContract
{
    public interface IStockTransferManagementService
    {
        public Task<bool> CreateStockTransferAsync(StockTransfer model);
        public Task<(IList<StockTransfer> data, int total, int totalDisplay)> GetStockTransferListAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order);
    }
}
