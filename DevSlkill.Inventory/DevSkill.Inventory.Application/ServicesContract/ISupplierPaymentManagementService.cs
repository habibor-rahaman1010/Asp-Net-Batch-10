using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities.PurchaseEntities;

namespace DevSkill.Inventory.Application.ServicesContract
{
    public interface ISupplierPaymentManagementService
    {
        Task<Guid> CreateSupplierPaymentAsync(SupplierPayment supplierPayment);
        Task<bool> UpdateSupplierPaymentAsync(SupplierPayment supplierPayment);
        Task<bool> DeleteSupplierPaymentAsync(Guid id);
        Task<SupplierPayment?> GetSupplierPaymentByIdAsync(Guid id);
        Task<(IList<SupplierPayment> data, int total, int totalDisplay)> GetSupplierPaymentsAsync(int pageIndex,
            int pageSize, DataTablesSearch search, string? order);
        Task<string> GeneratePaymentNoAsync();

        /// <summary>
        /// Every payment made to one supplier, cancelled ones included. The callers
        /// decide which of them still count.
        /// </summary>
        Task<IList<SupplierPayment>> GetSupplierPaymentsBySupplierAsync(Guid supplierId);

        /// <summary>
        /// Settles the invoices. This is the only place the purchase module records
        /// money leaving, and it happens inside one transaction with the invoices.
        /// </summary>
        Task ConfirmSupplierPaymentAsync(Guid id);

        /// <summary>
        /// Reverses a payment. A confirmed one puts the due straight back on the
        /// invoices it settled.
        /// </summary>
        Task CancelSupplierPaymentAsync(Guid id);

        /// <summary>
        /// The outstanding invoices of a supplier, ready to be paid.
        /// <paramref name="excludeSupplierPaymentId"/> lets an edit measure against
        /// the right figure by ignoring what that same payment is already holding.
        /// </summary>
        Task<IList<PayableInvoiceDto>> GetPayableInvoicesAsync(Guid supplierId,
            Guid? excludeSupplierPaymentId = null);
    }
}
