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
    /// Goods coming back from customers. A draft brings nothing back; confirming the
    /// return puts the stock in and raises the credit note that pays for it.
    /// </summary>
    [Area("Admin"), Authorize]
    public class SalesReturnController : Controller
    {
        private readonly ISalesReturnManagementService _salesReturnManagementService;
        private readonly ISalesInvoiceManagementService _salesInvoiceManagementService;
        private readonly ILogger<SalesReturnController> _logger;

        public SalesReturnController(
            ISalesReturnManagementService salesReturnManagementService,
            ISalesInvoiceManagementService salesInvoiceManagementService,
            ILogger<SalesReturnController> logger)
        {
            _salesReturnManagementService = salesReturnManagementService;
            _salesInvoiceManagementService = salesInvoiceManagementService;
            _logger = logger;
        }

        [Authorize(Policy = "ReadPermission")]
        public IActionResult SalesReturnList()
        {
            return View();
        }

        [HttpPost, Authorize(Policy = "ReadPermission")]
        public async Task<JsonResult> GetSalesReturnJsonData([FromBody] SalesReturnListModel model)
        {
            var result = await _salesReturnManagementService.GetSalesReturnsAsync(
                model.PageIndex, model.PageSize, model.Search,
                model.FormatSortExpression(
                    "ReturnNo",
                    "SalesInvoice.InvoiceNo",
                    "Customer.CustomerName",
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
                            HttpUtility.HtmlEncode(record.SalesInvoice?.InvoiceNo),
                            HttpUtility.HtmlEncode(record.Customer?.CustomerName),
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
        public async Task<IActionResult> CreateSalesReturn(Guid? salesInvoiceId)
        {
            var model = new SalesReturnCreateModel
            {
                ReturnNo = await _salesReturnManagementService.GenerateReturnNoAsync()
            };

            if (salesInvoiceId.HasValue && salesInvoiceId.Value != Guid.Empty)
            {
                model.SalesInvoiceId = salesInvoiceId.Value;

                try
                {
                    await LoadInvoiceAsync(model, null);
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    model.SalesInvoiceId = Guid.Empty;
                }
            }

            await PopulateAsync(model);

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "CreatePermission")]
        public async Task<IActionResult> CreateSalesReturn(SalesReturnCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                await RebuildAsync(model);
                return View(model);
            }

            try
            {
                var salesReturn = new SalesReturn
                {
                    SalesInvoiceId = model.SalesInvoiceId,
                    ReturnDate = model.ReturnDate,
                    Reason = model.Reason,
                    Notes = model.Notes,
                    CreatedBy = User.Identity?.Name ?? string.Empty,
                    SalesReturnItems = ToLineRequests(model.SalesReturnItems)
                };

                await _salesReturnManagementService.CreateSalesReturnAsync(salesReturn, model.ConfirmImmediately);

                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = model.ConfirmImmediately
                        ? "Sales return confirmed. The goods are back in stock and a credit note has been raised."
                        : "Sales return saved as a draft. Nothing has come back yet.",
                    Type = ResponseTypes.Success
                });

                return RedirectToAction(nameof(SalesReturnList));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                await RebuildAsync(model);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Sales return creation failed.");
                ModelState.AddModelError(string.Empty, "Sales return creation failed.");
                await RebuildAsync(model);
                return View(model);
            }
        }

        [Authorize(Policy = "UpdatePermission")]
        public async Task<IActionResult> UpdateSalesReturn(Guid id)
        {
            var salesReturn = await _salesReturnManagementService.GetSalesReturnByIdAsync(id);

            if (salesReturn == null)
            {
                return NotFound();
            }

            var model = new SalesReturnUpdateModel
            {
                Id = salesReturn.Id,
                ReturnNo = salesReturn.ReturnNo,
                SalesInvoiceId = salesReturn.SalesInvoiceId,
                InvoiceNo = salesReturn.SalesInvoice?.InvoiceNo ?? string.Empty,
                CustomerName = salesReturn.Customer?.CustomerName ?? string.Empty,
                WarehouseName = salesReturn.BusinessLocation?.LocationName ?? string.Empty,
                ReturnDate = salesReturn.ReturnDate,
                Reason = salesReturn.Reason,
                Notes = salesReturn.Notes,
                SalesReturnItems = await LoadLinesAsync(salesReturn.SalesInvoiceId, salesReturn.Id,
                    salesReturn.SalesReturnItems?.ToDictionary(x => x.ProductId, x => x.ReturnQuantity))
            };

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<IActionResult> UpdateSalesReturn(SalesReturnUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                await RebuildAsync(model);
                return View(model);
            }

            try
            {
                var salesReturn = new SalesReturn
                {
                    Id = model.Id,
                    SalesInvoiceId = model.SalesInvoiceId,
                    ReturnDate = model.ReturnDate,
                    Reason = model.Reason,
                    Notes = model.Notes,
                    SalesReturnItems = ToLineRequests(model.SalesReturnItems)
                };

                await _salesReturnManagementService.UpdateSalesReturnAsync(salesReturn);

                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "Sales return updated successfully.",
                    Type = ResponseTypes.Success
                });

                return RedirectToAction(nameof(SalesReturnList));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                await RebuildAsync(model);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Sales return update failed.");
                ModelState.AddModelError(string.Empty, "Sales return update failed.");
                await RebuildAsync(model);
                return View(model);
            }
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "DeletePermission")]
        public async Task<JsonResult> DeleteSalesReturn(Guid id)
        {
            return await RunAsync(
                () => _salesReturnManagementService.DeleteSalesReturnAsync(id),
                "Sales return deleted successfully.",
                "Sales return delete failed.");
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<JsonResult> ConfirmSalesReturn(Guid id)
        {
            return await RunAsync(
                () => _salesReturnManagementService.ConfirmSalesReturnAsync(id),
                "Goods taken back. Stock is up and a credit note has been raised for the customer.",
                "Confirming the sales return failed.");
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<JsonResult> CancelSalesReturn(Guid id)
        {
            return await RunAsync(
                () => _salesReturnManagementService.CancelSalesReturnAsync(id),
                "Sales return cancelled. The stock and the credit have both been put back.",
                "Cancelling the sales return failed.");
        }

        [HttpGet, Authorize(Policy = "ReadPermission")]
        public async Task<IActionResult> GetSalesReturnDetails(Guid id)
        {
            var salesReturn = await _salesReturnManagementService.GetSalesReturnByIdAsync(id);

            if (salesReturn == null)
            {
                return NotFound();
            }

            var result = new
            {
                id = salesReturn.Id,
                returnNo = salesReturn.ReturnNo,
                invoiceNo = salesReturn.SalesInvoice?.InvoiceNo,
                customerName = salesReturn.Customer?.CustomerName,
                customerCode = salesReturn.Customer?.CustomerCode,
                warehouse = salesReturn.BusinessLocation?.LocationName,
                returnDate = salesReturn.ReturnDate,
                reason = salesReturn.Reason,
                status = salesReturn.Status.ToString(),
                notes = salesReturn.Notes,
                totalAmount = salesReturn.TotalAmount,
                salesReturnItems = salesReturn.SalesReturnItems?.Select(x => new
                {
                    productName = x.Product?.ProductName,
                    unitName = x.Product?.Unit?.UnitName,
                    invoicedQuantity = x.InvoicedQuantity,
                    returnQuantity = x.ReturnQuantity,
                    unitPrice = x.UnitPrice,
                    taxAmount = x.TaxAmount,
                    lineTotal = x.LineTotal,
                    remarks = x.Remarks
                }).ToList()
            };

            return Json(result);
        }

        /// <summary>
        /// Feeds the create screen once an invoice is picked. Only what was actually
        /// billed may come back, and every figure comes from the server.
        /// </summary>
        [HttpGet, Authorize(Policy = "ReadPermission")]
        public async Task<JsonResult> GetReturnableLines(Guid salesInvoiceId)
        {
            if (salesInvoiceId == Guid.Empty)
            {
                return Json(new { success = false, message = "Please select a sales invoice." });
            }

            try
            {
                var invoice = await _salesInvoiceManagementService.GetSalesInvoiceByIdAsync(salesInvoiceId);

                if (invoice == null)
                {
                    return Json(new { success = false, message = "Sales invoice not found." });
                }

                var lines = await _salesReturnManagementService.GetReturnableLinesAsync(salesInvoiceId);

                return Json(new
                {
                    success = true,
                    customerName = invoice.Customer?.CustomerName,
                    warehouseName = invoice.BusinessLocation?.LocationName,
                    lines = lines.Select(x => new
                    {
                        productId = x.ProductId,
                        productName = x.ProductName,
                        unitName = x.UnitName,
                        invoicedQuantity = x.InvoicedQuantity,
                        returnedQuantity = x.ReturnedQuantity,
                        pendingQuantity = x.PendingQuantity,
                        remainingQuantity = x.RemainingQuantity,
                        unitPrice = x.UnitPrice,
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
                _logger.LogError(ex, "Loading returnable lines failed.");
                return Json(new { success = false, message = "The invoice lines could not be loaded." });
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
        /// Only product and quantity travel to the service; the price comes off the
        /// invoice there, never from the browser.
        /// </summary>
        private static List<SalesReturnItem> ToLineRequests(IEnumerable<SalesReturnItemModel> items)
        {
            return items
                .Where(x => x.ProductId != Guid.Empty && x.ReturnQuantity > 0)
                .Select(x => new SalesReturnItem
                {
                    ProductId = x.ProductId,
                    ReturnQuantity = x.ReturnQuantity,
                    Remarks = x.Remarks ?? string.Empty
                })
                .ToList();
        }

        private async Task<List<SalesReturnItemModel>> LoadLinesAsync(Guid salesInvoiceId, Guid? excludeReturnId,
            IDictionary<Guid, decimal>? enteredQuantities)
        {
            var lines = await _salesReturnManagementService.GetReturnableLinesAsync(salesInvoiceId, excludeReturnId);

            return lines.Select(x => new SalesReturnItemModel
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                UnitName = x.UnitName,
                InvoicedQuantity = x.InvoicedQuantity,
                AlreadyReturnedQuantity = x.ReturnedQuantity,
                PendingQuantity = x.PendingQuantity,
                RemainingQuantity = x.RemainingQuantity,
                UnitPrice = x.UnitPrice,
                TaxRate = x.TaxRate,

                // Nothing is defaulted in, because a full return is the exception
                // rather than the rule.
                ReturnQuantity = enteredQuantities != null
                    && enteredQuantities.TryGetValue(x.ProductId, out var entered)
                        ? entered
                        : 0m
            }).ToList();
        }

        private async Task LoadInvoiceAsync(SalesReturnCreateModel model, IDictionary<Guid, decimal>? entered)
        {
            var invoice = await _salesInvoiceManagementService.GetSalesInvoiceByIdAsync(model.SalesInvoiceId)
                ?? throw new InvalidOperationException("Sales invoice not found.");

            model.CustomerName = invoice.Customer?.CustomerName ?? string.Empty;
            model.WarehouseName = invoice.BusinessLocation?.LocationName ?? string.Empty;
            model.SalesReturnItems = await LoadLinesAsync(model.SalesInvoiceId, null, entered);
        }

        private async Task PopulateAsync(SalesReturnCreateModel model)
        {
            if (string.IsNullOrWhiteSpace(model.ReturnNo))
            {
                model.ReturnNo = await _salesReturnManagementService.GenerateReturnNoAsync();
            }

            model.SetSalesInvoiceValues(await _salesReturnManagementService.GetReturnableSalesInvoicesAsync());
        }

        /// <summary>
        /// A failed post only carries product ids and quantities back, so the names and
        /// the returnable quantities are read from the server again.
        /// </summary>
        private async Task RebuildAsync(SalesReturnCreateModel model)
        {
            var entered = model.SalesReturnItems
                .Where(x => x.ProductId != Guid.Empty)
                .GroupBy(x => x.ProductId)
                .ToDictionary(x => x.Key, x => x.Sum(i => i.ReturnQuantity));

            try
            {
                if (model.SalesInvoiceId != Guid.Empty)
                {
                    await LoadInvoiceAsync(model, entered);
                }
                else
                {
                    model.SalesReturnItems = new List<SalesReturnItemModel>();
                }
            }
            catch (InvalidOperationException)
            {
                model.SalesReturnItems = new List<SalesReturnItemModel>();
            }

            await PopulateAsync(model);
        }

        private async Task RebuildAsync(SalesReturnUpdateModel model)
        {
            var salesReturn = await _salesReturnManagementService.GetSalesReturnByIdAsync(model.Id);

            if (salesReturn == null)
            {
                model.SalesReturnItems = new List<SalesReturnItemModel>();
                return;
            }

            model.ReturnNo = salesReturn.ReturnNo;
            model.SalesInvoiceId = salesReturn.SalesInvoiceId;
            model.InvoiceNo = salesReturn.SalesInvoice?.InvoiceNo ?? string.Empty;
            model.CustomerName = salesReturn.Customer?.CustomerName ?? string.Empty;
            model.WarehouseName = salesReturn.BusinessLocation?.LocationName ?? string.Empty;

            var entered = model.SalesReturnItems
                .Where(x => x.ProductId != Guid.Empty)
                .GroupBy(x => x.ProductId)
                .ToDictionary(x => x.Key, x => x.Sum(i => i.ReturnQuantity));

            try
            {
                model.SalesReturnItems = await LoadLinesAsync(salesReturn.SalesInvoiceId, salesReturn.Id, entered);
            }
            catch (InvalidOperationException)
            {
                model.SalesReturnItems = new List<SalesReturnItemModel>();
            }
        }
    }
}
