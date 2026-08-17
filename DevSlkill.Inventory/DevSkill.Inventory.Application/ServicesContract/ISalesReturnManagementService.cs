using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities.SalesEntities;

namespace DevSkill.Inventory.Application.ServicesContract
{
    /// <summary>
    /// Goods coming back from customers. A draft return brings nothing back;
    /// confirming it puts the stock in and raises the credit note that pays for it.
    /// </summary>
    public interface ISalesReturnManagementService
    {
        Task<Guid> CreateSalesReturnAsync(SalesReturn salesReturn, bool confirmImmediately);
        Task<bool> UpdateSalesReturnAsync(SalesReturn salesReturn);
        Task<bool> DeleteSalesReturnAsync(Guid id);

        /// <summary>
        /// Takes the goods back: stock goes up, the invoice line records what came
        /// back, and a credit note is issued for the value of the return.
        /// </summary>
        Task<bool> ConfirmSalesReturnAsync(Guid id);

        /// <summary>Undoes a confirmed return: stock goes out again and the credit is withdrawn.</summary>
        Task<bool> CancelSalesReturnAsync(Guid id);

        Task<SalesReturn?> GetSalesReturnByIdAsync(Guid id);
        Task<(IList<SalesReturn> data, int total, int totalDisplay)> GetSalesReturnsAsync(int pageIndex,
            int pageSize, DataTablesSearch search, string? order);
        Task<string> GenerateReturnNoAsync();

        /// <summary>
        /// The lines of a posted invoice with what may still come back on each of them.
        /// <paramref name="excludeReturnId"/> leaves the return being edited out of the
        /// already returned totals.
        /// </summary>
        Task<IList<ReturnableSalesLineDto>> GetReturnableLinesAsync(Guid salesInvoiceId, Guid? excludeReturnId = null);

        /// <summary>Invoices a return may be raised against.</summary>
        Task<IList<SalesInvoice>> GetReturnableSalesInvoicesAsync();
    }
}
