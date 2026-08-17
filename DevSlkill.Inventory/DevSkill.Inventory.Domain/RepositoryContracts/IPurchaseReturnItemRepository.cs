using DevSkill.Inventory.Domain.Entities.PurchaseEntities;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface IPurchaseReturnItemRepository : IRepositoryBase<PurchaseReturnItem, Guid>
    {
        Task<IList<PurchaseReturnItem>> GetItemsByPurchaseReturnAsync(Guid purchaseReturnId);
        Task RemoveRangeAsync(IList<PurchaseReturnItem> items);
    }
}
