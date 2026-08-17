using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.Enums;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface ISalesCommissionRepository : IRepositoryBase<SalesCommission, Guid>
    {
        Task<(IList<SalesCommission> data, int total, int totalDisplay)> GetPagedSalesCommissionsAsync(int pageIndex,
            int pageSize, DataTablesSearch search, string? order);
        Task<SalesCommission?> GetSalesCommissionByIdAsync(Guid id);
        Task<IList<SalesCommission>> GetSalesCommissionsBySalespersonAsync(Guid salespersonId);

        /// <summary>
        /// The commission an invoice earned, so posting it twice or cancelling it can
        /// both be reasoned about from one place.
        /// </summary>
        Task<SalesCommission?> GetSalesCommissionBySalesInvoiceAsync(Guid salesInvoiceId);

        Task<IList<SalesCommission>> GetSalesCommissionsByStatusAsync(params CommissionStatus[] statuses);
        Task<IList<SalesCommission>> GetSalesCommissionsByDateRangeAsync(DateTime fromDate, DateTime toDate);
    }
}
