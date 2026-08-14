using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities.SalesEntities;

namespace DevSkill.Inventory.Application.ServicesContract
{
    public interface IProformaInvoiceManagementService
    {
        /// <summary>
        /// Creates a proforma invoice. Only the header fields and the
        /// product/quantity pairs of <paramref name="proformaInvoice"/> are used;
        /// every price, discount, tax and total is resolved on the server.
        /// </summary>
        Task<Guid> CreateProformaInvoiceAsync(ProformaInvoice proformaInvoice);

        /// <summary>
        /// Replaces the header and the whole line set of an existing proforma
        /// invoice. Prices, discounts, taxes and totals are recalculated from
        /// scratch exactly the same way as on create.
        /// </summary>
        Task<bool> UpdateProformaInvoiceAsync(ProformaInvoice proformaInvoice);

        Task<bool> DeleteProformaInvoiceAsync(Guid id);

        Task<ProformaInvoice?> GetProformaInvoiceByIdAsync(Guid id);

        Task<(IList<ProformaInvoice> data, int total, int totalDisplay)> GetProformaInvoicesAsync(int pageIndex,
            int pageSize, DataTablesSearch search, string? order);

        Task<string> GenerateProformaNoAsync();
    }
}
