using AutoMapper;
using DevSkill.Inventory.Application.Services;
using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Entities.StockAdjustmentEntites;
using DevSkill.Inventory.Infrastructure;
using DevSkill.Inventory.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdjustmentTypeController : Controller
    {
        private readonly IAdjustmentTypeManagementService _adjustmentTypeManagementService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public AdjustmentTypeController(IAdjustmentTypeManagementService adjustmentTypeManagementService,
            IMapper mapper,
            ILogger<AdjustmentTypeController> logger)
        {
            _adjustmentTypeManagementService = adjustmentTypeManagementService;
            _mapper = mapper;
            _logger = logger;
        }

        public IActionResult AdjustmentTypeList()
        {
            return View();
        }

        public async Task<JsonResult> GetAdjustmentTypeJsonData([FromBody] AdjustmentTypeListModel model)
        {
            var result = await _adjustmentTypeManagementService.GetAllAdjustmentTypeAsync(model.PageIndex, model.PageSize, model.Search,
               model.FormatSortExpression("Id", "AdjustmentTypeName", "Description" ));

            var adjustmentTypeJsonData = new
            {
                recordsTotal = result.total,
                recordsFiltered = result.totalDisplay,
                data = (from record in result.data
                        select new string[]
                        {
                            HttpUtility.HtmlEncode(record.AdjustmentTypeName),
                            HttpUtility.HtmlEncode(record.Description),
                            HttpUtility.HtmlEncode(record.Id.ToString())
                        }
                    ).ToArray()
            };

            return Json(adjustmentTypeJsonData);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<JsonResult> AddAdjustmentType(AdjustmentTypeCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var adjustmentType = _mapper.Map<AdjustmentType>(model);
                adjustmentType.Id = Guid.NewGuid();

                try
                {
                    await _adjustmentTypeManagementService.AddAdjustmentTypeAsync(adjustmentType);

                    return Json(new
                    {
                        success = true,
                        message = "Adjustment Type created successfully"
                    });
                }

                catch (Exception ex)
                {
                    _logger.LogError(ex, "Adjustment Type creation failed");
                    return Json(new
                    {
                        success = false,
                        message = "Adjustment Type creation failed"
                    });
                }
            }
            return Json(new
            {
                success = false,
                message = "Adjustment Type creation failed"
            });
        }    

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<JsonResult> DeleteAdjustmentType(Guid id)
        {
            try
            {
                await _adjustmentTypeManagementService.DeleteAdjustmentTypeAsync(id);

                return Json(new
                {
                    success = true,
                    message = "The AdjustmentType has deleted successfuly"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "The AdjustmentType deleted failed");
                return Json(new
                {
                    success = false,
                    message = "The AdjustmentType deleted failed"
                });
            }
        }

        public IActionResult GetAdjustmentTypeById(Guid id)
        {
            var adjustmentType = _adjustmentTypeManagementService.GetAdjustmentTypeByIdAsync(id);
            if (adjustmentType != null)
            { 
                return Json(new { success = true, data = adjustmentType });
            }
            return Json(new { success = false, message = "Adjustment Type not found." });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAdjustmentType(AdjustmentTypeUpdateModel model)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation("Updating Warranty with data: {@Model}", model);
                var adjustmentType = await _adjustmentTypeManagementService.GetAdjustmentTypeByIdAsync(model.Id);
                adjustmentType = _mapper.Map(model, adjustmentType);
                adjustmentType.Id = model.Id;
                await _adjustmentTypeManagementService.UpdateAdjustmentTypeAsync(adjustmentType);

                return Json(new { success = true, message = "Adjustment Type updated successfully." });
            }
            _logger.LogInformation("Updating Adjustment Type with data: {@Model}", model);
            return Json(new { success = false, message = "Error updating Adjustment Type." });
        }
    }
}
