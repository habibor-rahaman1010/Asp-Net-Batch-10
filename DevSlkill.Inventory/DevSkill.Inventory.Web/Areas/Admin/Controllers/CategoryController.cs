using AutoMapper;
using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using DevSkill.Inventory.Infrastructure;
using System.Web;
using DevSkill.Inventory.Application.Services;


namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
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

        public IActionResult CategoryList()
        {
            return View();
        }

        public async Task<JsonResult> GetCategoryJsonData([FromBody] CategoryListModel model)
        {
            var result = await _categoryManagementService.GetCategoriesAsync(model.PageIndex, model.PageSize, model.Search,
                model.FormatSortExpression("CategoryName", "CategoryCode", "Description"));

            var productJsonData = new
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

            return Json(productJsonData);
        }

        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
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



        [HttpPost, ValidateAntiForgeryToken]
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


        //This code for update the category
        public async Task<IActionResult> EditCategory(Guid id)
        {
            var category = await _categoryManagementService.GetCategoryById(id);
            var moddel = _mapper.Map<UpdateCategoryModel>(category);
            return View(moddel);
        }

        [HttpPost]
        public async Task<IActionResult> EditCategory(UpdateCategoryModel model)
        {
            if (ModelState.IsValid)
            {
                var category = await _categoryManagementService.GetCategoryById(model.Id);
                category = _mapper.Map(model, category);
               
                try
                {
                    await _categoryManagementService.UpdateCategoryAsync(category);
                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "The category has been update successfuly",
                        Type = ResponseTypes.Success
                    });
                    return RedirectToAction($"{nameof(CategoryList)}");
                }
                catch (Exception ex)
                {
                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "The category update has failed!",
                        Type = ResponseTypes.Danger
                    });
                    _logger.LogError(ex, "Ultimatly the category updated failed!");
                }
            }
            return View(model);
        }

    }
}
