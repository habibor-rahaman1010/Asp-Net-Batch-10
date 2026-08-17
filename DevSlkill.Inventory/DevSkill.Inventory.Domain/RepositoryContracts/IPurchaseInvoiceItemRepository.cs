using DevSkill.Inventory.Domain.Entities.PurchaseEntities;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface IPurchaseInvoiceItemRepository : IRepositoryBase<PurchaseInvoiceItem, Guid>
    {
        Task<IList<PurchaseInvoiceItem>> GetItemsByPurchaseInvoiceAsync(Guid purchaseInvoiceId);
        Task RemoveRangeAsync(IList<PurchaseInvoiceItem> items);
    }
}
