using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Domain.RepositoryContracts;
using DevSkill.Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class SalesReturnRepository : Repository<SalesReturn, Guid>, ISalesReturnRepository
    {
        private readonly InventoryDbContext _inventoryDbContext;

        public SalesReturnRepository(InventoryDbContext inventoryDbContext) : base(inventoryDbContext)
        {
            _inventoryDbContext = inventoryDbContext;
        }

        public async Task<(IList<SalesReturn> data, int total, int totalDisplay)> GetPagedSalesReturnsAsync(
            int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(search.Value))
                {
                    return await GetDynamicAsync(null, order,
                        x => x.Include(n => n.Customer)
                              .Include(n => n.BusinessLocation)
                              .Include(n => n.SalesInvoice),
                        pageIndex, pageSize, true);
                }
                else
                {
                    return await GetDynamicAsync(
                        x => x.ReturnNo.Contains(search.Value) ||
                             x.SalesInvoice!.InvoiceNo.Contains(search.Value) ||
                             x.Customer!.CustomerName.Contains(search.Value) ||
                             x.Customer!.CustomerCode.Contains(search.Value),
                        order,
                        x => x.Include(n => n.Customer)
                              .Include(n => n.BusinessLocation)
                              .Include(n => n.SalesInvoice),
                        pageIndex, pageSize, true);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<SalesReturn?> GetSalesReturnByIdAsync(Guid id)
        {
            return await _inventoryDbContext.SalesReturns
                .Include(x => x.Customer)
                .Include(x => x.BusinessLocation)
                .Include(x => x.SalesInvoice!)
                    .ThenInclude(x => x.SalesInvoiceItems!)
                        .ThenInclude(x => x.Product)
                .Include(x => x.SalesReturnItems!)
                    .ThenInclude(x => x.Product)
                        .ThenInclude(x => x!.Unit)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IList<SalesReturn>> GetSalesReturnsBySalesInvoiceAsync(Guid salesInvoiceId)
        {
            return await _inventoryDbContext.SalesReturns
                .Include(x => x.SalesReturnItems)
                .Where(x => x.SalesInvoiceId == salesInvoiceId)
                .ToListAsync();
        }

        public async Task<IList<SalesReturn>> GetSalesReturnsByCustomerAsync(Guid customerId)
        {
            return await _inventoryDbContext.SalesReturns
                .Include(x => x.SalesInvoice)
                .Where(x => x.CustomerId == customerId)
                .OrderByDescending(x => x.ReturnDate)
                .ToListAsync();
        }

        public async Task<IList<SalesReturn>> GetSalesReturnsByStatusAsync(params SalesReturnStatus[] statuses)
        {
            return await _inventoryDbContext.SalesReturns
                .Include(x => x.Customer)
                .Include(x => x.SalesInvoice)
                .Where(x => statuses.Contains(x.Status))
                .OrderByDescending(x => x.ReturnDate)
                .ToListAsync();
        }

        public async Task<IList<SalesReturn>> GetSalesReturnsByDateRangeAsync(DateTime fromDate, DateTime toDate)
        {
            return await _inventoryDbContext.SalesReturns
                .Include(x => x.Customer)
                .Include(x => x.BusinessLocation)
                .Include(x => x.SalesReturnItems!)
                    .ThenInclude(x => x.Product)
                        .ThenInclude(x => x!.Unit)
                .Where(x => x.ReturnDate >= fromDate && x.ReturnDate <= toDate)
                .OrderBy(x => x.ReturnDate)
                .ToListAsync();
        }

        public async Task<bool> IsReturnNoDuplicateAsync(string returnNo)
        {
            return await GetCountAsync(x => x.ReturnNo == returnNo) > 0;
        }
    }
}
