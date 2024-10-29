using AutoMapper;
using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SellingPriceTaxController : Controller
    {
        private readonly ISellingPriceTaxManagementService _sellingPriceTaxManagementService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public SellingPriceTaxController(ISellingPriceTaxManagementService sellingPriceTaxManagementService,
            IMapper mapper,
            ILogger<SellingPriceTaxController> logger)
        {
            _sellingPriceTaxManagementService = sellingPriceTaxManagementService;
            _mapper = mapper;
            _logger = logger;
        }

        public IActionResult SellingPriceTaxList()
        {
            return View();
        }

        //Get all selling price tax data... 
        public async Task<IActionResult> GetSellingPriceTaxJsonData([FromBody] SellingPriceTaxListModel model)
        {
            var result = await _sellingPriceTaxManagementService.GetAllSellingPriceTaxAsync(model.PageIndex, model.PageSize, model.Search,
               model.FormatSortExpression("Id", "ApplicableTaxName", "Description", "TaxRate"));

            var sellingPriceTaxJsonData = new
            {
                recordsTotal = result.total,
                recordsFiltered = result.totalDisplay,
                data = (from record in result.data
                        select new string[]
                        {
                            HttpUtility.HtmlEncode(record.SellingPriceTaxName),
                            HttpUtility.HtmlEncode(record.Description),
                            HttpUtility.HtmlEncode(record.TaxRate),
                            HttpUtility.HtmlEncode(record.Id.ToString())
                        }
                    ).ToArray()
            };

            return Json(sellingPriceTaxJsonData);
        }
    }
}
