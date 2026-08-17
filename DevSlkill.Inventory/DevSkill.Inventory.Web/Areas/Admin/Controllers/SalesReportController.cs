using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// The reading side of the sales module. Nothing here writes, so every action
    /// needs read permission only.
    /// </summary>
    [Area("Admin"), Authorize]
    public class SalesReportController : Controller
    {
        private readonly ISalesReportService _salesReportService;
        private readonly ICustomerLedgerService _customerLedgerService;
        private readonly ICustomerManagementService _customerManagementService;
        private readonly IProductManagementService _productManagementService;
        private readonly IBusinessLocationManagementService _businessLocationManagementService;
        private readonly ISalespersonManagementService _salespersonManagementService;
        private readonly ILogger<SalesReportController> _logger;

        public SalesReportController(
            ISalesReportService salesReportService,
            ICustomerLedgerService customerLedgerService,
            ICustomerManagementService customerManagementService,
            IProductManagementService productManagementService,
            IBusinessLocationManagementService businessLocationManagementService,
            ISalespersonManagementService salespersonManagementService,
            ILogger<SalesReportController> logger)
        {
            _salesReportService = salesReportService;
            _customerLedgerService = customerLedgerService;
            _customerManagementService = customerManagementService;
            _productManagementService = productManagementService;
            _businessLocationManagementService = businessLocationManagementService;
            _salespersonManagementService = salespersonManagementService;
            _logger = logger;
        }

        [HttpGet, Authorize(Policy = "ReadPermission")]
        public async Task<IActionResult> SalesReports(SalesReportModel model, bool run = false)
        {
            await PopulateAsync(model);

            // The page opens on an empty filter rather than running a year of data
            // nobody asked for.
            if (run)
            {
                try
                {
                    model.Report = await _salesReportService.GetSalesReportAsync(model.ToFilter());
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Sales report failed.");
                    ModelState.AddModelError(string.Empty, "The report could not be produced.");
                }
            }

            return View(model);
        }

        [HttpGet, Authorize(Policy = "ReadPermission")]
        public async Task<IActionResult> CustomerLedger(CustomerLedgerModel model)
        {
            model.SetCustomerValues(await _customerManagementService.GetAllCustomerAsync());

            if (model.CustomerId.HasValue && model.CustomerId.Value != Guid.Empty)
            {
                try
                {
                    model.Ledger = await _customerLedgerService.GetCustomerLedgerAsync(
                        model.CustomerId.Value, model.FromDate, model.ToDate);
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Customer ledger failed.");
                    ModelState.AddModelError(string.Empty, "The customer ledger could not be produced.");
                }
            }

            return View(model);
        }

        private async Task PopulateAsync(SalesReportModel model)
        {
            model.SetLookupValues(
                await _customerManagementService.GetAllCustomerAsync(),
                await _productManagementService.GetAllProductAsync(),
                await _businessLocationManagementService.GetAllBusinessLocationAsync(),
                await _salespersonManagementService.GetActiveSalespersonsAsync());
        }
    }
}
