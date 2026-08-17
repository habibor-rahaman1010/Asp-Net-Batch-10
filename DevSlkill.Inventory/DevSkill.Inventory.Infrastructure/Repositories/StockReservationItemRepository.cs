using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.RepositoryContracts;
using DevSkill.Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class StockReservationItemRepository : Repository<StockReservationItem, Guid>, IStockReservationItemRepository
    {
        private readonly InventoryDbContext _inventoryDbContext;

        public StockReservationItemRepository(InventoryDbContext inventoryDbContext) : base(inventoryDbContext)
        {
            _inventoryDbContext = inventoryDbContext;
        }

        public async Task<IList<StockReservationItem>> GetItemsByStockReservationAsync(Guid stockReservationId)
        {
            return await _inventoryDbContext.StockReservationItems
                .Include(x => x.Product)
                    .ThenInclude(x => x!.Unit)
                .Where(x => x.StockReservationId == stockReservationId)
                .ToListAsync();
        }

        public Task RemoveRangeAsync(IList<StockReservationItem> items)
        {
            try
            {
                _inventoryDbContext.StockReservationItems.RemoveRange(items);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }
    }
}
