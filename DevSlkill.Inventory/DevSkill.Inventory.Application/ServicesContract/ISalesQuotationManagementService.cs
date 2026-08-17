using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.Enums;

namespace DevSkill.Inventory.Application.ServicesContract
{
    public interface ISalesQuotationManagementService
    {
        Task<Guid> CreateSalesQuotationAsync(SalesQuotation salesQuotation);
        Task<bool> UpdateSalesQuotationAsync(SalesQuotation salesQuotation);
        Task<bool> DeleteSalesQuotationAsync(Guid id);
        Task<SalesQuotation?> GetSalesQuotationByIdAsync(Guid id);
        Task<(IList<SalesQuotation> data, int total, int totalDisplay)> GetSalesQuotationsAsync(int pageIndex,
            int pageSize, DataTablesSearch search, string? order);
        Task<IList<SalesQuotation>> GetSalesQuotationsByStatusAsync(params SalesQuotationStatus[] statuses);
        Task<string> GenerateQuotationNoAsync();

        /// <summary>Hands the offer to the customer; after this the prices are being held.</summary>
        Task SendSalesQuotationAsync(Guid id);

        /// <summary>The customer has taken the offer, so it may become a sales order.</summary>
        Task AcceptSalesQuotationAsync(Guid id);

        Task RejectSalesQuotationAsync(Guid id, string reason);

        /// <summary>
        /// Marks an offer whose validity has run out. Only a sent one can expire; an
        /// accepted offer stays accepted whatever the date says.
        /// </summary>
        Task ExpireSalesQuotationAsync(Guid id);

        Task CancelSalesQuotationAsync(Guid id);

        /// <summary>
        /// The still open part of an accepted quotation, ready to be dropped onto a
        /// new sales order.
        /// </summary>
        Task<IList<ConvertibleQuotationLineDto>> GetConvertibleQuotationLinesAsync(Guid salesQuotationId);

        /// <summary>
        /// Live preview of one quotation line, run through the very same server side
        /// arithmetic used when the quotation is saved.
        /// </summary>
        Task<SalesLinePreviewDto?> GetLinePreviewAsync(Guid productId, decimal quantity,
            decimal unitPrice, decimal discountAmount);
    }
}
