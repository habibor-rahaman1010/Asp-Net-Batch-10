using DevSkill.Inventory.Domain.Entities.SalesEntities;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface ISalesInvoiceItemRepository : IRepositoryBase<SalesInvoiceItem, Guid>
    {
        Task<IList<SalesInvoiceItem>> GetItemsBySalesInvoiceAsync(Guid salesInvoiceId);
        Task RemoveRangeAsync(IList<SalesInvoiceItem> items);
    }
}
