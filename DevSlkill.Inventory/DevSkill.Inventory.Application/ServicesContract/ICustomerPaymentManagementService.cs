using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities.SalesEntities;

namespace DevSkill.Inventory.Application.ServicesContract
{
    /// <summary>
    /// Money coming in from customers. A draft settles nothing; receiving it is what
    /// clears the invoices it was allocated to.
    /// </summary>
    public interface ICustomerPaymentManagementService
    {
        Task<Guid> CreateCustomerPaymentAsync(CustomerPayment customerPayment, bool receiveImmediately);
        Task<bool> UpdateCustomerPaymentAsync(CustomerPayment customerPayment);
        Task<bool> DeleteCustomerPaymentAsync(Guid id);

        /// <summary>Takes the money in: the allocated invoices are settled by that much.</summary>
        Task<bool> ReceiveCustomerPaymentAsync(Guid id);

        /// <summary>Puts a received collection back, so the invoices owe their money again.</summary>
        Task<bool> CancelCustomerPaymentAsync(Guid id);

        Task<CustomerPayment?> GetCustomerPaymentByIdAsync(Guid id);
        Task<(IList<CustomerPayment> data, int total, int totalDisplay)> GetCustomerPaymentsAsync(int pageIndex,
            int pageSize, DataTablesSearch search, string? order);
        Task<string> GeneratePaymentNoAsync();

        /// <summary>
        /// The customer's open invoices with what may still be put on each of them.
        /// <paramref name="excludePaymentId"/> leaves the collection being edited out
        /// of the already allocated totals.
        /// </summary>
        Task<IList<ReceivableInvoiceDto>> GetReceivableInvoicesAsync(Guid customerId, Guid? excludePaymentId = null);

        /// <summary>Credit notes of one customer that still have something left to spend.</summary>
        Task<IList<CreditNote>> GetSpendableCreditNotesAsync(Guid customerId);
    }
}
