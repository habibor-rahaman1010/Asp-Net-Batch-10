using DevSkill.Inventory.Domain.Entities.PurchaseEntities;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface ISupplierQuotationItemRepository : IRepositoryBase<SupplierQuotationItem, Guid>
    {
        Task<IList<SupplierQuotationItem>> GetItemsBySupplierQuotationAsync(Guid supplierQuotationId);
        Task RemoveRangeAsync(IList<SupplierQuotationItem> items);
    }
}
