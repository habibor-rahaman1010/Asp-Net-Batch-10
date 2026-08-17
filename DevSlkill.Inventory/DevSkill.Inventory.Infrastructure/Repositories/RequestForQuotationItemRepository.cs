using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using DevSkill.Inventory.Domain.RepositoryContracts;
using DevSkill.Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class RequestForQuotationItemRepository
        : Repository<RequestForQuotationItem, Guid>, IRequestForQuotationItemRepository
    {
        private readonly InventoryDbContext _inventoryDbContext;

        public RequestForQuotationItemRepository(InventoryDbContext inventoryDbContext) : base(inventoryDbContext)
        {
            _inventoryDbContext = inventoryDbContext;
        }

        public async Task<IList<RequestForQuotationItem>> GetItemsByRequestForQuotationAsync(
            Guid requestForQuotationId)
        {
            return await _inventoryDbContext.RequestForQuotationItems
                .Include(x => x.Product)
                    .ThenInclude(x => x!.Unit)
                .Where(x => x.RequestForQuotationId == requestForQuotationId)
                .ToListAsync();
        }

        public Task RemoveRangeAsync(IList<RequestForQuotationItem> items)
        {
            try
            {
                _inventoryDbContext.RequestForQuotationItems.RemoveRange(items);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }
    }
}
