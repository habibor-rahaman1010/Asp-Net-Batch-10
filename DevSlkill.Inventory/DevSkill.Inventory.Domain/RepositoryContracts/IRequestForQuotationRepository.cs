using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using DevSkill.Inventory.Domain.Enums;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface IRequestForQuotationRepository : IRepositoryBase<RequestForQuotation, Guid>
    {
        Task<(IList<RequestForQuotation> data, int total, int totalDisplay)> GetPagedRequestForQuotationsAsync(
            int pageIndex, int pageSize, DataTablesSearch search, string? order);
        Task<RequestForQuotation?> GetRequestForQuotationByIdAsync(Guid id);

        /// <summary>
        /// Used by the flows that only work on requests in a certain state, quoting
        /// against an open one being the first of them.
        /// </summary>
        Task<IList<RequestForQuotation>> GetRequestForQuotationsByStatusAsync(
            params RequestForQuotationStatus[] statuses);
        Task<IList<RequestForQuotation>> GetRequestForQuotationsByRequisitionAsync(Guid purchaseRequisitionId);
        Task<bool> IsRfqNoDuplicateAsync(string rfqNo);
    }
}
