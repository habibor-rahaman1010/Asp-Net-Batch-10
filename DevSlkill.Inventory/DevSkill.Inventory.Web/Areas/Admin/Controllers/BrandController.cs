using AutoMapper;
using DevSkill.Inventory.Infrastructure;
using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using DevSkill.Inventory.Application.Services;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController : Controller
    {
        private readonly IBrandManagementService _brandManagementService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public BrandController(IBrandManagementService brandManagementService, IMapper mapper, ILogger<BrandController> logger)
        {
            _brandManagementService = brandManagementService;
            _mapper = mapper;
            _logger = logger;
        }

        public IActionResult BrandList()
        {
            return View();
        }

        public async Task<JsonResult> GetBrandJsonData([FromBody] BrandListModel model)
        {
            var result = await _brandManagementService.GetBrandsAsync(model.PageIndex, model.PageSize, model.Search,
                model.FormatSortExpression("Id, BrandName", "BandOrigin", "Description"));

            var productJsonData = new
            {
                recordsTotal = result.total,
                recordsFiltered = result.totalDisplay,
                data = (from record in result.data
                        select new string[]
                        {
                            HttpUtility.HtmlEncode(record.BrandName),
                            HttpUtility.HtmlEncode(record.BandOrigin),
                            HttpUtility.HtmlEncode(record.Description),
                            HttpUtility.HtmlEncode(record.Id.ToString())
                        }
                    ).ToArray()
            };

            return Json(productJsonData);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<JsonResult> AddBrand(CreateBrandModel model)
        {
            if (ModelState.IsValid)
            {
                var brand = _mapper.Map<Brand>(model);
                brand.Id = Guid.NewGuid();

                try
                {
                    await _brandManagementService.AddBrandAsync(brand);

                    return Json(new
                    {
                        success = true,
                        message = "Brand created successfully"
                    });
                }

                catch (Exception ex)
                {
                    _logger.LogError(ex, "Brand creation failed");
                    return Json(new
                    {
                        success = false,
                        message = "Category creation failed"
                    });
                }
            }

            return Json(new
            {
                success = false,
                message = "Brand creation failed"
            });
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> BrandDelete(Guid id)
        {
            try
            {
                await _brandManagementService.DeleteBrandAsync(id);

                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "The brand has deleted successfuly",
                    Type = ResponseTypes.Success
                });

                return RedirectToAction(nameof(BrandList));
            }
            catch (Exception ex)
            {
                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "The brand has deleted failed",
                    Type = ResponseTypes.Danger
                });

                _logger.LogError(ex, "The brand deleted failed");
            }
            return View();
        }

        public IActionResult GetBrandById(Guid id)
        {
            var brand = _brandManagementService.GetBrandByIdAsync(id);
            if (brand != null)
            {
                return Json(new { success = true, data = brand });
            }
            return Json(new { success = false, message = "Brand not found." });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBrand(UpdateBrandModel model)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation("Updating brand with data: {@Model}", model);
                var brand = await _brandManagementService.GetBrandByIdAsync(model.Id);
                brand = _mapper.Map(model, brand);
                brand.Id = model.Id;
                await _brandManagementService.UpdateBrandAsync(brand);

                return Json(new { success = true, message = "Brand updated successfully." });

            }
            _logger.LogInformation("Updating brand with data: {@Model}", model);
            return Json(new { success = false, message = "Error updating Brand." });
        }

    }
}
