using AutoMapper;
using DevSkill.Inventory.Application.Services;
using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Infrastructure;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Entities.StockAdjustmentEntites;
using DevSkill.Inventory.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StockAdjustmentController : Controller
    {
        private readonly IStockAdjustmentManagementService _stockAdjustmentManagementService;
        private readonly IBusinessLocationManagementService _businessLocationManagementService;
        private readonly IAdjustmentTypeManagementService _adjustmentTypeManagementService;
        private readonly IProductManagementService _productManagementService;
        private readonly IApplicationTime _applicationTime;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public StockAdjustmentController(IStockAdjustmentManagementService stockAdjustmentManagementService,
            IBusinessLocationManagementService businessLocationManagementService,
            IAdjustmentTypeManagementService adjustmentTypeManagementService,
            IProductManagementService productManagementService,
            IApplicationTime applicationTime,
            IMapper mapper,
            ILogger<StockAdjustmentController> logger)
        {
            _stockAdjustmentManagementService = stockAdjustmentManagementService;
            _businessLocationManagementService = businessLocationManagementService;
            _adjustmentTypeManagementService = adjustmentTypeManagementService;
            _productManagementService = productManagementService;
            _applicationTime = applicationTime;
            _mapper = mapper;
            _logger = logger;
        }

        public IActionResult StockAdjustmentList()
        {
            return View();
        }

        public async Task<JsonResult> GetStockAdjustmentJsonData([FromBody] StockAdjustmentListModel model)
        {
            var result = await _stockAdjustmentManagementService.GetAllStockAdjustmentAsync(model.PageIndex, model.PageSize, model.Search,
               model.FormatSortExpression("BusinessLocation", "AdjustmentType", "ReferenceNo", "TotalAmount", "TotalAmountRecover", "Reason", "AdjustmentDate", "AddedBy"));

            var productJsonData = new
            {
                recordsTotal = result.total,
                recordsFiltered = result.totalDisplay,
                data = (from record in result.data
                        select new string[]
                        {
                            HttpUtility.HtmlEncode(record.BusinessLocation.LocationName),
                            HttpUtility.HtmlEncode(record.AdjustmentType.AdjustmentTypeName),
                            HttpUtility.HtmlEncode(record.ReferenceNo),
                            HttpUtility.HtmlEncode(record.TotalAmount),
                            HttpUtility.HtmlEncode(record.TotalAmountRecover),
                            HttpUtility.HtmlEncode(record.Reason),
                            HttpUtility.HtmlEncode(record.AdjustmentDate),
                            HttpUtility.HtmlEncode(record.AddedBy),
                            HttpUtility.HtmlEncode(record.Id.ToString())
                        }
                    ).ToArray()
            };

            return Json(productJsonData);
        }


        public async Task<IActionResult> CreateStockAdjustment()
        {
            var model = new StockAdjustmentCreateModel();
            model.SetBusinessLocationValues(await _businessLocationManagementService.GetAllBusinessLocationAsync());
            model.SetAdjustmentTypeValues(await _adjustmentTypeManagementService.GetAllAdjustmentTypeAsync());
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateStockAdjustment(StockAdjustmentCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var stockAdjustment = _mapper.Map<StockAdjustment>(model);
                var product = await _productManagementService.GetProductByIdAsync(model.ProductId);
                stockAdjustment.Product = product;
                stockAdjustment.BusinessLocation = await _businessLocationManagementService.GetBusinessLocationByIdAsync(model.BusinessLocationId);
                stockAdjustment.AdjustmentType = await _adjustmentTypeManagementService.GetAdjustmentTypeByIdAsync(model.AdjustmentTypeId);
                stockAdjustment.AdjustmentDate = _applicationTime.GetCurrentDateTime();

                product.CurrentStock += (int) model.AdjustmentQuantity;
                await _productManagementService.UpdateProductAsync(product);

                try
                {
                    await _stockAdjustmentManagementService.AddStockAdjustmentAsync(stockAdjustment);

                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "The Stock AdjustmentList has been created successfully!",
                        Type = ResponseTypes.Success
                    });

                    return RedirectToAction("StockAdjustmentList");
                }
                catch (Exception ex)
                {
                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "The Stock AdjustmentList creation has failed!",
                        Type = ResponseTypes.Danger
                    });
                    _logger.LogError(ex, "Ultimately, the stock adjustment creation failed!");
                }
            }

            model.SetBusinessLocationValues(await _businessLocationManagementService.GetAllBusinessLocationAsync());
            model.SetAdjustmentTypeValues(await _adjustmentTypeManagementService.GetAllAdjustmentTypeAsync());

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<JsonResult> DeleteStockAdjustment(Guid id)
        {
            try
            {
                await _stockAdjustmentManagementService.DeleteStockAdjustmentAsync(id);

                return Json(new
                {
                    success = true,
                    message = "The Stock Adjustment has deleted successfuly"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "The Stock Adjustment deleted failed");
                return Json(new
                {
                    success = false,
                    message = "The Stoc kAdjustment deleted failed"
                });
            }
        }
    }
}
