using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Domain.RepositoryContracts;
using DevSkill.Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class DiscountRuleRepository : Repository<DiscountRule, Guid>, IDiscountRuleRepository
    {
        private readonly InventoryDbContext _inventoryDbContext;

        public DiscountRuleRepository(InventoryDbContext inventoryDbContext) : base(inventoryDbContext)
        {
            _inventoryDbContext = inventoryDbContext;
        }

        public async Task<(IList<DiscountRule> data, int total, int totalDisplay)> GetPagedDiscountRulesAsync(int pageIndex,
            int pageSize, DataTablesSearch search, string? order)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(search.Value))
                {
                    return await GetDynamicAsync(null, order,
                        x => x.Include(n => n.Product).Include(n => n.Customer),
                        pageIndex, pageSize, true);
                }
                else
                {
                    return await GetDynamicAsync(x => x.RuleName.Contains(search.Value), order,
                        x => x.Include(n => n.Product).Include(n => n.Customer),
                        pageIndex, pageSize, true);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<DiscountRule?> GetDiscountRuleByIdAsync(Guid id)
        {
            return await _inventoryDbContext.DiscountRules
                .Include(x => x.Product)
                .Include(x => x.Customer)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IList<DiscountRule>> GetApplicableRulesAsync(DiscountScope scope, Guid? productId,
            Guid? customerId, CustomerType customerType, DateTime onDate)
        {
            return await _inventoryDbContext.DiscountRules
                .AsNoTracking()
                .Where(x => x.IsActive
                            && x.Scope == scope
                            && x.EffectiveFrom <= onDate
                            && (x.EffectiveTo == null || x.EffectiveTo >= onDate)
                            && (x.ProductId == null || x.ProductId == productId)
                            && (x.CustomerId == null || x.CustomerId == customerId)
                            && (x.CustomerType == null || x.CustomerType == customerType))
                .OrderBy(x => x.Priority)
                .ToListAsync();
        }
    }
}
