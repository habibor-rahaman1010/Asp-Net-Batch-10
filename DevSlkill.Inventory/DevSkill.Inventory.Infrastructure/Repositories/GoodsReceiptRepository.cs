using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Domain.RepositoryContracts;
using DevSkill.Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class GoodsReceiptRepository : Repository<GoodsReceipt, Guid>, IGoodsReceiptRepository
    {
        private readonly InventoryDbContext _inventoryDbContext;

        public GoodsReceiptRepository(InventoryDbContext inventoryDbContext) : base(inventoryDbContext)
        {
            _inventoryDbContext = inventoryDbContext;
        }

        public async Task<(IList<GoodsReceipt> data, int total, int totalDisplay)> GetPagedGoodsReceiptsAsync(
            int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(search.Value))
                {
                    return await GetDynamicAsync(null, order,
                        x => x.Include(n => n.Supplier).Include(n => n.BusinessLocation).Include(n => n.PurchaseOrder),
                        pageIndex, pageSize, true);
                }
                else
                {
                    return await GetDynamicAsync(
                        x => x.GoodsReceiptNo.Contains(search.Value) ||
                             x.SupplierChallanNo.Contains(search.Value) ||
                             x.PurchaseOrder!.PurchaseOrderNo.Contains(search.Value) ||
                             x.Supplier!.SupplierName.Contains(search.Value),
                        order,
                        x => x.Include(n => n.Supplier).Include(n => n.BusinessLocation).Include(n => n.PurchaseOrder),
                        pageIndex, pageSize, true);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<GoodsReceipt?> GetGoodsReceiptByIdAsync(Guid id)
        {
            return await _inventoryDbContext.GoodsReceipts
                .Include(x => x.Supplier)
                .Include(x => x.BusinessLocation)
                .Include(x => x.PurchaseOrder)
                .Include(x => x.GoodsReceiptItems!)
                    .ThenInclude(x => x.Product)
                        .ThenInclude(x => x!.Unit)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <summary>
        /// Every receipt raised against one order, including the cancelled ones. The
        /// callers decide which of them still count.
        /// </summary>
        public async Task<IList<GoodsReceipt>> GetGoodsReceiptsByPurchaseOrderAsync(Guid purchaseOrderId)
        {
            return await _inventoryDbContext.GoodsReceipts
                .Include(x => x.GoodsReceiptItems)
                .Where(x => x.PurchaseOrderId == purchaseOrderId)
                .ToListAsync();
        }

        /// <summary>
        /// Used by the flows that only work on receipts in a certain state, the
        /// purchase return being the first of them.
        /// </summary>
        public async Task<IList<GoodsReceipt>> GetGoodsReceiptsByStatusAsync(params GoodsReceiptStatus[] statuses)
        {
            return await _inventoryDbContext.GoodsReceipts
                .Include(x => x.Supplier)
                .Include(x => x.BusinessLocation)
                .Where(x => statuses.Contains(x.Status))
                .OrderByDescending(x => x.ReceiptDate)
                .ToListAsync();
        }

        public async Task<IList<GoodsReceipt>> GetGoodsReceiptsBySupplierAsync(Guid supplierId)
        {
            return await _inventoryDbContext.GoodsReceipts
                .Include(x => x.PurchaseOrder)
                .Include(x => x.GoodsReceiptItems)
                .Where(x => x.SupplierId == supplierId)
                .OrderByDescending(x => x.ReceiptDate)
                .ToListAsync();
        }

        public async Task<IList<GoodsReceipt>> GetGoodsReceiptsByDateRangeAsync(DateTime fromDate, DateTime toDate)
        {
            return await _inventoryDbContext.GoodsReceipts
                .Include(x => x.Supplier)
                .Include(x => x.BusinessLocation)
                .Include(x => x.PurchaseOrder)
                .Include(x => x.GoodsReceiptItems!)
                    .ThenInclude(x => x.Product)
                        .ThenInclude(x => x!.Unit)
                .Where(x => x.ReceiptDate >= fromDate && x.ReceiptDate <= toDate)
                .OrderBy(x => x.ReceiptDate)
                .ToListAsync();
        }

        public async Task<bool> IsGoodsReceiptNoDuplicateAsync(string goodsReceiptNo)
        {
            return await GetCountAsync(x => x.GoodsReceiptNo == goodsReceiptNo) > 0;
        }
    }
}
