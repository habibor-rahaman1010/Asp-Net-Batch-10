using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities.StockAdjustmentEntites;
using DevSkill.Inventory.Domain.Entities.StockTransferEntities;
using DevSkill.Inventory.Domain.RepositoryContracts;
using DevSkill.Inventory.Infrastructure.Data;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class StockAdjustmentItemRepository : Repository<StockAdjustmentItem, Guid>, IStockAdjustmentItemRepository
    {
        private readonly InventoryDbContext _inventoryDbContext;
        public StockAdjustmentItemRepository(InventoryDbContext inventoryDbContext) : base(inventoryDbContext)
        {
            _inventoryDbContext = inventoryDbContext;
        }

        public Task<(IList<StockTransferItem> data, int total, int totalDisplay)> GetStockAdjustmentListAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            try
            {

            }
            catch(Exception ex)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }
    }
}
