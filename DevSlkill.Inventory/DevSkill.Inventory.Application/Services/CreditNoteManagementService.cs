using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Domain.UnitOfWorkContracts;

namespace DevSkill.Inventory.Application.Services
{
    /// <summary>
    /// Credit owed back to customers. A note raised by a sales return belongs to that
    /// return and is not editable here; a price adjustment is raised by hand. Issuing
    /// a note takes the amount off what the customer owes, and spending it is done by
    /// a collection of method Adjustment that names it.
    /// </summary>
    public class CreditNoteManagementService : ICreditNoteManagementService
    {
        private readonly IInventoryUnitOfWork _creditNoteUnitOfWork;

        /// <summary>Only a claim already made on the customer can be adjusted down.</summary>
        private static readonly SalesInvoiceStatus[] CreditableInvoiceStatuses =
        {
            SalesInvoiceStatus.Posted,
            SalesInvoiceStatus.PartiallyPaid,
            SalesInvoiceStatus.Paid
        };

        public CreditNoteManagementService(IInventoryUnitOfWork unitOfWork)
        {
            _creditNoteUnitOfWork = unitOfWork;
        }

        public async Task<Guid> CreateCreditNoteAsync(CreditNote creditNote, bool issueImmediately)
        {
            await _creditNoteUnitOfWork.BeginTransactionAsync();

            try
            {
                var context = await ValidateAsync(creditNote, null);
                var creditNoteId = Guid.NewGuid();

                var items = BuildLines(context, creditNoteId);
                var totals = Summarise(items);

                var header = new CreditNote
                {
                    Id = creditNoteId,
                    CreditNoteNo = await GenerateCreditNoteNoAsync(),
                    CustomerId = context.Customer.Id,
                    SalesReturnId = null,
                    SalesInvoiceId = context.SalesInvoice?.Id,
                    CreditNoteDate = context.CreditNoteDate,
                    Reason = creditNote.Reason ?? string.Empty,
                    Notes = creditNote.Notes ?? string.Empty,
                    Status = CreditNoteStatus.Draft,
                    SubTotal = totals.SubTotal,
                    TotalTax = totals.TotalTax,
                    TotalAmount = totals.TotalAmount,
                    AppliedAmount = 0m,
                    CreatedBy = creditNote.CreatedBy ?? string.Empty,
                    Created = DateTime.Now,
                    Updated = DateTime.Now,
                    CreditNoteItems = items
                };

                await _creditNoteUnitOfWork.CreditNoteRepository.AddAsync(header);

                if (issueImmediately)
                {
                    await MoveOutstandingAsync(header.CustomerId, -header.TotalAmount);

                    header.Status = CreditNoteStatus.Issued;

                    await _creditNoteUnitOfWork.CreditNoteRepository.EditAsync(header);
                }

                await _creditNoteUnitOfWork.CommitTransactionAsync();

                return creditNoteId;
            }
            catch (InvalidOperationException)
            {
                await _creditNoteUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _creditNoteUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<bool> UpdateCreditNoteAsync(CreditNote creditNote)
        {
            await _creditNoteUnitOfWork.BeginTransactionAsync();

            try
            {
                var existing = await GetCreditNoteAsync(creditNote.Id);

                GuardAgainstReturnOwned(existing, "edited");

                // An issued credit is already sitting on the customer account and may
                // already have been spent, so it is cancelled rather than corrected.
                if (existing.Status != CreditNoteStatus.Draft)
                {
                    throw new InvalidOperationException(
                        $"Only a draft credit note can be edited, this one is {existing.Status.ToString().ToLower()}.");
                }

                // Who the credit belongs to is what the credit note is.
                creditNote.CustomerId = existing.CustomerId;

                var context = await ValidateAsync(creditNote, existing);
                var oldItems = existing.CreditNoteItems?.ToList() ?? new List<CreditNoteItem>();

                await _creditNoteUnitOfWork.CreditNoteItemRepository.RemoveRangeAsync(oldItems);

                var items = BuildLines(context, existing.Id);

                foreach (var item in items)
                {
                    await _creditNoteUnitOfWork.CreditNoteItemRepository.AddAsync(item);
                }

                var totals = Summarise(items);

                // The credit note number is issued once and never changes.
                existing.SalesInvoiceId = context.SalesInvoice?.Id;
                existing.CreditNoteDate = context.CreditNoteDate;
                existing.Reason = creditNote.Reason ?? string.Empty;
                existing.Notes = creditNote.Notes ?? string.Empty;
                existing.SubTotal = totals.SubTotal;
                existing.TotalTax = totals.TotalTax;
                existing.TotalAmount = totals.TotalAmount;
                existing.Updated = DateTime.Now;

                await _creditNoteUnitOfWork.CreditNoteRepository.EditAsync(existing);
                await _creditNoteUnitOfWork.CommitTransactionAsync();

                return true;
            }
            catch (InvalidOperationException)
            {
                await _creditNoteUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _creditNoteUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<bool> DeleteCreditNoteAsync(Guid id)
        {
            await _creditNoteUnitOfWork.BeginTransactionAsync();

            try
            {
                var creditNote = await GetCreditNoteAsync(id);

                GuardAgainstReturnOwned(creditNote, "deleted");

                if (creditNote.Status != CreditNoteStatus.Draft)
                {
                    throw new InvalidOperationException(
                        "Only a draft credit note can be deleted. Cancel the issued one instead, so the customer account is put right.");
                }

                var items = creditNote.CreditNoteItems?.ToList() ?? new List<CreditNoteItem>();

                await _creditNoteUnitOfWork.CreditNoteItemRepository.RemoveRangeAsync(items);
                await _creditNoteUnitOfWork.CreditNoteRepository.RemoveAsync(creditNote);
                await _creditNoteUnitOfWork.CommitTransactionAsync();

                return true;
            }
            catch (InvalidOperationException)
            {
                await _creditNoteUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _creditNoteUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<bool> IssueCreditNoteAsync(Guid id)
        {
            await _creditNoteUnitOfWork.BeginTransactionAsync();

            try
            {
                var creditNote = await GetCreditNoteAsync(id);

                if (creditNote.Status != CreditNoteStatus.Draft)
                {
                    throw new InvalidOperationException(
                        $"Only a draft credit note can be issued, this one is {creditNote.Status.ToString().ToLower()}.");
                }

                if (creditNote.TotalAmount <= 0)
                {
                    throw new InvalidOperationException("A credit note worth nothing cannot be issued.");
                }

                await MoveOutstandingAsync(creditNote.CustomerId, -creditNote.TotalAmount);

                creditNote.Status = CreditNoteStatus.Issued;
                creditNote.Updated = DateTime.Now;

                await _creditNoteUnitOfWork.CreditNoteRepository.EditAsync(creditNote);
                await _creditNoteUnitOfWork.CommitTransactionAsync();

                return true;
            }
            catch (InvalidOperationException)
            {
                await _creditNoteUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _creditNoteUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<bool> CancelCreditNoteAsync(Guid id)
        {
            await _creditNoteUnitOfWork.BeginTransactionAsync();

            try
            {
                var creditNote = await GetCreditNoteAsync(id);

                if (creditNote.Status == CreditNoteStatus.Cancelled)
                {
                    throw new InvalidOperationException("This credit note is already cancelled.");
                }

                GuardAgainstReturnOwned(creditNote, "cancelled on its own");

                if (creditNote.AppliedAmount > 0)
                {
                    throw new InvalidOperationException(
                        "This credit has already been spent, so it cannot be withdrawn. Cancel the collection that used it first.");
                }

                // A draft never reached the customer account, so there is nothing to undo.
                if (creditNote.Status != CreditNoteStatus.Draft)
                {
                    await MoveOutstandingAsync(creditNote.CustomerId, creditNote.TotalAmount);
                }

                creditNote.Status = CreditNoteStatus.Cancelled;
                creditNote.Updated = DateTime.Now;

                await _creditNoteUnitOfWork.CreditNoteRepository.EditAsync(creditNote);
                await _creditNoteUnitOfWork.CommitTransactionAsync();

                return true;
            }
            catch (InvalidOperationException)
            {
                await _creditNoteUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _creditNoteUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<CreditNote?> GetCreditNoteByIdAsync(Guid id)
        {
            return await _creditNoteUnitOfWork.CreditNoteRepository.GetCreditNoteByIdAsync(id);
        }

        public async Task<(IList<CreditNote> data, int total, int totalDisplay)> GetCreditNotesAsync(int pageIndex,
            int pageSize, DataTablesSearch search, string? order)
        {
            return await _creditNoteUnitOfWork
                .CreditNoteRepository
                .GetPagedCreditNotesAsync(pageIndex, pageSize, search, order);
        }

        public async Task<IList<CreditNote>> GetCreditNotesByStatusAsync(params CreditNoteStatus[] statuses)
        {
            return await _creditNoteUnitOfWork.CreditNoteRepository.GetCreditNotesByStatusAsync(statuses);
        }

        public async Task<string> GenerateCreditNoteNoAsync()
        {
            var count = await _creditNoteUnitOfWork.CreditNoteRepository.GetCountAsync();

            string creditNoteNo;
            do
            {
                count++;
                creditNoteNo = $"CN-{DateTime.Now:yyyyMM}-{count:D4}";
            }
            while (await _creditNoteUnitOfWork.CreditNoteRepository.IsCreditNoteNoDuplicateAsync(creditNoteNo));

            return creditNoteNo;
        }

        public async Task<IList<SalesInvoice>> GetCreditableSalesInvoicesAsync(Guid customerId)
        {
            var invoices = await _creditNoteUnitOfWork
                .SalesInvoiceRepository
                .GetSalesInvoicesByCustomerAsync(customerId);

            return invoices
                .Where(x => CreditableInvoiceStatuses.Contains(x.Status))
                .OrderByDescending(x => x.InvoiceDate)
                .ToList();
        }

        public async Task RefreshAppliedAmountAsync(Guid creditNoteId)
        {
            var creditNote = await _creditNoteUnitOfWork.CreditNoteRepository.GetByIdAsync(creditNoteId);

            if (creditNote == null)
            {
                return;
            }

            var payments = await _creditNoteUnitOfWork
                .CustomerPaymentRepository
                .GetCustomerPaymentsByCreditNoteAsync(creditNoteId);

            var spent = Round(payments
                .Where(x => x.Status != CustomerPaymentStatus.Cancelled)
                .Sum(x => x.Amount));

            creditNote.AppliedAmount = spent;

            // The state follows from the numbers, so a note can never read as fully
            // applied while it still has credit left to spend.
            if (creditNote.Status != CreditNoteStatus.Cancelled && creditNote.Status != CreditNoteStatus.Draft)
            {
                creditNote.Status = spent >= creditNote.TotalAmount && creditNote.TotalAmount > 0
                    ? CreditNoteStatus.Applied
                    : CreditNoteStatus.Issued;
            }

            creditNote.Updated = DateTime.Now;

            await _creditNoteUnitOfWork.CreditNoteRepository.EditAsync(creditNote);
            await _creditNoteUnitOfWork.SaveAsync();
        }

        /// <summary>
        /// Reads the request and checks it against the customer and, when the credit
        /// corrects an invoice, against that invoice too.
        /// </summary>
        private async Task<CreditNoteContext> ValidateAsync(CreditNote creditNote, CreditNote? existing)
        {
            if (creditNote.CustomerId == Guid.Empty)
            {
                throw new InvalidOperationException("A credit note has to name the customer it belongs to.");
            }

            var customer = await _creditNoteUnitOfWork
                .CustomerRepository
                .GetByIdAsync(creditNote.CustomerId)
                ?? throw new InvalidOperationException("Customer not found.");

            SalesInvoice? invoice = null;

            if (creditNote.SalesInvoiceId.HasValue && creditNote.SalesInvoiceId.Value != Guid.Empty)
            {
                invoice = await _creditNoteUnitOfWork
                    .SalesInvoiceRepository
                    .GetSalesInvoiceByIdAsync(creditNote.SalesInvoiceId.Value)
                    ?? throw new InvalidOperationException("Sales invoice not found.");

                if (invoice.CustomerId != customer.Id)
                {
                    throw new InvalidOperationException("That invoice belongs to another customer.");
                }

                if (!CreditableInvoiceStatuses.Contains(invoice.Status))
                {
                    throw new InvalidOperationException(
                        $"'{invoice.InvoiceNo}' is {invoice.Status.ToString().ToLower()} and cannot be credited.");
                }
            }

            var creditNoteDate = creditNote.CreditNoteDate == default
                ? DateTime.Now.Date
                : creditNote.CreditNoteDate.Date;

            var requested = (creditNote.CreditNoteItems ?? new List<CreditNoteItem>())
                .Where(x => x.LineTotal > 0 || (x.Quantity > 0 && x.UnitPrice > 0))
                .ToList();

            if (requested.Count == 0)
            {
                throw new InvalidOperationException("A credit note needs at least one line.");
            }

            var validated = new List<ValidatedLine>();

            foreach (var line in requested)
            {
                var description = line.Description?.Trim() ?? string.Empty;
                Guid? productId = null;

                // A line may credit a product or nothing at all; a pure price adjustment
                // carries only a description and an amount.
                if (line.ProductId.HasValue && line.ProductId.Value != Guid.Empty)
                {
                    var product = await _creditNoteUnitOfWork
                        .ProductRepository
                        .GetProductByIdAsync(line.ProductId.Value)
                        ?? throw new InvalidOperationException("Product not found.");

                    productId = product.Id;

                    if (string.IsNullOrWhiteSpace(description))
                    {
                        description = product.ProductName;
                    }
                }

                if (string.IsNullOrWhiteSpace(description))
                {
                    throw new InvalidOperationException("A credit note line has to say what is being credited.");
                }

                var quantity = line.Quantity > 0 ? Round(line.Quantity) : 1m;
                var unitPrice = Round(Math.Max(0m, line.UnitPrice));
                var taxRate = Round(Math.Max(0m, line.TaxRate));

                if (unitPrice <= 0)
                {
                    throw new InvalidOperationException($"'{description}' has to be credited for more than zero.");
                }

                validated.Add(new ValidatedLine(productId, description, quantity, unitPrice, taxRate));
            }

            // The credit can never come to more than the invoice it corrects, otherwise
            // the customer would be owed money they were never charged.
            if (invoice != null)
            {
                var creditTotal = Round(validated.Sum(x =>
                {
                    var amount = x.Quantity * x.UnitPrice;
                    return amount + amount * x.TaxRate / 100m;
                }));

                var alreadyCredited = await GetCreditedAgainstInvoiceAsync(invoice.Id, existing?.Id);
                var room = Round(invoice.GrandTotal - alreadyCredited);

                if (creditTotal > room)
                {
                    throw new InvalidOperationException(
                        $"'{invoice.InvoiceNo}' only has {room:0.##} left that may be credited.");
                }
            }

            return new CreditNoteContext(customer, invoice, creditNoteDate, validated);
        }

        private static IList<CreditNoteItem> BuildLines(CreditNoteContext context, Guid creditNoteId)
        {
            return context.Lines
                .Select(line =>
                {
                    var lineAmount = Round(line.Quantity * line.UnitPrice);
                    var taxAmount = Round(lineAmount * line.TaxRate / 100m);

                    return new CreditNoteItem
                    {
                        Id = Guid.NewGuid(),
                        CreditNoteId = creditNoteId,
                        ProductId = line.ProductId,
                        Description = line.Description,
                        Quantity = line.Quantity,
                        UnitPrice = line.UnitPrice,
                        TaxRate = line.TaxRate,
                        TaxAmount = taxAmount,
                        LineTotal = Round(lineAmount + taxAmount)
                    };
                })
                .ToList();
        }

        private static Totals Summarise(IList<CreditNoteItem> items)
        {
            var subTotal = Round(items.Sum(x => x.Quantity * x.UnitPrice));
            var totalTax = Round(items.Sum(x => x.TaxAmount));

            return new Totals(subTotal, totalTax, Round(subTotal + totalTax));
        }

        /// <summary>
        /// What one invoice has already been credited by other notes, so a second
        /// adjustment cannot hand back more than was ever charged.
        /// </summary>
        private async Task<decimal> GetCreditedAgainstInvoiceAsync(Guid salesInvoiceId, Guid? excludeCreditNoteId)
        {
            var invoice = await _creditNoteUnitOfWork
                .SalesInvoiceRepository
                .GetSalesInvoiceByIdAsync(salesInvoiceId);

            if (invoice == null)
            {
                return 0m;
            }

            var creditNotes = await _creditNoteUnitOfWork
                .CreditNoteRepository
                .GetCreditNotesByCustomerAsync(invoice.CustomerId);

            return Round(creditNotes
                .Where(x => x.SalesInvoiceId == salesInvoiceId)
                .Where(x => x.Status != CreditNoteStatus.Cancelled)
                .Where(x => !excludeCreditNoteId.HasValue || x.Id != excludeCreditNoteId.Value)
                .Sum(x => x.TotalAmount));
        }

        /// <summary>
        /// A note raised by a return belongs to that return. Editing it here would let
        /// the credit and the goods that justify it drift apart.
        /// </summary>
        private static void GuardAgainstReturnOwned(CreditNote creditNote, string action)
        {
            if (creditNote.SalesReturnId.HasValue && creditNote.SalesReturnId.Value != Guid.Empty)
            {
                throw new InvalidOperationException(
                    $"This credit note was raised by a sales return, so it cannot be {action}. Work on the return instead.");
            }
        }

        private async Task MoveOutstandingAsync(Guid customerId, decimal amount)
        {
            var customer = await _creditNoteUnitOfWork.CustomerRepository.GetByIdAsync(customerId);

            if (customer == null)
            {
                return;
            }

            customer.CurrentOutstanding = Round(customer.CurrentOutstanding + amount);
            customer.Updated = DateTime.Now;

            await _creditNoteUnitOfWork.CustomerRepository.EditAsync(customer);
        }

        private async Task<CreditNote> GetCreditNoteAsync(Guid id)
        {
            return await _creditNoteUnitOfWork
                .CreditNoteRepository
                .GetCreditNoteByIdAsync(id)
                ?? throw new InvalidOperationException("Credit note not found.");
        }

        private static decimal Round(decimal value)
        {
            return Math.Round(value, 2, MidpointRounding.AwayFromZero);
        }

        private sealed record ValidatedLine(Guid? ProductId, string Description, decimal Quantity, decimal UnitPrice,
            decimal TaxRate);

        private sealed record Totals(decimal SubTotal, decimal TotalTax, decimal TotalAmount);

        private sealed record CreditNoteContext(Customer Customer, SalesInvoice? SalesInvoice, DateTime CreditNoteDate,
            IList<ValidatedLine> Lines);
    }
}
