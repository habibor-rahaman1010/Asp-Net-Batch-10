using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Infrastructure.InventoryIdentity;
using DevSkill.Inventory.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class DashboardController : Controller
    {
        private readonly IUserActivityManagementService _userActivityManagementService;
        private readonly IProductManagementService _productManagementService;
        private readonly ICategoryManagementService _categoryManagementService;
        private readonly ApplicationUserManager _applicationUserManager;
        private readonly ICustomerManagementService _customerManagementService;
        private readonly ISupplierManagementService _supplierManagementService;
        private readonly IBrandManagementService _brandManagementService;
        private readonly IBusinessLocationManagementService _businessLocationManagementService;
        private readonly IDashboardAnalyticsService _dashboardAnalyticsService;
        private readonly ILogger<DashboardController> _logger;

        public DashboardController(IUserActivityManagementService userActivityManagementService,
            ICustomerManagementService customerManagementService,
            ISupplierManagementService supplierManagementService,
            IBrandManagementService brandManagementService,
            IBusinessLocationManagementService businessLocationManagementService,
            IProductManagementService productManagementService,
            ICategoryManagementService categoryManagementService,
            IDashboardAnalyticsService dashboardAnalyticsService,
            ApplicationUserManager applicationUserManager,
            ILogger<DashboardController> logger)
        {
            _productManagementService = productManagementService;
            _customerManagementService = customerManagementService;
            _supplierManagementService = supplierManagementService;
            _brandManagementService = brandManagementService;
            _businessLocationManagementService = businessLocationManagementService;
            _categoryManagementService = categoryManagementService;
            _userActivityManagementService = userActivityManagementService;
            _dashboardAnalyticsService = dashboardAnalyticsService;
            _applicationUserManager = applicationUserManager;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var yearOptions = await _dashboardAnalyticsService.GetYearOptionsAsync();

            var model = new DashboardViewModel
            {
                TotalProducts = await _productManagementService.GetTotalProductCount(),
                TotalCategoris = await _categoryManagementService.GetTotalCategoryCount(),
                TotalUsers = await _applicationUserManager.Users.CountAsync(),
                TotalCustomers = await _customerManagementService.GetTotalCustomerCount(),
                TotalSuppliers = await _supplierManagementService.GetTotalSupplierCount(),
                BounceRate = await _userActivityManagementService.CalculateBounceRate(),
                EngagementRate = await _userActivityManagementService.CalculateEngagementRateAsync(),
                ActiveUsers = await _userActivityManagementService.GetActiveUserCountAsync(),
                UniqueVisitors = await _userActivityManagementService.GetUniqueVisitors(),
                TotalBrands = await _brandManagementService.GetTotalBrandCount(),
                TotalWarehouse = await _businessLocationManagementService.GetTotalWarehouseCount(),
                StockHealth = await _dashboardAnalyticsService.GetStockHealthAsync(),
                WarehouseStock = await _dashboardAnalyticsService.GetWarehouseStockAsync(),
                AvailableYears = yearOptions.OfferedYears
            };

            // The chart opens on the years that hold invoices rather than on the whole
            // list, most of which may well be empty.
            model.SalesVsPurchase = await _dashboardAnalyticsService
                .GetSalesVsPurchaseAsync(yearOptions.DefaultFromYear, yearOptions.DefaultToYear);

            model.ProfitCost = await _dashboardAnalyticsService
                .GetProfitAndCostAsync(yearOptions.DefaultToYear);

            // Opens on the running year, which is the one somebody looking at a
            // salesperson chart almost always wants.
            model.SalespersonSales = await _dashboardAnalyticsService
                .GetSalesBySalespersonAsync(yearOptions.DefaultToYear);

            model.TopSellingProducts = await _dashboardAnalyticsService
                .GetTopSellingProductsAsync(yearOptions.DefaultToYear);

            model.SupplierPurchase = await _dashboardAnalyticsService
                .GetPurchasesBySupplierAsync(yearOptions.DefaultToYear);

            return View(model);
        }

        /// <summary>
        /// Redraws the sales-against-purchase chart for a year range. Only this chart is
        /// re-read, so changing the filter does not recompute the whole dashboard.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> SalesVsPurchase(int fromYear, int toYear)
        {
            try
            {
                var report = await _dashboardAnalyticsService.GetSalesVsPurchaseAsync(fromYear, toYear);

                // The whole report goes back as it stands, so the browser reads exactly the
                // same shape on a filter change as it does on first load.
                return Json(new { success = true, chart = report });
            }
            catch (InvalidOperationException ex)
            {
                // A range the user got wrong is worth saying out loud, unlike a fault.
                return Json(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "The sales against purchase chart could not be produced.");

                return Json(new { success = false, message = "The chart could not be produced." });
            }
        }

        /// <summary>
        /// Redraws the profit and cost chart for one year, the same way the other
        /// filters work: only this chart is re-read.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> ProfitAndCost(int year)
        {
            try
            {
                var report = await _dashboardAnalyticsService.GetProfitAndCostAsync(year);

                // The whole report goes back as it stands, so the browser reads exactly the
                // same shape on a filter change as it does on first load.
                return Json(new { success = true, chart = report });
            }
            catch (InvalidOperationException ex)
            {
                // A year the user got wrong is worth saying out loud, unlike a fault.
                return Json(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "The profit and cost chart could not be produced.");

                return Json(new { success = false, message = "The chart could not be produced." });
            }
        }

        /// <summary>
        /// Redraws the who-sold-what chart for one year. Like the other filter, only
        /// this chart is re-read rather than the whole dashboard.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> SalesBySalesperson(int year)
        {
            try
            {
                var report = await _dashboardAnalyticsService.GetSalesBySalespersonAsync(year);

                // The whole report goes back as it stands, so the browser reads exactly the
                // same shape on a filter change as it does on first load.
                return Json(new { success = true, chart = report });
            }
            catch (InvalidOperationException ex)
            {
                // A year the user got wrong is worth saying out loud, unlike a fault.
                return Json(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "The sales by salesperson chart could not be produced.");

                return Json(new { success = false, message = "The chart could not be produced." });
            }
        }

        /// <summary>
        /// Redraws the best-sellers pie for one year, the same way the other filters
        /// work: only this chart is re-read.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> TopSellingProducts(int year)
        {
            try
            {
                var report = await _dashboardAnalyticsService.GetTopSellingProductsAsync(year);

                // The whole report goes back as it stands, so the browser reads exactly the
                // same shape on a filter change as it does on first load.
                return Json(new { success = true, chart = report });
            }
            catch (InvalidOperationException ex)
            {
                // A year the user got wrong is worth saying out loud, unlike a fault.
                return Json(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "The top selling products chart could not be produced.");

                return Json(new { success = false, message = "The chart could not be produced." });
            }
        }

        /// <summary>
        /// Redraws the where-we-buy chart for one year, the same way the other two
        /// filters work: only this chart is re-read.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> PurchasesBySupplier(int year)
        {
            try
            {
                var report = await _dashboardAnalyticsService.GetPurchasesBySupplierAsync(year);

                // The whole report goes back as it stands, so the browser reads exactly the
                // same shape on a filter change as it does on first load.
                return Json(new { success = true, chart = report });
            }
            catch (InvalidOperationException ex)
            {
                // A year the user got wrong is worth saying out loud, unlike a fault.
                return Json(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "The purchases by supplier chart could not be produced.");

                return Json(new { success = false, message = "The chart could not be produced." });
            }
        }
    }
}
