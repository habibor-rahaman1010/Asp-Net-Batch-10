using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.RepositoryContracts;
using DevSkill.Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class SalesReturnItemRepository : Repository<SalesReturnItem, Guid>, ISalesReturnItemRepository
    {
        private readonly InventoryDbContext _inventoryDbContext;

        public SalesReturnItemRepository(InventoryDbContext inventoryDbContext) : base(inventoryDbContext)
        {
            _inventoryDbContext = inventoryDbContext;
        }

        public async Task<IList<SalesReturnItem>> GetItemsBySalesReturnAsync(Guid salesReturnId)
        {
            return await _inventoryDbContext.SalesReturnItems
                .Include(x => x.Product)
                    .ThenInclude(x => x!.Unit)
                .Where(x => x.SalesReturnId == salesReturnId)
                .ToListAsync();
        }

        public Task RemoveRangeAsync(IList<SalesReturnItem> items)
        {
            try
            {
                _inventoryDbContext.SalesReturnItems.RemoveRange(items);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }
    }
}
