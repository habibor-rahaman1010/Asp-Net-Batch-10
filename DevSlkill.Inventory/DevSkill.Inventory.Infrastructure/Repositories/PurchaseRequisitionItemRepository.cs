using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using DevSkill.Inventory.Domain.RepositoryContracts;
using DevSkill.Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class PurchaseRequisitionItemRepository
        : Repository<PurchaseRequisitionItem, Guid>, IPurchaseRequisitionItemRepository
    {
        private readonly InventoryDbContext _inventoryDbContext;

        public PurchaseRequisitionItemRepository(InventoryDbContext inventoryDbContext) : base(inventoryDbContext)
        {
            _inventoryDbContext = inventoryDbContext;
        }

        public async Task<IList<PurchaseRequisitionItem>> GetItemsByRequisitionAsync(Guid purchaseRequisitionId)
        {
            return await _inventoryDbContext.PurchaseRequisitionItems
                .Include(x => x.Product)
                    .ThenInclude(x => x!.Unit)
                .Where(x => x.PurchaseRequisitionId == purchaseRequisitionId)
                .ToListAsync();
        }

        public Task RemoveRangeAsync(IList<PurchaseRequisitionItem> items)
        {
            try
            {
                _inventoryDbContext.PurchaseRequisitionItems.RemoveRange(items);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }
    }
}
