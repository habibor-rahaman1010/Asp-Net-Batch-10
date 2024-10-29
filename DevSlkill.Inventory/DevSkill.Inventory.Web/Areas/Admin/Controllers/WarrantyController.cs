using AutoMapper;
using DevSkill.Inventory.Infrastructure;
using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using DevSkill.Inventory.Application.Services;
using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class WarrantyController : Controller
    {
        private readonly IWarrantyManagementService _warrantyManagementService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public WarrantyController(IWarrantyManagementService warrantyManagementService,
            IMapper mapper,
            ILogger<WarrantyController> logger)
        {
            _warrantyManagementService = warrantyManagementService;
            _mapper = mapper;
            _logger = logger;
        }

        public IActionResult WarrantyList()
        {
            return View();
        }

        public async Task<JsonResult> GetWarrantyJsonData([FromBody] WarrantyListModel model)
        {
            var result = await _warrantyManagementService.GetAllWarrantyAsync(model.PageIndex, model.PageSize, model.Search,
               model.FormatSortExpression("Id, Name", "Description", "WarrantyDuration"));

            var warrantyJsonData = new
            {
                recordsTotal = result.total,
                recordsFiltered = result.totalDisplay,
                data = (from record in result.data
                        select new string[]
                        {
                            HttpUtility.HtmlEncode(record.Name),
                            HttpUtility.HtmlEncode(record.Description),
                            HttpUtility.HtmlEncode(record.WarrantyDuration),
                            HttpUtility.HtmlEncode(record.Id.ToString())
                        }
                    ).ToArray()
            };

            return Json(warrantyJsonData);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<JsonResult> AddWarranty(WarrantyCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var warranty = _mapper.Map<Warranty>(model);
                warranty.Id = Guid.NewGuid();

                try
                {
                    await _warrantyManagementService.AddWarrantyAsync(warranty);

                    return Json(new
                    {
                        success = true,
                        message = "Warranty created successfully"
                    });
                }

                catch (Exception ex)
                {
                    _logger.LogError(ex, "Warranty creation failed");
                    return Json(new
                    {
                        success = false,
                        message = "Warranty creation failed"
                    });
                }
            }
            return Json(new
            {
                success = false,
                message = "Warranty creation failed"
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteWarranty(Guid id)
        {
            try
            {
                await _warrantyManagementService.DeleteWarrantyAsync(id);

                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "The warranty has deleted successfuly",
                    Type = ResponseTypes.Success
                });

                return RedirectToAction(nameof(WarrantyList));
            }
            catch (Exception ex)
            {
                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "The warrranty has deleted failed",
                    Type = ResponseTypes.Danger
                });

                _logger.LogError(ex, "The warranty deleted failed");
            }
            return View(nameof(WarrantyList));
        }

        public async Task<IActionResult> GetWarrantyById(Guid id)
        {
            var warranty = await _warrantyManagementService.GetWarrantyByIdAsync(id);
            if (warranty != null)
            {
                return Json(new { success = true, data = warranty });
            }
            return Json(new { success = false, message = "warranty not found." });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> EditWarranty(WarrantyUpdateModel model)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation("Updating Warranty with data: {@Model}", model);
                var warranty = await _warrantyManagementService.GetWarrantyByIdAsync(model.Id);
                warranty = _mapper.Map(model, warranty);
                warranty.Id = model.Id;
                await _warrantyManagementService.UpdateWarrantyAsync(warranty);

                return Json(new { success = true, message = "Warranty updated successfully." });
            }
            _logger.LogInformation("Updating Warranty with data: {@Model}", model);
            return Json(new { success = false, message = "Error updating Brand." });
        }
    }
}
