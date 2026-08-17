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
    public class RequestForQuotationController : Controller
    {
        private readonly IRequestForQuotationManagementService _requestForQuotationManagementService;
        private readonly IPurchaseRequisitionManagementService _purchaseRequisitionManagementService;
        private readonly IBusinessLocationManagementService _businessLocationManagementService;
        private readonly IProductManagementService _productManagementService;
        private readonly ILogger<RequestForQuotationController> _logger;

        public RequestForQuotationController(
            IRequestForQuotationManagementService requestForQuotationManagementService,
            IPurchaseRequisitionManagementService purchaseRequisitionManagementService,
            IBusinessLocationManagementService businessLocationManagementService,
            IProductManagementService productManagementService,
            ILogger<RequestForQuotationController> logger)
        {
            _requestForQuotationManagementService = requestForQuotationManagementService;
            _purchaseRequisitionManagementService = purchaseRequisitionManagementService;
            _businessLocationManagementService = businessLocationManagementService;
            _productManagementService = productManagementService;
            _logger = logger;
        }

        [Authorize(Policy = "ReadPermission")]
        public IActionResult RequestForQuotationList()
        {
            return View();
        }

        [HttpPost, Authorize(Policy = "ReadPermission")]
        public async Task<JsonResult> GetRequestForQuotationJsonData([FromBody] RequestForQuotationListModel model)
        {
            var result = await _requestForQuotationManagementService.GetRequestForQuotationsAsync(
                model.PageIndex, model.PageSize, model.Search,
                model.FormatSortExpression(
                    "RfqNo",
                    "RfqDate",
                    "ResponseDeadline",
                    "BusinessLocation.LocationName",
                    "PurchaseRequisition.RequisitionNo",
                    "Status",
                    "Id"));

            var jsonData = new
            {
                recordsTotal = result.total,
                recordsFiltered = result.totalDisplay,
                data = (from record in result.data
                        select new string[]
                        {
                            HttpUtility.HtmlEncode(record.RfqNo),
                            HttpUtility.HtmlEncode(record.RfqDate.ToString("dd-MM-yyyy")),
                            HttpUtility.HtmlEncode(record.ResponseDeadline.ToString("dd-MM-yyyy")),
                            HttpUtility.HtmlEncode(record.BusinessLocation?.LocationName),
                            HttpUtility.HtmlEncode(record.PurchaseRequisition?.RequisitionNo ?? "Direct"),
                            HttpUtility.HtmlEncode(record.Status.ToString()),
                            HttpUtility.HtmlEncode(record.Id.ToString())
                        }
                    ).ToArray()
            };

            return Json(jsonData);
        }

        [Authorize(Policy = "CreatePermission")]
        public async Task<IActionResult> CreateRequestForQuotation(Guid? purchaseRequisitionId)
        {
            var model = new RequestForQuotationCreateModel
            {
                RfqNo = await _requestForQuotationManagementService.GenerateRfqNoAsync()
            };

            // Coming in from the requisition list pre-fills the grid with what that
            // requisition asked for.
            if (purchaseRequisitionId.HasValue && purchaseRequisitionId.Value != Guid.Empty)
            {
                model.PurchaseRequisitionId = purchaseRequisitionId.Value;
                await FillLinesAsync(model);
            }

            await PopulateAsync(model);

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "CreatePermission")]
        public async Task<IActionResult> CreateRequestForQuotation(RequestForQuotationCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateAsync(model);
                return View(model);
            }

            try
            {
                var requestForQuotation = new RequestForQuotation
                {
                    PurchaseRequisitionId = model.PurchaseRequisitionId,
                    BusinessLocationId = model.BusinessLocationId,
                    RfqDate = model.RfqDate,
                    ResponseDeadline = model.ResponseDeadline,
                    Notes = model.Notes,
                    Status = model.Status,
                    CreatedBy = User.Identity?.Name ?? string.Empty,
                    RequestForQuotationItems = ToLineRequests(model.RequestForQuotationItems)
                };

                await _requestForQuotationManagementService.CreateRequestForQuotationAsync(requestForQuotation);

                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = model.Status == RequestForQuotationStatus.Sent
                        ? "Request for quotation sent. Supplier quotations can now be recorded against it."
                        : "Request for quotation saved as draft.",
                    Type = ResponseTypes.Success
                });

                return RedirectToAction(nameof(RequestForQuotationList));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                await PopulateAsync(model);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Request for quotation creation failed.");
                ModelState.AddModelError(string.Empty, "Request for quotation creation failed.");
                await PopulateAsync(model);
                return View(model);
            }
        }

        [Authorize(Policy = "UpdatePermission")]
        public async Task<IActionResult> UpdateRequestForQuotation(Guid id)
        {
            var requestForQuotation = await _requestForQuotationManagementService
                .GetRequestForQuotationByIdAsync(id);

            if (requestForQuotation == null)
            {
                return NotFound();
            }

            if (requestForQuotation.Status != RequestForQuotationStatus.Draft)
            {
                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = $"A {requestForQuotation.Status.ToString().ToLower()} request for quotation cannot be edited.",
                    Type = ResponseTypes.Danger
                });

                return RedirectToAction(nameof(RequestForQuotationList));
            }

            var model = new RequestForQuotationUpdateModel
            {
                Id = requestForQuotation.Id,
                RfqNo = requestForQuotation.RfqNo,
                PurchaseRequisitionId = requestForQuotation.PurchaseRequisitionId,
                BusinessLocationId = requestForQuotation.BusinessLocationId,
                RfqDate = requestForQuotation.RfqDate,
                ResponseDeadline = requestForQuotation.ResponseDeadline,
                Notes = requestForQuotation.Notes,
                Status = requestForQuotation.Status,
                RequestForQuotationItems = (requestForQuotation.RequestForQuotationItems
                    ?? new List<RequestForQuotationItem>())
                    .Select(x => new RequestForQuotationItemModel
                    {
                        ProductId = x.ProductId,
                        ProductName = x.Product?.ProductName ?? string.Empty,
                        UnitName = x.Product?.Unit?.UnitName ?? string.Empty,
                        Quantity = x.Quantity,
                        Remarks = x.Remarks
                    })
                    .ToList()
            };

            await PopulateAsync(model);

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<IActionResult> UpdateRequestForQuotation(RequestForQuotationUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateAsync(model);
                return View(model);
            }

            try
            {
                var requestForQuotation = new RequestForQuotation
                {
                    Id = model.Id,
                    PurchaseRequisitionId = model.PurchaseRequisitionId,
                    BusinessLocationId = model.BusinessLocationId,
                    RfqDate = model.RfqDate,
                    ResponseDeadline = model.ResponseDeadline,
                    Notes = model.Notes,
                    Status = model.Status,
                    RequestForQuotationItems = ToLineRequests(model.RequestForQuotationItems)
                };

                await _requestForQuotationManagementService.UpdateRequestForQuotationAsync(requestForQuotation);

                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = model.Status == RequestForQuotationStatus.Sent
                        ? "Request for quotation sent. Supplier quotations can now be recorded against it."
                        : "Request for quotation updated successfully.",
                    Type = ResponseTypes.Success
                });

                return RedirectToAction(nameof(RequestForQuotationList));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                await PopulateAsync(model);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Request for quotation update failed.");
                ModelState.AddModelError(string.Empty, "Request for quotation update failed.");
                await PopulateAsync(model);
                return View(model);
            }
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<JsonResult> SendRequestForQuotation(Guid id)
        {
            return await RunAsync(
                () => _requestForQuotationManagementService.SendRequestForQuotationAsync(id),
                "Request for quotation sent. Supplier quotations can now be recorded against it.",
                "Sending the request for quotation failed.");
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<JsonResult> CloseRequestForQuotation(Guid id)
        {
            return await RunAsync(
                () => _requestForQuotationManagementService.CloseRequestForQuotationAsync(id),
                "Request for quotation closed without an award.",
                "Closing the request for quotation failed.");
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<JsonResult> CancelRequestForQuotation(Guid id)
        {
            return await RunAsync(
                () => _requestForQuotationManagementService.CancelRequestForQuotationAsync(id),
                "Request for quotation cancelled.",
                "Cancelling the request for quotation failed.");
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "DeletePermission")]
        public async Task<JsonResult> DeleteRequestForQuotation(Guid id)
        {
            return await RunAsync(
                () => _requestForQuotationManagementService.DeleteRequestForQuotationAsync(id),
                "Request for quotation deleted successfully.",
                "Request for quotation delete failed.");
        }

        [HttpGet, Authorize(Policy = "ReadPermission")]
        public async Task<IActionResult> GetRequestForQuotationDetails(Guid id)
        {
            var requestForQuotation = await _requestForQuotationManagementService
                .GetRequestForQuotationByIdAsync(id);

            if (requestForQuotation == null)
            {
                return NotFound();
            }

            var result = new
            {
                id = requestForQuotation.Id,
                rfqNo = requestForQuotation.RfqNo,
                requisitionNo = requestForQuotation.PurchaseRequisition?.RequisitionNo ?? "Direct",
                warehouse = requestForQuotation.BusinessLocation?.LocationName,
                rfqDate = requestForQuotation.RfqDate,
                responseDeadline = requestForQuotation.ResponseDeadline,
                status = requestForQuotation.Status.ToString(),
                notes = requestForQuotation.Notes,
                requestForQuotationItems = requestForQuotation.RequestForQuotationItems?.Select(x => new
                {
                    productName = x.Product?.ProductName,
                    unitName = x.Product?.Unit?.UnitName,
                    quantity = x.Quantity,
                    remarks = x.Remarks
                }).ToList()
            };

            return Json(result);
        }

        /// <summary>
        /// The lines of an approved requisition, so the request screen can build its
        /// grid as soon as a requisition is chosen.
        /// </summary>
        [HttpGet, Authorize(Policy = "ReadPermission")]
        public async Task<JsonResult> GetRequisitionLines(Guid purchaseRequisitionId)
        {
            try
            {
                var requisition = await _purchaseRequisitionManagementService
                    .GetPurchaseRequisitionByIdAsync(purchaseRequisitionId);

                if (requisition == null)
                {
                    return Json(new { success = false, message = "Purchase requisition not found." });
                }

                return Json(new
                {
                    success = true,
                    warehouseId = requisition.BusinessLocationId,
                    lines = (requisition.PurchaseRequisitionItems ?? new List<PurchaseRequisitionItem>())
                        .Select(x => new
                        {
                            productId = x.ProductId,
                            productName = x.Product?.ProductName,
                            unitName = x.Product?.Unit?.UnitName,
                            quantity = x.Quantity,
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
                _logger.LogError(ex, "Loading requisition lines failed.");
                return Json(new { success = false, message = "The requisition lines could not be loaded." });
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

        private async Task FillLinesAsync(RequestForQuotationCreateModel model)
        {
            var requisition = await _purchaseRequisitionManagementService
                .GetPurchaseRequisitionByIdAsync(model.PurchaseRequisitionId!.Value);

            if (requisition == null)
            {
                return;
            }

            model.BusinessLocationId = requisition.BusinessLocationId;

            model.RequestForQuotationItems = (requisition.PurchaseRequisitionItems
                ?? new List<PurchaseRequisitionItem>())
                .Select(x => new RequestForQuotationItemModel
                {
                    ProductId = x.ProductId,
                    ProductName = x.Product?.ProductName ?? string.Empty,
                    UnitName = x.Product?.Unit?.UnitName ?? string.Empty,
                    Quantity = x.Quantity,
                    Remarks = x.Remarks
                })
                .ToList();
        }

        private static List<RequestForQuotationItem> ToLineRequests(
            IEnumerable<RequestForQuotationItemModel> items)
        {
            return items
                .Where(x => x.ProductId != Guid.Empty && x.Quantity > 0)
                .Select(x => new RequestForQuotationItem
                {
                    ProductId = x.ProductId,
                    Quantity = x.Quantity,
                    Remarks = x.Remarks ?? string.Empty
                })
                .ToList();
        }

        private async Task PopulateAsync(RequestForQuotationCreateModel model)
        {
            if (string.IsNullOrWhiteSpace(model.RfqNo))
            {
                model.RfqNo = await _requestForQuotationManagementService.GenerateRfqNoAsync();
            }

            model.SetBusinessLocationValues(await _businessLocationManagementService.GetAllBusinessLocationAsync());
            model.SetProductValues(await _productManagementService.GetAllProductAsync());

            // Only an approved requisition can be sent out for quotation.
            model.SetPurchaseRequisitionValues(
                await _purchaseRequisitionManagementService.GetPurchaseRequisitionsByStatusAsync(
                    PurchaseRequisitionStatus.Approved, PurchaseRequisitionStatus.PartiallyConverted));
        }

        private async Task PopulateAsync(RequestForQuotationUpdateModel model)
        {
            model.SetBusinessLocationValues(await _businessLocationManagementService.GetAllBusinessLocationAsync());
            model.SetProductValues(await _productManagementService.GetAllProductAsync());

            model.SetPurchaseRequisitionValues(
                await _purchaseRequisitionManagementService.GetPurchaseRequisitionsByStatusAsync(
                    PurchaseRequisitionStatus.Approved, PurchaseRequisitionStatus.PartiallyConverted));
        }
    }
}
