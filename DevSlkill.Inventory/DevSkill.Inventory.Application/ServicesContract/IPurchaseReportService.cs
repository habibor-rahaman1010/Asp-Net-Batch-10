using DevSkill.Inventory.Domain.Dtos;

namespace DevSkill.Inventory.Application.ServicesContract
{
    public interface IPurchaseReportService
    {
        /// <summary>
        /// Runs one of the purchase reports over a period. Every one of them reads
        /// the same documents, only grouped differently.
        /// </summary>
        Task<PurchaseReportDto> GetPurchaseReportAsync(PurchaseReportFilterDto filter);
    }
}
