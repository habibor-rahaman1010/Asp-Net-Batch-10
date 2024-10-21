using AutoMapper;
using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UnitController : Controller
    {

        private readonly IUnitManagementService _unitManagementService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public UnitController(IUnitManagementService unitManagementService,
            IMapper mapper,
            ILogger<UnitController> logger)
        {
            _unitManagementService = unitManagementService;
            _mapper = mapper;
            _logger = logger;
        }


        public IActionResult UnitList()
        {
            return View();
        }

        public async Task<JsonResult> GetUnitJsonResponse([FromBody] UnitListModel model)
        {
            var result = await _unitManagementService.GetAllUnitAsync(model.PageIndex, model.PageSize, model.Search,
               model.FormatSortExpression("Id, UnitName", "ShortName", "AllowDecimal"));

            var productJsonData = new
            {
                recordsTotal = result.total,
                recordsFiltered = result.totalDisplay,
                data = (from record in result.data
                        select new string[]
                        {
                            HttpUtility.HtmlEncode(record.UnitName),
                            HttpUtility.HtmlEncode(record.ShortName),
                            HttpUtility.HtmlEncode(record.AllowDecimal),
                            HttpUtility.HtmlEncode(record.Id.ToString())
                        }
                    ).ToArray()
            };

            return Json(productJsonData);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<JsonResult> AddUnit(UnitCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var unit = _mapper.Map<Unit>(model);
                unit.Id = Guid.NewGuid();

                try
                {
                    await _unitManagementService.AddUnitAsync(unit);

                    return Json(new
                    {
                        success = true,
                        message = "Unit created successfully"
                    });
                }

                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unit creation failed");
                    return Json(new
                    {
                        success = false,
                        message = "Unit creation failed"
                    });
                }
            }
            return Json(new
            {
                success = false,
                message = "Unit creation failed"
            });
        }
    }
}
