using DevSkill.Inventory.Domain.Entities.SalesEntities;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface ISalesReturnItemRepository : IRepositoryBase<SalesReturnItem, Guid>
    {
        Task<IList<SalesReturnItem>> GetItemsBySalesReturnAsync(Guid salesReturnId);
        Task RemoveRangeAsync(IList<SalesReturnItem> items);
    }
}
