using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Infrastructure;
using DevSkill.Inventory.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class DiscountController : Controller
    {
        private readonly IDiscountRuleManagementService _discountRuleManagementService;
        private readonly IProductManagementService _productManagementService;
        private readonly ICustomerManagementService _customerManagementService;
        private readonly ILogger<DiscountController> _logger;

        public DiscountController(IDiscountRuleManagementService discountRuleManagementService,
            IProductManagementService productManagementService,
            ICustomerManagementService customerManagementService,
            ILogger<DiscountController> logger)
        {
            _discountRuleManagementService = discountRuleManagementService;
            _productManagementService = productManagementService;
            _customerManagementService = customerManagementService;
            _logger = logger;
        }

        [Authorize(Policy = "ReadPermission")]
        public IActionResult DiscountRuleList()
        {
            return View();
        }

        [HttpPost, Authorize(Policy = "ReadPermission")]
        public async Task<JsonResult> GetDiscountRuleJsonData([FromBody] DiscountRuleListModel model)
        {
            var result = await _discountRuleManagementService.GetDiscountRulesAsync(model.PageIndex, model.PageSize, model.Search,
                model.FormatSortExpression(
                    "RuleName",
                    "Scope",
                    "DiscountType",
                    "DiscountValue",
                    "Id",
                    "Priority",
                    "IsActive",
                    "Id"));

            var discountRuleJsonData = new
            {
                recordsTotal = result.total,
                recordsFiltered = result.totalDisplay,
                data = (from record in result.data
                        select new string[]
                        {
                            HttpUtility.HtmlEncode(record.RuleName),
                            HttpUtility.HtmlEncode(record.Scope.ToString()),
                            HttpUtility.HtmlEncode(record.DiscountType.ToString()),
                            HttpUtility.HtmlEncode(record.DiscountType == DiscountType.Percentage
                                ? $"{record.DiscountValue:N2} %"
                                : record.DiscountValue.ToString("N2")),
                            HttpUtility.HtmlEncode(record.Product?.ProductName
                                ?? record.Customer?.CustomerName
                                ?? record.CustomerType?.ToString()
                                ?? "All"),
                            HttpUtility.HtmlEncode(record.Priority.ToString()),
                            HttpUtility.HtmlEncode(record.IsActive ? "Active" : "Inactive"),
                            HttpUtility.HtmlEncode(record.Id.ToString())
                        }
                    ).ToArray()
            };

            return Json(discountRuleJsonData);
        }

        [Authorize(Policy = "CreatePermission")]
        public async Task<IActionResult> CreateDiscountRule()
        {
            var model = new DiscountRuleCreateModel();

            model.SetProductValues(await _productManagementService.GetAllProductAsync());
            model.SetCustomerValues(await _customerManagementService.GetActiveCustomerAsync());

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "CreatePermission")]
        public async Task<IActionResult> CreateDiscountRule(DiscountRuleCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                await RepopulateAsync(model);
                return View(model);
            }

            try
            {
                var discountRule = new DiscountRule
                {
                    Id = Guid.NewGuid(),
                    RuleName = model.RuleName,
                    Scope = model.Scope,
                    DiscountType = model.DiscountType,
                    DiscountValue = model.DiscountValue,
                    ProductId = model.ProductId == Guid.Empty ? null : model.ProductId,
                    CustomerId = model.CustomerId == Guid.Empty ? null : model.CustomerId,
                    CustomerType = model.CustomerType,
                    MinQuantity = model.MinQuantity,
                    MinOrderAmount = model.MinOrderAmount,
                    MaxDiscountAmount = model.MaxDiscountAmount,
                    EffectiveFrom = model.EffectiveFrom,
                    EffectiveTo = model.EffectiveTo,
                    IsActive = model.IsActive,
                    Priority = model.Priority
                };

                await _discountRuleManagementService.CreateDiscountRuleAsync(discountRule);

                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "Discount rule created successfully.",
                    Type = ResponseTypes.Success
                });

                return RedirectToAction(nameof(DiscountRuleList));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                await RepopulateAsync(model);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Discount rule creation failed.");
                ModelState.AddModelError(string.Empty, "Discount rule creation failed.");
                await RepopulateAsync(model);
                return View(model);
            }
        }

        [Authorize(Policy = "UpdatePermission")]
        public async Task<IActionResult> UpdateDiscountRule(Guid id)
        {
            var discountRule = await _discountRuleManagementService.GetDiscountRuleByIdAsync(id);

            if (discountRule == null)
            {
                return NotFound();
            }

            var model = new DiscountRuleUpdateModel
            {
                Id = discountRule.Id,
                RuleName = discountRule.RuleName,
                Scope = discountRule.Scope,
                DiscountType = discountRule.DiscountType,
                DiscountValue = discountRule.DiscountValue,
                ProductId = discountRule.ProductId,
                CustomerId = discountRule.CustomerId,
                CustomerType = discountRule.CustomerType,
                MinQuantity = discountRule.MinQuantity,
                MinOrderAmount = discountRule.MinOrderAmount,
                MaxDiscountAmount = discountRule.MaxDiscountAmount,
                EffectiveFrom = discountRule.EffectiveFrom,
                EffectiveTo = discountRule.EffectiveTo,
                IsActive = discountRule.IsActive,
                Priority = discountRule.Priority
            };

            await RepopulateAsync(model);

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<IActionResult> UpdateDiscountRule(DiscountRuleUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                await RepopulateAsync(model);
                return View(model);
            }

            try
            {
                var discountRule = new DiscountRule
                {
                    Id = model.Id,
                    RuleName = model.RuleName,
                    Scope = model.Scope,
                    DiscountType = model.DiscountType,
                    DiscountValue = model.DiscountValue,
                    ProductId = model.ProductId == Guid.Empty ? null : model.ProductId,
                    CustomerId = model.CustomerId == Guid.Empty ? null : model.CustomerId,
                    CustomerType = model.CustomerType,
                    MinQuantity = model.MinQuantity,
                    MinOrderAmount = model.MinOrderAmount,
                    MaxDiscountAmount = model.MaxDiscountAmount,
                    EffectiveFrom = model.EffectiveFrom,
                    EffectiveTo = model.EffectiveTo,
                    IsActive = model.IsActive,
                    Priority = model.Priority
                };

                await _discountRuleManagementService.UpdateDiscountRuleAsync(discountRule);

                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "Discount rule updated successfully.",
                    Type = ResponseTypes.Success
                });

                return RedirectToAction(nameof(DiscountRuleList));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                await RepopulateAsync(model);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Discount rule update failed.");
                ModelState.AddModelError(string.Empty, "Discount rule update failed.");
                await RepopulateAsync(model);
                return View(model);
            }
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "DeletePermission")]
        public async Task<JsonResult> DeleteDiscountRule(Guid id)
        {
            try
            {
                await _discountRuleManagementService.DeleteDiscountRuleAsync(id);

                return Json(new
                {
                    success = true,
                    message = "Discount rule deleted successfully."
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Discount rule delete failed.");

                return Json(new
                {
                    success = false,
                    message = "Discount rule delete failed."
                });
            }
        }

        private async Task RepopulateAsync(DiscountRuleCreateModel model)
        {
            model.SetProductValues(await _productManagementService.GetAllProductAsync());
            model.SetCustomerValues(await _customerManagementService.GetActiveCustomerAsync());
        }

        private async Task RepopulateAsync(DiscountRuleUpdateModel model)
        {
            model.SetProductValues(await _productManagementService.GetAllProductAsync());
            model.SetCustomerValues(await _customerManagementService.GetActiveCustomerAsync());
        }
    }
}
