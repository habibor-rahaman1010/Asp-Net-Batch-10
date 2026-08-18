using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Infrastructure;
using DevSkill.Inventory.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Web;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class PurchaseInvoiceController : Controller
    {
        private readonly IPurchaseInvoiceManagementService _purchaseInvoiceManagementService;
        private readonly IPurchaseOrderManagementService _purchaseOrderManagementService;
        private readonly IGoodsReceiptManagementService _goodsReceiptManagementService;
        private readonly IPaymentTermManagementService _paymentTermManagementService;
        private readonly INotificationService _notificationService;
        private readonly ILogger<PurchaseInvoiceController> _logger;

        public PurchaseInvoiceController(
            IPurchaseInvoiceManagementService purchaseInvoiceManagementService,
            IPurchaseOrderManagementService purchaseOrderManagementService,
            IGoodsReceiptManagementService goodsReceiptManagementService,
            IPaymentTermManagementService paymentTermManagementService,
            ILogger<PurchaseInvoiceController> logger,
            INotificationService notificationService)
        {
            _purchaseInvoiceManagementService = purchaseInvoiceManagementService;
            _purchaseOrderManagementService = purchaseOrderManagementService;
            _goodsReceiptManagementService = goodsReceiptManagementService;
            _paymentTermManagementService = paymentTermManagementService;
            _logger = logger;
            _notificationService = notificationService;
        }

        [Authorize(Policy = "ReadPermission")]
        public IActionResult PurchaseInvoiceList()
        {
            return View();
        }

        [HttpPost, Authorize(Policy = "ReadPermission")]
        public async Task<JsonResult> GetPurchaseInvoiceJsonData([FromBody] PurchaseInvoiceListModel model)
        {
            var result = await _purchaseInvoiceManagementService.GetPurchaseInvoicesAsync(
                model.PageIndex, model.PageSize, model.Search,
                model.FormatSortExpression(
                    "InvoiceNo",
                    "SupplierInvoiceNo",
                    "Supplier.SupplierName",
                    "PurchaseOrder.PurchaseOrderNo",
                    "InvoiceDate",
                    "DueDate",
                    "GrandTotal",
                    "DueAmount",
                    "Status",
                    "Id"));

            var jsonData = new
            {
                recordsTotal = result.total,
                recordsFiltered = result.totalDisplay,
                data = (from record in result.data
                        select new string[]
                        {
                            HttpUtility.HtmlEncode(record.InvoiceNo),
                            HttpUtility.HtmlEncode(record.SupplierInvoiceNo),
                            HttpUtility.HtmlEncode(record.Supplier?.SupplierName),
                            HttpUtility.HtmlEncode(record.PurchaseOrder?.PurchaseOrderNo),
                            HttpUtility.HtmlEncode(record.InvoiceDate.ToString("dd-MM-yyyy")),
                            HttpUtility.HtmlEncode(record.DueDate.ToString("dd-MM-yyyy")),
                            HttpUtility.HtmlEncode(record.GrandTotal.ToString("N2")),
                            HttpUtility.HtmlEncode(record.DueAmount.ToString("N2")),
                            HttpUtility.HtmlEncode(record.Status.ToString()),
                            HttpUtility.HtmlEncode(record.Id.ToString())
                        }
                    ).ToArray()
            };

            return Json(jsonData);
        }

        [Authorize(Policy = "CreatePermission")]
        public async Task<IActionResult> CreatePurchaseInvoice(Guid? purchaseOrderId)
        {
            var model = new PurchaseInvoiceCreateModel
            {
                InvoiceNo = await _purchaseInvoiceManagementService.GenerateInvoiceNoAsync()
            };

            if (purchaseOrderId.HasValue && purchaseOrderId.Value != Guid.Empty)
            {
                model.PurchaseOrderId = purchaseOrderId.Value;
                await FillLinesAsync(model);
            }

            await PopulateAsync(model);

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "CreatePermission")]
        public async Task<IActionResult> CreatePurchaseInvoice(PurchaseInvoiceCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateAsync(model);
                return View(model);
            }

            try
            {
                var invoice = new PurchaseInvoice
                {
                    SupplierInvoiceNo = model.SupplierInvoiceNo,
                    PurchaseOrderId = model.PurchaseOrderId,
                    GoodsReceiptId = model.GoodsReceiptId,
                    InvoiceDate = model.InvoiceDate,
                    DueDate = model.DueDate,
                    PaymentTermId = model.PaymentTermId,
                    OtherCharges = model.OtherCharges,
                    Notes = model.Notes,
                    Status = model.Status,
                    CreatedBy = User.Identity?.Name ?? string.Empty,
                    PurchaseInvoiceItems = ToLineRequests(model.PurchaseInvoiceItems)
                };

                var purchaseInvoiceId = await _purchaseInvoiceManagementService
                    .CreatePurchaseInvoiceAsync(invoice);

                await _notificationService.RaiseAsync(NotificationEvent.PurchaseInvoicePosted,
                    "Purchase invoice raised",
                    $"A supplier invoice covering {model.PurchaseInvoiceItems.Count} line(s) has been entered.",
                    $"/Admin/PurchaseInvoice/UpdatePurchaseInvoice/{purchaseInvoiceId}",
                    purchaseInvoiceId, User.Identity?.Name);

                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = model.Status == PurchaseInvoiceStatus.Posted
                        ? "Purchase invoice posted and the supplier balance was updated."
                        : "Purchase invoice saved as draft. The supplier balance changes only when it is posted.",
                    Type = ResponseTypes.Success
                });

                return RedirectToAction(nameof(PurchaseInvoiceList));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                await PopulateAsync(model);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Purchase invoice creation failed.");
                ModelState.AddModelError(string.Empty, "Purchase invoice creation failed.");
                await PopulateAsync(model);
                return View(model);
            }
        }

        [Authorize(Policy = "UpdatePermission")]
        public async Task<IActionResult> UpdatePurchaseInvoice(Guid id)
        {
            var invoice = await _purchaseInvoiceManagementService.GetPurchaseInvoiceByIdAsync(id);

            if (invoice == null)
            {
                return NotFound();
            }

            if (invoice.Status != PurchaseInvoiceStatus.Draft)
            {
                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = $"A {invoice.Status.ToString().ToLower()} purchase invoice cannot be edited.",
                    Type = ResponseTypes.Danger
                });

                return RedirectToAction(nameof(PurchaseInvoiceList));
            }

            var model = new PurchaseInvoiceUpdateModel
            {
                Id = invoice.Id,
                InvoiceNo = invoice.InvoiceNo,
                SupplierInvoiceNo = invoice.SupplierInvoiceNo,
                PurchaseOrderId = invoice.PurchaseOrderId,
                PurchaseOrderNo = invoice.PurchaseOrder?.PurchaseOrderNo ?? string.Empty,
                GoodsReceiptId = invoice.GoodsReceiptId,
                InvoiceDate = invoice.InvoiceDate,
                DueDate = invoice.DueDate,
                PaymentTermId = invoice.PaymentTermId,
                OtherCharges = invoice.OtherCharges,
                Notes = invoice.Notes,
                Status = invoice.Status,
                SupplierName = invoice.Supplier?.SupplierName ?? string.Empty
            };

            model.PurchaseInvoiceItems = await BuildEditableLinesAsync(invoice);

            await PopulateAsync(model);

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<IActionResult> UpdatePurchaseInvoice(PurchaseInvoiceUpdateModel model)
        {
            var existing = await _purchaseInvoiceManagementService.GetPurchaseInvoiceByIdAsync(model.Id);

            if (existing == null)
            {
                return NotFound();
            }

            // An invoice never moves to another purchase order, so the posted value is
            // discarded here as well.
            model.PurchaseOrderId = existing.PurchaseOrderId;
            model.PurchaseOrderNo = existing.PurchaseOrder?.PurchaseOrderNo ?? string.Empty;
            model.SupplierName = existing.Supplier?.SupplierName ?? string.Empty;

            if (!ModelState.IsValid)
            {
                await PopulateAsync(model);
                return View(model);
            }

            try
            {
                var invoice = new PurchaseInvoice
                {
                    Id = model.Id,
                    SupplierInvoiceNo = model.SupplierInvoiceNo,
                    PurchaseOrderId = model.PurchaseOrderId,
                    GoodsReceiptId = model.GoodsReceiptId,
                    InvoiceDate = model.InvoiceDate,
                    DueDate = model.DueDate,
                    PaymentTermId = model.PaymentTermId,
                    OtherCharges = model.OtherCharges,
                    Notes = model.Notes,
                    Status = model.Status,
                    PurchaseInvoiceItems = ToLineRequests(model.PurchaseInvoiceItems)
                };

                await _purchaseInvoiceManagementService.UpdatePurchaseInvoiceAsync(invoice);

                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = model.Status == PurchaseInvoiceStatus.Posted
                        ? "Purchase invoice posted and the supplier balance was updated."
                        : "Purchase invoice updated successfully.",
                    Type = ResponseTypes.Success
                });

                return RedirectToAction(nameof(PurchaseInvoiceList));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                await PopulateAsync(model);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Purchase invoice update failed.");
                ModelState.AddModelError(string.Empty, "Purchase invoice update failed.");
                await PopulateAsync(model);
                return View(model);
            }
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<JsonResult> PostPurchaseInvoice(Guid id)
        {
            return await RunAsync(
                async () =>
                {
                    await _purchaseInvoiceManagementService.PostPurchaseInvoiceAsync(id);

                    await _notificationService.RaiseAsync(NotificationEvent.PurchaseInvoicePosted,
                        "Purchase invoice posted",
                        "A supplier invoice has been posted and is now payable.",
                        "/Admin/PurchaseInvoice/PurchaseInvoiceList",
                        id, User.Identity?.Name);
                },
                "Purchase invoice posted and the supplier balance was updated.",
                "Purchase invoice posting failed.");
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<JsonResult> CancelPurchaseInvoice(Guid id)
        {
            return await RunAsync(
                () => _purchaseInvoiceManagementService.CancelPurchaseInvoiceAsync(id),
                "Purchase invoice cancelled and any posted amount was reversed.",
                "Purchase invoice cancel failed.");
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "DeletePermission")]
        public async Task<JsonResult> DeletePurchaseInvoice(Guid id)
        {
            return await RunAsync(
                () => _purchaseInvoiceManagementService.DeletePurchaseInvoiceAsync(id),
                "Purchase invoice deleted successfully.",
                "Purchase invoice delete failed.");
        }

        [HttpGet, Authorize(Policy = "ReadPermission")]
        public async Task<IActionResult> GetPurchaseInvoiceDetails(Guid id)
        {
            var invoice = await _purchaseInvoiceManagementService.GetPurchaseInvoiceByIdAsync(id);

            if (invoice == null)
            {
                return NotFound();
            }

            var result = new
            {
                id = invoice.Id,
                invoiceNo = invoice.InvoiceNo,
                supplierInvoiceNo = invoice.SupplierInvoiceNo,
                supplierName = invoice.Supplier?.SupplierName,
                purchaseOrderNo = invoice.PurchaseOrder?.PurchaseOrderNo,
                goodsReceiptNo = invoice.GoodsReceipt?.GoodsReceiptNo,
                invoiceDate = invoice.InvoiceDate,
                dueDate = invoice.DueDate,
                paymentTerms = invoice.PaymentTerm?.TermName,
                status = invoice.Status.ToString(),
                notes = invoice.Notes,
                subTotal = invoice.SubTotal,
                itemDiscountTotal = invoice.ItemDiscountTotal,
                totalTax = invoice.TotalTax,
                otherCharges = invoice.OtherCharges,
                grandTotal = invoice.GrandTotal,
                paidAmount = invoice.PaidAmount,
                dueAmount = invoice.DueAmount,
                purchaseInvoiceItems = invoice.PurchaseInvoiceItems?.Select(x => new
                {
                    productName = x.Product?.ProductName,
                    unitName = x.Product?.Unit?.UnitName,
                    quantity = x.Quantity,
                    unitPrice = x.UnitPrice,
                    discountAmount = x.DiscountAmount,
                    taxRate = x.TaxRate,
                    taxAmount = x.TaxAmount,
                    lineTotal = x.LineTotal
                }).ToList()
            };

            return Json(result);
        }

        /// <summary>
        /// The received but unbilled lines of a purchase order, so the invoice screen
        /// can build its grid as soon as an order is chosen.
        /// </summary>
        [HttpGet, Authorize(Policy = "ReadPermission")]
        public async Task<JsonResult> GetBillableLines(Guid purchaseOrderId)
        {
            try
            {
                var purchaseOrder = await _purchaseOrderManagementService.GetPurchaseOrderByIdAsync(purchaseOrderId);

                if (purchaseOrder == null)
                {
                    return Json(new { success = false, message = "Purchase order not found." });
                }

                var lines = await _purchaseInvoiceManagementService.GetBillableLinesAsync(purchaseOrderId);
                var receipts = await LoadGoodsReceiptOptionsAsync(purchaseOrderId);

                return Json(new
                {
                    success = true,
                    supplierName = purchaseOrder.Supplier?.SupplierName,
                    paymentTermId = purchaseOrder.PaymentTermId,
                    goodsReceipts = receipts.Select(x => new { value = x.Value, text = x.Text }),
                    lines = lines.Select(x => new
                    {
                        productId = x.ProductId,
                        productName = x.ProductName,
                        unitName = x.UnitName,
                        receivedQuantity = x.ReceivedQuantity,
                        invoicedQuantity = x.InvoicedQuantity,
                        remainingQuantity = x.RemainingQuantity,
                        unitPrice = x.UnitPrice,
                        discountAmount = x.DiscountAmount,
                        taxRate = x.TaxRate
                    })
                });
            }
            catch (InvalidOperationException ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Loading billable lines failed.");
                return Json(new { success = false, message = "The purchase order lines could not be loaded." });
            }
        }

        /// <summary>
        /// The three way match screen: purchase order, goods receipts and purchase
        /// invoices of one order side by side.
        /// </summary>
        [Authorize(Policy = "ReadPermission")]
        public async Task<IActionResult> ThreeWayMatch(Guid? purchaseOrderId)
        {
            var model = new ThreeWayMatchViewModel();

            var orders = await _purchaseOrderManagementService.GetPurchaseOrdersByStatusAsync(
                PurchaseOrderStatus.PartiallyReceived,
                PurchaseOrderStatus.Received,
                PurchaseOrderStatus.Closed);

            model.SetPurchaseOrderValues(orders);

            if (purchaseOrderId.HasValue && purchaseOrderId.Value != Guid.Empty)
            {
                model.PurchaseOrderId = purchaseOrderId.Value;
                model.Match = await _purchaseInvoiceManagementService.GetThreeWayMatchAsync(purchaseOrderId.Value);
            }

            return View(model);
        }

        [HttpGet, Authorize(Policy = "ReadPermission")]
        public async Task<JsonResult> GetThreeWayMatch(Guid purchaseOrderId)
        {
            try
            {
                var match = await _purchaseInvoiceManagementService.GetThreeWayMatchAsync(purchaseOrderId);

                if (match == null)
                {
                    return Json(new { success = false, message = "Purchase order not found." });
                }

                return Json(new { success = true, match });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Three way match failed.");
                return Json(new { success = false, message = "The three way match could not be built." });
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

        private async Task FillLinesAsync(PurchaseInvoiceCreateModel model)
        {
            var purchaseOrder = await _purchaseOrderManagementService.GetPurchaseOrderByIdAsync(model.PurchaseOrderId);

            if (purchaseOrder == null)
            {
                return;
            }

            model.SupplierName = purchaseOrder.Supplier?.SupplierName ?? string.Empty;
            model.PaymentTermId ??= purchaseOrder.PaymentTermId;

            var lines = await _purchaseInvoiceManagementService.GetBillableLinesAsync(model.PurchaseOrderId);

            model.PurchaseInvoiceItems = lines.Select(x => new PurchaseInvoiceItemModel
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                UnitName = x.UnitName,
                ReceivedQuantity = x.ReceivedQuantity,
                InvoicedQuantity = x.InvoicedQuantity,
                RemainingQuantity = x.RemainingQuantity,
                Quantity = x.RemainingQuantity,
                UnitPrice = x.UnitPrice,
                DiscountAmount = x.DiscountAmount,
                TaxRate = x.TaxRate
            }).ToList();
        }

        private async Task<List<PurchaseInvoiceItemModel>> BuildEditableLinesAsync(PurchaseInvoice invoice)
        {
            var billable = (await _purchaseInvoiceManagementService
                .GetBillableLinesAsync(invoice.PurchaseOrderId, invoice.Id))
                .ToDictionary(x => x.ProductId);

            return (invoice.PurchaseInvoiceItems ?? new List<PurchaseInvoiceItem>())
                .Select(item =>
                {
                    billable.TryGetValue(item.ProductId, out var line);

                    return new PurchaseInvoiceItemModel
                    {
                        ProductId = item.ProductId,
                        ProductName = item.Product?.ProductName ?? string.Empty,
                        UnitName = item.Product?.Unit?.UnitName ?? string.Empty,
                        ReceivedQuantity = line?.ReceivedQuantity ?? item.Quantity,
                        InvoicedQuantity = line?.InvoicedQuantity ?? 0m,
                        RemainingQuantity = line?.RemainingQuantity ?? item.Quantity,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice,
                        DiscountAmount = item.DiscountAmount,
                        TaxRate = item.TaxRate
                    };
                })
                .ToList();
        }

        private static List<PurchaseInvoiceItem> ToLineRequests(IEnumerable<PurchaseInvoiceItemModel> items)
        {
            return items
                .Where(x => x.ProductId != Guid.Empty && x.Quantity > 0)
                .Select(x => new PurchaseInvoiceItem
                {
                    ProductId = x.ProductId,
                    Quantity = x.Quantity,
                    DiscountAmount = x.DiscountAmount
                })
                .ToList();
        }

        /// <summary>
        /// The confirmed receipts of an order, so the invoice can name the delivery it
        /// covers. An empty choice bills everything received so far.
        /// </summary>
        private async Task<IList<SelectListItem>> LoadGoodsReceiptOptionsAsync(Guid purchaseOrderId)
        {
            var options = new List<SelectListItem> { new SelectListItem("-- Whole order --", string.Empty) };

            if (purchaseOrderId == Guid.Empty)
            {
                return options;
            }

            var receipts = await _goodsReceiptManagementService.GetGoodsReceiptsByPurchaseOrderAsync(purchaseOrderId);

            options.AddRange(receipts
                .Where(x => x.Status == GoodsReceiptStatus.Received)
                .OrderByDescending(x => x.ReceiptDate)
                .Select(x => new SelectListItem(
                    $"{x.GoodsReceiptNo} ({x.ReceiptDate:dd-MM-yyyy})",
                    x.Id.ToString())));

            return options;
        }

        private async Task PopulateAsync(PurchaseInvoiceCreateModel model)
        {
            if (string.IsNullOrWhiteSpace(model.InvoiceNo))
            {
                model.InvoiceNo = await _purchaseInvoiceManagementService.GenerateInvoiceNoAsync();
            }

            // Only an order with goods actually received can be invoiced.
            model.SetPurchaseOrderValues(
                await _purchaseOrderManagementService.GetPurchaseOrdersByStatusAsync(
                    PurchaseOrderStatus.PartiallyReceived,
                    PurchaseOrderStatus.Received,
                    PurchaseOrderStatus.Closed));

            model.SetPaymentTermValues(await _paymentTermManagementService.GetActivePaymentTermAsync());
            model.GoodsReceipts = await LoadGoodsReceiptOptionsAsync(model.PurchaseOrderId);
        }

        private async Task PopulateAsync(PurchaseInvoiceUpdateModel model)
        {
            model.SetPaymentTermValues(await _paymentTermManagementService.GetActivePaymentTermAsync());
            model.GoodsReceipts = await LoadGoodsReceiptOptionsAsync(model.PurchaseOrderId);
        }
    }
}
