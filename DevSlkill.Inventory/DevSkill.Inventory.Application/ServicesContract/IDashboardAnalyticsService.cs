using DevSkill.Inventory.Domain.Dtos;

namespace DevSkill.Inventory.Application.ServicesContract
{
    public interface IDashboardAnalyticsService
    {
        /// <summary>
        /// How the goods held in house are standing right now: how many products are
        /// comfortable, how many are running down and how many have nothing left.
        /// </summary>
        Task<StockHealthDto> GetStockHealthAsync();

        /// <summary>
        /// What the year filter lists, and which years it opens on. The list always
        /// reaches back a few years so a range like last year to this year can be picked
        /// even before there are invoices that old.
        /// </summary>
        Task<SalesVsPurchaseYearOptionsDto> GetYearOptionsAsync();

        /// <summary>
        /// Sales against purchases over whole calendar years, both ends inclusive. A
        /// short range is drawn month by month; a long one steps up to whole years so
        /// the axis stays readable.
        /// </summary>
        Task<SalesVsPurchaseDto> GetSalesVsPurchaseAsync(int fromYear, int toYear);
    }
}
