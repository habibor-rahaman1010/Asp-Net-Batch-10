using DevSkill.Inventory.Domain.Entities.PurchaseEntities;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface IPurchaseReturnRepository : IRepositoryBase<PurchaseReturn, Guid>
    {
        Task<(IList<PurchaseReturn> data, int total, int totalDisplay)> GetPagedPurchaseReturnsAsync(int pageIndex,
            int pageSize, DataTablesSearch search, string? order);
        Task<PurchaseReturn?> GetPurchaseReturnByIdAsync(Guid id);

        /// <summary>
        /// Every return raised against one goods receipt, cancelled ones included.
        /// The callers decide which of them still count.
        /// </summary>
        Task<IList<PurchaseReturn>> GetPurchaseReturnsByGoodsReceiptAsync(Guid goodsReceiptId);
        Task<IList<PurchaseReturn>> GetPurchaseReturnsBySupplierAsync(Guid supplierId);

        /// <summary>
        /// Returns dated within a period, with their lines. The reporting side reads
        /// what went back through this.
        /// </summary>
        Task<IList<PurchaseReturn>> GetPurchaseReturnsByDateRangeAsync(DateTime fromDate, DateTime toDate);
        Task<bool> IsReturnNoDuplicateAsync(string returnNo);
    }
}
