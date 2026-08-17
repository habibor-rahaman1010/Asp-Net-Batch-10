using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Domain.UnitOfWorkContracts;

namespace DevSkill.Inventory.Application.Services
{
    public class SupplierQuotationManagementService : ISupplierQuotationManagementService
    {
        private readonly IInventoryUnitOfWork _supplierQuotationUnitOfWork;

        public SupplierQuotationManagementService(IInventoryUnitOfWork unitOfWork)
        {
            _supplierQuotationUnitOfWork = unitOfWork;
        }

        public async Task<Guid> CreateSupplierQuotationAsync(SupplierQuotation supplierQuotation)
        {
            await _supplierQuotationUnitOfWork.BeginTransactionAsync();

            try
            {
                var context = await ValidateAsync(supplierQuotation, null);
                var supplierQuotationId = Guid.NewGuid();

                var lines = BuildLines(context.Lines, supplierQuotationId);

                var header = new SupplierQuotation
                {
                    Id = supplierQuotationId,
                    QuotationNo = await GenerateQuotationNoAsync(),
                    SupplierQuotationNo = supplierQuotation.SupplierQuotationNo ?? string.Empty,
                    RequestForQuotationId = context.RequestForQuotation.Id,
                    SupplierId = context.Supplier.Id,
                    QuotationDate = context.QuotationDate,
                    ValidUntil = context.ValidUntil,
                    DeliveryDays = context.DeliveryDays,
                    PaymentTermId = context.PaymentTermId,
                    Notes = supplierQuotation.Notes ?? string.Empty,
                    Status = SupplierQuotationStatus.Received,
                    OtherCharges = context.OtherCharges,
                    CreatedBy = supplierQuotation.CreatedBy ?? string.Empty,
                    Created = DateTime.Now,
                    Updated = DateTime.Now,
                    SupplierQuotationItems = lines
                };

                ApplyTotals(header, lines);

                await _supplierQuotationUnitOfWork.SupplierQuotationRepository.AddAsync(header);
                await _supplierQuotationUnitOfWork.CommitTransactionAsync();

                return supplierQuotationId;
            }
            catch (InvalidOperationException)
            {
                await _supplierQuotationUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _supplierQuotationUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<bool> UpdateSupplierQuotationAsync(SupplierQuotation supplierQuotation)
        {
            await _supplierQuotationUnitOfWork.BeginTransactionAsync();

            try
            {
                var existing = await _supplierQuotationUnitOfWork
                    .SupplierQuotationRepository
                    .GetSupplierQuotationByIdAsync(supplierQuotation.Id)
                    ?? throw new InvalidOperationException("Supplier quotation not found.");

                // A quotation that has already won or lost is the record of a decision
                // that was made on these very figures.
                if (existing.Status != SupplierQuotationStatus.Received)
                {
                    throw new InvalidOperationException(
                        $"A {existing.Status.ToString().ToLower()} quotation cannot be edited.");
                }

                // The request and the supplier are what the quotation is.
                supplierQuotation.RequestForQuotationId = existing.RequestForQuotationId;
                supplierQuotation.SupplierId = existing.SupplierId;

                var context = await ValidateAsync(supplierQuotation, existing);

                await _supplierQuotationUnitOfWork
                    .SupplierQuotationItemRepository
                    .RemoveRangeAsync(existing.SupplierQuotationItems?.ToList()
                        ?? new List<SupplierQuotationItem>());

                var lines = BuildLines(context.Lines, existing.Id);

                foreach (var line in lines)
                {
                    await _supplierQuotationUnitOfWork.SupplierQuotationItemRepository.AddAsync(line);
                }

                // The quotation number is issued once and never changes.
                existing.SupplierQuotationNo = supplierQuotation.SupplierQuotationNo ?? string.Empty;
                existing.QuotationDate = context.QuotationDate;
                existing.ValidUntil = context.ValidUntil;
                existing.DeliveryDays = context.DeliveryDays;
                existing.PaymentTermId = context.PaymentTermId;
                existing.OtherCharges = context.OtherCharges;
                existing.Notes = supplierQuotation.Notes ?? string.Empty;
                existing.Updated = DateTime.Now;

                ApplyTotals(existing, lines);

                await _supplierQuotationUnitOfWork.SupplierQuotationRepository.EditAsync(existing);
                await _supplierQuotationUnitOfWork.CommitTransactionAsync();

                return true;
            }
            catch (InvalidOperationException)
            {
                await _supplierQuotationUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _supplierQuotationUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<bool> DeleteSupplierQuotationAsync(Guid id)
        {
            await _supplierQuotationUnitOfWork.BeginTransactionAsync();

            try
            {
                var supplierQuotation = await _supplierQuotationUnitOfWork
                    .SupplierQuotationRepository
                    .GetSupplierQuotationByIdAsync(id)
                    ?? throw new InvalidOperationException("Supplier quotation not found.");

                // Deleting the winner, or one of the offers it beat, would leave the
                // award standing on a comparison that can no longer be seen.
                if (supplierQuotation.Status is SupplierQuotationStatus.Selected
                    or SupplierQuotationStatus.Rejected)
                {
                    throw new InvalidOperationException(
                        $"A {supplierQuotation.Status.ToString().ToLower()} quotation cannot be deleted, " +
                        "because the award was decided on it.");
                }

                await _supplierQuotationUnitOfWork
                    .SupplierQuotationItemRepository
                    .RemoveRangeAsync(supplierQuotation.SupplierQuotationItems?.ToList()
                        ?? new List<SupplierQuotationItem>());

                await _supplierQuotationUnitOfWork.SupplierQuotationRepository.RemoveAsync(supplierQuotation);
                await _supplierQuotationUnitOfWork.CommitTransactionAsync();

                return true;
            }
            catch (InvalidOperationException)
            {
                await _supplierQuotationUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _supplierQuotationUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<SupplierQuotation?> GetSupplierQuotationByIdAsync(Guid id)
        {
            return await _supplierQuotationUnitOfWork
                .SupplierQuotationRepository
                .GetSupplierQuotationByIdAsync(id);
        }

        public async Task<(IList<SupplierQuotation> data, int total, int totalDisplay)> GetSupplierQuotationsAsync(
            int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            return await _supplierQuotationUnitOfWork
                .SupplierQuotationRepository
                .GetPagedSupplierQuotationsAsync(pageIndex, pageSize, search, order);
        }

        public async Task<IList<SupplierQuotation>> GetSupplierQuotationsByRequestAsync(Guid requestForQuotationId)
        {
            return await _supplierQuotationUnitOfWork
                .SupplierQuotationRepository
                .GetSupplierQuotationsByRequestAsync(requestForQuotationId);
        }

        public async Task<IList<SupplierQuotation>> GetAwardedQuotationsAsync()
        {
            return await _supplierQuotationUnitOfWork
                .SupplierQuotationRepository
                .GetSupplierQuotationsByStatusAsync(SupplierQuotationStatus.Selected);
        }

        public async Task<string> GenerateQuotationNoAsync()
        {
            var count = await _supplierQuotationUnitOfWork.SupplierQuotationRepository.GetCountAsync();

            string quotationNo;
            do
            {
                count++;
                quotationNo = $"SQT-{DateTime.Now:yyyyMM}-{count:D4}";
            }
            while (await _supplierQuotationUnitOfWork.SupplierQuotationRepository
                .IsQuotationNoDuplicateAsync(quotationNo));

            return quotationNo;
        }

        public async Task<QuotationComparisonDto> GetComparisonAsync(Guid requestForQuotationId)
        {
            var request = await _supplierQuotationUnitOfWork
                .RequestForQuotationRepository
                .GetRequestForQuotationByIdAsync(requestForQuotationId)
                ?? throw new InvalidOperationException("Request for quotation not found.");

            var quotations = (await _supplierQuotationUnitOfWork
                .SupplierQuotationRepository
                .GetSupplierQuotationsByRequestAsync(requestForQuotationId))
                .Where(x => x.Status != SupplierQuotationStatus.Cancelled)
                .ToList();

            var comparison = new QuotationComparisonDto
            {
                RequestForQuotationId = request.Id,
                RfqNo = request.RfqNo,
                WarehouseName = request.BusinessLocation?.LocationName ?? string.Empty,
                RfqDate = request.RfqDate,
                ResponseDeadline = request.ResponseDeadline,
                Status = request.Status.ToString(),
                SelectedSupplierQuotationId = request.SelectedSupplierQuotationId
            };

            var requestedLines = (request.RequestForQuotationItems ?? new List<RequestForQuotationItem>()).ToList();

            foreach (var line in requestedLines)
            {
                comparison.RequestedLines.Add(new QuotationRequestedLineDto
                {
                    ProductId = line.ProductId,
                    ProductName = line.Product?.ProductName ?? string.Empty,
                    UnitName = line.Product?.Unit?.UnitName ?? string.Empty,
                    Quantity = line.Quantity
                });
            }

            foreach (var quotation in quotations)
            {
                var lines = (quotation.SupplierQuotationItems ?? new List<SupplierQuotationItem>()).ToList();

                comparison.Offers.Add(new QuotationOfferDto
                {
                    SupplierQuotationId = quotation.Id,
                    QuotationNo = quotation.QuotationNo,
                    SupplierQuotationNo = quotation.SupplierQuotationNo,
                    SupplierName = quotation.Supplier?.SupplierName ?? string.Empty,
                    QuotationDate = quotation.QuotationDate,
                    ValidUntil = quotation.ValidUntil,
                    DeliveryDays = quotation.DeliveryDays,
                    PaymentTermName = quotation.PaymentTerm?.TermName ?? "—",
                    SubTotal = quotation.SubTotal,
                    DiscountTotal = quotation.DiscountTotal,
                    OtherCharges = quotation.OtherCharges,
                    GrandTotal = quotation.GrandTotal,
                    Status = quotation.Status.ToString(),
                    IsExpired = quotation.ValidUntil.Date < DateTime.Today,

                    // A supplier may quote only part of what was asked for. Saying so
                    // stops a short quote from looking cheap next to a complete one.
                    IsComplete = requestedLines.All(requested =>
                        lines.Any(x => x.ProductId == requested.ProductId && x.Quantity >= requested.Quantity)),

                    Lines = requestedLines.Select(requested =>
                    {
                        var line = lines.FirstOrDefault(x => x.ProductId == requested.ProductId);

                        return new QuotationOfferLineDto
                        {
                            ProductId = requested.ProductId,
                            Quantity = line?.Quantity ?? 0m,
                            UnitPrice = line?.UnitPrice ?? 0m,
                            LineTotal = line?.LineTotal ?? 0m
                        };
                    }).ToList()
                });
            }

            MarkBest(comparison);

            return comparison;
        }

        public async Task SelectWinningQuotationAsync(Guid supplierQuotationId)
        {
            await _supplierQuotationUnitOfWork.BeginTransactionAsync();

            try
            {
                var winner = await _supplierQuotationUnitOfWork
                    .SupplierQuotationRepository
                    .GetSupplierQuotationByIdAsync(supplierQuotationId)
                    ?? throw new InvalidOperationException("Supplier quotation not found.");

                if (winner.Status == SupplierQuotationStatus.Cancelled)
                {
                    throw new InvalidOperationException("A cancelled quotation cannot win the request.");
                }

                var request = await _supplierQuotationUnitOfWork
                    .RequestForQuotationRepository
                    .GetRequestForQuotationByIdAsync(winner.RequestForQuotationId)
                    ?? throw new InvalidOperationException("Request for quotation not found.");

                if (request.Status is not (RequestForQuotationStatus.Sent or RequestForQuotationStatus.Closed))
                {
                    throw new InvalidOperationException(
                        $"A {request.Status.ToString().ToLower()} request cannot be awarded. " +
                        "Send it out first, and undo an existing award before picking a different winner.");
                }

                var quotations = await _supplierQuotationUnitOfWork
                    .SupplierQuotationRepository
                    .GetSupplierQuotationsByRequestAsync(request.Id);

                // Exactly one offer wins; every other live one on the request loses at
                // the same moment, so the comparison stays readable afterwards.
                foreach (var quotation in quotations.Where(x => x.Status != SupplierQuotationStatus.Cancelled))
                {
                    quotation.Status = quotation.Id == winner.Id
                        ? SupplierQuotationStatus.Selected
                        : SupplierQuotationStatus.Rejected;

                    quotation.Updated = DateTime.Now;

                    await _supplierQuotationUnitOfWork.SupplierQuotationRepository.EditAsync(quotation);
                }

                request.Status = RequestForQuotationStatus.Awarded;
                request.SelectedSupplierQuotationId = winner.Id;
                request.Updated = DateTime.Now;

                await _supplierQuotationUnitOfWork.RequestForQuotationRepository.EditAsync(request);
                await _supplierQuotationUnitOfWork.CommitTransactionAsync();
            }
            catch (InvalidOperationException)
            {
                await _supplierQuotationUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _supplierQuotationUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task UndoAwardAsync(Guid requestForQuotationId)
        {
            await _supplierQuotationUnitOfWork.BeginTransactionAsync();

            try
            {
                var request = await _supplierQuotationUnitOfWork
                    .RequestForQuotationRepository
                    .GetRequestForQuotationByIdAsync(requestForQuotationId)
                    ?? throw new InvalidOperationException("Request for quotation not found.");

                if (request.Status != RequestForQuotationStatus.Awarded)
                {
                    throw new InvalidOperationException("This request for quotation has not been awarded.");
                }

                // An order raised from the winning quotation rests on that award, so it
                // cannot be pulled out from underneath it.
                if (request.SelectedSupplierQuotationId.HasValue
                    && await HasPurchaseOrderAsync(request.SelectedSupplierQuotationId.Value))
                {
                    throw new InvalidOperationException(
                        "A purchase order has already been raised from the winning quotation, " +
                        "so the award cannot be undone.");
                }

                var quotations = await _supplierQuotationUnitOfWork
                    .SupplierQuotationRepository
                    .GetSupplierQuotationsByRequestAsync(request.Id);

                foreach (var quotation in quotations.Where(x => x.Status is SupplierQuotationStatus.Selected
                    or SupplierQuotationStatus.Rejected))
                {
                    quotation.Status = SupplierQuotationStatus.Received;
                    quotation.Updated = DateTime.Now;

                    await _supplierQuotationUnitOfWork.SupplierQuotationRepository.EditAsync(quotation);
                }

                request.Status = RequestForQuotationStatus.Sent;
                request.SelectedSupplierQuotationId = null;
                request.Updated = DateTime.Now;

                await _supplierQuotationUnitOfWork.RequestForQuotationRepository.EditAsync(request);
                await _supplierQuotationUnitOfWork.CommitTransactionAsync();
            }
            catch (InvalidOperationException)
            {
                await _supplierQuotationUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _supplierQuotationUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task CancelSupplierQuotationAsync(Guid id)
        {
            await _supplierQuotationUnitOfWork.BeginTransactionAsync();

            try
            {
                var quotation = await _supplierQuotationUnitOfWork
                    .SupplierQuotationRepository
                    .GetSupplierQuotationByIdAsync(id)
                    ?? throw new InvalidOperationException("Supplier quotation not found.");

                if (quotation.Status == SupplierQuotationStatus.Cancelled)
                {
                    throw new InvalidOperationException("This quotation is already cancelled.");
                }

                if (quotation.Status == SupplierQuotationStatus.Selected)
                {
                    throw new InvalidOperationException(
                        "The winning quotation cannot be cancelled. Undo the award on the request first.");
                }

                quotation.Status = SupplierQuotationStatus.Cancelled;
                quotation.Updated = DateTime.Now;

                await _supplierQuotationUnitOfWork.SupplierQuotationRepository.EditAsync(quotation);
                await _supplierQuotationUnitOfWork.CommitTransactionAsync();
            }
            catch (InvalidOperationException)
            {
                await _supplierQuotationUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _supplierQuotationUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<IList<QuotationLineDto>> GetQuotationLinesAsync(Guid supplierQuotationId)
        {
            var quotation = await _supplierQuotationUnitOfWork
                .SupplierQuotationRepository
                .GetSupplierQuotationByIdAsync(supplierQuotationId);

            if (quotation == null)
            {
                return new List<QuotationLineDto>();
            }

            return (quotation.SupplierQuotationItems ?? new List<SupplierQuotationItem>())
                .Select(x => new QuotationLineDto
                {
                    ProductId = x.ProductId,
                    ProductName = x.Product?.ProductName ?? string.Empty,
                    UnitName = x.Product?.Unit?.UnitName ?? string.Empty,
                    Quantity = x.Quantity,
                    UnitPrice = x.UnitPrice,
                    DiscountAmount = x.DiscountAmount
                })
                .ToList();
        }

        /// <summary>
        /// Everything create and update both need before any line can be built.
        /// </summary>
        private async Task<QuotationContext> ValidateAsync(SupplierQuotation supplierQuotation,
            SupplierQuotation? existing)
        {
            var request = await _supplierQuotationUnitOfWork
                .RequestForQuotationRepository
                .GetRequestForQuotationByIdAsync(supplierQuotation.RequestForQuotationId)
                ?? throw new InvalidOperationException("Request for quotation not found.");

            // A draft has not gone out and an awarded one is decided, so neither is
            // open for a supplier to quote against.
            if (request.Status is not (RequestForQuotationStatus.Sent or RequestForQuotationStatus.Closed))
            {
                throw new InvalidOperationException(
                    $"A quotation cannot be recorded against a {request.Status.ToString().ToLower()} request. " +
                    "It has to be sent out first.");
            }

            var supplier = await _supplierQuotationUnitOfWork
                .SupplierRepository
                .GetSupplierByIdAsync(supplierQuotation.SupplierId)
                ?? throw new InvalidOperationException("Supplier not found.");

            if (supplier.Status == SupplierStatus.Blacklisted)
            {
                throw new InvalidOperationException(
                    "A blacklisted supplier cannot be quoted from. Change the supplier status first.");
            }

            if (await _supplierQuotationUnitOfWork
                .SupplierQuotationRepository
                .HasSupplierQuotedAsync(request.Id, supplier.Id, existing?.Id))
            {
                throw new InvalidOperationException(
                    $"'{supplier.SupplierName}' has already quoted against this request. Edit that quotation instead.");
            }

            var quotationDate = supplierQuotation.QuotationDate == default
                ? DateTime.Now
                : supplierQuotation.QuotationDate;

            var validUntil = supplierQuotation.ValidUntil == default
                ? quotationDate.AddDays(30)
                : supplierQuotation.ValidUntil;

            if (validUntil.Date < quotationDate.Date)
            {
                throw new InvalidOperationException(
                    "The validity date cannot be earlier than the quotation date.");
            }

            if (supplierQuotation.DeliveryDays < 0)
            {
                throw new InvalidOperationException("The delivery days cannot be negative.");
            }

            if (supplierQuotation.OtherCharges < 0)
            {
                throw new InvalidOperationException("Other charges cannot be negative.");
            }

            Guid? paymentTermId = null;

            if (supplierQuotation.PaymentTermId.HasValue && supplierQuotation.PaymentTermId.Value != Guid.Empty)
            {
                _ = await _supplierQuotationUnitOfWork
                    .PaymentTermRepository
                    .GetByIdAsync(supplierQuotation.PaymentTermId.Value)
                    ?? throw new InvalidOperationException("Payment term not found.");

                paymentTermId = supplierQuotation.PaymentTermId;
            }

            var requestedProducts = (request.RequestForQuotationItems ?? new List<RequestForQuotationItem>())
                .ToDictionary(x => x.ProductId);

            var grouped = supplierQuotation.SupplierQuotationItems?
                .Where(x => x.ProductId != Guid.Empty && x.Quantity > 0)
                .GroupBy(x => x.ProductId)
                .Select(g => new RequestedLine(
                    g.Key,
                    g.Sum(x => x.Quantity),
                    g.First().UnitPrice,
                    g.Sum(x => x.DiscountAmount),
                    g.Select(x => x.Remarks).FirstOrDefault(x => !string.IsNullOrWhiteSpace(x)) ?? string.Empty))
                .ToList();

            if (grouped == null || grouped.Count == 0)
            {
                throw new InvalidOperationException("At least one quoted line is required.");
            }

            var validated = new List<ValidatedLine>();

            foreach (var quoted in grouped)
            {
                // Quoting for something that was never asked for would leave the offers
                // with nothing to be compared on.
                if (!requestedProducts.TryGetValue(quoted.ProductId, out var requested))
                {
                    throw new InvalidOperationException(
                        "A product that is not part of this request for quotation cannot be quoted.");
                }

                var product = requested.Product
                    ?? await _supplierQuotationUnitOfWork.ProductRepository.GetByIdAsync(quoted.ProductId)
                    ?? throw new InvalidOperationException("Product not found.");

                if (quoted.UnitPrice < 0)
                {
                    throw new InvalidOperationException(
                        $"The unit price of '{product.ProductName}' cannot be negative.");
                }

                var lineValue = quoted.Quantity * quoted.UnitPrice;

                if (quoted.DiscountAmount < 0 || quoted.DiscountAmount > lineValue)
                {
                    throw new InvalidOperationException(
                        $"The discount on '{product.ProductName}' has to be between 0 and {lineValue:N2}.");
                }

                validated.Add(new ValidatedLine(
                    product.Id,
                    quoted.Quantity,
                    quoted.UnitPrice,
                    quoted.DiscountAmount,
                    quoted.Remarks));
            }

            return new QuotationContext(request, supplier, validated, quotationDate, validUntil,
                supplierQuotation.DeliveryDays, paymentTermId, Math.Round(supplierQuotation.OtherCharges, 2));
        }

        /// <summary>
        /// Was an order ever raised from this quotation? It is what stops an award
        /// being undone after the buying has already started.
        /// </summary>
        private async Task<bool> HasPurchaseOrderAsync(Guid supplierQuotationId)
        {
            return await _supplierQuotationUnitOfWork
                .PurchaseOrderRepository
                .GetCountAsync(x => x.SupplierQuotationId == supplierQuotationId) > 0;
        }

        /// <summary>
        /// Flags the cheapest and the quickest offer, and the cheapest unit price of
        /// each product, so the comparison can point at them.
        /// </summary>
        private static void MarkBest(QuotationComparisonDto comparison)
        {
            // Only a complete offer competes on price: a supplier who quoted half the
            // list would otherwise always look like the cheapest.
            var comparable = comparison.Offers.Where(x => x.IsComplete && x.GrandTotal > 0).ToList();

            if (comparable.Count > 0)
            {
                var lowest = comparable.Min(x => x.GrandTotal);
                var fastest = comparable.Min(x => x.DeliveryDays);

                foreach (var offer in comparable)
                {
                    offer.IsLowestPrice = offer.GrandTotal == lowest;
                    offer.IsFastestDelivery = offer.DeliveryDays == fastest;
                }
            }

            foreach (var requested in comparison.RequestedLines)
            {
                var quoted = comparison.Offers
                    .SelectMany(x => x.Lines)
                    .Where(x => x.ProductId == requested.ProductId && x.UnitPrice > 0)
                    .ToList();

                if (quoted.Count == 0)
                {
                    continue;
                }

                var lowest = quoted.Min(x => x.UnitPrice);

                foreach (var line in quoted.Where(x => x.UnitPrice == lowest))
                {
                    line.IsLowestUnitPrice = true;
                }
            }
        }

        private static void ApplyTotals(SupplierQuotation quotation, IList<SupplierQuotationItem> lines)
        {
            quotation.SubTotal = lines.Sum(x => x.Quantity * x.UnitPrice);
            quotation.DiscountTotal = lines.Sum(x => x.DiscountAmount);
            quotation.GrandTotal = quotation.SubTotal - quotation.DiscountTotal + quotation.OtherCharges;
        }

        private static List<SupplierQuotationItem> BuildLines(IList<ValidatedLine> lines, Guid supplierQuotationId)
        {
            return lines.Select(x => new SupplierQuotationItem
            {
                Id = Guid.NewGuid(),
                SupplierQuotationId = supplierQuotationId,
                ProductId = x.ProductId,
                Quantity = x.Quantity,
                UnitPrice = x.UnitPrice,
                DiscountAmount = x.DiscountAmount,
                LineTotal = Math.Round(x.Quantity * x.UnitPrice - x.DiscountAmount, 2),
                Remarks = x.Remarks
            }).ToList();
        }

        private sealed record RequestedLine(Guid ProductId, decimal Quantity, decimal UnitPrice,
            decimal DiscountAmount, string Remarks);

        private sealed record ValidatedLine(Guid ProductId, decimal Quantity, decimal UnitPrice,
            decimal DiscountAmount, string Remarks);

        private sealed record QuotationContext(
            RequestForQuotation RequestForQuotation,
            Supplier Supplier,
            IList<ValidatedLine> Lines,
            DateTime QuotationDate,
            DateTime ValidUntil,
            int DeliveryDays,
            Guid? PaymentTermId,
            decimal OtherCharges);
    }
}
