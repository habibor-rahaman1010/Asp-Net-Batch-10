using DevSkill.Inventory.Domain.Dtos;
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

        /// <summary>
        /// Billed value grouped by calendar month, added up in the database. Only the
        /// given statuses count, so a draft or a cancelled invoice never inflates a
        /// chart. A month with no invoice simply has no row.
        /// </summary>
        Task<IList<MonthlyAmountDto>> GetMonthlyInvoicedTotalsAsync(DateTime fromDate, DateTime toDate,
            params PurchaseInvoiceStatus[] statuses);

        /// <summary>
        /// The date of the oldest invoice that counts, so the year filter never offers a
        /// year with nothing behind it. Null when there is no such invoice at all.
        /// </summary>
        Task<DateTime?> GetEarliestInvoiceDateAsync(params PurchaseInvoiceStatus[] statuses);

        Task<bool> IsInvoiceNoDuplicateAsync(string invoiceNo);
        Task<bool> IsSupplierInvoiceNoDuplicateAsync(Guid supplierId, string supplierInvoiceNo, Guid? id = null);
    }
}
