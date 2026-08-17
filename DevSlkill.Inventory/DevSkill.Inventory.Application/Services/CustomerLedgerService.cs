using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Domain.UnitOfWorkContracts;

namespace DevSkill.Inventory.Application.Services
{
    /// <summary>
    /// Reads the customer account. Nothing here writes, so the statement can always
    /// be re-run and has to agree with the running total kept on the customer.
    /// </summary>
    public class CustomerLedgerService : ICustomerLedgerService
    {
        private readonly IInventoryUnitOfWork _customerLedgerUnitOfWork;

        public CustomerLedgerService(IInventoryUnitOfWork unitOfWork)
        {
            _customerLedgerUnitOfWork = unitOfWork;
        }

        public async Task<CustomerLedgerDto> GetCustomerLedgerAsync(Guid customerId, DateTime fromDate,
            DateTime toDate)
        {
            var customer = await _customerLedgerUnitOfWork
                .CustomerRepository
                .GetByIdAsync(customerId)
                ?? throw new InvalidOperationException("Customer not found.");

            // A period read back to front would silently return nothing, which reads as
            // "no activity" rather than as the mistake it is.
            if (toDate.Date < fromDate.Date)
            {
                throw new InvalidOperationException("The end date cannot be earlier than the start date.");
            }

            var movements = await GetMovementsAsync(customerId);

            var ledger = new CustomerLedgerDto
            {
                CustomerId = customer.Id,
                CustomerName = customer.CustomerName,
                CustomerCode = customer.CustomerCode,
                CreditLimit = customer.CreditLimit,
                FromDate = fromDate.Date,
                ToDate = toDate.Date,

                // Whatever the customer started with, moved forward by everything that
                // happened before the period.
                OpeningBalance = customer.OpeningBalance
                    + movements.Where(x => x.Date.Date < fromDate.Date).Sum(x => x.Debit - x.Credit)
            };

            var balance = ledger.OpeningBalance;

            foreach (var movement in movements
                .Where(x => x.Date.Date >= fromDate.Date && x.Date.Date <= toDate.Date)
                .OrderBy(x => x.Date)
                .ThenBy(x => x.DocumentType)
                .ThenBy(x => x.DocumentNo))
            {
                balance += movement.Debit - movement.Credit;

                ledger.Entries.Add(new CustomerLedgerEntryDto
                {
                    Date = movement.Date,
                    DocumentType = movement.DocumentType,
                    DocumentNo = movement.DocumentNo,
                    Particulars = movement.Particulars,
                    Debit = movement.Debit,
                    Credit = movement.Credit,
                    Balance = balance
                });
            }

            ledger.TotalDebit = ledger.Entries.Sum(x => x.Debit);
            ledger.TotalCredit = ledger.Entries.Sum(x => x.Credit);
            ledger.ClosingBalance = balance;

            return ledger;
        }

        /// <summary>
        /// Every document that moves the account, in one flat list. Only the states
        /// that really changed the balance are taken, which is what keeps this in step
        /// with the running total on the customer.
        /// </summary>
        /// <remarks>
        /// A sales return is not a row of its own: confirming one raises a credit note
        /// for exactly its value, and that credit note is the document that moves the
        /// money. Listing both would credit the customer twice.
        /// </remarks>
        private async Task<List<Movement>> GetMovementsAsync(Guid customerId)
        {
            var movements = new List<Movement>();

            var invoices = await _customerLedgerUnitOfWork
                .SalesInvoiceRepository
                .GetSalesInvoicesByCustomerAsync(customerId);

            // A draft invoice claims nothing and a cancelled one has been taken back off
            // the account, so neither belongs on the statement.
            foreach (var invoice in invoices.Where(x => x.Status is SalesInvoiceStatus.Posted
                or SalesInvoiceStatus.PartiallyPaid or SalesInvoiceStatus.Paid))
            {
                movements.Add(new Movement(
                    invoice.InvoiceDate,
                    "Invoice",
                    invoice.InvoiceNo,
                    invoice.SalesOrder == null
                        ? "Sales invoice"
                        : $"Sales invoice against order {invoice.SalesOrder.SalesOrderNo}",
                    invoice.GrandTotal,
                    0m));
            }

            var creditNotes = await _customerLedgerUnitOfWork
                .CreditNoteRepository
                .GetCreditNotesByCustomerAsync(customerId);

            foreach (var creditNote in creditNotes.Where(x => x.Status is CreditNoteStatus.Issued
                or CreditNoteStatus.Applied))
            {
                movements.Add(new Movement(
                    creditNote.CreditNoteDate,
                    "Credit Note",
                    creditNote.CreditNoteNo,
                    string.IsNullOrWhiteSpace(creditNote.Reason)
                        ? "Credit issued to customer"
                        : $"Credit issued to customer: {creditNote.Reason}",
                    0m,
                    creditNote.TotalAmount));
            }

            var payments = await _customerLedgerUnitOfWork
                .CustomerPaymentRepository
                .GetCustomerPaymentsByCustomerAsync(customerId);

            foreach (var payment in payments.Where(x => x.Status == CustomerPaymentStatus.Received))
            {
                // A collection funded by a credit note moves no money: the note already
                // credited the account, and this only settles the invoices it points at.
                if (payment.CreditNoteId.HasValue && payment.CreditNoteId.Value != Guid.Empty)
                {
                    continue;
                }

                // Only the allocated part settles invoices, so only that part moves the
                // account. Anything over is an advance and is called out in the text.
                var advance = payment.Amount - payment.AllocatedAmount;

                movements.Add(new Movement(
                    payment.PaymentDate,
                    "Collection",
                    payment.PaymentNo,
                    advance > 0
                        ? $"Collection by {payment.PaymentMethod} ({advance:N2} unallocated advance)"
                        : $"Collection by {payment.PaymentMethod}",
                    0m,
                    payment.AllocatedAmount));
            }

            return movements;
        }

        private sealed record Movement(DateTime Date, string DocumentType, string DocumentNo,
            string Particulars, decimal Debit, decimal Credit);
    }
}
