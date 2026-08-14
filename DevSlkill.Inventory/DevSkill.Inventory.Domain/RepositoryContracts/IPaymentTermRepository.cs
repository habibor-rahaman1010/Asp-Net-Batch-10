using DevSkill.Inventory.Domain.Entities.SalesEntities;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface IPaymentTermRepository : IRepositoryBase<PaymentTerm, Guid>
    {
        Task<IList<PaymentTerm>> GetActivePaymentTermsAsync();
    }
}
