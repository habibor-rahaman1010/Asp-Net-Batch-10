using DevSkill.Inventory.Domain.Entities.PurchaseEntities;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface IPurchaseRequisitionItemRepository : IRepositoryBase<PurchaseRequisitionItem, Guid>
    {
        Task<IList<PurchaseRequisitionItem>> GetItemsByRequisitionAsync(Guid purchaseRequisitionId);
        Task RemoveRangeAsync(IList<PurchaseRequisitionItem> items);
    }
}
