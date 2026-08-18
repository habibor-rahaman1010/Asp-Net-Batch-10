using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Infrastructure;
using DevSkill.Inventory.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// Billing the customer for goods that have already left the warehouse. Posting
    /// an invoice is the only thing that raises what the customer owes; it never
    /// touches stock, because the delivery has already done that.
    /// </summary>
    [Area("Admin"), Authorize]
    public class SalesInvoiceController : Controller
    {
        private readonly ISalesInvoiceManagementService _salesInvoiceManagementService;
        private readonly ISalesOrderManagementService _salesOrderManagementService;
        private readonly IPaymentTermManagementService _paymentTermManagementService;
        private readonly INotificationService _notificationService;
        private readonly ILogger<SalesInvoiceController> _logger;

        public SalesInvoiceController(
            ISalesInvoiceManagementService salesInvoiceManagementService,
            ISalesOrderManagementService salesOrderManagementService,
            IPaymentTermManagementService paymentTermManagementService,
            ILogger<SalesInvoiceController> logger,
            INotificationService notificationService)
        {
            _salesInvoiceManagementService = salesInvoiceManagementService;
            _salesOrderManagementService = salesOrderManagementService;
            _paymentTermManagementService = paymentTermManagementService;
            _logger = logger;
            _notificationService = notificationService;
        }

        [Authorize(Policy = "ReadPermission")]
        public IActionResult SalesInvoiceList()
        {
            return View();
        }

        [HttpPost, Authorize(Policy = "ReadPermission")]
        public async Task<JsonResult> GetSalesInvoiceJsonData([FromBody] SalesInvoiceListModel model)
        {
            var result = await _salesInvoiceManagementService.GetSalesInvoicesAsync(
                model.PageIndex, model.PageSize, model.Search,
                model.FormatSortExpression(
                    "InvoiceNo",
                    "Customer.CustomerName",
                    "InvoiceDate",
                    "DueDate",
                    "GrandTotal",
                    "PaidAmount",
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
                            HttpUtility.HtmlEncode(record.Customer?.CustomerName),
                            HttpUtility.HtmlEncode(record.InvoiceDate.ToString("dd-MM-yyyy")),
                            HttpUtility.HtmlEncode(record.DueDate.ToString("dd-MM-yyyy")),
                            HttpUtility.HtmlEncode(record.GrandTotal.ToString("N2")),
                            HttpUtility.HtmlEncode(record.PaidAmount.ToString("N2")),
                            HttpUtility.HtmlEncode(record.DueAmount.ToString("N2")),
                            HttpUtility.HtmlEncode(record.Status.ToString()),
                            HttpUtility.HtmlEncode(record.Id.ToString())
                        }
                    ).ToArray()
            };

            return Json(jsonData);
        }

        [Authorize(Policy = "CreatePermission")]
        public async Task<IActionResult> CreateSalesInvoice(Guid? salesOrderId)
        {
            var model = new SalesInvoiceCreateModel
            {
                InvoiceNo = await _salesInvoiceManagementService.GenerateInvoiceNoAsync()
            };

            if (salesOrderId.HasValue && salesOrderId.Value != Guid.Empty)
            {
                model.SalesOrderId = salesOrderId.Value;

                try
                {
                    await LoadOrderAsync(model, null);
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    model.SalesOrderId = Guid.Empty;
                }
            }

            await PopulateAsync(model);

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "CreatePermission")]
        public async Task<IActionResult> CreateSalesInvoice(SalesInvoiceCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                await RebuildAsync(model);
                return View(model);
            }

            try
            {
                var invoice = new SalesInvoice
                {
                    SalesOrderId = model.SalesOrderId,
                    DeliveryId = model.DeliveryId,
                    InvoiceDate = model.InvoiceDate,
                    DueDate = model.DueDate,
                    PaymentTermId = model.PaymentTermId,
                    OtherCharges = model.OtherCharges,
                    Notes = model.Notes,
                    CreatedBy = User.Identity?.Name ?? string.Empty,
                    SalesInvoiceItems = ToLineRequests(model.SalesInvoiceItems)
                };

                var salesInvoiceId = await _salesInvoiceManagementService
                    .CreateSalesInvoiceAsync(invoice, model.PostImmediately);

                // A draft invoice owes nothing yet, so only a posted one is reported.
                if (model.PostImmediately)
                {
                    await _notificationService.RaiseAsync(NotificationEvent.SalesInvoicePosted,
                        "Sales invoice posted",
                        "An invoice has been posted and the customer now owes it.",
                        "/Admin/SalesInvoice/SalesInvoiceList",
                        salesInvoiceId, User.Identity?.Name);
                }

                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = model.PostImmediately
                        ? "Sales invoice created and posted. The customer now owes this amount."
                        : "Sales invoice created as a draft. Nothing has been claimed yet.",
                    Type = ResponseTypes.Success
                });

                return RedirectToAction(nameof(SalesInvoiceList));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                await RebuildAsync(model);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Sales invoice creation failed.");
                ModelState.AddModelError(string.Empty, "Sales invoice creation failed.");
                await RebuildAsync(model);
                return View(model);
            }
        }

        [Authorize(Policy = "UpdatePermission")]
        public async Task<IActionResult> UpdateSalesInvoice(Guid id)
        {
            var invoice = await _salesInvoiceManagementService.GetSalesInvoiceByIdAsync(id);

            if (invoice == null)
            {
                return NotFound();
            }

            var model = new SalesInvoiceUpdateModel
            {
                Id = invoice.Id,
                InvoiceNo = invoice.InvoiceNo,
                SalesOrderId = invoice.SalesOrderId,
                SalesOrderNo = invoice.SalesOrder?.SalesOrderNo ?? string.Empty,
                DeliveryId = invoice.DeliveryId,
                DeliveryNo = invoice.Delivery?.DeliveryNo ?? "Whole order",
                CustomerName = invoice.Customer?.CustomerName ?? string.Empty,
                WarehouseName = invoice.BusinessLocation?.LocationName ?? string.Empty,
                InvoiceDate = invoice.InvoiceDate,
                DueDate = invoice.DueDate,
                PaymentTermId = invoice.PaymentTermId,
                OtherCharges = invoice.OtherCharges,
                Notes = invoice.Notes,
                SalesInvoiceItems = await LoadLinesAsync(invoice.SalesOrderId, invoice.Id,
                    invoice.SalesInvoiceItems?.ToDictionary(x => x.ProductId, x => x.Quantity))
            };

            await PopulateAsync(model);

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<IActionResult> UpdateSalesInvoice(SalesInvoiceUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                await RebuildAsync(model);
                return View(model);
            }

            try
            {
                var invoice = new SalesInvoice
                {
                    Id = model.Id,
                    SalesOrderId = model.SalesOrderId,
                    DeliveryId = model.DeliveryId,
                    InvoiceDate = model.InvoiceDate,
                    DueDate = model.DueDate,
                    PaymentTermId = model.PaymentTermId,
                    OtherCharges = model.OtherCharges,
                    Notes = model.Notes,
                    SalesInvoiceItems = ToLineRequests(model.SalesInvoiceItems)
                };

                await _salesInvoiceManagementService.UpdateSalesInvoiceAsync(invoice);

                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "Sales invoice updated successfully.",
                    Type = ResponseTypes.Success
                });

                return RedirectToAction(nameof(SalesInvoiceList));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                await RebuildAsync(model);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Sales invoice update failed.");
                ModelState.AddModelError(string.Empty, "Sales invoice update failed.");
                await RebuildAsync(model);
                return View(model);
            }
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "DeletePermission")]
        public async Task<JsonResult> DeleteSalesInvoice(Guid id)
        {
            return await RunAsync(
                () => _salesInvoiceManagementService.DeleteSalesInvoiceAsync(id),
                "Sales invoice deleted successfully.",
                "Sales invoice delete failed.");
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<JsonResult> PostSalesInvoice(Guid id)
        {
            return await RunAsync(
                async () =>
                {
                    await _salesInvoiceManagementService.PostSalesInvoiceAsync(id);

                    await _notificationService.RaiseAsync(NotificationEvent.SalesInvoicePosted,
                        "Sales invoice posted",
                        "An invoice has been posted and the customer now owes it.",
                        "/Admin/SalesInvoice/SalesInvoiceList",
                        id, User.Identity?.Name);
                },
                "Invoice posted. The customer now owes this amount.",
                "Posting the invoice failed.");
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<JsonResult> CancelSalesInvoice(Guid id)
        {
            return await RunAsync(
                () => _salesInvoiceManagementService.CancelSalesInvoiceAsync(id),
                "Invoice cancelled and taken back off the customer account.",
                "Cancelling the invoice failed.");
        }

        [HttpGet, Authorize(Policy = "ReadPermission")]
        public async Task<IActionResult> GetSalesInvoiceDetails(Guid id)
        {
            var invoice = await _salesInvoiceManagementService.GetSalesInvoiceByIdAsync(id);

            if (invoice == null)
            {
                return NotFound();
            }

            var result = new
            {
                id = invoice.Id,
                invoiceNo = invoice.InvoiceNo,
                salesOrderNo = invoice.SalesOrder?.SalesOrderNo,
                deliveryNo = invoice.Delivery?.DeliveryNo,
                customerName = invoice.Customer?.CustomerName,
                customerCode = invoice.Customer?.CustomerCode,
                warehouse = invoice.BusinessLocation?.LocationName,
                salesperson = invoice.Salesperson?.SalespersonName,
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
                salesInvoiceItems = invoice.SalesInvoiceItems?.Select(x => new
                {
                    productName = x.Product?.ProductName,
                    unitName = x.Product?.Unit?.UnitName,
                    quantity = x.Quantity,
                    unitPrice = x.UnitPrice,
                    discountAmount = x.DiscountAmount,
                    taxRate = x.TaxRate,
                    taxAmount = x.TaxAmount,
                    lineTotal = x.LineTotal,
                    returnedQuantity = x.ReturnedQuantity
                }).ToList()
            };

            return Json(result);
        }

        /// <summary>
        /// Feeds the create screen once a sales order is picked. Only what has actually
        /// been delivered may be billed, and every figure comes from the server.
        /// </summary>
        [HttpGet, Authorize(Policy = "ReadPermission")]
        public async Task<JsonResult> GetBillableLines(Guid salesOrderId)
        {
            if (salesOrderId == Guid.Empty)
            {
                return Json(new { success = false, message = "Please select a sales order." });
            }

            try
            {
                var salesOrder = await _salesOrderManagementService.GetSalesOrderByIdAsync(salesOrderId);

                if (salesOrder == null)
                {
                    return Json(new { success = false, message = "Sales order not found." });
                }

                var lines = await _salesInvoiceManagementService.GetBillableLinesAsync(salesOrderId);
                var deliveries = await _salesInvoiceManagementService.GetInvoiceableDeliveriesAsync(salesOrderId);

                return Json(new
                {
                    success = true,
                    customerName = salesOrder.Customer?.CustomerName,
                    warehouseName = salesOrder.BusinessLocation?.LocationName,
                    paymentTermId = salesOrder.PaymentTermId,
                    deliveries = deliveries.Select(x => new
                    {
                        id = x.Id,
                        text = $"{x.DeliveryNo} - {x.DeliveryDate:dd-MM-yyyy}"
                    }),
                    lines = lines.Select(x => new
                    {
                        productId = x.ProductId,
                        productName = x.ProductName,
                        unitName = x.UnitName,
                        orderedQuantity = x.OrderedQuantity,
                        deliveredQuantity = x.DeliveredQuantity,
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
                return Json(new { success = false, message = "The sales order lines could not be loaded." });
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
        /// Only product and quantity travel to the service; every price comes off the
        /// sales order there, never from the browser.
        /// </summary>
        private static List<SalesInvoiceItem> ToLineRequests(IEnumerable<SalesInvoiceItemModel> items)
        {
            return items
                .Where(x => x.ProductId != Guid.Empty && x.Quantity > 0)
                .Select(x => new SalesInvoiceItem
                {
                    ProductId = x.ProductId,
                    Quantity = x.Quantity
                })
                .ToList();
        }

        private async Task<List<SalesInvoiceItemModel>> LoadLinesAsync(Guid salesOrderId, Guid? excludeInvoiceId,
            IDictionary<Guid, decimal>? enteredQuantities)
        {
            var lines = await _salesInvoiceManagementService.GetBillableLinesAsync(salesOrderId, excludeInvoiceId);

            return lines.Select(x => new SalesInvoiceItemModel
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                UnitName = x.UnitName,
                OrderedQuantity = x.OrderedQuantity,
                DeliveredQuantity = x.DeliveredQuantity,
                AlreadyInvoicedQuantity = x.InvoicedQuantity,
                RemainingQuantity = x.RemainingQuantity,
                UnitPrice = x.UnitPrice,
                DiscountAmount = x.DiscountAmount,
                TaxRate = x.TaxRate,
                Quantity = enteredQuantities != null && enteredQuantities.TryGetValue(x.ProductId, out var entered)
                    ? entered
                    : x.RemainingQuantity
            }).ToList();
        }

        private async Task LoadOrderAsync(SalesInvoiceCreateModel model, IDictionary<Guid, decimal>? entered)
        {
            var salesOrder = await _salesOrderManagementService.GetSalesOrderByIdAsync(model.SalesOrderId)
                ?? throw new InvalidOperationException("Sales order not found.");

            model.CustomerName = salesOrder.Customer?.CustomerName ?? string.Empty;
            model.WarehouseName = salesOrder.BusinessLocation?.LocationName ?? string.Empty;
            model.PaymentTermId ??= salesOrder.PaymentTermId;
            model.SalesInvoiceItems = await LoadLinesAsync(model.SalesOrderId, null, entered);

            model.SetDeliveryValues(
                await _salesInvoiceManagementService.GetInvoiceableDeliveriesAsync(model.SalesOrderId));
        }

        private async Task PopulateAsync(SalesInvoiceCreateModel model)
        {
            if (string.IsNullOrWhiteSpace(model.InvoiceNo))
            {
                model.InvoiceNo = await _salesInvoiceManagementService.GenerateInvoiceNoAsync();
            }

            model.SetSalesOrderValues(await _salesInvoiceManagementService.GetBillableSalesOrdersAsync());
            model.SetPaymentTermValues(await _paymentTermManagementService.GetActivePaymentTermAsync());

            if (model.Deliveries.Count == 0)
            {
                model.SetDeliveryValues(new List<Delivery>());
            }
        }

        private async Task PopulateAsync(SalesInvoiceUpdateModel model)
        {
            model.SetPaymentTermValues(await _paymentTermManagementService.GetActivePaymentTermAsync());
        }

        /// <summary>
        /// A failed post only carries product ids and quantities back, so the names and
        /// the billable quantities are read from the server again.
        /// </summary>
        private async Task RebuildAsync(SalesInvoiceCreateModel model)
        {
            var entered = model.SalesInvoiceItems
                .Where(x => x.ProductId != Guid.Empty)
                .GroupBy(x => x.ProductId)
                .ToDictionary(x => x.Key, x => x.Sum(i => i.Quantity));

            try
            {
                if (model.SalesOrderId != Guid.Empty)
                {
                    await LoadOrderAsync(model, entered);
                }
                else
                {
                    model.SalesInvoiceItems = new List<SalesInvoiceItemModel>();
                }
            }
            catch (InvalidOperationException)
            {
                model.SalesInvoiceItems = new List<SalesInvoiceItemModel>();
            }

            await PopulateAsync(model);
        }

        private async Task RebuildAsync(SalesInvoiceUpdateModel model)
        {
            var invoice = await _salesInvoiceManagementService.GetSalesInvoiceByIdAsync(model.Id);

            if (invoice == null)
            {
                model.SalesInvoiceItems = new List<SalesInvoiceItemModel>();
                return;
            }

            model.InvoiceNo = invoice.InvoiceNo;
            model.SalesOrderId = invoice.SalesOrderId;
            model.SalesOrderNo = invoice.SalesOrder?.SalesOrderNo ?? string.Empty;
            model.DeliveryId = invoice.DeliveryId;
            model.DeliveryNo = invoice.Delivery?.DeliveryNo ?? "Whole order";
            model.CustomerName = invoice.Customer?.CustomerName ?? string.Empty;
            model.WarehouseName = invoice.BusinessLocation?.LocationName ?? string.Empty;

            var entered = model.SalesInvoiceItems
                .Where(x => x.ProductId != Guid.Empty)
                .GroupBy(x => x.ProductId)
                .ToDictionary(x => x.Key, x => x.Sum(i => i.Quantity));

            try
            {
                model.SalesInvoiceItems = await LoadLinesAsync(invoice.SalesOrderId, invoice.Id, entered);
            }
            catch (InvalidOperationException)
            {
                model.SalesInvoiceItems = new List<SalesInvoiceItemModel>();
            }

            await PopulateAsync(model);
        }
    }
}
