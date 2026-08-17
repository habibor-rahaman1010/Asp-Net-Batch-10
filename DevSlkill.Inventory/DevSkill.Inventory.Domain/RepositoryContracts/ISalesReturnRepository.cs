using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.Enums;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface ISalesReturnRepository : IRepositoryBase<SalesReturn, Guid>
    {
        Task<(IList<SalesReturn> data, int total, int totalDisplay)> GetPagedSalesReturnsAsync(int pageIndex,
            int pageSize, DataTablesSearch search, string? order);
        Task<SalesReturn?> GetSalesReturnByIdAsync(Guid id);

        /// <summary>
        /// Every return written against one invoice, whatever its state. The return
        /// screen works out what may still come back from these.
        /// </summary>
        Task<IList<SalesReturn>> GetSalesReturnsBySalesInvoiceAsync(Guid salesInvoiceId);

        Task<IList<SalesReturn>> GetSalesReturnsByCustomerAsync(Guid customerId);
        Task<IList<SalesReturn>> GetSalesReturnsByStatusAsync(params SalesReturnStatus[] statuses);
        Task<IList<SalesReturn>> GetSalesReturnsByDateRangeAsync(DateTime fromDate, DateTime toDate);
        Task<bool> IsReturnNoDuplicateAsync(string returnNo);
    }
}
