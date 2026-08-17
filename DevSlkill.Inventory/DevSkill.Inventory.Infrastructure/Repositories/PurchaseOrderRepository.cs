using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Domain.RepositoryContracts;
using DevSkill.Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class PurchaseOrderRepository : Repository<PurchaseOrder, Guid>, IPurchaseOrderRepository
    {
        private readonly InventoryDbContext _inventoryDbContext;

        public PurchaseOrderRepository(InventoryDbContext inventoryDbContext) : base(inventoryDbContext)
        {
            _inventoryDbContext = inventoryDbContext;
        }

        public async Task<(IList<PurchaseOrder> data, int total, int totalDisplay)> GetPagedPurchaseOrdersAsync(
            int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(search.Value))
                {
                    return await GetDynamicAsync(null, order,
                        x => x.Include(n => n.Supplier).Include(n => n.BusinessLocation),
                        pageIndex, pageSize, true);
                }
                else
                {
                    return await GetDynamicAsync(
                        x => x.PurchaseOrderNo.Contains(search.Value) ||
                             x.Supplier!.SupplierName.Contains(search.Value) ||
                             x.Supplier!.SupplierCode.Contains(search.Value),
                        order,
                        x => x.Include(n => n.Supplier).Include(n => n.BusinessLocation),
                        pageIndex, pageSize, true);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<PurchaseOrder?> GetPurchaseOrderByIdAsync(Guid id)
        {
            return await _inventoryDbContext.PurchaseOrders
                .Include(x => x.Supplier)
                .Include(x => x.BusinessLocation)
                .Include(x => x.PaymentTerm)
                .Include(x => x.PurchaseRequisition)
                .Include(x => x.PurchaseOrderItems!)
                    .ThenInclude(x => x.Product)
                        .ThenInclude(x => x!.Unit)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <summary>
        /// Used by the flows that only work on orders in a certain state, receiving
        /// and invoicing being the first of them.
        /// </summary>
        public async Task<IList<PurchaseOrder>> GetPurchaseOrdersByStatusAsync(params PurchaseOrderStatus[] statuses)
        {
            return await _inventoryDbContext.PurchaseOrders
                .Include(x => x.Supplier)
                .Include(x => x.BusinessLocation)
                .Where(x => statuses.Contains(x.Status))
                .OrderByDescending(x => x.OrderDate)
                .ToListAsync();
        }

        public async Task<IList<PurchaseOrder>> GetPurchaseOrdersByRequisitionAsync(Guid purchaseRequisitionId)
        {
            return await _inventoryDbContext.PurchaseOrders
                .Include(x => x.PurchaseOrderItems)
                .Where(x => x.PurchaseRequisitionId == purchaseRequisitionId)
                .ToListAsync();
        }

        public async Task<IList<PurchaseOrder>> GetPurchaseOrdersBySupplierAsync(Guid supplierId)
        {
            return await _inventoryDbContext.PurchaseOrders
                .Include(x => x.BusinessLocation)
                .Where(x => x.SupplierId == supplierId)
                .OrderByDescending(x => x.OrderDate)
                .ToListAsync();
        }

        public async Task<IList<PurchaseOrder>> GetPurchaseOrdersByDateRangeAsync(DateTime fromDate, DateTime toDate)
        {
            return await _inventoryDbContext.PurchaseOrders
                .Include(x => x.Supplier)
                .Include(x => x.BusinessLocation)
                .Include(x => x.PurchaseOrderItems!)
                    .ThenInclude(x => x.Product)
                        .ThenInclude(x => x!.Unit)
                .Where(x => x.OrderDate >= fromDate && x.OrderDate <= toDate)
                .OrderBy(x => x.OrderDate)
                .ToListAsync();
        }

        public async Task<bool> IsPurchaseOrderNoDuplicateAsync(string purchaseOrderNo)
        {
            return await GetCountAsync(x => x.PurchaseOrderNo == purchaseOrderNo) > 0;
        }
    }
}
