using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.RepositoryContracts;
using DevSkill.Inventory.Infrastructure.Data;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class PriceListItemRepository : Repository<PriceListItem, Guid>, IPriceListItemRepository
    {
        private readonly InventoryDbContext _inventoryDbContext;

        public PriceListItemRepository(InventoryDbContext inventoryDbContext) : base(inventoryDbContext)
        {
            _inventoryDbContext = inventoryDbContext;
        }

        public Task RemoveRangeAsync(IList<PriceListItem> priceListItems)
        {
            try
            {
                _inventoryDbContext.PriceListItems.RemoveRange(priceListItems);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }
    }
}
