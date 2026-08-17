using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using DevSkill.Inventory.Domain.RepositoryContracts;
using DevSkill.Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class PurchaseOrderItemRepository : Repository<PurchaseOrderItem, Guid>, IPurchaseOrderItemRepository
    {
        private readonly InventoryDbContext _inventoryDbContext;

        public PurchaseOrderItemRepository(InventoryDbContext inventoryDbContext) : base(inventoryDbContext)
        {
            _inventoryDbContext = inventoryDbContext;
        }

        public async Task<IList<PurchaseOrderItem>> GetItemsByPurchaseOrderAsync(Guid purchaseOrderId)
        {
            return await _inventoryDbContext.PurchaseOrderItems
                .Include(x => x.Product)
                    .ThenInclude(x => x!.Unit)
                .Where(x => x.PurchaseOrderId == purchaseOrderId)
                .ToListAsync();
        }

        public Task RemoveRangeAsync(IList<PurchaseOrderItem> items)
        {
            try
            {
                _inventoryDbContext.PurchaseOrderItems.RemoveRange(items);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }
    }
}
