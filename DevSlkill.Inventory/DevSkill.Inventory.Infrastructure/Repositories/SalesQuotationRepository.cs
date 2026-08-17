using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Domain.RepositoryContracts;
using DevSkill.Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class SalesQuotationRepository : Repository<SalesQuotation, Guid>, ISalesQuotationRepository
    {
        private readonly InventoryDbContext _inventoryDbContext;

        public SalesQuotationRepository(InventoryDbContext inventoryDbContext) : base(inventoryDbContext)
        {
            _inventoryDbContext = inventoryDbContext;
        }

        public async Task<(IList<SalesQuotation> data, int total, int totalDisplay)> GetPagedSalesQuotationsAsync(
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
                        x => x.QuotationNo.Contains(search.Value) ||
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

        public async Task<SalesQuotation?> GetSalesQuotationByIdAsync(Guid id)
        {
            return await _inventoryDbContext.SalesQuotations
                .Include(x => x.Customer)
                .Include(x => x.BusinessLocation)
                .Include(x => x.PaymentTerm)
                .Include(x => x.Salesperson)
                .Include(x => x.SalesQuotationItems!)
                    .ThenInclude(x => x.Product)
                        .ThenInclude(x => x!.Unit)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <summary>
        /// Used by the flows that only work on quotations in a certain state, turning
        /// one into a sales order being the first of them.
        /// </summary>
        public async Task<IList<SalesQuotation>> GetSalesQuotationsByStatusAsync(params SalesQuotationStatus[] statuses)
        {
            return await _inventoryDbContext.SalesQuotations
                .Include(x => x.Customer)
                .Include(x => x.BusinessLocation)
                .Where(x => statuses.Contains(x.Status))
                .OrderByDescending(x => x.QuotationDate)
                .ToListAsync();
        }

        public async Task<IList<SalesQuotation>> GetSalesQuotationsByCustomerAsync(Guid customerId)
        {
            return await _inventoryDbContext.SalesQuotations
                .Include(x => x.BusinessLocation)
                .Where(x => x.CustomerId == customerId)
                .OrderByDescending(x => x.QuotationDate)
                .ToListAsync();
        }

        public async Task<IList<SalesQuotation>> GetSalesQuotationsByDateRangeAsync(DateTime fromDate, DateTime toDate)
        {
            return await _inventoryDbContext.SalesQuotations
                .Include(x => x.Customer)
                .Include(x => x.BusinessLocation)
                .Include(x => x.SalesQuotationItems!)
                    .ThenInclude(x => x.Product)
                        .ThenInclude(x => x!.Unit)
                .Where(x => x.QuotationDate >= fromDate && x.QuotationDate <= toDate)
                .OrderBy(x => x.QuotationDate)
                .ToListAsync();
        }

        public async Task<bool> IsQuotationNoDuplicateAsync(string quotationNo)
        {
            return await GetCountAsync(x => x.QuotationNo == quotationNo) > 0;
        }
    }
}
