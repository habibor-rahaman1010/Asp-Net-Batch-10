using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.RepositoryContracts;
using DevSkill.Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class CustomerPaymentAllocationRepository
        : Repository<CustomerPaymentAllocation, Guid>, ICustomerPaymentAllocationRepository
    {
        private readonly InventoryDbContext _inventoryDbContext;

        public CustomerPaymentAllocationRepository(InventoryDbContext inventoryDbContext) : base(inventoryDbContext)
        {
            _inventoryDbContext = inventoryDbContext;
        }

        public async Task<IList<CustomerPaymentAllocation>> GetAllocationsByCustomerPaymentAsync(Guid customerPaymentId)
        {
            return await _inventoryDbContext.CustomerPaymentAllocations
                .Include(x => x.SalesInvoice)
                .Where(x => x.CustomerPaymentId == customerPaymentId)
                .ToListAsync();
        }

        /// <summary>
        /// The collection is loaded with every allocation, because only a received one
        /// really settles the invoice; a draft merely reserves the amount.
        /// </summary>
        public async Task<IList<CustomerPaymentAllocation>> GetAllocationsBySalesInvoiceAsync(Guid salesInvoiceId)
        {
            return await _inventoryDbContext.CustomerPaymentAllocations
                .Include(x => x.CustomerPayment)
                .Where(x => x.SalesInvoiceId == salesInvoiceId)
                .ToListAsync();
        }

        public Task RemoveRangeAsync(IList<CustomerPaymentAllocation> allocations)
        {
            try
            {
                _inventoryDbContext.CustomerPaymentAllocations.RemoveRange(allocations);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }
    }
}
