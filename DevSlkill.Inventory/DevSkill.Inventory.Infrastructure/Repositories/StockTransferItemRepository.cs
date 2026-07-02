using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities.StockTransferEntities;
using DevSkill.Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class StockTransferItemRepository : Repository<StockTransferItem, Guid>, IStockTransferItemRepository
    {
        private readonly InventoryDbContext _inventoryDbContext;

        public StockTransferItemRepository(InventoryDbContext inventoryDbContext) : base(inventoryDbContext)
        {
            _inventoryDbContext = inventoryDbContext;
        }

        public async Task<(IList<StockTransferItem> data, int total, int totalDisplay)> GetStockTransferListAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(search.Value))
                {
                    return await GetDynamicAsync(null, order, x => x
                        .Include(i => i.Product)
                        .Include(i => i.StockTransfer)
                        .ThenInclude(t => t.FromWarehouse)
                        .Include(i => i.StockTransfer)
                        .ThenInclude(t => t.ToWarehouse), pageIndex, pageSize, true);
                }
                else
                {
                    return await GetDynamicAsync(x => x.Product.ProductName.Contains(search.Value), order, null, pageIndex, pageSize, true);
                }
            }
            catch(Exception ex)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }
    }
}
