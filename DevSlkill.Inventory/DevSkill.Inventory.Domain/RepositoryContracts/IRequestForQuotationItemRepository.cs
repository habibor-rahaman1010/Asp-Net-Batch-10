using DevSkill.Inventory.Domain.Entities.PurchaseEntities;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface IRequestForQuotationItemRepository : IRepositoryBase<RequestForQuotationItem, Guid>
    {
        Task<IList<RequestForQuotationItem>> GetItemsByRequestForQuotationAsync(Guid requestForQuotationId);
        Task RemoveRangeAsync(IList<RequestForQuotationItem> items);
    }
}
