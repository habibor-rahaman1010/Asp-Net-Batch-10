using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Infrastructure;
using DevSkill.Inventory.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class GoodsReceiptController : Controller
    {
        private readonly IGoodsReceiptManagementService _goodsReceiptManagementService;
        private readonly IPurchaseOrderManagementService _purchaseOrderManagementService;
        private readonly ILogger<GoodsReceiptController> _logger;

        public GoodsReceiptController(
            IGoodsReceiptManagementService goodsReceiptManagementService,
            IPurchaseOrderManagementService purchaseOrderManagementService,
            ILogger<GoodsReceiptController> logger)
        {
            _goodsReceiptManagementService = goodsReceiptManagementService;
            _purchaseOrderManagementService = purchaseOrderManagementService;
            _logger = logger;
        }

        [Authorize(Policy = "ReadPermission")]
        public IActionResult GoodsReceiptList()
        {
            return View();
        }

        [HttpPost, Authorize(Policy = "ReadPermission")]
        public async Task<JsonResult> GetGoodsReceiptJsonData([FromBody] GoodsReceiptListModel model)
        {
            var result = await _goodsReceiptManagementService.GetGoodsReceiptsAsync(
                model.PageIndex, model.PageSize, model.Search,
                model.FormatSortExpression(
                    "GoodsReceiptNo",
                    "PurchaseOrder.PurchaseOrderNo",
                    "Supplier.SupplierName",
                    "ReceiptDate",
                    "BusinessLocation.LocationName",
                    "SupplierChallanNo",
                    "Status",
                    "Id"));

            var jsonData = new
            {
                recordsTotal = result.total,
                recordsFiltered = result.totalDisplay,
                data = (from record in result.data
                        select new string[]
                        {
                            HttpUtility.HtmlEncode(record.GoodsReceiptNo),
                            HttpUtility.HtmlEncode(record.PurchaseOrder?.PurchaseOrderNo),
                            HttpUtility.HtmlEncode(record.Supplier?.SupplierName),
                            HttpUtility.HtmlEncode(record.ReceiptDate.ToString("dd-MM-yyyy")),
                            HttpUtility.HtmlEncode(record.BusinessLocation?.LocationName),
                            HttpUtility.HtmlEncode(record.SupplierChallanNo),
                            HttpUtility.HtmlEncode(record.Status.ToString()),
                            HttpUtility.HtmlEncode(record.Id.ToString())
                        }
                    ).ToArray()
            };

            return Json(jsonData);
        }

        [Authorize(Policy = "CreatePermission")]
        public async Task<IActionResult> CreateGoodsReceipt(Guid? purchaseOrderId)
        {
            var model = new GoodsReceiptCreateModel
            {
                GoodsReceiptNo = await _goodsReceiptManagementService.GenerateGoodsReceiptNoAsync(),
                ReceivedBy = User.Identity?.Name ?? string.Empty
            };

            // Coming in from the purchase order list pre-fills the grid with whatever
            // that order still has outstanding.
            if (purchaseOrderId.HasValue && purchaseOrderId.Value != Guid.Empty)
            {
                model.PurchaseOrderId = purchaseOrderId.Value;
                await FillLinesAsync(model);
            }

            await PopulateAsync(model);

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "CreatePermission")]
        public async Task<IActionResult> CreateGoodsReceipt(GoodsReceiptCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateAsync(model);
                return View(model);
            }

            try
            {
                var goodsReceipt = new GoodsReceipt
                {
                    PurchaseOrderId = model.PurchaseOrderId,
                    ReceiptDate = model.ReceiptDate,
                    SupplierChallanNo = model.SupplierChallanNo,
                    ReceivedBy = model.ReceivedBy,
                    Notes = model.Notes,
                    Status = model.Status,
                    CreatedBy = User.Identity?.Name ?? string.Empty,
                    GoodsReceiptItems = ToLineRequests(model.GoodsReceiptItems)
                };

                await _goodsReceiptManagementService.CreateGoodsReceiptAsync(goodsReceipt);

                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = model.Status == GoodsReceiptStatus.Received
                        ? "Goods received and stock updated successfully."
                        : "Goods receipt saved as draft. Stock changes only when it is received.",
                    Type = ResponseTypes.Success
                });

                return RedirectToAction(nameof(GoodsReceiptList));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                await PopulateAsync(model);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Goods receipt creation failed.");
                ModelState.AddModelError(string.Empty, "Goods receipt creation failed.");
                await PopulateAsync(model);
                return View(model);
            }
        }

        [Authorize(Policy = "UpdatePermission")]
        public async Task<IActionResult> UpdateGoodsReceipt(Guid id)
        {
            var goodsReceipt = await _goodsReceiptManagementService.GetGoodsReceiptByIdAsync(id);

            if (goodsReceipt == null)
            {
                return NotFound();
            }

            if (goodsReceipt.Status != GoodsReceiptStatus.Draft)
            {
                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = $"A {goodsReceipt.Status.ToString().ToLower()} goods receipt cannot be edited.",
                    Type = ResponseTypes.Danger
                });

                return RedirectToAction(nameof(GoodsReceiptList));
            }

            var model = new GoodsReceiptUpdateModel
            {
                Id = goodsReceipt.Id,
                GoodsReceiptNo = goodsReceipt.GoodsReceiptNo,
                PurchaseOrderId = goodsReceipt.PurchaseOrderId,
                PurchaseOrderNo = goodsReceipt.PurchaseOrder?.PurchaseOrderNo ?? string.Empty,
                ReceiptDate = goodsReceipt.ReceiptDate,
                SupplierChallanNo = goodsReceipt.SupplierChallanNo,
                ReceivedBy = goodsReceipt.ReceivedBy,
                Notes = goodsReceipt.Notes,
                Status = goodsReceipt.Status,
                SupplierName = goodsReceipt.Supplier?.SupplierName ?? string.Empty,
                WarehouseName = goodsReceipt.BusinessLocation?.LocationName ?? string.Empty
            };

            model.GoodsReceiptItems = await BuildEditableLinesAsync(goodsReceipt);

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<IActionResult> UpdateGoodsReceipt(GoodsReceiptUpdateModel model)
        {
            var existing = await _goodsReceiptManagementService.GetGoodsReceiptByIdAsync(model.Id);

            if (existing == null)
            {
                return NotFound();
            }

            // A receipt never moves to another purchase order, so the posted value is
            // discarded here as well.
            model.PurchaseOrderId = existing.PurchaseOrderId;
            model.PurchaseOrderNo = existing.PurchaseOrder?.PurchaseOrderNo ?? string.Empty;
            model.SupplierName = existing.Supplier?.SupplierName ?? string.Empty;
            model.WarehouseName = existing.BusinessLocation?.LocationName ?? string.Empty;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var goodsReceipt = new GoodsReceipt
                {
                    Id = model.Id,
                    PurchaseOrderId = model.PurchaseOrderId,
                    ReceiptDate = model.ReceiptDate,
                    SupplierChallanNo = model.SupplierChallanNo,
                    ReceivedBy = model.ReceivedBy,
                    Notes = model.Notes,
                    Status = model.Status,
                    GoodsReceiptItems = ToLineRequests(model.GoodsReceiptItems)
                };

                await _goodsReceiptManagementService.UpdateGoodsReceiptAsync(goodsReceipt);

                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = model.Status == GoodsReceiptStatus.Received
                        ? "Goods received and stock updated successfully."
                        : "Goods receipt updated successfully.",
                    Type = ResponseTypes.Success
                });

                return RedirectToAction(nameof(GoodsReceiptList));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Goods receipt update failed.");
                ModelState.AddModelError(string.Empty, "Goods receipt update failed.");
                return View(model);
            }
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<JsonResult> ConfirmGoodsReceipt(Guid id)
        {
            return await RunAsync(
                () => _goodsReceiptManagementService.ConfirmGoodsReceiptAsync(id),
                "Goods received and stock updated successfully.",
                "Goods receipt confirmation failed.");
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<JsonResult> CancelGoodsReceipt(Guid id)
        {
            return await RunAsync(
                () => _goodsReceiptManagementService.CancelGoodsReceiptAsync(id),
                "Goods receipt cancelled and any stock it brought in was returned.",
                "Goods receipt cancel failed.");
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "DeletePermission")]
        public async Task<JsonResult> DeleteGoodsReceipt(Guid id)
        {
            return await RunAsync(
                () => _goodsReceiptManagementService.DeleteGoodsReceiptAsync(id),
                "Goods receipt deleted successfully.",
                "Goods receipt delete failed.");
        }

        [HttpGet, Authorize(Policy = "ReadPermission")]
        public async Task<IActionResult> GetGoodsReceiptDetails(Guid id)
        {
            var goodsReceipt = await _goodsReceiptManagementService.GetGoodsReceiptByIdAsync(id);

            if (goodsReceipt == null)
            {
                return NotFound();
            }

            var result = new
            {
                id = goodsReceipt.Id,
                goodsReceiptNo = goodsReceipt.GoodsReceiptNo,
                purchaseOrderNo = goodsReceipt.PurchaseOrder?.PurchaseOrderNo,
                supplierName = goodsReceipt.Supplier?.SupplierName,
                warehouse = goodsReceipt.BusinessLocation?.LocationName,
                receiptDate = goodsReceipt.ReceiptDate,
                supplierChallanNo = goodsReceipt.SupplierChallanNo,
                receivedBy = goodsReceipt.ReceivedBy,
                status = goodsReceipt.Status.ToString(),
                notes = goodsReceipt.Notes,
                totalValue = goodsReceipt.GoodsReceiptItems?.Sum(x => x.ReceivedQuantity * x.UnitPrice) ?? 0m,
                goodsReceiptItems = goodsReceipt.GoodsReceiptItems?.Select(x => new
                {
                    productName = x.Product?.ProductName,
                    unitName = x.Product?.Unit?.UnitName,
                    orderedQuantity = x.OrderedQuantity,
                    receivedQuantity = x.ReceivedQuantity,
                    rejectedQuantity = x.RejectedQuantity,
                    returnedQuantity = x.ReturnedQuantity,
                    unitPrice = x.UnitPrice,
                    lineValue = x.ReceivedQuantity * x.UnitPrice,
                    remarks = x.Remarks
                }).ToList()
            };

            return Json(result);
        }

        /// <summary>
        /// The still open lines of a purchase order, so the receiving screen can build
        /// its grid as soon as an order is chosen.
        /// </summary>
        [HttpGet, Authorize(Policy = "ReadPermission")]
        public async Task<JsonResult> GetReceivableLines(Guid purchaseOrderId)
        {
            try
            {
                var purchaseOrder = await _purchaseOrderManagementService.GetPurchaseOrderByIdAsync(purchaseOrderId);

                if (purchaseOrder == null)
                {
                    return Json(new { success = false, message = "Purchase order not found." });
                }

                var lines = await _goodsReceiptManagementService.GetReceivableLinesAsync(purchaseOrderId);

                return Json(new
                {
                    success = true,
                    supplierName = purchaseOrder.Supplier?.SupplierName,
                    warehouseName = purchaseOrder.BusinessLocation?.LocationName,
                    lines = lines.Select(x => new
                    {
                        productId = x.ProductId,
                        productName = x.ProductName,
                        unitName = x.UnitName,
                        orderedQuantity = x.OrderedQuantity,
                        receivedQuantity = x.ReceivedQuantity,
                        remainingQuantity = x.RemainingQuantity,
                        unitPrice = x.UnitPrice,
                        currentStock = x.CurrentStock
                    })
                });
            }
            catch (InvalidOperationException ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Loading receivable lines failed.");
                return Json(new { success = false, message = "The purchase order lines could not be loaded." });
            }
        }

        private async Task<JsonResult> RunAsync(Func<Task> action, string successMessage, string failureMessage)
        {
            try
            {
                await action();

                return Json(new { success = true, message = successMessage });
            }
            catch (InvalidOperationException ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, failureMessage);
                return Json(new { success = false, message = failureMessage });
            }
        }

        /// <summary>
        /// Rebuilds the grid from the order, defaulting each line to everything that
        /// is still outstanding.
        /// </summary>
        private async Task FillLinesAsync(GoodsReceiptCreateModel model)
        {
            var purchaseOrder = await _purchaseOrderManagementService.GetPurchaseOrderByIdAsync(model.PurchaseOrderId);

            if (purchaseOrder == null)
            {
                return;
            }

            model.SupplierName = purchaseOrder.Supplier?.SupplierName ?? string.Empty;
            model.WarehouseName = purchaseOrder.BusinessLocation?.LocationName ?? string.Empty;

            var lines = await _goodsReceiptManagementService.GetReceivableLinesAsync(model.PurchaseOrderId);

            model.GoodsReceiptItems = lines.Select(x => new GoodsReceiptItemModel
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                UnitName = x.UnitName,
                OrderedQuantity = x.OrderedQuantity,
                AlreadyReceivedQuantity = x.ReceivedQuantity,
                RemainingQuantity = x.RemainingQuantity,
                ReceivedQuantity = x.RemainingQuantity,
                UnitPrice = x.UnitPrice,
                CurrentStock = x.CurrentStock
            }).ToList();
        }

        /// <summary>
        /// The saved lines of a draft receipt, with the remaining quantity worked out
        /// as if this receipt were not there.
        /// </summary>
        private async Task<List<GoodsReceiptItemModel>> BuildEditableLinesAsync(GoodsReceipt goodsReceipt)
        {
            var receivable = (await _goodsReceiptManagementService
                .GetReceivableLinesAsync(goodsReceipt.PurchaseOrderId, goodsReceipt.Id))
                .ToDictionary(x => x.ProductId);

            return (goodsReceipt.GoodsReceiptItems ?? new List<GoodsReceiptItem>())
                .Select(item =>
                {
                    receivable.TryGetValue(item.ProductId, out var line);

                    return new GoodsReceiptItemModel
                    {
                        ProductId = item.ProductId,
                        ProductName = item.Product?.ProductName ?? string.Empty,
                        UnitName = item.Product?.Unit?.UnitName ?? string.Empty,
                        OrderedQuantity = item.OrderedQuantity,
                        AlreadyReceivedQuantity = line?.ReceivedQuantity ?? 0m,
                        RemainingQuantity = line?.RemainingQuantity ?? item.ReceivedQuantity,
                        ReceivedQuantity = item.ReceivedQuantity,
                        RejectedQuantity = item.RejectedQuantity,
                        UnitPrice = item.UnitPrice,
                        CurrentStock = line?.CurrentStock ?? 0,
                        Remarks = item.Remarks
                    };
                })
                .ToList();
        }

        private static List<GoodsReceiptItem> ToLineRequests(IEnumerable<GoodsReceiptItemModel> items)
        {
            return items
                .Where(x => x.ProductId != Guid.Empty && (x.ReceivedQuantity > 0 || x.RejectedQuantity > 0))
                .Select(x => new GoodsReceiptItem
                {
                    ProductId = x.ProductId,
                    ReceivedQuantity = x.ReceivedQuantity,
                    RejectedQuantity = x.RejectedQuantity,
                    Remarks = x.Remarks ?? string.Empty
                })
                .ToList();
        }

        private async Task PopulateAsync(GoodsReceiptCreateModel model)
        {
            if (string.IsNullOrWhiteSpace(model.GoodsReceiptNo))
            {
                model.GoodsReceiptNo = await _goodsReceiptManagementService.GenerateGoodsReceiptNoAsync();
            }

            // Only an approved order that still has something outstanding can be
            // received against.
            model.SetPurchaseOrderValues(
                await _purchaseOrderManagementService.GetPurchaseOrdersByStatusAsync(
                    PurchaseOrderStatus.Approved, PurchaseOrderStatus.PartiallyReceived));
        }
    }
}
