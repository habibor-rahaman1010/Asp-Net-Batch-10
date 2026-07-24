using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities.StockAdjustmentEntites;
using DevSkill.Inventory.Domain.RepositoryContracts;
using DevSkill.Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class StockAdjustmentItemRepository : Repository<StockAdjustmentItem, Guid>, IStockAdjustmentItemRepository
    {
        private readonly InventoryDbContext _inventoryDbContext;
        public StockAdjustmentItemRepository(InventoryDbContext inventoryDbContext) : base(inventoryDbContext)
        {
            _inventoryDbContext = inventoryDbContext;
        }

        public async Task<(IList<StockAdjustmentItem> data, int total, int totalDisplay)> GetStockAdjustmentListAsync( int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(search.Value))
                {
                    return await GetDynamicAsync(
                        null,
                        order,
                        x => x
                            .Include(i => i.Product)
                                .ThenInclude(p => p.BusinessLocation)
                            .Include(i => i.StockAdjustment)
                                .ThenInclude(sa => sa.BusinessLocation)
                            .Include(ad => ad.StockAdjustment)
                                .ThenInclude(n => n.AdjustmentType),
                        pageIndex,
                        pageSize,
                        true);
                }
                else
                {
                    return await GetDynamicAsync(
                        x =>
                            x.StockAdjustment!.ReferenceNo.Contains(search.Value) ||
                            x.Product!.ProductName.Contains(search.Value),
                        order,
                        x => x
                            .Include(i => i.Product)
                                .ThenInclude(p => p.BusinessLocation)
                            .Include(i => i.StockAdjustment)
                                .ThenInclude(sa => sa.BusinessLocation)
                            .Include(ad => ad.StockAdjustment)
                                .ThenInclude(n => n.AdjustmentType),
                        pageIndex,
                        pageSize,
                        true);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Exception Occured.", ex);
            }
        }

        public Task RemoveRangeAsync(IList<StockAdjustmentItem> stockAdjustmentItems)
        {
            try
            {
                _inventoryDbContext.StockAdjustmentItems.RemoveRange(stockAdjustmentItems);
                return Task.CompletedTask;
            }
            catch(Exception ex)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }
    }
}
