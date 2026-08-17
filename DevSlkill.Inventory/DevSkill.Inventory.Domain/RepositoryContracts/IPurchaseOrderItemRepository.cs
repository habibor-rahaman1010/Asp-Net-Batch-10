using DevSkill.Inventory.Domain.Entities.PurchaseEntities;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface IPurchaseOrderItemRepository : IRepositoryBase<PurchaseOrderItem, Guid>
    {
        Task<IList<PurchaseOrderItem>> GetItemsByPurchaseOrderAsync(Guid purchaseOrderId);
        Task RemoveRangeAsync(IList<PurchaseOrderItem> items);
    }
}
