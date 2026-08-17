using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using DevSkill.Inventory.Domain.Enums;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface IPurchaseInvoiceRepository : IRepositoryBase<PurchaseInvoice, Guid>
    {
        Task<(IList<PurchaseInvoice> data, int total, int totalDisplay)> GetPagedPurchaseInvoicesAsync(int pageIndex,
            int pageSize, DataTablesSearch search, string? order);
        Task<PurchaseInvoice?> GetPurchaseInvoiceByIdAsync(Guid id);
        Task<IList<PurchaseInvoice>> GetPurchaseInvoicesByPurchaseOrderAsync(Guid purchaseOrderId);
        Task<IList<PurchaseInvoice>> GetPurchaseInvoicesBySupplierAsync(Guid supplierId);
        Task<IList<PurchaseInvoice>> GetPurchaseInvoicesByStatusAsync(params PurchaseInvoiceStatus[] statuses);

        /// <summary>
        /// Invoices dated within a period, with their lines. The reporting side reads
        /// what was billed through this.
        /// </summary>
        Task<IList<PurchaseInvoice>> GetPurchaseInvoicesByDateRangeAsync(DateTime fromDate, DateTime toDate);
        Task<bool> IsInvoiceNoDuplicateAsync(string invoiceNo);
        Task<bool> IsSupplierInvoiceNoDuplicateAsync(Guid supplierId, string supplierInvoiceNo, Guid? id = null);
    }
}
