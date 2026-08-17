using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities.PurchaseEntities;

namespace DevSkill.Inventory.Application.ServicesContract
{
    public interface ISupplierQuotationManagementService
    {
        Task<Guid> CreateSupplierQuotationAsync(SupplierQuotation supplierQuotation);
        Task<bool> UpdateSupplierQuotationAsync(SupplierQuotation supplierQuotation);
        Task<bool> DeleteSupplierQuotationAsync(Guid id);
        Task<SupplierQuotation?> GetSupplierQuotationByIdAsync(Guid id);
        Task<(IList<SupplierQuotation> data, int total, int totalDisplay)> GetSupplierQuotationsAsync(int pageIndex,
            int pageSize, DataTablesSearch search, string? order);
        Task<string> GenerateQuotationNoAsync();

        Task<IList<SupplierQuotation>> GetSupplierQuotationsByRequestAsync(Guid requestForQuotationId);

        /// <summary>
        /// The quotations that won their request, which are the only ones a purchase
        /// order may be raised from.
        /// </summary>
        Task<IList<SupplierQuotation>> GetAwardedQuotationsAsync();

        /// <summary>
        /// Every offer against one request, lined up so price, terms and delivery can
        /// be read across in one go.
        /// </summary>
        Task<QuotationComparisonDto> GetComparisonAsync(Guid requestForQuotationId);

        /// <summary>
        /// Picks the winner. The chosen quotation is marked selected, every other one
        /// on the request is rejected and the request itself is awarded, all in one
        /// transaction.
        /// </summary>
        Task SelectWinningQuotationAsync(Guid supplierQuotationId);

        /// <summary>
        /// Puts the request back out to bid, which is only possible while no order has
        /// been raised from the winning quotation.
        /// </summary>
        Task UndoAwardAsync(Guid requestForQuotationId);

        Task CancelSupplierQuotationAsync(Guid id);

        /// <summary>
        /// The lines of an awarded quotation, shaped for the purchase order screen so
        /// a won quote can become an order without retyping.
        /// </summary>
        Task<IList<QuotationLineDto>> GetQuotationLinesAsync(Guid supplierQuotationId);
    }
}
