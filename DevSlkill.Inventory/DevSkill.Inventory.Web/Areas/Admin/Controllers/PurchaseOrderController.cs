using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Infrastructure;
using DevSkill.Inventory.Infrastructure.InventoryIdentity;
using DevSkill.Inventory.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class PurchaseOrderController : Controller
    {
        private readonly IPurchaseOrderManagementService _purchaseOrderManagementService;
        private readonly IPurchaseRequisitionManagementService _purchaseRequisitionManagementService;
        private readonly ISupplierQuotationManagementService _supplierQuotationManagementService;
        private readonly IRequestForQuotationManagementService _requestForQuotationManagementService;
        private readonly ISupplierManagementService _supplierManagementService;
        private readonly IBusinessLocationManagementService _businessLocationManagementService;
        private readonly IPaymentTermManagementService _paymentTermManagementService;
        private readonly IProductManagementService _productManagementService;
        private readonly ApplicationUserManager _applicationUserManager;
        private readonly INotificationService _notificationService;
        private readonly ILogger<PurchaseOrderController> _logger;

        public PurchaseOrderController(
            IPurchaseOrderManagementService purchaseOrderManagementService,
            IPurchaseRequisitionManagementService purchaseRequisitionManagementService,
            ISupplierQuotationManagementService supplierQuotationManagementService,
            IRequestForQuotationManagementService requestForQuotationManagementService,
            ISupplierManagementService supplierManagementService,
            IBusinessLocationManagementService businessLocationManagementService,
            IPaymentTermManagementService paymentTermManagementService,
            IProductManagementService productManagementService,
            ApplicationUserManager applicationUserManager,
            ILogger<PurchaseOrderController> logger,
            INotificationService notificationService)
        {
            _purchaseOrderManagementService = purchaseOrderManagementService;
            _purchaseRequisitionManagementService = purchaseRequisitionManagementService;
            _supplierQuotationManagementService = supplierQuotationManagementService;
            _requestForQuotationManagementService = requestForQuotationManagementService;
            _supplierManagementService = supplierManagementService;
            _businessLocationManagementService = businessLocationManagementService;
            _paymentTermManagementService = paymentTermManagementService;
            _productManagementService = productManagementService;
            _applicationUserManager = applicationUserManager;
            _logger = logger;
            _notificationService = notificationService;
        }

        [Authorize(Policy = "ReadPermission")]
        public IActionResult PurchaseOrderList()
        {
            return View();
        }

        [HttpPost, Authorize(Policy = "ReadPermission")]
        public async Task<JsonResult> GetPurchaseOrderJsonData([FromBody] PurchaseOrderListModel model)
        {
            var result = await _purchaseOrderManagementService.GetPurchaseOrdersAsync(
                model.PageIndex, model.PageSize, model.Search,
                model.FormatSortExpression(
                    "PurchaseOrderNo",
                    "Supplier.SupplierName",
                    "OrderDate",
                    "ExpectedDeliveryDate",
                    "BusinessLocation.LocationName",
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
                            HttpUtility.HtmlEncode(record.PurchaseOrderNo),
                            HttpUtility.HtmlEncode(record.Supplier?.SupplierName),
                            HttpUtility.HtmlEncode(record.OrderDate.ToString("dd-MM-yyyy")),
                            HttpUtility.HtmlEncode(record.ExpectedDeliveryDate.ToString("dd-MM-yyyy")),
                            HttpUtility.HtmlEncode(record.BusinessLocation?.LocationName),
                            HttpUtility.HtmlEncode(record.GrandTotal.ToString("N2")),
                            HttpUtility.HtmlEncode(record.Status.ToString()),
                            HttpUtility.HtmlEncode(record.Id.ToString())
                        }
                    ).ToArray()
            };

            return Json(jsonData);
        }

        [Authorize(Policy = "CreatePermission")]
        public async Task<IActionResult> CreatePurchaseOrder(Guid? requisitionId, Guid? supplierQuotationId)
        {
            var model = new PurchaseOrderCreateModel
            {
                PurchaseOrderNo = await _purchaseOrderManagementService.GeneratePurchaseOrderNoAsync()
            };

            // Coming in from the requisition list pre-fills the order with everything
            // that requisition still has open.
            if (requisitionId.HasValue && requisitionId.Value != Guid.Empty)
            {
                await PrefillFromRequisitionAsync(model, requisitionId.Value);
            }

            // Coming in from an awarded quotation pre-fills the order at the price that
            // was actually won, which is the whole point of having quoted.
            if (supplierQuotationId.HasValue && supplierQuotationId.Value != Guid.Empty)
            {
                await PrefillFromQuotationAsync(model, supplierQuotationId.Value);
            }

            await PopulateAsync(model);

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "CreatePermission")]
        public async Task<IActionResult> CreatePurchaseOrder(PurchaseOrderCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateAsync(model);
                return View(model);
            }

            try
            {
                var purchaseOrder = new PurchaseOrder
                {
                    SupplierId = model.SupplierId,
                    OrderDate = model.OrderDate,
                    ExpectedDeliveryDate = model.ExpectedDeliveryDate,
                    BusinessLocationId = model.BusinessLocationId,
                    PaymentTermId = model.PaymentTermId,
                    PurchaseRequisitionId = model.PurchaseRequisitionId,
                    SupplierQuotationId = model.SupplierQuotationId,
                    OtherCharges = model.OtherCharges,
                    Notes = model.Notes,
                    Status = model.Status,
                    CreatedBy = User.Identity?.Name ?? string.Empty,
                    PurchaseOrderItems = ToLineRequests(model.PurchaseOrderItems)
                };

                var purchaseOrderId = await _purchaseOrderManagementService
                    .CreatePurchaseOrderAsync(purchaseOrder);

                await _notificationService.RaiseAsync(NotificationEvent.PurchaseOrderCreated,
                    "New purchase order",
                    $"An order for {model.PurchaseOrderItems.Count} line(s) has been placed with a supplier.",
                    $"/Admin/PurchaseOrder/UpdatePurchaseOrder/{purchaseOrderId}",
                    purchaseOrderId, User.Identity?.Name);

                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "Purchase order created successfully.",
                    Type = ResponseTypes.Success
                });

                return RedirectToAction(nameof(PurchaseOrderList));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                await PopulateAsync(model);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Purchase order creation failed.");
                ModelState.AddModelError(string.Empty, "Purchase order creation failed.");
                await PopulateAsync(model);
                return View(model);
            }
        }

        [Authorize(Policy = "UpdatePermission")]
        public async Task<IActionResult> UpdatePurchaseOrder(Guid id)
        {
            var purchaseOrder = await _purchaseOrderManagementService.GetPurchaseOrderByIdAsync(id);

            if (purchaseOrder == null)
            {
                return NotFound();
            }

            var model = new PurchaseOrderUpdateModel
            {
                Id = purchaseOrder.Id,
                PurchaseOrderNo = purchaseOrder.PurchaseOrderNo,
                SupplierId = purchaseOrder.SupplierId,
                OrderDate = purchaseOrder.OrderDate,
                ExpectedDeliveryDate = purchaseOrder.ExpectedDeliveryDate,
                BusinessLocationId = purchaseOrder.BusinessLocationId,
                PaymentTermId = purchaseOrder.PaymentTermId,
                PurchaseRequisitionId = purchaseOrder.PurchaseRequisitionId,
                PurchaseRequisitionNo = purchaseOrder.PurchaseRequisition?.RequisitionNo ?? "Direct order",
                OtherCharges = purchaseOrder.OtherCharges,
                Notes = purchaseOrder.Notes,
                Status = purchaseOrder.Status,
                PurchaseOrderItems = purchaseOrder.PurchaseOrderItems?
                    .Select(x => new PurchaseOrderItemModel
                    {
                        ProductId = x.ProductId,
                        Quantity = x.Quantity,
                        UnitPrice = x.UnitPrice,
                        DiscountAmount = x.DiscountAmount,
                        ReceivedQuantity = x.ReceivedQuantity,
                        InvoicedQuantity = x.InvoicedQuantity
                    }).ToList() ?? new List<PurchaseOrderItemModel>()
            };

            if (model.PurchaseOrderItems.Count == 0)
            {
                model.PurchaseOrderItems.Add(new PurchaseOrderItemModel());
            }

            await PopulateAsync(model);

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<IActionResult> UpdatePurchaseOrder(PurchaseOrderUpdateModel model)
        {
            var existing = await _purchaseOrderManagementService.GetPurchaseOrderByIdAsync(model.Id);

            if (existing == null)
            {
                return NotFound();
            }

            // Supplier, warehouse and the source requisition are what the order is.
            // They are locked in the browser and the posted values are discarded here
            // as well.
            model.SupplierId = existing.SupplierId;
            model.BusinessLocationId = existing.BusinessLocationId;
            model.PurchaseRequisitionId = existing.PurchaseRequisitionId;
            model.PurchaseRequisitionNo = existing.PurchaseRequisition?.RequisitionNo ?? "Direct order";

            ModelState.Remove(nameof(model.SupplierId));
            ModelState.Remove(nameof(model.BusinessLocationId));

            if (!ModelState.IsValid)
            {
                await PopulateAsync(model);
                return View(model);
            }

            try
            {
                var purchaseOrder = new PurchaseOrder
                {
                    Id = model.Id,
                    SupplierId = model.SupplierId,
                    OrderDate = model.OrderDate,
                    ExpectedDeliveryDate = model.ExpectedDeliveryDate,
                    BusinessLocationId = model.BusinessLocationId,
                    PaymentTermId = model.PaymentTermId,
                    PurchaseRequisitionId = model.PurchaseRequisitionId,
                    OtherCharges = model.OtherCharges,
                    Notes = model.Notes,
                    Status = model.Status,
                    PurchaseOrderItems = ToLineRequests(model.PurchaseOrderItems)
                };

                await _purchaseOrderManagementService.UpdatePurchaseOrderAsync(purchaseOrder);

                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "Purchase order updated successfully.",
                    Type = ResponseTypes.Success
                });

                return RedirectToAction(nameof(PurchaseOrderList));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                await PopulateAsync(model);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Purchase order update failed.");
                ModelState.AddModelError(string.Empty, "Purchase order update failed.");
                await PopulateAsync(model);
                return View(model);
            }
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "DeletePermission")]
        public async Task<JsonResult> DeletePurchaseOrder(Guid id)
        {
            return await RunAsync(
                () => _purchaseOrderManagementService.DeletePurchaseOrderAsync(id),
                "Purchase order deleted successfully.",
                "Purchase order delete failed.");
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<JsonResult> SubmitPurchaseOrder(Guid id)
        {
            return await RunAsync(
                () => _purchaseOrderManagementService.SubmitPurchaseOrderAsync(id),
                "Purchase order submitted for approval.",
                "Purchase order submit failed.");
        }

        /// <summary>
        /// Approval reuses the existing update claim; the approver is taken from the
        /// signed in user, never from the browser.
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<JsonResult> ApprovePurchaseOrder(Guid id)
        {
            var (approverId, approverName) = await GetCurrentUserAsync();

            return await RunAsync(
                () => _purchaseOrderManagementService.ApprovePurchaseOrderAsync(id, approverId, approverName),
                "Purchase order approved.",
                "Purchase order approval failed.");
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<JsonResult> RejectPurchaseOrder(Guid id, string reason)
        {
            var (approverId, approverName) = await GetCurrentUserAsync();

            return await RunAsync(
                () => _purchaseOrderManagementService.RejectPurchaseOrderAsync(id, approverId, approverName, reason),
                "Purchase order rejected.",
                "Purchase order rejection failed.");
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<JsonResult> CancelPurchaseOrder(Guid id)
        {
            return await RunAsync(
                () => _purchaseOrderManagementService.CancelPurchaseOrderAsync(id),
                "Purchase order cancelled.",
                "Purchase order cancel failed.");
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<JsonResult> ClosePurchaseOrder(Guid id)
        {
            return await RunAsync(
                () => _purchaseOrderManagementService.ClosePurchaseOrderAsync(id),
                "Purchase order closed short.",
                "Purchase order close failed.");
        }

        [HttpGet, Authorize(Policy = "ReadPermission")]
        public async Task<IActionResult> GetPurchaseOrderDetails(Guid id)
        {
            var purchaseOrder = await _purchaseOrderManagementService.GetPurchaseOrderByIdAsync(id);

            if (purchaseOrder == null)
            {
                return NotFound();
            }

            var result = new
            {
                id = purchaseOrder.Id,
                purchaseOrderNo = purchaseOrder.PurchaseOrderNo,
                orderDate = purchaseOrder.OrderDate,
                expectedDeliveryDate = purchaseOrder.ExpectedDeliveryDate,
                supplierName = purchaseOrder.Supplier?.SupplierName,
                supplierCode = purchaseOrder.Supplier?.SupplierCode,
                warehouse = purchaseOrder.BusinessLocation?.LocationName,
                paymentTerms = purchaseOrder.PaymentTerm?.TermName,
                requisitionNo = purchaseOrder.PurchaseRequisition?.RequisitionNo,
                status = purchaseOrder.Status.ToString(),
                notes = purchaseOrder.Notes,
                approvedBy = purchaseOrder.ApprovedByName,
                approvedDate = purchaseOrder.ApprovedDate,
                rejectionReason = purchaseOrder.RejectionReason,
                subTotal = purchaseOrder.SubTotal,
                itemDiscountTotal = purchaseOrder.ItemDiscountTotal,
                totalTax = purchaseOrder.TotalTax,
                otherCharges = purchaseOrder.OtherCharges,
                grandTotal = purchaseOrder.GrandTotal,
                purchaseOrderItems = purchaseOrder.PurchaseOrderItems?.Select(x => new
                {
                    productName = x.Product?.ProductName,
                    unitName = x.Product?.Unit?.UnitName,
                    quantity = x.Quantity,
                    unitPrice = x.UnitPrice,
                    discountAmount = x.DiscountAmount,
                    taxRate = x.TaxRate,
                    taxAmount = x.TaxAmount,
                    lineTotal = x.LineTotal,
                    receivedQuantity = x.ReceivedQuantity,
                    invoicedQuantity = x.InvoicedQuantity,
                    pendingQuantity = x.Quantity - x.ReceivedQuantity
                }).ToList()
            };

            return Json(result);
        }

        /// <summary>
        /// Live preview of a single line. It runs the very same server side arithmetic
        /// used when the order is saved, so what the buyer sees while typing matches
        /// what gets stored.
        /// </summary>
        [HttpGet, Authorize(Policy = "ReadPermission")]
        public async Task<JsonResult> GetLinePreview(Guid productId, decimal quantity,
            decimal unitPrice, decimal discountAmount)
        {
            try
            {
                var preview = await _purchaseOrderManagementService
                    .GetLinePreviewAsync(productId, quantity, unitPrice, discountAmount);

                if (preview == null)
                {
                    return Json(new { success = false });
                }

                return Json(new
                {
                    success = true,
                    preview.UnitName,
                    preview.CurrentStock,
                    preview.LineAmount,
                    preview.DiscountAmount,
                    preview.TaxRate,
                    preview.TaxAmount,
                    preview.LineTotal
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Purchase order line preview failed.");
                return Json(new { success = false });
            }
        }

        /// <summary>
        /// The still open lines of an approved requisition, so the buyer can drop them
        /// straight onto a new order.
        /// </summary>
        [HttpGet, Authorize(Policy = "ReadPermission")]
        public async Task<JsonResult> GetRequisitionLines(Guid requisitionId)
        {
            try
            {
                var requisition = await _purchaseRequisitionManagementService
                    .GetPurchaseRequisitionByIdAsync(requisitionId);

                if (requisition == null)
                {
                    return Json(new { success = false, message = "Purchase requisition not found." });
                }

                var lines = await _purchaseOrderManagementService.GetConvertibleRequisitionLinesAsync(requisitionId);

                return Json(new
                {
                    success = true,
                    warehouseId = requisition.BusinessLocationId,
                    lines = lines.Where(x => x.RemainingQuantity > 0).Select(x => new
                    {
                        productId = x.ProductId,
                        productName = x.ProductName,
                        remainingQuantity = x.RemainingQuantity,
                        estimatedUnitPrice = x.EstimatedUnitPrice
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

        [HttpGet, Authorize(Policy = "ReadPermission")]
        public async Task<JsonResult> GetProductsByWarehouse(Guid warehouseId)
        {
            var products = await LoadWarehouseProductsAsync(warehouseId);

            return Json(products.Select(x => new
            {
                id = x.Id,
                text = x.ProductName
            }));
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

        private async Task PrefillFromRequisitionAsync(PurchaseOrderCreateModel model, Guid requisitionId)
        {
            var requisition = await _purchaseRequisitionManagementService.GetPurchaseRequisitionByIdAsync(requisitionId);

            if (requisition == null)
            {
                return;
            }

            try
            {
                var lines = (await _purchaseOrderManagementService.GetConvertibleRequisitionLinesAsync(requisitionId))
                    .Where(x => x.RemainingQuantity > 0)
                    .ToList();

                if (lines.Count == 0)
                {
                    ModelState.AddModelError(string.Empty,
                        "This requisition has nothing left to order.");
                    return;
                }

                model.PurchaseRequisitionId = requisitionId;
                model.BusinessLocationId = requisition.BusinessLocationId;
                model.ExpectedDeliveryDate = requisition.RequiredDate < DateTime.Today
                    ? DateTime.Today
                    : requisition.RequiredDate;

                model.PurchaseOrderItems = lines.Select(x => new PurchaseOrderItemModel
                {
                    ProductId = x.ProductId,
                    Quantity = x.RemainingQuantity,
                    UnitPrice = x.EstimatedUnitPrice
                }).ToList();
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
        }

        /// <summary>
        /// Builds the order from the quotation that won its request, so the buyer does
        /// not retype a price that has already been agreed.
        /// </summary>
        private async Task PrefillFromQuotationAsync(PurchaseOrderCreateModel model, Guid supplierQuotationId)
        {
            var quotation = await _supplierQuotationManagementService
                .GetSupplierQuotationByIdAsync(supplierQuotationId);

            if (quotation == null)
            {
                return;
            }

            try
            {
                var lines = await _supplierQuotationManagementService.GetQuotationLinesAsync(supplierQuotationId);

                if (lines.Count == 0)
                {
                    ModelState.AddModelError(string.Empty, "This quotation has no lines to order.");
                    return;
                }

                model.SupplierQuotationId = supplierQuotationId;
                model.SupplierId = quotation.SupplierId;
                model.PaymentTermId = quotation.PaymentTermId;
                model.ExpectedDeliveryDate = DateTime.Today.AddDays(quotation.DeliveryDays);
                model.OtherCharges = quotation.OtherCharges;

                var request = await _requestForQuotationManagementService
                    .GetRequestForQuotationByIdAsync(quotation.RequestForQuotationId);

                if (request != null)
                {
                    model.BusinessLocationId = request.BusinessLocationId;
                    model.PurchaseRequisitionId = request.PurchaseRequisitionId;
                }

                model.PurchaseOrderItems = lines.Select(x => new PurchaseOrderItemModel
                {
                    ProductId = x.ProductId,
                    Quantity = x.Quantity,
                    UnitPrice = x.UnitPrice,
                    DiscountAmount = x.DiscountAmount
                }).ToList();
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
        }

        /// <summary>
        /// The lines of an awarded quotation, so choosing one on the order screen fills
        /// the grid without a round trip through the comparison.
        /// </summary>
        [HttpGet, Authorize(Policy = "ReadPermission")]
        public async Task<JsonResult> GetQuotationLines(Guid supplierQuotationId)
        {
            try
            {
                var quotation = await _supplierQuotationManagementService
                    .GetSupplierQuotationByIdAsync(supplierQuotationId);

                if (quotation == null)
                {
                    return Json(new { success = false, message = "Supplier quotation not found." });
                }

                var request = await _requestForQuotationManagementService
                    .GetRequestForQuotationByIdAsync(quotation.RequestForQuotationId);

                var lines = await _supplierQuotationManagementService.GetQuotationLinesAsync(supplierQuotationId);

                return Json(new
                {
                    success = true,
                    supplierId = quotation.SupplierId,
                    paymentTermId = quotation.PaymentTermId,
                    otherCharges = quotation.OtherCharges,
                    deliveryDays = quotation.DeliveryDays,
                    warehouseId = request?.BusinessLocationId,
                    purchaseRequisitionId = request?.PurchaseRequisitionId,
                    lines = lines.Select(x => new
                    {
                        productId = x.ProductId,
                        productName = x.ProductName,
                        quantity = x.Quantity,
                        unitPrice = x.UnitPrice,
                        discountAmount = x.DiscountAmount
                    })
                });
            }
            catch (InvalidOperationException ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Loading quotation lines failed.");
                return Json(new { success = false, message = "The quotation lines could not be loaded." });
            }
        }

        private static List<PurchaseOrderItem> ToLineRequests(IEnumerable<PurchaseOrderItemModel> items)
        {
            return items
                .Where(x => x.ProductId != Guid.Empty && x.Quantity > 0)
                .Select(x => new PurchaseOrderItem
                {
                    ProductId = x.ProductId,
                    Quantity = x.Quantity,
                    UnitPrice = x.UnitPrice,
                    DiscountAmount = x.DiscountAmount
                })
                .ToList();
        }

        private async Task<IList<Product>> LoadWarehouseProductsAsync(Guid warehouseId)
        {
            if (warehouseId == Guid.Empty)
            {
                return new List<Product>();
            }

            return (await _productManagementService.GetAllProductByWarehouseAsync(warehouseId))
                .Where(x => x.Status == ProductStatus.Active)
                .OrderBy(x => x.ProductName)
                .ToList();
        }

        private async Task<(Guid? id, string name)> GetCurrentUserAsync()
        {
            var user = await _applicationUserManager.GetUserAsync(User);

            return user == null
                ? (null, User.Identity?.Name ?? string.Empty)
                : (user.Id, $"{user.FirstName} {user.LastName}".Trim());
        }

        private async Task PopulateAsync(PurchaseOrderCreateModel model)
        {
            if (string.IsNullOrWhiteSpace(model.PurchaseOrderNo))
            {
                model.PurchaseOrderNo = await _purchaseOrderManagementService.GeneratePurchaseOrderNoAsync();
            }

            model.SetSupplierValues(await _supplierManagementService.GetActiveSupplierAsync());
            model.SetBusinessLocationValues(await LoadWarehousesAsync());
            model.SetPaymentTermValues(await _paymentTermManagementService.GetActivePaymentTermAsync());

            model.SetPurchaseRequisitionValues(
                await _purchaseRequisitionManagementService.GetPurchaseRequisitionsByStatusAsync(
                    PurchaseRequisitionStatus.Approved, PurchaseRequisitionStatus.PartiallyConverted));

            // Only a quotation that won its request can be ordered from.
            model.SetSupplierQuotationValues(
                await _supplierQuotationManagementService.GetAwardedQuotationsAsync());

            // The product list follows the chosen warehouse, so nothing is offered
            // until one is picked.
            var products = await LoadWarehouseProductsAsync(model.BusinessLocationId);

            foreach (var item in model.PurchaseOrderItems)
            {
                item.SetProductValues(products);
            }
        }

        private async Task PopulateAsync(PurchaseOrderUpdateModel model)
        {
            model.SetSupplierValues(await _supplierManagementService.GetActiveSupplierAsync());
            model.SetBusinessLocationValues(await LoadWarehousesAsync());
            model.SetPaymentTermValues(await _paymentTermManagementService.GetActivePaymentTermAsync());

            var products = await IncludeExistingLineProductsAsync(
                await LoadWarehouseProductsAsync(model.BusinessLocationId),
                model.PurchaseOrderItems);

            foreach (var item in model.PurchaseOrderItems)
            {
                item.SetProductValues(products);
            }
        }

        /// <summary>
        /// A product that was ordered earlier but has since been deactivated or moved
        /// to another warehouse stays in the dropdown, otherwise its line would
        /// quietly disappear on the next save.
        /// </summary>
        private async Task<IList<Product>> IncludeExistingLineProductsAsync(IList<Product> warehouseProducts,
            IEnumerable<PurchaseOrderItemModel> items)
        {
            var products = warehouseProducts.ToList();

            var missingIds = items
                .Select(x => x.ProductId)
                .Where(x => x != Guid.Empty)
                .Distinct()
                .Where(id => products.All(x => x.Id != id))
                .ToList();

            foreach (var id in missingIds)
            {
                var product = await _productManagementService.GetProductByIdAsync(id);

                if (product != null)
                {
                    products.Add(product);
                }
            }

            return products.OrderBy(x => x.ProductName).ToList();
        }

        private async Task<IList<BusinessLocation>> LoadWarehousesAsync()
        {
            return (await _businessLocationManagementService.GetAllBusinessLocationAsync())
                .Where(x => x.IsActive)
                .ToList();
        }
    }
}
