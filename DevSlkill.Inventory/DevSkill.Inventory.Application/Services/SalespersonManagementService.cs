using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Domain.UnitOfWorkContracts;

namespace DevSkill.Inventory.Application.Services
{
    /// <summary>
    /// The people who sell, and the commission their sales earn. Commission lines are
    /// written by the invoice flow when an invoice is posted; nothing here creates
    /// one, it only moves an existing line through approval and payment.
    /// </summary>
    public class SalespersonManagementService : ISalespersonManagementService
    {
        private readonly IInventoryUnitOfWork _salespersonUnitOfWork;

        public SalespersonManagementService(IInventoryUnitOfWork unitOfWork)
        {
            _salespersonUnitOfWork = unitOfWork;
        }

        public async Task<Guid> CreateSalespersonAsync(Salesperson salesperson)
        {
            try
            {
                Validate(salesperson);

                var code = string.IsNullOrWhiteSpace(salesperson.SalespersonCode)
                    ? await GenerateSalespersonCodeAsync()
                    : salesperson.SalespersonCode.Trim();

                if (await _salespersonUnitOfWork.SalespersonRepository.IsSalespersonCodeDuplicateAsync(code))
                {
                    throw new InvalidOperationException($"'{code}' is already used by another salesperson.");
                }

                salesperson.Id = Guid.NewGuid();
                salesperson.SalespersonCode = code;
                salesperson.Created = DateTime.Now;
                salesperson.Updated = DateTime.Now;

                await _salespersonUnitOfWork.SalespersonRepository.AddAsync(salesperson);
                await _salespersonUnitOfWork.SaveAsync();

                return salesperson.Id;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<bool> UpdateSalespersonAsync(Salesperson salesperson)
        {
            try
            {
                var existing = await _salespersonUnitOfWork
                    .SalespersonRepository
                    .GetSalespersonByIdAsync(salesperson.Id)
                    ?? throw new InvalidOperationException("Salesperson not found.");

                Validate(salesperson);

                var code = salesperson.SalespersonCode.Trim();

                if (await _salespersonUnitOfWork
                    .SalespersonRepository
                    .IsSalespersonCodeDuplicateAsync(code, existing.Id))
                {
                    throw new InvalidOperationException($"'{code}' is already used by another salesperson.");
                }

                existing.SalespersonCode = code;
                existing.SalespersonName = salesperson.SalespersonName.Trim();
                existing.Phone = salesperson.Phone ?? string.Empty;
                existing.Email = salesperson.Email ?? string.Empty;
                existing.UserId = salesperson.UserId;

                // Changing the rate never rewrites commission that has already been
                // earned, because every line keeps the rate it was earned at.
                existing.CommissionBasis = salesperson.CommissionBasis;
                existing.CommissionRate = salesperson.CommissionRate;
                existing.MonthlyTarget = salesperson.MonthlyTarget;
                existing.Status = salesperson.Status;
                existing.Updated = DateTime.Now;

                await _salespersonUnitOfWork.SalespersonRepository.EditAsync(existing);
                await _salespersonUnitOfWork.SaveAsync();

                return true;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<bool> DeleteSalespersonAsync(Guid id)
        {
            try
            {
                var salesperson = await _salespersonUnitOfWork
                    .SalespersonRepository
                    .GetSalespersonByIdAsync(id)
                    ?? throw new InvalidOperationException("Salesperson not found.");

                // Deleting someone who has sold would take their invoices' history with
                // them. Making them inactive is what is meant here.
                var commissions = await _salespersonUnitOfWork
                    .SalesCommissionRepository
                    .GetSalesCommissionsBySalespersonAsync(id);

                if (commissions.Count > 0)
                {
                    throw new InvalidOperationException(
                        "This salesperson has already earned commission, so they cannot be deleted. Mark them inactive instead.");
                }

                var invoices = await _salespersonUnitOfWork
                    .SalesInvoiceRepository
                    .GetSalesInvoicesBySalespersonAsync(id);

                if (invoices.Count > 0)
                {
                    throw new InvalidOperationException(
                        "This salesperson is named on a sales invoice, so they cannot be deleted. Mark them inactive instead.");
                }

                await _salespersonUnitOfWork.SalespersonRepository.RemoveAsync(salesperson);
                await _salespersonUnitOfWork.SaveAsync();

                return true;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<Salesperson?> GetSalespersonByIdAsync(Guid id)
        {
            return await _salespersonUnitOfWork.SalespersonRepository.GetSalespersonByIdAsync(id);
        }

        public async Task<(IList<Salesperson> data, int total, int totalDisplay)> GetSalespersonsAsync(int pageIndex,
            int pageSize, DataTablesSearch search, string? order)
        {
            return await _salespersonUnitOfWork
                .SalespersonRepository
                .GetPagedSalespersonsAsync(pageIndex, pageSize, search, order);
        }

        public async Task<IList<Salesperson>> GetActiveSalespersonsAsync()
        {
            return await _salespersonUnitOfWork.SalespersonRepository.GetActiveSalespersonsAsync();
        }

        public async Task<string> GenerateSalespersonCodeAsync()
        {
            var count = await _salespersonUnitOfWork.SalespersonRepository.GetCountAsync();

            string code;
            do
            {
                count++;
                code = $"SP-{count:D4}";
            }
            while (await _salespersonUnitOfWork.SalespersonRepository.IsSalespersonCodeDuplicateAsync(code));

            return code;
        }

        public async Task<(IList<SalesCommission> data, int total, int totalDisplay)> GetSalesCommissionsAsync(
            int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            return await _salespersonUnitOfWork
                .SalesCommissionRepository
                .GetPagedSalesCommissionsAsync(pageIndex, pageSize, search, order);
        }

        public async Task<SalesCommission?> GetSalesCommissionByIdAsync(Guid id)
        {
            return await _salespersonUnitOfWork.SalesCommissionRepository.GetSalesCommissionByIdAsync(id);
        }

        public async Task<IList<SalesCommission>> GetSalesCommissionsByStatusAsync(params CommissionStatus[] statuses)
        {
            return await _salespersonUnitOfWork.SalesCommissionRepository.GetSalesCommissionsByStatusAsync(statuses);
        }

        public async Task ApproveCommissionAsync(Guid id)
        {
            var commission = await GetCommissionAsync(id);

            if (commission.Status != CommissionStatus.Pending)
            {
                throw new InvalidOperationException(
                    $"Only a pending commission can be approved, this one is {commission.Status.ToString().ToLower()}.");
            }

            commission.Status = CommissionStatus.Approved;
            commission.Updated = DateTime.Now;

            await SaveCommissionAsync(commission);
        }

        public async Task PayCommissionAsync(Guid id)
        {
            var commission = await GetCommissionAsync(id);

            if (commission.Status != CommissionStatus.Approved)
            {
                throw new InvalidOperationException(
                    "Only an approved commission can be paid. Approve it first.");
            }

            commission.Status = CommissionStatus.Paid;
            commission.PaidDate = DateTime.Now;
            commission.Updated = DateTime.Now;

            await SaveCommissionAsync(commission);
        }

        public async Task CancelCommissionAsync(Guid id, string reason)
        {
            if (string.IsNullOrWhiteSpace(reason))
            {
                throw new InvalidOperationException("A reason is required to write off a commission.");
            }

            var commission = await GetCommissionAsync(id);

            if (commission.Status == CommissionStatus.Paid)
            {
                throw new InvalidOperationException("A commission that has already been paid cannot be written off.");
            }

            if (commission.Status == CommissionStatus.Cancelled)
            {
                throw new InvalidOperationException("This commission has already been written off.");
            }

            commission.Status = CommissionStatus.Cancelled;
            commission.Notes = reason.Trim();
            commission.Updated = DateTime.Now;

            await SaveCommissionAsync(commission);
        }

        public async Task<CommissionSummaryDto> GetCommissionSummaryAsync(Guid salespersonId, DateTime fromDate,
            DateTime toDate)
        {
            var salesperson = await _salespersonUnitOfWork
                .SalespersonRepository
                .GetSalespersonByIdAsync(salespersonId)
                ?? throw new InvalidOperationException("Salesperson not found.");

            GuardPeriod(fromDate, toDate);

            return await BuildSummaryAsync(salesperson, fromDate.Date, toDate.Date);
        }

        public async Task<IList<CommissionSummaryDto>> GetCommissionSummariesAsync(DateTime fromDate, DateTime toDate)
        {
            GuardPeriod(fromDate, toDate);

            var summaries = new List<CommissionSummaryDto>();

            foreach (var salesperson in await _salespersonUnitOfWork
                .SalespersonRepository
                .GetActiveSalespersonsAsync())
            {
                summaries.Add(await BuildSummaryAsync(salesperson, fromDate.Date, toDate.Date));
            }

            return summaries
                .OrderByDescending(x => x.SalesAmount)
                .ToList();
        }

        /// <summary>
        /// One salesperson's standing over a period. The sales figure comes off the
        /// posted invoices and the commission figures off the commission lines, so the
        /// two can be compared without either being derived from the other.
        /// </summary>
        private async Task<CommissionSummaryDto> BuildSummaryAsync(Salesperson salesperson, DateTime fromDate,
            DateTime toDate)
        {
            var invoices = (await _salespersonUnitOfWork
                .SalesInvoiceRepository
                .GetSalesInvoicesBySalespersonAsync(salesperson.Id))
                .Where(x => x.InvoiceDate.Date >= fromDate && x.InvoiceDate.Date <= toDate)
                .Where(x => x.Status is SalesInvoiceStatus.Posted
                    or SalesInvoiceStatus.PartiallyPaid or SalesInvoiceStatus.Paid)
                .ToList();

            var commissions = (await _salespersonUnitOfWork
                .SalesCommissionRepository
                .GetSalesCommissionsBySalespersonAsync(salesperson.Id))
                .Where(x => x.EarnedDate.Date >= fromDate && x.EarnedDate.Date <= toDate)
                .ToList();

            var pending = commissions.Where(x => x.Status == CommissionStatus.Pending).Sum(x => x.CommissionAmount);
            var approved = commissions.Where(x => x.Status == CommissionStatus.Approved).Sum(x => x.CommissionAmount);
            var paid = commissions.Where(x => x.Status == CommissionStatus.Paid).Sum(x => x.CommissionAmount);

            return new CommissionSummaryDto
            {
                SalespersonId = salesperson.Id,
                SalespersonCode = salesperson.SalespersonCode,
                SalespersonName = salesperson.SalespersonName,
                FromDate = fromDate,
                ToDate = toDate,
                InvoiceCount = invoices.Count,
                SalesAmount = invoices.Sum(x => x.GrandTotal),
                TargetAmount = salesperson.MonthlyTarget * MonthsIn(fromDate, toDate),
                PendingCommission = pending,
                ApprovedCommission = approved,
                PaidCommission = paid,
                TotalCommission = pending + approved + paid
            };
        }

        /// <summary>
        /// The target is stated per month, so a period of any other length is scaled
        /// to it rather than being compared against a single month's figure.
        /// </summary>
        private static decimal MonthsIn(DateTime fromDate, DateTime toDate)
        {
            var days = (decimal)(toDate - fromDate).TotalDays + 1m;

            return Math.Round(days / 30m, 4, MidpointRounding.AwayFromZero);
        }

        private static void GuardPeriod(DateTime fromDate, DateTime toDate)
        {
            if (toDate.Date < fromDate.Date)
            {
                throw new InvalidOperationException("The end date cannot be earlier than the start date.");
            }
        }

        private static void Validate(Salesperson salesperson)
        {
            if (string.IsNullOrWhiteSpace(salesperson.SalespersonName))
            {
                throw new InvalidOperationException("The salesperson's name is required.");
            }

            if (salesperson.CommissionRate < 0)
            {
                throw new InvalidOperationException("The commission rate cannot be negative.");
            }

            // A percentage over a hundred would pay out more than the sale was worth.
            if (salesperson.CommissionBasis == CommissionBasis.Percentage && salesperson.CommissionRate > 100)
            {
                throw new InvalidOperationException("A percentage commission cannot be more than 100.");
            }

            if (salesperson.MonthlyTarget < 0)
            {
                throw new InvalidOperationException("The monthly target cannot be negative.");
            }
        }

        private async Task<SalesCommission> GetCommissionAsync(Guid id)
        {
            return await _salespersonUnitOfWork
                .SalesCommissionRepository
                .GetSalesCommissionByIdAsync(id)
                ?? throw new InvalidOperationException("Commission not found.");
        }

        private async Task SaveCommissionAsync(SalesCommission commission)
        {
            await _salespersonUnitOfWork.SalesCommissionRepository.EditAsync(commission);
            await _salespersonUnitOfWork.SaveAsync();
        }
    }
}
