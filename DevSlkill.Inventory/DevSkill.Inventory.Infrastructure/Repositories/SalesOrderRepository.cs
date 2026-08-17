using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Domain.RepositoryContracts;
using DevSkill.Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class SalesOrderRepository : Repository<SalesOrder, Guid>, ISalesOrderRepository
    {
        private readonly InventoryDbContext _inventoryDbContext;

        public SalesOrderRepository(InventoryDbContext inventoryDbContext) : base(inventoryDbContext)
        {
            _inventoryDbContext = inventoryDbContext;
        }

        public async Task<(IList<SalesOrder> data, int total, int totalDisplay)> GetPagedSalesOrdersAsync(
            int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(search.Value))
                {
                    return await GetDynamicAsync(null, order,
                        x => x.Include(n => n.Customer).Include(n => n.BusinessLocation),
                        pageIndex, pageSize, true);
                }
                else
                {
                    return await GetDynamicAsync(
                        x => x.SalesOrderNo.Contains(search.Value) ||
                             x.CustomerReference.Contains(search.Value) ||
                             x.Customer!.CustomerName.Contains(search.Value) ||
                             x.Customer!.CustomerCode.Contains(search.Value),
                        order,
                        x => x.Include(n => n.Customer).Include(n => n.BusinessLocation),
                        pageIndex, pageSize, true);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<SalesOrder?> GetSalesOrderByIdAsync(Guid id)
        {
            return await _inventoryDbContext.SalesOrders
                .Include(x => x.Customer)
                .Include(x => x.BusinessLocation)
                .Include(x => x.PaymentTerm)
                .Include(x => x.Salesperson)
                .Include(x => x.SalesQuotation)
                .Include(x => x.SalesOrderItems!)
                    .ThenInclude(x => x.Product)
                        .ThenInclude(x => x!.Unit)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <summary>
        /// Used by the flows that only work on orders in a certain state: reserving,
        /// delivering and invoicing all start from here.
        /// </summary>
        public async Task<IList<SalesOrder>> GetSalesOrdersByStatusAsync(params SalesOrderStatus[] statuses)
        {
            return await _inventoryDbContext.SalesOrders
                .Include(x => x.Customer)
                .Include(x => x.BusinessLocation)
                .Where(x => statuses.Contains(x.Status))
                .OrderByDescending(x => x.OrderDate)
                .ToListAsync();
        }

        public async Task<IList<SalesOrder>> GetSalesOrdersByQuotationAsync(Guid salesQuotationId)
        {
            return await _inventoryDbContext.SalesOrders
                .Include(x => x.SalesOrderItems)
                .Where(x => x.SalesQuotationId == salesQuotationId)
                .ToListAsync();
        }

        public async Task<IList<SalesOrder>> GetSalesOrdersByCustomerAsync(Guid customerId)
        {
            return await _inventoryDbContext.SalesOrders
                .Include(x => x.BusinessLocation)
                .Where(x => x.CustomerId == customerId)
                .OrderByDescending(x => x.OrderDate)
                .ToListAsync();
        }

        public async Task<IList<SalesOrder>> GetSalesOrdersByDateRangeAsync(DateTime fromDate, DateTime toDate)
        {
            return await _inventoryDbContext.SalesOrders
                .Include(x => x.Customer)
                .Include(x => x.BusinessLocation)
                .Include(x => x.Salesperson)
                .Include(x => x.SalesOrderItems!)
                    .ThenInclude(x => x.Product)
                        .ThenInclude(x => x!.Unit)
                .Where(x => x.OrderDate >= fromDate && x.OrderDate <= toDate)
                .OrderBy(x => x.OrderDate)
                .ToListAsync();
        }

        public async Task<bool> IsSalesOrderNoDuplicateAsync(string salesOrderNo)
        {
            return await GetCountAsync(x => x.SalesOrderNo == salesOrderNo) > 0;
        }
    }
}
