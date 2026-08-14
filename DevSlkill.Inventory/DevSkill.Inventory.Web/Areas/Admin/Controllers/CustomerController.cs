using AutoMapper;
using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Infrastructure;
using DevSkill.Inventory.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class CustomerController : Controller
    {
        private readonly ICustomerManagementService _customerManagementService;
        private readonly IMapper _mapper;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(ICustomerManagementService customerManagementService,
            IMapper mapper,
            ILogger<CustomerController> logger)
        {
            _customerManagementService = customerManagementService;
            _mapper = mapper;
            _logger = logger;
        }

        [Authorize(Policy = "ReadPermission")]
        public IActionResult CustomerList()
        {
            return View();
        }

        [HttpPost, Authorize(Policy = "ReadPermission")]
        public async Task<JsonResult> GetCustomerJsonData([FromBody] CustomerListModel model)
        {
            var result = await _customerManagementService.GetCustomersAsync(model.PageIndex, model.PageSize, model.Search,
                model.FormatSortExpression(
                    "CustomerCode",
                    "CustomerName",
                    "Phone",
                    "Email",
                    "CustomerType",
                    "CreditLimit",
                    "CurrentOutstanding",
                    "Status",
                    "Id"));

            var customerJsonData = new
            {
                recordsTotal = result.total,
                recordsFiltered = result.totalDisplay,
                data = (from record in result.data
                        select new string[]
                        {
                            HttpUtility.HtmlEncode(record.CustomerCode),
                            HttpUtility.HtmlEncode(record.CustomerName),
                            HttpUtility.HtmlEncode(record.Phone),
                            HttpUtility.HtmlEncode(record.Email),
                            HttpUtility.HtmlEncode(record.CustomerType.ToString()),
                            HttpUtility.HtmlEncode(record.CreditLimit.ToString("N2")),
                            HttpUtility.HtmlEncode(record.CurrentOutstanding.ToString("N2")),
                            HttpUtility.HtmlEncode(record.Status.ToString()),
                            HttpUtility.HtmlEncode(record.Id.ToString())
                        }
                    ).ToArray()
            };

            return Json(customerJsonData);
        }

        [Authorize(Policy = "CreatePermission")]
        public async Task<IActionResult> CreateCustomer()
        {
            var model = new CustomerCreateModel
            {
                CustomerCode = await _customerManagementService.GenerateCustomerCodeAsync()
            };

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "CreatePermission")]
        public async Task<IActionResult> CreateCustomer(CustomerCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var customer = _mapper.Map<Customer>(model);
                customer.Id = Guid.NewGuid();

                await _customerManagementService.CreateCustomerAsync(customer);

                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "Customer created successfully.",
                    Type = ResponseTypes.Success
                });

                return RedirectToAction(nameof(CustomerList));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Customer creation failed.");
                ModelState.AddModelError(string.Empty, "Customer creation failed.");
                return View(model);
            }
        }

        [Authorize(Policy = "UpdatePermission")]
        public async Task<IActionResult> UpdateCustomer(Guid id)
        {
            var customer = await _customerManagementService.GetCustomerByIdAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<CustomerUpdateModel>(customer);

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<IActionResult> UpdateCustomer(CustomerUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var customer = _mapper.Map<Customer>(model);

                await _customerManagementService.UpdateCustomerAsync(customer);

                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "Customer updated successfully.",
                    Type = ResponseTypes.Success
                });

                return RedirectToAction(nameof(CustomerList));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Customer update failed.");
                ModelState.AddModelError(string.Empty, "Customer update failed.");
                return View(model);
            }
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "DeletePermission")]
        public async Task<JsonResult> DeleteCustomer(Guid id)
        {
            try
            {
                await _customerManagementService.DeleteCustomerAsync(id);

                return Json(new
                {
                    success = true,
                    message = "Customer deleted successfully."
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Customer delete failed.");

                return Json(new
                {
                    success = false,
                    message = "Customer delete failed. It may already be used by a sale."
                });
            }
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<JsonResult> ToggleCustomerStatus(Guid id)
        {
            try
            {
                await _customerManagementService.ToggleCustomerStatusAsync(id);

                return Json(new
                {
                    success = true,
                    message = "Customer status changed successfully."
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Customer status change failed.");

                return Json(new
                {
                    success = false,
                    message = "Customer status change failed."
                });
            }
        }

        [HttpGet, Authorize(Policy = "ReadPermission")]
        public async Task<IActionResult> GetCustomerDetails(Guid id)
        {
            var customer = await _customerManagementService.GetCustomerByIdAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            var result = new
            {
                id = customer.Id,
                customerCode = customer.CustomerCode,
                customerName = customer.CustomerName,
                phone = customer.Phone,
                email = customer.Email,
                address = customer.Address,
                customerType = customer.CustomerType.ToString(),
                creditLimit = customer.CreditLimit,
                openingBalance = customer.OpeningBalance,
                currentOutstanding = customer.CurrentOutstanding,
                availableCredit = customer.CreditLimit - customer.CurrentOutstanding,
                status = customer.Status.ToString(),
                created = customer.Created,
                updated = customer.Updated
            };

            return Json(result);
        }

        [HttpGet, Authorize(Policy = "ReadPermission")]
        public async Task<JsonResult> SearchCustomers(string term)
        {
            var customers = string.IsNullOrWhiteSpace(term)
                ? await _customerManagementService.GetActiveCustomerAsync()
                : await _customerManagementService.SearchCustomersByNameAsync(term);

            return Json(customers.Select(x => new
            {
                id = x.Id,
                text = $"{x.CustomerName} ({x.CustomerCode})"
            }));
        }
    }
}
