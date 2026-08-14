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
    /// Everything that happens between a customer accepting a proforma invoice and the
    /// goods leaving the warehouse. Stock only ever moves from here, inside a
    /// transaction, and only when a delivery is confirmed or cancelled.
    /// </summary>
    public class DeliveryManagementService : IDeliveryManagementService
    {
        /// <summary>
        /// A proforma invoice may only be shipped once the customer has accepted it,
        /// and a partially shipped one stays open until the rest follows.
        /// </summary>
        private static readonly ProformaInvoiceStatus[] DeliverableStatuses =
        {
            ProformaInvoiceStatus.Accepted,
            ProformaInvoiceStatus.Converted,
            ProformaInvoiceStatus.PartiallyDelivered
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
                var proformaInvoice = await LoadDeliverableProformaInvoiceAsync(delivery.ProformaInvoiceId);

                await GuardPartiesAsync(proformaInvoice);
                GuardDeliveryDate(delivery.DeliveryDate, proformaInvoice.ProformaDate);

                var deliveryId = Guid.NewGuid();
                var lines = await BuildLinesAsync(proformaInvoice, delivery.DeliveryItems, deliveryId, null);

                var header = new Delivery
                {
                    Id = deliveryId,
                    DeliveryNo = await GenerateDeliveryNoAsync(),
                    ProformaInvoiceId = proformaInvoice.Id,

                    // Customer and warehouse always follow the proforma invoice, they are
                    // never taken from the browser.
                    CustomerId = proformaInvoice.CustomerId,
                    BusinessLocationId = proformaInvoice.BusinessLocationId,

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

                    await SyncProformaInvoiceStatusAsync(proformaInvoice, deliveryId, lines);
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

                var proformaInvoice = await LoadDeliverableProformaInvoiceAsync(existing.ProformaInvoiceId);

                await GuardPartiesAsync(proformaInvoice);
                GuardDeliveryDate(delivery.DeliveryDate, proformaInvoice.ProformaDate);

                // The old lines go first, so the quantity being edited never counts
                // against itself while the new lines are checked.
                await _deliveryUnitOfWork
                    .DeliveryItemRepository
                    .RemoveRangeAsync(existing.DeliveryItems?.ToList() ?? new List<DeliveryItem>());

                var lines = await BuildLinesAsync(proformaInvoice, delivery.DeliveryItems, existing.Id, existing.Id);

                foreach (var line in lines)
                {
                    await _deliveryUnitOfWork.DeliveryItemRepository.AddAsync(line);
                }

                // The delivery number, the proforma invoice, the customer and the
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

                var proformaInvoice = await LoadDeliverableProformaInvoiceAsync(delivery.ProformaInvoiceId);

                await GuardPartiesAsync(proformaInvoice);

                // The draft may have been sitting around while other shipments went out,
                // so what is still owed is checked again at the moment of truth.
                var deliverable = (await BuildDeliverableLinesAsync(proformaInvoice, delivery.Id))
                    .ToDictionary(x => x.ProductId);

                foreach (var line in lines)
                {
                    if (!deliverable.TryGetValue(line.ProductId, out var deliverableLine))
                    {
                        throw new InvalidOperationException(
                            "A product of this delivery is no longer part of the proforma invoice.");
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
                await SyncProformaInvoiceStatusAsync(proformaInvoice, delivery.Id, lines);

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

                var lines = delivery.DeliveryItems?.ToList() ?? new List<DeliveryItem>();

                if (delivery.Status == DeliveryStatus.Confirmed)
                {
                    // The goods come back as free stock. Nothing is reserved again,
                    // whoever wants them has to ask for them once more.
                    await RestoreStockAsync(lines);
                }

                delivery.Status = DeliveryStatus.Cancelled;
                delivery.Updated = DateTime.Now;

                await _deliveryUnitOfWork.DeliveryRepository.EditAsync(delivery);

                var proformaInvoice = await _deliveryUnitOfWork
                    .ProformaInvoiceRepository
                    .GetProformaInvoiceByIdAsync(delivery.ProformaInvoiceId);

                if (proformaInvoice != null)
                {
                    await SyncProformaInvoiceStatusAsync(proformaInvoice, delivery.Id, null);
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

        public async Task<IList<DeliverableLineDto>> GetDeliverableLinesAsync(Guid proformaInvoiceId,
            Guid? excludeDeliveryId = null)
        {
            var proformaInvoice = await LoadDeliverableProformaInvoiceAsync(proformaInvoiceId);

            return await BuildDeliverableLinesAsync(proformaInvoice, excludeDeliveryId);
        }

        public async Task<IList<ProformaInvoice>> GetDeliverableProformaInvoicesAsync()
        {
            return await _deliveryUnitOfWork
                .ProformaInvoiceRepository
                .GetProformaInvoicesByStatusAsync(DeliverableStatuses);
        }

        private async Task<ProformaInvoice> LoadDeliverableProformaInvoiceAsync(Guid proformaInvoiceId)
        {
            var proformaInvoice = await _deliveryUnitOfWork
                .ProformaInvoiceRepository
                .GetProformaInvoiceByIdAsync(proformaInvoiceId)
                ?? throw new InvalidOperationException("Proforma invoice not found.");

            if (!DeliverableStatuses.Contains(proformaInvoice.Status))
            {
                throw new InvalidOperationException(
                    $"'{proformaInvoice.ProformaNo}' is {proformaInvoice.Status.ToString().ToLower()}, " +
                    "so nothing can be delivered against it.");
            }

            return proformaInvoice;
        }

        private async Task GuardPartiesAsync(ProformaInvoice proformaInvoice)
        {
            var customer = await _deliveryUnitOfWork
                .CustomerRepository
                .GetByIdAsync(proformaInvoice.CustomerId)
                ?? throw new InvalidOperationException("Customer not found.");

            if (customer.Status != CustomerStatus.Active)
            {
                throw new InvalidOperationException(
                    $"'{customer.CustomerName}' is inactive, so no goods can be delivered.");
            }

            var warehouse = await _deliveryUnitOfWork
                .BusinessLocationRepository
                .GetByIdAsync(proformaInvoice.BusinessLocationId)
                ?? throw new InvalidOperationException("Warehouse not found.");

            if (!warehouse.IsActive)
            {
                throw new InvalidOperationException(
                    $"'{warehouse.LocationName}' is inactive, so no goods can be delivered from it.");
            }
        }

        private static void GuardDeliveryDate(DateTime deliveryDate, DateTime proformaDate)
        {
            if (deliveryDate.Date < proformaDate.Date)
            {
                throw new InvalidOperationException("The delivery date cannot be earlier than the proforma date.");
            }
        }

        /// <summary>
        /// Turns what the user typed into checked delivery lines. Quantities are
        /// validated against the proforma invoice here, never in the browser.
        /// </summary>
        private async Task<List<DeliveryItem>> BuildLinesAsync(ProformaInvoice proformaInvoice,
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

            var deliverable = (await BuildDeliverableLinesAsync(proformaInvoice, excludeDeliveryId))
                .ToDictionary(x => x.ProductId);

            var lines = new List<DeliveryItem>();

            foreach (var item in requested)
            {
                if (!deliverable.TryGetValue(item.ProductId, out var deliverableLine))
                {
                    throw new InvalidOperationException(
                        "A product that is not part of this proforma invoice cannot be delivered.");
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
        /// Reads every shipment already written against the proforma invoice and works
        /// out what each of its lines still allows.
        /// </summary>
        private async Task<IList<DeliverableLineDto>> BuildDeliverableLinesAsync(ProformaInvoice proformaInvoice,
            Guid? excludeDeliveryId)
        {
            var deliveries = await _deliveryUnitOfWork
                .DeliveryRepository
                .GetDeliveriesByProformaInvoiceAsync(proformaInvoice.Id);

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

            foreach (var item in proformaInvoice.ProformaInvoiceItems ?? new List<ProformaInvoiceItem>())
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
                var available = product.CurrentStock - product.ReservedStock;

                if (quantity > available)
                {
                    throw new InvalidOperationException(
                        $"'{product.ProductName}' has only {available} available, but {quantity} is being delivered.");
                }

                // Whatever was being held for this sale is released together with the
                // goods. Nothing reserves stock yet, so the guard simply keeps the
                // column from ever going below zero.
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
        /// Restates the proforma invoice from the shipments that actually went out.
        /// <paramref name="extraConfirmedLines"/> carries the delivery being confirmed
        /// right now, which is not readable from the database yet.
        /// </summary>
        private async Task SyncProformaInvoiceStatusAsync(ProformaInvoice proformaInvoice, Guid? ignoreDeliveryId,
            IList<DeliveryItem>? extraConfirmedLines)
        {
            var orderedLines = proformaInvoice.ProformaInvoiceItems?.ToList() ?? new List<ProformaInvoiceItem>();

            if (orderedLines.Count == 0)
            {
                return;
            }

            var deliveries = await _deliveryUnitOfWork
                .DeliveryRepository
                .GetDeliveriesByProformaInvoiceAsync(proformaInvoice.Id);

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
            var fullyDelivered = orderedLines.All(x => delivered.GetValueOrDefault(x.ProductId) >= x.Quantity);

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
    }
}
