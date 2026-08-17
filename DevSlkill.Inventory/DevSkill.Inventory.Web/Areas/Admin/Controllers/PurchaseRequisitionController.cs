using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using DevSkill.Inventory.Domain.Enums;
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
    public class PurchaseRequisitionController : Controller
    {
        private readonly IPurchaseRequisitionManagementService _purchaseRequisitionManagementService;
        private readonly IBusinessLocationManagementService _businessLocationManagementService;
        private readonly IProductManagementService _productManagementService;
        private readonly ApplicationUserManager _applicationUserManager;
        private readonly ILogger<PurchaseRequisitionController> _logger;

        public PurchaseRequisitionController(
            IPurchaseRequisitionManagementService purchaseRequisitionManagementService,
            IBusinessLocationManagementService businessLocationManagementService,
            IProductManagementService productManagementService,
            ApplicationUserManager applicationUserManager,
            ILogger<PurchaseRequisitionController> logger)
        {
            _purchaseRequisitionManagementService = purchaseRequisitionManagementService;
            _businessLocationManagementService = businessLocationManagementService;
            _productManagementService = productManagementService;
            _applicationUserManager = applicationUserManager;
            _logger = logger;
        }

        [Authorize(Policy = "ReadPermission")]
        public IActionResult PurchaseRequisitionList()
        {
            return View();
        }

        [HttpPost, Authorize(Policy = "ReadPermission")]
        public async Task<JsonResult> GetPurchaseRequisitionJsonData([FromBody] PurchaseRequisitionListModel model)
        {
            var result = await _purchaseRequisitionManagementService.GetPurchaseRequisitionsAsync(
                model.PageIndex, model.PageSize, model.Search,
                model.FormatSortExpression(
                    "RequisitionNo",
                    "RequisitionDate",
                    "RequiredDate",
                    "BusinessLocation.LocationName",
                    "RequestedByName",
                    "EstimatedTotal",
                    "Status",
                    "Id"));

            var jsonData = new
            {
                recordsTotal = result.total,
                recordsFiltered = result.totalDisplay,
                data = (from record in result.data
                        select new string[]
                        {
                            HttpUtility.HtmlEncode(record.RequisitionNo),
                            HttpUtility.HtmlEncode(record.RequisitionDate.ToString("dd-MM-yyyy")),
                            HttpUtility.HtmlEncode(record.RequiredDate.ToString("dd-MM-yyyy")),
                            HttpUtility.HtmlEncode(record.BusinessLocation?.LocationName),
                            HttpUtility.HtmlEncode(record.RequestedByName),
                            HttpUtility.HtmlEncode(record.EstimatedTotal.ToString("N2")),
                            HttpUtility.HtmlEncode(record.Status.ToString()),
                            HttpUtility.HtmlEncode(record.Id.ToString())
                        }
                    ).ToArray()
            };

            return Json(jsonData);
        }

        [Authorize(Policy = "CreatePermission")]
        public async Task<IActionResult> CreatePurchaseRequisition()
        {
            var model = new PurchaseRequisitionCreateModel
            {
                RequisitionNo = await _purchaseRequisitionManagementService.GenerateRequisitionNoAsync()
            };

            await PopulateAsync(model);

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "CreatePermission")]
        public async Task<IActionResult> CreatePurchaseRequisition(PurchaseRequisitionCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateAsync(model);
                return View(model);
            }

            try
            {
                var requisition = new PurchaseRequisition
                {
                    RequisitionDate = model.RequisitionDate,
                    RequiredDate = model.RequiredDate,
                    BusinessLocationId = model.BusinessLocationId,
                    RequestedById = model.RequestedById == Guid.Empty ? null : model.RequestedById,
                    RequestedByName = await ResolveUserNameAsync(model.RequestedById),
                    Notes = model.Notes,
                    Status = model.Status,
                    CreatedBy = User.Identity?.Name ?? string.Empty,
                    PurchaseRequisitionItems = ToLineRequests(model.PurchaseRequisitionItems)
                };

                await _purchaseRequisitionManagementService.CreatePurchaseRequisitionAsync(requisition);

                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "Purchase requisition created successfully.",
                    Type = ResponseTypes.Success
                });

                return RedirectToAction(nameof(PurchaseRequisitionList));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                await PopulateAsync(model);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Purchase requisition creation failed.");
                ModelState.AddModelError(string.Empty, "Purchase requisition creation failed.");
                await PopulateAsync(model);
                return View(model);
            }
        }

        [Authorize(Policy = "UpdatePermission")]
        public async Task<IActionResult> UpdatePurchaseRequisition(Guid id)
        {
            var requisition = await _purchaseRequisitionManagementService.GetPurchaseRequisitionByIdAsync(id);

            if (requisition == null)
            {
                return NotFound();
            }

            var model = new PurchaseRequisitionUpdateModel
            {
                Id = requisition.Id,
                RequisitionNo = requisition.RequisitionNo,
                RequisitionDate = requisition.RequisitionDate,
                RequiredDate = requisition.RequiredDate,
                BusinessLocationId = requisition.BusinessLocationId,
                RequestedById = requisition.RequestedById,
                Notes = requisition.Notes,
                Status = requisition.Status,
                PurchaseRequisitionItems = requisition.PurchaseRequisitionItems?
                    .Select(x => new PurchaseRequisitionItemModel
                    {
                        ProductId = x.ProductId,
                        Quantity = x.Quantity,
                        EstimatedUnitPrice = x.EstimatedUnitPrice,
                        OrderedQuantity = x.OrderedQuantity,
                        Remarks = x.Remarks
                    }).ToList() ?? new List<PurchaseRequisitionItemModel>()
            };

            if (model.PurchaseRequisitionItems.Count == 0)
            {
                model.PurchaseRequisitionItems.Add(new PurchaseRequisitionItemModel());
            }

            await PopulateAsync(model);

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<IActionResult> UpdatePurchaseRequisition(PurchaseRequisitionUpdateModel model)
        {
            var existing = await _purchaseRequisitionManagementService.GetPurchaseRequisitionByIdAsync(model.Id);

            if (existing == null)
            {
                return NotFound();
            }

            // The warehouse decides which products the lines may hold, so it stays as
            // it was raised. The field is locked in the browser and the posted value
            // is discarded here as well.
            model.BusinessLocationId = existing.BusinessLocationId;
            ModelState.Remove(nameof(model.BusinessLocationId));

            if (!ModelState.IsValid)
            {
                await PopulateAsync(model);
                return View(model);
            }

            try
            {
                var requisition = new PurchaseRequisition
                {
                    Id = model.Id,
                    RequisitionDate = model.RequisitionDate,
                    RequiredDate = model.RequiredDate,
                    BusinessLocationId = model.BusinessLocationId,
                    RequestedById = model.RequestedById == Guid.Empty ? null : model.RequestedById,
                    RequestedByName = await ResolveUserNameAsync(model.RequestedById),
                    Notes = model.Notes,
                    Status = model.Status,
                    PurchaseRequisitionItems = ToLineRequests(model.PurchaseRequisitionItems)
                };

                await _purchaseRequisitionManagementService.UpdatePurchaseRequisitionAsync(requisition);

                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "Purchase requisition updated successfully.",
                    Type = ResponseTypes.Success
                });

                return RedirectToAction(nameof(PurchaseRequisitionList));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                await PopulateAsync(model);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Purchase requisition update failed.");
                ModelState.AddModelError(string.Empty, "Purchase requisition update failed.");
                await PopulateAsync(model);
                return View(model);
            }
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "DeletePermission")]
        public async Task<JsonResult> DeletePurchaseRequisition(Guid id)
        {
            try
            {
                await _purchaseRequisitionManagementService.DeletePurchaseRequisitionAsync(id);

                return Json(new { success = true, message = "Purchase requisition deleted successfully." });
            }
            catch (InvalidOperationException ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Purchase requisition delete failed.");
                return Json(new { success = false, message = "Purchase requisition delete failed." });
            }
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<JsonResult> SubmitPurchaseRequisition(Guid id)
        {
            try
            {
                await _purchaseRequisitionManagementService.SubmitPurchaseRequisitionAsync(id);

                return Json(new { success = true, message = "Purchase requisition submitted for approval." });
            }
            catch (InvalidOperationException ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Purchase requisition submit failed.");
                return Json(new { success = false, message = "Purchase requisition submit failed." });
            }
        }

        /// <summary>
        /// Approval reuses the existing update claim; the approver is taken from the
        /// signed in user, never from the browser.
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<JsonResult> ApprovePurchaseRequisition(Guid id)
        {
            try
            {
                var (approverId, approverName) = await GetCurrentUserAsync();

                await _purchaseRequisitionManagementService.ApprovePurchaseRequisitionAsync(id, approverId, approverName);

                return Json(new { success = true, message = "Purchase requisition approved." });
            }
            catch (InvalidOperationException ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Purchase requisition approval failed.");
                return Json(new { success = false, message = "Purchase requisition approval failed." });
            }
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<JsonResult> RejectPurchaseRequisition(Guid id, string reason)
        {
            try
            {
                var (approverId, approverName) = await GetCurrentUserAsync();

                await _purchaseRequisitionManagementService.RejectPurchaseRequisitionAsync(id, approverId, approverName, reason);

                return Json(new { success = true, message = "Purchase requisition rejected." });
            }
            catch (InvalidOperationException ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Purchase requisition rejection failed.");
                return Json(new { success = false, message = "Purchase requisition rejection failed." });
            }
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<JsonResult> CancelPurchaseRequisition(Guid id)
        {
            try
            {
                await _purchaseRequisitionManagementService.CancelPurchaseRequisitionAsync(id);

                return Json(new { success = true, message = "Purchase requisition cancelled." });
            }
            catch (InvalidOperationException ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Purchase requisition cancel failed.");
                return Json(new { success = false, message = "Purchase requisition cancel failed." });
            }
        }

        [HttpGet, Authorize(Policy = "ReadPermission")]
        public async Task<IActionResult> GetPurchaseRequisitionDetails(Guid id)
        {
            var requisition = await _purchaseRequisitionManagementService.GetPurchaseRequisitionByIdAsync(id);

            if (requisition == null)
            {
                return NotFound();
            }

            var result = new
            {
                id = requisition.Id,
                requisitionNo = requisition.RequisitionNo,
                requisitionDate = requisition.RequisitionDate,
                requiredDate = requisition.RequiredDate,
                warehouse = requisition.BusinessLocation?.LocationName,
                requestedBy = requisition.RequestedByName,
                status = requisition.Status.ToString(),
                notes = requisition.Notes,
                approvedBy = requisition.ApprovedByName,
                approvedDate = requisition.ApprovedDate,
                rejectionReason = requisition.RejectionReason,
                estimatedTotal = requisition.EstimatedTotal,
                purchaseRequisitionItems = requisition.PurchaseRequisitionItems?.Select(x => new
                {
                    productName = x.Product?.ProductName,
                    unitName = x.Product?.Unit?.UnitName,
                    quantity = x.Quantity,
                    estimatedUnitPrice = x.EstimatedUnitPrice,
                    orderedQuantity = x.OrderedQuantity,
                    remainingQuantity = x.Quantity - x.OrderedQuantity,
                    lineTotal = x.Quantity * x.EstimatedUnitPrice,
                    remarks = x.Remarks
                }).ToList()
            };

            return Json(result);
        }

        /// <summary>
        /// Feeds the product dropdowns after a warehouse is chosen. A requisition is
        /// raised for one warehouse, so only its products may be requested.
        /// </summary>
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

        private static List<PurchaseRequisitionItem> ToLineRequests(IEnumerable<PurchaseRequisitionItemModel> items)
        {
            return items
                .Where(x => x.ProductId != Guid.Empty && x.Quantity > 0)
                .Select(x => new PurchaseRequisitionItem
                {
                    ProductId = x.ProductId,
                    Quantity = x.Quantity,
                    EstimatedUnitPrice = x.EstimatedUnitPrice,
                    Remarks = x.Remarks ?? string.Empty
                })
                .ToList();
        }

        private async Task<(Guid? id, string name)> GetCurrentUserAsync()
        {
            var user = await _applicationUserManager.GetUserAsync(User);

            return user == null
                ? (null, User.Identity?.Name ?? string.Empty)
                : (user.Id, $"{user.FirstName} {user.LastName}".Trim());
        }

        private async Task<string> ResolveUserNameAsync(Guid? userId)
        {
            if (!userId.HasValue || userId == Guid.Empty)
            {
                return string.Empty;
            }

            var user = await _applicationUserManager.FindByIdAsync(userId.Value.ToString());

            return user == null
                ? string.Empty
                : $"{user.FirstName} {user.LastName}".Trim();
        }

        private async Task PopulateAsync(PurchaseRequisitionCreateModel model)
        {
            if (string.IsNullOrWhiteSpace(model.RequisitionNo))
            {
                model.RequisitionNo = await _purchaseRequisitionManagementService.GenerateRequisitionNoAsync();
            }

            model.SetBusinessLocationValues(await LoadWarehousesAsync());
            model.SetRequesterValues(await LoadUsersAsync());

            // The product list follows the chosen warehouse, so nothing is offered
            // until one is picked.
            var products = await LoadWarehouseProductsAsync(model.BusinessLocationId);

            foreach (var item in model.PurchaseRequisitionItems)
            {
                item.SetProductValues(products);
            }
        }

        private async Task PopulateAsync(PurchaseRequisitionUpdateModel model)
        {
            model.SetBusinessLocationValues(await LoadWarehousesAsync());
            model.SetRequesterValues(await LoadUsersAsync());

            var products = await IncludeExistingLineProductsAsync(
                await LoadWarehouseProductsAsync(model.BusinessLocationId),
                model.PurchaseRequisitionItems);

            foreach (var item in model.PurchaseRequisitionItems)
            {
                item.SetProductValues(products);
            }
        }

        /// <summary>
        /// A product that was requested earlier but has since been deactivated or
        /// moved to another warehouse stays in the dropdown, otherwise its line would
        /// quietly disappear on the next save.
        /// </summary>
        private async Task<IList<Product>> IncludeExistingLineProductsAsync(IList<Product> warehouseProducts,
            IEnumerable<PurchaseRequisitionItemModel> items)
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

        private async Task<IList<SelectListItem>> LoadUsersAsync()
        {
            var users = await _applicationUserManager.Users
                .OrderBy(x => x.FirstName)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = (x.FirstName + " " + x.LastName).Trim() == "" ? x.Email! : x.FirstName + " " + x.LastName
                })
                .ToListAsync();

            users.Insert(0, new SelectListItem("-- Select Requester --", string.Empty));

            return users;
        }
    }
}
