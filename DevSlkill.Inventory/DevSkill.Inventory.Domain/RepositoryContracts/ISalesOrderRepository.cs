using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.Enums;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface ISalesOrderRepository : IRepositoryBase<SalesOrder, Guid>
    {
        Task<(IList<SalesOrder> data, int total, int totalDisplay)> GetPagedSalesOrdersAsync(int pageIndex,
            int pageSize, DataTablesSearch search, string? order);
        Task<SalesOrder?> GetSalesOrderByIdAsync(Guid id);
        Task<IList<SalesOrder>> GetSalesOrdersByStatusAsync(params SalesOrderStatus[] statuses);
        Task<IList<SalesOrder>> GetSalesOrdersByQuotationAsync(Guid salesQuotationId);
        Task<IList<SalesOrder>> GetSalesOrdersByCustomerAsync(Guid customerId);

        /// <summary>
        /// Orders raised within a period, with their lines. The reporting side reads
        /// the sales book through this.
        /// </summary>
        Task<IList<SalesOrder>> GetSalesOrdersByDateRangeAsync(DateTime fromDate, DateTime toDate);
        Task<bool> IsSalesOrderNoDuplicateAsync(string salesOrderNo);
    }
}
