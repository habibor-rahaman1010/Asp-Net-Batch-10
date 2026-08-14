using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.UnitOfWorkContracts;

namespace DevSkill.Inventory.Application.Services
{
    public class PaymentTermManagementService : IPaymentTermManagementService
    {
        private readonly IInventoryUnitOfWork _paymentTermUnitOfWork;

        public PaymentTermManagementService(IInventoryUnitOfWork unitOfWork)
        {
            _paymentTermUnitOfWork = unitOfWork;
        }

        public async Task<IList<PaymentTerm>> GetAllPaymentTermAsync()
        {
            return await _paymentTermUnitOfWork.PaymentTermRepository.GetAllAsync();
        }

        public async Task<IList<PaymentTerm>> GetActivePaymentTermAsync()
        {
            return await _paymentTermUnitOfWork.PaymentTermRepository.GetActivePaymentTermsAsync();
        }

        public async Task<PaymentTerm?> GetPaymentTermByIdAsync(Guid id)
        {
            return await _paymentTermUnitOfWork.PaymentTermRepository.GetByIdAsync(id);
        }
    }
}
