using AutoMapper;
using DevSkill.Inventory.Infrastructure;
using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using System.Web;

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

            var productJsonData = new
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

            return Json(productJsonData);
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
    }
}
