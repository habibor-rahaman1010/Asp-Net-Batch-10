using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using DevSkill.Inventory.Domain.Enums;

namespace DevSkill.Inventory.Application.ServicesContract
{
    public interface IPurchaseOrderManagementService
    {
        Task<Guid> CreatePurchaseOrderAsync(PurchaseOrder purchaseOrder);
        Task<bool> UpdatePurchaseOrderAsync(PurchaseOrder purchaseOrder);
        Task<bool> DeletePurchaseOrderAsync(Guid id);
        Task<PurchaseOrder?> GetPurchaseOrderByIdAsync(Guid id);
        Task<(IList<PurchaseOrder> data, int total, int totalDisplay)> GetPurchaseOrdersAsync(int pageIndex,
            int pageSize, DataTablesSearch search, string? order);
        Task<IList<PurchaseOrder>> GetPurchaseOrdersByStatusAsync(params PurchaseOrderStatus[] statuses);
        Task<string> GeneratePurchaseOrderNoAsync();

        Task SubmitPurchaseOrderAsync(Guid id);
        Task ApprovePurchaseOrderAsync(Guid id, Guid? approvedById, string approvedByName);
        Task RejectPurchaseOrderAsync(Guid id, Guid? approvedById, string approvedByName, string reason);
        Task CancelPurchaseOrderAsync(Guid id);
        Task ClosePurchaseOrderAsync(Guid id);

        /// <summary>
        /// The still open part of an approved requisition, ready to be dropped onto a
        /// new purchase order.
        /// </summary>
        Task<IList<ConvertibleRequisitionLineDto>> GetConvertibleRequisitionLinesAsync(Guid purchaseRequisitionId);

        /// <summary>
        /// Live preview of one order line, run through the very same server side
        /// arithmetic used when the order is saved.
        /// </summary>
        Task<PurchaseLinePreviewDto?> GetLinePreviewAsync(Guid productId, decimal quantity,
            decimal unitPrice, decimal discountAmount);
    }
}
