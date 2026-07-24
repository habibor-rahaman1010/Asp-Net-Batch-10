using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities.StockAdjustmentEntites;
using DevSkill.Inventory.Domain.RepositoryContracts;
using DevSkill.Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class StockAdjustmentRepository : Repository<StockAdjustment, Guid>, IStockAdjustmentRepository
    {
        private readonly InventoryDbContext _inventoryDbContext; 
        public StockAdjustmentRepository(InventoryDbContext context) : base(context)
        {
            _inventoryDbContext = context;
        }

        public async Task<StockAdjustment?> GetStockAdjustmentByIdAsync(Guid id)
        {
            return await _inventoryDbContext.StockAdjustments
                .Include(x => x.BusinessLocation)
                .Include(x => x.AdjustmentType)
                .Include(x => x.StockAdjustmentItems)
                    .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<(IList<StockAdjustment> data, int total, int totalDisplay)> GetPagedStockAdjustmentsAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            if (string.IsNullOrWhiteSpace(search.Value))
            {
                return await GetDynamicAsync(null, order, 
                    x => x.Include(l => l.BusinessLocation).Include(y => y.AdjustmentType), 
                    pageIndex, pageSize, true);
            }
            else
            {
                return await GetDynamicAsync(x => x.BusinessLocation.LocationName.Contains(search.Value) 
                || x.AdjustmentType.AdjustmentTypeName.Contains(search.Value), 
                order, x => x.Include(l => l.BusinessLocation).Include(y => y.AdjustmentType), pageIndex, pageSize, true);
            }
        }

        //public async Task<bool> HasStockAdjustmentsAsync(Guid productId)
        //{
        //    return await _inventoryDbContext.StockAdjustments.AnyAsync(sa => sa.ProductId == productId);
        //}
    }
}
