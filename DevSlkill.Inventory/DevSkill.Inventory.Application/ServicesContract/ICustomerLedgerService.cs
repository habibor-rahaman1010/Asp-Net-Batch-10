using DevSkill.Inventory.Domain.Dtos;

namespace DevSkill.Inventory.Application.ServicesContract
{
    public interface ICustomerLedgerService
    {
        /// <summary>
        /// The customer account over one period. Nothing here writes, so the statement
        /// can always be re-run and has to agree with the running total kept on the
        /// customer.
        /// </summary>
        Task<CustomerLedgerDto> GetCustomerLedgerAsync(Guid customerId, DateTime fromDate, DateTime toDate);
    }
}
