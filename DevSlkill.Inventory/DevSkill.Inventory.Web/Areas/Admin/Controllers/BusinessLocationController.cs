using AutoMapper;
using DevSkill.Inventory.Infrastructure;
using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using DevSkill.Inventory.Application.Services;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json.Serialization;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class BusinessLocationController : Controller
    {
        private readonly IBusinessLocationManagementService _businessLocationManagementService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public BusinessLocationController(IBusinessLocationManagementService businessLocationManagementService,
            IMapper mapper,
            ILogger<BusinessLocationController> logger)
        {
            _businessLocationManagementService = businessLocationManagementService;
            _mapper = mapper;
            _logger = logger;
        }

        [Authorize(Policy = "ReadPermission")]
        public IActionResult BusinessLocationList()
        {
            return View();
        }

        [Authorize(Policy = "ReadPermission")]
        public async Task<JsonResult> GetBusinessLocationJsonData([FromBody] BusinessLocationListModel model)
        {
            var result = await _businessLocationManagementService.GetAllBusinessLocationAsync(model.PageIndex, model.PageSize, model.Search,
               model.FormatSortExpression("Id", "LocationName", "Address", "City", "State", "ZipCode", "Country"));

            var businessLocationJsonData = new
            {
                recordsTotal = result.total,
                recordsFiltered = result.totalDisplay,
                data = (from record in result.data
                        select new string[]
                        {
                            HttpUtility.HtmlEncode(record.LocationName),
                            HttpUtility.HtmlEncode(record.Address),
                            HttpUtility.HtmlEncode(record.City),
                            HttpUtility.HtmlEncode(record.State),
                            HttpUtility.HtmlEncode(record.ZipCode),
                            HttpUtility.HtmlEncode(record.Country),
                            HttpUtility.HtmlEncode(record.Id.ToString())
                        }
                    ).ToArray()
            };

            return Json(businessLocationJsonData);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "CustomAdminAccess")]
        public async Task<JsonResult> AddBussinessLocation(BusinessLocationCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var businessLocation = _mapper.Map<BusinessLocation>(model);
                businessLocation.Id = Guid.NewGuid();

                try
                {
                    await _businessLocationManagementService.AddBusinessLocationAsync(businessLocation);

                    return Json(new
                    {
                        success = true,
                        message = "Business Location created successfully"
                    });
                }

                catch (Exception ex)
                {
                    _logger.LogError(ex, "Business Location creation failed");
                    return Json(new
                    {
                        success = false,
                        message = "Business Location creation failed"
                    });
                }
            }
            return Json(new
            {
                success = false,
                message = "Business Location creation failed"
            });
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "CustomAdminAccess")]
        public async Task<IActionResult> DeleteBussinessLocation(Guid id)
        {
            try
            {
                await _businessLocationManagementService.DeleteBusinessLocationAsync(id);

                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "The Business Location has deleted successfuly",
                    Type = ResponseTypes.Success
                });

                return RedirectToAction(nameof(BusinessLocationList));
            }
            catch (Exception ex)
            {
                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "The Business Location has deleted failed",
                    Type = ResponseTypes.Danger
                });

                _logger.LogError(ex, "The Business Location deleted failed");
            }
            return View(nameof(BusinessLocationList));
        }

        [Authorize(Policy = "CustomAdminAccess")]
        public IActionResult GetBusinessLocationById(Guid id)
        {
            var businessLocation = _businessLocationManagementService.GetBusinessLocationByIdAsync(id);
            if (businessLocation != null)
            {
                return Json(new { success = true, data = businessLocation });
            }
            return Json(new { success = false, message = "Business Location not found." });
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "CustomAdminAccess")]
        public async Task<IActionResult> EditBusinessLocation(BusinessLocationUpdateModel model)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation("Updating Warranty with data: {@Model}", model);
                var businessLocation = await _businessLocationManagementService.GetBusinessLocationByIdAsync(model.Id);
                businessLocation = _mapper.Map(model, businessLocation);
                businessLocation.Id = model.Id;
                await _businessLocationManagementService.UpdateBusinessLocationAsync(businessLocation);

                return Json(new { success = true, message = "Business Location updated successfully." });
            }
            _logger.LogInformation("Updating Business Location with data: {@Model}", model);
            return Json(new { success = false, message = "Error updating Business Location." });
        } 

    }
}
