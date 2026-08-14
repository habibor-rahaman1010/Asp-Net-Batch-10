using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Infrastructure;
using DevSkill.Inventory.Infrastructure.InventoryIdentity;
using DevSkill.Inventory.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Web;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class ProformaInvoiceController : Controller
    {
        private readonly IProformaInvoiceManagementService _proformaInvoiceManagementService;
        private readonly ICustomerManagementService _customerManagementService;
        private readonly IBusinessLocationManagementService _businessLocationManagementService;
        private readonly IProductManagementService _productManagementService;
        private readonly IPriceListManagementService _priceListManagementService;
        private readonly IDiscountRuleManagementService _discountRuleManagementService;
        private readonly IPaymentTermManagementService _paymentTermManagementService;
        private readonly ApplicationUserManager _applicationUserManager;
        private readonly ILogger<ProformaInvoiceController> _logger;

        public ProformaInvoiceController(IProformaInvoiceManagementService proformaInvoiceManagementService,
            ICustomerManagementService customerManagementService,
            IBusinessLocationManagementService businessLocationManagementService,
            IProductManagementService productManagementService,
            IPriceListManagementService priceListManagementService,
            IDiscountRuleManagementService discountRuleManagementService,
            IPaymentTermManagementService paymentTermManagementService,
            ApplicationUserManager applicationUserManager,
            ILogger<ProformaInvoiceController> logger)
        {
            _proformaInvoiceManagementService = proformaInvoiceManagementService;
            _customerManagementService = customerManagementService;
            _businessLocationManagementService = businessLocationManagementService;
            _productManagementService = productManagementService;
            _priceListManagementService = priceListManagementService;
            _discountRuleManagementService = discountRuleManagementService;
            _paymentTermManagementService = paymentTermManagementService;
            _applicationUserManager = applicationUserManager;
            _logger = logger;
        }

        [Authorize(Policy = "CreatePermission")]
        public async Task<IActionResult> CreateProformaInvoice()
        {
            var model = new ProformaInvoiceCreateModel
            {
                ProformaNo = await _proformaInvoiceManagementService.GenerateProformaNoAsync()
            };

            await PopulateAsync(model);

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "CreatePermission")]
        public async Task<IActionResult> CreateProformaInvoice(ProformaInvoiceCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateAsync(model);
                return View(model);
            }

            try
            {
                var proformaInvoice = new ProformaInvoice
                {
                    CustomerId = model.CustomerId,
                    ProformaDate = model.ProformaDate,
                    ValidUntil = model.ValidUntil,
                    BusinessLocationId = model.BusinessLocationId,
                    SalespersonId = model.SalespersonId == Guid.Empty ? null : model.SalespersonId,
                    SalespersonName = await ResolveSalespersonNameAsync(model.SalespersonId),
                    PaymentTermId = model.PaymentTermId,
                    Notes = model.Notes,
                    Status = model.Status,
                    CreatedBy = User.Identity?.Name ?? string.Empty,
                    ProformaInvoiceItems = ToLineRequests(model.ProformaInvoiceItems)
                };

                await _proformaInvoiceManagementService.CreateProformaInvoiceAsync(proformaInvoice);

                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "Proforma invoice created successfully.",
                    Type = ResponseTypes.Success
                });

                return RedirectToAction(nameof(ProformaInvoiceList));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                await PopulateAsync(model);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Proforma invoice creation failed.");
                ModelState.AddModelError(string.Empty, "Proforma invoice creation failed.");
                await PopulateAsync(model);
                return View(model);
            }
        }

        [Authorize(Policy = "UpdatePermission")]
        public async Task<IActionResult> UpdateProformaInvoice(Guid id)
        {
            var proformaInvoice = await _proformaInvoiceManagementService.GetProformaInvoiceByIdAsync(id);

            if (proformaInvoice == null)
            {
                return NotFound();
            }

            var model = new ProformaInvoiceUpdateModel
            {
                Id = proformaInvoice.Id,
                ProformaNo = proformaInvoice.ProformaNo,
                CustomerId = proformaInvoice.CustomerId,
                ProformaDate = proformaInvoice.ProformaDate,
                ValidUntil = proformaInvoice.ValidUntil,
                BusinessLocationId = proformaInvoice.BusinessLocationId,
                SalespersonId = proformaInvoice.SalespersonId,
                PaymentTermId = proformaInvoice.PaymentTermId,
                Notes = proformaInvoice.Notes,
                Status = proformaInvoice.Status,
                ProformaInvoiceItems = proformaInvoice.ProformaInvoiceItems?
                    .Select(x => new ProformaInvoiceItemModel
                    {
                        ProductId = x.ProductId,
                        Quantity = x.Quantity
                    }).ToList() ?? new List<ProformaInvoiceItemModel>()
            };

            if (model.ProformaInvoiceItems.Count == 0)
            {
                model.ProformaInvoiceItems.Add(new ProformaInvoiceItemModel());
            }

            await PopulateAsync(model);

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<IActionResult> UpdateProformaInvoice(ProformaInvoiceUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateAsync(model);
                return View(model);
            }

            try
            {
                var proformaInvoice = new ProformaInvoice
                {
                    Id = model.Id,
                    CustomerId = model.CustomerId,
                    ProformaDate = model.ProformaDate,
                    ValidUntil = model.ValidUntil,
                    BusinessLocationId = model.BusinessLocationId,
                    SalespersonId = model.SalespersonId == Guid.Empty ? null : model.SalespersonId,
                    SalespersonName = await ResolveSalespersonNameAsync(model.SalespersonId),
                    PaymentTermId = model.PaymentTermId,
                    Notes = model.Notes,
                    Status = model.Status,
                    ProformaInvoiceItems = ToLineRequests(model.ProformaInvoiceItems)
                };

                await _proformaInvoiceManagementService.UpdateProformaInvoiceAsync(proformaInvoice);

                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "Proforma invoice updated successfully.",
                    Type = ResponseTypes.Success
                });

                return RedirectToAction(nameof(ProformaInvoiceList));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                await PopulateAsync(model);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Proforma invoice update failed.");
                ModelState.AddModelError(string.Empty, "Proforma invoice update failed.");
                await PopulateAsync(model);
                return View(model);
            }
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "DeletePermission")]
        public async Task<JsonResult> DeleteProformaInvoice(Guid id)
        {
            try
            {
                await _proformaInvoiceManagementService.DeleteProformaInvoiceAsync(id);

                return Json(new
                {
                    success = true,
                    message = "Proforma invoice deleted successfully."
                });
            }
            catch (InvalidOperationException ex)
            {
                return Json(new
                {
                    success = false,
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Proforma invoice delete failed.");

                return Json(new
                {
                    success = false,
                    message = "Proforma invoice delete failed."
                });
            }
        }

        [Authorize(Policy = "ReadPermission")]
        public IActionResult ProformaInvoiceList()
        {
            return View();
        }

        [HttpPost, Authorize(Policy = "ReadPermission")]
        public async Task<JsonResult> GetProformaInvoiceJsonData([FromBody] ProformaInvoiceListModel model)
        {
            var result = await _proformaInvoiceManagementService.GetProformaInvoicesAsync(model.PageIndex, model.PageSize, model.Search,
                model.FormatSortExpression(
                    "ProformaNo",
                    "Customer.CustomerName",
                    "ProformaDate",
                    "ValidUntil",
                    "BusinessLocation.LocationName",
                    "GrandTotal",
                    "Status",
                    "Id"));

            var proformaInvoiceJsonData = new
            {
                recordsTotal = result.total,
                recordsFiltered = result.totalDisplay,
                data = (from record in result.data
                        select new string[]
                        {
                            HttpUtility.HtmlEncode(record.ProformaNo),
                            HttpUtility.HtmlEncode(record.Customer?.CustomerName),
                            HttpUtility.HtmlEncode(record.ProformaDate.ToString("dd-MM-yyyy")),
                            HttpUtility.HtmlEncode(record.ValidUntil.ToString("dd-MM-yyyy")),
                            HttpUtility.HtmlEncode(record.BusinessLocation?.LocationName),
                            HttpUtility.HtmlEncode(record.GrandTotal.ToString("N2")),
                            HttpUtility.HtmlEncode(record.Status.ToString()),
                            HttpUtility.HtmlEncode(record.Id.ToString())
                        }
                    ).ToArray()
            };

            return Json(proformaInvoiceJsonData);
        }

        [HttpGet, Authorize(Policy = "ReadPermission")]
        public async Task<IActionResult> GetProformaInvoiceDetails(Guid id)
        {
            var proformaInvoice = await _proformaInvoiceManagementService.GetProformaInvoiceByIdAsync(id);

            if (proformaInvoice == null)
            {
                return NotFound();
            }

            var result = new
            {
                id = proformaInvoice.Id,
                proformaNo = proformaInvoice.ProformaNo,
                proformaDate = proformaInvoice.ProformaDate,
                validUntil = proformaInvoice.ValidUntil,
                status = proformaInvoice.Status.ToString(),
                paymentTerms = proformaInvoice.PaymentTerm?.TermName,
                notes = proformaInvoice.Notes,
                salespersonName = proformaInvoice.SalespersonName,
                customerName = proformaInvoice.Customer?.CustomerName,
                customerCode = proformaInvoice.Customer?.CustomerCode,
                warehouse = proformaInvoice.BusinessLocation?.LocationName,
                subTotal = proformaInvoice.SubTotal,
                itemDiscountTotal = proformaInvoice.ItemDiscountTotal,
                orderDiscount = proformaInvoice.OrderDiscount,
                totalTax = proformaInvoice.TotalTax,
                grandTotal = proformaInvoice.GrandTotal,
                proformaInvoiceItems = proformaInvoice.ProformaInvoiceItems?.Select(x => new
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
        /// Live preview of a single line. It runs the very same server side pricing
        /// used when the proforma invoice is saved, so what the user sees while typing
        /// matches what gets stored.
        /// </summary>
        [HttpGet, Authorize(Policy = "ReadPermission")]
        public async Task<JsonResult> GetLinePreview(Guid productId, Guid customerId, decimal quantity)
        {
            if (productId == Guid.Empty || customerId == Guid.Empty || quantity <= 0)
            {
                return Json(new { success = false });
            }

            try
            {
                var customer = await _customerManagementService.GetCustomerByIdAsync(customerId);
                var product = await _productManagementService.GetProductByIdAsync(productId);

                if (customer == null || product == null)
                {
                    return Json(new { success = false });
                }

                var onDate = DateTime.Now;

                var unitPrice = await _priceListManagementService.GetEffectiveUnitPriceAsync(
                    productId, customer.CustomerType, quantity, onDate);

                var lineAmount = Math.Round(unitPrice * quantity, 2, MidpointRounding.AwayFromZero);

                var discountAmount = await _discountRuleManagementService.CalculateLineDiscountAsync(
                    productId, customerId, customer.CustomerType, quantity, lineAmount, onDate);

                var taxRate = product.ApplicableTax?.TaxRate ?? 0m;
                var taxAmount = Math.Round((lineAmount - discountAmount) * taxRate / 100m, 2, MidpointRounding.AwayFromZero);

                return Json(new
                {
                    success = true,
                    availableQuantity = product.CurrentStock - product.ReservedStock,
                    unitName = product.Unit?.UnitName,
                    unitPrice,
                    discountAmount,
                    taxRate,
                    taxAmount,
                    lineTotal = lineAmount - discountAmount + taxAmount
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Proforma invoice line preview failed.");
                return Json(new { success = false });
            }
        }

        /// <summary>
        /// Only product and quantity travel to the service; it prices the lines itself.
        /// </summary>
        private static List<ProformaInvoiceItem> ToLineRequests(IEnumerable<ProformaInvoiceItemModel> items)
        {
            return items
                .Where(x => x.ProductId != Guid.Empty && x.Quantity > 0)
                .Select(x => new ProformaInvoiceItem
                {
                    ProductId = x.ProductId,
                    Quantity = x.Quantity
                })
                .ToList();
        }

        private async Task<string> ResolveSalespersonNameAsync(Guid? salespersonId)
        {
            if (!salespersonId.HasValue || salespersonId == Guid.Empty)
            {
                return string.Empty;
            }

            var salesperson = await _applicationUserManager.FindByIdAsync(salespersonId.Value.ToString());

            return salesperson == null
                ? string.Empty
                : $"{salesperson.FirstName} {salesperson.LastName}".Trim();
        }

        private async Task PopulateAsync(ProformaInvoiceCreateModel model)
        {
            if (string.IsNullOrWhiteSpace(model.ProformaNo))
            {
                model.ProformaNo = await _proformaInvoiceManagementService.GenerateProformaNoAsync();
            }

            var lookups = await LoadLookupsAsync();

            model.SetCustomerValues(lookups.Customers);
            model.SetBusinessLocationValues(lookups.Warehouses);
            model.SetSalespersonValues(lookups.Salespersons);
            model.SetPaymentTermValues(lookups.PaymentTerms);

            foreach (var item in model.ProformaInvoiceItems)
            {
                item.SetProductValues(lookups.Products);
            }
        }

        private async Task PopulateAsync(ProformaInvoiceUpdateModel model)
        {
            var lookups = await LoadLookupsAsync();

            model.SetCustomerValues(lookups.Customers);
            model.SetBusinessLocationValues(lookups.Warehouses);
            model.SetSalespersonValues(lookups.Salespersons);
            model.SetPaymentTermValues(lookups.PaymentTerms);

            foreach (var item in model.ProformaInvoiceItems)
            {
                item.SetProductValues(lookups.Products);
            }
        }

        private async Task<LookupData> LoadLookupsAsync()
        {
            var customers = await _customerManagementService.GetActiveCustomerAsync();

            var warehouses = (await _businessLocationManagementService.GetAllBusinessLocationAsync())
                .Where(x => x.IsActive)
                .ToList();

            var salespersons = await _applicationUserManager.Users
                .OrderBy(x => x.FirstName)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = (x.FirstName + " " + x.LastName).Trim() == "" ? x.Email! : x.FirstName + " " + x.LastName
                })
                .ToListAsync();

            salespersons.Insert(0, new SelectListItem("-- Select Salesperson --", string.Empty));

            var products = await _productManagementService.GetAllProductAsync();

            var paymentTerms = await _paymentTermManagementService.GetActivePaymentTermAsync();

            return new LookupData(customers, warehouses, salespersons, products, paymentTerms);
        }

        private sealed record LookupData(
            IList<Customer> Customers,
            IList<BusinessLocation> Warehouses,
            IList<SelectListItem> Salespersons,
            IList<Product> Products,
            IList<PaymentTerm> PaymentTerms);
    }
}
