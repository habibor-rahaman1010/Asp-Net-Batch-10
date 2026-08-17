using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using DevSkill.Inventory.Domain.RepositoryContracts;
using DevSkill.Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class SupplierPaymentAllocationRepository
        : Repository<SupplierPaymentAllocation, Guid>, ISupplierPaymentAllocationRepository
    {
        private readonly InventoryDbContext _inventoryDbContext;

        public SupplierPaymentAllocationRepository(InventoryDbContext inventoryDbContext) : base(inventoryDbContext)
        {
            _inventoryDbContext = inventoryDbContext;
        }

        public async Task<IList<SupplierPaymentAllocation>> GetAllocationsByPaymentAsync(Guid supplierPaymentId)
        {
            return await _inventoryDbContext.SupplierPaymentAllocations
                .Include(x => x.PurchaseInvoice)
                .Where(x => x.SupplierPaymentId == supplierPaymentId)
                .ToListAsync();
        }

        public Task RemoveRangeAsync(IList<SupplierPaymentAllocation> allocations)
        {
            try
            {
                _inventoryDbContext.SupplierPaymentAllocations.RemoveRange(allocations);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }
    }
}
