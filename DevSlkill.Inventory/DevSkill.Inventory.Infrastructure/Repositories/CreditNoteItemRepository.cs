using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.RepositoryContracts;
using DevSkill.Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class CreditNoteItemRepository : Repository<CreditNoteItem, Guid>, ICreditNoteItemRepository
    {
        private readonly InventoryDbContext _inventoryDbContext;

        public CreditNoteItemRepository(InventoryDbContext inventoryDbContext) : base(inventoryDbContext)
        {
            _inventoryDbContext = inventoryDbContext;
        }

        public async Task<IList<CreditNoteItem>> GetItemsByCreditNoteAsync(Guid creditNoteId)
        {
            return await _inventoryDbContext.CreditNoteItems
                .Include(x => x.Product)
                    .ThenInclude(x => x!.Unit)
                .Where(x => x.CreditNoteId == creditNoteId)
                .ToListAsync();
        }

        public Task RemoveRangeAsync(IList<CreditNoteItem> items)
        {
            try
            {
                _inventoryDbContext.CreditNoteItems.RemoveRange(items);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }
    }
}
