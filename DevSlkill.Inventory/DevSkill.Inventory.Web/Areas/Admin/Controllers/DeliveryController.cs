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
    public class DeliveryController : Controller
    {
        private readonly IDeliveryManagementService _deliveryManagementService;
        private readonly ILogger<DeliveryController> _logger;

        public DeliveryController(IDeliveryManagementService deliveryManagementService,
            ILogger<DeliveryController> logger)
        {
            _deliveryManagementService = deliveryManagementService;
            _logger = logger;
        }

        [Authorize(Policy = "CreatePermission")]
        public async Task<IActionResult> CreateDelivery(Guid? proformaInvoiceId, Guid? salesOrderId)
        {
            var model = new DeliveryCreateModel
            {
                DeliveryNo = await _deliveryManagementService.GenerateDeliveryNoAsync()
            };

            // Coming straight from the proforma invoice or sales order list, the source
            // document is already known, so the lines are shown without a second click.
            if (proformaInvoiceId.HasValue && proformaInvoiceId.Value != Guid.Empty)
            {
                model.SourceType = DeliveryCreateModel.ProformaSource;
                model.ProformaInvoiceId = proformaInvoiceId.Value;
            }
            else if (salesOrderId.HasValue && salesOrderId.Value != Guid.Empty)
            {
                model.SourceType = DeliveryCreateModel.SalesOrderSource;
                model.SalesOrderId = salesOrderId.Value;
            }

            if (model.SelectedSourceId.HasValue && model.SelectedSourceId.Value != Guid.Empty)
            {
                try
                {
                    model.DeliveryItems = await LoadLinesAsync(model.ProformaInvoiceId, model.SalesOrderId, null, null);
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    model.ProformaInvoiceId = null;
                    model.SalesOrderId = null;
                }
            }

            await PopulateAsync(model);

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "CreatePermission")]
        public async Task<IActionResult> CreateDelivery(DeliveryCreateModel model)
        {
            // Only the id of the chosen source is kept; the other one is cleared so a
            // stale value left in the browser cannot reach the service.
            if (model.SourceType == DeliveryCreateModel.ProformaSource)
            {
                model.SalesOrderId = null;
            }
            else
            {
                model.ProformaInvoiceId = null;
            }

            if (!model.SelectedSourceId.HasValue || model.SelectedSourceId.Value == Guid.Empty)
            {
                ModelState.AddModelError(string.Empty,
                    "A delivery has to be raised against a proforma invoice or a sales order.");
            }

            if (!ModelState.IsValid)
            {
                await RebuildAsync(model);
                return View(model);
            }

            try
            {
                var delivery = new Delivery
                {
                    ProformaInvoiceId = model.ProformaInvoiceId,
                    SalesOrderId = model.SalesOrderId,
                    DeliveryDate = model.DeliveryDate,
                    ReceivedBy = model.ReceivedBy,
                    Notes = model.Notes,
                    CreatedBy = User.Identity?.Name ?? string.Empty,
                    DeliveryItems = ToLineRequests(model.DeliveryItems)
                };

                await _deliveryManagementService.CreateDeliveryAsync(delivery, model.ConfirmImmediately);

                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = model.ConfirmImmediately
                        ? "Delivery created and confirmed successfully. Stock has been released."
                        : "Delivery created successfully.",
                    Type = ResponseTypes.Success
                });

                return RedirectToAction(nameof(DeliveryList));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                await RebuildAsync(model);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Delivery creation failed.");
                ModelState.AddModelError(string.Empty, "Delivery creation failed.");
                await RebuildAsync(model);
                return View(model);
            }
        }

        [Authorize(Policy = "UpdatePermission")]
        public async Task<IActionResult> UpdateDelivery(Guid id)
        {
            var delivery = await _deliveryManagementService.GetDeliveryByIdAsync(id);

            if (delivery == null)
            {
                return NotFound();
            }

            var model = new DeliveryUpdateModel
            {
                Id = delivery.Id,
                DeliveryNo = delivery.DeliveryNo,
                SourceLabel = delivery.SalesOrderId.HasValue ? "Sales Order" : "Proforma Invoice",
                SourceNo = delivery.SalesOrder?.SalesOrderNo ?? delivery.ProformaInvoice?.ProformaNo ?? string.Empty,
                CustomerName = delivery.Customer?.CustomerName ?? string.Empty,
                WarehouseName = delivery.BusinessLocation?.LocationName ?? string.Empty,
                DeliveryDate = delivery.DeliveryDate,
                ReceivedBy = delivery.ReceivedBy,
                Notes = delivery.Notes,
                DeliveryItems = await LoadLinesAsync(delivery.ProformaInvoiceId, delivery.SalesOrderId, delivery.Id,
                    delivery.DeliveryItems?.ToDictionary(x => x.ProductId, x => x.DeliveredQuantity))
            };

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<IActionResult> UpdateDelivery(DeliveryUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                await RebuildAsync(model);
                return View(model);
            }

            try
            {
                var delivery = new Delivery
                {
                    Id = model.Id,
                    DeliveryDate = model.DeliveryDate,
                    ReceivedBy = model.ReceivedBy,
                    Notes = model.Notes,
                    DeliveryItems = ToLineRequests(model.DeliveryItems)
                };

                await _deliveryManagementService.UpdateDeliveryAsync(delivery);

                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "Delivery updated successfully.",
                    Type = ResponseTypes.Success
                });

                return RedirectToAction(nameof(DeliveryList));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                await RebuildAsync(model);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Delivery update failed.");
                ModelState.AddModelError(string.Empty, "Delivery update failed.");
                await RebuildAsync(model);
                return View(model);
            }
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "DeletePermission")]
        public async Task<JsonResult> DeleteDelivery(Guid id)
        {
            try
            {
                await _deliveryManagementService.DeleteDeliveryAsync(id);

                return Json(new
                {
                    success = true,
                    message = "Delivery deleted successfully."
                });
            }
            catch (InvalidOperationException ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Delivery delete failed.");

                return Json(new { success = false, message = "Delivery delete failed." });
            }
        }

        /// <summary>
        /// Handing the goods over. This is the action that takes stock out, so it sits
        /// behind the update permission and never behind a plain link.
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<JsonResult> ConfirmDelivery(Guid id)
        {
            try
            {
                await _deliveryManagementService.ConfirmDeliveryAsync(id);

                return Json(new
                {
                    success = true,
                    message = "Delivery confirmed. Stock has been released from the warehouse."
                });
            }
            catch (InvalidOperationException ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Delivery confirmation failed.");

                return Json(new { success = false, message = "Delivery confirmation failed." });
            }
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<JsonResult> CancelDelivery(Guid id)
        {
            try
            {
                await _deliveryManagementService.CancelDeliveryAsync(id);

                return Json(new
                {
                    success = true,
                    message = "Delivery cancelled. Any released stock has been returned."
                });
            }
            catch (InvalidOperationException ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Delivery cancellation failed.");

                return Json(new { success = false, message = "Delivery cancellation failed." });
            }
        }

        [Authorize(Policy = "ReadPermission")]
        public IActionResult DeliveryList()
        {
            return View();
        }

        [HttpPost, Authorize(Policy = "ReadPermission")]
        public async Task<JsonResult> GetDeliveryJsonData([FromBody] DeliveryListModel model)
        {
            var result = await _deliveryManagementService.GetDeliveriesAsync(model.PageIndex, model.PageSize, model.Search,
                model.FormatSortExpression(
                    "DeliveryNo",
                    "ProformaInvoice.ProformaNo",
                    "Customer.CustomerName",
                    "DeliveryDate",
                    "BusinessLocation.LocationName",
                    "Status",
                    "Id"));

            var deliveryJsonData = new
            {
                recordsTotal = result.total,
                recordsFiltered = result.totalDisplay,
                data = (from record in result.data
                        select new string[]
                        {
                            HttpUtility.HtmlEncode(record.DeliveryNo),
                            HttpUtility.HtmlEncode(record.SalesOrder?.SalesOrderNo
                                ?? record.ProformaInvoice?.ProformaNo),
                            HttpUtility.HtmlEncode(record.Customer?.CustomerName),
                            HttpUtility.HtmlEncode(record.DeliveryDate.ToString("dd-MM-yyyy")),
                            HttpUtility.HtmlEncode(record.BusinessLocation?.LocationName),
                            HttpUtility.HtmlEncode(record.Status.ToString()),
                            HttpUtility.HtmlEncode(record.Id.ToString())
                        }
                    ).ToArray()
            };

            return Json(deliveryJsonData);
        }

        [HttpGet, Authorize(Policy = "ReadPermission")]
        public async Task<IActionResult> GetDeliveryDetails(Guid id)
        {
            var delivery = await _deliveryManagementService.GetDeliveryByIdAsync(id);

            if (delivery == null)
            {
                return NotFound();
            }

            var result = new
            {
                id = delivery.Id,
                deliveryNo = delivery.DeliveryNo,
                sourceLabel = delivery.SalesOrderId.HasValue ? "Sales Order" : "Proforma Invoice",
                sourceNo = delivery.SalesOrder?.SalesOrderNo ?? delivery.ProformaInvoice?.ProformaNo,
                deliveryDate = delivery.DeliveryDate,
                status = delivery.Status.ToString(),
                customerName = delivery.Customer?.CustomerName,
                customerCode = delivery.Customer?.CustomerCode,
                warehouse = delivery.BusinessLocation?.LocationName,
                receivedBy = delivery.ReceivedBy,
                notes = delivery.Notes,
                createdBy = delivery.CreatedBy,
                deliveryItems = delivery.DeliveryItems?.Select(x => new
                {
                    productName = x.Product?.ProductName,
                    unitName = x.Product?.Unit?.UnitName,
                    orderedQuantity = x.OrderedQuantity,
                    deliveredQuantity = x.DeliveredQuantity,
                    remainingQuantity = x.OrderedQuantity - x.DeliveredQuantity
                }).ToList()
            };

            return Json(result);
        }

        /// <summary>
        /// Feeds the create screen once a source document is picked, whichever of the
        /// two it is. Every quantity here comes from the server, the browser only
        /// prints it.
        /// </summary>
        [HttpGet, Authorize(Policy = "ReadPermission")]
        public async Task<JsonResult> GetSourceLines(Guid? proformaInvoiceId, Guid? salesOrderId)
        {
            var hasProforma = proformaInvoiceId.HasValue && proformaInvoiceId.Value != Guid.Empty;
            var hasSalesOrder = salesOrderId.HasValue && salesOrderId.Value != Guid.Empty;

            if (hasProforma == hasSalesOrder)
            {
                return Json(new
                {
                    success = false,
                    message = "Please pick either a proforma invoice or a sales order."
                });
            }

            try
            {
                var lines = await _deliveryManagementService.GetDeliverableLinesAsync(
                    hasProforma ? proformaInvoiceId : null,
                    hasSalesOrder ? salesOrderId : null);

                return Json(new
                {
                    success = true,
                    lines = lines.Select(x => new
                    {
                        productId = x.ProductId,
                        productName = x.ProductName,
                        unitName = x.UnitName,
                        orderedQuantity = x.OrderedQuantity,
                        deliveredQuantity = x.DeliveredQuantity,
                        pendingQuantity = x.PendingQuantity,
                        remainingQuantity = x.RemainingQuantity,
                        availableStock = x.AvailableStock
                    })
                });
            }
            catch (InvalidOperationException ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Loading the source document lines failed.");

                return Json(new { success = false, message = "Loading the source document lines failed." });
            }
        }

        /// <summary>
        /// Only product and quantity travel to the service; it decides what is allowed.
        /// </summary>
        private static List<DeliveryItem> ToLineRequests(IEnumerable<DeliveryItemModel> items)
        {
            return items
                .Where(x => x.ProductId != Guid.Empty && x.DeliveredQuantity > 0)
                .Select(x => new DeliveryItem
                {
                    ProductId = x.ProductId,
                    DeliveredQuantity = x.DeliveredQuantity
                })
                .ToList();
        }

        /// <summary>
        /// Builds the line rows of the screen from the source document, whichever of
        /// the two it is, keeping the quantities the user already typed.
        /// </summary>
        private async Task<List<DeliveryItemModel>> LoadLinesAsync(Guid? proformaInvoiceId, Guid? salesOrderId,
            Guid? excludeDeliveryId, IDictionary<Guid, decimal>? enteredQuantities)
        {
            var lines = await _deliveryManagementService.GetDeliverableLinesAsync(
                proformaInvoiceId, salesOrderId, excludeDeliveryId);

            return lines.Select(x => new DeliveryItemModel
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                UnitName = x.UnitName,
                OrderedQuantity = x.OrderedQuantity,
                AlreadyDeliveredQuantity = x.DeliveredQuantity,
                RemainingQuantity = x.RemainingQuantity,
                AvailableStock = x.AvailableStock,
                DeliveredQuantity = enteredQuantities != null && enteredQuantities.TryGetValue(x.ProductId, out var entered)
                    ? entered
                    : x.RemainingQuantity
            }).ToList();
        }

        private async Task PopulateAsync(DeliveryCreateModel model)
        {
            if (string.IsNullOrWhiteSpace(model.DeliveryNo))
            {
                model.DeliveryNo = await _deliveryManagementService.GenerateDeliveryNoAsync();
            }

            model.SetProformaInvoiceValues(await _deliveryManagementService.GetDeliverableProformaInvoicesAsync());
            model.SetSalesOrderValues(await _deliveryManagementService.GetDeliverableSalesOrdersAsync());
        }

        /// <summary>
        /// A failed post only carries product ids and quantities back, so the product
        /// names and the remaining quantities are read from the server again.
        /// </summary>
        private async Task RebuildAsync(DeliveryCreateModel model)
        {
            var entered = model.DeliveryItems
                .Where(x => x.ProductId != Guid.Empty)
                .GroupBy(x => x.ProductId)
                .ToDictionary(x => x.Key, x => x.Sum(i => i.DeliveredQuantity));

            try
            {
                model.DeliveryItems = model.SelectedSourceId.HasValue && model.SelectedSourceId.Value != Guid.Empty
                    ? await LoadLinesAsync(model.ProformaInvoiceId, model.SalesOrderId, null, entered)
                    : new List<DeliveryItemModel>();
            }
            catch (InvalidOperationException)
            {
                model.DeliveryItems = new List<DeliveryItemModel>();
            }

            await PopulateAsync(model);
        }

        private async Task RebuildAsync(DeliveryUpdateModel model)
        {
            var delivery = await _deliveryManagementService.GetDeliveryByIdAsync(model.Id);

            if (delivery == null)
            {
                model.DeliveryItems = new List<DeliveryItemModel>();
                return;
            }

            model.DeliveryNo = delivery.DeliveryNo;
            model.SourceLabel = delivery.SalesOrderId.HasValue ? "Sales Order" : "Proforma Invoice";
            model.SourceNo = delivery.SalesOrder?.SalesOrderNo ?? delivery.ProformaInvoice?.ProformaNo ?? string.Empty;
            model.CustomerName = delivery.Customer?.CustomerName ?? string.Empty;
            model.WarehouseName = delivery.BusinessLocation?.LocationName ?? string.Empty;

            var entered = model.DeliveryItems
                .Where(x => x.ProductId != Guid.Empty)
                .GroupBy(x => x.ProductId)
                .ToDictionary(x => x.Key, x => x.Sum(i => i.DeliveredQuantity));

            try
            {
                model.DeliveryItems = await LoadLinesAsync(delivery.ProformaInvoiceId, delivery.SalesOrderId,
                    delivery.Id, entered);
            }
            catch (InvalidOperationException)
            {
                model.DeliveryItems = new List<DeliveryItemModel>();
            }
        }
    }
}
