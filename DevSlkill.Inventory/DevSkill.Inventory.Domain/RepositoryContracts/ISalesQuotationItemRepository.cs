using DevSkill.Inventory.Domain.Entities.SalesEntities;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface ISalesQuotationItemRepository : IRepositoryBase<SalesQuotationItem, Guid>
    {
        Task<IList<SalesQuotationItem>> GetItemsBySalesQuotationAsync(Guid salesQuotationId);
        Task RemoveRangeAsync(IList<SalesQuotationItem> items);
    }
}
