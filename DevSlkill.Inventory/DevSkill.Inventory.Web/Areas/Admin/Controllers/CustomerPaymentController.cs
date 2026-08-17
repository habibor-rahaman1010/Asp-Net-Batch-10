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
    /// Money coming in from customers. A draft settles nothing; receiving the
    /// collection is what clears the invoices it was allocated to.
    /// </summary>
    [Area("Admin"), Authorize]
    public class CustomerPaymentController : Controller
    {
        private readonly ICustomerPaymentManagementService _customerPaymentManagementService;
        private readonly ISalesInvoiceManagementService _salesInvoiceManagementService;
        private readonly ICustomerManagementService _customerManagementService;
        private readonly ILogger<CustomerPaymentController> _logger;

        public CustomerPaymentController(
            ICustomerPaymentManagementService customerPaymentManagementService,
            ISalesInvoiceManagementService salesInvoiceManagementService,
            ICustomerManagementService customerManagementService,
            ILogger<CustomerPaymentController> logger)
        {
            _customerPaymentManagementService = customerPaymentManagementService;
            _salesInvoiceManagementService = salesInvoiceManagementService;
            _customerManagementService = customerManagementService;
            _logger = logger;
        }

        [Authorize(Policy = "ReadPermission")]
        public IActionResult CustomerPaymentList()
        {
            return View();
        }

        [HttpPost, Authorize(Policy = "ReadPermission")]
        public async Task<JsonResult> GetCustomerPaymentJsonData([FromBody] CustomerPaymentListModel model)
        {
            var result = await _customerPaymentManagementService.GetCustomerPaymentsAsync(
                model.PageIndex, model.PageSize, model.Search,
                model.FormatSortExpression(
                    "PaymentNo",
                    "Customer.CustomerName",
                    "PaymentDate",
                    "PaymentMethod",
                    "ReferenceNo",
                    "Amount",
                    "AllocatedAmount",
                    "Status",
                    "Id"));

            var jsonData = new
            {
                recordsTotal = result.total,
                recordsFiltered = result.totalDisplay,
                data = (from record in result.data
                        select new string[]
                        {
                            HttpUtility.HtmlEncode(record.PaymentNo),
                            HttpUtility.HtmlEncode(record.Customer?.CustomerName),
                            HttpUtility.HtmlEncode(record.PaymentDate.ToString("dd-MM-yyyy")),
                            HttpUtility.HtmlEncode(record.PaymentMethod.ToString()),
                            HttpUtility.HtmlEncode(record.ReferenceNo),
                            HttpUtility.HtmlEncode(record.Amount.ToString("N2")),
                            HttpUtility.HtmlEncode(record.AllocatedAmount.ToString("N2")),
                            HttpUtility.HtmlEncode(record.Status.ToString()),
                            HttpUtility.HtmlEncode(record.Id.ToString())
                        }
                    ).ToArray()
            };

            return Json(jsonData);
        }

        [Authorize(Policy = "CreatePermission")]
        public async Task<IActionResult> CreateCustomerPayment(Guid? salesInvoiceId)
        {
            var model = new CustomerPaymentCreateModel
            {
                PaymentNo = await _customerPaymentManagementService.GeneratePaymentNoAsync()
            };

            // Coming straight from the invoice list, the customer is already known, so
            // their open invoices are shown with this one already filled in.
            if (salesInvoiceId.HasValue && salesInvoiceId.Value != Guid.Empty)
            {
                var invoice = await _salesInvoiceManagementService.GetSalesInvoiceByIdAsync(salesInvoiceId.Value);

                if (invoice != null)
                {
                    model.CustomerId = invoice.CustomerId;

                    await LoadCustomerAsync(model, null);

                    var target = model.CustomerPaymentAllocations
                        .FirstOrDefault(x => x.SalesInvoiceId == salesInvoiceId.Value);

                    if (target != null)
                    {
                        target.AllocatedAmount = target.RemainingAmount;
                        model.Amount = target.RemainingAmount;
                    }
                }
            }

            await PopulateAsync(model);

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "CreatePermission")]
        public async Task<IActionResult> CreateCustomerPayment(CustomerPaymentCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                await RebuildAsync(model);
                return View(model);
            }

            try
            {
                var payment = new CustomerPayment
                {
                    CustomerId = model.CustomerId,
                    PaymentDate = model.PaymentDate,
                    PaymentMethod = model.PaymentMethod,
                    ReferenceNo = model.ReferenceNo,
                    CreditNoteId = model.CreditNoteId,
                    Amount = model.Amount,
                    Notes = model.Notes,
                    CreatedBy = User.Identity?.Name ?? string.Empty,
                    CustomerPaymentAllocations = ToAllocationRequests(model.CustomerPaymentAllocations)
                };

                await _customerPaymentManagementService
                    .CreateCustomerPaymentAsync(payment, model.ReceiveImmediately);

                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = model.ReceiveImmediately
                        ? "Collection received. The invoices it was put against are settled by that much."
                        : "Collection saved as a draft. Nothing has been settled yet.",
                    Type = ResponseTypes.Success
                });

                return RedirectToAction(nameof(CustomerPaymentList));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                await RebuildAsync(model);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Collection creation failed.");
                ModelState.AddModelError(string.Empty, "Collection creation failed.");
                await RebuildAsync(model);
                return View(model);
            }
        }

        [Authorize(Policy = "UpdatePermission")]
        public async Task<IActionResult> UpdateCustomerPayment(Guid id)
        {
            var payment = await _customerPaymentManagementService.GetCustomerPaymentByIdAsync(id);

            if (payment == null)
            {
                return NotFound();
            }

            var model = new CustomerPaymentUpdateModel
            {
                Id = payment.Id,
                PaymentNo = payment.PaymentNo,
                CustomerId = payment.CustomerId,
                CustomerName = payment.Customer?.CustomerName ?? string.Empty,
                PaymentDate = payment.PaymentDate,
                PaymentMethod = payment.PaymentMethod,
                ReferenceNo = payment.ReferenceNo,
                CreditNoteId = payment.CreditNoteId,
                Amount = payment.Amount,
                Notes = payment.Notes,
                CustomerPaymentAllocations = await LoadAllocationsAsync(payment.CustomerId, payment.Id,
                    payment.CustomerPaymentAllocations?
                        .ToDictionary(x => x.SalesInvoiceId, x => x.AllocatedAmount))
            };

            await PopulateAsync(model);

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<IActionResult> UpdateCustomerPayment(CustomerPaymentUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                await RebuildAsync(model);
                return View(model);
            }

            try
            {
                var payment = new CustomerPayment
                {
                    Id = model.Id,
                    CustomerId = model.CustomerId,
                    PaymentDate = model.PaymentDate,
                    PaymentMethod = model.PaymentMethod,
                    ReferenceNo = model.ReferenceNo,
                    CreditNoteId = model.CreditNoteId,
                    Amount = model.Amount,
                    Notes = model.Notes,
                    CustomerPaymentAllocations = ToAllocationRequests(model.CustomerPaymentAllocations)
                };

                await _customerPaymentManagementService.UpdateCustomerPaymentAsync(payment);

                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "Collection updated successfully.",
                    Type = ResponseTypes.Success
                });

                return RedirectToAction(nameof(CustomerPaymentList));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                await RebuildAsync(model);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Collection update failed.");
                ModelState.AddModelError(string.Empty, "Collection update failed.");
                await RebuildAsync(model);
                return View(model);
            }
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "DeletePermission")]
        public async Task<JsonResult> DeleteCustomerPayment(Guid id)
        {
            return await RunAsync(
                () => _customerPaymentManagementService.DeleteCustomerPaymentAsync(id),
                "Collection deleted successfully.",
                "Collection delete failed.");
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<JsonResult> ReceiveCustomerPayment(Guid id)
        {
            return await RunAsync(
                () => _customerPaymentManagementService.ReceiveCustomerPaymentAsync(id),
                "Money received. The allocated invoices are settled by that much.",
                "Receiving the collection failed.");
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<JsonResult> CancelCustomerPayment(Guid id)
        {
            return await RunAsync(
                () => _customerPaymentManagementService.CancelCustomerPaymentAsync(id),
                "Collection cancelled. The invoices owe their money again.",
                "Cancelling the collection failed.");
        }

        [HttpGet, Authorize(Policy = "ReadPermission")]
        public async Task<IActionResult> GetCustomerPaymentDetails(Guid id)
        {
            var payment = await _customerPaymentManagementService.GetCustomerPaymentByIdAsync(id);

            if (payment == null)
            {
                return NotFound();
            }

            var result = new
            {
                id = payment.Id,
                paymentNo = payment.PaymentNo,
                customerName = payment.Customer?.CustomerName,
                customerCode = payment.Customer?.CustomerCode,
                paymentDate = payment.PaymentDate,
                paymentMethod = payment.PaymentMethod.ToString(),
                referenceNo = payment.ReferenceNo,
                creditNoteNo = payment.CreditNote?.CreditNoteNo,
                amount = payment.Amount,
                allocatedAmount = payment.AllocatedAmount,
                unallocatedAmount = payment.Amount - payment.AllocatedAmount,
                status = payment.Status.ToString(),
                notes = payment.Notes,
                allocations = payment.CustomerPaymentAllocations?.Select(x => new
                {
                    invoiceNo = x.SalesInvoice?.InvoiceNo,
                    invoiceDate = x.SalesInvoice?.InvoiceDate,
                    grandTotal = x.SalesInvoice?.GrandTotal,
                    dueAmount = x.DueAmount,
                    allocatedAmount = x.AllocatedAmount,
                    remarks = x.Remarks
                }).ToList()
            };

            return Json(result);
        }

        /// <summary>
        /// Feeds the collection screen once a customer is picked: their open invoices,
        /// oldest debt first, and whatever credit they still have to spend.
        /// </summary>
        [HttpGet, Authorize(Policy = "ReadPermission")]
        public async Task<JsonResult> GetReceivableInvoices(Guid customerId)
        {
            if (customerId == Guid.Empty)
            {
                return Json(new { success = false, message = "Please select a customer." });
            }

            try
            {
                var invoices = await _customerPaymentManagementService.GetReceivableInvoicesAsync(customerId);
                var creditNotes = await _customerPaymentManagementService.GetSpendableCreditNotesAsync(customerId);

                return Json(new
                {
                    success = true,
                    creditNotes = creditNotes.Select(x => new
                    {
                        id = x.Id,
                        text = $"{x.CreditNoteNo} - {(x.TotalAmount - x.AppliedAmount):N2} left"
                    }),
                    invoices = invoices.Select(x => new
                    {
                        salesInvoiceId = x.SalesInvoiceId,
                        invoiceNo = x.InvoiceNo,
                        invoiceDate = x.InvoiceDate,
                        dueDate = x.DueDate,
                        overdueDays = x.OverdueDays,
                        grandTotal = x.GrandTotal,
                        paidAmount = x.PaidAmount,
                        pendingAmount = x.PendingAmount,
                        remainingAmount = x.RemainingAmount,
                        status = x.Status
                    })
                });
            }
            catch (InvalidOperationException ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Loading receivable invoices failed.");
                return Json(new { success = false, message = "The customer's open invoices could not be loaded." });
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
        /// Only the invoice and the amount travel to the service; it checks what may
        /// actually go against each one.
        /// </summary>
        private static List<CustomerPaymentAllocation> ToAllocationRequests(
            IEnumerable<CustomerPaymentAllocationModel> allocations)
        {
            return allocations
                .Where(x => x.SalesInvoiceId != Guid.Empty && x.AllocatedAmount > 0)
                .Select(x => new CustomerPaymentAllocation
                {
                    SalesInvoiceId = x.SalesInvoiceId,
                    AllocatedAmount = x.AllocatedAmount,
                    Remarks = x.Remarks ?? string.Empty
                })
                .ToList();
        }

        private async Task<List<CustomerPaymentAllocationModel>> LoadAllocationsAsync(Guid customerId,
            Guid? excludePaymentId, IDictionary<Guid, decimal>? enteredAmounts)
        {
            var invoices = await _customerPaymentManagementService
                .GetReceivableInvoicesAsync(customerId, excludePaymentId);

            return invoices.Select(x => new CustomerPaymentAllocationModel
            {
                SalesInvoiceId = x.SalesInvoiceId,
                InvoiceNo = x.InvoiceNo,
                InvoiceDate = x.InvoiceDate,
                DueDate = x.DueDate,
                OverdueDays = x.OverdueDays,
                GrandTotal = x.GrandTotal,
                PaidAmount = x.PaidAmount,
                PendingAmount = x.PendingAmount,
                RemainingAmount = x.RemainingAmount,
                DueAmount = x.RemainingAmount,

                // Nothing is put on an invoice by default: how the money is split is the
                // whole decision being made on this screen.
                AllocatedAmount = enteredAmounts != null
                    && enteredAmounts.TryGetValue(x.SalesInvoiceId, out var entered)
                        ? entered
                        : 0m
            }).ToList();
        }

        private async Task LoadCustomerAsync(CustomerPaymentCreateModel model, IDictionary<Guid, decimal>? entered)
        {
            model.CustomerPaymentAllocations = await LoadAllocationsAsync(model.CustomerId, null, entered);

            model.SetCreditNoteValues(
                await _customerPaymentManagementService.GetSpendableCreditNotesAsync(model.CustomerId));
        }

        private async Task PopulateAsync(CustomerPaymentCreateModel model)
        {
            if (string.IsNullOrWhiteSpace(model.PaymentNo))
            {
                model.PaymentNo = await _customerPaymentManagementService.GeneratePaymentNoAsync();
            }

            model.SetCustomerValues(await _customerManagementService.GetActiveCustomerAsync());

            if (model.CreditNotes.Count == 0)
            {
                model.SetCreditNoteValues(new List<CreditNote>());
            }
        }

        private async Task PopulateAsync(CustomerPaymentUpdateModel model)
        {
            model.SetCreditNoteValues(
                await _customerPaymentManagementService.GetSpendableCreditNotesAsync(model.CustomerId));
        }

        /// <summary>
        /// A failed post only carries invoice ids and amounts back, so the invoice
        /// numbers and the outstanding amounts are read from the server again.
        /// </summary>
        private async Task RebuildAsync(CustomerPaymentCreateModel model)
        {
            var entered = model.CustomerPaymentAllocations
                .Where(x => x.SalesInvoiceId != Guid.Empty)
                .GroupBy(x => x.SalesInvoiceId)
                .ToDictionary(x => x.Key, x => x.Sum(a => a.AllocatedAmount));

            try
            {
                if (model.CustomerId != Guid.Empty)
                {
                    await LoadCustomerAsync(model, entered);
                }
                else
                {
                    model.CustomerPaymentAllocations = new List<CustomerPaymentAllocationModel>();
                }
            }
            catch (InvalidOperationException)
            {
                model.CustomerPaymentAllocations = new List<CustomerPaymentAllocationModel>();
            }

            await PopulateAsync(model);
        }

        private async Task RebuildAsync(CustomerPaymentUpdateModel model)
        {
            var payment = await _customerPaymentManagementService.GetCustomerPaymentByIdAsync(model.Id);

            if (payment == null)
            {
                model.CustomerPaymentAllocations = new List<CustomerPaymentAllocationModel>();
                return;
            }

            model.PaymentNo = payment.PaymentNo;
            model.CustomerId = payment.CustomerId;
            model.CustomerName = payment.Customer?.CustomerName ?? string.Empty;

            var entered = model.CustomerPaymentAllocations
                .Where(x => x.SalesInvoiceId != Guid.Empty)
                .GroupBy(x => x.SalesInvoiceId)
                .ToDictionary(x => x.Key, x => x.Sum(a => a.AllocatedAmount));

            try
            {
                model.CustomerPaymentAllocations = await LoadAllocationsAsync(payment.CustomerId, payment.Id, entered);
            }
            catch (InvalidOperationException)
            {
                model.CustomerPaymentAllocations = new List<CustomerPaymentAllocationModel>();
            }

            await PopulateAsync(model);
        }
    }
}
