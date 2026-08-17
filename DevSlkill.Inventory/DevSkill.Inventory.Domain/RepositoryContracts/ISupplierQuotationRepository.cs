using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using DevSkill.Inventory.Domain.Enums;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface ISupplierQuotationRepository : IRepositoryBase<SupplierQuotation, Guid>
    {
        Task<(IList<SupplierQuotation> data, int total, int totalDisplay)> GetPagedSupplierQuotationsAsync(
            int pageIndex, int pageSize, DataTablesSearch search, string? order);
        Task<SupplierQuotation?> GetSupplierQuotationByIdAsync(Guid id);

        /// <summary>
        /// Every quotation received against one request, which is exactly what the
        /// comparison reads.
        /// </summary>
        Task<IList<SupplierQuotation>> GetSupplierQuotationsByRequestAsync(Guid requestForQuotationId);
        Task<IList<SupplierQuotation>> GetSupplierQuotationsBySupplierAsync(Guid supplierId);
        Task<IList<SupplierQuotation>> GetSupplierQuotationsByStatusAsync(params SupplierQuotationStatus[] statuses);
        Task<bool> IsQuotationNoDuplicateAsync(string quotationNo);

        /// <summary>
        /// One supplier answers a given request once; a revised price is an edit of
        /// that same quotation rather than a second one.
        /// </summary>
        Task<bool> HasSupplierQuotedAsync(Guid requestForQuotationId, Guid supplierId, Guid? id = null);
    }
}
