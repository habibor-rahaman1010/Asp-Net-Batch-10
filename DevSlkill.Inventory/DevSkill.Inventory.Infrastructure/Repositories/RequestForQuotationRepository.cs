using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Domain.RepositoryContracts;
using DevSkill.Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class RequestForQuotationRepository
        : Repository<RequestForQuotation, Guid>, IRequestForQuotationRepository
    {
        private readonly InventoryDbContext _inventoryDbContext;

        public RequestForQuotationRepository(InventoryDbContext inventoryDbContext) : base(inventoryDbContext)
        {
            _inventoryDbContext = inventoryDbContext;
        }

        public async Task<(IList<RequestForQuotation> data, int total, int totalDisplay)>
            GetPagedRequestForQuotationsAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(search.Value))
                {
                    return await GetDynamicAsync(null, order,
                        x => x.Include(n => n.BusinessLocation).Include(n => n.PurchaseRequisition),
                        pageIndex, pageSize, true);
                }
                else
                {
                    return await GetDynamicAsync(
                        x => x.RfqNo.Contains(search.Value) ||
                             x.Notes.Contains(search.Value) ||
                             x.BusinessLocation!.LocationName.Contains(search.Value),
                        order,
                        x => x.Include(n => n.BusinessLocation).Include(n => n.PurchaseRequisition),
                        pageIndex, pageSize, true);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<RequestForQuotation?> GetRequestForQuotationByIdAsync(Guid id)
        {
            return await _inventoryDbContext.RequestForQuotations
                .Include(x => x.BusinessLocation)
                .Include(x => x.PurchaseRequisition)
                .Include(x => x.RequestForQuotationItems!)
                    .ThenInclude(x => x.Product)
                        .ThenInclude(x => x!.Unit)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IList<RequestForQuotation>> GetRequestForQuotationsByStatusAsync(
            params RequestForQuotationStatus[] statuses)
        {
            return await _inventoryDbContext.RequestForQuotations
                .Include(x => x.BusinessLocation)
                .Where(x => statuses.Contains(x.Status))
                .OrderByDescending(x => x.RfqDate)
                .ToListAsync();
        }

        public async Task<IList<RequestForQuotation>> GetRequestForQuotationsByRequisitionAsync(
            Guid purchaseRequisitionId)
        {
            return await _inventoryDbContext.RequestForQuotations
                .Include(x => x.RequestForQuotationItems)
                .Where(x => x.PurchaseRequisitionId == purchaseRequisitionId)
                .ToListAsync();
        }

        public async Task<bool> IsRfqNoDuplicateAsync(string rfqNo)
        {
            return await GetCountAsync(x => x.RfqNo == rfqNo) > 0;
        }
    }
}
