using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Domain.RepositoryContracts;
using DevSkill.Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class CustomerPaymentRepository : Repository<CustomerPayment, Guid>, ICustomerPaymentRepository
    {
        private readonly InventoryDbContext _inventoryDbContext;

        public CustomerPaymentRepository(InventoryDbContext inventoryDbContext) : base(inventoryDbContext)
        {
            _inventoryDbContext = inventoryDbContext;
        }

        public async Task<(IList<CustomerPayment> data, int total, int totalDisplay)> GetPagedCustomerPaymentsAsync(
            int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(search.Value))
                {
                    return await GetDynamicAsync(null, order,
                        x => x.Include(n => n.Customer),
                        pageIndex, pageSize, true);
                }
                else
                {
                    return await GetDynamicAsync(
                        x => x.PaymentNo.Contains(search.Value) ||
                             x.ReferenceNo.Contains(search.Value) ||
                             x.Customer!.CustomerName.Contains(search.Value) ||
                             x.Customer!.CustomerCode.Contains(search.Value),
                        order,
                        x => x.Include(n => n.Customer),
                        pageIndex, pageSize, true);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<CustomerPayment?> GetCustomerPaymentByIdAsync(Guid id)
        {
            return await _inventoryDbContext.CustomerPayments
                .Include(x => x.Customer)
                .Include(x => x.CreditNote)
                .Include(x => x.CustomerPaymentAllocations!)
                    .ThenInclude(x => x.SalesInvoice)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IList<CustomerPayment>> GetCustomerPaymentsByCustomerAsync(Guid customerId)
        {
            return await _inventoryDbContext.CustomerPayments
                .Include(x => x.CustomerPaymentAllocations)
                .Where(x => x.CustomerId == customerId)
                .OrderByDescending(x => x.PaymentDate)
                .ToListAsync();
        }

        public async Task<IList<CustomerPayment>> GetCustomerPaymentsByStatusAsync(
            params CustomerPaymentStatus[] statuses)
        {
            return await _inventoryDbContext.CustomerPayments
                .Include(x => x.Customer)
                .Include(x => x.CustomerPaymentAllocations)
                .Where(x => statuses.Contains(x.Status))
                .OrderByDescending(x => x.PaymentDate)
                .ToListAsync();
        }

        public async Task<IList<CustomerPayment>> GetCustomerPaymentsByCreditNoteAsync(Guid creditNoteId)
        {
            return await _inventoryDbContext.CustomerPayments
                .Include(x => x.CustomerPaymentAllocations)
                .Where(x => x.CreditNoteId == creditNoteId)
                .ToListAsync();
        }

        public async Task<IList<CustomerPayment>> GetCustomerPaymentsByDateRangeAsync(DateTime fromDate, DateTime toDate)
        {
            return await _inventoryDbContext.CustomerPayments
                .Include(x => x.Customer)
                .Include(x => x.CustomerPaymentAllocations)
                .Where(x => x.PaymentDate >= fromDate && x.PaymentDate <= toDate)
                .OrderBy(x => x.PaymentDate)
                .ToListAsync();
        }

        public async Task<bool> IsPaymentNoDuplicateAsync(string paymentNo)
        {
            return await GetCountAsync(x => x.PaymentNo == paymentNo) > 0;
        }
    }
}
