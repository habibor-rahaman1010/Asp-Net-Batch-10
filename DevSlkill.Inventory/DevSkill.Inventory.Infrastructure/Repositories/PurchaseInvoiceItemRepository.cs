using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using DevSkill.Inventory.Domain.RepositoryContracts;
using DevSkill.Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class PurchaseInvoiceItemRepository : Repository<PurchaseInvoiceItem, Guid>, IPurchaseInvoiceItemRepository
    {
        private readonly InventoryDbContext _inventoryDbContext;

        public PurchaseInvoiceItemRepository(InventoryDbContext inventoryDbContext) : base(inventoryDbContext)
        {
            _inventoryDbContext = inventoryDbContext;
        }

        public async Task<IList<PurchaseInvoiceItem>> GetItemsByPurchaseInvoiceAsync(Guid purchaseInvoiceId)
        {
            return await _inventoryDbContext.PurchaseInvoiceItems
                .Include(x => x.Product)
                    .ThenInclude(x => x!.Unit)
                .Where(x => x.PurchaseInvoiceId == purchaseInvoiceId)
                .ToListAsync();
        }

        public Task RemoveRangeAsync(IList<PurchaseInvoiceItem> items)
        {
            try
            {
                _inventoryDbContext.PurchaseInvoiceItems.RemoveRange(items);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }
    }
}
