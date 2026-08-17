using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.Enums;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface ICustomerPaymentRepository : IRepositoryBase<CustomerPayment, Guid>
    {
        Task<(IList<CustomerPayment> data, int total, int totalDisplay)> GetPagedCustomerPaymentsAsync(int pageIndex,
            int pageSize, DataTablesSearch search, string? order);
        Task<CustomerPayment?> GetCustomerPaymentByIdAsync(Guid id);
        Task<IList<CustomerPayment>> GetCustomerPaymentsByCustomerAsync(Guid customerId);
        Task<IList<CustomerPayment>> GetCustomerPaymentsByStatusAsync(params CustomerPaymentStatus[] statuses);

        /// <summary>Collections funded by one credit note, so its spent amount can be re-read.</summary>
        Task<IList<CustomerPayment>> GetCustomerPaymentsByCreditNoteAsync(Guid creditNoteId);

        Task<IList<CustomerPayment>> GetCustomerPaymentsByDateRangeAsync(DateTime fromDate, DateTime toDate);
        Task<bool> IsPaymentNoDuplicateAsync(string paymentNo);
    }
}
