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
    /// Everything that happens between a sale being agreed and the goods leaving the
    /// warehouse. Stock only ever moves from here, inside a transaction, and only when
    /// a delivery is confirmed or cancelled.
    /// </summary>
    /// <remarks>
    /// A shipment is raised against one of two source documents: a proforma invoice,
    /// which is the short path, or a confirmed sales order, which is the path that
    /// carries on into invoicing and collection. Everything below the source lookup
    /// works the same way for both.
    /// </remarks>
    public class DeliveryManagementService : IDeliveryManagementService
    {
        /// <summary>
        /// A proforma invoice may only be shipped once the customer has accepted it,
        /// and a partially shipped one stays open until the rest follows.
        /// </summary>
        private static readonly ProformaInvoiceStatus[] DeliverableProformaStatuses =
        {
            ProformaInvoiceStatus.Accepted,
            ProformaInvoiceStatus.Converted,
            ProformaInvoiceStatus.PartiallyDelivered
        };

        /// <summary>
        /// A sales order may only be shipped once it is confirmed, and a partially
        /// shipped one stays open until the rest follows.
        /// </summary>
        private static readonly SalesOrderStatus[] DeliverableSalesOrderStatuses =
        {
            SalesOrderStatus.Confirmed,
            SalesOrderStatus.PartiallyDelivered
        };

        private readonly IInventoryUnitOfWork _deliveryUnitOfWork;

        public DeliveryManagementService(IInventoryUnitOfWork unitOfWork)
        {
            _deliveryUnitOfWork = unitOfWork;
        }

        public async Task<Guid> CreateDeliveryAsync(Delivery delivery, bool confirmImmediately)
        {
            await _deliveryUnitOfWork.BeginTransactionAsync();

            try
            {
                var source = await LoadDeliverableSourceAsync(delivery.ProformaInvoiceId, delivery.SalesOrderId);

                await GuardPartiesAsync(source);
                GuardDeliveryDate(delivery.DeliveryDate, source.DocumentDate);

                var deliveryId = Guid.NewGuid();
                var lines = await BuildLinesAsync(source, delivery.DeliveryItems, deliveryId, null);

                var header = new Delivery
                {
                    Id = deliveryId,
                    DeliveryNo = await GenerateDeliveryNoAsync(),
                    ProformaInvoiceId = source.IsSalesOrder ? null : source.Id,
                    SalesOrderId = source.IsSalesOrder ? source.Id : null,

                    // Customer and warehouse always follow the source document, they are
                    // never taken from the browser.
                    CustomerId = source.CustomerId,
                    BusinessLocationId = source.BusinessLocationId,

                    DeliveryDate = delivery.DeliveryDate,
                    Status = DeliveryStatus.Draft,
                    ReceivedBy = delivery.ReceivedBy ?? string.Empty,
                    Notes = delivery.Notes ?? string.Empty,
                    CreatedBy = delivery.CreatedBy ?? string.Empty,
                    Created = DateTime.Now,
                    Updated = DateTime.Now,
                    DeliveryItems = lines
                };

                await _deliveryUnitOfWork.DeliveryRepository.AddAsync(header);

                if (confirmImmediately)
                {
                    await ApplyStockOutAsync(lines);

                    header.Status = DeliveryStatus.Confirmed;

                    await ConsumeReservationsAsync(source, lines);
                    await SyncSourceStatusAsync(source, deliveryId, lines);
                }

                await _deliveryUnitOfWork.CommitTransactionAsync();

                return deliveryId;
            }
            catch (InvalidOperationException)
            {
                await _deliveryUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _deliveryUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<bool> UpdateDeliveryAsync(Delivery delivery)
        {
            await _deliveryUnitOfWork.BeginTransactionAsync();

            try
            {
                var existing = await _deliveryUnitOfWork
                    .DeliveryRepository
                    .GetDeliveryByIdAsync(delivery.Id)
                    ?? throw new InvalidOperationException("Delivery not found.");

                if (existing.Status != DeliveryStatus.Draft)
                {
                    throw new InvalidOperationException(
                        $"A {existing.Status.ToString().ToLower()} delivery cannot be edited.");
                }

                var source = await LoadDeliverableSourceAsync(existing.ProformaInvoiceId, existing.SalesOrderId);

                await GuardPartiesAsync(source);
                GuardDeliveryDate(delivery.DeliveryDate, source.DocumentDate);

                // The old lines go first, so the quantity being edited never counts
                // against itself while the new lines are checked.
                await _deliveryUnitOfWork
                    .DeliveryItemRepository
                    .RemoveRangeAsync(existing.DeliveryItems?.ToList() ?? new List<DeliveryItem>());

                var lines = await BuildLinesAsync(source, delivery.DeliveryItems, existing.Id, existing.Id);

                foreach (var line in lines)
                {
                    await _deliveryUnitOfWork.DeliveryItemRepository.AddAsync(line);
                }

                // The delivery number, the source document, the customer and the
                // warehouse are issued once and never change.
                existing.DeliveryDate = delivery.DeliveryDate;
                existing.ReceivedBy = delivery.ReceivedBy ?? string.Empty;
                existing.Notes = delivery.Notes ?? string.Empty;
                existing.Updated = DateTime.Now;

                await _deliveryUnitOfWork.DeliveryRepository.EditAsync(existing);
                await _deliveryUnitOfWork.CommitTransactionAsync();

                return true;
            }
            catch (InvalidOperationException)
            {
                await _deliveryUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _deliveryUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<bool> DeleteDeliveryAsync(Guid id)
        {
            await _deliveryUnitOfWork.BeginTransactionAsync();

            try
            {
                var delivery = await _deliveryUnitOfWork
                    .DeliveryRepository
                    .GetDeliveryByIdAsync(id)
                    ?? throw new InvalidOperationException("Delivery not found.");

                // A confirmed delivery has already moved stock. It has to be cancelled
                // first, so the stock finds its way back before the paper disappears.
                if (delivery.Status == DeliveryStatus.Confirmed)
                {
                    throw new InvalidOperationException(
                        "A confirmed delivery cannot be deleted. Cancel it first.");
                }

                await GuardAgainstInvoicesAsync(delivery.Id, "deleted");

                await _deliveryUnitOfWork
                    .DeliveryItemRepository
                    .RemoveRangeAsync(delivery.DeliveryItems?.ToList() ?? new List<DeliveryItem>());

                await _deliveryUnitOfWork.DeliveryRepository.RemoveAsync(delivery);
                await _deliveryUnitOfWork.CommitTransactionAsync();

                return true;
            }
            catch (InvalidOperationException)
            {
                await _deliveryUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _deliveryUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<bool> ConfirmDeliveryAsync(Guid id)
        {
            await _deliveryUnitOfWork.BeginTransactionAsync();

            try
            {
                var delivery = await _deliveryUnitOfWork
                    .DeliveryRepository
                    .GetDeliveryByIdAsync(id)
                    ?? throw new InvalidOperationException("Delivery not found.");

                if (delivery.Status != DeliveryStatus.Draft)
                {
                    throw new InvalidOperationException(
                        $"Only a draft delivery can be confirmed, this one is {delivery.Status.ToString().ToLower()}.");
                }

                var lines = delivery.DeliveryItems?.ToList() ?? new List<DeliveryItem>();

                if (lines.Count == 0)
                {
                    throw new InvalidOperationException("A delivery without any line cannot be confirmed.");
                }

                var source = await LoadDeliverableSourceAsync(delivery.ProformaInvoiceId, delivery.SalesOrderId);

                await GuardPartiesAsync(source);

                // The draft may have been sitting around while other shipments went out,
                // so what is still owed is checked again at the moment of truth.
                var deliverable = (await BuildDeliverableLinesAsync(source, delivery.Id))
                    .ToDictionary(x => x.ProductId);

                foreach (var line in lines)
                {
                    if (!deliverable.TryGetValue(line.ProductId, out var deliverableLine))
                    {
                        throw new InvalidOperationException(
                            $"A product of this delivery is no longer part of '{source.DocumentNo}'.");
                    }

                    var stillOwed = deliverableLine.OrderedQuantity - deliverableLine.DeliveredQuantity;

                    if (line.DeliveredQuantity > stillOwed)
                    {
                        throw new InvalidOperationException(
                            $"'{deliverableLine.ProductName}': only {stillOwed:N2} is still owed to the customer, " +
                            $"but this delivery carries {line.DeliveredQuantity:N2}.");
                    }
                }

                await ApplyStockOutAsync(lines);

                delivery.Status = DeliveryStatus.Confirmed;
                delivery.Updated = DateTime.Now;

                await _deliveryUnitOfWork.DeliveryRepository.EditAsync(delivery);
                await ConsumeReservationsAsync(source, lines);
                await SyncSourceStatusAsync(source, delivery.Id, lines);

                await _deliveryUnitOfWork.CommitTransactionAsync();

                return true;
            }
            catch (InvalidOperationException)
            {
                await _deliveryUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _deliveryUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<bool> CancelDeliveryAsync(Guid id)
        {
            await _deliveryUnitOfWork.BeginTransactionAsync();

            try
            {
                var delivery = await _deliveryUnitOfWork
                    .DeliveryRepository
                    .GetDeliveryByIdAsync(id)
                    ?? throw new InvalidOperationException("Delivery not found.");

                if (delivery.Status == DeliveryStatus.Cancelled)
                {
                    throw new InvalidOperationException("This delivery is already cancelled.");
                }

                await GuardAgainstInvoicesAsync(delivery.Id, "cancelled");

                var lines = delivery.DeliveryItems?.ToList() ?? new List<DeliveryItem>();

                if (delivery.Status == DeliveryStatus.Confirmed)
                {
                    // The goods come back as free stock. Nothing is reserved again,
                    // whoever wants them has to ask for them once more, which is also
                    // why the consumed reservations are left as they are.
                    await RestoreStockAsync(lines);
                }

                delivery.Status = DeliveryStatus.Cancelled;
                delivery.Updated = DateTime.Now;

                await _deliveryUnitOfWork.DeliveryRepository.EditAsync(delivery);

                var source = await TryLoadSourceAsync(delivery.ProformaInvoiceId, delivery.SalesOrderId);

                if (source != null)
                {
                    await SyncSourceStatusAsync(source, delivery.Id, null);
                }

                await _deliveryUnitOfWork.CommitTransactionAsync();

                return true;
            }
            catch (InvalidOperationException)
            {
                await _deliveryUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _deliveryUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<Delivery?> GetDeliveryByIdAsync(Guid id)
        {
            return await _deliveryUnitOfWork.DeliveryRepository.GetDeliveryByIdAsync(id);
        }

        public async Task<(IList<Delivery> data, int total, int totalDisplay)> GetDeliveriesAsync(int pageIndex,
            int pageSize, DataTablesSearch search, string? order)
        {
            return await _deliveryUnitOfWork
                .DeliveryRepository
                .GetPagedDeliveriesAsync(pageIndex, pageSize, search, order);
        }

        public async Task<string> GenerateDeliveryNoAsync()
        {
            var count = await _deliveryUnitOfWork.DeliveryRepository.GetCountAsync();

            string deliveryNo;
            do
            {
                count++;
                deliveryNo = $"DL-{DateTime.Now:yyyyMM}-{count:D4}";
            }
            while (await _deliveryUnitOfWork.DeliveryRepository.IsDeliveryNoDuplicateAsync(deliveryNo));

            return deliveryNo;
        }

        public async Task<IList<DeliverableLineDto>> GetDeliverableLinesAsync(Guid? proformaInvoiceId,
            Guid? salesOrderId, Guid? excludeDeliveryId = null)
        {
            var source = await LoadDeliverableSourceAsync(proformaInvoiceId, salesOrderId);

            return await BuildDeliverableLinesAsync(source, excludeDeliveryId);
        }

        public async Task<IList<ProformaInvoice>> GetDeliverableProformaInvoicesAsync()
        {
            return await _deliveryUnitOfWork
                .ProformaInvoiceRepository
                .GetProformaInvoicesByStatusAsync(DeliverableProformaStatuses);
        }

        public async Task<IList<SalesOrder>> GetDeliverableSalesOrdersAsync()
        {
            return await _deliveryUnitOfWork
                .SalesOrderRepository
                .GetSalesOrdersByStatusAsync(DeliverableSalesOrderStatuses);
        }

        /// <summary>
        /// Reads whichever source document the shipment names and checks it is in a
        /// state that allows shipping. Everything downstream works off the flat shape
        /// this returns, so neither source gets special treatment.
        /// </summary>
        private async Task<SourceDocument> LoadDeliverableSourceAsync(Guid? proformaInvoiceId, Guid? salesOrderId)
        {
            var hasProforma = proformaInvoiceId.HasValue && proformaInvoiceId.Value != Guid.Empty;
            var hasSalesOrder = salesOrderId.HasValue && salesOrderId.Value != Guid.Empty;

            if (hasProforma == hasSalesOrder)
            {
                throw new InvalidOperationException(
                    "A delivery is raised against either a proforma invoice or a sales order, not both and not neither.");
            }

            if (hasSalesOrder)
            {
                var salesOrder = await _deliveryUnitOfWork
                    .SalesOrderRepository
                    .GetSalesOrderByIdAsync(salesOrderId!.Value)
                    ?? throw new InvalidOperationException("Sales order not found.");

                if (!DeliverableSalesOrderStatuses.Contains(salesOrder.Status))
                {
                    throw new InvalidOperationException(
                        $"'{salesOrder.SalesOrderNo}' is {salesOrder.Status.ToString().ToLower()}, " +
                        "so nothing can be delivered against it.");
                }

                return ToSource(salesOrder);
            }

            var proformaInvoice = await _deliveryUnitOfWork
                .ProformaInvoiceRepository
                .GetProformaInvoiceByIdAsync(proformaInvoiceId!.Value)
                ?? throw new InvalidOperationException("Proforma invoice not found.");

            if (!DeliverableProformaStatuses.Contains(proformaInvoice.Status))
            {
                throw new InvalidOperationException(
                    $"'{proformaInvoice.ProformaNo}' is {proformaInvoice.Status.ToString().ToLower()}, " +
                    "so nothing can be delivered against it.");
            }

            return ToSource(proformaInvoice);
        }

        /// <summary>
        /// The same lookup without the state check, for cancelling: a source that has
        /// since moved on still has to be restated.
        /// </summary>
        private async Task<SourceDocument?> TryLoadSourceAsync(Guid? proformaInvoiceId, Guid? salesOrderId)
        {
            if (salesOrderId.HasValue && salesOrderId.Value != Guid.Empty)
            {
                var salesOrder = await _deliveryUnitOfWork
                    .SalesOrderRepository
                    .GetSalesOrderByIdAsync(salesOrderId.Value);

                return salesOrder == null ? null : ToSource(salesOrder);
            }

            if (proformaInvoiceId.HasValue && proformaInvoiceId.Value != Guid.Empty)
            {
                var proformaInvoice = await _deliveryUnitOfWork
                    .ProformaInvoiceRepository
                    .GetProformaInvoiceByIdAsync(proformaInvoiceId.Value);

                return proformaInvoice == null ? null : ToSource(proformaInvoice);
            }

            return null;
        }

        private static SourceDocument ToSource(ProformaInvoice proformaInvoice)
        {
            return new SourceDocument(
                proformaInvoice.Id,
                proformaInvoice.ProformaNo,
                false,
                proformaInvoice.CustomerId,
                proformaInvoice.BusinessLocationId,
                proformaInvoice.ProformaDate,
                (proformaInvoice.ProformaInvoiceItems ?? new List<ProformaInvoiceItem>())
                    .Select(x => new SourceLine(x.ProductId, x.Quantity, x.Product))
                    .ToList(),
                proformaInvoice,
                null);
        }

        private static SourceDocument ToSource(SalesOrder salesOrder)
        {
            return new SourceDocument(
                salesOrder.Id,
                salesOrder.SalesOrderNo,
                true,
                salesOrder.CustomerId,
                salesOrder.BusinessLocationId,
                salesOrder.OrderDate,
                (salesOrder.SalesOrderItems ?? new List<SalesOrderItem>())
                    .Select(x => new SourceLine(x.ProductId, x.Quantity, x.Product))
                    .ToList(),
                null,
                salesOrder);
        }

        private async Task<IList<Delivery>> GetDeliveriesForSourceAsync(SourceDocument source)
        {
            return source.IsSalesOrder
                ? await _deliveryUnitOfWork.DeliveryRepository.GetDeliveriesBySalesOrderAsync(source.Id)
                : await _deliveryUnitOfWork.DeliveryRepository.GetDeliveriesByProformaInvoiceAsync(source.Id);
        }

        private async Task GuardPartiesAsync(SourceDocument source)
        {
            var customer = await _deliveryUnitOfWork
                .CustomerRepository
                .GetByIdAsync(source.CustomerId)
                ?? throw new InvalidOperationException("Customer not found.");

            if (customer.Status != CustomerStatus.Active)
            {
                throw new InvalidOperationException(
                    $"'{customer.CustomerName}' is inactive, so no goods can be delivered.");
            }

            var warehouse = await _deliveryUnitOfWork
                .BusinessLocationRepository
                .GetByIdAsync(source.BusinessLocationId)
                ?? throw new InvalidOperationException("Warehouse not found.");

            if (!warehouse.IsActive)
            {
                throw new InvalidOperationException(
                    $"'{warehouse.LocationName}' is inactive, so no goods can be delivered from it.");
            }
        }

        private static void GuardDeliveryDate(DateTime deliveryDate, DateTime documentDate)
        {
            if (deliveryDate.Date < documentDate.Date)
            {
                throw new InvalidOperationException(
                    "The delivery date cannot be earlier than the date of the document it is raised against.");
            }
        }

        /// <summary>
        /// Turns what the user typed into checked delivery lines. Quantities are
        /// validated against the source document here, never in the browser.
        /// </summary>
        private async Task<List<DeliveryItem>> BuildLinesAsync(SourceDocument source,
            IEnumerable<DeliveryItem>? requestedItems, Guid deliveryId, Guid? excludeDeliveryId)
        {
            var requested = requestedItems?
                .Where(x => x.ProductId != Guid.Empty && x.DeliveredQuantity > 0)
                .GroupBy(x => x.ProductId)
                .Select(g => new { ProductId = g.Key, Quantity = g.Sum(x => x.DeliveredQuantity) })
                .ToList();

            if (requested == null || requested.Count == 0)
            {
                throw new InvalidOperationException("At least one product line with a delivered quantity is required.");
            }

            var deliverable = (await BuildDeliverableLinesAsync(source, excludeDeliveryId))
                .ToDictionary(x => x.ProductId);

            var lines = new List<DeliveryItem>();

            foreach (var item in requested)
            {
                if (!deliverable.TryGetValue(item.ProductId, out var deliverableLine))
                {
                    throw new InvalidOperationException(
                        $"A product that is not part of '{source.DocumentNo}' cannot be delivered.");
                }

                var product = await _deliveryUnitOfWork
                    .ProductRepository
                    .GetByIdAsync(item.ProductId)
                    ?? throw new InvalidOperationException("Product not found.");

                if (product.Status != ProductStatus.Active)
                {
                    throw new InvalidOperationException($"'{product.ProductName}' is inactive and cannot be delivered.");
                }

                GuardWholeQuantity(product.ProductName, item.Quantity);

                if (item.Quantity > deliverableLine.RemainingQuantity)
                {
                    throw new InvalidOperationException(
                        $"'{product.ProductName}': only {deliverableLine.RemainingQuantity:N2} can still be delivered, " +
                        $"but {item.Quantity:N2} was entered.");
                }

                lines.Add(new DeliveryItem
                {
                    Id = Guid.NewGuid(),
                    DeliveryId = deliveryId,
                    ProductId = product.Id,
                    OrderedQuantity = deliverableLine.OrderedQuantity,
                    DeliveredQuantity = item.Quantity
                });
            }

            return lines;
        }

        /// <summary>
        /// Reads every shipment already written against the source document and works
        /// out what each of its lines still allows.
        /// </summary>
        private async Task<IList<DeliverableLineDto>> BuildDeliverableLinesAsync(SourceDocument source,
            Guid? excludeDeliveryId)
        {
            var deliveries = await GetDeliveriesForSourceAsync(source);

            var delivered = new Dictionary<Guid, decimal>();
            var pending = new Dictionary<Guid, decimal>();

            foreach (var delivery in deliveries)
            {
                if (excludeDeliveryId.HasValue && delivery.Id == excludeDeliveryId.Value)
                {
                    continue;
                }

                var target = delivery.Status switch
                {
                    DeliveryStatus.Confirmed => delivered,
                    DeliveryStatus.Draft => pending,
                    _ => null
                };

                if (target == null)
                {
                    continue;
                }

                foreach (var item in delivery.DeliveryItems ?? new List<DeliveryItem>())
                {
                    target[item.ProductId] = target.GetValueOrDefault(item.ProductId) + item.DeliveredQuantity;
                }
            }

            var lines = new List<DeliverableLineDto>();

            foreach (var item in source.Lines)
            {
                var product = item.Product ?? await _deliveryUnitOfWork.ProductRepository.GetByIdAsync(item.ProductId);

                var deliveredQuantity = delivered.GetValueOrDefault(item.ProductId);
                var pendingQuantity = pending.GetValueOrDefault(item.ProductId);
                var remaining = item.Quantity - deliveredQuantity - pendingQuantity;

                lines.Add(new DeliverableLineDto
                {
                    ProductId = item.ProductId,
                    ProductName = product?.ProductName ?? string.Empty,
                    UnitName = product?.Unit?.UnitName ?? string.Empty,
                    OrderedQuantity = item.Quantity,
                    DeliveredQuantity = deliveredQuantity,
                    PendingQuantity = pendingQuantity,
                    RemainingQuantity = remaining < 0 ? 0 : remaining,
                    AvailableStock = product == null ? 0 : product.CurrentStock - product.ReservedStock
                });
            }

            return lines;
        }

        /// <summary>
        /// Takes the goods out of the warehouse. This is the only place a confirmed
        /// delivery touches the product table.
        /// </summary>
        private async Task ApplyStockOutAsync(IList<DeliveryItem> lines)
        {
            foreach (var line in lines)
            {
                var product = await _deliveryUnitOfWork
                    .ProductRepository
                    .GetByIdAsync(line.ProductId)
                    ?? throw new InvalidOperationException("Product not found.");

                if (product.Status != ProductStatus.Active)
                {
                    throw new InvalidOperationException($"'{product.ProductName}' is inactive and cannot be delivered.");
                }

                GuardWholeQuantity(product.ProductName, line.DeliveredQuantity);

                var quantity = (int)line.DeliveredQuantity;

                if (quantity > product.CurrentStock)
                {
                    throw new InvalidOperationException(
                        $"'{product.ProductName}' has only {product.CurrentStock} in stock, " +
                        $"but {quantity} is being delivered.");
                }

                // Whatever was being held for this sale goes out together with the
                // goods, so the hold is released as the stock leaves. The guard keeps
                // the column from ever going below zero when nothing was reserved.
                product.ReservedStock -= Math.Min(product.ReservedStock, quantity);
                product.CurrentStock -= quantity;

                await _deliveryUnitOfWork.ProductRepository.EditAsync(product);
            }
        }

        private async Task RestoreStockAsync(IList<DeliveryItem> lines)
        {
            foreach (var line in lines)
            {
                var product = await _deliveryUnitOfWork
                    .ProductRepository
                    .GetByIdAsync(line.ProductId);

                if (product == null)
                {
                    continue;
                }

                product.CurrentStock += (int)line.DeliveredQuantity;

                await _deliveryUnitOfWork.ProductRepository.EditAsync(product);
            }
        }

        /// <summary>
        /// Writes the shipped quantity onto the order's own reservations, so a hold
        /// that has been fully served stops reading as active. The stock itself was
        /// already released by <see cref="ApplyStockOutAsync"/>; this only keeps the
        /// paperwork honest.
        /// </summary>
        private async Task ConsumeReservationsAsync(SourceDocument source, IList<DeliveryItem> lines)
        {
            if (!source.IsSalesOrder)
            {
                return;
            }

            var reservations = (await _deliveryUnitOfWork
                .StockReservationRepository
                .GetStockReservationsBySalesOrderAsync(source.Id))
                .Where(x => x.Status == StockReservationStatus.Reserved)
                .OrderBy(x => x.ReservationDate)
                .ToList();

            foreach (var line in lines)
            {
                var outstanding = line.DeliveredQuantity;

                // Oldest hold first, so a shipment served out of two reservations
                // clears them in the order they were made.
                foreach (var reservation in reservations)
                {
                    if (outstanding <= 0)
                    {
                        break;
                    }

                    var item = reservation.StockReservationItems?
                        .FirstOrDefault(x => x.ProductId == line.ProductId);

                    if (item == null)
                    {
                        continue;
                    }

                    var available = item.ReservedQuantity - item.ConsumedQuantity;

                    if (available <= 0)
                    {
                        continue;
                    }

                    var take = Math.Min(available, outstanding);

                    item.ConsumedQuantity += take;
                    outstanding -= take;

                    await _deliveryUnitOfWork.StockReservationItemRepository.EditAsync(item);
                }
            }

            foreach (var reservation in reservations)
            {
                var items = reservation.StockReservationItems ?? new List<StockReservationItem>();

                if (items.Count > 0 && items.All(x => x.ConsumedQuantity >= x.ReservedQuantity))
                {
                    reservation.Status = StockReservationStatus.Consumed;
                    reservation.Updated = DateTime.Now;

                    await _deliveryUnitOfWork.StockReservationRepository.EditAsync(reservation);
                }
            }
        }

        /// <summary>
        /// Restates the source document from the shipments that actually went out.
        /// <paramref name="extraConfirmedLines"/> carries the delivery being confirmed
        /// right now, which is not readable from the database yet.
        /// </summary>
        private async Task SyncSourceStatusAsync(SourceDocument source, Guid? ignoreDeliveryId,
            IList<DeliveryItem>? extraConfirmedLines)
        {
            if (source.Lines.Count == 0)
            {
                return;
            }

            var deliveries = await GetDeliveriesForSourceAsync(source);

            var delivered = new Dictionary<Guid, decimal>();

            foreach (var delivery in deliveries.Where(x => x.Status == DeliveryStatus.Confirmed))
            {
                if (ignoreDeliveryId.HasValue && delivery.Id == ignoreDeliveryId.Value)
                {
                    continue;
                }

                foreach (var item in delivery.DeliveryItems ?? new List<DeliveryItem>())
                {
                    delivered[item.ProductId] = delivered.GetValueOrDefault(item.ProductId) + item.DeliveredQuantity;
                }
            }

            foreach (var item in extraConfirmedLines ?? new List<DeliveryItem>())
            {
                delivered[item.ProductId] = delivered.GetValueOrDefault(item.ProductId) + item.DeliveredQuantity;
            }

            var anyDelivered = delivered.Values.Any(x => x > 0);
            var fullyDelivered = source.Lines.All(x => delivered.GetValueOrDefault(x.ProductId) >= x.Quantity);

            if (source.IsSalesOrder)
            {
                var salesOrder = source.SalesOrder!;

                // The delivered quantity lives on the order line, because invoicing
                // reads it from there rather than adding shipments up again.
                foreach (var item in salesOrder.SalesOrderItems ?? new List<SalesOrderItem>())
                {
                    item.DeliveredQuantity = delivered.GetValueOrDefault(item.ProductId);

                    await _deliveryUnitOfWork.SalesOrderItemRepository.EditAsync(item);
                }

                // A closed or cancelled order is out of the delivery flow's hands.
                if (salesOrder.Status is SalesOrderStatus.Confirmed
                    or SalesOrderStatus.PartiallyDelivered
                    or SalesOrderStatus.Delivered)
                {
                    salesOrder.Status = fullyDelivered
                        ? SalesOrderStatus.Delivered
                        : anyDelivered
                            ? SalesOrderStatus.PartiallyDelivered
                            : SalesOrderStatus.Confirmed;
                }

                salesOrder.Updated = DateTime.Now;

                await _deliveryUnitOfWork.SalesOrderRepository.EditAsync(salesOrder);

                return;
            }

            var proformaInvoice = source.ProformaInvoice!;

            // Nothing shipped means the document is open again. Accepted is the state a
            // proforma invoice has to be in before it may be delivered at all.
            proformaInvoice.Status = fullyDelivered
                ? ProformaInvoiceStatus.Delivered
                : anyDelivered
                    ? ProformaInvoiceStatus.PartiallyDelivered
                    : ProformaInvoiceStatus.Accepted;

            proformaInvoice.Updated = DateTime.Now;

            await _deliveryUnitOfWork.ProformaInvoiceRepository.EditAsync(proformaInvoice);
        }

        /// <summary>
        /// A shipment that has already been billed states what the customer was
        /// charged for, so it stays as it is until that invoice is cancelled.
        /// </summary>
        private async Task GuardAgainstInvoicesAsync(Guid deliveryId, string action)
        {
            var invoices = await _deliveryUnitOfWork
                .SalesInvoiceRepository
                .GetSalesInvoicesByStatusAsync(SalesInvoiceStatus.Draft, SalesInvoiceStatus.Posted,
                    SalesInvoiceStatus.PartiallyPaid, SalesInvoiceStatus.Paid);

            if (invoices.Any(x => x.DeliveryId == deliveryId))
            {
                throw new InvalidOperationException(
                    $"A delivery that has already been invoiced cannot be {action}.");
            }
        }

        /// <summary>
        /// Stock is counted in whole units by <see cref="Product.CurrentStock"/>, so a
        /// fractional shipment could never be taken out of the warehouse honestly.
        /// </summary>
        private static void GuardWholeQuantity(string productName, decimal quantity)
        {
            if (quantity != decimal.Truncate(quantity))
            {
                throw new InvalidOperationException(
                    $"'{productName}': stock is kept in whole units, so the delivered quantity cannot be {quantity:N2}.");
            }
        }

        /// <summary>One line of whichever document the shipment is being raised against.</summary>
        private sealed record SourceLine(Guid ProductId, decimal Quantity, Product? Product);

        /// <summary>
        /// A proforma invoice or a sales order, flattened to the handful of things the
        /// delivery flow actually needs. The two originals are kept so the status sync
        /// can write back to whichever one it came from.
        /// </summary>
        private sealed record SourceDocument(
            Guid Id,
            string DocumentNo,
            bool IsSalesOrder,
            Guid CustomerId,
            Guid BusinessLocationId,
            DateTime DocumentDate,
            IList<SourceLine> Lines,
            ProformaInvoice? ProformaInvoice,
            SalesOrder? SalesOrder);
    }
}
