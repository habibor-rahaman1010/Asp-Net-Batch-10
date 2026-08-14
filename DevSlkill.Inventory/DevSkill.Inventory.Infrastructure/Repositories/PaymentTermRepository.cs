using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.RepositoryContracts;
using DevSkill.Inventory.Infrastructure.Data;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class PaymentTermRepository : Repository<PaymentTerm, Guid>, IPaymentTermRepository
    {
        public PaymentTermRepository(InventoryDbContext inventoryDbContext) : base(inventoryDbContext)
        {
        }

        public async Task<IList<PaymentTerm>> GetActivePaymentTermsAsync()
        {
            return await GetAsync(x => x.IsActive,
                y => y.OrderBy(z => z.DueDays).ThenBy(z => z.TermName), null, true);
        }
    }
}
