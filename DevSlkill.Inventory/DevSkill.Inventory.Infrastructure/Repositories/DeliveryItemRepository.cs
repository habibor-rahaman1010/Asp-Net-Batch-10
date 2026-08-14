using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.RepositoryContracts;
using DevSkill.Inventory.Infrastructure.Data;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class DeliveryItemRepository : Repository<DeliveryItem, Guid>, IDeliveryItemRepository
    {
        private readonly InventoryDbContext _inventoryDbContext;

        public DeliveryItemRepository(InventoryDbContext inventoryDbContext) : base(inventoryDbContext)
        {
            _inventoryDbContext = inventoryDbContext;
        }

        public Task RemoveRangeAsync(IList<DeliveryItem> deliveryItems)
        {
            try
            {
                _inventoryDbContext.DeliveryItems.RemoveRange(deliveryItems);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }
    }
}
