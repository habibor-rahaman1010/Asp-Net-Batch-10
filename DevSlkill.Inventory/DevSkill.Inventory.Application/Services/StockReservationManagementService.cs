using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Domain.UnitOfWorkContracts;

namespace DevSkill.Inventory.Application.Services
{
    /// <summary>
    /// Holding stock for a confirmed sale. Nothing physical moves here: the hold
    /// only raises <see cref="Product.ReservedStock"/>, which is what stops the same
    /// goods being promised to a second customer before the delivery goes out.
    /// </summary>
    public class StockReservationManagementService : IStockReservationManagementService
    {
        private readonly IInventoryUnitOfWork _stockReservationUnitOfWork;

        /// <summary>An order may only be held against while it is still going to ship.</summary>
        private static readonly SalesOrderStatus[] ReservableOrderStatuses =
        {
            SalesOrderStatus.Confirmed,
            SalesOrderStatus.PartiallyDelivered
        };

        public StockReservationManagementService(IInventoryUnitOfWork unitOfWork)
        {
            _stockReservationUnitOfWork = unitOfWork;
        }

        public async Task<Guid> CreateStockReservationAsync(StockReservation stockReservation, bool reserveImmediately)
        {
            await _stockReservationUnitOfWork.BeginTransactionAsync();

            try
            {
                var context = await ValidateAsync(stockReservation, null);
                var reservationId = Guid.NewGuid();

                var header = new StockReservation
                {
                    Id = reservationId,
                    ReservationNo = await GenerateReservationNoAsync(),
                    SalesOrderId = context.SalesOrder.Id,
                    CustomerId = context.SalesOrder.CustomerId,
                    BusinessLocationId = context.SalesOrder.BusinessLocationId,
                    ReservationDate = context.ReservationDate,
                    ExpiryDate = context.ExpiryDate,
                    Status = StockReservationStatus.Draft,
                    Notes = stockReservation.Notes ?? string.Empty,
                    CreatedBy = stockReservation.CreatedBy ?? string.Empty,
                    Created = DateTime.Now,
                    Updated = DateTime.Now,
                    StockReservationItems = BuildLines(context, reservationId)
                };

                await _stockReservationUnitOfWork.StockReservationRepository.AddAsync(header);

                // Holding straight away is the normal case: the user asked for the stock
                // to be put aside, not for a note saying it might be.
                if (reserveImmediately)
                {
                    // The header is still an unwritten insert here, so holding only sets the
                    // status on it. Marking it modified would turn that insert into an
                    // update of a row that does not exist yet.
                    header.Status = StockReservationStatus.Reserved;

                    await ApplyHoldAsync(header);
                }

                await _stockReservationUnitOfWork.CommitTransactionAsync();

                return reservationId;
            }
            catch (InvalidOperationException)
            {
                await _stockReservationUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _stockReservationUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<bool> UpdateStockReservationAsync(StockReservation stockReservation)
        {
            await _stockReservationUnitOfWork.BeginTransactionAsync();

            try
            {
                var existing = await GetReservationAsync(stockReservation.Id);

                // Once the stock is actually held, changing the quantities would move
                // the reserved column behind the user's back. Release and re-hold instead.
                if (existing.Status != StockReservationStatus.Draft)
                {
                    throw new InvalidOperationException(
                        $"Only a draft reservation can be edited, this one is {existing.Status.ToString().ToLower()}.");
                }

                // The order a hold belongs to is what the hold is; an edit may change the
                // quantities but never the document behind them.
                stockReservation.SalesOrderId = existing.SalesOrderId;

                var context = await ValidateAsync(stockReservation, existing);
                var oldLines = existing.StockReservationItems?.ToList() ?? new List<StockReservationItem>();

                await _stockReservationUnitOfWork.StockReservationItemRepository.RemoveRangeAsync(oldLines);

                foreach (var line in BuildLines(context, existing.Id))
                {
                    await _stockReservationUnitOfWork.StockReservationItemRepository.AddAsync(line);
                }

                // The reservation number is issued once and never changes.
                existing.ReservationDate = context.ReservationDate;
                existing.ExpiryDate = context.ExpiryDate;
                existing.Notes = stockReservation.Notes ?? string.Empty;
                existing.Updated = DateTime.Now;

                await _stockReservationUnitOfWork.StockReservationRepository.EditAsync(existing);
                await _stockReservationUnitOfWork.CommitTransactionAsync();

                return true;
            }
            catch (InvalidOperationException)
            {
                await _stockReservationUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _stockReservationUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<bool> DeleteStockReservationAsync(Guid id)
        {
            await _stockReservationUnitOfWork.BeginTransactionAsync();

            try
            {
                var reservation = await GetReservationAsync(id);

                if (reservation.Status != StockReservationStatus.Draft)
                {
                    throw new InvalidOperationException(
                        "Only a draft reservation can be deleted. Release or cancel the held one instead, so the stock goes back.");
                }

                var lines = reservation.StockReservationItems?.ToList() ?? new List<StockReservationItem>();

                await _stockReservationUnitOfWork.StockReservationItemRepository.RemoveRangeAsync(lines);
                await _stockReservationUnitOfWork.StockReservationRepository.RemoveAsync(reservation);
                await _stockReservationUnitOfWork.CommitTransactionAsync();

                return true;
            }
            catch (InvalidOperationException)
            {
                await _stockReservationUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _stockReservationUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<bool> ConfirmStockReservationAsync(Guid id)
        {
            await _stockReservationUnitOfWork.BeginTransactionAsync();

            try
            {
                var reservation = await GetReservationAsync(id);

                if (reservation.Status != StockReservationStatus.Draft)
                {
                    throw new InvalidOperationException(
                        $"Only a draft reservation can be confirmed, this one is {reservation.Status.ToString().ToLower()}.");
                }

                if (reservation.StockReservationItems == null || reservation.StockReservationItems.Count == 0)
                {
                    throw new InvalidOperationException("A reservation without any product line cannot be confirmed.");
                }

                GuardOrderIsReservable(reservation.SalesOrder);

                await ApplyHoldAsync(reservation);

                reservation.Status = StockReservationStatus.Reserved;
                reservation.Updated = DateTime.Now;

                await _stockReservationUnitOfWork.StockReservationRepository.EditAsync(reservation);
                await _stockReservationUnitOfWork.CommitTransactionAsync();

                return true;
            }
            catch (InvalidOperationException)
            {
                await _stockReservationUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _stockReservationUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<bool> ReleaseStockReservationAsync(Guid id)
        {
            return await EndHoldAsync(id, StockReservationStatus.Released, "released");
        }

        public async Task<bool> CancelStockReservationAsync(Guid id)
        {
            return await EndHoldAsync(id, StockReservationStatus.Cancelled, "cancelled");
        }

        public async Task<StockReservation?> GetStockReservationByIdAsync(Guid id)
        {
            return await _stockReservationUnitOfWork.StockReservationRepository.GetStockReservationByIdAsync(id);
        }

        public async Task<(IList<StockReservation> data, int total, int totalDisplay)> GetStockReservationsAsync(
            int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            return await _stockReservationUnitOfWork
                .StockReservationRepository
                .GetPagedStockReservationsAsync(pageIndex, pageSize, search, order);
        }

        public async Task<string> GenerateReservationNoAsync()
        {
            var count = await _stockReservationUnitOfWork.StockReservationRepository.GetCountAsync();

            string reservationNo;
            do
            {
                count++;
                reservationNo = $"SR-{DateTime.Now:yyyyMM}-{count:D4}";
            }
            while (await _stockReservationUnitOfWork
                .StockReservationRepository
                .IsReservationNoDuplicateAsync(reservationNo));

            return reservationNo;
        }

        public async Task<IList<ReservableLineDto>> GetReservableLinesAsync(Guid salesOrderId,
            Guid? excludeReservationId = null)
        {
            var salesOrder = await _stockReservationUnitOfWork
                .SalesOrderRepository
                .GetSalesOrderByIdAsync(salesOrderId);

            if (salesOrder?.SalesOrderItems == null)
            {
                return new List<ReservableLineDto>();
            }

            var held = await GetHeldQuantitiesAsync(salesOrderId, excludeReservationId);
            var lines = new List<ReservableLineDto>();

            foreach (var item in salesOrder.SalesOrderItems)
            {
                var reserved = held.TryGetValue(item.ProductId, out var totals) ? totals.Reserved : 0m;
                var pending = held.TryGetValue(item.ProductId, out var draft) ? draft.Pending : 0m;

                var product = item.Product ?? await _stockReservationUnitOfWork
                    .ProductRepository
                    .GetProductByIdAsync(item.ProductId);

                var currentStock = product?.CurrentStock ?? 0;
                var freeStock = Math.Max(0, currentStock - (product?.ReservedStock ?? 0));

                // What is already out of the door no longer needs holding, and neither
                // does what another hold on the same order has already claimed.
                var outstanding = item.Quantity - item.DeliveredQuantity - reserved - pending;

                lines.Add(new ReservableLineDto
                {
                    ProductId = item.ProductId,
                    ProductName = product?.ProductName ?? string.Empty,
                    UnitName = product?.Unit?.UnitName ?? string.Empty,
                    OrderedQuantity = item.Quantity,
                    ReservedQuantity = reserved,
                    PendingQuantity = pending,
                    DeliveredQuantity = item.DeliveredQuantity,
                    RemainingQuantity = Math.Max(0m, Round(outstanding)),
                    CurrentStock = currentStock,
                    AvailableStock = freeStock
                });
            }

            return lines;
        }

        public async Task<IList<SalesOrder>> GetReservableSalesOrdersAsync()
        {
            return await _stockReservationUnitOfWork
                .SalesOrderRepository
                .GetSalesOrdersByStatusAsync(ReservableOrderStatuses);
        }

        public async Task<IList<StockReservation>> GetExpiredStockReservationsAsync()
        {
            return await _stockReservationUnitOfWork
                .StockReservationRepository
                .GetExpiredStockReservationsAsync(DateTime.Now);
        }

        /// <summary>
        /// Reads the request, checks it against the order behind it and hands back
        /// everything the write needs. Nothing is written from here.
        /// </summary>
        private async Task<ReservationContext> ValidateAsync(StockReservation stockReservation,
            StockReservation? existing)
        {
            if (stockReservation.SalesOrderId == Guid.Empty)
            {
                throw new InvalidOperationException("A reservation has to be raised against a sales order.");
            }

            var salesOrder = await _stockReservationUnitOfWork
                .SalesOrderRepository
                .GetSalesOrderByIdAsync(stockReservation.SalesOrderId)
                ?? throw new InvalidOperationException("Sales order not found.");

            GuardOrderIsReservable(salesOrder);

            var reservationDate = stockReservation.ReservationDate == default
                ? DateTime.Now.Date
                : stockReservation.ReservationDate.Date;

            var expiryDate = stockReservation.ExpiryDate == default
                ? reservationDate.AddDays(7)
                : stockReservation.ExpiryDate.Date;

            if (expiryDate < reservationDate)
            {
                throw new InvalidOperationException("A reservation cannot expire before the day it was made.");
            }

            var requested = (stockReservation.StockReservationItems ?? new List<StockReservationItem>())
                .Where(x => x.ProductId != Guid.Empty && x.ReservedQuantity > 0)
                .GroupBy(x => x.ProductId)
                .Select(x => new RequestedLine(
                    x.Key,
                    Round(x.Sum(l => l.ReservedQuantity)),
                    x.Select(l => l.Remarks).FirstOrDefault(r => !string.IsNullOrWhiteSpace(r)) ?? string.Empty))
                .ToList();

            if (requested.Count == 0)
            {
                throw new InvalidOperationException("A reservation needs at least one product line.");
            }

            var orderLines = (salesOrder.SalesOrderItems ?? new List<SalesOrderItem>())
                .GroupBy(x => x.ProductId)
                .ToDictionary(x => x.Key, x => new
                {
                    Quantity = x.Sum(l => l.Quantity),
                    Delivered = x.Sum(l => l.DeliveredQuantity)
                });

            // The reservation being edited is left out of the running totals, otherwise
            // it would be blocked by the quantity it is already holding itself.
            var held = await GetHeldQuantitiesAsync(salesOrder.Id, existing?.Id);
            var validated = new List<ValidatedLine>();

            foreach (var line in requested)
            {
                if (!orderLines.TryGetValue(line.ProductId, out var orderLine))
                {
                    throw new InvalidOperationException(
                        "A product that is not on the sales order cannot be reserved against it.");
                }

                var product = await _stockReservationUnitOfWork
                    .ProductRepository
                    .GetProductByIdAsync(line.ProductId)
                    ?? throw new InvalidOperationException("Product not found.");

                var alreadyHeld = held.TryGetValue(line.ProductId, out var totals)
                    ? totals.Reserved + totals.Pending
                    : 0m;

                var remaining = Round(orderLine.Quantity - orderLine.Delivered - alreadyHeld);

                if (remaining <= 0)
                {
                    throw new InvalidOperationException(
                        $"'{product.ProductName}' has nothing left to reserve on this sales order.");
                }

                if (line.Quantity > remaining)
                {
                    throw new InvalidOperationException(
                        $"'{product.ProductName}' has only {remaining:0.##} left to reserve on this sales order.");
                }

                validated.Add(new ValidatedLine(line.ProductId, orderLine.Quantity, line.Quantity, line.Remarks));
            }

            return new ReservationContext(salesOrder, reservationDate, expiryDate, validated);
        }

        private static IList<StockReservationItem> BuildLines(ReservationContext context, Guid reservationId)
        {
            return context.Lines
                .Select(line => new StockReservationItem
                {
                    Id = Guid.NewGuid(),
                    StockReservationId = reservationId,
                    ProductId = line.ProductId,
                    OrderedQuantity = Round(line.OrderedQuantity),
                    ReservedQuantity = Round(line.ReservedQuantity),
                    ConsumedQuantity = 0m,
                    Remarks = line.Remarks
                })
                .ToList();
        }

        /// <summary>
        /// Puts the hold on. Free stock is on-hand minus whatever is already held, so a
        /// second customer can never be promised the same goods.
        /// </summary>
        private async Task ApplyHoldAsync(StockReservation reservation)
        {
            foreach (var item in reservation.StockReservationItems ?? new List<StockReservationItem>())
            {
                var quantity = (int)Math.Ceiling(item.ReservedQuantity);

                if (quantity <= 0)
                {
                    continue;
                }

                var product = await _stockReservationUnitOfWork
                    .ProductRepository
                    .GetByIdAsync(item.ProductId)
                    ?? throw new InvalidOperationException("Product not found.");

                var freeStock = product.CurrentStock - product.ReservedStock;

                if (quantity > freeStock)
                {
                    throw new InvalidOperationException(
                        $"'{product.ProductName}' has only {Math.Max(0, freeStock)} free in stock, {quantity} cannot be reserved.");
                }

                product.ReservedStock += quantity;

                await _stockReservationUnitOfWork.ProductRepository.EditAsync(product);
            }
        }

        /// <summary>
        /// Ends a hold, whether it was given back on purpose or the sale fell through.
        /// Whatever a delivery has already taken stays taken; only the untouched part
        /// goes back to free stock.
        /// </summary>
        private async Task<bool> EndHoldAsync(Guid id, StockReservationStatus target, string action)
        {
            await _stockReservationUnitOfWork.BeginTransactionAsync();

            try
            {
                var reservation = await GetReservationAsync(id);

                if (reservation.Status is StockReservationStatus.Released
                    or StockReservationStatus.Cancelled
                    or StockReservationStatus.Consumed)
                {
                    throw new InvalidOperationException(
                        $"This reservation is already {reservation.Status.ToString().ToLower()} and cannot be {action}.");
                }

                if (target == StockReservationStatus.Released && reservation.Status != StockReservationStatus.Reserved)
                {
                    throw new InvalidOperationException(
                        "Only a reservation that is actually holding stock can be released.");
                }

                if (reservation.Status == StockReservationStatus.Reserved)
                {
                    await GiveBackHoldAsync(reservation);
                }

                reservation.Status = target;
                reservation.Updated = DateTime.Now;

                await _stockReservationUnitOfWork.StockReservationRepository.EditAsync(reservation);
                await _stockReservationUnitOfWork.CommitTransactionAsync();

                return true;
            }
            catch (InvalidOperationException)
            {
                await _stockReservationUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _stockReservationUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        private async Task GiveBackHoldAsync(StockReservation reservation)
        {
            foreach (var item in reservation.StockReservationItems ?? new List<StockReservationItem>())
            {
                var outstanding = item.ReservedQuantity - item.ConsumedQuantity;

                if (outstanding <= 0)
                {
                    continue;
                }

                var product = await _stockReservationUnitOfWork.ProductRepository.GetByIdAsync(item.ProductId);

                if (product == null)
                {
                    continue;
                }

                var quantity = (int)Math.Ceiling(outstanding);

                // Never below zero: the delivery flow releases its share too, and the
                // reserved column is a running total, not this document's own ledger.
                product.ReservedStock -= Math.Min(product.ReservedStock, quantity);

                await _stockReservationUnitOfWork.ProductRepository.EditAsync(product);
            }
        }

        /// <summary>
        /// Per product, how much of one sales order is already spoken for: held for real
        /// against a confirmed reservation, and pencilled in on a draft one.
        /// </summary>
        private async Task<IDictionary<Guid, (decimal Reserved, decimal Pending)>> GetHeldQuantitiesAsync(
            Guid salesOrderId, Guid? excludeReservationId)
        {
            var reservations = await _stockReservationUnitOfWork
                .StockReservationRepository
                .GetStockReservationsBySalesOrderAsync(salesOrderId);

            var totals = new Dictionary<Guid, (decimal Reserved, decimal Pending)>();

            foreach (var reservation in reservations)
            {
                if (excludeReservationId.HasValue && reservation.Id == excludeReservationId.Value)
                {
                    continue;
                }

                if (reservation.Status is not (StockReservationStatus.Draft or StockReservationStatus.Reserved))
                {
                    continue;
                }

                foreach (var item in reservation.StockReservationItems ?? new List<StockReservationItem>())
                {
                    // Consumed quantity has already left on a delivery, so it counts on the
                    // order's delivered side rather than here.
                    var outstanding = item.ReservedQuantity - item.ConsumedQuantity;

                    if (outstanding <= 0)
                    {
                        continue;
                    }

                    totals.TryGetValue(item.ProductId, out var current);

                    totals[item.ProductId] = reservation.Status == StockReservationStatus.Reserved
                        ? (current.Reserved + outstanding, current.Pending)
                        : (current.Reserved, current.Pending + outstanding);
                }
            }

            return totals;
        }

        private static void GuardOrderIsReservable(SalesOrder? salesOrder)
        {
            if (salesOrder == null)
            {
                throw new InvalidOperationException("Sales order not found.");
            }

            if (!ReservableOrderStatuses.Contains(salesOrder.Status))
            {
                throw new InvalidOperationException(
                    $"Stock can only be reserved against a confirmed sales order, '{salesOrder.SalesOrderNo}' is {salesOrder.Status.ToString().ToLower()}.");
            }
        }

        private async Task<StockReservation> GetReservationAsync(Guid id)
        {
            return await _stockReservationUnitOfWork
                .StockReservationRepository
                .GetStockReservationByIdAsync(id)
                ?? throw new InvalidOperationException("Stock reservation not found.");
        }

        private static decimal Round(decimal value)
        {
            return Math.Round(value, 2, MidpointRounding.AwayFromZero);
        }

        private sealed record RequestedLine(Guid ProductId, decimal Quantity, string Remarks);

        private sealed record ValidatedLine(Guid ProductId, decimal OrderedQuantity, decimal ReservedQuantity,
            string Remarks);

        private sealed record ReservationContext(SalesOrder SalesOrder, DateTime ReservationDate, DateTime ExpiryDate,
            IList<ValidatedLine> Lines);
    }
}
