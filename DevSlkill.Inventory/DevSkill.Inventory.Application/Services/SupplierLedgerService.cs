using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Domain.UnitOfWorkContracts;

namespace DevSkill.Inventory.Application.Services
{
    /// <summary>
    /// Reads the supplier account. Nothing here writes, so the statement can always
    /// be re-run and has to agree with the running total kept on the supplier.
    /// </summary>
    public class SupplierLedgerService : ISupplierLedgerService
    {
        private readonly IInventoryUnitOfWork _supplierLedgerUnitOfWork;

        public SupplierLedgerService(IInventoryUnitOfWork unitOfWork)
        {
            _supplierLedgerUnitOfWork = unitOfWork;
        }

        public async Task<SupplierLedgerDto> GetSupplierLedgerAsync(Guid supplierId, DateTime fromDate,
            DateTime toDate)
        {
            var supplier = await _supplierLedgerUnitOfWork
                .SupplierRepository
                .GetSupplierByIdAsync(supplierId)
                ?? throw new InvalidOperationException("Supplier not found.");

            // A period read back to front would silently return nothing, which reads
            // as "no activity" rather than as the mistake it is.
            if (toDate.Date < fromDate.Date)
            {
                throw new InvalidOperationException("The end date cannot be earlier than the start date.");
            }

            var movements = await GetMovementsAsync(supplierId);

            var ledger = new SupplierLedgerDto
            {
                SupplierId = supplier.Id,
                SupplierName = supplier.SupplierName,
                SupplierCode = supplier.SupplierCode,
                FromDate = fromDate.Date,
                ToDate = toDate.Date,

                // Whatever the supplier started with, moved forward by everything that
                // happened before the period.
                OpeningBalance = supplier.OpeningBalance
                    + movements.Where(x => x.Date.Date < fromDate.Date).Sum(x => x.Credit - x.Debit)
            };

            var balance = ledger.OpeningBalance;

            foreach (var movement in movements
                .Where(x => x.Date.Date >= fromDate.Date && x.Date.Date <= toDate.Date)
                .OrderBy(x => x.Date)
                .ThenBy(x => x.DocumentType)
                .ThenBy(x => x.DocumentNo))
            {
                balance += movement.Credit - movement.Debit;

                ledger.Entries.Add(new SupplierLedgerEntryDto
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
        /// that really changed the balance are taken, which is what keeps this in
        /// step with the running total on the supplier.
        /// </summary>
        private async Task<List<Movement>> GetMovementsAsync(Guid supplierId)
        {
            var movements = new List<Movement>();

            var invoices = await _supplierLedgerUnitOfWork
                .PurchaseInvoiceRepository
                .GetPurchaseInvoicesBySupplierAsync(supplierId);

            // A draft invoice owes nothing and a cancelled one has been taken back off
            // the account, so neither belongs on the statement.
            foreach (var invoice in invoices.Where(x => x.Status is PurchaseInvoiceStatus.Posted
                or PurchaseInvoiceStatus.PartiallyPaid or PurchaseInvoiceStatus.Paid))
            {
                movements.Add(new Movement(
                    invoice.InvoiceDate,
                    "Invoice",
                    invoice.InvoiceNo,
                    string.IsNullOrWhiteSpace(invoice.SupplierInvoiceNo)
                        ? "Purchase invoice"
                        : $"Purchase invoice against supplier bill {invoice.SupplierInvoiceNo}",
                    0m,
                    invoice.GrandTotal));
            }

            var returns = await _supplierLedgerUnitOfWork
                .PurchaseReturnRepository
                .GetPurchaseReturnsBySupplierAsync(supplierId);

            foreach (var purchaseReturn in returns.Where(x => x.Status == PurchaseReturnStatus.Returned))
            {
                movements.Add(new Movement(
                    purchaseReturn.ReturnDate,
                    "Return",
                    purchaseReturn.ReturnNo,
                    string.IsNullOrWhiteSpace(purchaseReturn.Reason)
                        ? "Goods returned to supplier"
                        : $"Goods returned to supplier: {purchaseReturn.Reason}",
                    purchaseReturn.TotalAmount,
                    0m));
            }

            var payments = await _supplierLedgerUnitOfWork
                .SupplierPaymentRepository
                .GetSupplierPaymentsBySupplierAsync(supplierId);

            foreach (var payment in payments.Where(x => x.Status == SupplierPaymentStatus.Paid))
            {
                // Only the allocated part settles invoices, so only that part moves the
                // account. Anything over is an advance and is called out in the text.
                var advance = payment.Amount - payment.AllocatedAmount;

                movements.Add(new Movement(
                    payment.PaymentDate,
                    "Payment",
                    payment.PaymentNo,
                    advance > 0
                        ? $"Payment by {payment.PaymentMethod} ({advance:N2} unallocated advance)"
                        : $"Payment by {payment.PaymentMethod}",
                    payment.AllocatedAmount,
                    0m));
            }

            return movements;
        }

        private sealed record Movement(DateTime Date, string DocumentType, string DocumentNo,
            string Particulars, decimal Debit, decimal Credit);
    }
}
