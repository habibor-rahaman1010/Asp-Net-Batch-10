using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.Enums;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface ICreditNoteRepository : IRepositoryBase<CreditNote, Guid>
    {
        Task<(IList<CreditNote> data, int total, int totalDisplay)> GetPagedCreditNotesAsync(int pageIndex,
            int pageSize, DataTablesSearch search, string? order);
        Task<CreditNote?> GetCreditNoteByIdAsync(Guid id);
        Task<IList<CreditNote>> GetCreditNotesByCustomerAsync(Guid customerId);
        Task<IList<CreditNote>> GetCreditNotesByStatusAsync(params CreditNoteStatus[] statuses);

        /// <summary>The credit note a return raised, so the two never drift apart.</summary>
        Task<CreditNote?> GetCreditNoteBySalesReturnAsync(Guid salesReturnId);

        Task<IList<CreditNote>> GetCreditNotesByDateRangeAsync(DateTime fromDate, DateTime toDate);
        Task<bool> IsCreditNoteNoDuplicateAsync(string creditNoteNo);
    }
}
