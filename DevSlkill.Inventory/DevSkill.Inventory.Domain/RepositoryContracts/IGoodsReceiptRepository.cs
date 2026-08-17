using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using DevSkill.Inventory.Domain.Enums;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface IGoodsReceiptRepository : IRepositoryBase<GoodsReceipt, Guid>
    {
        Task<(IList<GoodsReceipt> data, int total, int totalDisplay)> GetPagedGoodsReceiptsAsync(int pageIndex,
            int pageSize, DataTablesSearch search, string? order);
        Task<GoodsReceipt?> GetGoodsReceiptByIdAsync(Guid id);
        Task<IList<GoodsReceipt>> GetGoodsReceiptsByPurchaseOrderAsync(Guid purchaseOrderId);
        Task<IList<GoodsReceipt>> GetGoodsReceiptsByStatusAsync(params GoodsReceiptStatus[] statuses);
        Task<IList<GoodsReceipt>> GetGoodsReceiptsBySupplierAsync(Guid supplierId);

        /// <summary>
        /// Receipts dated within a period, with their lines. The reporting side reads
        /// what actually arrived through this.
        /// </summary>
        Task<IList<GoodsReceipt>> GetGoodsReceiptsByDateRangeAsync(DateTime fromDate, DateTime toDate);
        Task<bool> IsGoodsReceiptNoDuplicateAsync(string goodsReceiptNo);
    }
}
