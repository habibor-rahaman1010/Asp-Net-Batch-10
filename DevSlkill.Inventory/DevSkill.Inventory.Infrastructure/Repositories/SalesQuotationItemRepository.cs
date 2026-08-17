using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.RepositoryContracts;
using DevSkill.Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class SalesQuotationItemRepository : Repository<SalesQuotationItem, Guid>, ISalesQuotationItemRepository
    {
        private readonly InventoryDbContext _inventoryDbContext;

        public SalesQuotationItemRepository(InventoryDbContext inventoryDbContext) : base(inventoryDbContext)
        {
            _inventoryDbContext = inventoryDbContext;
        }

        public async Task<IList<SalesQuotationItem>> GetItemsBySalesQuotationAsync(Guid salesQuotationId)
        {
            return await _inventoryDbContext.SalesQuotationItems
                .Include(x => x.Product)
                    .ThenInclude(x => x!.Unit)
                .Where(x => x.SalesQuotationId == salesQuotationId)
                .ToListAsync();
        }

        public Task RemoveRangeAsync(IList<SalesQuotationItem> items)
        {
            try
            {
                _inventoryDbContext.SalesQuotationItems.RemoveRange(items);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }
    }
}
