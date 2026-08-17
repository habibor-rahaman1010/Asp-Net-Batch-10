using DevSkill.Inventory.Domain.Entities.SalesEntities;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface ICustomerPaymentAllocationRepository : IRepositoryBase<CustomerPaymentAllocation, Guid>
    {
        Task<IList<CustomerPaymentAllocation>> GetAllocationsByCustomerPaymentAsync(Guid customerPaymentId);

        /// <summary>
        /// Every allocation pointing at one invoice, with its collection loaded, so
        /// the invoice can tell settled money from money that is only pencilled in.
        /// </summary>
        Task<IList<CustomerPaymentAllocation>> GetAllocationsBySalesInvoiceAsync(Guid salesInvoiceId);

        Task RemoveRangeAsync(IList<CustomerPaymentAllocation> allocations);
    }
}
