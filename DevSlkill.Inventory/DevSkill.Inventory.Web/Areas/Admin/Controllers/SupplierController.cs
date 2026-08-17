using AutoMapper;
using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using DevSkill.Inventory.Infrastructure;
using DevSkill.Inventory.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class SupplierController : Controller
    {
        private readonly ISupplierManagementService _supplierManagementService;
        private readonly IPaymentTermManagementService _paymentTermManagementService;
        private readonly IMapper _mapper;
        private readonly ILogger<SupplierController> _logger;

        public SupplierController(ISupplierManagementService supplierManagementService,
            IPaymentTermManagementService paymentTermManagementService,
            IMapper mapper,
            ILogger<SupplierController> logger)
        {
            _supplierManagementService = supplierManagementService;
            _paymentTermManagementService = paymentTermManagementService;
            _mapper = mapper;
            _logger = logger;
        }

        [Authorize(Policy = "ReadPermission")]
        public IActionResult SupplierList()
        {
            return View();
        }

        [HttpPost, Authorize(Policy = "ReadPermission")]
        public async Task<JsonResult> GetSupplierJsonData([FromBody] SupplierListModel model)
        {
            var result = await _supplierManagementService.GetSuppliersAsync(model.PageIndex, model.PageSize, model.Search,
                model.FormatSortExpression(
                    "SupplierCode",
                    "SupplierName",
                    "ContactPerson",
                    "Phone",
                    "Email",
                    "CreditLimit",
                    "CurrentOutstanding",
                    "Status",
                    "Id"));

            var supplierJsonData = new
            {
                recordsTotal = result.total,
                recordsFiltered = result.totalDisplay,
                data = (from record in result.data
                        select new string[]
                        {
                            HttpUtility.HtmlEncode(record.SupplierCode),
                            HttpUtility.HtmlEncode(record.SupplierName),
                            HttpUtility.HtmlEncode(record.ContactPerson),
                            HttpUtility.HtmlEncode(record.Phone),
                            HttpUtility.HtmlEncode(record.Email),
                            HttpUtility.HtmlEncode(record.CreditLimit.ToString("N2")),
                            HttpUtility.HtmlEncode(record.CurrentOutstanding.ToString("N2")),
                            HttpUtility.HtmlEncode(record.Status.ToString()),
                            HttpUtility.HtmlEncode(record.Id.ToString())
                        }
                    ).ToArray()
            };

            return Json(supplierJsonData);
        }

        [Authorize(Policy = "CreatePermission")]
        public async Task<IActionResult> CreateSupplier()
        {
            var model = new SupplierCreateModel
            {
                SupplierCode = await _supplierManagementService.GenerateSupplierCodeAsync()
            };

            await PopulateAsync(model);

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "CreatePermission")]
        public async Task<IActionResult> CreateSupplier(SupplierCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateAsync(model);
                return View(model);
            }

            try
            {
                var supplier = _mapper.Map<Supplier>(model);
                supplier.Id = Guid.NewGuid();

                await _supplierManagementService.CreateSupplierAsync(supplier);

                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "Supplier created successfully.",
                    Type = ResponseTypes.Success
                });

                return RedirectToAction(nameof(SupplierList));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                await PopulateAsync(model);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Supplier creation failed.");
                ModelState.AddModelError(string.Empty, "Supplier creation failed.");
                await PopulateAsync(model);
                return View(model);
            }
        }

        [Authorize(Policy = "UpdatePermission")]
        public async Task<IActionResult> UpdateSupplier(Guid id)
        {
            var supplier = await _supplierManagementService.GetSupplierByIdAsync(id);

            if (supplier == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<SupplierUpdateModel>(supplier);

            await PopulateAsync(model);

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<IActionResult> UpdateSupplier(SupplierUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateAsync(model);
                return View(model);
            }

            try
            {
                var supplier = _mapper.Map<Supplier>(model);

                await _supplierManagementService.UpdateSupplierAsync(supplier);

                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "Supplier updated successfully.",
                    Type = ResponseTypes.Success
                });

                return RedirectToAction(nameof(SupplierList));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                await PopulateAsync(model);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Supplier update failed.");
                ModelState.AddModelError(string.Empty, "Supplier update failed.");
                await PopulateAsync(model);
                return View(model);
            }
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "DeletePermission")]
        public async Task<JsonResult> DeleteSupplier(Guid id)
        {
            try
            {
                await _supplierManagementService.DeleteSupplierAsync(id);

                return Json(new
                {
                    success = true,
                    message = "Supplier deleted successfully."
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Supplier delete failed.");

                return Json(new
                {
                    success = false,
                    message = "Supplier delete failed. It may already be used by a purchase."
                });
            }
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<JsonResult> ToggleSupplierStatus(Guid id)
        {
            try
            {
                await _supplierManagementService.ToggleSupplierStatusAsync(id);

                return Json(new
                {
                    success = true,
                    message = "Supplier status changed successfully."
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
                _logger.LogError(ex, "Supplier status change failed.");

                return Json(new
                {
                    success = false,
                    message = "Supplier status change failed."
                });
            }
        }

        [HttpGet, Authorize(Policy = "ReadPermission")]
        public async Task<IActionResult> GetSupplierDetails(Guid id)
        {
            var supplier = await _supplierManagementService.GetSupplierByIdAsync(id);

            if (supplier == null)
            {
                return NotFound();
            }

            var result = new
            {
                id = supplier.Id,
                supplierCode = supplier.SupplierCode,
                supplierName = supplier.SupplierName,
                contactPerson = supplier.ContactPerson,
                phone = supplier.Phone,
                email = supplier.Email,
                address = supplier.Address,
                taxNumber = supplier.TaxNumber,
                paymentTerms = supplier.PaymentTerm?.TermName,
                creditLimit = supplier.CreditLimit,
                openingBalance = supplier.OpeningBalance,
                currentOutstanding = supplier.CurrentOutstanding,
                availableCredit = supplier.CreditLimit - supplier.CurrentOutstanding,
                status = supplier.Status.ToString(),
                created = supplier.Created,
                updated = supplier.Updated
            };

            return Json(result);
        }

        [HttpGet, Authorize(Policy = "ReadPermission")]
        public async Task<JsonResult> SearchSuppliers(string term)
        {
            var suppliers = string.IsNullOrWhiteSpace(term)
                ? await _supplierManagementService.GetActiveSupplierAsync()
                : await _supplierManagementService.SearchSuppliersByNameAsync(term);

            return Json(suppliers.Select(x => new
            {
                id = x.Id,
                text = $"{x.SupplierName} ({x.SupplierCode})"
            }));
        }

        private async Task PopulateAsync(SupplierCreateModel model)
        {
            if (string.IsNullOrWhiteSpace(model.SupplierCode))
            {
                model.SupplierCode = await _supplierManagementService.GenerateSupplierCodeAsync();
            }

            model.SetPaymentTermValues(await _paymentTermManagementService.GetActivePaymentTermAsync());
        }

        private async Task PopulateAsync(SupplierUpdateModel model)
        {
            model.SetPaymentTermValues(await _paymentTermManagementService.GetActivePaymentTermAsync());
        }
    }
}
