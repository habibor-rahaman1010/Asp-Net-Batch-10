using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using DevSkill.Inventory.Domain.RepositoryContracts;
using DevSkill.Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class SupplierQuotationItemRepository
        : Repository<SupplierQuotationItem, Guid>, ISupplierQuotationItemRepository
    {
        private readonly InventoryDbContext _inventoryDbContext;

        public SupplierQuotationItemRepository(InventoryDbContext inventoryDbContext) : base(inventoryDbContext)
        {
            _inventoryDbContext = inventoryDbContext;
        }

        public async Task<IList<SupplierQuotationItem>> GetItemsBySupplierQuotationAsync(Guid supplierQuotationId)
        {
            return await _inventoryDbContext.SupplierQuotationItems
                .Include(x => x.Product)
                    .ThenInclude(x => x!.Unit)
                .Where(x => x.SupplierQuotationId == supplierQuotationId)
                .ToListAsync();
        }

        public Task RemoveRangeAsync(IList<SupplierQuotationItem> items)
        {
            try
            {
                _inventoryDbContext.SupplierQuotationItems.RemoveRange(items);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }
    }
}
