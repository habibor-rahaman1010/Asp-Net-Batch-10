using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.Enums;

namespace DevSkill.Inventory.Application.ServicesContract
{
    /// <summary>
    /// Credit owed back to customers. A note raised by a sales return is written by
    /// that return and may not be edited here; a price adjustment is raised by hand.
    /// Spending a credit is done by a collection of method Adjustment that names it.
    /// </summary>
    public interface ICreditNoteManagementService
    {
        Task<Guid> CreateCreditNoteAsync(CreditNote creditNote, bool issueImmediately);
        Task<bool> UpdateCreditNoteAsync(CreditNote creditNote);
        Task<bool> DeleteCreditNoteAsync(Guid id);

        /// <summary>Puts the credit on the customer account, so it can be spent.</summary>
        Task<bool> IssueCreditNoteAsync(Guid id);

        /// <summary>Withdraws a credit that has not been spent yet.</summary>
        Task<bool> CancelCreditNoteAsync(Guid id);

        Task<CreditNote?> GetCreditNoteByIdAsync(Guid id);
        Task<(IList<CreditNote> data, int total, int totalDisplay)> GetCreditNotesAsync(int pageIndex,
            int pageSize, DataTablesSearch search, string? order);
        Task<IList<CreditNote>> GetCreditNotesByStatusAsync(params CreditNoteStatus[] statuses);
        Task<string> GenerateCreditNoteNoAsync();

        /// <summary>Invoices of one customer a price adjustment may be raised against.</summary>
        Task<IList<SalesInvoice>> GetCreditableSalesInvoicesAsync(Guid customerId);

        /// <summary>
        /// Re-reads how much of a credit note has been spent from the collections that
        /// name it, and restates its status. Called whenever such a collection moves.
        /// </summary>
        Task RefreshAppliedAmountAsync(Guid creditNoteId);
    }
}
