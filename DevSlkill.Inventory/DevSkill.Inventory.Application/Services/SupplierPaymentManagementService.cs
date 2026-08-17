using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Domain.UnitOfWorkContracts;

namespace DevSkill.Inventory.Application.Services
{
    public class SupplierPaymentManagementService : ISupplierPaymentManagementService
    {
        private readonly IInventoryUnitOfWork _supplierPaymentUnitOfWork;

        public SupplierPaymentManagementService(IInventoryUnitOfWork unitOfWork)
        {
            _supplierPaymentUnitOfWork = unitOfWork;
        }

        public async Task<Guid> CreateSupplierPaymentAsync(SupplierPayment supplierPayment)
        {
            await _supplierPaymentUnitOfWork.BeginTransactionAsync();

            try
            {
                var context = await ValidateAsync(supplierPayment, null);
                var supplierPaymentId = Guid.NewGuid();

                var allocations = BuildAllocations(context.Allocations, supplierPaymentId);

                var header = new SupplierPayment
                {
                    Id = supplierPaymentId,
                    PaymentNo = await GeneratePaymentNoAsync(),
                    SupplierId = context.Supplier.Id,
                    PaymentDate = context.PaymentDate,
                    PaymentMethod = supplierPayment.PaymentMethod,
                    ReferenceNo = supplierPayment.ReferenceNo ?? string.Empty,
                    Amount = context.Amount,
                    AllocatedAmount = allocations.Sum(x => x.AllocatedAmount),
                    Notes = supplierPayment.Notes ?? string.Empty,
                    Status = SupplierPaymentStatus.Draft,
                    CreatedBy = supplierPayment.CreatedBy ?? string.Empty,
                    Created = DateTime.Now,
                    Updated = DateTime.Now,
                    SupplierPaymentAllocations = allocations
                };

                await _supplierPaymentUnitOfWork.SupplierPaymentRepository.AddAsync(header);

                // A draft settles nothing. Asking for it to be paid straight away runs
                // the confirmation inside this very transaction.
                if (supplierPayment.Status == SupplierPaymentStatus.Paid)
                {
                    await ApplyConfirmationAsync(header, allocations);
                }

                await _supplierPaymentUnitOfWork.CommitTransactionAsync();

                return supplierPaymentId;
            }
            catch (InvalidOperationException)
            {
                await _supplierPaymentUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _supplierPaymentUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<bool> UpdateSupplierPaymentAsync(SupplierPayment supplierPayment)
        {
            await _supplierPaymentUnitOfWork.BeginTransactionAsync();

            try
            {
                var existing = await _supplierPaymentUnitOfWork
                    .SupplierPaymentRepository
                    .GetSupplierPaymentByIdAsync(supplierPayment.Id)
                    ?? throw new InvalidOperationException("Supplier payment not found.");

                // A confirmed payment has already settled invoices, so its figures are
                // history. Cancel it and record a new one instead.
                if (existing.Status != SupplierPaymentStatus.Draft)
                {
                    throw new InvalidOperationException(
                        $"A {existing.Status.ToString().ToLower()} supplier payment cannot be edited.");
                }

                // The supplier a payment belongs to is what the payment is.
                supplierPayment.SupplierId = existing.SupplierId;

                var context = await ValidateAsync(supplierPayment, existing);

                await _supplierPaymentUnitOfWork
                    .SupplierPaymentAllocationRepository
                    .RemoveRangeAsync(existing.SupplierPaymentAllocations?.ToList()
                        ?? new List<SupplierPaymentAllocation>());

                var allocations = BuildAllocations(context.Allocations, existing.Id);

                foreach (var allocation in allocations)
                {
                    await _supplierPaymentUnitOfWork.SupplierPaymentAllocationRepository.AddAsync(allocation);
                }

                // The payment number is issued once and never changes.
                existing.PaymentDate = context.PaymentDate;
                existing.PaymentMethod = supplierPayment.PaymentMethod;
                existing.ReferenceNo = supplierPayment.ReferenceNo ?? string.Empty;
                existing.Amount = context.Amount;
                existing.AllocatedAmount = allocations.Sum(x => x.AllocatedAmount);
                existing.Notes = supplierPayment.Notes ?? string.Empty;
                existing.Updated = DateTime.Now;

                await _supplierPaymentUnitOfWork.SupplierPaymentRepository.EditAsync(existing);

                if (supplierPayment.Status == SupplierPaymentStatus.Paid)
                {
                    await ApplyConfirmationAsync(existing, allocations);
                }

                await _supplierPaymentUnitOfWork.CommitTransactionAsync();

                return true;
            }
            catch (InvalidOperationException)
            {
                await _supplierPaymentUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _supplierPaymentUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<bool> DeleteSupplierPaymentAsync(Guid id)
        {
            await _supplierPaymentUnitOfWork.BeginTransactionAsync();

            try
            {
                var supplierPayment = await _supplierPaymentUnitOfWork
                    .SupplierPaymentRepository
                    .GetSupplierPaymentByIdAsync(id)
                    ?? throw new InvalidOperationException("Supplier payment not found.");

                // Deleting a confirmed payment would erase money that really moved.
                // Cancelling it puts the due back and keeps the record.
                if (supplierPayment.Status == SupplierPaymentStatus.Paid)
                {
                    throw new InvalidOperationException(
                        "A confirmed supplier payment cannot be deleted. Cancel it instead, which puts the due back.");
                }

                await _supplierPaymentUnitOfWork
                    .SupplierPaymentAllocationRepository
                    .RemoveRangeAsync(supplierPayment.SupplierPaymentAllocations?.ToList()
                        ?? new List<SupplierPaymentAllocation>());

                await _supplierPaymentUnitOfWork.SupplierPaymentRepository.RemoveAsync(supplierPayment);

                await _supplierPaymentUnitOfWork.CommitTransactionAsync();

                return true;
            }
            catch (InvalidOperationException)
            {
                await _supplierPaymentUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _supplierPaymentUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<SupplierPayment?> GetSupplierPaymentByIdAsync(Guid id)
        {
            return await _supplierPaymentUnitOfWork.SupplierPaymentRepository.GetSupplierPaymentByIdAsync(id);
        }

        public async Task<(IList<SupplierPayment> data, int total, int totalDisplay)> GetSupplierPaymentsAsync(
            int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            return await _supplierPaymentUnitOfWork
                .SupplierPaymentRepository
                .GetPagedSupplierPaymentsAsync(pageIndex, pageSize, search, order);
        }

        public async Task<IList<SupplierPayment>> GetSupplierPaymentsBySupplierAsync(Guid supplierId)
        {
            return await _supplierPaymentUnitOfWork
                .SupplierPaymentRepository
                .GetSupplierPaymentsBySupplierAsync(supplierId);
        }

        public async Task<string> GeneratePaymentNoAsync()
        {
            var count = await _supplierPaymentUnitOfWork.SupplierPaymentRepository.GetCountAsync();

            string paymentNo;
            do
            {
                count++;
                paymentNo = $"SPM-{DateTime.Now:yyyyMM}-{count:D4}";
            }
            while (await _supplierPaymentUnitOfWork.SupplierPaymentRepository.IsPaymentNoDuplicateAsync(paymentNo));

            return paymentNo;
        }

        public async Task ConfirmSupplierPaymentAsync(Guid id)
        {
            await _supplierPaymentUnitOfWork.BeginTransactionAsync();

            try
            {
                var supplierPayment = await _supplierPaymentUnitOfWork
                    .SupplierPaymentRepository
                    .GetSupplierPaymentByIdAsync(id)
                    ?? throw new InvalidOperationException("Supplier payment not found.");

                if (supplierPayment.Status != SupplierPaymentStatus.Draft)
                {
                    throw new InvalidOperationException(
                        $"Only a draft supplier payment can be confirmed. This one is already {supplierPayment.Status.ToString().ToLower()}.");
                }

                var allocations = supplierPayment.SupplierPaymentAllocations?.ToList()
                    ?? new List<SupplierPaymentAllocation>();

                // The dues are checked again at confirmation time, because another
                // payment may have settled them since this draft was saved.
                await GuardAgainstOverPayingAsync(supplierPayment.SupplierId, allocations, supplierPayment.Id);

                await ApplyConfirmationAsync(supplierPayment, allocations);

                await _supplierPaymentUnitOfWork.CommitTransactionAsync();
            }
            catch (InvalidOperationException)
            {
                await _supplierPaymentUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _supplierPaymentUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task CancelSupplierPaymentAsync(Guid id)
        {
            await _supplierPaymentUnitOfWork.BeginTransactionAsync();

            try
            {
                var supplierPayment = await _supplierPaymentUnitOfWork
                    .SupplierPaymentRepository
                    .GetSupplierPaymentByIdAsync(id)
                    ?? throw new InvalidOperationException("Supplier payment not found.");

                if (supplierPayment.Status == SupplierPaymentStatus.Cancelled)
                {
                    throw new InvalidOperationException("This supplier payment is already cancelled.");
                }

                var allocations = supplierPayment.SupplierPaymentAllocations?.ToList()
                    ?? new List<SupplierPaymentAllocation>();

                if (supplierPayment.Status == SupplierPaymentStatus.Paid)
                {
                    // What this payment settled goes straight back onto the invoices.
                    await ApplyInvoiceSettlementAsync(allocations, settle: false);
                    await AdjustSupplierBalanceAsync(supplierPayment.SupplierId, supplierPayment.AllocatedAmount);
                }

                supplierPayment.Status = SupplierPaymentStatus.Cancelled;
                supplierPayment.Updated = DateTime.Now;

                await _supplierPaymentUnitOfWork.SupplierPaymentRepository.EditAsync(supplierPayment);
                await _supplierPaymentUnitOfWork.CommitTransactionAsync();
            }
            catch (InvalidOperationException)
            {
                await _supplierPaymentUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _supplierPaymentUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<IList<PayableInvoiceDto>> GetPayableInvoicesAsync(Guid supplierId,
            Guid? excludeSupplierPaymentId = null)
        {
            var invoices = await _supplierPaymentUnitOfWork
                .PurchaseInvoiceRepository
                .GetPurchaseInvoicesBySupplierAsync(supplierId);

            var pending = await GetPendingAmountsAsync(supplierId, excludeSupplierPaymentId);

            var payable = new List<PayableInvoiceDto>();

            foreach (var invoice in invoices.Where(IsOutstanding))
            {
                var pendingAmount = pending.GetValueOrDefault(invoice.Id);
                var remaining = invoice.DueAmount - pendingAmount;

                payable.Add(new PayableInvoiceDto
                {
                    PurchaseInvoiceId = invoice.Id,
                    InvoiceNo = invoice.InvoiceNo,
                    SupplierInvoiceNo = invoice.SupplierInvoiceNo,
                    InvoiceDate = invoice.InvoiceDate,
                    DueDate = invoice.DueDate,
                    OverdueDays = (int)(DateTime.Today - invoice.DueDate.Date).TotalDays,
                    GrandTotal = invoice.GrandTotal,
                    PaidAmount = invoice.PaidAmount,
                    PendingAmount = pendingAmount,
                    RemainingAmount = remaining < 0 ? 0 : remaining,
                    Status = invoice.Status.ToString()
                });
            }

            return payable.OrderBy(x => x.DueDate).ToList();
        }

        /// <summary>
        /// Everything create and update both need before any allocation can be built.
        /// </summary>
        private async Task<PaymentContext> ValidateAsync(SupplierPayment supplierPayment, SupplierPayment? existing)
        {
            var supplier = await _supplierPaymentUnitOfWork
                .SupplierRepository
                .GetSupplierByIdAsync(supplierPayment.SupplierId)
                ?? throw new InvalidOperationException("Supplier not found.");

            if (existing == null && supplier.Status == SupplierStatus.Blacklisted)
            {
                throw new InvalidOperationException(
                    "A blacklisted supplier cannot be paid. Change the supplier status first.");
            }

            var paymentDate = supplierPayment.PaymentDate == default ? DateTime.Now : supplierPayment.PaymentDate;

            if (supplierPayment.Amount <= 0)
            {
                throw new InvalidOperationException("The payment amount has to be greater than zero.");
            }

            var invoices = (await _supplierPaymentUnitOfWork
                .PurchaseInvoiceRepository
                .GetPurchaseInvoicesBySupplierAsync(supplierPayment.SupplierId))
                .ToDictionary(x => x.Id);

            var grouped = supplierPayment.SupplierPaymentAllocations?
                .Where(x => x.PurchaseInvoiceId != Guid.Empty && x.AllocatedAmount > 0)
                .GroupBy(x => x.PurchaseInvoiceId)
                .Select(g => new RequestedAllocation(
                    g.Key,
                    g.Sum(x => x.AllocatedAmount),
                    g.Select(x => x.Remarks).FirstOrDefault(x => !string.IsNullOrWhiteSpace(x)) ?? string.Empty))
                .ToList();

            if (grouped == null || grouped.Count == 0)
            {
                throw new InvalidOperationException("At least one invoice has to be allocated an amount.");
            }

            var validated = new List<ValidatedAllocation>();

            foreach (var requested in grouped)
            {
                if (!invoices.TryGetValue(requested.PurchaseInvoiceId, out var invoice))
                {
                    throw new InvalidOperationException(
                        "An invoice that does not belong to this supplier cannot be paid on this payment.");
                }

                if (!IsOutstanding(invoice))
                {
                    throw new InvalidOperationException(
                        $"Invoice '{invoice.InvoiceNo}' is {invoice.Status.ToString().ToLower()} and has nothing left to pay.");
                }

                validated.Add(new ValidatedAllocation(
                    invoice.Id,
                    invoice.DueAmount,
                    Math.Round(requested.AllocatedAmount, 2),
                    requested.Remarks));
            }

            var allocatedTotal = validated.Sum(x => x.AllocatedAmount);

            // Paying out more than was allocated is an advance and is allowed. The
            // other way round would settle money that never left the account.
            if (allocatedTotal > Math.Round(supplierPayment.Amount, 2))
            {
                throw new InvalidOperationException(
                    $"The allocations come to {allocatedTotal:N2}, which is more than the payment of {supplierPayment.Amount:N2}.");
            }

            await GuardAgainstOverPayingAsync(supplier.Id,
                validated.Select(x => new SupplierPaymentAllocation
                {
                    PurchaseInvoiceId = x.PurchaseInvoiceId,
                    AllocatedAmount = x.AllocatedAmount
                }).ToList(),
                existing?.Id);

            return new PaymentContext(supplier, validated, paymentDate, Math.Round(supplierPayment.Amount, 2));
        }

        /// <summary>
        /// The invoice due is the ceiling. Draft payments count towards it too, so
        /// the same invoice cannot be settled twice while a draft is open.
        /// </summary>
        private async Task GuardAgainstOverPayingAsync(Guid supplierId,
            IList<SupplierPaymentAllocation> allocations, Guid? excludeSupplierPaymentId)
        {
            var invoices = (await _supplierPaymentUnitOfWork
                .PurchaseInvoiceRepository
                .GetPurchaseInvoicesBySupplierAsync(supplierId))
                .ToDictionary(x => x.Id);

            var pending = await GetPendingAmountsAsync(supplierId, excludeSupplierPaymentId);

            foreach (var allocation in allocations.Where(x => x.AllocatedAmount > 0))
            {
                if (!invoices.TryGetValue(allocation.PurchaseInvoiceId, out var invoice))
                {
                    throw new InvalidOperationException(
                        "An invoice that does not belong to this supplier cannot be paid on this payment.");
                }

                var remaining = invoice.DueAmount - pending.GetValueOrDefault(allocation.PurchaseInvoiceId);

                if (allocation.AllocatedAmount > remaining)
                {
                    throw new InvalidOperationException(
                        $"Invoice '{invoice.InvoiceNo}' has only {(remaining < 0 ? 0 : remaining):N2} left to pay, " +
                        $"but {allocation.AllocatedAmount:N2} is being allocated to it.");
                }
            }
        }

        /// <summary>
        /// Draft payments of the supplier, invoice by invoice. One payment can be
        /// left out so an edit is measured against the right figure.
        /// </summary>
        private async Task<Dictionary<Guid, decimal>> GetPendingAmountsAsync(Guid supplierId,
            Guid? excludeSupplierPaymentId)
        {
            var payments = await _supplierPaymentUnitOfWork
                .SupplierPaymentRepository
                .GetSupplierPaymentsBySupplierAsync(supplierId);

            return payments
                .Where(x => x.Status == SupplierPaymentStatus.Draft
                            && (!excludeSupplierPaymentId.HasValue || x.Id != excludeSupplierPaymentId.Value))
                .SelectMany(x => x.SupplierPaymentAllocations ?? new List<SupplierPaymentAllocation>())
                .GroupBy(x => x.PurchaseInvoiceId)
                .ToDictionary(g => g.Key, g => g.Sum(x => x.AllocatedAmount));
        }

        /// <summary>
        /// Marks the payment as paid, settles the invoices and takes the amount off
        /// what the supplier is owed. Every caller already runs inside a transaction.
        /// </summary>
        private async Task ApplyConfirmationAsync(SupplierPayment supplierPayment,
            IList<SupplierPaymentAllocation> allocations)
        {
            await ApplyInvoiceSettlementAsync(allocations, settle: true);
            await AdjustSupplierBalanceAsync(supplierPayment.SupplierId, -allocations.Sum(x => x.AllocatedAmount));

            supplierPayment.Status = SupplierPaymentStatus.Paid;
            supplierPayment.Updated = DateTime.Now;

            await _supplierPaymentUnitOfWork.SupplierPaymentRepository.EditAsync(supplierPayment);
        }

        /// <summary>
        /// Moves the paid amount on the invoices and re-reads their status from it, so
        /// each one says exactly how much is still outstanding.
        /// </summary>
        private async Task ApplyInvoiceSettlementAsync(IEnumerable<SupplierPaymentAllocation> allocations, bool settle)
        {
            foreach (var allocation in allocations.Where(x => x.AllocatedAmount > 0))
            {
                var invoice = await _supplierPaymentUnitOfWork
                    .PurchaseInvoiceRepository
                    .GetByIdAsync(allocation.PurchaseInvoiceId)
                    ?? throw new InvalidOperationException("Purchase invoice not found.");

                invoice.PaidAmount += settle ? allocation.AllocatedAmount : -allocation.AllocatedAmount;

                if (invoice.PaidAmount < 0)
                {
                    invoice.PaidAmount = 0;
                }

                invoice.DueAmount = invoice.GrandTotal - invoice.PaidAmount;

                // A cancelled invoice keeps its own state; the payment flow only ever
                // writes the paid states of a live one.
                if (invoice.Status != PurchaseInvoiceStatus.Cancelled)
                {
                    invoice.Status = invoice.PaidAmount <= 0
                        ? PurchaseInvoiceStatus.Posted
                        : invoice.DueAmount <= 0
                            ? PurchaseInvoiceStatus.Paid
                            : PurchaseInvoiceStatus.PartiallyPaid;
                }

                invoice.Updated = DateTime.Now;

                await _supplierPaymentUnitOfWork.PurchaseInvoiceRepository.EditAsync(invoice);
            }
        }

        /// <summary>
        /// The single place a payment moves what is owed to a supplier, kept in step
        /// with the way the purchase invoice and return do it.
        /// </summary>
        private async Task AdjustSupplierBalanceAsync(Guid supplierId, decimal amount)
        {
            var supplier = await _supplierPaymentUnitOfWork
                .SupplierRepository
                .GetByIdAsync(supplierId)
                ?? throw new InvalidOperationException("Supplier not found.");

            supplier.CurrentOutstanding += amount;
            supplier.Updated = DateTime.Now;

            await _supplierPaymentUnitOfWork.SupplierRepository.EditAsync(supplier);
        }

        /// <summary>
        /// Only a posted invoice that still owes something can be paid against.
        /// </summary>
        private static bool IsOutstanding(PurchaseInvoice invoice)
        {
            return invoice.Status is PurchaseInvoiceStatus.Posted or PurchaseInvoiceStatus.PartiallyPaid
                && invoice.DueAmount > 0;
        }

        private static List<SupplierPaymentAllocation> BuildAllocations(IList<ValidatedAllocation> allocations,
            Guid supplierPaymentId)
        {
            return allocations.Select(x => new SupplierPaymentAllocation
            {
                Id = Guid.NewGuid(),
                SupplierPaymentId = supplierPaymentId,
                PurchaseInvoiceId = x.PurchaseInvoiceId,
                DueAmount = x.DueAmount,
                AllocatedAmount = x.AllocatedAmount,
                Remarks = x.Remarks
            }).ToList();
        }

        private sealed record RequestedAllocation(Guid PurchaseInvoiceId, decimal AllocatedAmount, string Remarks);

        private sealed record ValidatedAllocation(Guid PurchaseInvoiceId, decimal DueAmount,
            decimal AllocatedAmount, string Remarks);

        private sealed record PaymentContext(
            Supplier Supplier,
            IList<ValidatedAllocation> Allocations,
            DateTime PaymentDate,
            decimal Amount);
    }
}
