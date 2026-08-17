using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using DevSkill.Inventory.Domain.RepositoryContracts;
using DevSkill.Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class PurchaseReturnItemRepository : Repository<PurchaseReturnItem, Guid>, IPurchaseReturnItemRepository
    {
        private readonly InventoryDbContext _inventoryDbContext;

        public PurchaseReturnItemRepository(InventoryDbContext inventoryDbContext) : base(inventoryDbContext)
        {
            _inventoryDbContext = inventoryDbContext;
        }

        public async Task<IList<PurchaseReturnItem>> GetItemsByPurchaseReturnAsync(Guid purchaseReturnId)
        {
            return await _inventoryDbContext.PurchaseReturnItems
                .Include(x => x.Product)
                    .ThenInclude(x => x!.Unit)
                .Where(x => x.PurchaseReturnId == purchaseReturnId)
                .ToListAsync();
        }

        public Task RemoveRangeAsync(IList<PurchaseReturnItem> items)
        {
            try
            {
                _inventoryDbContext.PurchaseReturnItems.RemoveRange(items);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }
    }
}
