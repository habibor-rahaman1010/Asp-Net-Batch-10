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
    /// Credit owed back to customers. A note raised by a sales return belongs to that
    /// return and is not editable here; a price adjustment is raised by hand. Spending
    /// a credit is done by a collection of method Adjustment that names it.
    /// </summary>
    [Area("Admin"), Authorize]
    public class CreditNoteController : Controller
    {
        private readonly ICreditNoteManagementService _creditNoteManagementService;
        private readonly ICustomerManagementService _customerManagementService;
        private readonly IProductManagementService _productManagementService;
        private readonly ILogger<CreditNoteController> _logger;

        public CreditNoteController(
            ICreditNoteManagementService creditNoteManagementService,
            ICustomerManagementService customerManagementService,
            IProductManagementService productManagementService,
            ILogger<CreditNoteController> logger)
        {
            _creditNoteManagementService = creditNoteManagementService;
            _customerManagementService = customerManagementService;
            _productManagementService = productManagementService;
            _logger = logger;
        }

        [Authorize(Policy = "ReadPermission")]
        public IActionResult CreditNoteList()
        {
            return View();
        }

        [HttpPost, Authorize(Policy = "ReadPermission")]
        public async Task<JsonResult> GetCreditNoteJsonData([FromBody] CreditNoteListModel model)
        {
            var result = await _creditNoteManagementService.GetCreditNotesAsync(
                model.PageIndex, model.PageSize, model.Search,
                model.FormatSortExpression(
                    "CreditNoteNo",
                    "Customer.CustomerName",
                    "CreditNoteDate",
                    "SalesReturn.ReturnNo",
                    "TotalAmount",
                    "AppliedAmount",
                    "Status",
                    "Id"));

            var jsonData = new
            {
                recordsTotal = result.total,
                recordsFiltered = result.totalDisplay,
                data = (from record in result.data
                        select new string[]
                        {
                            HttpUtility.HtmlEncode(record.CreditNoteNo),
                            HttpUtility.HtmlEncode(record.Customer?.CustomerName),
                            HttpUtility.HtmlEncode(record.CreditNoteDate.ToString("dd-MM-yyyy")),

                            // A note raised by a return says so, because it cannot be
                            // worked on from this screen.
                            HttpUtility.HtmlEncode(record.SalesReturn?.ReturnNo ?? "Price adjustment"),

                            HttpUtility.HtmlEncode(record.TotalAmount.ToString("N2")),
                            HttpUtility.HtmlEncode((record.TotalAmount - record.AppliedAmount).ToString("N2")),
                            HttpUtility.HtmlEncode(record.Status.ToString()),
                            HttpUtility.HtmlEncode(record.Id.ToString())
                        }
                    ).ToArray()
            };

            return Json(jsonData);
        }

        [Authorize(Policy = "CreatePermission")]
        public async Task<IActionResult> CreateCreditNote(Guid? customerId)
        {
            var model = new CreditNoteCreateModel
            {
                CreditNoteNo = await _creditNoteManagementService.GenerateCreditNoteNoAsync()
            };

            if (customerId.HasValue && customerId.Value != Guid.Empty)
            {
                model.CustomerId = customerId.Value;
            }

            await PopulateAsync(model);

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "CreatePermission")]
        public async Task<IActionResult> CreateCreditNote(CreditNoteCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateAsync(model);
                return View(model);
            }

            try
            {
                var creditNote = new CreditNote
                {
                    CustomerId = model.CustomerId,
                    SalesInvoiceId = model.SalesInvoiceId,
                    CreditNoteDate = model.CreditNoteDate,
                    Reason = model.Reason,
                    Notes = model.Notes,
                    CreatedBy = User.Identity?.Name ?? string.Empty,
                    CreditNoteItems = ToLineRequests(model.CreditNoteItems)
                };

                await _creditNoteManagementService.CreateCreditNoteAsync(creditNote, model.IssueImmediately);

                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = model.IssueImmediately
                        ? "Credit note issued. The customer owes that much less and can spend the credit."
                        : "Credit note saved as a draft. Nothing has been credited yet.",
                    Type = ResponseTypes.Success
                });

                return RedirectToAction(nameof(CreditNoteList));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                await PopulateAsync(model);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Credit note creation failed.");
                ModelState.AddModelError(string.Empty, "Credit note creation failed.");
                await PopulateAsync(model);
                return View(model);
            }
        }

        [Authorize(Policy = "UpdatePermission")]
        public async Task<IActionResult> UpdateCreditNote(Guid id)
        {
            var creditNote = await _creditNoteManagementService.GetCreditNoteByIdAsync(id);

            if (creditNote == null)
            {
                return NotFound();
            }

            // A note that came out of a return belongs to that return, so it is not
            // opened for editing at all.
            if (creditNote.SalesReturnId.HasValue)
            {
                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "This credit note was raised by a sales return. Work on the return instead.",
                    Type = ResponseTypes.Danger
                });

                return RedirectToAction(nameof(CreditNoteList));
            }

            var model = new CreditNoteUpdateModel
            {
                Id = creditNote.Id,
                CreditNoteNo = creditNote.CreditNoteNo,
                CustomerId = creditNote.CustomerId,
                CustomerName = creditNote.Customer?.CustomerName ?? string.Empty,
                SalesInvoiceId = creditNote.SalesInvoiceId,
                CreditNoteDate = creditNote.CreditNoteDate,
                Reason = creditNote.Reason,
                Notes = creditNote.Notes,
                CreditNoteItems = creditNote.CreditNoteItems?
                    .Select(x => new CreditNoteItemModel
                    {
                        ProductId = x.ProductId,
                        Description = x.Description,
                        Quantity = x.Quantity,
                        UnitPrice = x.UnitPrice,
                        TaxRate = x.TaxRate
                    }).ToList() ?? new List<CreditNoteItemModel>()
            };

            if (model.CreditNoteItems.Count == 0)
            {
                model.CreditNoteItems.Add(new CreditNoteItemModel());
            }

            await PopulateAsync(model);

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<IActionResult> UpdateCreditNote(CreditNoteUpdateModel model)
        {
            var existing = await _creditNoteManagementService.GetCreditNoteByIdAsync(model.Id);

            if (existing == null)
            {
                return NotFound();
            }

            // Who the credit belongs to is what the credit note is.
            model.CustomerId = existing.CustomerId;
            model.CustomerName = existing.Customer?.CustomerName ?? string.Empty;

            if (!ModelState.IsValid)
            {
                await PopulateAsync(model);
                return View(model);
            }

            try
            {
                var creditNote = new CreditNote
                {
                    Id = model.Id,
                    CustomerId = model.CustomerId,
                    SalesInvoiceId = model.SalesInvoiceId,
                    CreditNoteDate = model.CreditNoteDate,
                    Reason = model.Reason,
                    Notes = model.Notes,
                    CreditNoteItems = ToLineRequests(model.CreditNoteItems)
                };

                await _creditNoteManagementService.UpdateCreditNoteAsync(creditNote);

                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "Credit note updated successfully.",
                    Type = ResponseTypes.Success
                });

                return RedirectToAction(nameof(CreditNoteList));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                await PopulateAsync(model);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Credit note update failed.");
                ModelState.AddModelError(string.Empty, "Credit note update failed.");
                await PopulateAsync(model);
                return View(model);
            }
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "DeletePermission")]
        public async Task<JsonResult> DeleteCreditNote(Guid id)
        {
            return await RunAsync(
                () => _creditNoteManagementService.DeleteCreditNoteAsync(id),
                "Credit note deleted successfully.",
                "Credit note delete failed.");
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<JsonResult> IssueCreditNote(Guid id)
        {
            return await RunAsync(
                () => _creditNoteManagementService.IssueCreditNoteAsync(id),
                "Credit issued. The customer owes that much less and can now spend it.",
                "Issuing the credit note failed.");
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<JsonResult> CancelCreditNote(Guid id)
        {
            return await RunAsync(
                () => _creditNoteManagementService.CancelCreditNoteAsync(id),
                "Credit note withdrawn.",
                "Cancelling the credit note failed.");
        }

        [HttpGet, Authorize(Policy = "ReadPermission")]
        public async Task<IActionResult> GetCreditNoteDetails(Guid id)
        {
            var creditNote = await _creditNoteManagementService.GetCreditNoteByIdAsync(id);

            if (creditNote == null)
            {
                return NotFound();
            }

            var result = new
            {
                id = creditNote.Id,
                creditNoteNo = creditNote.CreditNoteNo,
                customerName = creditNote.Customer?.CustomerName,
                customerCode = creditNote.Customer?.CustomerCode,
                salesReturnNo = creditNote.SalesReturn?.ReturnNo,
                salesInvoiceNo = creditNote.SalesInvoice?.InvoiceNo,
                creditNoteDate = creditNote.CreditNoteDate,
                reason = creditNote.Reason,
                status = creditNote.Status.ToString(),
                notes = creditNote.Notes,
                subTotal = creditNote.SubTotal,
                totalTax = creditNote.TotalTax,
                totalAmount = creditNote.TotalAmount,
                appliedAmount = creditNote.AppliedAmount,
                remainingAmount = creditNote.TotalAmount - creditNote.AppliedAmount,
                creditNoteItems = creditNote.CreditNoteItems?.Select(x => new
                {
                    description = x.Description,
                    productName = x.Product?.ProductName,
                    unitName = x.Product?.Unit?.UnitName,
                    quantity = x.Quantity,
                    unitPrice = x.UnitPrice,
                    taxRate = x.TaxRate,
                    taxAmount = x.TaxAmount,
                    lineTotal = x.LineTotal
                }).ToList()
            };

            return Json(result);
        }

        /// <summary>
        /// The invoices of one customer a price adjustment may be raised against, so
        /// the screen can narrow the list once a customer is picked.
        /// </summary>
        [HttpGet, Authorize(Policy = "ReadPermission")]
        public async Task<JsonResult> GetCreditableInvoices(Guid customerId)
        {
            if (customerId == Guid.Empty)
            {
                return Json(new { success = false, message = "Please select a customer." });
            }

            try
            {
                var invoices = await _creditNoteManagementService.GetCreditableSalesInvoicesAsync(customerId);

                return Json(new
                {
                    success = true,
                    invoices = invoices.Select(x => new
                    {
                        id = x.Id,
                        text = $"{x.InvoiceNo} - {x.GrandTotal:N2}"
                    })
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Loading creditable invoices failed.");
                return Json(new { success = false, message = "The customer's invoices could not be loaded." });
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

        private static List<CreditNoteItem> ToLineRequests(IEnumerable<CreditNoteItemModel> items)
        {
            return items
                .Where(x => x.UnitPrice > 0
                    && (!string.IsNullOrWhiteSpace(x.Description) || (x.ProductId.HasValue && x.ProductId != Guid.Empty)))
                .Select(x => new CreditNoteItem
                {
                    ProductId = x.ProductId == Guid.Empty ? null : x.ProductId,
                    Description = x.Description ?? string.Empty,
                    Quantity = x.Quantity,
                    UnitPrice = x.UnitPrice,
                    TaxRate = x.TaxRate
                })
                .ToList();
        }

        private async Task<IList<Product>> LoadProductsAsync()
        {
            return (await _productManagementService.GetAllProductAsync())
                .Where(x => x.Status == ProductStatus.Active)
                .OrderBy(x => x.ProductName)
                .ToList();
        }

        private async Task PopulateAsync(CreditNoteCreateModel model)
        {
            if (string.IsNullOrWhiteSpace(model.CreditNoteNo))
            {
                model.CreditNoteNo = await _creditNoteManagementService.GenerateCreditNoteNoAsync();
            }

            model.SetCustomerValues(await _customerManagementService.GetActiveCustomerAsync());

            model.SetSalesInvoiceValues(model.CustomerId == Guid.Empty
                ? new List<SalesInvoice>()
                : await _creditNoteManagementService.GetCreditableSalesInvoicesAsync(model.CustomerId));

            var products = await LoadProductsAsync();

            foreach (var item in model.CreditNoteItems)
            {
                item.SetProductValues(products);
            }
        }

        private async Task PopulateAsync(CreditNoteUpdateModel model)
        {
            model.SetSalesInvoiceValues(
                await _creditNoteManagementService.GetCreditableSalesInvoicesAsync(model.CustomerId));

            var products = await LoadProductsAsync();

            foreach (var item in model.CreditNoteItems)
            {
                item.SetProductValues(products);
            }
        }
    }
}
