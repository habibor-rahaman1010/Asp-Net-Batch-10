using DevSkill.Inventory.Domain.Entities.SalesEntities;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface ISalesOrderItemRepository : IRepositoryBase<SalesOrderItem, Guid>
    {
        Task<IList<SalesOrderItem>> GetItemsBySalesOrderAsync(Guid salesOrderId);
        Task RemoveRangeAsync(IList<SalesOrderItem> items);
    }
}
