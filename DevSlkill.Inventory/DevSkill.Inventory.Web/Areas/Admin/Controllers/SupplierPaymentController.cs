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
    public class SupplierPaymentController : Controller
    {
        private readonly ISupplierPaymentManagementService _supplierPaymentManagementService;
        private readonly ISupplierManagementService _supplierManagementService;
        private readonly ILogger<SupplierPaymentController> _logger;

        public SupplierPaymentController(
            ISupplierPaymentManagementService supplierPaymentManagementService,
            ISupplierManagementService supplierManagementService,
            ILogger<SupplierPaymentController> logger)
        {
            _supplierPaymentManagementService = supplierPaymentManagementService;
            _supplierManagementService = supplierManagementService;
            _logger = logger;
        }

        [Authorize(Policy = "ReadPermission")]
        public IActionResult SupplierPaymentList()
        {
            return View();
        }

        [HttpPost, Authorize(Policy = "ReadPermission")]
        public async Task<JsonResult> GetSupplierPaymentJsonData([FromBody] SupplierPaymentListModel model)
        {
            var result = await _supplierPaymentManagementService.GetSupplierPaymentsAsync(
                model.PageIndex, model.PageSize, model.Search,
                model.FormatSortExpression(
                    "PaymentNo",
                    "Supplier.SupplierName",
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
                            HttpUtility.HtmlEncode(record.Supplier?.SupplierName),
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
        public async Task<IActionResult> CreateSupplierPayment(Guid? supplierId)
        {
            var model = new SupplierPaymentCreateModel
            {
                PaymentNo = await _supplierPaymentManagementService.GeneratePaymentNoAsync()
            };

            // Coming in from the supplier or invoice list pre-fills the grid with
            // whatever that supplier still owes.
            if (supplierId.HasValue && supplierId.Value != Guid.Empty)
            {
                model.SupplierId = supplierId.Value;
                await FillAllocationsAsync(model);
            }

            await PopulateAsync(model);

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "CreatePermission")]
        public async Task<IActionResult> CreateSupplierPayment(SupplierPaymentCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateAsync(model);
                return View(model);
            }

            try
            {
                var supplierPayment = new SupplierPayment
                {
                    SupplierId = model.SupplierId,
                    PaymentDate = model.PaymentDate,
                    PaymentMethod = model.PaymentMethod,
                    ReferenceNo = model.ReferenceNo,
                    Amount = model.Amount,
                    Notes = model.Notes,
                    Status = model.Status,
                    CreatedBy = User.Identity?.Name ?? string.Empty,
                    SupplierPaymentAllocations = ToAllocationRequests(model.SupplierPaymentAllocations)
                };

                await _supplierPaymentManagementService.CreateSupplierPaymentAsync(supplierPayment);

                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = model.Status == SupplierPaymentStatus.Paid
                        ? "Supplier payment recorded and the invoices were settled."
                        : "Supplier payment saved as draft. Nothing is settled until it is confirmed.",
                    Type = ResponseTypes.Success
                });

                return RedirectToAction(nameof(SupplierPaymentList));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                await PopulateAsync(model);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Supplier payment creation failed.");
                ModelState.AddModelError(string.Empty, "Supplier payment creation failed.");
                await PopulateAsync(model);
                return View(model);
            }
        }

        [Authorize(Policy = "UpdatePermission")]
        public async Task<IActionResult> UpdateSupplierPayment(Guid id)
        {
            var supplierPayment = await _supplierPaymentManagementService.GetSupplierPaymentByIdAsync(id);

            if (supplierPayment == null)
            {
                return NotFound();
            }

            if (supplierPayment.Status != SupplierPaymentStatus.Draft)
            {
                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = $"A {supplierPayment.Status.ToString().ToLower()} supplier payment cannot be edited.",
                    Type = ResponseTypes.Danger
                });

                return RedirectToAction(nameof(SupplierPaymentList));
            }

            var model = new SupplierPaymentUpdateModel
            {
                Id = supplierPayment.Id,
                PaymentNo = supplierPayment.PaymentNo,
                SupplierId = supplierPayment.SupplierId,
                SupplierName = supplierPayment.Supplier?.SupplierName ?? string.Empty,
                PaymentDate = supplierPayment.PaymentDate,
                PaymentMethod = supplierPayment.PaymentMethod,
                ReferenceNo = supplierPayment.ReferenceNo,
                Amount = supplierPayment.Amount,
                Notes = supplierPayment.Notes,
                Status = supplierPayment.Status,
                CurrentOutstanding = supplierPayment.Supplier?.CurrentOutstanding ?? 0m
            };

            model.SupplierPaymentAllocations = await BuildEditableAllocationsAsync(supplierPayment);

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<IActionResult> UpdateSupplierPayment(SupplierPaymentUpdateModel model)
        {
            var existing = await _supplierPaymentManagementService.GetSupplierPaymentByIdAsync(model.Id);

            if (existing == null)
            {
                return NotFound();
            }

            // A payment never moves to another supplier, so the posted value is
            // discarded here as well.
            model.SupplierId = existing.SupplierId;
            model.SupplierName = existing.Supplier?.SupplierName ?? string.Empty;
            model.CurrentOutstanding = existing.Supplier?.CurrentOutstanding ?? 0m;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var supplierPayment = new SupplierPayment
                {
                    Id = model.Id,
                    SupplierId = model.SupplierId,
                    PaymentDate = model.PaymentDate,
                    PaymentMethod = model.PaymentMethod,
                    ReferenceNo = model.ReferenceNo,
                    Amount = model.Amount,
                    Notes = model.Notes,
                    Status = model.Status,
                    SupplierPaymentAllocations = ToAllocationRequests(model.SupplierPaymentAllocations)
                };

                await _supplierPaymentManagementService.UpdateSupplierPaymentAsync(supplierPayment);

                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = model.Status == SupplierPaymentStatus.Paid
                        ? "Supplier payment recorded and the invoices were settled."
                        : "Supplier payment updated successfully.",
                    Type = ResponseTypes.Success
                });

                return RedirectToAction(nameof(SupplierPaymentList));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Supplier payment update failed.");
                ModelState.AddModelError(string.Empty, "Supplier payment update failed.");
                return View(model);
            }
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<JsonResult> ConfirmSupplierPayment(Guid id)
        {
            return await RunAsync(
                () => _supplierPaymentManagementService.ConfirmSupplierPaymentAsync(id),
                "Supplier payment confirmed and the invoices were settled.",
                "Supplier payment confirmation failed.");
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<JsonResult> CancelSupplierPayment(Guid id)
        {
            return await RunAsync(
                () => _supplierPaymentManagementService.CancelSupplierPaymentAsync(id),
                "Supplier payment cancelled and the due was put back on the invoices.",
                "Supplier payment cancel failed.");
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "DeletePermission")]
        public async Task<JsonResult> DeleteSupplierPayment(Guid id)
        {
            return await RunAsync(
                () => _supplierPaymentManagementService.DeleteSupplierPaymentAsync(id),
                "Supplier payment deleted successfully.",
                "Supplier payment delete failed.");
        }

        [HttpGet, Authorize(Policy = "ReadPermission")]
        public async Task<IActionResult> GetSupplierPaymentDetails(Guid id)
        {
            var supplierPayment = await _supplierPaymentManagementService.GetSupplierPaymentByIdAsync(id);

            if (supplierPayment == null)
            {
                return NotFound();
            }

            var result = new
            {
                id = supplierPayment.Id,
                paymentNo = supplierPayment.PaymentNo,
                supplierName = supplierPayment.Supplier?.SupplierName,
                paymentDate = supplierPayment.PaymentDate,
                paymentMethod = supplierPayment.PaymentMethod.ToString(),
                referenceNo = supplierPayment.ReferenceNo,
                amount = supplierPayment.Amount,
                allocatedAmount = supplierPayment.AllocatedAmount,
                unallocatedAmount = supplierPayment.Amount - supplierPayment.AllocatedAmount,
                status = supplierPayment.Status.ToString(),
                notes = supplierPayment.Notes,
                supplierPaymentAllocations = supplierPayment.SupplierPaymentAllocations?.Select(x => new
                {
                    invoiceNo = x.PurchaseInvoice?.InvoiceNo,
                    supplierInvoiceNo = x.PurchaseInvoice?.SupplierInvoiceNo,
                    invoiceDate = x.PurchaseInvoice?.InvoiceDate,
                    grandTotal = x.PurchaseInvoice?.GrandTotal ?? 0m,
                    dueAmount = x.DueAmount,
                    allocatedAmount = x.AllocatedAmount,
                    remarks = x.Remarks
                }).ToList()
            };

            return Json(result);
        }

        /// <summary>
        /// The outstanding invoices of a supplier, so the payment screen can build
        /// its grid as soon as a supplier is chosen.
        /// </summary>
        [HttpGet, Authorize(Policy = "ReadPermission")]
        public async Task<JsonResult> GetPayableInvoices(Guid supplierId)
        {
            try
            {
                var supplier = await _supplierManagementService.GetSupplierByIdAsync(supplierId);

                if (supplier == null)
                {
                    return Json(new { success = false, message = "Supplier not found." });
                }

                var invoices = await _supplierPaymentManagementService.GetPayableInvoicesAsync(supplierId);

                return Json(new
                {
                    success = true,
                    currentOutstanding = supplier.CurrentOutstanding,
                    invoices = invoices.Select(x => new
                    {
                        purchaseInvoiceId = x.PurchaseInvoiceId,
                        invoiceNo = x.InvoiceNo,
                        supplierInvoiceNo = x.SupplierInvoiceNo,
                        invoiceDate = x.InvoiceDate,
                        dueDate = x.DueDate,
                        overdueDays = x.OverdueDays,
                        grandTotal = x.GrandTotal,
                        paidAmount = x.PaidAmount,
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
                _logger.LogError(ex, "Loading payable invoices failed.");
                return Json(new { success = false, message = "The supplier invoices could not be loaded." });
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
        /// Rebuilds the grid from the supplier. Nothing is defaulted into the paid
        /// column, because how much goes on which invoice is the whole decision.
        /// </summary>
        private async Task FillAllocationsAsync(SupplierPaymentCreateModel model)
        {
            var supplier = await _supplierManagementService.GetSupplierByIdAsync(model.SupplierId);

            if (supplier == null)
            {
                return;
            }

            model.CurrentOutstanding = supplier.CurrentOutstanding;

            var invoices = await _supplierPaymentManagementService.GetPayableInvoicesAsync(model.SupplierId);

            model.SupplierPaymentAllocations = invoices.Select(x => new SupplierPaymentAllocationModel
            {
                PurchaseInvoiceId = x.PurchaseInvoiceId,
                InvoiceNo = x.InvoiceNo,
                SupplierInvoiceNo = x.SupplierInvoiceNo,
                InvoiceDate = x.InvoiceDate,
                DueDate = x.DueDate,
                OverdueDays = x.OverdueDays,
                GrandTotal = x.GrandTotal,
                PaidAmount = x.PaidAmount,
                RemainingAmount = x.RemainingAmount,
                AllocatedAmount = 0m
            }).ToList();
        }

        /// <summary>
        /// The saved allocations of a draft payment, with the outstanding worked out
        /// as if this payment were not there.
        /// </summary>
        private async Task<List<SupplierPaymentAllocationModel>> BuildEditableAllocationsAsync(
            SupplierPayment supplierPayment)
        {
            var payable = (await _supplierPaymentManagementService
                .GetPayableInvoicesAsync(supplierPayment.SupplierId, supplierPayment.Id))
                .ToDictionary(x => x.PurchaseInvoiceId);

            var rows = new List<SupplierPaymentAllocationModel>();

            // The invoices this payment already points at come first, so the amounts
            // that were typed are never lost by a reload.
            foreach (var allocation in supplierPayment.SupplierPaymentAllocations
                ?? new List<SupplierPaymentAllocation>())
            {
                payable.TryGetValue(allocation.PurchaseInvoiceId, out var line);

                rows.Add(new SupplierPaymentAllocationModel
                {
                    PurchaseInvoiceId = allocation.PurchaseInvoiceId,
                    InvoiceNo = allocation.PurchaseInvoice?.InvoiceNo ?? line?.InvoiceNo ?? string.Empty,
                    SupplierInvoiceNo = allocation.PurchaseInvoice?.SupplierInvoiceNo
                        ?? line?.SupplierInvoiceNo ?? string.Empty,
                    InvoiceDate = allocation.PurchaseInvoice?.InvoiceDate ?? line?.InvoiceDate ?? DateTime.Today,
                    DueDate = allocation.PurchaseInvoice?.DueDate ?? line?.DueDate ?? DateTime.Today,
                    OverdueDays = line?.OverdueDays ?? 0,
                    GrandTotal = allocation.PurchaseInvoice?.GrandTotal ?? line?.GrandTotal ?? 0m,
                    PaidAmount = allocation.PurchaseInvoice?.PaidAmount ?? line?.PaidAmount ?? 0m,
                    RemainingAmount = line?.RemainingAmount ?? allocation.AllocatedAmount,
                    AllocatedAmount = allocation.AllocatedAmount,
                    Remarks = allocation.Remarks
                });
            }

            // Anything else the supplier still owes is offered alongside, so an edit
            // can spread the payment further without starting again.
            foreach (var line in payable.Values.Where(x =>
                rows.All(r => r.PurchaseInvoiceId != x.PurchaseInvoiceId)))
            {
                rows.Add(new SupplierPaymentAllocationModel
                {
                    PurchaseInvoiceId = line.PurchaseInvoiceId,
                    InvoiceNo = line.InvoiceNo,
                    SupplierInvoiceNo = line.SupplierInvoiceNo,
                    InvoiceDate = line.InvoiceDate,
                    DueDate = line.DueDate,
                    OverdueDays = line.OverdueDays,
                    GrandTotal = line.GrandTotal,
                    PaidAmount = line.PaidAmount,
                    RemainingAmount = line.RemainingAmount,
                    AllocatedAmount = 0m
                });
            }

            return rows.OrderBy(x => x.DueDate).ToList();
        }

        private static List<SupplierPaymentAllocation> ToAllocationRequests(
            IEnumerable<SupplierPaymentAllocationModel> allocations)
        {
            return allocations
                .Where(x => x.PurchaseInvoiceId != Guid.Empty && x.AllocatedAmount > 0)
                .Select(x => new SupplierPaymentAllocation
                {
                    PurchaseInvoiceId = x.PurchaseInvoiceId,
                    AllocatedAmount = x.AllocatedAmount,
                    Remarks = x.Remarks ?? string.Empty
                })
                .ToList();
        }

        private async Task PopulateAsync(SupplierPaymentCreateModel model)
        {
            if (string.IsNullOrWhiteSpace(model.PaymentNo))
            {
                model.PaymentNo = await _supplierPaymentManagementService.GeneratePaymentNoAsync();
            }

            model.SetSupplierValues(await _supplierManagementService.GetActiveSupplierAsync());
        }
    }
}
