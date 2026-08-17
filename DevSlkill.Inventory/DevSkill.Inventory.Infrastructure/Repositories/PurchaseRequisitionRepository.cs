using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Domain.RepositoryContracts;
using DevSkill.Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class PurchaseRequisitionRepository : Repository<PurchaseRequisition, Guid>, IPurchaseRequisitionRepository
    {
        private readonly InventoryDbContext _inventoryDbContext;

        public PurchaseRequisitionRepository(InventoryDbContext inventoryDbContext) : base(inventoryDbContext)
        {
            _inventoryDbContext = inventoryDbContext;
        }

        public async Task<(IList<PurchaseRequisition> data, int total, int totalDisplay)> GetPagedPurchaseRequisitionsAsync(
            int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(search.Value))
                {
                    return await GetDynamicAsync(null, order,
                        x => x.Include(n => n.BusinessLocation),
                        pageIndex, pageSize, true);
                }
                else
                {
                    return await GetDynamicAsync(
                        x => x.RequisitionNo.Contains(search.Value) ||
                             x.RequestedByName.Contains(search.Value) ||
                             x.BusinessLocation!.LocationName.Contains(search.Value),
                        order,
                        x => x.Include(n => n.BusinessLocation),
                        pageIndex, pageSize, true);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<PurchaseRequisition?> GetPurchaseRequisitionByIdAsync(Guid id)
        {
            return await _inventoryDbContext.PurchaseRequisitions
                .Include(x => x.BusinessLocation)
                .Include(x => x.PurchaseRequisitionItems!)
                    .ThenInclude(x => x.Product)
                        .ThenInclude(x => x!.Unit)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <summary>
        /// Used by the flows that only work on requisitions in a certain state, the
        /// purchase order conversion being the first of them.
        /// </summary>
        public async Task<IList<PurchaseRequisition>> GetPurchaseRequisitionsByStatusAsync(
            params PurchaseRequisitionStatus[] statuses)
        {
            return await _inventoryDbContext.PurchaseRequisitions
                .Include(x => x.BusinessLocation)
                .Where(x => statuses.Contains(x.Status))
                .OrderByDescending(x => x.RequisitionDate)
                .ToListAsync();
        }

        public async Task<bool> IsRequisitionNoDuplicateAsync(string requisitionNo)
        {
            return await GetCountAsync(x => x.RequisitionNo == requisitionNo) > 0;
        }
    }
}
