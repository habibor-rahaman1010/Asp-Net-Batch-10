using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Domain.RepositoryContracts;
using DevSkill.Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class SalesInvoiceRepository : Repository<SalesInvoice, Guid>, ISalesInvoiceRepository
    {
        private readonly InventoryDbContext _inventoryDbContext;

        public SalesInvoiceRepository(InventoryDbContext inventoryDbContext) : base(inventoryDbContext)
        {
            _inventoryDbContext = inventoryDbContext;
        }

        public async Task<(IList<SalesInvoice> data, int total, int totalDisplay)> GetPagedSalesInvoicesAsync(
            int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(search.Value))
                {
                    return await GetDynamicAsync(null, order,
                        x => x.Include(n => n.Customer).Include(n => n.SalesOrder),
                        pageIndex, pageSize, true);
                }
                else
                {
                    return await GetDynamicAsync(
                        x => x.InvoiceNo.Contains(search.Value) ||
                             x.SalesOrder!.SalesOrderNo.Contains(search.Value) ||
                             x.Customer!.CustomerName.Contains(search.Value) ||
                             x.Customer!.CustomerCode.Contains(search.Value),
                        order,
                        x => x.Include(n => n.Customer).Include(n => n.SalesOrder),
                        pageIndex, pageSize, true);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<SalesInvoice?> GetSalesInvoiceByIdAsync(Guid id)
        {
            return await _inventoryDbContext.SalesInvoices
                .Include(x => x.Customer)
                .Include(x => x.BusinessLocation)
                .Include(x => x.PaymentTerm)
                .Include(x => x.Salesperson)
                .Include(x => x.SalesOrder)
                .Include(x => x.Delivery)
                .Include(x => x.SalesInvoiceItems!)
                    .ThenInclude(x => x.Product)
                        .ThenInclude(x => x!.Unit)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IList<SalesInvoice>> GetSalesInvoicesBySalesOrderAsync(Guid salesOrderId)
        {
            return await _inventoryDbContext.SalesInvoices
                .Include(x => x.SalesInvoiceItems)
                .Where(x => x.SalesOrderId == salesOrderId)
                .ToListAsync();
        }

        public async Task<IList<SalesInvoice>> GetSalesInvoicesByCustomerAsync(Guid customerId)
        {
            return await _inventoryDbContext.SalesInvoices
                .Include(x => x.SalesOrder)
                .Where(x => x.CustomerId == customerId)
                .OrderByDescending(x => x.InvoiceDate)
                .ToListAsync();
        }

        public async Task<IList<SalesInvoice>> GetSalesInvoicesByStatusAsync(params SalesInvoiceStatus[] statuses)
        {
            return await _inventoryDbContext.SalesInvoices
                .Include(x => x.Customer)
                .Include(x => x.SalesOrder)
                .Where(x => statuses.Contains(x.Status))
                .OrderByDescending(x => x.InvoiceDate)
                .ToListAsync();
        }

        public async Task<IList<SalesInvoice>> GetSalesInvoicesBySalespersonAsync(Guid salespersonId)
        {
            return await _inventoryDbContext.SalesInvoices
                .Include(x => x.Customer)
                .Where(x => x.SalespersonId == salespersonId)
                .OrderByDescending(x => x.InvoiceDate)
                .ToListAsync();
        }

        public async Task<IList<SalesInvoice>> GetSalesInvoicesByDateRangeAsync(DateTime fromDate, DateTime toDate)
        {
            return await _inventoryDbContext.SalesInvoices
                .Include(x => x.Customer)
                .Include(x => x.BusinessLocation)
                .Include(x => x.Salesperson)
                .Include(x => x.SalesInvoiceItems!)
                    .ThenInclude(x => x.Product)
                        .ThenInclude(x => x!.Unit)
                .Where(x => x.InvoiceDate >= fromDate && x.InvoiceDate <= toDate)
                .OrderBy(x => x.InvoiceDate)
                .ToListAsync();
        }

        public async Task<IList<MonthlyAmountDto>> GetMonthlyInvoicedTotalsAsync(DateTime fromDate, DateTime toDate,
            params SalesInvoiceStatus[] statuses)
        {
            return await _inventoryDbContext.SalesInvoices
                .Where(x => x.InvoiceDate >= fromDate
                         && x.InvoiceDate <= toDate
                         && statuses.Contains(x.Status))
                .GroupBy(x => new { x.InvoiceDate.Year, x.InvoiceDate.Month })
                .Select(g => new MonthlyAmountDto
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Total = g.Sum(x => x.GrandTotal)
                })
                .OrderBy(x => x.Year).ThenBy(x => x.Month)
                .ToListAsync();
        }

        public async Task<DateTime?> GetEarliestInvoiceDateAsync(params SalesInvoiceStatus[] statuses)
        {
            return await _inventoryDbContext.SalesInvoices
                .Where(x => statuses.Contains(x.Status))
                .OrderBy(x => x.InvoiceDate)
                .Select(x => (DateTime?)x.InvoiceDate)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> IsInvoiceNoDuplicateAsync(string invoiceNo)
        {
            return await GetCountAsync(x => x.InvoiceNo == invoiceNo) > 0;
        }
    }
}
