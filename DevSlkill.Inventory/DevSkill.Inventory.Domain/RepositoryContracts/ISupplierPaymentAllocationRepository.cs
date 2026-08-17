using DevSkill.Inventory.Domain.Entities.PurchaseEntities;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface ISupplierPaymentAllocationRepository : IRepositoryBase<SupplierPaymentAllocation, Guid>
    {
        Task<IList<SupplierPaymentAllocation>> GetAllocationsByPaymentAsync(Guid supplierPaymentId);
        Task RemoveRangeAsync(IList<SupplierPaymentAllocation> allocations);
    }
}
