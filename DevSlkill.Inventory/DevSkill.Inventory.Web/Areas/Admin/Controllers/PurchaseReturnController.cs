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
    public class PurchaseReturnController : Controller
    {
        private readonly IPurchaseReturnManagementService _purchaseReturnManagementService;
        private readonly IGoodsReceiptManagementService _goodsReceiptManagementService;
        private readonly INotificationService _notificationService;
        private readonly ILogger<PurchaseReturnController> _logger;

        public PurchaseReturnController(
            IPurchaseReturnManagementService purchaseReturnManagementService,
            IGoodsReceiptManagementService goodsReceiptManagementService,
            ILogger<PurchaseReturnController> logger,
            INotificationService notificationService)
        {
            _purchaseReturnManagementService = purchaseReturnManagementService;
            _goodsReceiptManagementService = goodsReceiptManagementService;
            _logger = logger;
            _notificationService = notificationService;
        }

        [Authorize(Policy = "ReadPermission")]
        public IActionResult PurchaseReturnList()
        {
            return View();
        }

        [HttpPost, Authorize(Policy = "ReadPermission")]
        public async Task<JsonResult> GetPurchaseReturnJsonData([FromBody] PurchaseReturnListModel model)
        {
            var result = await _purchaseReturnManagementService.GetPurchaseReturnsAsync(
                model.PageIndex, model.PageSize, model.Search,
                model.FormatSortExpression(
                    "ReturnNo",
                    "GoodsReceipt.GoodsReceiptNo",
                    "Supplier.SupplierName",
                    "ReturnDate",
                    "BusinessLocation.LocationName",
                    "TotalAmount",
                    "Status",
                    "Id"));

            var jsonData = new
            {
                recordsTotal = result.total,
                recordsFiltered = result.totalDisplay,
                data = (from record in result.data
                        select new string[]
                        {
                            HttpUtility.HtmlEncode(record.ReturnNo),
                            HttpUtility.HtmlEncode(record.GoodsReceipt?.GoodsReceiptNo),
                            HttpUtility.HtmlEncode(record.Supplier?.SupplierName),
                            HttpUtility.HtmlEncode(record.ReturnDate.ToString("dd-MM-yyyy")),
                            HttpUtility.HtmlEncode(record.BusinessLocation?.LocationName),
                            HttpUtility.HtmlEncode(record.TotalAmount.ToString("N2")),
                            HttpUtility.HtmlEncode(record.Status.ToString()),
                            HttpUtility.HtmlEncode(record.Id.ToString())
                        }
                    ).ToArray()
            };

            return Json(jsonData);
        }

        [Authorize(Policy = "CreatePermission")]
        public async Task<IActionResult> CreatePurchaseReturn(Guid? goodsReceiptId)
        {
            var model = new PurchaseReturnCreateModel
            {
                ReturnNo = await _purchaseReturnManagementService.GenerateReturnNoAsync()
            };

            // Coming in from the goods receipt list pre-fills the grid with whatever
            // that receipt still has left to return.
            if (goodsReceiptId.HasValue && goodsReceiptId.Value != Guid.Empty)
            {
                model.GoodsReceiptId = goodsReceiptId.Value;
                await FillLinesAsync(model);
            }

            await PopulateAsync(model);

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "CreatePermission")]
        public async Task<IActionResult> CreatePurchaseReturn(PurchaseReturnCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateAsync(model);
                return View(model);
            }

            try
            {
                var purchaseReturn = new PurchaseReturn
                {
                    GoodsReceiptId = model.GoodsReceiptId,
                    ReturnDate = model.ReturnDate,
                    Reason = model.Reason,
                    Notes = model.Notes,
                    Status = model.Status,
                    CreatedBy = User.Identity?.Name ?? string.Empty,
                    PurchaseReturnItems = ToLineRequests(model.PurchaseReturnItems)
                };

                var purchaseReturnId = await _purchaseReturnManagementService
                    .CreatePurchaseReturnAsync(purchaseReturn);

                await _notificationService.RaiseAsync(NotificationEvent.PurchaseReturnCreated,
                    "Purchase return raised",
                    $"{model.PurchaseReturnItems.Count} line(s) are going back to a supplier.",
                    $"/Admin/PurchaseReturn/UpdatePurchaseReturn/{purchaseReturnId}",
                    purchaseReturnId, User.Identity?.Name);

                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = model.Status == PurchaseReturnStatus.Returned
                        ? "Goods returned to the supplier and stock updated successfully."
                        : "Purchase return saved as draft. Stock changes only when it is confirmed.",
                    Type = ResponseTypes.Success
                });

                return RedirectToAction(nameof(PurchaseReturnList));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                await PopulateAsync(model);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Purchase return creation failed.");
                ModelState.AddModelError(string.Empty, "Purchase return creation failed.");
                await PopulateAsync(model);
                return View(model);
            }
        }

        [Authorize(Policy = "UpdatePermission")]
        public async Task<IActionResult> UpdatePurchaseReturn(Guid id)
        {
            var purchaseReturn = await _purchaseReturnManagementService.GetPurchaseReturnByIdAsync(id);

            if (purchaseReturn == null)
            {
                return NotFound();
            }

            if (purchaseReturn.Status != PurchaseReturnStatus.Draft)
            {
                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = $"A {purchaseReturn.Status.ToString().ToLower()} purchase return cannot be edited.",
                    Type = ResponseTypes.Danger
                });

                return RedirectToAction(nameof(PurchaseReturnList));
            }

            var model = new PurchaseReturnUpdateModel
            {
                Id = purchaseReturn.Id,
                ReturnNo = purchaseReturn.ReturnNo,
                GoodsReceiptId = purchaseReturn.GoodsReceiptId,
                GoodsReceiptNo = purchaseReturn.GoodsReceipt?.GoodsReceiptNo ?? string.Empty,
                ReturnDate = purchaseReturn.ReturnDate,
                Reason = purchaseReturn.Reason,
                Notes = purchaseReturn.Notes,
                Status = purchaseReturn.Status,
                SupplierName = purchaseReturn.Supplier?.SupplierName ?? string.Empty,
                WarehouseName = purchaseReturn.BusinessLocation?.LocationName ?? string.Empty
            };

            model.PurchaseReturnItems = await BuildEditableLinesAsync(purchaseReturn);

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<IActionResult> UpdatePurchaseReturn(PurchaseReturnUpdateModel model)
        {
            var existing = await _purchaseReturnManagementService.GetPurchaseReturnByIdAsync(model.Id);

            if (existing == null)
            {
                return NotFound();
            }

            // A return never moves to another goods receipt, so the posted value is
            // discarded here as well.
            model.GoodsReceiptId = existing.GoodsReceiptId;
            model.GoodsReceiptNo = existing.GoodsReceipt?.GoodsReceiptNo ?? string.Empty;
            model.SupplierName = existing.Supplier?.SupplierName ?? string.Empty;
            model.WarehouseName = existing.BusinessLocation?.LocationName ?? string.Empty;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var purchaseReturn = new PurchaseReturn
                {
                    Id = model.Id,
                    GoodsReceiptId = model.GoodsReceiptId,
                    ReturnDate = model.ReturnDate,
                    Reason = model.Reason,
                    Notes = model.Notes,
                    Status = model.Status,
                    PurchaseReturnItems = ToLineRequests(model.PurchaseReturnItems)
                };

                await _purchaseReturnManagementService.UpdatePurchaseReturnAsync(purchaseReturn);

                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = model.Status == PurchaseReturnStatus.Returned
                        ? "Goods returned to the supplier and stock updated successfully."
                        : "Purchase return updated successfully.",
                    Type = ResponseTypes.Success
                });

                return RedirectToAction(nameof(PurchaseReturnList));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Purchase return update failed.");
                ModelState.AddModelError(string.Empty, "Purchase return update failed.");
                return View(model);
            }
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<JsonResult> ConfirmPurchaseReturn(Guid id)
        {
            return await RunAsync(
                () => _purchaseReturnManagementService.ConfirmPurchaseReturnAsync(id),
                "Goods returned to the supplier and stock updated successfully.",
                "Purchase return confirmation failed.");
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<JsonResult> CancelPurchaseReturn(Guid id)
        {
            return await RunAsync(
                () => _purchaseReturnManagementService.CancelPurchaseReturnAsync(id),
                "Purchase return cancelled and any stock it sent back was brought in again.",
                "Purchase return cancel failed.");
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "DeletePermission")]
        public async Task<JsonResult> DeletePurchaseReturn(Guid id)
        {
            return await RunAsync(
                () => _purchaseReturnManagementService.DeletePurchaseReturnAsync(id),
                "Purchase return deleted successfully.",
                "Purchase return delete failed.");
        }

        [HttpGet, Authorize(Policy = "ReadPermission")]
        public async Task<IActionResult> GetPurchaseReturnDetails(Guid id)
        {
            var purchaseReturn = await _purchaseReturnManagementService.GetPurchaseReturnByIdAsync(id);

            if (purchaseReturn == null)
            {
                return NotFound();
            }

            var result = new
            {
                id = purchaseReturn.Id,
                returnNo = purchaseReturn.ReturnNo,
                goodsReceiptNo = purchaseReturn.GoodsReceipt?.GoodsReceiptNo,
                supplierName = purchaseReturn.Supplier?.SupplierName,
                warehouse = purchaseReturn.BusinessLocation?.LocationName,
                returnDate = purchaseReturn.ReturnDate,
                reason = purchaseReturn.Reason,
                status = purchaseReturn.Status.ToString(),
                notes = purchaseReturn.Notes,
                totalAmount = purchaseReturn.TotalAmount,
                purchaseReturnItems = purchaseReturn.PurchaseReturnItems?.Select(x => new
                {
                    productName = x.Product?.ProductName,
                    unitName = x.Product?.Unit?.UnitName,
                    receivedQuantity = x.ReceivedQuantity,
                    returnQuantity = x.ReturnQuantity,
                    unitPrice = x.UnitPrice,
                    lineTotal = x.LineTotal,
                    remarks = x.Remarks
                }).ToList()
            };

            return Json(result);
        }

        /// <summary>
        /// The lines of a goods receipt that may still go back, so the return screen
        /// can build its grid as soon as a receipt is chosen.
        /// </summary>
        [HttpGet, Authorize(Policy = "ReadPermission")]
        public async Task<JsonResult> GetReturnableLines(Guid goodsReceiptId)
        {
            try
            {
                var goodsReceipt = await _goodsReceiptManagementService.GetGoodsReceiptByIdAsync(goodsReceiptId);

                if (goodsReceipt == null)
                {
                    return Json(new { success = false, message = "Goods receipt not found." });
                }

                var lines = await _purchaseReturnManagementService.GetReturnableLinesAsync(goodsReceiptId);

                return Json(new
                {
                    success = true,
                    supplierName = goodsReceipt.Supplier?.SupplierName,
                    warehouseName = goodsReceipt.BusinessLocation?.LocationName,
                    receiptDate = goodsReceipt.ReceiptDate,
                    lines = lines.Select(x => new
                    {
                        productId = x.ProductId,
                        productName = x.ProductName,
                        unitName = x.UnitName,
                        receivedQuantity = x.ReceivedQuantity,
                        returnedQuantity = x.ReturnedQuantity,
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
                _logger.LogError(ex, "Loading returnable lines failed.");
                return Json(new { success = false, message = "The goods receipt lines could not be loaded." });
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
        /// Rebuilds the grid from the receipt. Nothing is defaulted into the return
        /// quantity, because sending everything back is the exception, not the rule.
        /// </summary>
        private async Task FillLinesAsync(PurchaseReturnCreateModel model)
        {
            var goodsReceipt = await _goodsReceiptManagementService.GetGoodsReceiptByIdAsync(model.GoodsReceiptId);

            if (goodsReceipt == null)
            {
                return;
            }

            model.SupplierName = goodsReceipt.Supplier?.SupplierName ?? string.Empty;
            model.WarehouseName = goodsReceipt.BusinessLocation?.LocationName ?? string.Empty;

            var lines = await _purchaseReturnManagementService.GetReturnableLinesAsync(model.GoodsReceiptId);

            model.PurchaseReturnItems = lines.Select(x => new PurchaseReturnItemModel
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                UnitName = x.UnitName,
                ReceivedQuantity = x.ReceivedQuantity,
                AlreadyReturnedQuantity = x.ReturnedQuantity,
                RemainingQuantity = x.RemainingQuantity,
                ReturnQuantity = 0,
                UnitPrice = x.UnitPrice,
                CurrentStock = x.CurrentStock
            }).ToList();
        }

        /// <summary>
        /// The saved lines of a draft return, with the returnable quantity worked out
        /// as if this return were not there.
        /// </summary>
        private async Task<List<PurchaseReturnItemModel>> BuildEditableLinesAsync(PurchaseReturn purchaseReturn)
        {
            var returnable = (await _purchaseReturnManagementService
                .GetReturnableLinesAsync(purchaseReturn.GoodsReceiptId, purchaseReturn.Id))
                .ToDictionary(x => x.ProductId);

            return (purchaseReturn.PurchaseReturnItems ?? new List<PurchaseReturnItem>())
                .Select(item =>
                {
                    returnable.TryGetValue(item.ProductId, out var line);

                    return new PurchaseReturnItemModel
                    {
                        ProductId = item.ProductId,
                        ProductName = item.Product?.ProductName ?? string.Empty,
                        UnitName = item.Product?.Unit?.UnitName ?? string.Empty,
                        ReceivedQuantity = item.ReceivedQuantity,
                        AlreadyReturnedQuantity = line?.ReturnedQuantity ?? 0m,
                        RemainingQuantity = line?.RemainingQuantity ?? item.ReturnQuantity,
                        ReturnQuantity = item.ReturnQuantity,
                        UnitPrice = item.UnitPrice,
                        CurrentStock = line?.CurrentStock ?? 0,
                        Remarks = item.Remarks
                    };
                })
                .ToList();
        }

        private static List<PurchaseReturnItem> ToLineRequests(IEnumerable<PurchaseReturnItemModel> items)
        {
            return items
                .Where(x => x.ProductId != Guid.Empty && x.ReturnQuantity > 0)
                .Select(x => new PurchaseReturnItem
                {
                    ProductId = x.ProductId,
                    ReturnQuantity = x.ReturnQuantity,
                    Remarks = x.Remarks ?? string.Empty
                })
                .ToList();
        }

        private async Task PopulateAsync(PurchaseReturnCreateModel model)
        {
            if (string.IsNullOrWhiteSpace(model.ReturnNo))
            {
                model.ReturnNo = await _purchaseReturnManagementService.GenerateReturnNoAsync();
            }

            // Only a received goods receipt holds stock that can be sent back.
            model.SetGoodsReceiptValues(
                await _purchaseReturnManagementService.GetReturnableGoodsReceiptsAsync(GoodsReceiptStatus.Received));
        }
    }
}
