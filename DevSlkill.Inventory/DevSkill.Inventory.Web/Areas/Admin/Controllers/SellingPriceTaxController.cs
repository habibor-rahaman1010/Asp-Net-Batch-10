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

        //This is method for create new selling price tax 
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSellingPriceTax(SellingPriceTaxCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var sellingPriceTax = _mapper.Map<SellingPriceTax>(model);
                sellingPriceTax.Id = Guid.NewGuid();

                try
                {
                    await _sellingPriceTaxManagementService.AddSellingPriceTaxAsync(sellingPriceTax);

                    return Json(new
                    {
                        success = true,
                        message = "Selling Price Tax created successfully"
                    });
                }

                catch (Exception ex)
                {
                    _logger.LogError(ex, "Selling Price Tax creation failed");
                    return Json(new
                    {
                        success = false,
                        message = "Selling Price Tax creation failed"
                    });
                }
            }
            return Json(new
            {
                success = false,
                message = "Selling Price Tax creation failed"
            });
        }

        //This code for Selling Price tax update...
        public async Task<IActionResult> GetSellingPriceTaxById(Guid id)
        {
            var sellingPriceTax = await _sellingPriceTaxManagementService.GetSellingPriceTaxByIdAsync(id);
            if (sellingPriceTax != null)
            {
                return Json(new { success = true, data = sellingPriceTax });
            }
            return Json(new { success = false, message = "Selling price tax not found." });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateSellingPriceTax(SellingPriceTaxUpdateModel model)
        {

            if (ModelState.IsValid)
            {
                var sellingPriceTax = await _sellingPriceTaxManagementService.GetSellingPriceTaxByIdAsync(model.Id);
                sellingPriceTax = _mapper.Map(model, sellingPriceTax);
                sellingPriceTax.Id = model.Id;
                await _sellingPriceTaxManagementService.UpdateSellingPriceTaxAsync(sellingPriceTax);

                return Json(new { success = true, message = "Selling Price tax updated successfully." });
            }
            return Json(new { success = false, message = "Error updating selling price tax." });
        }

        //This code for delete
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<JsonResult> DeleteSellingPriceTax(Guid id)
        {
            try
            {
                await _sellingPriceTaxManagementService.DeleteSellingPriceTaxAsync(id);

                return Json(new
                {
                    success = true,
                    message = "The selling price tax has deleted successfuly"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "The selling price tax deleted failed");
                return Json(new
                {
                    success = false,
                    message = "The selling price tax deleted failed"
                });
            }
        }
    }
}
