using DevSkill.Inventory.Domain.Entities.SalesEntities;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface IProformaInvoiceItemRepository : IRepositoryBase<ProformaInvoiceItem, Guid>
    {
        Task RemoveRangeAsync(IList<ProformaInvoiceItem> proformaInvoiceItems);
    }
}
