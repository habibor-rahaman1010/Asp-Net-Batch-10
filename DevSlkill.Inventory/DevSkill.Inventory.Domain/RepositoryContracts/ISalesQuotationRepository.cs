using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.Enums;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface ISalesQuotationRepository : IRepositoryBase<SalesQuotation, Guid>
    {
        Task<(IList<SalesQuotation> data, int total, int totalDisplay)> GetPagedSalesQuotationsAsync(int pageIndex,
            int pageSize, DataTablesSearch search, string? order);
        Task<SalesQuotation?> GetSalesQuotationByIdAsync(Guid id);
        Task<IList<SalesQuotation>> GetSalesQuotationsByStatusAsync(params SalesQuotationStatus[] statuses);
        Task<IList<SalesQuotation>> GetSalesQuotationsByCustomerAsync(Guid customerId);

        /// <summary>Quotations raised within a period, with their lines, for reporting.</summary>
        Task<IList<SalesQuotation>> GetSalesQuotationsByDateRangeAsync(DateTime fromDate, DateTime toDate);
        Task<bool> IsQuotationNoDuplicateAsync(string quotationNo);
    }
}
