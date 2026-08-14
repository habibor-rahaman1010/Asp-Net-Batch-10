using DevSkill.Inventory.Domain.Entities.SalesEntities;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface IDeliveryItemRepository : IRepositoryBase<DeliveryItem, Guid>
    {
        Task RemoveRangeAsync(IList<DeliveryItem> deliveryItems);
    }
}
