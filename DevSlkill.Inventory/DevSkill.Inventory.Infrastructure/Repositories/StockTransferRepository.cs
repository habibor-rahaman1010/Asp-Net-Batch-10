using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities.StockTransferEntities;
using DevSkill.Inventory.Domain.RepositoryContracts;
using DevSkill.Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class StockTransferRepository : Repository<StockTransfer, Guid>, IStockTransferRepository
    {
        private readonly InventoryDbContext _inventoryDbContext;
        public StockTransferRepository(InventoryDbContext context) : base(context)
        {
            _inventoryDbContext = context;
        }

        public async Task<StockTransfer> GetStockTransferByIdAsync(Guid id)
        {
            try
            {
                return await _inventoryDbContext.StockTransfers
                .Include(x => x.FromWarehouse)
                .Include(x => x.ToWarehouse)
                .Include(x => x.StockTransferItems)
                    .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<(IList<StockTransfer> data, int total, int totalDisplay)> GetStockTransferListAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(search.Value))
                {
                    return await GetDynamicAsync(null, order, x => x
                        .Include(n => n.FromWarehouse)
                        .Include(n => n.ToWarehouse)
                        .Include(x => x.StockTransferItems)
                        .ThenInclude(x => x.Product), pageIndex, pageSize, true);
                }
                else
                {
                    return await GetDynamicAsync(x => x.TransferNo.Contains(search.Value), order, null, pageIndex, pageSize, true);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }
    }
}
