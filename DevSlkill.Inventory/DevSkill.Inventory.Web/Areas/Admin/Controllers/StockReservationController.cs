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
    /// Holding stock for a confirmed sale. Nothing physical moves here: the hold only
    /// raises the reserved column, which is what stops the same goods being promised
    /// to a second customer before the delivery goes out.
    /// </summary>
    [Area("Admin"), Authorize]
    public class StockReservationController : Controller
    {
        private readonly IStockReservationManagementService _stockReservationManagementService;
        private readonly ISalesOrderManagementService _salesOrderManagementService;
        private readonly ILogger<StockReservationController> _logger;

        public StockReservationController(
            IStockReservationManagementService stockReservationManagementService,
            ISalesOrderManagementService salesOrderManagementService,
            ILogger<StockReservationController> logger)
        {
            _stockReservationManagementService = stockReservationManagementService;
            _salesOrderManagementService = salesOrderManagementService;
            _logger = logger;
        }

        [Authorize(Policy = "ReadPermission")]
        public IActionResult StockReservationList()
        {
            return View();
        }

        [HttpPost, Authorize(Policy = "ReadPermission")]
        public async Task<JsonResult> GetStockReservationJsonData([FromBody] StockReservationListModel model)
        {
            var result = await _stockReservationManagementService.GetStockReservationsAsync(
                model.PageIndex, model.PageSize, model.Search,
                model.FormatSortExpression(
                    "ReservationNo",
                    "SalesOrder.SalesOrderNo",
                    "Customer.CustomerName",
                    "ReservationDate",
                    "ExpiryDate",
                    "BusinessLocation.LocationName",
                    "Status",
                    "Id"));

            var today = DateTime.Today;

            var jsonData = new
            {
                recordsTotal = result.total,
                recordsFiltered = result.totalDisplay,
                data = (from record in result.data
                        select new string[]
                        {
                            HttpUtility.HtmlEncode(record.ReservationNo),
                            HttpUtility.HtmlEncode(record.SalesOrder?.SalesOrderNo),
                            HttpUtility.HtmlEncode(record.Customer?.CustomerName),
                            HttpUtility.HtmlEncode(record.ReservationDate.ToString("dd-MM-yyyy")),

                            // An expired hold still holds stock until someone releases it,
                            // so it is called out here rather than quietly let go.
                            HttpUtility.HtmlEncode(record.ExpiryDate.Date < today
                                && record.Status == Domain.Enums.StockReservationStatus.Reserved
                                    ? $"{record.ExpiryDate:dd-MM-yyyy} (expired)"
                                    : record.ExpiryDate.ToString("dd-MM-yyyy")),

                            HttpUtility.HtmlEncode(record.BusinessLocation?.LocationName),
                            HttpUtility.HtmlEncode(record.Status.ToString()),
                            HttpUtility.HtmlEncode(record.Id.ToString())
                        }
                    ).ToArray()
            };

            return Json(jsonData);
        }

        [Authorize(Policy = "CreatePermission")]
        public async Task<IActionResult> CreateStockReservation(Guid? salesOrderId)
        {
            var model = new StockReservationCreateModel
            {
                ReservationNo = await _stockReservationManagementService.GenerateReservationNoAsync()
            };

            // Coming straight from the sales order list, the order is already known, so
            // its holdable lines are shown without a second click.
            if (salesOrderId.HasValue && salesOrderId.Value != Guid.Empty)
            {
                model.SalesOrderId = salesOrderId.Value;

                try
                {
                    await LoadOrderAsync(model, null);
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    model.SalesOrderId = Guid.Empty;
                }
            }

            await PopulateAsync(model);

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "CreatePermission")]
        public async Task<IActionResult> CreateStockReservation(StockReservationCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                await RebuildAsync(model);
                return View(model);
            }

            try
            {
                var reservation = new StockReservation
                {
                    SalesOrderId = model.SalesOrderId,
                    ReservationDate = model.ReservationDate,
                    ExpiryDate = model.ExpiryDate,
                    Notes = model.Notes,
                    CreatedBy = User.Identity?.Name ?? string.Empty,
                    StockReservationItems = ToLineRequests(model.StockReservationItems)
                };

                await _stockReservationManagementService
                    .CreateStockReservationAsync(reservation, model.ReserveImmediately);

                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = model.ReserveImmediately
                        ? "Stock reservation created. The stock is now held for this order."
                        : "Stock reservation created as a draft. No stock is held yet.",
                    Type = ResponseTypes.Success
                });

                return RedirectToAction(nameof(StockReservationList));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                await RebuildAsync(model);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Stock reservation creation failed.");
                ModelState.AddModelError(string.Empty, "Stock reservation creation failed.");
                await RebuildAsync(model);
                return View(model);
            }
        }

        [Authorize(Policy = "UpdatePermission")]
        public async Task<IActionResult> UpdateStockReservation(Guid id)
        {
            var reservation = await _stockReservationManagementService.GetStockReservationByIdAsync(id);

            if (reservation == null)
            {
                return NotFound();
            }

            var model = new StockReservationUpdateModel
            {
                Id = reservation.Id,
                ReservationNo = reservation.ReservationNo,
                SalesOrderId = reservation.SalesOrderId,
                SalesOrderNo = reservation.SalesOrder?.SalesOrderNo ?? string.Empty,
                CustomerName = reservation.Customer?.CustomerName ?? string.Empty,
                WarehouseName = reservation.BusinessLocation?.LocationName ?? string.Empty,
                ReservationDate = reservation.ReservationDate,
                ExpiryDate = reservation.ExpiryDate,
                Notes = reservation.Notes,
                StockReservationItems = await LoadLinesAsync(reservation.SalesOrderId, reservation.Id,
                    reservation.StockReservationItems?.ToDictionary(x => x.ProductId, x => x.ReservedQuantity))
            };

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<IActionResult> UpdateStockReservation(StockReservationUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                await RebuildAsync(model);
                return View(model);
            }

            try
            {
                var reservation = new StockReservation
                {
                    Id = model.Id,
                    SalesOrderId = model.SalesOrderId,
                    ReservationDate = model.ReservationDate,
                    ExpiryDate = model.ExpiryDate,
                    Notes = model.Notes,
                    StockReservationItems = ToLineRequests(model.StockReservationItems)
                };

                await _stockReservationManagementService.UpdateStockReservationAsync(reservation);

                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "Stock reservation updated successfully.",
                    Type = ResponseTypes.Success
                });

                return RedirectToAction(nameof(StockReservationList));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                await RebuildAsync(model);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Stock reservation update failed.");
                ModelState.AddModelError(string.Empty, "Stock reservation update failed.");
                await RebuildAsync(model);
                return View(model);
            }
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "DeletePermission")]
        public async Task<JsonResult> DeleteStockReservation(Guid id)
        {
            return await RunAsync(
                () => _stockReservationManagementService.DeleteStockReservationAsync(id),
                "Stock reservation deleted successfully.",
                "Stock reservation delete failed.");
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<JsonResult> ConfirmStockReservation(Guid id)
        {
            return await RunAsync(
                () => _stockReservationManagementService.ConfirmStockReservationAsync(id),
                "Stock reserved. It is no longer available to anyone else.",
                "Stock reservation confirmation failed.");
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<JsonResult> ReleaseStockReservation(Guid id)
        {
            return await RunAsync(
                () => _stockReservationManagementService.ReleaseStockReservationAsync(id),
                "Reservation released. The stock is available again.",
                "Releasing the reservation failed.");
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<JsonResult> CancelStockReservation(Guid id)
        {
            return await RunAsync(
                () => _stockReservationManagementService.CancelStockReservationAsync(id),
                "Reservation cancelled. Any stock it was holding is available again.",
                "Cancelling the reservation failed.");
        }

        [HttpGet, Authorize(Policy = "ReadPermission")]
        public async Task<IActionResult> GetStockReservationDetails(Guid id)
        {
            var reservation = await _stockReservationManagementService.GetStockReservationByIdAsync(id);

            if (reservation == null)
            {
                return NotFound();
            }

            var result = new
            {
                id = reservation.Id,
                reservationNo = reservation.ReservationNo,
                salesOrderNo = reservation.SalesOrder?.SalesOrderNo,
                customerName = reservation.Customer?.CustomerName,
                customerCode = reservation.Customer?.CustomerCode,
                warehouse = reservation.BusinessLocation?.LocationName,
                reservationDate = reservation.ReservationDate,
                expiryDate = reservation.ExpiryDate,
                isExpired = reservation.ExpiryDate.Date < DateTime.Today,
                status = reservation.Status.ToString(),
                notes = reservation.Notes,
                stockReservationItems = reservation.StockReservationItems?.Select(x => new
                {
                    productName = x.Product?.ProductName,
                    unitName = x.Product?.Unit?.UnitName,
                    orderedQuantity = x.OrderedQuantity,
                    reservedQuantity = x.ReservedQuantity,
                    consumedQuantity = x.ConsumedQuantity,
                    outstandingQuantity = x.ReservedQuantity - x.ConsumedQuantity,
                    remarks = x.Remarks
                }).ToList()
            };

            return Json(result);
        }

        /// <summary>
        /// Feeds the create screen once a sales order is picked. Every quantity here
        /// comes from the server, the browser only prints it.
        /// </summary>
        [HttpGet, Authorize(Policy = "ReadPermission")]
        public async Task<JsonResult> GetReservableLines(Guid salesOrderId)
        {
            if (salesOrderId == Guid.Empty)
            {
                return Json(new { success = false, message = "Please select a sales order." });
            }

            try
            {
                var salesOrder = await _salesOrderManagementService.GetSalesOrderByIdAsync(salesOrderId);

                if (salesOrder == null)
                {
                    return Json(new { success = false, message = "Sales order not found." });
                }

                var lines = await _stockReservationManagementService.GetReservableLinesAsync(salesOrderId);

                return Json(new
                {
                    success = true,
                    customerName = salesOrder.Customer?.CustomerName,
                    warehouseName = salesOrder.BusinessLocation?.LocationName,
                    lines = lines.Select(x => new
                    {
                        productId = x.ProductId,
                        productName = x.ProductName,
                        unitName = x.UnitName,
                        orderedQuantity = x.OrderedQuantity,
                        reservedQuantity = x.ReservedQuantity,
                        pendingQuantity = x.PendingQuantity,
                        deliveredQuantity = x.DeliveredQuantity,
                        remainingQuantity = x.RemainingQuantity,
                        currentStock = x.CurrentStock,
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
                _logger.LogError(ex, "Loading reservable lines failed.");
                return Json(new { success = false, message = "The sales order lines could not be loaded." });
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

        /// <summary>Only product and quantity travel to the service; it decides what is allowed.</summary>
        private static List<StockReservationItem> ToLineRequests(IEnumerable<StockReservationItemModel> items)
        {
            return items
                .Where(x => x.ProductId != Guid.Empty && x.ReservedQuantity > 0)
                .Select(x => new StockReservationItem
                {
                    ProductId = x.ProductId,
                    ReservedQuantity = x.ReservedQuantity,
                    Remarks = x.Remarks ?? string.Empty
                })
                .ToList();
        }

        private async Task<List<StockReservationItemModel>> LoadLinesAsync(Guid salesOrderId,
            Guid? excludeReservationId, IDictionary<Guid, decimal>? enteredQuantities)
        {
            var lines = await _stockReservationManagementService
                .GetReservableLinesAsync(salesOrderId, excludeReservationId);

            return lines.Select(x => new StockReservationItemModel
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                UnitName = x.UnitName,
                OrderedQuantity = x.OrderedQuantity,
                AlreadyReservedQuantity = x.ReservedQuantity,
                PendingQuantity = x.PendingQuantity,
                DeliveredQuantity = x.DeliveredQuantity,
                RemainingQuantity = x.RemainingQuantity,
                CurrentStock = x.CurrentStock,
                AvailableStock = x.AvailableStock,

                // Nothing is defaulted in beyond what can actually be held, so the hold
                // never asks for stock that is not there.
                ReservedQuantity = enteredQuantities != null
                    && enteredQuantities.TryGetValue(x.ProductId, out var entered)
                        ? entered
                        : Math.Min(x.RemainingQuantity, x.AvailableStock)
            }).ToList();
        }

        private async Task LoadOrderAsync(StockReservationCreateModel model, IDictionary<Guid, decimal>? entered)
        {
            var salesOrder = await _salesOrderManagementService.GetSalesOrderByIdAsync(model.SalesOrderId)
                ?? throw new InvalidOperationException("Sales order not found.");

            model.CustomerName = salesOrder.Customer?.CustomerName ?? string.Empty;
            model.WarehouseName = salesOrder.BusinessLocation?.LocationName ?? string.Empty;
            model.StockReservationItems = await LoadLinesAsync(model.SalesOrderId, null, entered);
        }

        private async Task PopulateAsync(StockReservationCreateModel model)
        {
            if (string.IsNullOrWhiteSpace(model.ReservationNo))
            {
                model.ReservationNo = await _stockReservationManagementService.GenerateReservationNoAsync();
            }

            model.SetSalesOrderValues(await _stockReservationManagementService.GetReservableSalesOrdersAsync());
        }

        /// <summary>
        /// A failed post only carries product ids and quantities back, so the names and
        /// the holdable quantities are read from the server again.
        /// </summary>
        private async Task RebuildAsync(StockReservationCreateModel model)
        {
            var entered = model.StockReservationItems
                .Where(x => x.ProductId != Guid.Empty)
                .GroupBy(x => x.ProductId)
                .ToDictionary(x => x.Key, x => x.Sum(i => i.ReservedQuantity));

            try
            {
                if (model.SalesOrderId != Guid.Empty)
                {
                    await LoadOrderAsync(model, entered);
                }
                else
                {
                    model.StockReservationItems = new List<StockReservationItemModel>();
                }
            }
            catch (InvalidOperationException)
            {
                model.StockReservationItems = new List<StockReservationItemModel>();
            }

            await PopulateAsync(model);
        }

        private async Task RebuildAsync(StockReservationUpdateModel model)
        {
            var reservation = await _stockReservationManagementService.GetStockReservationByIdAsync(model.Id);

            if (reservation == null)
            {
                model.StockReservationItems = new List<StockReservationItemModel>();
                return;
            }

            model.ReservationNo = reservation.ReservationNo;
            model.SalesOrderId = reservation.SalesOrderId;
            model.SalesOrderNo = reservation.SalesOrder?.SalesOrderNo ?? string.Empty;
            model.CustomerName = reservation.Customer?.CustomerName ?? string.Empty;
            model.WarehouseName = reservation.BusinessLocation?.LocationName ?? string.Empty;

            var entered = model.StockReservationItems
                .Where(x => x.ProductId != Guid.Empty)
                .GroupBy(x => x.ProductId)
                .ToDictionary(x => x.Key, x => x.Sum(i => i.ReservedQuantity));

            try
            {
                model.StockReservationItems = await LoadLinesAsync(reservation.SalesOrderId, reservation.Id, entered);
            }
            catch (InvalidOperationException)
            {
                model.StockReservationItems = new List<StockReservationItemModel>();
            }
        }
    }
}
