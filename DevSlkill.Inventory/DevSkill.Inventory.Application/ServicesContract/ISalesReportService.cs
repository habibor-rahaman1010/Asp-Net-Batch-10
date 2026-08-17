using DevSkill.Inventory.Domain.Dtos;

namespace DevSkill.Inventory.Application.ServicesContract
{
    public interface ISalesReportService
    {
        /// <summary>
        /// Runs one sales report. Everything is read off the same documents, so any
        /// two reports over the same period have to agree.
        /// </summary>
        Task<SalesReportDto> GetSalesReportAsync(SalesReportFilterDto filter);
    }
}
