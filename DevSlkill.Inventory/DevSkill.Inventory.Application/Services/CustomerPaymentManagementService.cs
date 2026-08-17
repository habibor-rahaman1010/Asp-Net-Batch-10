using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Domain.UnitOfWorkContracts;

namespace DevSkill.Inventory.Application.Services
{
    /// <summary>
    /// Money coming in from customers. A draft settles nothing; receiving the
    /// collection is what clears the invoices it was allocated to and takes the
    /// amount off what the customer owes.
    /// </summary>
    public class CustomerPaymentManagementService : ICustomerPaymentManagementService
    {
        private readonly IInventoryUnitOfWork _customerPaymentUnitOfWork;

        /// <summary>Only an invoice that has actually been claimed can be settled.</summary>
        private static readonly SalesInvoiceStatus[] ReceivableInvoiceStatuses =
        {
            SalesInvoiceStatus.Posted,
            SalesInvoiceStatus.PartiallyPaid
        };

        public CustomerPaymentManagementService(IInventoryUnitOfWork unitOfWork)
        {
            _customerPaymentUnitOfWork = unitOfWork;
        }

        public async Task<Guid> CreateCustomerPaymentAsync(CustomerPayment customerPayment, bool receiveImmediately)
        {
            await _customerPaymentUnitOfWork.BeginTransactionAsync();

            try
            {
                var context = await ValidateAsync(customerPayment, null);
                var paymentId = Guid.NewGuid();

                var allocations = BuildAllocations(context, paymentId);

                var header = new CustomerPayment
                {
                    Id = paymentId,
                    PaymentNo = await GeneratePaymentNoAsync(),
                    CustomerId = context.Customer.Id,
                    PaymentDate = context.PaymentDate,
                    PaymentMethod = context.PaymentMethod,
                    ReferenceNo = customerPayment.ReferenceNo ?? string.Empty,
                    CreditNoteId = context.CreditNote?.Id,
                    Amount = context.Amount,
                    AllocatedAmount = Round(allocations.Sum(x => x.AllocatedAmount)),
                    Notes = customerPayment.Notes ?? string.Empty,
                    Status = CustomerPaymentStatus.Draft,
                    CreatedBy = customerPayment.CreatedBy ?? string.Empty,
                    Created = DateTime.Now,
                    Updated = DateTime.Now,
                    CustomerPaymentAllocations = allocations
                };

                await _customerPaymentUnitOfWork.CustomerPaymentRepository.AddAsync(header);

                if (receiveImmediately)
                {
                    await ApplyReceiptAsync(header);

                    header.Status = CustomerPaymentStatus.Received;

                    await _customerPaymentUnitOfWork.CustomerPaymentRepository.EditAsync(header);
                }

                await _customerPaymentUnitOfWork.CommitTransactionAsync();

                return paymentId;
            }
            catch (InvalidOperationException)
            {
                await _customerPaymentUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _customerPaymentUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<bool> UpdateCustomerPaymentAsync(CustomerPayment customerPayment)
        {
            await _customerPaymentUnitOfWork.BeginTransactionAsync();

            try
            {
                var existing = await GetPaymentAsync(customerPayment.Id);

                // Once the money is in, moving it around would silently re-settle
                // invoices. Cancel the collection and take it again instead.
                if (existing.Status != CustomerPaymentStatus.Draft)
                {
                    throw new InvalidOperationException(
                        $"Only a draft collection can be edited, this one is {existing.Status.ToString().ToLower()}.");
                }

                // Who paid is what the collection is; an edit may move the money about
                // but never hand it to a different customer.
                customerPayment.CustomerId = existing.CustomerId;

                var context = await ValidateAsync(customerPayment, existing);
                var oldAllocations = existing.CustomerPaymentAllocations?.ToList()
                    ?? new List<CustomerPaymentAllocation>();

                await _customerPaymentUnitOfWork
                    .CustomerPaymentAllocationRepository
                    .RemoveRangeAsync(oldAllocations);

                var allocations = BuildAllocations(context, existing.Id);

                foreach (var allocation in allocations)
                {
                    await _customerPaymentUnitOfWork.CustomerPaymentAllocationRepository.AddAsync(allocation);
                }

                // The collection number is issued once and never changes.
                existing.PaymentDate = context.PaymentDate;
                existing.PaymentMethod = context.PaymentMethod;
                existing.ReferenceNo = customerPayment.ReferenceNo ?? string.Empty;
                existing.CreditNoteId = context.CreditNote?.Id;
                existing.Amount = context.Amount;
                existing.AllocatedAmount = Round(allocations.Sum(x => x.AllocatedAmount));
                existing.Notes = customerPayment.Notes ?? string.Empty;
                existing.Updated = DateTime.Now;

                await _customerPaymentUnitOfWork.CustomerPaymentRepository.EditAsync(existing);
                await _customerPaymentUnitOfWork.CommitTransactionAsync();

                return true;
            }
            catch (InvalidOperationException)
            {
                await _customerPaymentUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _customerPaymentUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<bool> DeleteCustomerPaymentAsync(Guid id)
        {
            await _customerPaymentUnitOfWork.BeginTransactionAsync();

            try
            {
                var payment = await GetPaymentAsync(id);

                if (payment.Status != CustomerPaymentStatus.Draft)
                {
                    throw new InvalidOperationException(
                        "Only a draft collection can be deleted. Cancel the received one instead, so the invoices owe their money again.");
                }

                var allocations = payment.CustomerPaymentAllocations?.ToList()
                    ?? new List<CustomerPaymentAllocation>();

                await _customerPaymentUnitOfWork.CustomerPaymentAllocationRepository.RemoveRangeAsync(allocations);
                await _customerPaymentUnitOfWork.CustomerPaymentRepository.RemoveAsync(payment);
                await _customerPaymentUnitOfWork.CommitTransactionAsync();

                return true;
            }
            catch (InvalidOperationException)
            {
                await _customerPaymentUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _customerPaymentUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<bool> ReceiveCustomerPaymentAsync(Guid id)
        {
            await _customerPaymentUnitOfWork.BeginTransactionAsync();

            try
            {
                var payment = await GetPaymentAsync(id);

                if (payment.Status != CustomerPaymentStatus.Draft)
                {
                    throw new InvalidOperationException(
                        $"Only a draft collection can be received, this one is {payment.Status.ToString().ToLower()}.");
                }

                if (payment.Amount <= 0)
                {
                    throw new InvalidOperationException("A collection of nothing cannot be received.");
                }

                await ApplyReceiptAsync(payment);

                payment.Status = CustomerPaymentStatus.Received;
                payment.Updated = DateTime.Now;

                await _customerPaymentUnitOfWork.CustomerPaymentRepository.EditAsync(payment);
                await _customerPaymentUnitOfWork.CommitTransactionAsync();

                return true;
            }
            catch (InvalidOperationException)
            {
                await _customerPaymentUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _customerPaymentUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<bool> CancelCustomerPaymentAsync(Guid id)
        {
            await _customerPaymentUnitOfWork.BeginTransactionAsync();

            try
            {
                var payment = await GetPaymentAsync(id);

                if (payment.Status == CustomerPaymentStatus.Cancelled)
                {
                    throw new InvalidOperationException("This collection is already cancelled.");
                }

                // A draft never touched an invoice, so there is nothing to put back.
                if (payment.Status == CustomerPaymentStatus.Received)
                {
                    await ReverseReceiptAsync(payment);
                }

                payment.Status = CustomerPaymentStatus.Cancelled;
                payment.Updated = DateTime.Now;

                await _customerPaymentUnitOfWork.CustomerPaymentRepository.EditAsync(payment);
                await _customerPaymentUnitOfWork.CommitTransactionAsync();

                return true;
            }
            catch (InvalidOperationException)
            {
                await _customerPaymentUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _customerPaymentUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<CustomerPayment?> GetCustomerPaymentByIdAsync(Guid id)
        {
            return await _customerPaymentUnitOfWork.CustomerPaymentRepository.GetCustomerPaymentByIdAsync(id);
        }

        public async Task<(IList<CustomerPayment> data, int total, int totalDisplay)> GetCustomerPaymentsAsync(
            int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            return await _customerPaymentUnitOfWork
                .CustomerPaymentRepository
                .GetPagedCustomerPaymentsAsync(pageIndex, pageSize, search, order);
        }

        public async Task<string> GeneratePaymentNoAsync()
        {
            var count = await _customerPaymentUnitOfWork.CustomerPaymentRepository.GetCountAsync();

            string paymentNo;
            do
            {
                count++;
                paymentNo = $"CR-{DateTime.Now:yyyyMM}-{count:D4}";
            }
            while (await _customerPaymentUnitOfWork.CustomerPaymentRepository.IsPaymentNoDuplicateAsync(paymentNo));

            return paymentNo;
        }

        public async Task<IList<ReceivableInvoiceDto>> GetReceivableInvoicesAsync(Guid customerId,
            Guid? excludePaymentId = null)
        {
            var invoices = await _customerPaymentUnitOfWork
                .SalesInvoiceRepository
                .GetSalesInvoicesByCustomerAsync(customerId);

            var pending = await GetPendingAllocationsAsync(customerId, excludePaymentId);
            var today = DateTime.Now.Date;
            var receivables = new List<ReceivableInvoiceDto>();

            foreach (var invoice in invoices.Where(x => ReceivableInvoiceStatuses.Contains(x.Status)))
            {
                pending.TryGetValue(invoice.Id, out var pendingAmount);

                var remaining = Round(invoice.GrandTotal - invoice.PaidAmount - pendingAmount);

                if (remaining <= 0)
                {
                    continue;
                }

                receivables.Add(new ReceivableInvoiceDto
                {
                    SalesInvoiceId = invoice.Id,
                    InvoiceNo = invoice.InvoiceNo,
                    InvoiceDate = invoice.InvoiceDate,
                    DueDate = invoice.DueDate,
                    OverdueDays = (int)(today - invoice.DueDate.Date).TotalDays,
                    GrandTotal = invoice.GrandTotal,
                    PaidAmount = invoice.PaidAmount,
                    PendingAmount = pendingAmount,
                    RemainingAmount = remaining,
                    Status = invoice.Status.ToString()
                });
            }

            // Oldest debt first, which is the order a collection is normally applied in.
            return receivables
                .OrderBy(x => x.DueDate)
                .ThenBy(x => x.InvoiceDate)
                .ToList();
        }

        public async Task<IList<CreditNote>> GetSpendableCreditNotesAsync(Guid customerId)
        {
            var creditNotes = await _customerPaymentUnitOfWork
                .CreditNoteRepository
                .GetCreditNotesByCustomerAsync(customerId);

            return creditNotes
                .Where(x => x.Status == CreditNoteStatus.Issued && x.TotalAmount - x.AppliedAmount > 0)
                .OrderBy(x => x.CreditNoteDate)
                .ToList();
        }

        /// <summary>
        /// Reads the request and checks it against the customer's open invoices. The
        /// amounts are re-derived here; browser figures are never trusted.
        /// </summary>
        private async Task<PaymentContext> ValidateAsync(CustomerPayment customerPayment, CustomerPayment? existing)
        {
            if (customerPayment.CustomerId == Guid.Empty)
            {
                throw new InvalidOperationException("A collection has to name the customer it came from.");
            }

            var customer = await _customerPaymentUnitOfWork
                .CustomerRepository
                .GetByIdAsync(customerPayment.CustomerId)
                ?? throw new InvalidOperationException("Customer not found.");

            var paymentDate = customerPayment.PaymentDate == default
                ? DateTime.Now.Date
                : customerPayment.PaymentDate.Date;

            var amount = Round(customerPayment.Amount);

            if (amount <= 0)
            {
                throw new InvalidOperationException("A collection has to be for more than zero.");
            }

            var creditNote = await ResolveCreditNoteAsync(customerPayment, customer.Id, existing, amount);

            var requested = (customerPayment.CustomerPaymentAllocations ?? new List<CustomerPaymentAllocation>())
                .Where(x => x.SalesInvoiceId != Guid.Empty && x.AllocatedAmount > 0)
                .GroupBy(x => x.SalesInvoiceId)
                .Select(x => new RequestedAllocation(
                    x.Key,
                    Round(x.Sum(a => a.AllocatedAmount)),
                    x.Select(a => a.Remarks).FirstOrDefault(r => !string.IsNullOrWhiteSpace(r)) ?? string.Empty))
                .ToList();

            if (requested.Count == 0)
            {
                throw new InvalidOperationException("A collection needs at least one invoice to go against.");
            }

            var pending = await GetPendingAllocationsAsync(customer.Id, existing?.Id);
            var validated = new List<ValidatedAllocation>();

            foreach (var allocation in requested)
            {
                var invoice = await _customerPaymentUnitOfWork
                    .SalesInvoiceRepository
                    .GetSalesInvoiceByIdAsync(allocation.SalesInvoiceId)
                    ?? throw new InvalidOperationException("Sales invoice not found.");

                if (invoice.CustomerId != customer.Id)
                {
                    throw new InvalidOperationException(
                        $"'{invoice.InvoiceNo}' belongs to another customer and cannot be settled here.");
                }

                if (!ReceivableInvoiceStatuses.Contains(invoice.Status))
                {
                    throw new InvalidOperationException(
                        $"'{invoice.InvoiceNo}' is {invoice.Status.ToString().ToLower()} and cannot take a collection.");
                }

                pending.TryGetValue(invoice.Id, out var pendingAmount);

                var remaining = Round(invoice.GrandTotal - invoice.PaidAmount - pendingAmount);

                if (remaining <= 0)
                {
                    throw new InvalidOperationException($"'{invoice.InvoiceNo}' has nothing left owing.");
                }

                if (allocation.Amount > remaining)
                {
                    throw new InvalidOperationException(
                        $"'{invoice.InvoiceNo}' only has {remaining:0.##} left owing, {allocation.Amount:0.##} cannot go against it.");
                }

                validated.Add(new ValidatedAllocation(invoice.Id, remaining, allocation.Amount, allocation.Remarks));
            }

            // Allocating more than came in would settle money that was never received.
            // Less is fine: the rest simply sits as an advance.
            var allocatedTotal = Round(validated.Sum(x => x.AllocatedAmount));

            if (allocatedTotal > amount)
            {
                throw new InvalidOperationException(
                    $"The allocations come to {allocatedTotal:0.##} but only {amount:0.##} was collected.");
            }

            return new PaymentContext(customer, creditNote, paymentDate, customerPayment.PaymentMethod, amount,
                validated);
        }

        /// <summary>
        /// A collection may be funded by a credit note instead of money. That is what
        /// spends the credit, so it is checked against what the note still has left.
        /// </summary>
        private async Task<CreditNote?> ResolveCreditNoteAsync(CustomerPayment customerPayment, Guid customerId,
            CustomerPayment? existing, decimal amount)
        {
            if (!customerPayment.CreditNoteId.HasValue || customerPayment.CreditNoteId.Value == Guid.Empty)
            {
                if (customerPayment.PaymentMethod == PaymentMethod.Adjustment)
                {
                    throw new InvalidOperationException(
                        "An adjustment collection has to name the credit note that funds it.");
                }

                return null;
            }

            var creditNote = await _customerPaymentUnitOfWork
                .CreditNoteRepository
                .GetCreditNoteByIdAsync(customerPayment.CreditNoteId.Value)
                ?? throw new InvalidOperationException("Credit note not found.");

            if (creditNote.CustomerId != customerId)
            {
                throw new InvalidOperationException("That credit note belongs to another customer.");
            }

            if (creditNote.Status != CreditNoteStatus.Issued)
            {
                throw new InvalidOperationException(
                    $"'{creditNote.CreditNoteNo}' is {creditNote.Status.ToString().ToLower()} and cannot be spent.");
            }

            var spentElsewhere = await GetCreditSpentAsync(creditNote.Id, existing?.Id);
            var remaining = Round(creditNote.TotalAmount - spentElsewhere);

            if (amount > remaining)
            {
                throw new InvalidOperationException(
                    $"'{creditNote.CreditNoteNo}' only has {remaining:0.##} left to spend.");
            }

            return creditNote;
        }

        private static IList<CustomerPaymentAllocation> BuildAllocations(PaymentContext context, Guid paymentId)
        {
            return context.Allocations
                .Select(allocation => new CustomerPaymentAllocation
                {
                    Id = Guid.NewGuid(),
                    CustomerPaymentId = paymentId,
                    SalesInvoiceId = allocation.SalesInvoiceId,
                    DueAmount = allocation.DueAmount,
                    AllocatedAmount = allocation.AllocatedAmount,
                    Remarks = allocation.Remarks
                })
                .ToList();
        }

        /// <summary>
        /// Everything receiving a collection does: the invoices are settled by what was
        /// put on them, the customer owes that much less and any credit behind it is spent.
        /// </summary>
        private async Task ApplyReceiptAsync(CustomerPayment payment)
        {
            await MoveAllocationsAsync(payment, add: true);
            await MoveOutstandingAsync(payment, -payment.Amount);
            await RefreshCreditNoteAsync(payment.CreditNoteId);
        }

        private async Task ReverseReceiptAsync(CustomerPayment payment)
        {
            await MoveAllocationsAsync(payment, add: false);
            await MoveOutstandingAsync(payment, payment.Amount);
            await RefreshCreditNoteAsync(payment.CreditNoteId);
        }

        private async Task MoveAllocationsAsync(CustomerPayment payment, bool add)
        {
            foreach (var allocation in payment.CustomerPaymentAllocations ?? new List<CustomerPaymentAllocation>())
            {
                var invoice = await _customerPaymentUnitOfWork
                    .SalesInvoiceRepository
                    .GetByIdAsync(allocation.SalesInvoiceId);

                if (invoice == null || invoice.Status == SalesInvoiceStatus.Cancelled)
                {
                    continue;
                }

                var moved = add ? allocation.AllocatedAmount : -allocation.AllocatedAmount;

                invoice.PaidAmount = Math.Max(0m, Round(invoice.PaidAmount + moved));
                invoice.DueAmount = Round(invoice.GrandTotal - invoice.PaidAmount);

                // The invoice's own state follows from the numbers, so it can never
                // read as paid while it still has something owing.
                invoice.Status = invoice.DueAmount <= 0
                    ? SalesInvoiceStatus.Paid
                    : invoice.PaidAmount > 0
                        ? SalesInvoiceStatus.PartiallyPaid
                        : SalesInvoiceStatus.Posted;

                invoice.Updated = DateTime.Now;

                await _customerPaymentUnitOfWork.SalesInvoiceRepository.EditAsync(invoice);
            }
        }

        /// <summary>
        /// Moves what the customer owes. A collection funded by a credit note moves
        /// nothing: issuing that note already took the amount off the account, and
        /// taking it off twice would leave the customer owing less than they do.
        /// </summary>
        private async Task MoveOutstandingAsync(CustomerPayment payment, decimal amount)
        {
            if (payment.CreditNoteId.HasValue && payment.CreditNoteId.Value != Guid.Empty)
            {
                return;
            }

            var customer = await _customerPaymentUnitOfWork.CustomerRepository.GetByIdAsync(payment.CustomerId);

            if (customer == null)
            {
                return;
            }

            customer.CurrentOutstanding = Round(customer.CurrentOutstanding + amount);
            customer.Updated = DateTime.Now;

            await _customerPaymentUnitOfWork.CustomerRepository.EditAsync(customer);
        }

        /// <summary>
        /// Re-reads how much of a credit note has been spent from the collections that
        /// name it, so the note is never told a number that was worked out elsewhere.
        /// </summary>
        private async Task RefreshCreditNoteAsync(Guid? creditNoteId)
        {
            if (!creditNoteId.HasValue || creditNoteId.Value == Guid.Empty)
            {
                return;
            }

            var creditNote = await _customerPaymentUnitOfWork
                .CreditNoteRepository
                .GetByIdAsync(creditNoteId.Value);

            if (creditNote == null)
            {
                return;
            }

            var spent = await GetCreditSpentAsync(creditNote.Id, null);

            creditNote.AppliedAmount = Round(spent);
            creditNote.Status = creditNote.AppliedAmount >= creditNote.TotalAmount && creditNote.TotalAmount > 0
                ? CreditNoteStatus.Applied
                : CreditNoteStatus.Issued;
            creditNote.Updated = DateTime.Now;

            await _customerPaymentUnitOfWork.CreditNoteRepository.EditAsync(creditNote);
        }

        /// <summary>
        /// What one credit note has been spent on. A draft collection counts too: it has
        /// already claimed the credit even though the invoices are not settled yet.
        /// </summary>
        private async Task<decimal> GetCreditSpentAsync(Guid creditNoteId, Guid? excludePaymentId)
        {
            var payments = await _customerPaymentUnitOfWork
                .CustomerPaymentRepository
                .GetCustomerPaymentsByCreditNoteAsync(creditNoteId);

            return Round(payments
                .Where(x => x.Status != CustomerPaymentStatus.Cancelled)
                .Where(x => !excludePaymentId.HasValue || x.Id != excludePaymentId.Value)
                .Sum(x => x.Amount));
        }

        /// <summary>
        /// Per invoice, what the customer's draft collections have already pencilled in.
        /// Received money is already on the invoice itself, so it is not counted twice.
        /// </summary>
        private async Task<IDictionary<Guid, decimal>> GetPendingAllocationsAsync(Guid customerId,
            Guid? excludePaymentId)
        {
            var payments = await _customerPaymentUnitOfWork
                .CustomerPaymentRepository
                .GetCustomerPaymentsByCustomerAsync(customerId);

            var totals = new Dictionary<Guid, decimal>();

            foreach (var payment in payments.Where(x => x.Status == CustomerPaymentStatus.Draft))
            {
                if (excludePaymentId.HasValue && payment.Id == excludePaymentId.Value)
                {
                    continue;
                }

                foreach (var allocation in payment.CustomerPaymentAllocations
                    ?? new List<CustomerPaymentAllocation>())
                {
                    totals.TryGetValue(allocation.SalesInvoiceId, out var current);
                    totals[allocation.SalesInvoiceId] = current + allocation.AllocatedAmount;
                }
            }

            return totals;
        }

        private async Task<CustomerPayment> GetPaymentAsync(Guid id)
        {
            return await _customerPaymentUnitOfWork
                .CustomerPaymentRepository
                .GetCustomerPaymentByIdAsync(id)
                ?? throw new InvalidOperationException("Collection not found.");
        }

        private static decimal Round(decimal value)
        {
            return Math.Round(value, 2, MidpointRounding.AwayFromZero);
        }

        private sealed record RequestedAllocation(Guid SalesInvoiceId, decimal Amount, string Remarks);

        private sealed record ValidatedAllocation(Guid SalesInvoiceId, decimal DueAmount, decimal AllocatedAmount,
            string Remarks);

        private sealed record PaymentContext(Customer Customer, CreditNote? CreditNote, DateTime PaymentDate,
            PaymentMethod PaymentMethod, decimal Amount, IList<ValidatedAllocation> Allocations);
    }
}
