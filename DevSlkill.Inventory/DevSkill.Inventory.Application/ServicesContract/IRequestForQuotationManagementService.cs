using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using DevSkill.Inventory.Domain.Enums;

namespace DevSkill.Inventory.Application.ServicesContract
{
    public interface IRequestForQuotationManagementService
    {
        Task<Guid> CreateRequestForQuotationAsync(RequestForQuotation requestForQuotation);
        Task<bool> UpdateRequestForQuotationAsync(RequestForQuotation requestForQuotation);
        Task<bool> DeleteRequestForQuotationAsync(Guid id);
        Task<RequestForQuotation?> GetRequestForQuotationByIdAsync(Guid id);
        Task<(IList<RequestForQuotation> data, int total, int totalDisplay)> GetRequestForQuotationsAsync(
            int pageIndex, int pageSize, DataTablesSearch search, string? order);
        Task<string> GenerateRfqNoAsync();

        Task<IList<RequestForQuotation>> GetRequestForQuotationsByStatusAsync(
            params RequestForQuotationStatus[] statuses);

        /// <summary>
        /// Sends the request out. Only from this point can a supplier quotation be
        /// recorded against it, and the lines stop changing.
        /// </summary>
        Task SendRequestForQuotationAsync(Guid id);

        /// <summary>
        /// Stops taking quotations without picking a winner, for a request that came
        /// to nothing.
        /// </summary>
        Task CloseRequestForQuotationAsync(Guid id);

        Task CancelRequestForQuotationAsync(Guid id);
    }
}
