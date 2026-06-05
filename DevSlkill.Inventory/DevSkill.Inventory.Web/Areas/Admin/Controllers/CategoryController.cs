using AutoMapper;
using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using DevSkill.Inventory.Infrastructure;
using System.Web;
using DevSkill.Inventory.Application.Services;
using Microsoft.AspNetCore.Authorization;


namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryManagementService _categoryManagementService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public CategoryController(ICategoryManagementService categoryManagementService,
            IMapper mapper, 
            ILogger<CategoryController> logger)
        {
            _categoryManagementService = categoryManagementService;
            _mapper = mapper;
            _logger = logger;
        }

        [Authorize(Policy = "ReadPermission")]
        public IActionResult CategoryList()
        {
            return View();
        }

        [Authorize(Policy = "ReadPermission")]
        public async Task<JsonResult> GetCategoryJsonData([FromBody] CategoryListModel model)
        {
            var result = await _categoryManagementService.GetCategoriesAsync(model.PageIndex, model.PageSize, model.Search,
                model.FormatSortExpression("Id","CategoryName", "CategoryCode", "Description"));

            var categoryJsonData = new
            {
                recordsTotal = result.total,
                recordsFiltered = result.totalDisplay,
                data = (from record in result.data
                        select new string[]
                        {
                            HttpUtility.HtmlEncode(record.CategoryName),
                            HttpUtility.HtmlEncode(record.CategoryCode),
                            HttpUtility.HtmlEncode(record.Description),
                            HttpUtility.HtmlEncode(record.Id.ToString())
                        }
                    ).ToArray()
            };

            return Json(categoryJsonData);
        }       

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "AdminOnly")]
        public async Task<JsonResult> AddCategory(CategoryCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var category = _mapper.Map<Category>(model);
                category.Id = Guid.NewGuid();

                try
                {
                    await _categoryManagementService.AddCategoryAsync(category);

                    return Json(new
                    {
                        success = true,
                        message = "Category created successfully"
                    });
                }

                catch (Exception ex)
                {
                    _logger.LogError(ex, "category creation failed");
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
                message = "Category creation failed"
            });
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> CategoryDelete(Guid id)
        {
            try
            {
                await _categoryManagementService.DeleteCategoryAsync(id);

                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "The category has deleted successfuly",
                    Type = ResponseTypes.Success
                });

                return RedirectToAction(nameof(CategoryList));
            }
            catch (Exception ex)
            {
                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "The category has deleted failed",
                    Type = ResponseTypes.Danger
                });

                _logger.LogError(ex, "The category deleted failed");
            }
            return View();
        }

        [Authorize(Policy = "AdminOnly")]
        public IActionResult GetCategoryById(Guid id)
        {
            var category = _categoryManagementService.GetCategoryById(id);
            if (category != null)
            {
                return Json(new { success = true, data = category });
            }
            return Json(new { success = false, message = "Category not found." });
        }
         
        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> EditCategoryModal(UpdateCategoryModel model)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation("Updating category with data: {@Model}", model);
                var category = await _categoryManagementService.GetCategoryById(model.Id);
                category = _mapper.Map(model, category);
                category.Id = model.Id;
                await _categoryManagementService.UpdateCategoryAsync(category);
                
               return Json(new { success = true, message = "Category updated successfully." });
               
            }
            _logger.LogInformation("Updating category with data: {@Model}", model);
            return Json(new { success = false, message = "Error updating category." });
        }
    }
}
