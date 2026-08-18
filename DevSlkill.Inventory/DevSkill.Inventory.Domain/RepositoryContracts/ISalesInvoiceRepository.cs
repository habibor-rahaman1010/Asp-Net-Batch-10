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
        /// The date of the oldest invoice that counts, so the year filter never offers a
        /// year with nothing behind it. Null when there is no such invoice at all.
        /// </summary>
        Task<DateTime?> GetEarliestInvoiceDateAsync(params SalesInvoiceStatus[] statuses);

        Task<bool> IsInvoiceNoDuplicateAsync(string invoiceNo);
    }
}
