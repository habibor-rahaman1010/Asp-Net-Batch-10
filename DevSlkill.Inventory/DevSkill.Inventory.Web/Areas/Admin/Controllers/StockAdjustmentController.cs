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

            //[HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "AdminOnly")]
            //public async Task<JsonResult> DeleteStockAdjustment(Guid id)
            //{
            //    try
            //    {
            //        // When we delete stock the current stock Adjustment Quantity delete the product's current stock.
            //        var stock = await _stockAdjustmentManagementService.GetStockAdjustmentByIdAsync(id);
            //        var productId = stock.ProductId;
            //        var product = await _productManagementService.GetProductByIdAsync(productId);
            //        product.CurrentStock -= stock.AdjustmentQuantity;
            //        await _productManagementService.UpdateProductAsync(product);

            //        await _stockAdjustmentManagementService.DeleteStockAdjustmentAsync(id);

            //        return Json(new
            //        {
            //            success = true,
            //            message = "The Stock Adjustment has deleted successfuly"
            //        });
            //    }
            //    catch (Exception ex)
            //    {
            //        _logger.LogError(ex, "The Stock Adjustment deleted failed");
            //        return Json(new
            //        {
            //            success = false,
            //            message = "The Stock Adjustment deleted failed"
            //        });
            //    }
            //}

            //[HttpGet, Authorize(Policy = "ReadPermission")]
            //public async Task<IActionResult> GetStockAdjustmentyById(Guid id)
            //{
            //    var stockAdjustment = await _stockAdjustmentManagementService.GetStockAdjustmentByIdAsync(id);
            //    if (stockAdjustment == null)
            //    {
            //        return NotFound();
            //    }
            //    return Json(stockAdjustment);
            //}


            [Authorize(Policy = "AdminOnly")]
            public async Task<IActionResult> UpdateStockAdjustment(Guid id)
            {
                try
                {
                    var stockAdjustment = await _stockAdjustmentManagementService.GetStockAdjustmentByIdAsync(id);

                    if (stockAdjustment == null)
                        return NotFound();

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
                    var stockAdjustment = await _stockAdjustmentManagementService.GetStockAdjustmentByIdAsync(model.Id);

                    if (stockAdjustment == null)
                    {
                        return NotFound();
                    }

                    stockAdjustment.BusinessLocationId = model.BusinessLocationId;
                    stockAdjustment.AdjustmentTypeId = model.AdjustmentTypeId;
                    stockAdjustment.TotalAmountRecover = model.TotalAmountRecover;
                    stockAdjustment.Reason = model.Reason;
                    stockAdjustment.AddedBy = model.AddedBy;

                    stockAdjustment.StockAdjustmentItems.Clear();

                    // নতুন Detail Insert
                    stockAdjustment.StockAdjustmentItems = model.StockAdjustmentItems
                        .Where(x => x.ProductId != Guid.Empty && x.AdjustmentQuantity > 0)
                        .Select(x => new StockAdjustmentItem
                        {
                            Id = Guid.NewGuid(),
                            StockAdjustmentId = stockAdjustment.Id,
                            ProductId = x.ProductId,
                            AdjustmentQuantity = (int)x.AdjustmentQuantity,
                            UnitPrice = x.UnitPrice,
                            TotalAmount = x.AdjustmentQuantity * x.UnitPrice

                        }).ToList();

                    //await _stockAdjustmentManagementService.UpdateStockAdjustmentAsync(stockAdjustment);

                    return RedirectToAction(nameof(StockAdjustmentList));
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Exception Occured:", ex);
                }
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
