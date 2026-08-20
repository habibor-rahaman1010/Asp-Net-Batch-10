using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.Enums;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface ISalesInvoiceRepository : IRepositoryBase<SalesInvoice, Guid>
    {
        Task<(IList<SalesInvoice> data, int total, int totalDisplay)> GetPagedSalesInvoicesAsync(int pageIndex,
            int pageSize, DataTablesSearch search, string? order);
        Task<SalesInvoice?> GetSalesInvoiceByIdAsync(Guid id);
        Task<IList<SalesInvoice>> GetSalesInvoicesBySalesOrderAsync(Guid salesOrderId);
        Task<IList<SalesInvoice>> GetSalesInvoicesByCustomerAsync(Guid customerId);
        Task<IList<SalesInvoice>> GetSalesInvoicesByStatusAsync(params SalesInvoiceStatus[] statuses);

        /// <summary>Posted invoices of one salesperson, for the commission side.</summary>
        Task<IList<SalesInvoice>> GetSalesInvoicesBySalespersonAsync(Guid salespersonId);

        /// <summary>Invoices raised within a period, with their lines, for reporting.</summary>
        Task<IList<SalesInvoice>> GetSalesInvoicesByDateRangeAsync(DateTime fromDate, DateTime toDate);

        /// <summary>
        /// Invoiced value grouped by calendar month, added up in the database. Only the
        /// given statuses count, so a draft or a cancelled invoice never inflates a
        /// chart. A month with no invoice simply has no row.
        /// </summary>
        Task<IList<MonthlyAmountDto>> GetMonthlyInvoicedTotalsAsync(DateTime fromDate, DateTime toDate,
            params SalesInvoiceStatus[] statuses);

        /// <summary>
        /// Invoiced value grouped by salesperson over a period, added up in the
        /// database. Invoices raised without a salesperson come back as one row with
        /// no name, so the rows still add up to the period's sales.
        /// </summary>
        Task<IList<SalespersonSalesPointDto>> GetSalespersonInvoicedTotalsAsync(DateTime fromDate,
            DateTime toDate, params SalesInvoiceStatus[] statuses);

        /// <summary>
        /// Billed value grouped by product over a period, added up in the database.
        /// Read off the invoice lines rather than the invoice totals, so an
        /// invoice-level charge belongs to no product and is simply left out.
        /// </summary>
        Task<IList<TopSellingProductPointDto>> GetProductInvoicedTotalsAsync(DateTime fromDate,
            DateTime toDate, params SalesInvoiceStatus[] statuses);

        /// <summary>
        /// What each product sold for, month by month, net of what has since gone back
        /// on a return. Left ungrouped by product on purpose: costing has to happen
        /// product by product, so adding the products up here would throw away the one
        /// key the cost is looked up on.
        /// </summary>
        Task<IList<MonthlyProductSalesDto>> GetMonthlyProductSalesAsync(DateTime fromDate,
            DateTime toDate, params SalesInvoiceStatus[] statuses);

        /// <summary>
        /// The date of the oldest invoice that counts, so the year filter never offers a
        /// year with nothing behind it. Null when there is no such invoice at all.
        /// </summary>
        Task<DateTime?> GetEarliestInvoiceDateAsync(params SalesInvoiceStatus[] statuses);

        Task<bool> IsInvoiceNoDuplicateAsync(string invoiceNo);
    }
}
