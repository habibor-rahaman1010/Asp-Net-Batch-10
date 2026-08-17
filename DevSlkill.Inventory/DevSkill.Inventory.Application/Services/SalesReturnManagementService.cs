using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Domain.UnitOfWorkContracts;

namespace DevSkill.Inventory.Application.Services
{
    /// <summary>
    /// Goods coming back from customers. A draft return brings nothing back;
    /// confirming it puts the stock in and raises the credit note that pays for it,
    /// both in the same transaction so the two can never disagree.
    /// </summary>
    public class SalesReturnManagementService : ISalesReturnManagementService
    {
        private readonly IInventoryUnitOfWork _salesReturnUnitOfWork;

        /// <summary>Only goods that have actually been billed can come back.</summary>
        private static readonly SalesInvoiceStatus[] ReturnableInvoiceStatuses =
        {
            SalesInvoiceStatus.Posted,
            SalesInvoiceStatus.PartiallyPaid,
            SalesInvoiceStatus.Paid
        };

        public SalesReturnManagementService(IInventoryUnitOfWork unitOfWork)
        {
            _salesReturnUnitOfWork = unitOfWork;
        }

        public async Task<Guid> CreateSalesReturnAsync(SalesReturn salesReturn, bool confirmImmediately)
        {
            await _salesReturnUnitOfWork.BeginTransactionAsync();

            try
            {
                var context = await ValidateAsync(salesReturn, null);
                var returnId = Guid.NewGuid();

                var lines = BuildLines(context, returnId);

                var header = new SalesReturn
                {
                    Id = returnId,
                    ReturnNo = await GenerateReturnNoAsync(),
                    SalesInvoiceId = context.SalesInvoice.Id,
                    CustomerId = context.SalesInvoice.CustomerId,
                    BusinessLocationId = context.SalesInvoice.BusinessLocationId,
                    ReturnDate = context.ReturnDate,
                    Reason = salesReturn.Reason ?? string.Empty,
                    Notes = salesReturn.Notes ?? string.Empty,
                    Status = SalesReturnStatus.Draft,
                    TotalAmount = Round(lines.Sum(x => x.LineTotal)),
                    CreatedBy = salesReturn.CreatedBy ?? string.Empty,
                    Created = DateTime.Now,
                    Updated = DateTime.Now,
                    SalesReturnItems = lines
                };

                await _salesReturnUnitOfWork.SalesReturnRepository.AddAsync(header);

                if (confirmImmediately)
                {
                    await ApplyReturnAsync(header, context.SalesInvoice);

                    header.Status = SalesReturnStatus.Returned;

                    await _salesReturnUnitOfWork.SalesReturnRepository.EditAsync(header);
                }

                await _salesReturnUnitOfWork.CommitTransactionAsync();

                return returnId;
            }
            catch (InvalidOperationException)
            {
                await _salesReturnUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _salesReturnUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<bool> UpdateSalesReturnAsync(SalesReturn salesReturn)
        {
            await _salesReturnUnitOfWork.BeginTransactionAsync();

            try
            {
                var existing = await GetReturnAsync(salesReturn.Id);

                // Once the goods are back on the shelf and credited, an edit would move
                // stock behind the user's back. Cancel it and raise a new one instead.
                if (existing.Status != SalesReturnStatus.Draft)
                {
                    throw new InvalidOperationException(
                        $"Only a draft return can be edited, this one is {existing.Status.ToString().ToLower()}.");
                }

                // The invoice a return came off is what the return is.
                salesReturn.SalesInvoiceId = existing.SalesInvoiceId;

                var context = await ValidateAsync(salesReturn, existing);
                var oldLines = existing.SalesReturnItems?.ToList() ?? new List<SalesReturnItem>();

                await _salesReturnUnitOfWork.SalesReturnItemRepository.RemoveRangeAsync(oldLines);

                var lines = BuildLines(context, existing.Id);

                foreach (var line in lines)
                {
                    await _salesReturnUnitOfWork.SalesReturnItemRepository.AddAsync(line);
                }

                // The return number is issued once and never changes.
                existing.ReturnDate = context.ReturnDate;
                existing.Reason = salesReturn.Reason ?? string.Empty;
                existing.Notes = salesReturn.Notes ?? string.Empty;
                existing.TotalAmount = Round(lines.Sum(x => x.LineTotal));
                existing.Updated = DateTime.Now;

                await _salesReturnUnitOfWork.SalesReturnRepository.EditAsync(existing);
                await _salesReturnUnitOfWork.CommitTransactionAsync();

                return true;
            }
            catch (InvalidOperationException)
            {
                await _salesReturnUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _salesReturnUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<bool> DeleteSalesReturnAsync(Guid id)
        {
            await _salesReturnUnitOfWork.BeginTransactionAsync();

            try
            {
                var salesReturn = await GetReturnAsync(id);

                if (salesReturn.Status != SalesReturnStatus.Draft)
                {
                    throw new InvalidOperationException(
                        "Only a draft return can be deleted. Cancel the confirmed one instead, so the stock and the credit are put right.");
                }

                var lines = salesReturn.SalesReturnItems?.ToList() ?? new List<SalesReturnItem>();

                await _salesReturnUnitOfWork.SalesReturnItemRepository.RemoveRangeAsync(lines);
                await _salesReturnUnitOfWork.SalesReturnRepository.RemoveAsync(salesReturn);
                await _salesReturnUnitOfWork.CommitTransactionAsync();

                return true;
            }
            catch (InvalidOperationException)
            {
                await _salesReturnUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _salesReturnUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<bool> ConfirmSalesReturnAsync(Guid id)
        {
            await _salesReturnUnitOfWork.BeginTransactionAsync();

            try
            {
                var salesReturn = await GetReturnAsync(id);

                if (salesReturn.Status != SalesReturnStatus.Draft)
                {
                    throw new InvalidOperationException(
                        $"Only a draft return can be confirmed, this one is {salesReturn.Status.ToString().ToLower()}.");
                }

                if (salesReturn.SalesReturnItems == null || salesReturn.SalesReturnItems.Count == 0)
                {
                    throw new InvalidOperationException("A return without any product line cannot be confirmed.");
                }

                var invoice = await _salesReturnUnitOfWork
                    .SalesInvoiceRepository
                    .GetSalesInvoiceByIdAsync(salesReturn.SalesInvoiceId)
                    ?? throw new InvalidOperationException("Sales invoice not found.");

                await ApplyReturnAsync(salesReturn, invoice);

                salesReturn.Status = SalesReturnStatus.Returned;
                salesReturn.Updated = DateTime.Now;

                await _salesReturnUnitOfWork.SalesReturnRepository.EditAsync(salesReturn);
                await _salesReturnUnitOfWork.CommitTransactionAsync();

                return true;
            }
            catch (InvalidOperationException)
            {
                await _salesReturnUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _salesReturnUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<bool> CancelSalesReturnAsync(Guid id)
        {
            await _salesReturnUnitOfWork.BeginTransactionAsync();

            try
            {
                var salesReturn = await GetReturnAsync(id);

                if (salesReturn.Status == SalesReturnStatus.Cancelled)
                {
                    throw new InvalidOperationException("This return is already cancelled.");
                }

                // A draft never moved stock nor raised a credit, so there is nothing to undo.
                if (salesReturn.Status == SalesReturnStatus.Returned)
                {
                    await ReverseReturnAsync(salesReturn);
                }

                salesReturn.Status = SalesReturnStatus.Cancelled;
                salesReturn.Updated = DateTime.Now;

                await _salesReturnUnitOfWork.SalesReturnRepository.EditAsync(salesReturn);
                await _salesReturnUnitOfWork.CommitTransactionAsync();

                return true;
            }
            catch (InvalidOperationException)
            {
                await _salesReturnUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _salesReturnUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<SalesReturn?> GetSalesReturnByIdAsync(Guid id)
        {
            return await _salesReturnUnitOfWork.SalesReturnRepository.GetSalesReturnByIdAsync(id);
        }

        public async Task<(IList<SalesReturn> data, int total, int totalDisplay)> GetSalesReturnsAsync(int pageIndex,
            int pageSize, DataTablesSearch search, string? order)
        {
            return await _salesReturnUnitOfWork
                .SalesReturnRepository
                .GetPagedSalesReturnsAsync(pageIndex, pageSize, search, order);
        }

        public async Task<string> GenerateReturnNoAsync()
        {
            var count = await _salesReturnUnitOfWork.SalesReturnRepository.GetCountAsync();

            string returnNo;
            do
            {
                count++;
                returnNo = $"SRT-{DateTime.Now:yyyyMM}-{count:D4}";
            }
            while (await _salesReturnUnitOfWork.SalesReturnRepository.IsReturnNoDuplicateAsync(returnNo));

            return returnNo;
        }

        public async Task<IList<ReturnableSalesLineDto>> GetReturnableLinesAsync(Guid salesInvoiceId,
            Guid? excludeReturnId = null)
        {
            var invoice = await _salesReturnUnitOfWork
                .SalesInvoiceRepository
                .GetSalesInvoiceByIdAsync(salesInvoiceId);

            if (invoice?.SalesInvoiceItems == null)
            {
                return new List<ReturnableSalesLineDto>();
            }

            var returned = await GetReturnedQuantitiesAsync(salesInvoiceId, excludeReturnId);
            var lines = new List<ReturnableSalesLineDto>();

            foreach (var item in invoice.SalesInvoiceItems)
            {
                returned.TryGetValue(item.ProductId, out var totals);

                lines.Add(new ReturnableSalesLineDto
                {
                    ProductId = item.ProductId,
                    ProductName = item.Product?.ProductName ?? string.Empty,
                    UnitName = item.Product?.Unit?.UnitName ?? string.Empty,
                    InvoicedQuantity = item.Quantity,
                    ReturnedQuantity = totals.Returned,
                    PendingQuantity = totals.Pending,
                    RemainingQuantity = Math.Max(0m, Round(item.Quantity - totals.Returned - totals.Pending)),
                    UnitPrice = item.UnitPrice,
                    TaxRate = item.TaxRate
                });
            }

            return lines;
        }

        public async Task<IList<SalesInvoice>> GetReturnableSalesInvoicesAsync()
        {
            return await _salesReturnUnitOfWork
                .SalesInvoiceRepository
                .GetSalesInvoicesByStatusAsync(ReturnableInvoiceStatuses);
        }

        /// <summary>
        /// Reads the request and checks it against the invoice behind it. Prices come
        /// off the invoice, never from the browser, so the credit is worth what was billed.
        /// </summary>
        private async Task<ReturnContext> ValidateAsync(SalesReturn salesReturn, SalesReturn? existing)
        {
            if (salesReturn.SalesInvoiceId == Guid.Empty)
            {
                throw new InvalidOperationException("A return has to be raised against a sales invoice.");
            }

            var invoice = await _salesReturnUnitOfWork
                .SalesInvoiceRepository
                .GetSalesInvoiceByIdAsync(salesReturn.SalesInvoiceId)
                ?? throw new InvalidOperationException("Sales invoice not found.");

            if (!ReturnableInvoiceStatuses.Contains(invoice.Status))
            {
                throw new InvalidOperationException(
                    $"'{invoice.InvoiceNo}' is {invoice.Status.ToString().ToLower()}, so nothing can come back against it.");
            }

            var returnDate = salesReturn.ReturnDate == default
                ? DateTime.Now.Date
                : salesReturn.ReturnDate.Date;

            if (returnDate < invoice.InvoiceDate.Date)
            {
                throw new InvalidOperationException("Goods cannot come back before the day they were invoiced.");
            }

            var requested = (salesReturn.SalesReturnItems ?? new List<SalesReturnItem>())
                .Where(x => x.ProductId != Guid.Empty && x.ReturnQuantity > 0)
                .GroupBy(x => x.ProductId)
                .Select(x => new RequestedLine(
                    x.Key,
                    Round(x.Sum(l => l.ReturnQuantity)),
                    x.Select(l => l.Remarks).FirstOrDefault(r => !string.IsNullOrWhiteSpace(r)) ?? string.Empty))
                .ToList();

            if (requested.Count == 0)
            {
                throw new InvalidOperationException("A return needs at least one product line.");
            }

            var invoiceLines = (invoice.SalesInvoiceItems ?? new List<SalesInvoiceItem>())
                .GroupBy(x => x.ProductId)
                .ToDictionary(x => x.Key, x => x.First());

            var returned = await GetReturnedQuantitiesAsync(invoice.Id, existing?.Id);
            var validated = new List<ValidatedLine>();

            foreach (var line in requested)
            {
                if (!invoiceLines.TryGetValue(line.ProductId, out var invoiceLine))
                {
                    throw new InvalidOperationException(
                        "A product that is not on the invoice cannot be returned against it.");
                }

                var product = invoiceLine.Product ?? await _salesReturnUnitOfWork
                    .ProductRepository
                    .GetProductByIdAsync(line.ProductId)
                    ?? throw new InvalidOperationException("Product not found.");

                returned.TryGetValue(line.ProductId, out var totals);

                var remaining = Round(invoiceLine.Quantity - totals.Returned - totals.Pending);

                if (remaining <= 0)
                {
                    throw new InvalidOperationException(
                        $"'{product.ProductName}' has already come back in full on this invoice.");
                }

                if (line.Quantity > remaining)
                {
                    throw new InvalidOperationException(
                        $"'{product.ProductName}' has only {remaining:0.##} left that may come back.");
                }

                validated.Add(new ValidatedLine(line.ProductId, invoiceLine.Quantity, line.Quantity,
                    invoiceLine.UnitPrice, invoiceLine.TaxRate, line.Remarks));
            }

            return new ReturnContext(invoice, returnDate, validated);
        }

        private static IList<SalesReturnItem> BuildLines(ReturnContext context, Guid returnId)
        {
            return context.Lines
                .Select(line =>
                {
                    var lineAmount = Round(line.ReturnQuantity * line.UnitPrice);
                    var taxAmount = Round(lineAmount * line.TaxRate / 100m);

                    return new SalesReturnItem
                    {
                        Id = Guid.NewGuid(),
                        SalesReturnId = returnId,
                        ProductId = line.ProductId,
                        InvoicedQuantity = line.InvoicedQuantity,
                        ReturnQuantity = line.ReturnQuantity,
                        UnitPrice = line.UnitPrice,
                        TaxRate = line.TaxRate,
                        TaxAmount = taxAmount,
                        LineTotal = Round(lineAmount + taxAmount),
                        Remarks = line.Remarks
                    };
                })
                .ToList();
        }

        /// <summary>
        /// Everything confirming a return does: the goods go back on the shelf, the
        /// invoice records what came back and a credit note is raised for its value.
        /// </summary>
        private async Task ApplyReturnAsync(SalesReturn salesReturn, SalesInvoice invoice)
        {
            await MoveStockAsync(salesReturn, add: true);
            await MoveReturnedQuantitiesAsync(salesReturn, invoice, add: true);
            await RaiseCreditNoteAsync(salesReturn, invoice);
        }

        private async Task ReverseReturnAsync(SalesReturn salesReturn)
        {
            // The credit goes first: if it has already been spent, nothing else moves.
            await WithdrawCreditNoteAsync(salesReturn);

            var invoice = await _salesReturnUnitOfWork
                .SalesInvoiceRepository
                .GetSalesInvoiceByIdAsync(salesReturn.SalesInvoiceId);

            await MoveStockAsync(salesReturn, add: false);

            if (invoice != null)
            {
                await MoveReturnedQuantitiesAsync(salesReturn, invoice, add: false);
            }
        }

        private async Task MoveStockAsync(SalesReturn salesReturn, bool add)
        {
            foreach (var item in salesReturn.SalesReturnItems ?? new List<SalesReturnItem>())
            {
                var quantity = (int)Math.Ceiling(item.ReturnQuantity);

                if (quantity <= 0)
                {
                    continue;
                }

                var product = await _salesReturnUnitOfWork.ProductRepository.GetByIdAsync(item.ProductId);

                if (product == null)
                {
                    continue;
                }

                if (add)
                {
                    product.CurrentStock += quantity;
                }
                else
                {
                    if (product.CurrentStock < quantity)
                    {
                        throw new InvalidOperationException(
                            $"'{product.ProductName}' only has {product.CurrentStock} in stock, so this return cannot be undone.");
                    }

                    product.CurrentStock -= quantity;
                }

                await _salesReturnUnitOfWork.ProductRepository.EditAsync(product);
            }
        }

        private async Task MoveReturnedQuantitiesAsync(SalesReturn salesReturn, SalesInvoice invoice, bool add)
        {
            var invoiceLines = (invoice.SalesInvoiceItems ?? new List<SalesInvoiceItem>())
                .GroupBy(x => x.ProductId)
                .ToDictionary(x => x.Key, x => x.First());

            foreach (var item in salesReturn.SalesReturnItems ?? new List<SalesReturnItem>())
            {
                if (!invoiceLines.TryGetValue(item.ProductId, out var invoiceLine))
                {
                    continue;
                }

                var moved = add ? item.ReturnQuantity : -item.ReturnQuantity;

                invoiceLine.ReturnedQuantity = Math.Max(0m, Round(invoiceLine.ReturnedQuantity + moved));

                await _salesReturnUnitOfWork.SalesInvoiceItemRepository.EditAsync(invoiceLine);
            }
        }

        /// <summary>
        /// Raises the credit the return is worth. It is written here rather than by
        /// hand so a return can never sit on the books without the customer being
        /// credited for it.
        /// </summary>
        private async Task RaiseCreditNoteAsync(SalesReturn salesReturn, SalesInvoice invoice)
        {
            var existing = await _salesReturnUnitOfWork
                .CreditNoteRepository
                .GetCreditNoteBySalesReturnAsync(salesReturn.Id);

            if (existing != null && existing.Status != CreditNoteStatus.Cancelled)
            {
                return;
            }

            if (existing != null)
            {
                var oldItems = await _salesReturnUnitOfWork
                    .CreditNoteItemRepository
                    .GetItemsByCreditNoteAsync(existing.Id);

                await _salesReturnUnitOfWork.CreditNoteItemRepository.RemoveRangeAsync(oldItems);
                await _salesReturnUnitOfWork.CreditNoteRepository.RemoveAsync(existing);
            }

            var creditNoteId = Guid.NewGuid();

            var items = (salesReturn.SalesReturnItems ?? new List<SalesReturnItem>())
                .Select(item => new CreditNoteItem
                {
                    Id = Guid.NewGuid(),
                    CreditNoteId = creditNoteId,
                    ProductId = item.ProductId,
                    Description = item.Product?.ProductName ?? string.Empty,
                    Quantity = item.ReturnQuantity,
                    UnitPrice = item.UnitPrice,
                    TaxRate = item.TaxRate,
                    TaxAmount = item.TaxAmount,
                    LineTotal = item.LineTotal
                })
                .ToList();

            var subTotal = Round(items.Sum(x => x.Quantity * x.UnitPrice));
            var totalTax = Round(items.Sum(x => x.TaxAmount));

            await _salesReturnUnitOfWork.CreditNoteRepository.AddAsync(new CreditNote
            {
                Id = creditNoteId,
                CreditNoteNo = await GenerateCreditNoteNoAsync(),
                CustomerId = salesReturn.CustomerId,
                SalesReturnId = salesReturn.Id,
                SalesInvoiceId = invoice.Id,
                CreditNoteDate = salesReturn.ReturnDate,
                Reason = string.IsNullOrWhiteSpace(salesReturn.Reason)
                    ? $"Goods returned on {salesReturn.ReturnNo}."
                    : salesReturn.Reason,
                Notes = $"Raised automatically by {salesReturn.ReturnNo}.",
                Status = CreditNoteStatus.Issued,
                SubTotal = subTotal,
                TotalTax = totalTax,
                TotalAmount = Round(subTotal + totalTax),
                AppliedAmount = 0m,
                CreatedBy = salesReturn.CreatedBy,
                Created = DateTime.Now,
                Updated = DateTime.Now,
                CreditNoteItems = items
            });

            // Issuing the credit is what takes the money off the customer account; the
            // collection that later spends it only settles invoices.
            await MoveOutstandingAsync(salesReturn.CustomerId, -Round(subTotal + totalTax));
        }

        private async Task WithdrawCreditNoteAsync(SalesReturn salesReturn)
        {
            var creditNote = await _salesReturnUnitOfWork
                .CreditNoteRepository
                .GetCreditNoteBySalesReturnAsync(salesReturn.Id);

            if (creditNote == null || creditNote.Status == CreditNoteStatus.Cancelled)
            {
                return;
            }

            if (creditNote.AppliedAmount > 0)
            {
                throw new InvalidOperationException(
                    $"'{creditNote.CreditNoteNo}' has already been spent, so this return cannot be cancelled. Cancel the collection that used it first.");
            }

            creditNote.Status = CreditNoteStatus.Cancelled;
            creditNote.Notes = $"Withdrawn with {salesReturn.ReturnNo}.";
            creditNote.Updated = DateTime.Now;

            await _salesReturnUnitOfWork.CreditNoteRepository.EditAsync(creditNote);

            await MoveOutstandingAsync(salesReturn.CustomerId, creditNote.TotalAmount);
        }

        private async Task MoveOutstandingAsync(Guid customerId, decimal amount)
        {
            var customer = await _salesReturnUnitOfWork.CustomerRepository.GetByIdAsync(customerId);

            if (customer == null)
            {
                return;
            }

            customer.CurrentOutstanding = Round(customer.CurrentOutstanding + amount);
            customer.Updated = DateTime.Now;

            await _salesReturnUnitOfWork.CustomerRepository.EditAsync(customer);
        }

        /// <summary>
        /// Per product, what has already come back on one invoice: confirmed returns
        /// that moved stock, and drafts that have merely claimed the quantity.
        /// </summary>
        private async Task<IDictionary<Guid, (decimal Returned, decimal Pending)>> GetReturnedQuantitiesAsync(
            Guid salesInvoiceId, Guid? excludeReturnId)
        {
            var returns = await _salesReturnUnitOfWork
                .SalesReturnRepository
                .GetSalesReturnsBySalesInvoiceAsync(salesInvoiceId);

            var totals = new Dictionary<Guid, (decimal Returned, decimal Pending)>();

            foreach (var salesReturn in returns)
            {
                if (excludeReturnId.HasValue && salesReturn.Id == excludeReturnId.Value)
                {
                    continue;
                }

                if (salesReturn.Status == SalesReturnStatus.Cancelled)
                {
                    continue;
                }

                foreach (var item in salesReturn.SalesReturnItems ?? new List<SalesReturnItem>())
                {
                    totals.TryGetValue(item.ProductId, out var current);

                    totals[item.ProductId] = salesReturn.Status == SalesReturnStatus.Returned
                        ? (current.Returned + item.ReturnQuantity, current.Pending)
                        : (current.Returned, current.Pending + item.ReturnQuantity);
                }
            }

            return totals;
        }

        private async Task<string> GenerateCreditNoteNoAsync()
        {
            var count = await _salesReturnUnitOfWork.CreditNoteRepository.GetCountAsync();

            string creditNoteNo;
            do
            {
                count++;
                creditNoteNo = $"CN-{DateTime.Now:yyyyMM}-{count:D4}";
            }
            while (await _salesReturnUnitOfWork.CreditNoteRepository.IsCreditNoteNoDuplicateAsync(creditNoteNo));

            return creditNoteNo;
        }

        private async Task<SalesReturn> GetReturnAsync(Guid id)
        {
            return await _salesReturnUnitOfWork
                .SalesReturnRepository
                .GetSalesReturnByIdAsync(id)
                ?? throw new InvalidOperationException("Sales return not found.");
        }

        private static decimal Round(decimal value)
        {
            return Math.Round(value, 2, MidpointRounding.AwayFromZero);
        }

        private sealed record RequestedLine(Guid ProductId, decimal Quantity, string Remarks);

        private sealed record ValidatedLine(Guid ProductId, decimal InvoicedQuantity, decimal ReturnQuantity,
            decimal UnitPrice, decimal TaxRate, string Remarks);

        private sealed record ReturnContext(SalesInvoice SalesInvoice, DateTime ReturnDate, IList<ValidatedLine> Lines);
    }
}
