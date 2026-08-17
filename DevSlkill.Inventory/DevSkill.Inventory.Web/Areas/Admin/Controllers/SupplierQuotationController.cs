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
    public class SupplierQuotationController : Controller
    {
        private readonly ISupplierQuotationManagementService _supplierQuotationManagementService;
        private readonly IRequestForQuotationManagementService _requestForQuotationManagementService;
        private readonly ISupplierManagementService _supplierManagementService;
        private readonly IPaymentTermManagementService _paymentTermManagementService;
        private readonly ILogger<SupplierQuotationController> _logger;

        public SupplierQuotationController(
            ISupplierQuotationManagementService supplierQuotationManagementService,
            IRequestForQuotationManagementService requestForQuotationManagementService,
            ISupplierManagementService supplierManagementService,
            IPaymentTermManagementService paymentTermManagementService,
            ILogger<SupplierQuotationController> logger)
        {
            _supplierQuotationManagementService = supplierQuotationManagementService;
            _requestForQuotationManagementService = requestForQuotationManagementService;
            _supplierManagementService = supplierManagementService;
            _paymentTermManagementService = paymentTermManagementService;
            _logger = logger;
        }

        [Authorize(Policy = "ReadPermission")]
        public IActionResult SupplierQuotationList()
        {
            return View();
        }

        [HttpPost, Authorize(Policy = "ReadPermission")]
        public async Task<JsonResult> GetSupplierQuotationJsonData([FromBody] SupplierQuotationListModel model)
        {
            var result = await _supplierQuotationManagementService.GetSupplierQuotationsAsync(
                model.PageIndex, model.PageSize, model.Search,
                model.FormatSortExpression(
                    "QuotationNo",
                    "RequestForQuotation.RfqNo",
                    "Supplier.SupplierName",
                    "QuotationDate",
                    "ValidUntil",
                    "DeliveryDays",
                    "GrandTotal",
                    "Status",
                    "Id"));

            var jsonData = new
            {
                recordsTotal = result.total,
                recordsFiltered = result.totalDisplay,
                data = (from record in result.data
                        select new string[]
                        {
                            HttpUtility.HtmlEncode(record.QuotationNo),
                            HttpUtility.HtmlEncode(record.RequestForQuotation?.RfqNo),
                            HttpUtility.HtmlEncode(record.Supplier?.SupplierName),
                            HttpUtility.HtmlEncode(record.QuotationDate.ToString("dd-MM-yyyy")),
                            HttpUtility.HtmlEncode(record.ValidUntil.ToString("dd-MM-yyyy")),
                            HttpUtility.HtmlEncode(record.DeliveryDays.ToString()),
                            HttpUtility.HtmlEncode(record.GrandTotal.ToString("N2")),
                            HttpUtility.HtmlEncode(record.Status.ToString()),
                            HttpUtility.HtmlEncode(record.Id.ToString())
                        }
                    ).ToArray()
            };

            return Json(jsonData);
        }

        [Authorize(Policy = "CreatePermission")]
        public async Task<IActionResult> CreateSupplierQuotation(Guid? requestForQuotationId)
        {
            var model = new SupplierQuotationCreateModel
            {
                QuotationNo = await _supplierQuotationManagementService.GenerateQuotationNoAsync()
            };

            // Coming in from the request list pre-fills the grid with what that
            // request asked to be quoted for.
            if (requestForQuotationId.HasValue && requestForQuotationId.Value != Guid.Empty)
            {
                model.RequestForQuotationId = requestForQuotationId.Value;
                await FillLinesAsync(model);
            }

            await PopulateAsync(model);

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "CreatePermission")]
        public async Task<IActionResult> CreateSupplierQuotation(SupplierQuotationCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateAsync(model);
                return View(model);
            }

            try
            {
                var supplierQuotation = new SupplierQuotation
                {
                    SupplierQuotationNo = model.SupplierQuotationNo,
                    RequestForQuotationId = model.RequestForQuotationId,
                    SupplierId = model.SupplierId,
                    QuotationDate = model.QuotationDate,
                    ValidUntil = model.ValidUntil,
                    DeliveryDays = model.DeliveryDays,
                    PaymentTermId = model.PaymentTermId,
                    OtherCharges = model.OtherCharges,
                    Notes = model.Notes,
                    CreatedBy = User.Identity?.Name ?? string.Empty,
                    SupplierQuotationItems = ToLineRequests(model.SupplierQuotationItems)
                };

                await _supplierQuotationManagementService.CreateSupplierQuotationAsync(supplierQuotation);

                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "Supplier quotation recorded. Compare it against the other offers to pick a winner.",
                    Type = ResponseTypes.Success
                });

                return RedirectToAction(nameof(SupplierQuotationList));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                await PopulateAsync(model);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Supplier quotation creation failed.");
                ModelState.AddModelError(string.Empty, "Supplier quotation creation failed.");
                await PopulateAsync(model);
                return View(model);
            }
        }

        [Authorize(Policy = "UpdatePermission")]
        public async Task<IActionResult> UpdateSupplierQuotation(Guid id)
        {
            var quotation = await _supplierQuotationManagementService.GetSupplierQuotationByIdAsync(id);

            if (quotation == null)
            {
                return NotFound();
            }

            if (quotation.Status != SupplierQuotationStatus.Received)
            {
                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = $"A {quotation.Status.ToString().ToLower()} quotation cannot be edited.",
                    Type = ResponseTypes.Danger
                });

                return RedirectToAction(nameof(SupplierQuotationList));
            }

            var model = new SupplierQuotationUpdateModel
            {
                Id = quotation.Id,
                QuotationNo = quotation.QuotationNo,
                SupplierQuotationNo = quotation.SupplierQuotationNo,
                RequestForQuotationId = quotation.RequestForQuotationId,
                RfqNo = quotation.RequestForQuotation?.RfqNo ?? string.Empty,
                SupplierId = quotation.SupplierId,
                SupplierName = quotation.Supplier?.SupplierName ?? string.Empty,
                QuotationDate = quotation.QuotationDate,
                ValidUntil = quotation.ValidUntil,
                DeliveryDays = quotation.DeliveryDays,
                PaymentTermId = quotation.PaymentTermId,
                OtherCharges = quotation.OtherCharges,
                Notes = quotation.Notes
            };

            model.SupplierQuotationItems = await BuildEditableLinesAsync(quotation);
            model.SetPaymentTermValues(await _paymentTermManagementService.GetAllPaymentTermAsync());

            var request = await _requestForQuotationManagementService
                .GetRequestForQuotationByIdAsync(quotation.RequestForQuotationId);

            model.WarehouseName = request?.BusinessLocation?.LocationName ?? string.Empty;

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<IActionResult> UpdateSupplierQuotation(SupplierQuotationUpdateModel model)
        {
            var existing = await _supplierQuotationManagementService.GetSupplierQuotationByIdAsync(model.Id);

            if (existing == null)
            {
                return NotFound();
            }

            // The request and the supplier are what the quotation is, so the posted
            // values are discarded here as well.
            model.RequestForQuotationId = existing.RequestForQuotationId;
            model.RfqNo = existing.RequestForQuotation?.RfqNo ?? string.Empty;
            model.SupplierId = existing.SupplierId;
            model.SupplierName = existing.Supplier?.SupplierName ?? string.Empty;

            model.SetPaymentTermValues(await _paymentTermManagementService.GetAllPaymentTermAsync());

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var supplierQuotation = new SupplierQuotation
                {
                    Id = model.Id,
                    SupplierQuotationNo = model.SupplierQuotationNo,
                    RequestForQuotationId = model.RequestForQuotationId,
                    SupplierId = model.SupplierId,
                    QuotationDate = model.QuotationDate,
                    ValidUntil = model.ValidUntil,
                    DeliveryDays = model.DeliveryDays,
                    PaymentTermId = model.PaymentTermId,
                    OtherCharges = model.OtherCharges,
                    Notes = model.Notes,
                    SupplierQuotationItems = ToLineRequests(model.SupplierQuotationItems)
                };

                await _supplierQuotationManagementService.UpdateSupplierQuotationAsync(supplierQuotation);

                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "Supplier quotation updated successfully.",
                    Type = ResponseTypes.Success
                });

                return RedirectToAction(nameof(SupplierQuotationList));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Supplier quotation update failed.");
                ModelState.AddModelError(string.Empty, "Supplier quotation update failed.");
                return View(model);
            }
        }

        /// <summary>
        /// The comparison screen: every offer against one request, lined up so price,
        /// terms and delivery can be read across in one go.
        /// </summary>
        [HttpGet, Authorize(Policy = "ReadPermission")]
        public async Task<IActionResult> CompareQuotations(QuotationComparisonModel model)
        {
            model.SetRequestForQuotationValues(
                await _requestForQuotationManagementService.GetRequestForQuotationsByStatusAsync(
                    RequestForQuotationStatus.Sent,
                    RequestForQuotationStatus.Closed,
                    RequestForQuotationStatus.Awarded));

            if (model.RequestForQuotationId.HasValue && model.RequestForQuotationId.Value != Guid.Empty)
            {
                try
                {
                    model.Comparison = await _supplierQuotationManagementService
                        .GetComparisonAsync(model.RequestForQuotationId.Value);
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Quotation comparison failed.");
                    ModelState.AddModelError(string.Empty, "The quotations could not be compared.");
                }
            }

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<JsonResult> SelectWinningQuotation(Guid id)
        {
            return await RunAsync(
                () => _supplierQuotationManagementService.SelectWinningQuotationAsync(id),
                "Winner selected. Every other offer on this request was rejected.",
                "Selecting the winning quotation failed.");
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<JsonResult> UndoAward(Guid requestForQuotationId)
        {
            return await RunAsync(
                () => _supplierQuotationManagementService.UndoAwardAsync(requestForQuotationId),
                "Award undone. The request is open for bidding again.",
                "Undoing the award failed.");
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<JsonResult> CancelSupplierQuotation(Guid id)
        {
            return await RunAsync(
                () => _supplierQuotationManagementService.CancelSupplierQuotationAsync(id),
                "Supplier quotation cancelled.",
                "Supplier quotation cancel failed.");
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "DeletePermission")]
        public async Task<JsonResult> DeleteSupplierQuotation(Guid id)
        {
            return await RunAsync(
                () => _supplierQuotationManagementService.DeleteSupplierQuotationAsync(id),
                "Supplier quotation deleted successfully.",
                "Supplier quotation delete failed.");
        }

        [HttpGet, Authorize(Policy = "ReadPermission")]
        public async Task<IActionResult> GetSupplierQuotationDetails(Guid id)
        {
            var quotation = await _supplierQuotationManagementService.GetSupplierQuotationByIdAsync(id);

            if (quotation == null)
            {
                return NotFound();
            }

            var result = new
            {
                id = quotation.Id,
                quotationNo = quotation.QuotationNo,
                supplierQuotationNo = quotation.SupplierQuotationNo,
                rfqNo = quotation.RequestForQuotation?.RfqNo,
                supplierName = quotation.Supplier?.SupplierName,
                quotationDate = quotation.QuotationDate,
                validUntil = quotation.ValidUntil,
                deliveryDays = quotation.DeliveryDays,
                paymentTerm = quotation.PaymentTerm?.TermName ?? "—",
                status = quotation.Status.ToString(),
                notes = quotation.Notes,
                subTotal = quotation.SubTotal,
                discountTotal = quotation.DiscountTotal,
                otherCharges = quotation.OtherCharges,
                grandTotal = quotation.GrandTotal,
                supplierQuotationItems = quotation.SupplierQuotationItems?.Select(x => new
                {
                    productName = x.Product?.ProductName,
                    unitName = x.Product?.Unit?.UnitName,
                    quantity = x.Quantity,
                    unitPrice = x.UnitPrice,
                    discountAmount = x.DiscountAmount,
                    lineTotal = x.LineTotal,
                    remarks = x.Remarks
                }).ToList()
            };

            return Json(result);
        }

        /// <summary>
        /// The lines of a sent request, so the quotation screen can build its grid as
        /// soon as a request is chosen.
        /// </summary>
        [HttpGet, Authorize(Policy = "ReadPermission")]
        public async Task<JsonResult> GetRequestLines(Guid requestForQuotationId)
        {
            try
            {
                var request = await _requestForQuotationManagementService
                    .GetRequestForQuotationByIdAsync(requestForQuotationId);

                if (request == null)
                {
                    return Json(new { success = false, message = "Request for quotation not found." });
                }

                return Json(new
                {
                    success = true,
                    warehouseName = request.BusinessLocation?.LocationName,
                    responseDeadline = request.ResponseDeadline,
                    lines = (request.RequestForQuotationItems ?? new List<RequestForQuotationItem>())
                        .Select(x => new
                        {
                            productId = x.ProductId,
                            productName = x.Product?.ProductName,
                            unitName = x.Product?.Unit?.UnitName,
                            requestedQuantity = x.Quantity,
                            remarks = x.Remarks
                        })
                });
            }
            catch (InvalidOperationException ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Loading request lines failed.");
                return Json(new { success = false, message = "The request lines could not be loaded." });
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

        private async Task FillLinesAsync(SupplierQuotationCreateModel model)
        {
            var request = await _requestForQuotationManagementService
                .GetRequestForQuotationByIdAsync(model.RequestForQuotationId);

            if (request == null)
            {
                return;
            }

            model.WarehouseName = request.BusinessLocation?.LocationName ?? string.Empty;

            model.SupplierQuotationItems = (request.RequestForQuotationItems
                ?? new List<RequestForQuotationItem>())
                .Select(x => new SupplierQuotationItemModel
                {
                    ProductId = x.ProductId,
                    ProductName = x.Product?.ProductName ?? string.Empty,
                    UnitName = x.Product?.Unit?.UnitName ?? string.Empty,
                    RequestedQuantity = x.Quantity,
                    Quantity = x.Quantity
                })
                .ToList();
        }

        /// <summary>
        /// The saved lines of a quotation, read back against what the request asked
        /// for so a short quote still shows the gap.
        /// </summary>
        private async Task<List<SupplierQuotationItemModel>> BuildEditableLinesAsync(SupplierQuotation quotation)
        {
            var request = await _requestForQuotationManagementService
                .GetRequestForQuotationByIdAsync(quotation.RequestForQuotationId);

            var requested = (request?.RequestForQuotationItems ?? new List<RequestForQuotationItem>())
                .ToDictionary(x => x.ProductId);

            var quoted = (quotation.SupplierQuotationItems ?? new List<SupplierQuotationItem>())
                .ToDictionary(x => x.ProductId);

            var rows = new List<SupplierQuotationItemModel>();

            // The request drives the grid, so a product the supplier left out still
            // appears with an empty price rather than quietly vanishing.
            foreach (var line in requested.Values)
            {
                quoted.TryGetValue(line.ProductId, out var quotedLine);

                rows.Add(new SupplierQuotationItemModel
                {
                    ProductId = line.ProductId,
                    ProductName = line.Product?.ProductName ?? string.Empty,
                    UnitName = line.Product?.Unit?.UnitName ?? string.Empty,
                    RequestedQuantity = line.Quantity,
                    Quantity = quotedLine?.Quantity ?? 0m,
                    UnitPrice = quotedLine?.UnitPrice ?? 0m,
                    DiscountAmount = quotedLine?.DiscountAmount ?? 0m,
                    Remarks = quotedLine?.Remarks ?? string.Empty
                });
            }

            return rows;
        }

        private static List<SupplierQuotationItem> ToLineRequests(IEnumerable<SupplierQuotationItemModel> items)
        {
            return items
                .Where(x => x.ProductId != Guid.Empty && x.Quantity > 0)
                .Select(x => new SupplierQuotationItem
                {
                    ProductId = x.ProductId,
                    Quantity = x.Quantity,
                    UnitPrice = x.UnitPrice,
                    DiscountAmount = x.DiscountAmount,
                    Remarks = x.Remarks ?? string.Empty
                })
                .ToList();
        }

        private async Task PopulateAsync(SupplierQuotationCreateModel model)
        {
            if (string.IsNullOrWhiteSpace(model.QuotationNo))
            {
                model.QuotationNo = await _supplierQuotationManagementService.GenerateQuotationNoAsync();
            }

            model.SetSupplierValues(await _supplierManagementService.GetActiveSupplierAsync());
            model.SetPaymentTermValues(await _paymentTermManagementService.GetAllPaymentTermAsync());

            // Only a request that has gone out can be quoted against.
            model.SetRequestForQuotationValues(
                await _requestForQuotationManagementService.GetRequestForQuotationsByStatusAsync(
                    RequestForQuotationStatus.Sent, RequestForQuotationStatus.Closed));
        }
    }
}
