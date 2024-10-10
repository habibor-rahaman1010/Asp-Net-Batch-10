using AutoMapper;
using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using DevSkill.Inventory.Infrastructure;
using System.Web;


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

        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var category = _mapper.Map<Category>(model);
                category.Id = Guid.NewGuid();

                try
                {
                    await _categoryManagementService.AddCategoryAsync(category);

                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "Categoey created successfuly",
                        Type = ResponseTypes.Success
                    });

                    return RedirectToAction(nameof(CategoryList));
                }

                catch (Exception ex)
                {
                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "Category post creation failed",
                        Type = ResponseTypes.Danger
                    });
                    _logger.LogError(ex, "category creation failed");
                }
            }

            return View(model);
        }


    }
}
