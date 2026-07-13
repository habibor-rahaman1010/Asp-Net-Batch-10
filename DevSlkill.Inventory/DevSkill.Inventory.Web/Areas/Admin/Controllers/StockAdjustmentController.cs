using AutoMapper;
using DevSkill.Inventory.Application.Services;
using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities.StockAdjustmentEntites;
using DevSkill.Inventory.Infrastructure;
using DevSkill.Inventory.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class StockAdjustmentController : Controller
    {
        private readonly IStockAdjustmentManagementService _stockAdjustmentManagementService;
        private readonly IBusinessLocationManagementService _businessLocationManagementService;
        private readonly IAdjustmentTypeManagementService _adjustmentTypeManagementService;
        private readonly IProductManagementService _productManagementService;
        private readonly IUnitManagementService _unitManagementService;
        private readonly IApplicationTime _applicationTime;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public StockAdjustmentController(IStockAdjustmentManagementService stockAdjustmentManagementService,
            IBusinessLocationManagementService businessLocationManagementService,
            IAdjustmentTypeManagementService adjustmentTypeManagementService,
            IProductManagementService productManagementService,
            IUnitManagementService unitManagementService,
            IApplicationTime applicationTime,
            IMapper mapper,
            ILogger<StockAdjustmentController> logger)
        {
            _stockAdjustmentManagementService = stockAdjustmentManagementService;
            _businessLocationManagementService = businessLocationManagementService;
            _adjustmentTypeManagementService = adjustmentTypeManagementService;
            _productManagementService = productManagementService;
            _unitManagementService = unitManagementService;
            _applicationTime = applicationTime;
            _mapper = mapper;
            _logger = logger;
        }

        [Authorize(Policy = "ReadPermission")]
        public IActionResult StockAdjustmentList()
        {
            return View();
        }

        [Authorize(Policy = "ReadPermission")]
        public async Task<JsonResult> GetStockAdjustmentJsonData([FromBody] StockAdjustmentListModel model)
        {
            var result = await _stockAdjustmentManagementService.GetAllStockAdjustmentAsync(model.PageIndex, model.PageSize, model.Search,
               model.FormatSortExpression("Id", "BusinessLocation", "AdjustmentType", "Product", "ReferenceNo", "TotalAmount", "TotalAmountRecover", "Reason", "AdjustmentDate", "AddedBy"));

            var stockAdjustmentJsonData = new
            {
                recordsTotal = result.total,
                recordsFiltered = result.totalDisplay,
                data = (from record in result.data
                        select new string[]
                        {
                            HttpUtility.HtmlEncode(record.BusinessLocation.LocationName),
                            HttpUtility.HtmlEncode(record.AdjustmentType.AdjustmentTypeName),
                            HttpUtility.HtmlEncode(record.Product.ProductName),
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

            return Json(stockAdjustmentJsonData);
        }

        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> CreateStockAdjustment()
        {
            var model = new StockAdjustmentCreateModel();
            model.SetBusinessLocationValues(await _businessLocationManagementService.GetAllBusinessLocationAsync());
            model.SetAdjustmentTypeValues(await _adjustmentTypeManagementService.GetAllAdjustmentTypeAsync());
            
            foreach (var item in model.StockAdjustmentItems)
            {
                item.SetUnitValues(await _unitManagementService.GetAllUnitAsync());
            }
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> CreateStockAdjustment(StockAdjustmentCreateModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var businessLocation = await _businessLocationManagementService.GetBusinessLocationByIdAsync(model.BusinessLocationId);

                    var adjustmentType = await _adjustmentTypeManagementService.GetAdjustmentTypeByIdAsync(model.AdjustmentTypeId);

                    foreach (var item in model.StockAdjustmentItems)
                    {
                        var product = await _productManagementService.GetProductByIdAsync(item.ProductId);

                        var stockAdjustment = new StockAdjustment
                        {
                            ReferenceNo = model.ReferenceNo,
                            AdjustmentDate = _applicationTime.GetCurrentDateTime(),
                            TotalAmount = model.TotalAmount,
                            TotalAmountRecover = model.TotalAmountRecover,
                            Reason = model.Reason,
                            AddedBy = model.AddedBy,

                            AdjustmentQuantity = (int) item.AdjustmentQuantity,
                            UnitPrice = item.UnitPrice,

                            Product = product,
                            BusinessLocation = businessLocation,
                            AdjustmentType = adjustmentType
                        };

                        product.CurrentStock += (int)item.AdjustmentQuantity * adjustmentType.Sign;

                        await _productManagementService.UpdateProductAsync(product);
                        await _stockAdjustmentManagementService.AddStockAdjustmentAsync(stockAdjustment);
                    }

                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "The Stock Adjustment has been created successfully!",
                        Type = ResponseTypes.Success
                    });

                    return RedirectToAction(nameof(StockAdjustmentList));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Stock Adjustment creation failed.");

                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "The Stock Adjustment creation has failed!",
                        Type = ResponseTypes.Danger
                    });
                }
            }

            model.SetBusinessLocationValues(await _businessLocationManagementService.GetAllBusinessLocationAsync());

            model.SetAdjustmentTypeValues(await _adjustmentTypeManagementService.GetAllAdjustmentTypeAsync());

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "AdminOnly")]
        public async Task<JsonResult> DeleteStockAdjustment(Guid id)
        {
            try
            {
                // When we delete stock the current stock Adjustment Quantity delete the product's current stock.
                var stock = await _stockAdjustmentManagementService.GetStockAdjustmentByIdAsync(id);
                var productId = stock.ProductId;
                var product = await _productManagementService.GetProductByIdAsync(productId);
                product.CurrentStock -= stock.AdjustmentQuantity;
                await _productManagementService.UpdateProductAsync(product);

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
                    message = "The Stock Adjustment deleted failed"
                });
            }
        }

        [HttpGet, Authorize(Policy = "ReadPermission")]
        public async Task<IActionResult> GetStockAdjustmentyById(Guid id)
        {
            var stockAdjustment = await _stockAdjustmentManagementService.GetStockAdjustmentByIdAsync(id);
            if (stockAdjustment == null)
            {
                return NotFound();
            }
            return Json(stockAdjustment);
        }

        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> UpdateStockAdjustment(Guid id)
        {
            var stockAdjustment = await _stockAdjustmentManagementService.GetStockAdjustmentByIdAsync(id);

            if (stockAdjustment == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<StockAdjustmentUpdateModel>(stockAdjustment);
            model.Product = stockAdjustment.Product!;
            model.SetBusinessLocationValues(await _businessLocationManagementService.GetAllBusinessLocationAsync());
            model.SetAdjustmentTypeValues(await _adjustmentTypeManagementService.GetAllAdjustmentTypeAsync());

            return View(model);
        }
    }
}
