using AutoMapper;
using DevSkill.Inventory.Application.Services;
using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class ApplicableTaxController : Controller
    {
        private readonly IApplicableTaxManagementService _applicableTaxManagementService;
        private readonly IMapper _mapper;
        private readonly ILogger<ApplicableTaxController> _logger;

        public ApplicableTaxController(IApplicableTaxManagementService applicableTaxManagementService,
            IMapper mapper,
            ILogger<ApplicableTaxController> logger)
        {
            _applicableTaxManagementService = applicableTaxManagementService;
            _mapper = mapper;
            _logger = logger;
        }

        [Authorize(Policy = "ReadPermission")]
        public IActionResult ApplicableTaxList()
        {
            return View();
        }

        //Get all applicable tax data... 
        [Authorize(Policy = "ReadPermission")]
        public async Task<IActionResult> GetApplicableTaxJsonData([FromBody] ApplicableTaxListModel model)
        {
            var result = await _applicableTaxManagementService.GetAllApplicableTaxAsync(model.PageIndex, model.PageSize, model.Search,
               model.FormatSortExpression("Id", "ApplicableTaxName", "Description", "TaxRate"));

            var productTypeJsonData = new
            {
                recordsTotal = result.total,
                recordsFiltered = result.totalDisplay,
                data = (from record in result.data
                        select new string[]
                        {
                            HttpUtility.HtmlEncode(record.ApplicableTaxName),
                            HttpUtility.HtmlEncode(record.Description),
                            HttpUtility.HtmlEncode(record.TaxRate),
                            HttpUtility.HtmlEncode(record.Id.ToString())
                        }
                    ).ToArray()
            };

            return Json(productTypeJsonData);
        }

        //This is method for create new applicable tax 
        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "CustomAdminAccess")]
        public async Task<IActionResult> AddApplicableTax(ApplicableTaxCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var applicableTax = _mapper.Map<ApplicableTax>(model);
                applicableTax.Id = Guid.NewGuid();

                try
                {
                    await _applicableTaxManagementService.AddApplicableTaxAsync(applicableTax);

                    return Json(new
                    {
                        success = true,
                        message = "Applicable Tax created successfully"
                    });
                }

                catch (Exception ex)
                {
                    _logger.LogError(ex, "Applicable Tax creation failed");
                    return Json(new
                    {
                        success = false,
                        message = "Applicable Tax creation failed"
                    });
                }
            }
            return Json(new
            {
                success = false,
                message = "Applicable Tax creation failed"
            });
        }

        //This code for applicable tax update...
        [Authorize(Policy = "CustomAdminAccess")]
        public async Task<IActionResult> GetApplicableTaxById(Guid id)
        {
            var applicableTax = await _applicableTaxManagementService.GetApplicableTaxByIdAsync(id);
            if (applicableTax != null)
            {
                return Json(new { success = true, data = applicableTax });
            }
            return Json(new { success = false, message = "Applicable tax not found." });
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "CustomAdminAccess")]
        public async Task<IActionResult> UpdateApplicableTax(ApplicableTaxUpdateModel model)
        {

            if (ModelState.IsValid)
            {
                var applicableTax = await _applicableTaxManagementService.GetApplicableTaxByIdAsync(model.Id);
                applicableTax = _mapper.Map(model, applicableTax);
                applicableTax.Id = model.Id;
                await _applicableTaxManagementService.UpdateApplicableTaxAsync(applicableTax);

                return Json(new { success = true, message = "Applicable tax updated successfully." });
            }
            return Json(new { success = false, message = "Error updating applicable tax." });
        }

        //This code for delete
        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "CustomAdminAccess")]
        public async Task<JsonResult> DeleteApplicableTax(Guid id)
        {
            try
            {
                await _applicableTaxManagementService.DeleteApplicableTaxAsync(id);

                return Json(new
                {
                    success = true,
                    message = "The applicable tax has deleted successfuly"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "The applicable tax deleted failed");
                return Json(new
                {
                    success = false,
                    message = "The applicable tax deleted failed"
                });
            }
        }
    }
}
