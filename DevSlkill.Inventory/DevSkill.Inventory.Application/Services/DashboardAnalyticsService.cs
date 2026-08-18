using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Domain.UnitOfWorkContracts;
using System.Globalization;

namespace DevSkill.Inventory.Application.Services
{
    /// <summary>
    /// Feeds the dashboard charts. Nothing here writes, and both figures are read off
    /// the same documents the reports use, so a chart and a report over the same
    /// period have to agree.
    /// </summary>
    public class DashboardAnalyticsService : IDashboardAnalyticsService
    {
        private readonly IInventoryUnitOfWork _dashboardUnitOfWork;

        /// <summary>
        /// The earliest year the filter will accept. Anything older is a mistyped year
        /// rather than a range somebody meant.
        /// </summary>
        private const int MinimumYear = 2000;

        /// <summary>
        /// Up to this many years are still drawn month by month. Past it the chart steps
        /// up to whole years, because forty-odd bars stop being readable.
        /// </summary>
        private const int MaximumMonthlyYears = 3;

        /// <summary>
        /// How many years the filter lists at the very least, the running year included.
        /// Listing only the years that already hold invoices leaves a fresh database with
        /// a single option and nothing to compare it against.
        /// </summary>
        private const int MinimumYearsOffered = 5;

        /// <summary>Only an invoice that was really claimed counts as sales.</summary>
        private static readonly SalesInvoiceStatus[] CountableSalesStatuses =
        {
            SalesInvoiceStatus.Posted,
            SalesInvoiceStatus.PartiallyPaid,
            SalesInvoiceStatus.Paid
        };

        /// <summary>The same rule on the buying side: a draft owes the supplier nothing.</summary>
        private static readonly PurchaseInvoiceStatus[] CountablePurchaseStatuses =
        {
            PurchaseInvoiceStatus.Posted,
            PurchaseInvoiceStatus.PartiallyPaid,
            PurchaseInvoiceStatus.Paid
        };

        public DashboardAnalyticsService(IInventoryUnitOfWork unitOfWork)
        {
            _dashboardUnitOfWork = unitOfWork;
        }

