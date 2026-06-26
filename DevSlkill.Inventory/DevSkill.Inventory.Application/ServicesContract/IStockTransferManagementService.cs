
using DevSkill.Inventory.Domain.Entities.StockTransferEntities;

namespace DevSkill.Inventory.Application.ServicesContract
{
    public interface IStockTransferManagementService
    {
        public Task<bool> CreateStockTransferAsync(StockTransfer model);
    }
}
