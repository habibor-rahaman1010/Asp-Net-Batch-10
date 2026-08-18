using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Infrastructure;
using DevSkill.Inventory.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// The firm commitment to sell. Raising or confirming an order never changes
    /// stock; only a reservation holds it and only a delivery takes it out.
    /// </summary>
    [Area("Admin"), Authorize]
    public class SalesOrderController : Controller
    {
        private readonly ISalesOrderManagementService _salesOrderManagementService;
        private readonly ISalesQuotationManagementService _salesQuotationManagementService;
        private readonly ICustomerManagementService _customerManagementService;
        private readonly IBusinessLocationManagementService _businessLocationManagementService;
        private readonly IPaymentTermManagementService _paymentTermManagementService;
        private readonly ISalespersonManagementService _salespersonManagementService;
        private readonly IProductManagementService _productManagementService;
        private readonly INotificationService _notificationService;
        private readonly ILogger<SalesOrderController> _logger;

        public SalesOrderController(
            ISalesOrderManagementService salesOrderManagementService,
            ISalesQuotationManagementService salesQuotationManagementService,
            ICustomerManagementService customerManagementService,
            IBusinessLocationManagementService businessLocationManagementService,
            IPaymentTermManagementService paymentTermManagementService,
            ISalespersonManagementService salespersonManagementService,
            IProductManagementService productManagementService,
            ILogger<SalesOrderController> logger,
            INotificationService notificationService)
        {
            _salesOrderManagementService = salesOrderManagementService;
            _salesQuotationManagementService = salesQuotationManagementService;
            _customerManagementService = customerManagementService;
            _businessLocationManagementService = businessLocationManagementService;
            _paymentTermManagementService = paymentTermManagementService;
            _salespersonManagementService = salespersonManagementService;
            _productManagementService = productManagementService;
            _logger = logger;
            _notificationService = notificationService;
        }

        [Authorize(Policy = "ReadPermission")]
        public IActionResult SalesOrderList()
        {
            return View();
        }

        [HttpPost, Authorize(Policy = "ReadPermission")]
        public async Task<JsonResult> GetSalesOrderJsonData([FromBody] SalesOrderListModel model)
        {
            var result = await _salesOrderManagementService.GetSalesOrdersAsync(
                model.PageIndex, model.PageSize, model.Search,
                model.FormatSortExpression(
                    "SalesOrderNo",
                    "Customer.CustomerName",
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
                            HttpUtility.HtmlEncode(record.SalesOrderNo),
                            HttpUtility.HtmlEncode(record.Customer?.CustomerName),
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
        public async Task<IActionResult> CreateSalesOrder(Guid? salesQuotationId)
        {
            var model = new SalesOrderCreateModel
            {
                SalesOrderNo = await _salesOrderManagementService.GenerateSalesOrderNoAsync()
            };

            // Coming in from an accepted quotation pre-fills the order at the price the
            // customer already agreed to, which is the whole point of having quoted.
            if (salesQuotationId.HasValue && salesQuotationId.Value != Guid.Empty)
            {
                await PrefillFromQuotationAsync(model, salesQuotationId.Value);
            }

            await PopulateAsync(model);

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "CreatePermission")]
        public async Task<IActionResult> CreateSalesOrder(SalesOrderCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateAsync(model);
                return View(model);
            }

            try
            {
                var salesOrder = new SalesOrder
                {
                    CustomerId = model.CustomerId,
                    OrderDate = model.OrderDate,
                    ExpectedDeliveryDate = model.ExpectedDeliveryDate,
                    BusinessLocationId = model.BusinessLocationId,
                    SalespersonId = model.SalespersonId,
                    PaymentTermId = model.PaymentTermId,
                    SalesQuotationId = model.SalesQuotationId,
                    CustomerReference = model.CustomerReference,
                    OtherCharges = model.OtherCharges,
                    Notes = model.Notes,
                    Status = model.Status,
                    CreatedBy = User.Identity?.Name ?? string.Empty,
                    SalesOrderItems = ToLineRequests(model.SalesOrderItems)
                };

                var salesOrderId = await _salesOrderManagementService.CreateSalesOrderAsync(salesOrder);

                await _notificationService.RaiseAsync(NotificationEvent.SalesOrderCreated,
                    "New sales order",
                    $"An order with {model.SalesOrderItems.Count} line(s) has been entered.",
                    $"/Admin/SalesOrder/UpdateSalesOrder/{salesOrderId}",
                    salesOrderId, User.Identity?.Name);

                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "Sales order created successfully.",
                    Type = ResponseTypes.Success
                });

                return RedirectToAction(nameof(SalesOrderList));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                await PopulateAsync(model);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Sales order creation failed.");
                ModelState.AddModelError(string.Empty, "Sales order creation failed.");
                await PopulateAsync(model);
                return View(model);
            }
        }

        [Authorize(Policy = "UpdatePermission")]
        public async Task<IActionResult> UpdateSalesOrder(Guid id)
        {
            var salesOrder = await _salesOrderManagementService.GetSalesOrderByIdAsync(id);

            if (salesOrder == null)
            {
                return NotFound();
            }

            var model = new SalesOrderUpdateModel
            {
                Id = salesOrder.Id,
                SalesOrderNo = salesOrder.SalesOrderNo,
                CustomerId = salesOrder.CustomerId,
                OrderDate = salesOrder.OrderDate,
                ExpectedDeliveryDate = salesOrder.ExpectedDeliveryDate,
                BusinessLocationId = salesOrder.BusinessLocationId,
                SalespersonId = salesOrder.SalespersonId,
                PaymentTermId = salesOrder.PaymentTermId,
                SalesQuotationId = salesOrder.SalesQuotationId,
                SalesQuotationNo = salesOrder.SalesQuotation?.QuotationNo ?? "Direct order",
                CustomerReference = salesOrder.CustomerReference,
                OtherCharges = salesOrder.OtherCharges,
                Notes = salesOrder.Notes,
                Status = salesOrder.Status,
                SalesOrderItems = salesOrder.SalesOrderItems?
                    .Select(x => new SalesOrderItemModel
                    {
                        ProductId = x.ProductId,
                        Quantity = x.Quantity,
                        UnitPrice = x.UnitPrice,
                        DiscountAmount = x.DiscountAmount,
                        DeliveredQuantity = x.DeliveredQuantity,
                        InvoicedQuantity = x.InvoicedQuantity
                    }).ToList() ?? new List<SalesOrderItemModel>()
            };

            if (model.SalesOrderItems.Count == 0)
            {
                model.SalesOrderItems.Add(new SalesOrderItemModel());
            }

            await PopulateAsync(model);

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<IActionResult> UpdateSalesOrder(SalesOrderUpdateModel model)
        {
            var existing = await _salesOrderManagementService.GetSalesOrderByIdAsync(model.Id);

            if (existing == null)
            {
                return NotFound();
            }

            // Customer, warehouse and the source quotation are what the order is. They
            // are locked in the browser and the posted values are discarded here too.
            model.CustomerId = existing.CustomerId;
            model.BusinessLocationId = existing.BusinessLocationId;
            model.SalesQuotationId = existing.SalesQuotationId;
            model.SalesQuotationNo = existing.SalesQuotation?.QuotationNo ?? "Direct order";

            ModelState.Remove(nameof(model.CustomerId));
            ModelState.Remove(nameof(model.BusinessLocationId));

            if (!ModelState.IsValid)
            {
                await PopulateAsync(model);
                return View(model);
            }

            try
            {
                var salesOrder = new SalesOrder
                {
                    Id = model.Id,
                    CustomerId = model.CustomerId,
                    OrderDate = model.OrderDate,
                    ExpectedDeliveryDate = model.ExpectedDeliveryDate,
                    BusinessLocationId = model.BusinessLocationId,
                    SalespersonId = model.SalespersonId,
                    PaymentTermId = model.PaymentTermId,
                    SalesQuotationId = model.SalesQuotationId,
                    CustomerReference = model.CustomerReference,
                    OtherCharges = model.OtherCharges,
                    Notes = model.Notes,
                    Status = model.Status,
                    SalesOrderItems = ToLineRequests(model.SalesOrderItems)
                };

                await _salesOrderManagementService.UpdateSalesOrderAsync(salesOrder);

                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "Sales order updated successfully.",
                    Type = ResponseTypes.Success
                });

                return RedirectToAction(nameof(SalesOrderList));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                await PopulateAsync(model);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Sales order update failed.");
                ModelState.AddModelError(string.Empty, "Sales order update failed.");
                await PopulateAsync(model);
                return View(model);
            }
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "DeletePermission")]
        public async Task<JsonResult> DeleteSalesOrder(Guid id)
        {
            return await RunAsync(
                () => _salesOrderManagementService.DeleteSalesOrderAsync(id),
                "Sales order deleted successfully.",
                "Sales order delete failed.");
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<JsonResult> ConfirmSalesOrder(Guid id)
        {
            return await RunAsync(
                async () =>
                {
                    await _salesOrderManagementService.ConfirmSalesOrderAsync(id);

                    await _notificationService.RaiseAsync(NotificationEvent.SalesOrderConfirmed,
                        "Sales order confirmed",
                        "An order has been confirmed and can now be reserved and delivered against.",
                        "/Admin/SalesOrder/SalesOrderList",
                        id, User.Identity?.Name);
                },
                "Sales order confirmed. Stock can now be reserved and delivered against it.",
                "Sales order confirmation failed.");
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<JsonResult> CancelSalesOrder(Guid id)
        {
            return await RunAsync(
                () => _salesOrderManagementService.CancelSalesOrderAsync(id),
                "Sales order cancelled. Any stock it was holding has been released.",
                "Sales order cancel failed.");
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<JsonResult> CloseSalesOrder(Guid id)
        {
            return await RunAsync(
                () => _salesOrderManagementService.CloseSalesOrderAsync(id),
                "Sales order closed short. Any remaining hold has been released.",
                "Sales order close failed.");
        }

        [HttpGet, Authorize(Policy = "ReadPermission")]
        public async Task<IActionResult> GetSalesOrderDetails(Guid id)
        {
            var salesOrder = await _salesOrderManagementService.GetSalesOrderByIdAsync(id);

            if (salesOrder == null)
            {
                return NotFound();
            }

            var result = new
            {
                id = salesOrder.Id,
                salesOrderNo = salesOrder.SalesOrderNo,
                orderDate = salesOrder.OrderDate,
                expectedDeliveryDate = salesOrder.ExpectedDeliveryDate,
                customerName = salesOrder.Customer?.CustomerName,
                customerCode = salesOrder.Customer?.CustomerCode,
                warehouse = salesOrder.BusinessLocation?.LocationName,
                salesperson = salesOrder.Salesperson?.SalespersonName,
                paymentTerms = salesOrder.PaymentTerm?.TermName,
                quotationNo = salesOrder.SalesQuotation?.QuotationNo,
                customerReference = salesOrder.CustomerReference,
                status = salesOrder.Status.ToString(),
                notes = salesOrder.Notes,
                subTotal = salesOrder.SubTotal,
                itemDiscountTotal = salesOrder.ItemDiscountTotal,
                totalTax = salesOrder.TotalTax,
                otherCharges = salesOrder.OtherCharges,
                grandTotal = salesOrder.GrandTotal,
                salesOrderItems = salesOrder.SalesOrderItems?.Select(x => new
                {
                    productName = x.Product?.ProductName,
                    unitName = x.Product?.Unit?.UnitName,
                    quantity = x.Quantity,
                    unitPrice = x.UnitPrice,
                    discountAmount = x.DiscountAmount,
                    taxRate = x.TaxRate,
                    taxAmount = x.TaxAmount,
                    lineTotal = x.LineTotal,
                    deliveredQuantity = x.DeliveredQuantity,
                    invoicedQuantity = x.InvoicedQuantity,
                    pendingQuantity = x.Quantity - x.DeliveredQuantity
                }).ToList()
            };

            return Json(result);
        }

        /// <summary>
        /// Live preview of a single line, run through the very same server side
        /// arithmetic used when the order is saved.
        /// </summary>
        [HttpGet, Authorize(Policy = "ReadPermission")]
        public async Task<JsonResult> GetLinePreview(Guid productId, decimal quantity,
            decimal unitPrice, decimal discountAmount)
        {
            try
            {
                var preview = await _salesOrderManagementService
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
                    preview.AvailableStock,
                    preview.SellingPrice,
                    preview.LineAmount,
                    preview.DiscountAmount,
                    preview.TaxRate,
                    preview.TaxAmount,
                    preview.LineTotal
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Sales order line preview failed.");
                return Json(new { success = false });
            }
        }

        /// <summary>
        /// The still open lines of an accepted quotation, so choosing one on the order
        /// screen fills the grid without a round trip.
        /// </summary>
        [HttpGet, Authorize(Policy = "ReadPermission")]
        public async Task<JsonResult> GetQuotationLines(Guid salesQuotationId)
        {
            try
            {
                var quotation = await _salesQuotationManagementService
                    .GetSalesQuotationByIdAsync(salesQuotationId);

                if (quotation == null)
                {
                    return Json(new { success = false, message = "Sales quotation not found." });
                }

                var lines = await _salesQuotationManagementService
                    .GetConvertibleQuotationLinesAsync(salesQuotationId);

                return Json(new
                {
                    success = true,
                    customerId = quotation.CustomerId,
                    warehouseId = quotation.BusinessLocationId,
                    salespersonId = quotation.SalespersonId,
                    paymentTermId = quotation.PaymentTermId,
                    customerReference = quotation.CustomerReference,
                    otherCharges = quotation.OtherCharges,
                    lines = lines.Where(x => x.RemainingQuantity > 0).Select(x => new
                    {
                        productId = x.ProductId,
                        productName = x.ProductName,
                        remainingQuantity = x.RemainingQuantity,
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

        /// <summary>
        /// Builds the order from the offer the customer accepted, so nobody retypes a
        /// price that has already been agreed.
        /// </summary>
        private async Task PrefillFromQuotationAsync(SalesOrderCreateModel model, Guid salesQuotationId)
        {
            var quotation = await _salesQuotationManagementService.GetSalesQuotationByIdAsync(salesQuotationId);

            if (quotation == null)
            {
                return;
            }

            try
            {
                var lines = (await _salesQuotationManagementService
                    .GetConvertibleQuotationLinesAsync(salesQuotationId))
                    .Where(x => x.RemainingQuantity > 0)
                    .ToList();

                if (lines.Count == 0)
                {
                    ModelState.AddModelError(string.Empty, "This quotation has nothing left to order.");
                    return;
                }

                model.SalesQuotationId = salesQuotationId;
                model.CustomerId = quotation.CustomerId;
                model.BusinessLocationId = quotation.BusinessLocationId;
                model.SalespersonId = quotation.SalespersonId;
                model.PaymentTermId = quotation.PaymentTermId;
                model.CustomerReference = quotation.CustomerReference;
                model.OtherCharges = quotation.OtherCharges;

                model.SalesOrderItems = lines.Select(x => new SalesOrderItemModel
                {
                    ProductId = x.ProductId,
                    Quantity = x.RemainingQuantity,
                    UnitPrice = x.UnitPrice,
                    DiscountAmount = x.DiscountAmount
                }).ToList();
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
        }

        private static List<SalesOrderItem> ToLineRequests(IEnumerable<SalesOrderItemModel> items)
        {
            return items
                .Where(x => x.ProductId != Guid.Empty && x.Quantity > 0)
                .Select(x => new SalesOrderItem
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

        private async Task PopulateAsync(SalesOrderCreateModel model)
        {
            if (string.IsNullOrWhiteSpace(model.SalesOrderNo))
            {
                model.SalesOrderNo = await _salesOrderManagementService.GenerateSalesOrderNoAsync();
            }

            model.SetCustomerValues(await _customerManagementService.GetActiveCustomerAsync());
            model.SetBusinessLocationValues(await LoadWarehousesAsync());
            model.SetPaymentTermValues(await _paymentTermManagementService.GetActivePaymentTermAsync());
            model.SetSalespersonValues(await _salespersonManagementService.GetActiveSalespersonsAsync());

            // Only an offer the customer has taken up can be ordered from.
            model.SetSalesQuotationValues(
                await _salesQuotationManagementService.GetSalesQuotationsByStatusAsync(
                    SalesQuotationStatus.Accepted));

            var products = await LoadWarehouseProductsAsync(model.BusinessLocationId);

            foreach (var item in model.SalesOrderItems)
            {
                item.SetProductValues(products);
            }
        }

        private async Task PopulateAsync(SalesOrderUpdateModel model)
        {
            model.SetCustomerValues(await _customerManagementService.GetActiveCustomerAsync());
            model.SetBusinessLocationValues(await LoadWarehousesAsync());
            model.SetPaymentTermValues(await _paymentTermManagementService.GetActivePaymentTermAsync());
            model.SetSalespersonValues(await _salespersonManagementService.GetActiveSalespersonsAsync());

            var products = await IncludeExistingLineProductsAsync(
                await LoadWarehouseProductsAsync(model.BusinessLocationId),
                model.SalesOrderItems);

            foreach (var item in model.SalesOrderItems)
            {
                item.SetProductValues(products);
            }
        }

        /// <summary>
        /// A product that was sold earlier but has since been deactivated or moved to
        /// another warehouse stays in the dropdown, otherwise its line would quietly
        /// disappear on the next save.
        /// </summary>
        private async Task<IList<Product>> IncludeExistingLineProductsAsync(IList<Product> warehouseProducts,
            IEnumerable<SalesOrderItemModel> items)
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
