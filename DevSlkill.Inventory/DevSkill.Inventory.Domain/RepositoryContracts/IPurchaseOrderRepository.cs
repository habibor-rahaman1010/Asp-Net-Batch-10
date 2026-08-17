using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using DevSkill.Inventory.Domain.Enums;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface IPurchaseOrderRepository : IRepositoryBase<PurchaseOrder, Guid>
    {
        Task<(IList<PurchaseOrder> data, int total, int totalDisplay)> GetPagedPurchaseOrdersAsync(int pageIndex,
            int pageSize, DataTablesSearch search, string? order);
        Task<PurchaseOrder?> GetPurchaseOrderByIdAsync(Guid id);
        Task<IList<PurchaseOrder>> GetPurchaseOrdersByStatusAsync(params PurchaseOrderStatus[] statuses);
        Task<IList<PurchaseOrder>> GetPurchaseOrdersByRequisitionAsync(Guid purchaseRequisitionId);
        Task<IList<PurchaseOrder>> GetPurchaseOrdersBySupplierAsync(Guid supplierId);

        /// <summary>
        /// Orders raised within a period, with their lines. The reporting side reads
        /// the purchase book through this.
        /// </summary>
        Task<IList<PurchaseOrder>> GetPurchaseOrdersByDateRangeAsync(DateTime fromDate, DateTime toDate);
        Task<bool> IsPurchaseOrderNoDuplicateAsync(string purchaseOrderNo);
    }
}
