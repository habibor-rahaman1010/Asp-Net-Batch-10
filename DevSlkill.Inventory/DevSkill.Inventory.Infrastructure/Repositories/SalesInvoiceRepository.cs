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

        public async Task<IList<SalespersonSalesPointDto>> GetSalespersonInvoicedTotalsAsync(DateTime fromDate,
            DateTime toDate, params SalesInvoiceStatus[] statuses)
        {
            return await _inventoryDbContext.SalesInvoices
                .Where(x => x.InvoiceDate >= fromDate
                         && x.InvoiceDate <= toDate
                         && statuses.Contains(x.Status))
                // Grouped on the key rather than the name, so two salespeople who happen
                // to share a name are never added up into one bar.
                .GroupBy(x => new
                {
                    x.SalespersonId,
                    Name = x.Salesperson!.SalespersonName,
                    Code = x.Salesperson!.SalespersonCode
                })
                .Select(g => new SalespersonSalesPointDto
                {
                    SalespersonName = g.Key.Name ?? string.Empty,
                    SalespersonCode = g.Key.Code ?? string.Empty,
                    InvoicedAmount = g.Sum(x => x.GrandTotal),
                    InvoiceCount = g.Count()
                })
                .OrderByDescending(x => x.InvoicedAmount)
                .ToListAsync();
        }

        public async Task<IList<TopSellingProductPointDto>> GetProductInvoicedTotalsAsync(DateTime fromDate,
            DateTime toDate, params SalesInvoiceStatus[] statuses)
        {
            return await _inventoryDbContext.SalesInvoiceItems
                .Where(x => x.SalesInvoice!.InvoiceDate >= fromDate
                         && x.SalesInvoice!.InvoiceDate <= toDate
                         && statuses.Contains(x.SalesInvoice!.Status))
                // Grouped on the key rather than the name, so two products that happen
                // to share a name are never added up into one slice.
                .GroupBy(x => new
                {
                    x.ProductId,
                    Name = x.Product!.ProductName,
                    Code = x.Product!.SKU
                })
                .Select(g => new TopSellingProductPointDto
                {
                    ProductName = g.Key.Name ?? string.Empty,
                    Sku = g.Key.Code ?? string.Empty,
                    InvoicedAmount = g.Sum(x => x.LineTotal),
                    QuantitySold = g.Sum(x => x.Quantity),
                    // Counted over the invoices rather than the lines, so a product
                    // billed twice on one invoice is still one invoice.
                    InvoiceCount = g.Select(x => x.SalesInvoiceId).Distinct().Count()
                })
                .OrderByDescending(x => x.InvoicedAmount)
                .ToListAsync();
        }

        public async Task<IList<MonthlyProductSalesDto>> GetMonthlyProductSalesAsync(DateTime fromDate,
            DateTime toDate, params SalesInvoiceStatus[] statuses)
        {
            return await _inventoryDbContext.SalesInvoiceItems
                .Where(x => x.SalesInvoice!.InvoiceDate >= fromDate
                         && x.SalesInvoice!.InvoiceDate <= toDate
                         && statuses.Contains(x.SalesInvoice!.Status))
                // The product's own price rides along in the key, so a product that was
                // never bought can still be costed without a second read.
                .GroupBy(x => new
                {
                    x.SalesInvoice!.InvoiceDate.Year,
                    x.SalesInvoice!.InvoiceDate.Month,
                    x.ProductId,
                    x.Product!.Price
                })
                .Select(g => new MonthlyProductSalesDto
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    ProductId = g.Key.ProductId,
                    ListUnitCost = g.Key.Price,

                    // Goods that came back were never really sold, so they are taken off
                    // both sides: the units here and the value below.
                    Quantity = g.Sum(x => x.Quantity - x.ReturnedQuantity),
                    Revenue = g.Sum(x => x.Quantity == 0
                        ? 0
                        : x.LineTotal * (x.Quantity - x.ReturnedQuantity) / x.Quantity)
                })
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
