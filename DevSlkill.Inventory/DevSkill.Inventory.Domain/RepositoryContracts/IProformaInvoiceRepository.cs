using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.Enums;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface IProformaInvoiceRepository : IRepositoryBase<ProformaInvoice, Guid>
    {
        Task<IList<ProformaInvoice>> GetProformaInvoicesByStatusAsync(params ProformaInvoiceStatus[] statuses);
        Task<(IList<ProformaInvoice> data, int total, int totalDisplay)> GetPagedProformaInvoicesAsync(int pageIndex,
            int pageSize, DataTablesSearch search, string? order);
        Task<ProformaInvoice?> GetProformaInvoiceByIdAsync(Guid id);
        Task<bool> IsProformaNoDuplicateAsync(string proformaNo);
    }
}
