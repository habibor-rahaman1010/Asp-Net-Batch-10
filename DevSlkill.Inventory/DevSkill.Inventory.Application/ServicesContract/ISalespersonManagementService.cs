using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.Enums;

namespace DevSkill.Inventory.Application.ServicesContract
{
    /// <summary>
    /// The people who sell, and the commission their sales earn. Commission is
    /// written by the invoice flow, so nothing here ever creates it by hand; this
    /// side only approves, pays and reports on what was earned.
    /// </summary>
    public interface ISalespersonManagementService
    {
        Task<Guid> CreateSalespersonAsync(Salesperson salesperson);
        Task<bool> UpdateSalespersonAsync(Salesperson salesperson);
        Task<bool> DeleteSalespersonAsync(Guid id);
        Task<Salesperson?> GetSalespersonByIdAsync(Guid id);
        Task<(IList<Salesperson> data, int total, int totalDisplay)> GetSalespersonsAsync(int pageIndex,
            int pageSize, DataTablesSearch search, string? order);
        Task<IList<Salesperson>> GetActiveSalespersonsAsync();
        Task<string> GenerateSalespersonCodeAsync();

        Task<(IList<SalesCommission> data, int total, int totalDisplay)> GetSalesCommissionsAsync(int pageIndex,
            int pageSize, DataTablesSearch search, string? order);
        Task<SalesCommission?> GetSalesCommissionByIdAsync(Guid id);
        Task<IList<SalesCommission>> GetSalesCommissionsByStatusAsync(params CommissionStatus[] statuses);

        /// <summary>Confirms the commission is really owed, which is what allows paying it.</summary>
        Task ApproveCommissionAsync(Guid id);

        /// <summary>Settles an approved commission.</summary>
        Task PayCommissionAsync(Guid id);

        /// <summary>Writes off a commission that will not be paid, leaving the invoice alone.</summary>
        Task CancelCommissionAsync(Guid id, string reason);

        /// <summary>
        /// What one salesperson billed over a period and what it earned them, read
        /// straight off the posted invoices and the commission lines.
        /// </summary>
        Task<CommissionSummaryDto> GetCommissionSummaryAsync(Guid salespersonId, DateTime fromDate, DateTime toDate);

        /// <summary>The same summary for every active salesperson, so they can be ranked.</summary>
        Task<IList<CommissionSummaryDto>> GetCommissionSummariesAsync(DateTime fromDate, DateTime toDate);
    }
}
