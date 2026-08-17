using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.RepositoryContracts;
using DevSkill.Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class SalesInvoiceItemRepository : Repository<SalesInvoiceItem, Guid>, ISalesInvoiceItemRepository
    {
        private readonly InventoryDbContext _inventoryDbContext;

        public SalesInvoiceItemRepository(InventoryDbContext inventoryDbContext) : base(inventoryDbContext)
        {
            _inventoryDbContext = inventoryDbContext;
        }

        public async Task<IList<SalesInvoiceItem>> GetItemsBySalesInvoiceAsync(Guid salesInvoiceId)
        {
            return await _inventoryDbContext.SalesInvoiceItems
                .Include(x => x.Product)
                    .ThenInclude(x => x!.Unit)
                .Where(x => x.SalesInvoiceId == salesInvoiceId)
                .ToListAsync();
        }

        public Task RemoveRangeAsync(IList<SalesInvoiceItem> items)
        {
            try
            {
                _inventoryDbContext.SalesInvoiceItems.RemoveRange(items);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }
    }
}
