using Autofac.Extras.Moq;
using DevSkill.Inventory.Application.Services;
using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Domain.RepositoryContracts;
using DevSkill.Inventory.Domain.UnitOfWorkContracts;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace DevSkill.Inventory.Application.Tests
{
    [ExcludeFromCodeCoverage]
    public class DashboardAnalyticsServiceTests
    {
        private AutoMock _moq;
        private IDashboardAnalyticsService _dashboardAnalyticsService;
        private Mock<ISalesInvoiceRepository> _salesInvoiceRepositoryMock;
        private Mock<IPurchaseInvoiceRepository> _purchaseInvoiceRepositoryMock;
        private Mock<IInventoryUnitOfWork> _unitOfWorkMock;

        /// <summary>The span the filter offers at minimum, mirroring the service.</summary>
        private const int MinimumYearsOffered = 5;

        private static int RunningYear => DateTime.Today.Year;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _moq = AutoMock.GetLoose();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _moq?.Dispose();
        }

        [SetUp]
        public void Setup()
        {
            _dashboardAnalyticsService = _moq.Create<DashboardAnalyticsService>();
            _salesInvoiceRepositoryMock = _moq.Mock<ISalesInvoiceRepository>();
            _purchaseInvoiceRepositoryMock = _moq.Mock<IPurchaseInvoiceRepository>();
            _unitOfWorkMock = _moq.Mock<IInventoryUnitOfWork>();

            _unitOfWorkMock.Setup(x => x.SalesInvoiceRepository)
                .Returns(_salesInvoiceRepositoryMock.Object);
            _unitOfWorkMock.Setup(x => x.PurchaseInvoiceRepository)
                .Returns(_purchaseInvoiceRepositoryMock.Object);
        }

        [TearDown]
        public void Teardown()
        {
            _salesInvoiceRepositoryMock?.Reset();
            _purchaseInvoiceRepositoryMock?.Reset();
            _unitOfWorkMock?.Reset();
        }

        /// <summary>Points both sides at the given oldest invoice date, or at nothing.</summary>
        private void GivenOldestInvoices(DateTime? sales, DateTime? purchases)
        {
            _salesInvoiceRepositoryMock
                .Setup(x => x.GetEarliestInvoiceDateAsync(It.IsAny<SalesInvoiceStatus[]>()))
                .ReturnsAsync(sales);

            _purchaseInvoiceRepositoryMock
                .Setup(x => x.GetEarliestInvoiceDateAsync(It.IsAny<PurchaseInvoiceStatus[]>()))
                .ReturnsAsync(purchases);
        }

        private void GivenMonthlyTotals(IList<MonthlyAmountDto> sales, IList<MonthlyAmountDto> purchases)
        {
            _salesInvoiceRepositoryMock
                .Setup(x => x.GetMonthlyInvoicedTotalsAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>(),
                    It.IsAny<SalesInvoiceStatus[]>()))
                .ReturnsAsync(sales);

            _purchaseInvoiceRepositoryMock
                .Setup(x => x.GetMonthlyInvoicedTotalsAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>(),
                    It.IsAny<PurchaseInvoiceStatus[]>()))
                .ReturnsAsync(purchases);
        }

        private static MonthlyAmountDto Month(int year, int month, decimal total) =>
            new() { Year = year, Month = month, Total = total };

        // ---- The year filter ----------------------------------------------------

        [Test]
        public async Task GetYearOptionsAsync_ShouldOfferEarlierYears_WhenEveryInvoiceIsFromTheRunningYear()
        {
            // Arrange: the whole book sits in the running year, which is the state that
            // used to leave the filter with one year in it and nothing to compare.
            GivenOldestInvoices(new DateTime(RunningYear, 3, 14), new DateTime(RunningYear, 5, 2));

            // Act
            var options = await _dashboardAnalyticsService.GetYearOptionsAsync();

            // Assert
            Assert.That(options.FirstOfferedYear, Is.EqualTo(RunningYear - MinimumYearsOffered + 1));
            Assert.That(options.LastOfferedYear, Is.EqualTo(RunningYear));
            Assert.That(options.OfferedYears, Has.Count.EqualTo(MinimumYearsOffered));

            // The previous year has to be pickable, which is the range that was asked for.
            Assert.That(options.OfferedYears, Contains.Item(RunningYear - 1));
        }

        [Test]
        public async Task GetYearOptionsAsync_ShouldListNewestFirst()
        {
            // Arrange
            GivenOldestInvoices(new DateTime(RunningYear, 1, 1), null);

            // Act
            var options = await _dashboardAnalyticsService.GetYearOptionsAsync();

            // Assert
            Assert.That(options.OfferedYears.First(), Is.EqualTo(RunningYear));
            Assert.That(options.OfferedYears, Is.Ordered.Descending);
        }

        [Test]
        public async Task GetYearOptionsAsync_ShouldReachBackToTheOldestInvoice_WhenItPredatesTheFixedSpan()
        {
            // Arrange: a book older than the fixed span must not be cut off by it.
            GivenOldestInvoices(new DateTime(RunningYear - 8, 7, 1), new DateTime(RunningYear - 2, 1, 1));

            // Act
            var options = await _dashboardAnalyticsService.GetYearOptionsAsync();

            // Assert
            Assert.That(options.FirstOfferedYear, Is.EqualTo(RunningYear - 8));
            Assert.That(options.OfferedYears, Has.Count.EqualTo(9));
        }

        [Test]
        public async Task GetYearOptionsAsync_ShouldTakeTheOlderOfTheTwoSides()
        {
            // Arrange: purchases started earlier than sales here.
            GivenOldestInvoices(new DateTime(RunningYear - 1, 1, 1), new DateTime(RunningYear - 7, 1, 1));

            // Act
            var options = await _dashboardAnalyticsService.GetYearOptionsAsync();

            // Assert
            Assert.That(options.FirstOfferedYear, Is.EqualTo(RunningYear - 7));
        }

        [Test]
        public async Task GetYearOptionsAsync_ShouldStillOfferTheFixedSpan_WhenThereAreNoInvoicesAtAll()
        {
            // Arrange
            GivenOldestInvoices(null, null);

            // Act
            var options = await _dashboardAnalyticsService.GetYearOptionsAsync();

            // Assert
            Assert.That(options.FirstOfferedYear, Is.EqualTo(RunningYear - MinimumYearsOffered + 1));
            Assert.That(options.DefaultFromYear, Is.EqualTo(RunningYear));
            Assert.That(options.DefaultToYear, Is.EqualTo(RunningYear));
        }

        [Test]
        public async Task GetYearOptionsAsync_ShouldOpenOnTheYearsHoldingInvoices_NotOnTheWholeList()
        {
            // Arrange: the list reaches back five years, but only this one has invoices.
            GivenOldestInvoices(new DateTime(RunningYear, 2, 1), null);

            // Act
            var options = await _dashboardAnalyticsService.GetYearOptionsAsync();

            // Assert: opening on the full list would step the chart to a yearly view of
            // four empty years.
            Assert.That(options.DefaultFromYear, Is.EqualTo(RunningYear));
            Assert.That(options.DefaultToYear, Is.EqualTo(RunningYear));
            Assert.That(options.FirstOfferedYear, Is.LessThan(options.DefaultFromYear));
        }

        [Test]
        public async Task GetYearOptionsAsync_ShouldNotOfferAFutureYear_WhenAnInvoiceIsDatedAhead()
        {
            // Arrange
            GivenOldestInvoices(new DateTime(RunningYear + 3, 1, 1), null);

            // Act
            var options = await _dashboardAnalyticsService.GetYearOptionsAsync();

            // Assert
            Assert.That(options.LastOfferedYear, Is.EqualTo(RunningYear));
            Assert.That(options.OfferedYears, Has.No.Member(RunningYear + 1));
        }

        // ---- The chart itself ---------------------------------------------------

        [Test]
        public void GetSalesVsPurchaseAsync_ShouldThrow_WhenTheRangeIsReversed()
        {
            // Arrange
            GivenMonthlyTotals(new List<MonthlyAmountDto>(), new List<MonthlyAmountDto>());

            // Act + Assert: an empty chart would read as "no activity" rather than as the
            // mistake it is.
            Assert.That(async () => await _dashboardAnalyticsService
                    .GetSalesVsPurchaseAsync(RunningYear, RunningYear - 1),
                Throws.InstanceOf<InvalidOperationException>());
        }

        [Test]
        public void GetSalesVsPurchaseAsync_ShouldThrow_WhenTheRangeRunsIntoTheFuture()
        {
            // Arrange
            GivenMonthlyTotals(new List<MonthlyAmountDto>(), new List<MonthlyAmountDto>());

            // Act + Assert
            Assert.That(async () => await _dashboardAnalyticsService
                    .GetSalesVsPurchaseAsync(RunningYear, RunningYear + 1),
                Throws.InstanceOf<InvalidOperationException>());
        }

        [Test]
        public async Task GetSalesVsPurchaseAsync_ShouldDrawOneBarPerMonth_OverASingleFinishedYear()
        {
            // Arrange
            var year = RunningYear - 1;
            GivenMonthlyTotals(
                new List<MonthlyAmountDto> { Month(year, 1, 500), Month(year, 12, 700) },
                new List<MonthlyAmountDto> { Month(year, 1, 300) });

            // Act
            var report = await _dashboardAnalyticsService.GetSalesVsPurchaseAsync(year, year);

            // Assert
            Assert.That(report.Granularity, Is.EqualTo(SalesVsPurchaseGranularity.Month));
            Assert.That(report.Points, Has.Count.EqualTo(12));
            Assert.That(report.Points.First().SalesAmount, Is.EqualTo(500));
            Assert.That(report.Points.Last().SalesAmount, Is.EqualTo(700));
            Assert.That(report.TotalSales, Is.EqualTo(1200));
            Assert.That(report.TotalPurchase, Is.EqualTo(300));
            Assert.That(report.Gap, Is.EqualTo(900));
        }

        [Test]
        public async Task GetSalesVsPurchaseAsync_ShouldCarryAQuietMonthAsZero()
        {
            // Arrange: only one month of the year billed anything.
            var year = RunningYear - 1;
            GivenMonthlyTotals(
                new List<MonthlyAmountDto> { Month(year, 6, 900) },
                new List<MonthlyAmountDto>());

            // Act
            var report = await _dashboardAnalyticsService.GetSalesVsPurchaseAsync(year, year);

            // Assert: dropping the quiet months would skew the spacing and hide them.
            Assert.That(report.Points, Has.Count.EqualTo(12));
            Assert.That(report.Points[5].SalesAmount, Is.EqualTo(900));
            Assert.That(report.Points.Where((_, i) => i != 5).Select(x => x.SalesAmount),
                Is.All.EqualTo(0));
        }

        [Test]
        public async Task GetSalesVsPurchaseAsync_ShouldStepUpToYears_WhenTheRangeIsWide()
        {
            // Arrange: four years is past the point where monthly bars stay readable.
            var from = RunningYear - 3;
            GivenMonthlyTotals(
                new List<MonthlyAmountDto> { Month(from, 4, 100), Month(from, 9, 150), Month(RunningYear, 1, 40) },
                new List<MonthlyAmountDto> { Month(from + 1, 2, 90) });

            // Act
            var report = await _dashboardAnalyticsService.GetSalesVsPurchaseAsync(from, RunningYear);

            // Assert
            Assert.That(report.Granularity, Is.EqualTo(SalesVsPurchaseGranularity.Year));
            Assert.That(report.Points, Has.Count.EqualTo(4));
            Assert.That(report.Points.Select(x => x.Label),
                Is.EqualTo(new[] { $"{from}", $"{from + 1}", $"{from + 2}", $"{RunningYear}" }));

            // The two months of the first year are added into its one bar.
            Assert.That(report.Points.First().SalesAmount, Is.EqualTo(250));
            Assert.That(report.Points[1].PurchaseAmount, Is.EqualTo(90));
            Assert.That(report.TotalSales, Is.EqualTo(290));
        }

        [Test]
        public async Task GetSalesVsPurchaseAsync_ShouldStayMonthly_AtTheWidestMonthlyRange()
        {
            // Arrange: three years is the widest range still drawn month by month.
            var from = RunningYear - 2;
            GivenMonthlyTotals(new List<MonthlyAmountDto>(), new List<MonthlyAmountDto>());

            // Act
            var report = await _dashboardAnalyticsService.GetSalesVsPurchaseAsync(from, RunningYear);

            // Assert
            Assert.That(report.Granularity, Is.EqualTo(SalesVsPurchaseGranularity.Month));

            // Two finished years plus the months of the running one that have happened.
            Assert.That(report.Points, Has.Count.EqualTo(24 + DateTime.Today.Month));
        }

        [Test]
        public async Task GetSalesVsPurchaseAsync_ShouldStopAtTheRunningMonth_RatherThanTrailIntoEmptyOnes()
        {
            // Arrange
            GivenMonthlyTotals(new List<MonthlyAmountDto>(), new List<MonthlyAmountDto>());

            // Act
            var report = await _dashboardAnalyticsService
                .GetSalesVsPurchaseAsync(RunningYear, RunningYear);

            // Assert: months that have not happened yet would read as a collapse.
            Assert.That(report.Points, Has.Count.EqualTo(DateTime.Today.Month));
            Assert.That(report.EndsOnRunningYear, Is.True);
        }

        [Test]
        public async Task GetSalesVsPurchaseAsync_ShouldNotFlagAFinishedRangeAsRunning()
        {
            // Arrange
            GivenMonthlyTotals(new List<MonthlyAmountDto>(), new List<MonthlyAmountDto>());

            // Act
            var report = await _dashboardAnalyticsService
                .GetSalesVsPurchaseAsync(RunningYear - 1, RunningYear - 1);

            // Assert
            Assert.That(report.EndsOnRunningYear, Is.False);
        }

        [Test]
        public async Task GetSalesVsPurchaseAsync_ShouldCountOnlyClaimedInvoices()
        {
            // Arrange
            var year = RunningYear - 1;
            GivenMonthlyTotals(new List<MonthlyAmountDto>(), new List<MonthlyAmountDto>());

            // Act
            await _dashboardAnalyticsService.GetSalesVsPurchaseAsync(year, year);

            // Assert: a draft claims nothing and a cancelled invoice claims nothing either,
            // so neither may reach the chart.
            _salesInvoiceRepositoryMock.Verify(x => x.GetMonthlyInvoicedTotalsAsync(
                It.IsAny<DateTime>(), It.IsAny<DateTime>(),
                It.Is<SalesInvoiceStatus[]>(s => !s.Contains(SalesInvoiceStatus.Draft)
                                              && !s.Contains(SalesInvoiceStatus.Cancelled)
                                              && s.Contains(SalesInvoiceStatus.Posted))), Times.Once);

            _purchaseInvoiceRepositoryMock.Verify(x => x.GetMonthlyInvoicedTotalsAsync(
                It.IsAny<DateTime>(), It.IsAny<DateTime>(),
                It.Is<PurchaseInvoiceStatus[]>(s => !s.Contains(PurchaseInvoiceStatus.Draft)
                                                 && !s.Contains(PurchaseInvoiceStatus.Cancelled)
                                                 && s.Contains(PurchaseInvoiceStatus.Posted))), Times.Once);
        }
    }
}
