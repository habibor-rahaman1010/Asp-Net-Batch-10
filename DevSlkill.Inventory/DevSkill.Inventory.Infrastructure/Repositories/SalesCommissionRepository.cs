using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Domain.RepositoryContracts;
using DevSkill.Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class SalesCommissionRepository : Repository<SalesCommission, Guid>, ISalesCommissionRepository
    {
        private readonly InventoryDbContext _inventoryDbContext;

        public SalesCommissionRepository(InventoryDbContext inventoryDbContext) : base(inventoryDbContext)
        {
            _inventoryDbContext = inventoryDbContext;
        }

        public async Task<(IList<SalesCommission> data, int total, int totalDisplay)> GetPagedSalesCommissionsAsync(
            int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(search.Value))
                {
                    return await GetDynamicAsync(null, order,
                        x => x.Include(n => n.Salesperson).Include(n => n.SalesInvoice),
                        pageIndex, pageSize, true);
                }
                else
                {
                    return await GetDynamicAsync(
                        x => x.Salesperson!.SalespersonName.Contains(search.Value) ||
                             x.Salesperson!.SalespersonCode.Contains(search.Value) ||
                             x.SalesInvoice!.InvoiceNo.Contains(search.Value),
                        order,
                        x => x.Include(n => n.Salesperson).Include(n => n.SalesInvoice),
                        pageIndex, pageSize, true);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<SalesCommission?> GetSalesCommissionByIdAsync(Guid id)
        {
            return await _inventoryDbContext.SalesCommissions
                .Include(x => x.Salesperson)
                .Include(x => x.SalesInvoice!)
                    .ThenInclude(x => x.Customer)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IList<SalesCommission>> GetSalesCommissionsBySalespersonAsync(Guid salespersonId)
        {
            return await _inventoryDbContext.SalesCommissions
                .Include(x => x.SalesInvoice!)
                    .ThenInclude(x => x.Customer)
                .Where(x => x.SalespersonId == salespersonId)
                .OrderByDescending(x => x.EarnedDate)
                .ToListAsync();
        }

        public async Task<SalesCommission?> GetSalesCommissionBySalesInvoiceAsync(Guid salesInvoiceId)
        {
            return await _inventoryDbContext.SalesCommissions
                .FirstOrDefaultAsync(x => x.SalesInvoiceId == salesInvoiceId);
        }

        public async Task<IList<SalesCommission>> GetSalesCommissionsByStatusAsync(params CommissionStatus[] statuses)
        {
            return await _inventoryDbContext.SalesCommissions
                .Include(x => x.Salesperson)
                .Include(x => x.SalesInvoice)
                .Where(x => statuses.Contains(x.Status))
                .OrderByDescending(x => x.EarnedDate)
                .ToListAsync();
        }

        public async Task<IList<SalesCommission>> GetSalesCommissionsByDateRangeAsync(DateTime fromDate, DateTime toDate)
        {
            return await _inventoryDbContext.SalesCommissions
                .Include(x => x.Salesperson)
                .Include(x => x.SalesInvoice)
                .Where(x => x.EarnedDate >= fromDate && x.EarnedDate <= toDate)
                .OrderBy(x => x.EarnedDate)
                .ToListAsync();
        }
    }
}
