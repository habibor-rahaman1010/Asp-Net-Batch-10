using DevSkill.Inventory.Application.Services;
using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private ICategoryManagementService _categoryManagementService;
        public CategoryController(ICategoryManagementService categoryManagementService)
        {
            _categoryManagementService = categoryManagementService;
        }

        public IActionResult CategoryList()
        {
            return View();
        }

        public async Task<JsonResult> GetCategoryJsonData([FromBody] CategoryListModel model)
        {
            var result = await _categoryManagementService.GetCategoriesAsync(model.PageIndex, model.PageSize, model.Search,
                model.FormatSortExpression("Id", "ProductName", "Description", "Price", "Ratings"));

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
    }
}
