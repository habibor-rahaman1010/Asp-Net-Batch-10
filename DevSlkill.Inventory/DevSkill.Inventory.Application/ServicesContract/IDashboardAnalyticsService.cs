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
        /// Where those goods sit: how much of the catalogue each warehouse keeps, and
        /// how much of it was never placed in one at all.
        /// </summary>
        Task<WarehouseStockDto> GetWarehouseStockAsync();

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

        /// <summary>
        /// Revenue against the cost of the goods behind it, month by month over one
        /// calendar year. The goods are costed at their weighted average purchase
        /// price, so this is a gross profit and never a bottom line: no wage, rent or
        /// freight is held anywhere in the system to take off it.
        /// </summary>
        Task<ProfitCostDto> GetProfitAndCostAsync(int year);

        /// <summary>
        /// Who sold what over one calendar year, biggest seller first. Read off the
        /// same invoices the sales-against-purchase chart uses, so the year's bars add
        /// up to that chart's sales figure for the same year.
        /// </summary>
        Task<SalespersonSalesDto> GetSalesBySalespersonAsync(int year);

        /// <summary>
        /// What sold best over one calendar year, biggest earner first. Only the
        /// leading few products get a slice of their own; everything else is carried
        /// as one lumped slice, so a catalogue of hundreds still reads as a pie.
        /// </summary>
        Task<TopSellingProductDto> GetTopSellingProductsAsync(int year);

        /// <summary>
        /// Where the goods were bought over one calendar year, biggest supplier first.
        /// Read off the same invoices the sales-against-purchase chart uses, so the
        /// year's bars add up to that chart's purchase figure for the same year.
        /// </summary>
        Task<SupplierPurchaseDto> GetPurchasesBySupplierAsync(int year);
    }
}
