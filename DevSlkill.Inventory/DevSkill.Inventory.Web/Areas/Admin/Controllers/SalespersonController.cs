using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Infrastructure;
using DevSkill.Inventory.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// The people who sell, and the commission their sales earn. Commission lines are
    /// written by the invoice flow, so nothing here creates one by hand; this side
    /// only approves, pays and reports on what was earned.
    /// </summary>
    [Area("Admin"), Authorize]
    public class SalespersonController : Controller
    {
        private readonly ISalespersonManagementService _salespersonManagementService;
        private readonly ILogger<SalespersonController> _logger;

        public SalespersonController(ISalespersonManagementService salespersonManagementService,
            ILogger<SalespersonController> logger)
        {
            _salespersonManagementService = salespersonManagementService;
            _logger = logger;
        }

        [Authorize(Policy = "ReadPermission")]
        public IActionResult SalespersonList()
        {
            return View();
        }

        [HttpPost, Authorize(Policy = "ReadPermission")]
        public async Task<JsonResult> GetSalespersonJsonData([FromBody] SalespersonListModel model)
        {
            var result = await _salespersonManagementService.GetSalespersonsAsync(
                model.PageIndex, model.PageSize, model.Search,
                model.FormatSortExpression(
                    "SalespersonCode",
                    "SalespersonName",
                    "Phone",
                    "Email",
                    "CommissionRate",
                    "MonthlyTarget",
                    "Status",
                    "Id"));

            var jsonData = new
            {
                recordsTotal = result.total,
                recordsFiltered = result.totalDisplay,
                data = (from record in result.data
                        select new string[]
                        {
                            HttpUtility.HtmlEncode(record.SalespersonCode),
                            HttpUtility.HtmlEncode(record.SalespersonName),
                            HttpUtility.HtmlEncode(record.Phone),
                            HttpUtility.HtmlEncode(record.Email),
                            HttpUtility.HtmlEncode($"{record.CommissionRate:N2} ({record.CommissionBasis})"),
                            HttpUtility.HtmlEncode(record.MonthlyTarget.ToString("N2")),
                            HttpUtility.HtmlEncode(record.Status.ToString()),
                            HttpUtility.HtmlEncode(record.Id.ToString())
                        }
                    ).ToArray()
            };

            return Json(jsonData);
        }

        [Authorize(Policy = "CreatePermission")]
        public async Task<IActionResult> CreateSalesperson()
        {
            var model = new SalespersonCreateModel
            {
                SalespersonCode = await _salespersonManagementService.GenerateSalespersonCodeAsync()
            };

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "CreatePermission")]
        public async Task<IActionResult> CreateSalesperson(SalespersonCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var salesperson = new Salesperson
                {
                    SalespersonCode = model.SalespersonCode,
                    SalespersonName = model.SalespersonName,
                    Phone = model.Phone,
                    Email = model.Email,
                    CommissionBasis = model.CommissionBasis,
                    CommissionRate = model.CommissionRate,
                    MonthlyTarget = model.MonthlyTarget,
                    Status = model.Status
                };

                await _salespersonManagementService.CreateSalespersonAsync(salesperson);

                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "Salesperson created successfully.",
                    Type = ResponseTypes.Success
                });

                return RedirectToAction(nameof(SalespersonList));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Salesperson creation failed.");
                ModelState.AddModelError(string.Empty, "Salesperson creation failed.");
                return View(model);
            }
        }

        [Authorize(Policy = "UpdatePermission")]
        public async Task<IActionResult> UpdateSalesperson(Guid id)
        {
            var salesperson = await _salespersonManagementService.GetSalespersonByIdAsync(id);

            if (salesperson == null)
            {
                return NotFound();
            }

            var model = new SalespersonUpdateModel
            {
                Id = salesperson.Id,
                SalespersonCode = salesperson.SalespersonCode,
                SalespersonName = salesperson.SalespersonName,
                Phone = salesperson.Phone,
                Email = salesperson.Email,
                CommissionBasis = salesperson.CommissionBasis,
                CommissionRate = salesperson.CommissionRate,
                MonthlyTarget = salesperson.MonthlyTarget,
                Status = salesperson.Status
            };

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<IActionResult> UpdateSalesperson(SalespersonUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var salesperson = new Salesperson
                {
                    Id = model.Id,
                    SalespersonCode = model.SalespersonCode,
                    SalespersonName = model.SalespersonName,
                    Phone = model.Phone,
                    Email = model.Email,
                    CommissionBasis = model.CommissionBasis,
                    CommissionRate = model.CommissionRate,
                    MonthlyTarget = model.MonthlyTarget,
                    Status = model.Status
                };

                await _salespersonManagementService.UpdateSalespersonAsync(salesperson);

                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "Salesperson updated successfully.",
                    Type = ResponseTypes.Success
                });

                return RedirectToAction(nameof(SalespersonList));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Salesperson update failed.");
                ModelState.AddModelError(string.Empty, "Salesperson update failed.");
                return View(model);
            }
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "DeletePermission")]
        public async Task<JsonResult> DeleteSalesperson(Guid id)
        {
            return await RunAsync(
                () => _salespersonManagementService.DeleteSalespersonAsync(id),
                "Salesperson deleted successfully.",
                "Salesperson delete failed.");
        }

        [HttpGet, Authorize(Policy = "ReadPermission")]
        public async Task<IActionResult> GetSalespersonDetails(Guid id)
        {
            var salesperson = await _salespersonManagementService.GetSalespersonByIdAsync(id);

            if (salesperson == null)
            {
                return NotFound();
            }

            // The standing is shown over the running month, which is the period a
            // target is actually set against.
            var from = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            var summary = await _salespersonManagementService
                .GetCommissionSummaryAsync(salesperson.Id, from, DateTime.Today);

            return Json(new
            {
                id = salesperson.Id,
                salespersonCode = salesperson.SalespersonCode,
                salespersonName = salesperson.SalespersonName,
                phone = salesperson.Phone,
                email = salesperson.Email,
                commissionBasis = salesperson.CommissionBasis.ToString(),
                commissionRate = salesperson.CommissionRate,
                monthlyTarget = salesperson.MonthlyTarget,
                status = salesperson.Status.ToString(),
                summary.InvoiceCount,
                summary.SalesAmount,
                summary.TargetAmount,
                summary.PendingCommission,
                summary.ApprovedCommission,
                summary.PaidCommission,
                summary.TotalCommission
            });
        }

        [Authorize(Policy = "ReadPermission")]
        public IActionResult CommissionList()
        {
            return View();
        }

        [HttpPost, Authorize(Policy = "ReadPermission")]
        public async Task<JsonResult> GetCommissionJsonData([FromBody] SalesCommissionListModel model)
        {
            var result = await _salespersonManagementService.GetSalesCommissionsAsync(
                model.PageIndex, model.PageSize, model.Search,
                model.FormatSortExpression(
                    "EarnedDate",
                    "Salesperson.SalespersonName",
                    "SalesInvoice.InvoiceNo",
                    "BasisAmount",
                    "CommissionRate",
                    "CommissionAmount",
                    "Status",
                    "Id"));

            var jsonData = new
            {
                recordsTotal = result.total,
                recordsFiltered = result.totalDisplay,
                data = (from record in result.data
                        select new string[]
                        {
                            HttpUtility.HtmlEncode(record.EarnedDate.ToString("dd-MM-yyyy")),
                            HttpUtility.HtmlEncode(record.Salesperson?.SalespersonName),
                            HttpUtility.HtmlEncode(record.SalesInvoice?.InvoiceNo),
                            HttpUtility.HtmlEncode(record.BasisAmount.ToString("N2")),
                            HttpUtility.HtmlEncode(record.CommissionBasis == Domain.Enums.CommissionBasis.Percentage
                                ? $"{record.CommissionRate:N2} %"
                                : record.CommissionRate.ToString("N2")),
                            HttpUtility.HtmlEncode(record.CommissionAmount.ToString("N2")),
                            HttpUtility.HtmlEncode(record.Status.ToString()),
                            HttpUtility.HtmlEncode(record.Id.ToString())
                        }
                    ).ToArray()
            };

            return Json(jsonData);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<JsonResult> ApproveCommission(Guid id)
        {
            return await RunAsync(
                () => _salespersonManagementService.ApproveCommissionAsync(id),
                "Commission approved.",
                "Commission approval failed.");
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<JsonResult> PayCommission(Guid id)
        {
            return await RunAsync(
                () => _salespersonManagementService.PayCommissionAsync(id),
                "Commission paid.",
                "Commission payment failed.");
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<JsonResult> CancelCommission(Guid id, string reason)
        {
            return await RunAsync(
                () => _salespersonManagementService.CancelCommissionAsync(id, reason),
                "Commission cancelled.",
                "Commission cancel failed.");
        }

        private async Task<JsonResult> RunAsync(Func<Task> action, string successMessage, string failureMessage)
        {
            try
            {
                await action();

                return Json(new { success = true, message = successMessage });
            }
            catch (InvalidOperationException ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, failureMessage);
                return Json(new { success = false, message = failureMessage });
            }
        }
    }
}
