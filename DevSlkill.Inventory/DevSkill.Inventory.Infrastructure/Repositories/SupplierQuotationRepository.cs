using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Domain.RepositoryContracts;
using DevSkill.Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class SupplierQuotationRepository : Repository<SupplierQuotation, Guid>, ISupplierQuotationRepository
    {
        private readonly InventoryDbContext _inventoryDbContext;

        public SupplierQuotationRepository(InventoryDbContext inventoryDbContext) : base(inventoryDbContext)
        {
            _inventoryDbContext = inventoryDbContext;
        }

        public async Task<(IList<SupplierQuotation> data, int total, int totalDisplay)>
            GetPagedSupplierQuotationsAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(search.Value))
                {
                    return await GetDynamicAsync(null, order,
                        x => x.Include(n => n.Supplier).Include(n => n.RequestForQuotation).Include(n => n.PaymentTerm),
                        pageIndex, pageSize, true);
                }
                else
                {
                    return await GetDynamicAsync(
                        x => x.QuotationNo.Contains(search.Value) ||
                             x.SupplierQuotationNo.Contains(search.Value) ||
                             x.RequestForQuotation!.RfqNo.Contains(search.Value) ||
                             x.Supplier!.SupplierName.Contains(search.Value),
                        order,
                        x => x.Include(n => n.Supplier).Include(n => n.RequestForQuotation).Include(n => n.PaymentTerm),
                        pageIndex, pageSize, true);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<SupplierQuotation?> GetSupplierQuotationByIdAsync(Guid id)
        {
            return await _inventoryDbContext.SupplierQuotations
                .Include(x => x.Supplier)
                .Include(x => x.PaymentTerm)
                .Include(x => x.RequestForQuotation)
                .Include(x => x.SupplierQuotationItems!)
                    .ThenInclude(x => x.Product)
                        .ThenInclude(x => x!.Unit)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IList<SupplierQuotation>> GetSupplierQuotationsByRequestAsync(Guid requestForQuotationId)
        {
            return await _inventoryDbContext.SupplierQuotations
                .Include(x => x.Supplier)
                .Include(x => x.PaymentTerm)
                .Include(x => x.SupplierQuotationItems!)
                    .ThenInclude(x => x.Product)
                        .ThenInclude(x => x!.Unit)
                .Where(x => x.RequestForQuotationId == requestForQuotationId)
                .OrderBy(x => x.GrandTotal)
                .ToListAsync();
        }

        public async Task<IList<SupplierQuotation>> GetSupplierQuotationsBySupplierAsync(Guid supplierId)
        {
            return await _inventoryDbContext.SupplierQuotations
                .Include(x => x.RequestForQuotation)
                .Include(x => x.SupplierQuotationItems)
                .Where(x => x.SupplierId == supplierId)
                .OrderByDescending(x => x.QuotationDate)
                .ToListAsync();
        }

        public async Task<IList<SupplierQuotation>> GetSupplierQuotationsByStatusAsync(
            params SupplierQuotationStatus[] statuses)
        {
            return await _inventoryDbContext.SupplierQuotations
                .Include(x => x.Supplier)
                .Include(x => x.RequestForQuotation)
                .Where(x => statuses.Contains(x.Status))
                .OrderByDescending(x => x.QuotationDate)
                .ToListAsync();
        }

        public async Task<bool> IsQuotationNoDuplicateAsync(string quotationNo)
        {
            return await GetCountAsync(x => x.QuotationNo == quotationNo) > 0;
        }

        public async Task<bool> HasSupplierQuotedAsync(Guid requestForQuotationId, Guid supplierId, Guid? id = null)
        {
            return await _inventoryDbContext.SupplierQuotations
                .AnyAsync(x => x.RequestForQuotationId == requestForQuotationId
                    && x.SupplierId == supplierId
                    && x.Status != SupplierQuotationStatus.Cancelled
                    && (!id.HasValue || x.Id != id.Value));
        }
    }
}
