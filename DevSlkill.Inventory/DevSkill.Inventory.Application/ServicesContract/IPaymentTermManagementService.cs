using DevSkill.Inventory.Domain.Entities.SalesEntities;

namespace DevSkill.Inventory.Application.ServicesContract
{
    public interface IPaymentTermManagementService
    {
        public Task<IList<PaymentTerm>> GetAllPaymentTermAsync();
        public Task<IList<PaymentTerm>> GetActivePaymentTermAsync();
        public Task<PaymentTerm?> GetPaymentTermByIdAsync(Guid id);
    }
}
