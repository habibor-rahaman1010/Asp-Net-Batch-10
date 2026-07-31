using AutoMapper;
using DevSkill.Inventory.Application.Services;
using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain.Entities.StockTransferEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Infrastructure.RazorUtility;
using DevSkill.Inventory.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Policy = "AdminOnly")]
    public class StockTransferController : Controller
    {
        private readonly IBusinessLocationManagementService _businessLocationManagementService;
        private readonly IUnitManagementService _unitManagementService;
        private readonly IProductManagementService _productManagementService;
        private readonly IStockTransferManagementService _stockTransferManagementService;
        private readonly IMapper _mapper;
        private readonly ILogger<StockTransferController> _logger;

        public StockTransferController(IBusinessLocationManagementService businessLocationManagementService,
            IProductManagementService productManagementService,
            IStockTransferManagementService stockAdjustmentManagementService,
            IMapper mapper,
            ILogger<StockTransferController> logger,
            IUnitManagementService unitManagementService)
        {
            _unitManagementService = unitManagementService;
            _productManagementService = productManagementService;
            _stockTransferManagementService = stockAdjustmentManagementService;
            _mapper = mapper;
            _logger = logger;
            _businessLocationManagementService = businessLocationManagementService;
        }

        public async Task<IActionResult> CreateStockTransfer()
        {
            try
            {
                var model = new StockTransferCreateModel();
                model.TransferNo = GenerateTransferNo();
                model.SetFromWarehouseValues(await _businessLocationManagementService.GetAllBusinessLocationAsync());
                model.SetToWarehouseValues(await _businessLocationManagementService.GetAllBusinessLocationAsync());

                foreach (var item in model.StockTransferItems)
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

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> CreateStockTransfer(StockTransferCreateModel model)
        {
            try
            {
                Guid StockTransferId = Guid.NewGuid();

                var stockTransfer = new StockTransfer
                {
                    Id = StockTransferId,
                    TransferNo = GenerateTransferNo(),
                    TransferDate = DateTime.Now,
                    FromWarehouseId = model.FromWarehouseId,
                    ToWarehouseId = model.ToWarehouseId,
                    Remarks = model.Remarks,
                    Status = StockTransferStatus.Pending,

                    StockTransferItems = model.StockTransferItems
                    .Where(x => x.ProductId != Guid.Empty && x.Quantity > 0)
                    .Select(x =>
                        new StockTransferItem
                        {
                            Id = Guid.NewGuid(),
                            StockTransferId = StockTransferId,
                            ProductId = x.ProductId,
                            Quantity = x.Quantity

                        }).ToList()
                };
                await _stockTransferManagementService.CreateStockTransferAsync(stockTransfer);
                return RedirectToAction(nameof(GetStockTransferList));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public IActionResult GetStockTransferList()
        {
            return View();
        }

        
        public async Task<JsonResult> GetStockTransferJsonData([FromBody] StockTransferListModel model)
        {
            var result = await _stockTransferManagementService.GetStockTransferListAsync(model.PageIndex, model.PageSize, model.Search,
                model.FormatSortExpression(
                "StockTransfer.TransferNo",
                "Product.ProductName",
                "StockTransfer.FromWarehouse.LocationName",
                "StockTransfer.ToWarehouse.LocationName",
                "Quantity",
                "StockTransfer.TransferDate",
                "StockTransfer.Status",
                "StockTransfer.Remarks",
                "Id"));

            var stockTransferJsonData = new
            {
                recordsTotal = result.total,
                recordsFiltered = result.totalDisplay,
                data = (from record in result.data
                        select new string[]
                        {
                            HttpUtility.HtmlEncode(record.StockTransfer.TransferNo),
                            HttpUtility.HtmlEncode(record.Product.ProductName),
                            HttpUtility.HtmlEncode(record.StockTransfer.FromWarehouse.LocationName),
                            HttpUtility.HtmlEncode(record.StockTransfer.ToWarehouse.LocationName),
                            HttpUtility.HtmlEncode(record.Quantity),
                            HttpUtility.HtmlEncode(record.StockTransfer.TransferDate),
                            HttpUtility.HtmlEncode(record.StockTransfer.Status),
                            HttpUtility.HtmlEncode(record.StockTransfer.Remarks),
                            HttpUtility.HtmlEncode(record.StockTransferId.ToString())
                        }
                    ).ToArray()
            };

            return Json(stockTransferJsonData);
        }

        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> UpdateStockTransfer(Guid id)
        {
            var stockTransfer = await _stockTransferManagementService.GetStockTransferByIdAsync(id);

            if (stockTransfer == null)
            {
                return NotFound();
            }

            var model = new StockTransferUpdateModel
            {
                Id = stockTransfer.Id,
                FromWarehouseId = stockTransfer.FromWarehouseId,
                ToWarehouseId = stockTransfer.ToWarehouseId,
                TransferNo = stockTransfer.TransferNo,
                TransferDate = stockTransfer.TransferDate,
                StockTransferStatus = stockTransfer.Status,
                Remarks = stockTransfer.Remarks,
                StockTransferItems = stockTransfer.StockTransferItems.Select(x => new StockTransferItemModel
                {
                    ProductId = x.ProductId,
                    Quantity = x.Quantity,
                    UnitId = x.Product.UnitId

                }).ToList()
            };

            model.SetFromWarehouseValues(await _businessLocationManagementService.GetAllBusinessLocationAsync());

            model.SetToWarehouseValues(await _businessLocationManagementService.GetAllBusinessLocationAsync());

            model.StockTransferStatuses = Utility.ConvertEnumToSelectList<StockTransferStatus>();

            foreach (var item in model.StockTransferItems)
            {
                item.SetUnitValues(await _unitManagementService.GetAllUnitAsync());

                item.SetProductValues((await _productManagementService.GetAllProductByWarehouseAsync(model.FromWarehouseId)).ToList());
            }

            model.SetFromWarehouseValues(await _businessLocationManagementService.GetAllBusinessLocationAsync());

            model.SetToWarehouseValues(await _businessLocationManagementService.GetAllBusinessLocationAsync());

            model.StockTransferStatuses = Utility.ConvertEnumToSelectList<StockTransferStatus>();

            foreach (var item in model.StockTransferItems)
            {
                item.SetUnitValues(await _unitManagementService.GetAllUnitAsync());
                item.SetProductValues((await _productManagementService.GetAllProductByWarehouseAsync(stockTransfer.FromWarehouse.Id)).ToList());
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> UpdateStockTransfer(StockTransferUpdateModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    model.SetFromWarehouseValues(await _businessLocationManagementService.GetAllBusinessLocationAsync());
                    model.SetToWarehouseValues(await _businessLocationManagementService.GetAllBusinessLocationAsync());
                    model.StockTransferStatuses = Utility.ConvertEnumToSelectList<StockTransferStatus>();

                    var products = (await _productManagementService.GetAllProductByWarehouseAsync(model.FromWarehouseId)).ToList();
                    var units = (await _unitManagementService.GetAllUnitAsync()).ToList();

                    foreach (var item in model.StockTransferItems)
                    {
                        item.SetUnitValues(units);
                        item.SetProductValues(products);
                    }

                    return View(model);
                }

                var stockTransfer = new StockTransfer
                {
                    Id = model.Id,
                    FromWarehouseId = model.FromWarehouseId,
                    ToWarehouseId = model.ToWarehouseId,
                    TransferNo = model.TransferNo,
                    TransferDate = model.TransferDate,
                    Status = model.StockTransferStatus,
                    Remarks = model.Remarks,

                    StockTransferItems = model.StockTransferItems
                        .Where(x => x.ProductId != Guid.Empty && x.Quantity > 0)
                        .Select(x => new StockTransferItem
                        {
                            Id = Guid.NewGuid(),
                            StockTransferId = model.Id,
                            ProductId = x.ProductId,
                            Quantity = x.Quantity

                        }).ToList()
                };

                await _stockTransferManagementService.UpdateStockTransferAsync(stockTransfer);

                return RedirectToAction(nameof(GetStockTransferList));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Exception Occurred: ", ex);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AdminOnly")]
        public async Task<JsonResult> DeleteStockTransferAsync(Guid id)
        {
            try
            {
                await _stockTransferManagementService.DeleteStockTransferAsync(id);

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
        public async Task<IActionResult> GetStockTransferDetails(Guid id)
        {
            var stockTransfer = await _stockTransferManagementService
                .GetStockTransferByIdAsync(id);

            if (stockTransfer == null)
            {
                return NotFound();
            }

            var result = new
            {
                id = stockTransfer.Id,
                transferNo = stockTransfer.TransferNo,
                transferDate = stockTransfer.TransferDate,
                remarks = stockTransfer.Remarks,
                status = stockTransfer.Status.ToString(),

                fromWarehouse = new
                {
                    id = stockTransfer.FromWarehouseId,
                    locationName = stockTransfer.FromWarehouse?.LocationName
                },

                toWarehouse = new
                {
                    id = stockTransfer.ToWarehouseId,
                    locationName = stockTransfer.ToWarehouse?.LocationName
                },

                totalQuantity = stockTransfer.StockTransferItems?.Sum(x => x.Quantity) ?? 0,
                totalAmount = stockTransfer.StockTransferItems?.Sum(x => x.Quantity * (int)(x.Product?.Price ?? 0)) ?? 0,

                stockTransferItems = stockTransfer.StockTransferItems?
                    .Select(x => new
                    {
                        id = x.Id,
                        productId = x.ProductId,
                        productName = x.Product?.ProductName,
                        quantity = x.Quantity,
                        unitPrice = x.Product?.Price ?? 0,
                        subtotal = x.Quantity * (int)(x.Product?.Price ?? 0)

                    }).ToList()
            };

            return Json(result);
        }

        private string GenerateTransferNo()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var random = new Random();

            string letters = new string(Enumerable.Repeat(chars, 6)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            return $"TR-{letters}-{DateTime.Now:dd-MM-yyyy}";
        }
    }
}
