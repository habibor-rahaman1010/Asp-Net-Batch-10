using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// The reading side of the purchase module. Nothing here writes, so every action
    /// needs read permission only.
    /// </summary>
    [Area("Admin"), Authorize]
    public class PurchaseReportController : Controller
    {
        private readonly IPurchaseReportService _purchaseReportService;
        private readonly ISupplierLedgerService _supplierLedgerService;
        private readonly ISupplierManagementService _supplierManagementService;
        private readonly IProductManagementService _productManagementService;
        private readonly IBusinessLocationManagementService _businessLocationManagementService;
        private readonly ILogger<PurchaseReportController> _logger;

        public PurchaseReportController(
            IPurchaseReportService purchaseReportService,
            ISupplierLedgerService supplierLedgerService,
            ISupplierManagementService supplierManagementService,
            IProductManagementService productManagementService,
            IBusinessLocationManagementService businessLocationManagementService,
            ILogger<PurchaseReportController> logger)
        {
            _purchaseReportService = purchaseReportService;
            _supplierLedgerService = supplierLedgerService;
            _supplierManagementService = supplierManagementService;
            _productManagementService = productManagementService;
            _businessLocationManagementService = businessLocationManagementService;
            _logger = logger;
        }

        [HttpGet, Authorize(Policy = "ReadPermission")]
        public async Task<IActionResult> PurchaseReports(PurchaseReportModel model, bool run = false)
        {
            await PopulateAsync(model);

            // The page opens on an empty filter rather than running a year of data
            // nobody asked for.
            if (run)
            {
                try
                {
                    model.Report = await _purchaseReportService.GetPurchaseReportAsync(model.ToFilter());
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Purchase report failed.");
                    ModelState.AddModelError(string.Empty, "The report could not be produced.");
                }
            }

            return View(model);
        }

        [HttpGet, Authorize(Policy = "ReadPermission")]
        public async Task<IActionResult> SupplierLedger(SupplierLedgerModel model)
        {
            model.SetSupplierValues(await _supplierManagementService.GetAllSupplierAsync());

            if (model.SupplierId.HasValue && model.SupplierId.Value != Guid.Empty)
            {
                try
                {
                    model.Ledger = await _supplierLedgerService.GetSupplierLedgerAsync(
                        model.SupplierId.Value, model.FromDate, model.ToDate);
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Supplier ledger failed.");
                    ModelState.AddModelError(string.Empty, "The supplier ledger could not be produced.");
                }
            }

            return View(model);
        }

        private async Task PopulateAsync(PurchaseReportModel model)
        {
            model.SetLookupValues(
                await _supplierManagementService.GetAllSupplierAsync(),
                await _productManagementService.GetAllProductAsync(),
                await _businessLocationManagementService.GetAllBusinessLocationAsync());
        }
    }
}
