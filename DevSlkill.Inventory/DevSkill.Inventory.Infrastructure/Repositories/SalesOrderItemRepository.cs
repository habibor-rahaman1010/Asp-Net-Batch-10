using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.RepositoryContracts;
using DevSkill.Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class SalesOrderItemRepository : Repository<SalesOrderItem, Guid>, ISalesOrderItemRepository
    {
        private readonly InventoryDbContext _inventoryDbContext;

        public SalesOrderItemRepository(InventoryDbContext inventoryDbContext) : base(inventoryDbContext)
        {
            _inventoryDbContext = inventoryDbContext;
        }

        public async Task<IList<SalesOrderItem>> GetItemsBySalesOrderAsync(Guid salesOrderId)
        {
            return await _inventoryDbContext.SalesOrderItems
                .Include(x => x.Product)
                    .ThenInclude(x => x!.Unit)
                .Where(x => x.SalesOrderId == salesOrderId)
                .ToListAsync();
        }

        public Task RemoveRangeAsync(IList<SalesOrderItem> items)
        {
            try
            {
                _inventoryDbContext.SalesOrderItems.RemoveRange(items);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }
    }
}
