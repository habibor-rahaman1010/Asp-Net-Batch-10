using AutoMapper;
using DevSkill.Inventory.Application.Services;
using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Infrastructure;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Web;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UnitController : Controller
    {

        private readonly IUnitManagementService _unitManagementService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public UnitController(IUnitManagementService unitManagementService,
            IMapper mapper,
            ILogger<UnitController> logger)
        {
            _unitManagementService = unitManagementService;
            _mapper = mapper;
            _logger = logger;
        }


        public IActionResult UnitList()
        {
            var allowDecimalOptions = Enum.GetValues(typeof(AllowDecimal))
                                   .Cast<AllowDecimal>()
                                   .Select(e => new SelectListItem
                                   {
                                       Value = ((int)e).ToString(),
                                       Text = e.ToString()
                                   })
                                   .ToList();

            allowDecimalOptions.Insert(0, new SelectListItem
            {
                Value = "",
                Text = "Select"
            });

            ViewBag.AllowDecimalOptions = allowDecimalOptions;
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<JsonResult> AddUnit(UnitCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var unit = _mapper.Map<Unit>(model);
                unit.Id = Guid.NewGuid();

                try
                {
                    await _unitManagementService.AddUnitAsync(unit);

                    return Json(new
                    {
                        success = true,
                        message = "Unit created successfully"
                    });
                }

                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unit creation failed");
                    return Json(new
                    {
                        success = false,
                        message = "Unit creation failed"
                    });
                }
            }
            return Json(new
            {
                success = false,
                message = "Unit creation failed"
            });
        }

        public async Task<JsonResult> GetUnitJsonResponse([FromBody] UnitListModel model)
        {
            var result = await _unitManagementService.GetAllUnitAsync(model.PageIndex, model.PageSize, model.Search,
               model.FormatSortExpression("Id, UnitName", "ShortName", "AllowDecimal"));

            var productJsonData = new
            {
                recordsTotal = result.total,
                recordsFiltered = result.totalDisplay,
                data = (from record in result.data
                        select new string[]
                        {
                            HttpUtility.HtmlEncode(record.UnitName),
                            HttpUtility.HtmlEncode(record.ShortName),
                            HttpUtility.HtmlEncode(record.AllowDecimal),
                            HttpUtility.HtmlEncode(record.Id.ToString())
                        }
                    ).ToArray()
            };

            return Json(productJsonData);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUnit(Guid id)
        {
            try
            {
                await _unitManagementService.DeleteUnitAsync(id);

                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "The unit has deleted successfuly",
                    Type = ResponseTypes.Success
                });

                return RedirectToAction(nameof(UnitList));
            }
            catch (Exception ex)
            {
                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "The unit has deleted failed",
                    Type = ResponseTypes.Danger
                });

                _logger.LogError(ex, "The unit deleted failed");
            }
            return View(nameof(UnitList));
        }

        public IActionResult GetUnitById(Guid id)
        {
            var unit = _unitManagementService.GetUnitByIdAsync(id);
            if (unit != null)
            {
                return Json(new { success = true, data = unit });
            }
            return Json(new { success = false, message = "Unit not found." });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUnit(UnitUpdateModel model)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation("Updating unit with data: {@Model}", model);
                var unit = await _unitManagementService.GetUnitByIdAsync(model.Id);
                unit = _mapper.Map(model, unit);
                unit.Id = model.Id;
                await _unitManagementService.UpdateUnitAsync(unit);

                return Json(new { success = true, message = "Unit updated successfully." });
            }
            _logger.LogInformation("Updating unit with data: {@Model}", model);
            return Json(new { success = false, message = "Error updating unit." });
        }
    }
}
