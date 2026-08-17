using DevSkill.Inventory.Domain.Entities.PurchaseEntities;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface ISupplierPaymentRepository : IRepositoryBase<SupplierPayment, Guid>
    {
        Task<(IList<SupplierPayment> data, int total, int totalDisplay)> GetPagedSupplierPaymentsAsync(int pageIndex,
            int pageSize, DataTablesSearch search, string? order);
        Task<SupplierPayment?> GetSupplierPaymentByIdAsync(Guid id);

        /// <summary>
        /// Every payment made to one supplier, cancelled ones included. The callers
        /// decide which of them still count.
        /// </summary>
        Task<IList<SupplierPayment>> GetSupplierPaymentsBySupplierAsync(Guid supplierId);

        /// <summary>
        /// Every payment that has an allocation against one invoice, which is what
        /// tells the invoice flow whether it may still be cancelled.
        /// </summary>
        Task<IList<SupplierPayment>> GetSupplierPaymentsByInvoiceAsync(Guid purchaseInvoiceId);
        Task<bool> IsPaymentNoDuplicateAsync(string paymentNo);
    }
}
