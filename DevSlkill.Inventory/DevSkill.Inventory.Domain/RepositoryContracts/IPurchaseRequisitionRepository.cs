using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using DevSkill.Inventory.Domain.Enums;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface IPurchaseRequisitionRepository : IRepositoryBase<PurchaseRequisition, Guid>
    {
        Task<(IList<PurchaseRequisition> data, int total, int totalDisplay)> GetPagedPurchaseRequisitionsAsync(int pageIndex,
            int pageSize, DataTablesSearch search, string? order);
        Task<PurchaseRequisition?> GetPurchaseRequisitionByIdAsync(Guid id);
        Task<IList<PurchaseRequisition>> GetPurchaseRequisitionsByStatusAsync(params PurchaseRequisitionStatus[] statuses);
        Task<bool> IsRequisitionNoDuplicateAsync(string requisitionNo);
    }
}
