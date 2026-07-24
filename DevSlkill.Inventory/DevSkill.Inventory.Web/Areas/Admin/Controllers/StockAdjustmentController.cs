using AutoMapper;
using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities.StockAdjustmentEntites;
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
                model.FormatSortExpression(
                    "Product.ProductName",
                    "AdjustmentQuantity",
                    "UnitPrice",
                    "StockAdjustment.TotalAmountRecover",
                    "StockAdjustment.Reason",
                    "StockAdjustment.BusinessLocation.LocationName",
                    "StockAdjustment.AdjustmentType",
                    "StockAdjustment.AddedBy",
                    "Id"));

            var stockAdjustmentJsonData = new
            {
                recordsTotal = result.total,
                recordsFiltered = result.totalDisplay,
                data = (from record in result.data
                        select new string[]
                        {
                            HttpUtility.HtmlEncode(record.StockAdjustment.ReferenceNo),
                            HttpUtility.HtmlEncode(record.Product.ProductName),
                            HttpUtility.HtmlEncode(record.StockAdjustment.BusinessLocation.LocationName),
                            HttpUtility.HtmlEncode(record.StockAdjustment.AdjustmentType.AdjustmentTypeName),
                            HttpUtility.HtmlEncode(record.StockAdjustment.Reason),
                            HttpUtility.HtmlEncode(record.AdjustmentQuantity),
                            HttpUtility.HtmlEncode(record.TotalAmount),
                            HttpUtility.HtmlEncode(record.StockAdjustment.TotalAmountRecover),
                            HttpUtility.HtmlEncode(record.StockAdjustment.AdjustmentDate),
                            HttpUtility.HtmlEncode(record.StockAdjustment.AddedBy),
                            HttpUtility.HtmlEncode(record.StockAdjustmentId.ToString())
                        }
                    ).ToArray()
            };

            return Json(stockAdjustmentJsonData);
        }


        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> CreateStockAdjustment()
        {
            try
            {
                var model = new StockAdjustmentCreateModel();

                model.ReferenceNo = GenerateReferenceNo();

                model.SetBusinessLocationValues(await _businessLocationManagementService.GetAllBusinessLocationAsync());
                model.SetAdjustmentTypeValues(await _adjustmentTypeManagementService.GetAllAdjustmentTypeAsync());

                foreach (var item in model.StockAdjustmentItems)
                {
                    item.SetUnitValues(await _unitManagementService.GetAllUnitAsync());
                }

                return View(model);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> CreateStockAdjustment(StockAdjustmentCreateModel model)
        {
            try
            {
                Guid stockAdjustmentId = Guid.NewGuid();

                var stockAdjustment = new StockAdjustment
                {
                    Id = stockAdjustmentId,
                    ReferenceNo = GenerateReferenceNo(),
                    AdjustmentDate = _applicationTime.GetCurrentDateTime(),
                    BusinessLocationId = model.BusinessLocationId,
                    AdjustmentTypeId = model.AdjustmentTypeId,
                    TotalAmountRecover = model.TotalAmountRecover,
                    Reason = model.Reason,
                    AddedBy = model.AddedBy,

                    StockAdjustmentItems = model.StockAdjustmentItems
                        .Where(x => x.ProductId != Guid.Empty && x.AdjustmentQuantity > 0)
                        .Select(x => new StockAdjustmentItem
                        {
                            Id = Guid.NewGuid(),
                            StockAdjustmentId = stockAdjustmentId,
                            ProductId = x.ProductId,
                            AdjustmentQuantity = (int)x.AdjustmentQuantity,
                            UnitPrice = x.UnitPrice,
                            TotalAmount = x.AdjustmentQuantity * x.UnitPrice

                        }).ToList()
                };

                await _stockAdjustmentManagementService.AddStockAdjustmentAsync(stockAdjustment);

                return RedirectToAction(nameof(StockAdjustmentList));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> UpdateStockAdjustment(Guid id)
        {
            try
            {
                var stockAdjustment = await _stockAdjustmentManagementService.GetStockAdjustmentByIdAsync(id);

                if (stockAdjustment == null)
                {
                    return NotFound();
                }

                var model = new StockAdjustmentUpdateModel
                {
                    Id = stockAdjustment.Id,
                    ReferenceNo = stockAdjustment.ReferenceNo,
                    AdjustmentDate = stockAdjustment.AdjustmentDate,
                    BusinessLocationId = stockAdjustment.BusinessLocationId,
                    AdjustmentTypeId = stockAdjustment.AdjustmentTypeId,
                    TotalAmountRecover = stockAdjustment.TotalAmountRecover,
                    Reason = stockAdjustment.Reason,
                    AddedBy = stockAdjustment.AddedBy,

                    StockAdjustmentItems = stockAdjustment.StockAdjustmentItems
                        .Select(x => new StockAdjustmentItemModel
                        {
                            ProductId = x.ProductId,
                            AdjustmentQuantity = x.AdjustmentQuantity,
                            UnitPrice = x.UnitPrice,
                            UnitId = x.Product.UnitId

                        }).ToList()
                };

                model.SetBusinessLocationValues(await _businessLocationManagementService.GetAllBusinessLocationAsync());
                model.SetAdjustmentTypeValues(await _adjustmentTypeManagementService.GetAllAdjustmentTypeAsync());

                foreach (var item in model.StockAdjustmentItems)
                {
                    item.SetUnitValues(await _unitManagementService.GetAllUnitAsync());
                    item.SetProductValues((await _productManagementService.GetAllProductByWarehouseAsync(stockAdjustment.BusinessLocation.Id)).ToList());
                }

                return View(model);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Exception Occured:", ex);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> UpdateStockAdjustment(StockAdjustmentUpdateModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    model.SetBusinessLocationValues(await _businessLocationManagementService.GetAllBusinessLocationAsync());
                    model.SetAdjustmentTypeValues(await _adjustmentTypeManagementService.GetAllAdjustmentTypeAsync());

                    foreach (var item in model.StockAdjustmentItems)
                    {
                        item.SetUnitValues(await _unitManagementService.GetAllUnitAsync());
                        item.SetProductValues((await _productManagementService
                            .GetAllProductByWarehouseAsync(model.BusinessLocationId)).ToList());
                    }

                    return View(model);
                }

                var stockAdjustment = new StockAdjustment
                {
                    Id = model.Id,
                    BusinessLocationId = model.BusinessLocationId,
                    AdjustmentTypeId = model.AdjustmentTypeId,
                    TotalAmountRecover = model.TotalAmountRecover,
                    Reason = model.Reason,
                    AddedBy = model.AddedBy,

                    StockAdjustmentItems = model.StockAdjustmentItems
                        .Where(x => x.ProductId != Guid.Empty && x.AdjustmentQuantity > 0)
                        .Select(x => new StockAdjustmentItem
                        {
                            ProductId = x.ProductId,
                            AdjustmentQuantity = (int)x.AdjustmentQuantity,
                            UnitPrice = x.UnitPrice,
                            TotalAmount = x.AdjustmentQuantity * x.UnitPrice
                        }).ToList()
                };

                await _stockAdjustmentManagementService.UpdateStockAdjustmentAsync(stockAdjustment);

                return RedirectToAction(nameof(StockAdjustmentList));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Exception Occured:", ex);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AdminOnly")]
        public async Task<JsonResult> DeleteStockAdjustment(Guid id)
        {
            try
            {
                await _stockAdjustmentManagementService.DeleteStockAdjustmentAsync(id);

                return Json(new
                {
                    success = true,
                    message = "Stock Adjustment deleted successfully."
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Stock Adjustment delete failed.");

                return Json(new
                {
                    success = false,
                    message = "Stock Adjustment delete failed."
                });
            }
        }

        [HttpGet]
        [Authorize(Policy = "ReadPermission")]
        public async Task<IActionResult> GetStockAdjustmentDetails(Guid id)
        {
            var stockAdjustment = await _stockAdjustmentManagementService
                .GetStockAdjustmentByIdAsync(id);

            if (stockAdjustment == null)
            {
                return NotFound();
            }

            var result = new
            {
                id = stockAdjustment.Id,
                referenceNo = stockAdjustment.ReferenceNo,
                adjustmentDate = stockAdjustment.AdjustmentDate,
                totalAmount = stockAdjustment.TotalAmount,
                totalAmountRecover = stockAdjustment.TotalAmountRecover,
                reason = stockAdjustment.Reason,
                addedBy = stockAdjustment.AddedBy,

                businessLocation = new
                {
                    locationName = stockAdjustment.BusinessLocation?.LocationName
                },

                adjustmentType = new
                {
                    adjustmentTypeName = stockAdjustment.AdjustmentType?.AdjustmentTypeName
                },

                stockAdjustmentItems = stockAdjustment.StockAdjustmentItems.Select(x => new
                {
                    productId = x.ProductId,
                    productName = x.Product?.ProductName,
                    adjustmentQuantity = x.AdjustmentQuantity,
                    unitPrice = x.UnitPrice,
                    totalAmount = x.TotalAmount

                }).ToList()
            };

            return Json(result);
        }

        private string GenerateReferenceNo()
        {
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                var random = new Random();

                string letters = new string(Enumerable.Repeat(chars, 6)
                    .Select(s => s[random.Next(s.Length)]).ToArray());

                return $"TR-{letters}-{DateTime.Now:dd-MM-yyyy}";
        }
    }
}