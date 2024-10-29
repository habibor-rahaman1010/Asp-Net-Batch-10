using AutoMapper;
using DevSkill.Inventory.Application.Services;
using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Web;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BarcodeTypeController : Controller
    {
        private readonly IBarcodeTypeManagementService _barcodeTypeManagementService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public BarcodeTypeController(IBarcodeTypeManagementService barcodeTypeManagementService,
            IMapper mapper,
            ILogger<BarcodeTypeController> logger)
        {
            _barcodeTypeManagementService = barcodeTypeManagementService;
            _mapper = mapper;
            _logger = logger;
        }

        public IActionResult BarcodeTypeList()
        {
            return View();
        }

        public async Task<JsonResult> GetBarcodeTypeJsonData([FromBody] BarcodeTypeListModel model)
        {
            var result = await _barcodeTypeManagementService.GetAllBarcodeTypeAsync(model.PageIndex, model.PageSize, model.Search,
               model.FormatSortExpression("Id, BarcodeTypeName", "BarcodeDescription", "BarcodeTypeCode"));

            var productJsonData = new
            {
                recordsTotal = result.total,
                recordsFiltered = result.totalDisplay,
                data = (from record in result.data
                        select new string[]
                        {
                            HttpUtility.HtmlEncode(record.BarcodeTypeName),
                            HttpUtility.HtmlEncode(record.BarcodeDescription),
                            HttpUtility.HtmlEncode(record.BarcodeTypeCode),
                            HttpUtility.HtmlEncode(record.Id.ToString())
                        }
                    ).ToArray()
            };

            return Json(productJsonData);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<JsonResult> AddBarcodeType(BarcodeTypeCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var barcodeType = _mapper.Map<BarcodeType>(model);
                barcodeType.Id = Guid.NewGuid();

                try
                {
                    await _barcodeTypeManagementService.AddBarcodeTypeAsync(barcodeType);

                    return Json(new
                    {
                        success = true,
                        message = "Barcode type created successfully"
                    });
                }

                catch (Exception ex)
                {
                    _logger.LogError(ex, "Barcode creation failed");
                    return Json(new
                    {
                        success = false,
                        message = "Barcode type creation failed"
                    });
                }
            }
            return Json(new
            {
                success = false,
                message = "Barcode type creation failed"
            });
        }

        public async Task<IActionResult> GetBarcodeTypeById(Guid id)
        {
            var barcodeType = await _barcodeTypeManagementService.GetBarcodeTypeId(id);
            if (barcodeType != null)
            {
                return Json(new { success = true, data = barcodeType });
            }
            return Json(new { success = false, message = "Barcode Type not found." });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateBarcodeType(BarcodeTypeUpdateModel model)
        {

           /* foreach (var modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    _logger.LogError(error.ErrorMessage);
                }
            }

            var messages = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));*/

            if (ModelState.IsValid)
            {
                var barcodeType = await _barcodeTypeManagementService.GetBarcodeTypeId(model.Id);
                barcodeType = _mapper.Map(model, barcodeType);
                barcodeType.Id = model.Id;
                await _barcodeTypeManagementService.UpdateBarcodeTypeAsync(barcodeType);

                return Json(new { success = true, message = "Barcode Type updated successfully." });
            }
            return Json(new { success = false, message = "Error updating Barcode Type." });
        }

        //this code for delete
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<JsonResult> DeleteBarcodeType(Guid id)
        {
            try
            {
                await _barcodeTypeManagementService.DeleteBarcodeTypeAsync(id);

                return Json(new
                {
                    success = true,
                    message = "The Barcode Type has deleted successfuly"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "The Barcode Type deleted failed");
                return Json(new
                {
                    success = false,
                    message = "The Barcode Type deleted failed"
                });
            }
        }
    }
}
