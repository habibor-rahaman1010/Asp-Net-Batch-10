using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using DevSkill.Inventory.Domain.RepositoryContracts;
using DevSkill.Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class PurchaseReturnRepository : Repository<PurchaseReturn, Guid>, IPurchaseReturnRepository
    {
        private readonly InventoryDbContext _inventoryDbContext;

        public PurchaseReturnRepository(InventoryDbContext inventoryDbContext) : base(inventoryDbContext)
        {
            _inventoryDbContext = inventoryDbContext;
        }

        public async Task<(IList<PurchaseReturn> data, int total, int totalDisplay)> GetPagedPurchaseReturnsAsync(
            int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(search.Value))
                {
                    return await GetDynamicAsync(null, order,
                        x => x.Include(n => n.Supplier).Include(n => n.BusinessLocation).Include(n => n.GoodsReceipt),
                        pageIndex, pageSize, true);
                }
                else
                {
                    return await GetDynamicAsync(
                        x => x.ReturnNo.Contains(search.Value) ||
                             x.Reason.Contains(search.Value) ||
                             x.GoodsReceipt!.GoodsReceiptNo.Contains(search.Value) ||
                             x.Supplier!.SupplierName.Contains(search.Value),
                        order,
                        x => x.Include(n => n.Supplier).Include(n => n.BusinessLocation).Include(n => n.GoodsReceipt),
                        pageIndex, pageSize, true);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<PurchaseReturn?> GetPurchaseReturnByIdAsync(Guid id)
        {
            return await _inventoryDbContext.PurchaseReturns
                .Include(x => x.Supplier)
                .Include(x => x.BusinessLocation)
                .Include(x => x.GoodsReceipt)
                .Include(x => x.PurchaseReturnItems!)
                    .ThenInclude(x => x.Product)
                        .ThenInclude(x => x!.Unit)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <summary>
        /// Every return raised against one receipt, including the cancelled ones. The
        /// callers decide which of them still count.
        /// </summary>
        public async Task<IList<PurchaseReturn>> GetPurchaseReturnsByGoodsReceiptAsync(Guid goodsReceiptId)
        {
            return await _inventoryDbContext.PurchaseReturns
                .Include(x => x.PurchaseReturnItems)
                .Where(x => x.GoodsReceiptId == goodsReceiptId)
                .ToListAsync();
        }

        public async Task<IList<PurchaseReturn>> GetPurchaseReturnsBySupplierAsync(Guid supplierId)
        {
            return await _inventoryDbContext.PurchaseReturns
                .Include(x => x.GoodsReceipt)
                .Include(x => x.PurchaseReturnItems)
                .Where(x => x.SupplierId == supplierId)
                .OrderByDescending(x => x.ReturnDate)
                .ToListAsync();
        }

        public async Task<IList<PurchaseReturn>> GetPurchaseReturnsByDateRangeAsync(DateTime fromDate, DateTime toDate)
        {
            return await _inventoryDbContext.PurchaseReturns
                .Include(x => x.Supplier)
                .Include(x => x.BusinessLocation)
                .Include(x => x.GoodsReceipt)
                .Include(x => x.PurchaseReturnItems!)
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
