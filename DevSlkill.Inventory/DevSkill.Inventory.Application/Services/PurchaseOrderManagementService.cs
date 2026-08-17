using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Domain.UnitOfWorkContracts;

namespace DevSkill.Inventory.Application.Services
{
    public class PurchaseOrderManagementService : IPurchaseOrderManagementService
    {
        private readonly IInventoryUnitOfWork _purchaseOrderUnitOfWork;

        public PurchaseOrderManagementService(IInventoryUnitOfWork unitOfWork)
        {
            _purchaseOrderUnitOfWork = unitOfWork;
        }

        public async Task<Guid> CreatePurchaseOrderAsync(PurchaseOrder purchaseOrder)
        {
            await _purchaseOrderUnitOfWork.BeginTransactionAsync();

            try
            {
                var context = await ValidateAsync(purchaseOrder, null);
                var purchaseOrderId = Guid.NewGuid();

                var priced = PriceLines(context, purchaseOrderId);

                var header = new PurchaseOrder
                {
                    Id = purchaseOrderId,
                    PurchaseOrderNo = await GeneratePurchaseOrderNoAsync(),
                    SupplierId = context.Supplier.Id,
                    OrderDate = context.OrderDate,
                    ExpectedDeliveryDate = purchaseOrder.ExpectedDeliveryDate,
                    BusinessLocationId = context.Warehouse.Id,
                    PaymentTermId = context.PaymentTermId,
                    PurchaseRequisitionId = context.Requisition?.Id,
                    SupplierQuotationId = context.Quotation?.Id,
                    Notes = purchaseOrder.Notes ?? string.Empty,
                    Status = RestrictCreateStatus(purchaseOrder.Status),
                    SubTotal = priced.SubTotal,
                    ItemDiscountTotal = priced.ItemDiscountTotal,
                    TotalTax = priced.TotalTax,
                    OtherCharges = context.OtherCharges,
                    GrandTotal = priced.GrandTotal + context.OtherCharges,
                    CreatedBy = purchaseOrder.CreatedBy ?? string.Empty,
                    Created = DateTime.Now,
                    Updated = DateTime.Now,
                    PurchaseOrderItems = priced.Lines
                };

                await _purchaseOrderUnitOfWork.PurchaseOrderRepository.AddAsync(header);

                // The requisition only learns about the order once the order exists,
                // and both moves live in the same transaction.
                if (context.Requisition != null)
                {
                    await ApplyRequisitionConsumptionAsync(context.Requisition, priced.Lines, add: true);
                }

                await _purchaseOrderUnitOfWork.CommitTransactionAsync();

                return purchaseOrderId;
            }
            catch (InvalidOperationException)
            {
                await _purchaseOrderUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _purchaseOrderUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<bool> UpdatePurchaseOrderAsync(PurchaseOrder purchaseOrder)
        {
            await _purchaseOrderUnitOfWork.BeginTransactionAsync();

            try
            {
                var existing = await _purchaseOrderUnitOfWork
                    .PurchaseOrderRepository
                    .GetPurchaseOrderByIdAsync(purchaseOrder.Id)
                    ?? throw new InvalidOperationException("Purchase order not found.");

                GuardAgainstLockedStatus(existing.Status, "edited");
                await GuardAgainstReceiptsAsync(existing.Id, "edited");

                // Supplier, warehouse and where the order came from are what the order
                // is, so an edit may change the lines but never any of those.
                purchaseOrder.SupplierId = existing.SupplierId;
                purchaseOrder.BusinessLocationId = existing.BusinessLocationId;
                purchaseOrder.PurchaseRequisitionId = existing.PurchaseRequisitionId;
                purchaseOrder.SupplierQuotationId = existing.SupplierQuotationId;

                var context = await ValidateAsync(purchaseOrder, existing);
                var oldLines = existing.PurchaseOrderItems?.ToList() ?? new List<PurchaseOrderItem>();

                // Whatever this order was holding on the requisition is handed back
                // before the new lines claim their share.
                if (context.Requisition != null)
                {
                    await ApplyRequisitionConsumptionAsync(context.Requisition, oldLines, add: false);
                }

                await _purchaseOrderUnitOfWork.PurchaseOrderItemRepository.RemoveRangeAsync(oldLines);

                var priced = PriceLines(context, existing.Id);

                foreach (var line in priced.Lines)
                {
                    await _purchaseOrderUnitOfWork.PurchaseOrderItemRepository.AddAsync(line);
                }

                if (context.Requisition != null)
                {
                    await ApplyRequisitionConsumptionAsync(context.Requisition, priced.Lines, add: true);
                }

                // The purchase order number is issued once and never changes.
                existing.OrderDate = context.OrderDate;
                existing.ExpectedDeliveryDate = purchaseOrder.ExpectedDeliveryDate;
                existing.PaymentTermId = context.PaymentTermId;
                existing.Notes = purchaseOrder.Notes ?? string.Empty;
                existing.Status = RestrictUpdateStatus(purchaseOrder.Status);
                existing.SubTotal = priced.SubTotal;
                existing.ItemDiscountTotal = priced.ItemDiscountTotal;
                existing.TotalTax = priced.TotalTax;
                existing.OtherCharges = context.OtherCharges;
                existing.GrandTotal = priced.GrandTotal + context.OtherCharges;
                existing.Updated = DateTime.Now;

                await _purchaseOrderUnitOfWork.PurchaseOrderRepository.EditAsync(existing);
                await _purchaseOrderUnitOfWork.CommitTransactionAsync();

                return true;
            }
            catch (InvalidOperationException)
            {
                await _purchaseOrderUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _purchaseOrderUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<bool> DeletePurchaseOrderAsync(Guid id)
        {
            await _purchaseOrderUnitOfWork.BeginTransactionAsync();

            try
            {
                var purchaseOrder = await _purchaseOrderUnitOfWork
                    .PurchaseOrderRepository
                    .GetPurchaseOrderByIdAsync(id)
                    ?? throw new InvalidOperationException("Purchase order not found.");

                GuardAgainstLockedStatus(purchaseOrder.Status, "deleted");
                await GuardAgainstReceiptsAsync(purchaseOrder.Id, "deleted");

                var lines = purchaseOrder.PurchaseOrderItems?.ToList() ?? new List<PurchaseOrderItem>();

                // The requisition gets its quantity back, otherwise it would stay
                // blocked by an order that no longer exists.
                if (purchaseOrder.PurchaseRequisitionId.HasValue)
                {
                    var requisition = await _purchaseOrderUnitOfWork
                        .PurchaseRequisitionRepository
                        .GetPurchaseRequisitionByIdAsync(purchaseOrder.PurchaseRequisitionId.Value);

                    if (requisition != null)
                    {
                        await ApplyRequisitionConsumptionAsync(requisition, lines, add: false);
                    }
                }

                await _purchaseOrderUnitOfWork.PurchaseOrderItemRepository.RemoveRangeAsync(lines);
                await _purchaseOrderUnitOfWork.PurchaseOrderRepository.RemoveAsync(purchaseOrder);

                await _purchaseOrderUnitOfWork.CommitTransactionAsync();

                return true;
            }
            catch (InvalidOperationException)
            {
                await _purchaseOrderUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _purchaseOrderUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<PurchaseOrder?> GetPurchaseOrderByIdAsync(Guid id)
        {
            return await _purchaseOrderUnitOfWork.PurchaseOrderRepository.GetPurchaseOrderByIdAsync(id);
        }

        public async Task<(IList<PurchaseOrder> data, int total, int totalDisplay)> GetPurchaseOrdersAsync(
            int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            return await _purchaseOrderUnitOfWork
                .PurchaseOrderRepository
                .GetPagedPurchaseOrdersAsync(pageIndex, pageSize, search, order);
        }

        public async Task<IList<PurchaseOrder>> GetPurchaseOrdersByStatusAsync(params PurchaseOrderStatus[] statuses)
        {
            return await _purchaseOrderUnitOfWork.PurchaseOrderRepository.GetPurchaseOrdersByStatusAsync(statuses);
        }

        public async Task<string> GeneratePurchaseOrderNoAsync()
        {
            var count = await _purchaseOrderUnitOfWork.PurchaseOrderRepository.GetCountAsync();

            string purchaseOrderNo;
            do
            {
                count++;
                purchaseOrderNo = $"PO-{DateTime.Now:yyyyMM}-{count:D4}";
            }
            while (await _purchaseOrderUnitOfWork.PurchaseOrderRepository.IsPurchaseOrderNoDuplicateAsync(purchaseOrderNo));

            return purchaseOrderNo;
        }

        public async Task SubmitPurchaseOrderAsync(Guid id)
        {
            var purchaseOrder = await GetOrderAsync(id);

            if (purchaseOrder.Status != PurchaseOrderStatus.Draft)
            {
                throw new InvalidOperationException("Only a draft purchase order can be submitted for approval.");
            }

            if (purchaseOrder.PurchaseOrderItems == null || purchaseOrder.PurchaseOrderItems.Count == 0)
            {
                throw new InvalidOperationException("A purchase order without any product line cannot be submitted.");
            }

            purchaseOrder.Status = PurchaseOrderStatus.Submitted;
            purchaseOrder.Updated = DateTime.Now;

            await SaveHeaderAsync(purchaseOrder);
        }

        public async Task ApprovePurchaseOrderAsync(Guid id, Guid? approvedById, string approvedByName)
        {
            var purchaseOrder = await GetSubmittedOrderAsync(id, "approved");

            purchaseOrder.Status = PurchaseOrderStatus.Approved;
            purchaseOrder.ApprovedById = approvedById;
            purchaseOrder.ApprovedByName = approvedByName ?? string.Empty;
            purchaseOrder.ApprovedDate = DateTime.Now;
            purchaseOrder.RejectionReason = string.Empty;
            purchaseOrder.Updated = DateTime.Now;

            await SaveHeaderAsync(purchaseOrder);
        }

        public async Task RejectPurchaseOrderAsync(Guid id, Guid? approvedById, string approvedByName, string reason)
        {
            if (string.IsNullOrWhiteSpace(reason))
            {
                throw new InvalidOperationException("A rejection reason is required.");
            }

            var purchaseOrder = await GetSubmittedOrderAsync(id, "rejected");

            purchaseOrder.Status = PurchaseOrderStatus.Rejected;
            purchaseOrder.ApprovedById = approvedById;
            purchaseOrder.ApprovedByName = approvedByName ?? string.Empty;
            purchaseOrder.ApprovedDate = DateTime.Now;
            purchaseOrder.RejectionReason = reason.Trim();
            purchaseOrder.Updated = DateTime.Now;

            await SaveHeaderAsync(purchaseOrder);
        }

        /// <summary>
        /// Withdraws the order. Anything already received has to be returned through
        /// the purchase return flow first, so a receipt blocks the cancellation.
        /// </summary>
        public async Task CancelPurchaseOrderAsync(Guid id)
        {
            var purchaseOrder = await GetOrderAsync(id);

            if (purchaseOrder.Status is PurchaseOrderStatus.Cancelled or PurchaseOrderStatus.Closed)
            {
                throw new InvalidOperationException("This purchase order is already closed.");
            }

            await GuardAgainstReceiptsAsync(purchaseOrder.Id, "cancelled");

            purchaseOrder.Status = PurchaseOrderStatus.Cancelled;
            purchaseOrder.Updated = DateTime.Now;

            await SaveHeaderAsync(purchaseOrder);
        }

        /// <summary>
        /// Finishes an order short of its ordered quantity, for when the supplier will
        /// send no more. Only a part received order can be closed this way.
        /// </summary>
        public async Task ClosePurchaseOrderAsync(Guid id)
        {
            var purchaseOrder = await GetOrderAsync(id);

            if (purchaseOrder.Status is not (PurchaseOrderStatus.Approved or PurchaseOrderStatus.PartiallyReceived))
            {
                throw new InvalidOperationException(
                    "Only an approved or partly received purchase order can be closed short.");
            }

            purchaseOrder.Status = PurchaseOrderStatus.Closed;
            purchaseOrder.Updated = DateTime.Now;

            await SaveHeaderAsync(purchaseOrder);
        }

        public async Task<IList<ConvertibleRequisitionLineDto>> GetConvertibleRequisitionLinesAsync(
            Guid purchaseRequisitionId)
        {
            var requisition = await _purchaseOrderUnitOfWork
                .PurchaseRequisitionRepository
                .GetPurchaseRequisitionByIdAsync(purchaseRequisitionId);

            if (requisition == null)
            {
                return new List<ConvertibleRequisitionLineDto>();
            }

            if (requisition.Status is not (PurchaseRequisitionStatus.Approved
                or PurchaseRequisitionStatus.PartiallyConverted))
            {
                throw new InvalidOperationException(
                    "Only an approved requisition can be turned into a purchase order.");
            }

            var lines = new List<ConvertibleRequisitionLineDto>();

            foreach (var item in requisition.PurchaseRequisitionItems ?? new List<PurchaseRequisitionItem>())
            {
                var remaining = item.Quantity - item.OrderedQuantity;

                lines.Add(new ConvertibleRequisitionLineDto
                {
                    ProductId = item.ProductId,
                    ProductName = item.Product?.ProductName ?? string.Empty,
                    UnitName = item.Product?.Unit?.UnitName ?? string.Empty,
                    RequestedQuantity = item.Quantity,
                    OrderedQuantity = item.OrderedQuantity,
                    RemainingQuantity = remaining < 0 ? 0 : remaining,
                    EstimatedUnitPrice = item.EstimatedUnitPrice
                });
            }

            return lines;
        }

        public async Task<PurchaseLinePreviewDto?> GetLinePreviewAsync(Guid productId, decimal quantity,
            decimal unitPrice, decimal discountAmount)
        {
            if (productId == Guid.Empty || quantity <= 0)
            {
                return null;
            }

            var product = await _purchaseOrderUnitOfWork.ProductRepository.GetByIdAsync(productId);

            if (product == null)
            {
                return null;
            }

            var line = CalculateLine(quantity, unitPrice, discountAmount,
                await GetTaxRateAsync(product.ApplicableTaxId));

            return new PurchaseLinePreviewDto
            {
                UnitName = product.Unit?.UnitName ?? string.Empty,
                CurrentStock = product.CurrentStock,
                LineAmount = line.LineAmount,
                DiscountAmount = line.DiscountAmount,
                TaxRate = line.TaxRate,
                TaxAmount = line.TaxAmount,
                LineTotal = line.LineTotal
            };
        }

        /// <summary>
        /// Everything create and update both need before any line can be priced.
        /// </summary>
        private async Task<OrderContext> ValidateAsync(PurchaseOrder purchaseOrder, PurchaseOrder? existing)
        {
            var supplier = await _purchaseOrderUnitOfWork
                .SupplierRepository
                .GetByIdAsync(purchaseOrder.SupplierId)
                ?? throw new InvalidOperationException("Supplier not found.");

            // An existing order keeps the supplier it was raised on, so a supplier that
            // went inactive later does not block an edit of that order.
            if (existing == null && supplier.Status != SupplierStatus.Active)
            {
                throw new InvalidOperationException(
                    $"'{supplier.SupplierName}' is {supplier.Status.ToString().ToLower()}, so no purchase order can be raised on it.");
            }

            var warehouse = await _purchaseOrderUnitOfWork
                .BusinessLocationRepository
                .GetByIdAsync(purchaseOrder.BusinessLocationId)
                ?? throw new InvalidOperationException("Warehouse not found.");

            if (existing == null && !warehouse.IsActive)
            {
                throw new InvalidOperationException(
                    $"'{warehouse.LocationName}' is inactive, so goods cannot be ordered into it.");
            }

            var orderDate = purchaseOrder.OrderDate == default ? DateTime.Now : purchaseOrder.OrderDate;

            if (purchaseOrder.ExpectedDeliveryDate.Date < orderDate.Date)
            {
                throw new InvalidOperationException("Expected Delivery Date cannot be earlier than the order date.");
            }

            if (purchaseOrder.OtherCharges < 0)
            {
                throw new InvalidOperationException("Other charges cannot be negative.");
            }

            var paymentTermId = await ResolvePaymentTermIdAsync(purchaseOrder.PaymentTermId);
            var requisition = await ResolveRequisitionAsync(purchaseOrder.PurchaseRequisitionId, warehouse);
            var quotation = await ResolveQuotationAsync(purchaseOrder.SupplierQuotationId, supplier);

            var grouped = purchaseOrder.PurchaseOrderItems?
                .Where(x => x.ProductId != Guid.Empty && x.Quantity > 0)
                .GroupBy(x => x.ProductId)
                .Select(g => new RequestedLine(
                    g.Key,
                    g.Sum(x => x.Quantity),
                    g.Max(x => x.UnitPrice),
                    g.Sum(x => x.DiscountAmount)))
                .ToList();

            if (grouped == null || grouped.Count == 0)
            {
                throw new InvalidOperationException("At least one product line with a positive quantity is required.");
            }

            var validated = new List<ValidatedLine>();

            foreach (var requested in grouped)
            {
                var product = await _purchaseOrderUnitOfWork
                    .ProductRepository
                    .GetByIdAsync(requested.ProductId)
                    ?? throw new InvalidOperationException("Product not found.");

                if (product.Status != ProductStatus.Active)
                {
                    throw new InvalidOperationException($"'{product.ProductName}' is inactive and cannot be ordered.");
                }

                // The dropdown only offers the warehouse's own products; this makes the
                // same rule hold for anything that reaches the server another way.
                if (product.BusinessLocationId != warehouse.Id)
                {
                    throw new InvalidOperationException(
                        $"'{product.ProductName}' does not belong to '{warehouse.LocationName}'.");
                }

                if (requested.UnitPrice < 0)
                {
                    throw new InvalidOperationException($"The unit price of '{product.ProductName}' cannot be negative.");
                }

                var lineAmount = Round(requested.UnitPrice * requested.Quantity);

                if (requested.DiscountAmount < 0 || requested.DiscountAmount > lineAmount)
                {
                    throw new InvalidOperationException(
                        $"The discount on '{product.ProductName}' must be between 0 and {lineAmount:N2}.");
                }

                GuardAgainstRequisitionOverrun(requisition, existing, product, requested.Quantity);

                validated.Add(new ValidatedLine(
                    product.Id,
                    requested.Quantity,
                    requested.UnitPrice,
                    requested.DiscountAmount,
                    await GetTaxRateAsync(product.ApplicableTaxId)));
            }

            return new OrderContext(supplier, warehouse, requisition, quotation, validated, orderDate,
                paymentTermId, Round(purchaseOrder.OtherCharges));
        }

        /// <summary>
        /// An order may only name a quotation that actually won, and only the winner's
        /// own supplier, so the price on the order stays tied to the quote it was won
        /// on rather than to any offer that happens to exist.
        /// </summary>
        private async Task<SupplierQuotation?> ResolveQuotationAsync(Guid? supplierQuotationId, Supplier supplier)
        {
            if (!supplierQuotationId.HasValue || supplierQuotationId.Value == Guid.Empty)
            {
                return null;
            }

            var quotation = await _purchaseOrderUnitOfWork
                .SupplierQuotationRepository
                .GetSupplierQuotationByIdAsync(supplierQuotationId.Value)
                ?? throw new InvalidOperationException("Supplier quotation not found.");

            if (quotation.Status != SupplierQuotationStatus.Selected)
            {
                throw new InvalidOperationException(
                    "Only the quotation that won its request can be turned into a purchase order.");
            }

            if (quotation.SupplierId != supplier.Id)
            {
                throw new InvalidOperationException(
                    $"Quotation '{quotation.QuotationNo}' belongs to another supplier, so '{supplier.SupplierName}' cannot be ordered from on it.");
            }

            return quotation;
        }

        /// <summary>
        /// A purchase order built from a requisition may never order more of a product
        /// than the requisition still has open. What this very order already holds is
        /// added back first, so an edit is measured against the right figure.
        /// </summary>
        private static void GuardAgainstRequisitionOverrun(PurchaseRequisition? requisition, PurchaseOrder? existing,
            Product product, decimal quantity)
        {
            if (requisition == null)
            {
                return;
            }

            var requisitionLine = requisition.PurchaseRequisitionItems?
                .FirstOrDefault(x => x.ProductId == product.Id)
                ?? throw new InvalidOperationException(
                    $"'{product.ProductName}' is not part of requisition '{requisition.RequisitionNo}'.");

            var heldByThisOrder = existing?.PurchaseOrderItems?
                .Where(x => x.ProductId == product.Id)
                .Sum(x => x.Quantity) ?? 0m;

            var available = requisitionLine.Quantity - requisitionLine.OrderedQuantity + heldByThisOrder;

            if (quantity > available)
            {
                throw new InvalidOperationException(
                    $"'{product.ProductName}' has only {available:N2} left on requisition " +
                    $"'{requisition.RequisitionNo}', but {quantity:N2} is being ordered.");
            }
        }

        private async Task<PurchaseRequisition?> ResolveRequisitionAsync(Guid? requisitionId, BusinessLocation warehouse)
        {
            if (!requisitionId.HasValue || requisitionId.Value == Guid.Empty)
            {
                return null;
            }

            var requisition = await _purchaseOrderUnitOfWork
                .PurchaseRequisitionRepository
                .GetPurchaseRequisitionByIdAsync(requisitionId.Value)
                ?? throw new InvalidOperationException("Purchase requisition not found.");

            if (requisition.Status is not (PurchaseRequisitionStatus.Approved
                or PurchaseRequisitionStatus.PartiallyConverted
                or PurchaseRequisitionStatus.Converted))
            {
                throw new InvalidOperationException(
                    "Only an approved requisition can be turned into a purchase order.");
            }

            if (requisition.BusinessLocationId != warehouse.Id)
            {
                throw new InvalidOperationException(
                    "The purchase order must be delivered into the same warehouse the requisition was raised for.");
            }

            return requisition;
        }

        /// <summary>
        /// Moves the ordered quantity on the source requisition and re-reads its
        /// status from the lines, so it says exactly how much is still open.
        /// </summary>
        private async Task ApplyRequisitionConsumptionAsync(PurchaseRequisition requisition,
            IEnumerable<PurchaseOrderItem> lines, bool add)
        {
            foreach (var line in lines)
            {
                var requisitionLine = requisition.PurchaseRequisitionItems?
                    .FirstOrDefault(x => x.ProductId == line.ProductId);

                if (requisitionLine == null)
                {
                    continue;
                }

                requisitionLine.OrderedQuantity += add ? line.Quantity : -line.Quantity;

                if (requisitionLine.OrderedQuantity < 0)
                {
                    requisitionLine.OrderedQuantity = 0;
                }

                await _purchaseOrderUnitOfWork.PurchaseRequisitionItemRepository.EditAsync(requisitionLine);
            }

            var items = requisition.PurchaseRequisitionItems ?? new List<PurchaseRequisitionItem>();

            requisition.Status = items.All(x => x.OrderedQuantity >= x.Quantity)
                ? PurchaseRequisitionStatus.Converted
                : items.Any(x => x.OrderedQuantity > 0)
                    ? PurchaseRequisitionStatus.PartiallyConverted
                    : PurchaseRequisitionStatus.Approved;

            requisition.Updated = DateTime.Now;

            await _purchaseOrderUnitOfWork.PurchaseRequisitionRepository.EditAsync(requisition);
        }

        private async Task<Guid?> ResolvePaymentTermIdAsync(Guid? paymentTermId)
        {
            if (!paymentTermId.HasValue || paymentTermId.Value == Guid.Empty)
            {
                return null;
            }

            var paymentTerm = await _purchaseOrderUnitOfWork
                .PaymentTermRepository
                .GetByIdAsync(paymentTermId.Value)
                ?? throw new InvalidOperationException("Payment term not found.");

            if (!paymentTerm.IsActive)
            {
                throw new InvalidOperationException($"'{paymentTerm.TermName}' is inactive and cannot be used.");
            }

            return paymentTerm.Id;
        }

        /// <summary>
        /// Turns the validated lines into stored lines. This is the single place where
        /// the money on a purchase order is decided.
        /// </summary>
        private static PricedResult PriceLines(OrderContext context, Guid purchaseOrderId)
        {
            decimal subTotal = 0m;
            decimal itemDiscountTotal = 0m;
            decimal totalTax = 0m;

            var lines = new List<PurchaseOrderItem>();

            foreach (var validated in context.RequestedItems)
            {
                var calculated = CalculateLine(validated.Quantity, validated.UnitPrice,
                    validated.DiscountAmount, validated.TaxRate);

                lines.Add(new PurchaseOrderItem
                {
                    Id = Guid.NewGuid(),
                    PurchaseOrderId = purchaseOrderId,
                    ProductId = validated.ProductId,
                    Quantity = validated.Quantity,
                    UnitPrice = validated.UnitPrice,
                    DiscountAmount = calculated.DiscountAmount,
                    TaxRate = calculated.TaxRate,
                    TaxAmount = calculated.TaxAmount,
                    LineTotal = calculated.LineTotal,
                    ReceivedQuantity = 0,
                    InvoicedQuantity = 0
                });

                subTotal += calculated.LineAmount;
                itemDiscountTotal += calculated.DiscountAmount;
                totalTax += calculated.TaxAmount;
            }

            return new PricedResult(
                lines,
                Round(subTotal),
                Round(itemDiscountTotal),
                Round(totalTax),
                Round(subTotal - itemDiscountTotal + totalTax));
        }

        private static CalculatedLine CalculateLine(decimal quantity, decimal unitPrice,
            decimal discountAmount, decimal taxRate)
        {
            var lineAmount = Round(unitPrice * quantity);
            var discount = Round(Math.Clamp(discountAmount, 0m, lineAmount));
            var taxableAmount = lineAmount - discount;
            var taxAmount = Round(taxableAmount * taxRate / 100m);

            return new CalculatedLine(lineAmount, discount, taxRate, taxAmount, taxableAmount + taxAmount);
        }

        private async Task<decimal> GetTaxRateAsync(Guid? applicableTaxId)
        {
            if (!applicableTaxId.HasValue || applicableTaxId.Value == Guid.Empty)
            {
                return 0m;
            }

            var applicableTax = await _purchaseOrderUnitOfWork
                .ApplicableTaxRepository
                .GetByIdAsync(applicableTaxId.Value);

            return applicableTax?.TaxRate ?? 0m;
        }

        private async Task<PurchaseOrder> GetOrderAsync(Guid id)
        {
            return await _purchaseOrderUnitOfWork
                .PurchaseOrderRepository
                .GetPurchaseOrderByIdAsync(id)
                ?? throw new InvalidOperationException("Purchase order not found.");
        }

        private async Task<PurchaseOrder> GetSubmittedOrderAsync(Guid id, string action)
        {
            var purchaseOrder = await GetOrderAsync(id);

            if (purchaseOrder.Status != PurchaseOrderStatus.Submitted)
            {
                throw new InvalidOperationException($"Only a submitted purchase order can be {action}.");
            }

            return purchaseOrder;
        }

        private async Task SaveHeaderAsync(PurchaseOrder purchaseOrder)
        {
            await _purchaseOrderUnitOfWork.PurchaseOrderRepository.EditAsync(purchaseOrder);
            await _purchaseOrderUnitOfWork.SaveAsync();
        }

        /// <summary>
        /// An order that is being approved, has been received against, or was closed
        /// is out of the buyer's hands.
        /// </summary>
        private static void GuardAgainstLockedStatus(PurchaseOrderStatus status, string action)
        {
            if (status == PurchaseOrderStatus.Submitted)
            {
                throw new InvalidOperationException(
                    $"A purchase order waiting for approval cannot be {action}. Ask the approver to reject it first.");
            }

            if (status is PurchaseOrderStatus.PartiallyReceived or PurchaseOrderStatus.Received)
            {
                throw new InvalidOperationException($"A received purchase order cannot be {action}.");
            }

            if (status == PurchaseOrderStatus.Cancelled)
            {
                throw new InvalidOperationException($"A cancelled purchase order cannot be {action}.");
            }

            if (status == PurchaseOrderStatus.Closed)
            {
                throw new InvalidOperationException($"A closed purchase order cannot be {action}.");
            }
        }

        /// <summary>
        /// Even a draft goods receipt states what the warehouse expects, so the order
        /// behind it stays as it is until that receipt is cancelled.
        /// </summary>
        private async Task GuardAgainstReceiptsAsync(Guid purchaseOrderId, string action)
        {
            var receipts = await _purchaseOrderUnitOfWork
                .GoodsReceiptRepository
                .GetGoodsReceiptsByPurchaseOrderAsync(purchaseOrderId);

            if (receipts.Any(x => x.Status != GoodsReceiptStatus.Cancelled))
            {
                throw new InvalidOperationException(
                    $"A purchase order that already has a goods receipt cannot be {action}.");
            }
        }

        private static PurchaseOrderStatus RestrictCreateStatus(PurchaseOrderStatus status)
        {
            // A brand new order is either kept as a draft or sent straight for
            // approval. It can never approve itself.
            return status == PurchaseOrderStatus.Submitted
                ? PurchaseOrderStatus.Submitted
                : PurchaseOrderStatus.Draft;
        }

        private static PurchaseOrderStatus RestrictUpdateStatus(PurchaseOrderStatus status)
        {
            // Approval, receiving and closing all belong to their own flows. An edit
            // may never reach for any of them.
            return status is PurchaseOrderStatus.Draft or PurchaseOrderStatus.Submitted
                ? status
                : PurchaseOrderStatus.Draft;
        }

        private static decimal Round(decimal value)
        {
            return Math.Round(value, 2, MidpointRounding.AwayFromZero);
        }

        private sealed record RequestedLine(Guid ProductId, decimal Quantity, decimal UnitPrice, decimal DiscountAmount);

        private sealed record ValidatedLine(Guid ProductId, decimal Quantity, decimal UnitPrice,
            decimal DiscountAmount, decimal TaxRate);

        private sealed record CalculatedLine(decimal LineAmount, decimal DiscountAmount, decimal TaxRate,
            decimal TaxAmount, decimal LineTotal);

        private sealed record OrderContext(
            Supplier Supplier,
            BusinessLocation Warehouse,
            PurchaseRequisition? Requisition,
            SupplierQuotation? Quotation,
            IList<ValidatedLine> RequestedItems,
            DateTime OrderDate,
            Guid? PaymentTermId,
            decimal OtherCharges);

        private sealed record PricedResult(
            List<PurchaseOrderItem> Lines,
            decimal SubTotal,
            decimal ItemDiscountTotal,
            decimal TotalTax,
            decimal GrandTotal);
    }
}
