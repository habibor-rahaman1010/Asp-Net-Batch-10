using AutoMapper;
using DevSkill.Inventory.Application.ServicesContract;
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
    }
}
