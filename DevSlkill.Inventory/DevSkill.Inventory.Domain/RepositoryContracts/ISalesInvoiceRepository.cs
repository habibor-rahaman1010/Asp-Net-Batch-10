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
        Task<bool> IsInvoiceNoDuplicateAsync(string invoiceNo);
    }
}
