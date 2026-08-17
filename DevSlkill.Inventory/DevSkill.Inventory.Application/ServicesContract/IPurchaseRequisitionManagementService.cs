using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using DevSkill.Inventory.Domain.Enums;

namespace DevSkill.Inventory.Application.ServicesContract
{
    public interface IPurchaseRequisitionManagementService
    {
        Task<Guid> CreatePurchaseRequisitionAsync(PurchaseRequisition purchaseRequisition);
        Task<bool> UpdatePurchaseRequisitionAsync(PurchaseRequisition purchaseRequisition);
        Task<bool> DeletePurchaseRequisitionAsync(Guid id);
        Task<PurchaseRequisition?> GetPurchaseRequisitionByIdAsync(Guid id);
        Task<(IList<PurchaseRequisition> data, int total, int totalDisplay)> GetPurchaseRequisitionsAsync(int pageIndex,
            int pageSize, DataTablesSearch search, string? order);
        Task<IList<PurchaseRequisition>> GetPurchaseRequisitionsByStatusAsync(params PurchaseRequisitionStatus[] statuses);
        Task<string> GenerateRequisitionNoAsync();

        Task SubmitPurchaseRequisitionAsync(Guid id);
        Task ApprovePurchaseRequisitionAsync(Guid id, Guid? approvedById, string approvedByName);
        Task RejectPurchaseRequisitionAsync(Guid id, Guid? approvedById, string approvedByName, string reason);
        Task CancelPurchaseRequisitionAsync(Guid id);
    }
}