        public async Task<StockHealthDto> GetStockHealthAsync()
        {
            try
            {
                var products = _dashboardUnitOfWork.ProductRepository;

                // The three buckets are written so no product can land in two of them:
                // empty, at or under the alert line, or above it.
                var outOfStock = await products.GetCountAsync(x => x.CurrentStock <= 0);
                var lowStock = await products.GetCountAsync(x => x.CurrentStock > 0
                                                             && x.CurrentStock <= x.AlertQuantity);
                var healthy = await products.GetCountAsync(x => x.CurrentStock > 0
                                                            && x.CurrentStock > x.AlertQuantity);

                var (unitsOnHand, unitsReserved) = await products.GetStockUnitTotalsAsync();

                return new StockHealthDto
                {
                    HealthyProducts = healthy,
                    LowStockProducts = lowStock,
                    OutOfStockProducts = outOfStock,
                    UnitsOnHand = unitsOnHand,
                    UnitsReserved = unitsReserved
                };
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<SalesVsPurchaseYearOptionsDto> GetYearOptionsAsync()
        {
            try
            {
                var runningYear = DateTime.Today.Year;

                var earliestSales = await _dashboardUnitOfWork.SalesInvoiceRepository
                    .GetEarliestInvoiceDateAsync(CountableSalesStatuses);

                var earliestPurchase = await _dashboardUnitOfWork.PurchaseInvoiceRepository
                    .GetEarliestInvoiceDateAsync(CountablePurchaseStatuses);

                // Whichever side reaches back further sets the start, so neither module
                // has its history cut off by the other having started later.
                var years = new[] { earliestSales, earliestPurchase }
                    .Where(x => x.HasValue)
                    .Select(x => x!.Value.Year)
                    .ToList();

                // A back-dated typo or a future-dated invoice must not push the filter
                // outside the years it can actually draw.
                var earliestWithData = years.Count > 0
                    ? Math.Clamp(years.Min(), MinimumYear, runningYear)
                    : runningYear;

                // The list runs back the fixed span, or further still when the invoices do.
                // Otherwise a database whose invoices are all from this year would offer
                // this year alone and the filter would be pointless.
                var firstOffered = Math.Max(MinimumYear,
                    Math.Min(earliestWithData, runningYear - MinimumYearsOffered + 1));

                return new SalesVsPurchaseYearOptionsDto
                {
                    FirstOfferedYear = firstOffered,
                    LastOfferedYear = runningYear,

                    // Opening on the years that hold invoices keeps the first paint on real
                    // bars, and keeps a narrow history month by month rather than stepping
                    // straight to a yearly view of mostly empty years.
                    DefaultFromYear = earliestWithData,
                    DefaultToYear = runningYear
                };
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<SalesVsPurchaseDto> GetSalesVsPurchaseAsync(int fromYear, int toYear)
        {
            var runningYear = DateTime.Today.Year;

            // A range read back to front would silently return nothing, which reads as
            // "no activity" rather than as the mistake it is.
            if (toYear < fromYear)
            {
                throw new InvalidOperationException("The end year cannot be earlier than the start year.");
            }

            if (fromYear < MinimumYear || toYear > runningYear)
            {
                throw new InvalidOperationException(
                    $"The year range has to fall between {MinimumYear} and {runningYear}.");
            }

            try
            {
                var firstMonth = new DateTime(fromYear, 1, 1);

                // Stopping at the running month keeps the chart from trailing off into
                // months that have not happened yet and reading as a collapse.
                var lastMonth = new DateTime(toYear, 12, 1);
                var runningMonth = new DateTime(runningYear, DateTime.Today.Month, 1);

                if (lastMonth > runningMonth)
                {
                    lastMonth = runningMonth;
                }

                // The end of the window is inclusive, so a document timed during its last
                // month counts.
                var to = lastMonth.AddMonths(1).AddTicks(-1);

                var sales = await _dashboardUnitOfWork.SalesInvoiceRepository
                    .GetMonthlyInvoicedTotalsAsync(firstMonth, to, CountableSalesStatuses);

                var purchases = await _dashboardUnitOfWork.PurchaseInvoiceRepository
                    .GetMonthlyInvoicedTotalsAsync(firstMonth, to, CountablePurchaseStatuses);

                var report = new SalesVsPurchaseDto
                {
                    FromYear = fromYear,
                    ToYear = toYear,
                    Granularity = toYear - fromYear + 1 > MaximumMonthlyYears
                        ? SalesVsPurchaseGranularity.Year
                        : SalesVsPurchaseGranularity.Month
                };

                if (report.Granularity == SalesVsPurchaseGranularity.Month)
                {
                    AddMonthlyPoints(report, sales, purchases, firstMonth, lastMonth);
                }
                else
                {
                    AddYearlyPoints(report, sales, purchases, fromYear, toYear);
                }

                return report;
            }
            catch (Exception ex) when (ex is not InvalidOperationException)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        /// <summary>
        /// Walks the months rather than the rows, which is what keeps a quiet month on
        /// the axis as a zero instead of dropping it and skewing the spacing.
        /// </summary>
        private static void AddMonthlyPoints(SalesVsPurchaseDto report, IList<MonthlyAmountDto> sales,
            IList<MonthlyAmountDto> purchases, DateTime firstMonth, DateTime lastMonth)
        {
            for (var month = firstMonth; month <= lastMonth; month = month.AddMonths(1))
            {
                AddPoint(report, month.ToString("MMM yy", CultureInfo.InvariantCulture),
                    MonthTotal(sales, month.Year, month.Month),
                    MonthTotal(purchases, month.Year, month.Month));
            }
        }

        /// <summary>
        /// The same walk a year at a time, for a range too wide to draw month by month.
        /// </summary>
        private static void AddYearlyPoints(SalesVsPurchaseDto report, IList<MonthlyAmountDto> sales,
            IList<MonthlyAmountDto> purchases, int fromYear, int toYear)
        {
            for (var year = fromYear; year <= toYear; year++)
            {
                AddPoint(report, year.ToString(CultureInfo.InvariantCulture),
                    sales.Where(x => x.Year == year).Sum(x => x.Total),
                    purchases.Where(x => x.Year == year).Sum(x => x.Total));
            }
        }

        private static void AddPoint(SalesVsPurchaseDto report, string label, decimal salesAmount,
            decimal purchaseAmount)
        {
            report.Points.Add(new SalesVsPurchasePointDto
            {
                Label = label,
                SalesAmount = salesAmount,
                PurchaseAmount = purchaseAmount
            });

            report.TotalSales += salesAmount;
            report.TotalPurchase += purchaseAmount;
        }

        private static decimal MonthTotal(IList<MonthlyAmountDto> totals, int year, int month)
        {
            return totals.FirstOrDefault(x => x.Year == year && x.Month == month)?.Total ?? 0;
        }
    }
}
