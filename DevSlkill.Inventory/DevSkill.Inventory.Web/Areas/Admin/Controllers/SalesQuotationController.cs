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
    /// The priced offer made to a customer. It never touches stock and never posts to
    /// the customer account; it only says what the sale would cost while the offer is
    /// still being held.
    /// </summary>
    [Area("Admin"), Authorize]
    public class SalesQuotationController : Controller
    {
        private readonly ISalesQuotationManagementService _salesQuotationManagementService;
        private readonly ICustomerManagementService _customerManagementService;
        private readonly IBusinessLocationManagementService _businessLocationManagementService;
        private readonly IPaymentTermManagementService _paymentTermManagementService;
        private readonly ISalespersonManagementService _salespersonManagementService;
        private readonly IProductManagementService _productManagementService;
        private readonly ILogger<SalesQuotationController> _logger;

        public SalesQuotationController(
            ISalesQuotationManagementService salesQuotationManagementService,
            ICustomerManagementService customerManagementService,
            IBusinessLocationManagementService businessLocationManagementService,
            IPaymentTermManagementService paymentTermManagementService,
            ISalespersonManagementService salespersonManagementService,
            IProductManagementService productManagementService,
            ILogger<SalesQuotationController> logger)
        {
            _salesQuotationManagementService = salesQuotationManagementService;
            _customerManagementService = customerManagementService;
            _businessLocationManagementService = businessLocationManagementService;
            _paymentTermManagementService = paymentTermManagementService;
            _salespersonManagementService = salespersonManagementService;
            _productManagementService = productManagementService;
            _logger = logger;
        }

        [Authorize(Policy = "ReadPermission")]
        public IActionResult SalesQuotationList()
        {
            return View();
        }

        [HttpPost, Authorize(Policy = "ReadPermission")]
        public async Task<JsonResult> GetSalesQuotationJsonData([FromBody] SalesQuotationListModel model)
        {
            var result = await _salesQuotationManagementService.GetSalesQuotationsAsync(
                model.PageIndex, model.PageSize, model.Search,
                model.FormatSortExpression(
                    "QuotationNo",
                    "Customer.CustomerName",
                    "QuotationDate",
                    "ValidUntil",
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
                            HttpUtility.HtmlEncode(record.QuotationNo),
                            HttpUtility.HtmlEncode(record.Customer?.CustomerName),
                            HttpUtility.HtmlEncode(record.QuotationDate.ToString("dd-MM-yyyy")),
                            HttpUtility.HtmlEncode(record.ValidUntil.ToString("dd-MM-yyyy")),
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
        public async Task<IActionResult> CreateSalesQuotation()
        {
            var model = new SalesQuotationCreateModel
            {
                QuotationNo = await _salesQuotationManagementService.GenerateQuotationNoAsync()
            };

            await PopulateAsync(model);

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "CreatePermission")]
        public async Task<IActionResult> CreateSalesQuotation(SalesQuotationCreateModel model)
        {
            if (!ModelState.IsValid)
            {

                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        _logger.LogWarning(
                            "ModelState Error | Field: {Field} | Error: {Error} | Exception: {Exception}",
                            state.Key,
                            error.ErrorMessage,
                            error.Exception?.Message
                        );
                    }
                }

                await PopulateAsync(model);
                return View(model);
            }

            try
            {
                var salesQuotation = new SalesQuotation
                {
                    CustomerId = model.CustomerId,
                    QuotationDate = model.QuotationDate,
                    ValidUntil = model.ValidUntil,
                    BusinessLocationId = model.BusinessLocationId,
                    SalespersonId = model.SalespersonId,
                    PaymentTermId = model.PaymentTermId,
                    CustomerReference = model.CustomerReference,
                    OtherCharges = model.OtherCharges,
                    Notes = model.Notes,
                    Status = model.Status,
                    CreatedBy = User.Identity?.Name ?? string.Empty,
                    SalesQuotationItems = ToLineRequests(model.SalesQuotationItems)
                };

                await _salesQuotationManagementService.CreateSalesQuotationAsync(salesQuotation);

                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "Sales quotation created successfully.",
                    Type = ResponseTypes.Success
                });

                return RedirectToAction(nameof(SalesQuotationList));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                await PopulateAsync(model);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Sales quotation creation failed.");
                ModelState.AddModelError(string.Empty, "Sales quotation creation failed.");
                await PopulateAsync(model);
                return View(model);
            }
        }

        [Authorize(Policy = "UpdatePermission")]
        public async Task<IActionResult> UpdateSalesQuotation(Guid id)
        {
            var salesQuotation = await _salesQuotationManagementService.GetSalesQuotationByIdAsync(id);

            if (salesQuotation == null)
            {
                return NotFound();
            }

            var model = new SalesQuotationUpdateModel
            {
                Id = salesQuotation.Id,
                QuotationNo = salesQuotation.QuotationNo,
                CustomerId = salesQuotation.CustomerId,
                QuotationDate = salesQuotation.QuotationDate,
                ValidUntil = salesQuotation.ValidUntil,
                BusinessLocationId = salesQuotation.BusinessLocationId,
                SalespersonId = salesQuotation.SalespersonId,
                PaymentTermId = salesQuotation.PaymentTermId,
                CustomerReference = salesQuotation.CustomerReference,
                OtherCharges = salesQuotation.OtherCharges,
                Notes = salesQuotation.Notes,
                Status = salesQuotation.Status,
                SalesQuotationItems = salesQuotation.SalesQuotationItems?
                    .Select(x => new SalesQuotationItemModel
                    {
                        ProductId = x.ProductId,
                        Quantity = x.Quantity,
                        UnitPrice = x.UnitPrice,
                        DiscountAmount = x.DiscountAmount,
                        OrderedQuantity = x.OrderedQuantity,
                        Remarks = x.Remarks
                    }).ToList() ?? new List<SalesQuotationItemModel>()
            };

            if (model.SalesQuotationItems.Count == 0)
            {
                model.SalesQuotationItems.Add(new SalesQuotationItemModel());
            }

            await PopulateAsync(model);

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<IActionResult> UpdateSalesQuotation(SalesQuotationUpdateModel model)
        {
            var existing = await _salesQuotationManagementService.GetSalesQuotationByIdAsync(model.Id);

            if (existing == null)
            {
                return NotFound();
            }

            // Customer and warehouse are what the offer is. They are locked in the
            // browser and the posted values are discarded here as well.
            model.CustomerId = existing.CustomerId;
            model.BusinessLocationId = existing.BusinessLocationId;

            ModelState.Remove(nameof(model.CustomerId));
            ModelState.Remove(nameof(model.BusinessLocationId));

            if (!ModelState.IsValid)
            {
                await PopulateAsync(model);
                return View(model);
            }

            try
            {
                var salesQuotation = new SalesQuotation
                {
                    Id = model.Id,
                    CustomerId = model.CustomerId,
                    QuotationDate = model.QuotationDate,
                    ValidUntil = model.ValidUntil,
                    BusinessLocationId = model.BusinessLocationId,
                    SalespersonId = model.SalespersonId,
                    PaymentTermId = model.PaymentTermId,
                    CustomerReference = model.CustomerReference,
                    OtherCharges = model.OtherCharges,
                    Notes = model.Notes,
                    Status = model.Status,
                    SalesQuotationItems = ToLineRequests(model.SalesQuotationItems)
                };

                await _salesQuotationManagementService.UpdateSalesQuotationAsync(salesQuotation);

                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "Sales quotation updated successfully.",
                    Type = ResponseTypes.Success
                });

                return RedirectToAction(nameof(SalesQuotationList));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                await PopulateAsync(model);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Sales quotation update failed.");
                ModelState.AddModelError(string.Empty, "Sales quotation update failed.");
                await PopulateAsync(model);
                return View(model);
            }
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "DeletePermission")]
        public async Task<JsonResult> DeleteSalesQuotation(Guid id)
        {
            return await RunAsync(
                () => _salesQuotationManagementService.DeleteSalesQuotationAsync(id),
                "Sales quotation deleted successfully.",
                "Sales quotation delete failed.");
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<JsonResult> SendSalesQuotation(Guid id)
        {
            return await RunAsync(
                () => _salesQuotationManagementService.SendSalesQuotationAsync(id),
                "Sales quotation sent to the customer.",
                "Sending the sales quotation failed.");
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<JsonResult> AcceptSalesQuotation(Guid id)
        {
            return await RunAsync(
                () => _salesQuotationManagementService.AcceptSalesQuotationAsync(id),
                "Sales quotation accepted.",
                "Accepting the sales quotation failed.");
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<JsonResult> RejectSalesQuotation(Guid id, string reason)
        {
            return await RunAsync(
                () => _salesQuotationManagementService.RejectSalesQuotationAsync(id, reason),
                "Sales quotation rejected.",
                "Rejecting the sales quotation failed.");
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<JsonResult> ExpireSalesQuotation(Guid id)
        {
            return await RunAsync(
                () => _salesQuotationManagementService.ExpireSalesQuotationAsync(id),
                "Sales quotation marked as expired.",
                "Expiring the sales quotation failed.");
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<JsonResult> CancelSalesQuotation(Guid id)
        {
            return await RunAsync(
                () => _salesQuotationManagementService.CancelSalesQuotationAsync(id),
                "Sales quotation cancelled.",
                "Cancelling the sales quotation failed.");
        }

        [HttpGet, Authorize(Policy = "ReadPermission")]
        public async Task<IActionResult> GetSalesQuotationDetails(Guid id)
        {
            var salesQuotation = await _salesQuotationManagementService.GetSalesQuotationByIdAsync(id);

            if (salesQuotation == null)
            {
                return NotFound();
            }

            var result = new
            {
                id = salesQuotation.Id,
                quotationNo = salesQuotation.QuotationNo,
                quotationDate = salesQuotation.QuotationDate,
                validUntil = salesQuotation.ValidUntil,
                customerName = salesQuotation.Customer?.CustomerName,
                customerCode = salesQuotation.Customer?.CustomerCode,
                warehouse = salesQuotation.BusinessLocation?.LocationName,
                salesperson = salesQuotation.Salesperson?.SalespersonName,
                paymentTerms = salesQuotation.PaymentTerm?.TermName,
                customerReference = salesQuotation.CustomerReference,
                status = salesQuotation.Status.ToString(),
                notes = salesQuotation.Notes,
                subTotal = salesQuotation.SubTotal,
                itemDiscountTotal = salesQuotation.ItemDiscountTotal,
                totalTax = salesQuotation.TotalTax,
                otherCharges = salesQuotation.OtherCharges,
                grandTotal = salesQuotation.GrandTotal,
                salesQuotationItems = salesQuotation.SalesQuotationItems?.Select(x => new
                {
                    productName = x.Product?.ProductName,
                    unitName = x.Product?.Unit?.UnitName,
                    quantity = x.Quantity,
                    unitPrice = x.UnitPrice,
                    discountAmount = x.DiscountAmount,
                    taxRate = x.TaxRate,
                    taxAmount = x.TaxAmount,
                    lineTotal = x.LineTotal,
                    orderedQuantity = x.OrderedQuantity,
                    remainingQuantity = x.Quantity - x.OrderedQuantity
                }).ToList()
            };

            return Json(result);
        }

        /// <summary>
        /// Live preview of a single line. It runs the very same server side arithmetic
        /// used when the quotation is saved, so what is seen while typing matches what
        /// gets stored, price list and discount rules included.
        /// </summary>
        [HttpGet, Authorize(Policy = "ReadPermission")]
        public async Task<JsonResult> GetLinePreview(Guid productId, decimal quantity,
            decimal unitPrice, decimal discountAmount)
        {
            try
            {
                var preview = await _salesQuotationManagementService
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
                _logger.LogError(ex, "Sales quotation line preview failed.");
                return Json(new { success = false });
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

        private static List<SalesQuotationItem> ToLineRequests(IEnumerable<SalesQuotationItemModel> items)
        {
            return items
                .Where(x => x.ProductId.HasValue && x.ProductId.Value != Guid.Empty && x.Quantity > 0)
                .Select(x => new SalesQuotationItem
                {
                    ProductId = x.ProductId!.Value,
                    Quantity = x.Quantity,
                    UnitPrice = x.UnitPrice,
                    DiscountAmount = x.DiscountAmount,
                    Remarks = x.Remarks ?? string.Empty
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

        private async Task PopulateAsync(SalesQuotationCreateModel model)
        {
            if (string.IsNullOrWhiteSpace(model.QuotationNo))
            {
                model.QuotationNo = await _salesQuotationManagementService.GenerateQuotationNoAsync();
            }

            model.SetCustomerValues(await _customerManagementService.GetActiveCustomerAsync());
            model.SetBusinessLocationValues(await LoadWarehousesAsync());
            model.SetPaymentTermValues(await _paymentTermManagementService.GetActivePaymentTermAsync());
            model.SetSalespersonValues(await _salespersonManagementService.GetActiveSalespersonsAsync());

            // The product list follows the chosen warehouse, so nothing is offered
            // until one is picked.
            var products = await LoadWarehouseProductsAsync(model.BusinessLocationId);

            foreach (var item in model.SalesQuotationItems)
            {
                item.SetProductValues(products);
            }
        }

        private async Task PopulateAsync(SalesQuotationUpdateModel model)
        {
            model.SetCustomerValues(await _customerManagementService.GetActiveCustomerAsync());
            model.SetBusinessLocationValues(await LoadWarehousesAsync());
            model.SetPaymentTermValues(await _paymentTermManagementService.GetActivePaymentTermAsync());
            model.SetSalespersonValues(await _salespersonManagementService.GetActiveSalespersonsAsync());

            var products = await IncludeExistingLineProductsAsync(
                await LoadWarehouseProductsAsync(model.BusinessLocationId),
                model.SalesQuotationItems);

            foreach (var item in model.SalesQuotationItems)
            {
                item.SetProductValues(products);
            }
        }

        /// <summary>
        /// A product that was quoted earlier but has since been deactivated or moved to
        /// another warehouse stays in the dropdown, otherwise its line would quietly
        /// disappear on the next save.
        /// </summary>
        private async Task<IList<Product>> IncludeExistingLineProductsAsync(IList<Product> warehouseProducts,
            IEnumerable<SalesQuotationItemModel> items)
        {
            var products = warehouseProducts.ToList();

            var missingIds = items
                .Where(x => x.ProductId.HasValue && x.ProductId.Value != Guid.Empty)
                .Select(x => x.ProductId!.Value)
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
