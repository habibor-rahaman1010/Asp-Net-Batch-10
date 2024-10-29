using AutoMapper;
using DevSkill.Inventory.Application.Services;
using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubCategoryController : Controller
    {
        private readonly ISubCategoryManagementService _subCategoryManagementService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public SubCategoryController(ISubCategoryManagementService subCategoryManagementService,
            IMapper mapper,
            ILogger<SubCategoryController> logger)
        {
            _subCategoryManagementService = subCategoryManagementService;
            _mapper = mapper;
            _logger = logger;
        }

        public IActionResult SubCategoryList()
        {
            return View();
        }

        //Get all sub category data
        public async Task<JsonResult> GetAllSubCategoryJsonData([FromBody] SubCategoryListModel model)
        {
            var result = await _subCategoryManagementService.GetAllSubCategoryAsync(model.PageIndex, model.PageSize, model.Search,
                model.FormatSortExpression("Id", "SubCategoryName", "Description", "CategoryCode"));

            var subCategoryJsonData = new
            {
                recordsTotal = result.total,
                recordsFiltered = result.totalDisplay,
                data = (from record in result.data
                        select new string[]
                        {
                            HttpUtility.HtmlEncode(record.SubCategoryName),
                            HttpUtility.HtmlEncode(record.Description),
                            HttpUtility.HtmlEncode(record.CategoryCode),
                            HttpUtility.HtmlEncode(record.Id.ToString())
                        }
                    ).ToArray()
            };

            return Json(subCategoryJsonData);
        }

        //This is method for create new sub category 
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<JsonResult> AddSubCategory(SubCategoryCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var subCategory = _mapper.Map<SubCategory>(model);
                subCategory.Id = Guid.NewGuid();

                try
                {
                    await _subCategoryManagementService.AddSubCategoryAsync(subCategory);

                    return Json(new
                    {
                        success = true,
                        message = "Sub Category created successfully"
                    });
                }

                catch (Exception ex)
                {
                    _logger.LogError(ex, "Sub Category creation failed");
                    return Json(new
                    {
                        success = false,
                        message = "Sub Category creation failed"
                    });
                }
            }
            return Json(new
            {
                success = false,
                message = "Sub Category creation failed"
            });
        }

        //This code for product type update
        public async Task<IActionResult> GetSubCategoryById(Guid id)
        {
            var subCategory = await _subCategoryManagementService.GetSubCategoryByIdAsync(id);
            if (subCategory != null)
            {
                return Json(new { success = true, data = subCategory });
            }
            return Json(new { success = false, message = "Sub Category not found." });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateSubCategory(SubCategoryUpdateModel model)
        {

            if (ModelState.IsValid)
            {
                var subCategory = await _subCategoryManagementService.GetSubCategoryByIdAsync(model.Id);
                subCategory = _mapper.Map(model, subCategory);
                subCategory.Id = model.Id;
                await _subCategoryManagementService.UpdateSubCategoryAsync(subCategory);

                return Json(new { success = true, message = "Sub Category updated successfully." });
            }
            return Json(new { success = false, message = "Error updating Sub Category." });
        }

       //This code for delete
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<JsonResult> DeleteSubCategory(Guid id)
        {
            try
            {
                await _subCategoryManagementService.DeleteSubCategoryAsync(id);

                return Json(new
                {
                    success = true,
                    message = "The Sub Category has deleted successfuly"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "The Sub Category deleted failed");
                return Json(new
                {
                    success = false,
                    message = "The Sub Category deleted failed"
                });
            }
        }
    }
}
