using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.Enums;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface IDiscountRuleRepository : IRepositoryBase<DiscountRule, Guid>
    {
        Task<(IList<DiscountRule> data, int total, int totalDisplay)> GetPagedDiscountRulesAsync(int pageIndex,
            int pageSize, DataTablesSearch search, string? order);
        Task<DiscountRule?> GetDiscountRuleByIdAsync(Guid id);

        /// <summary>
        /// Every active rule that could apply to the given customer/product on the
        /// given date. Ranking and amount capping stay in the application layer.
        /// </summary>
        Task<IList<DiscountRule>> GetApplicableRulesAsync(DiscountScope scope, Guid? productId,
            Guid? customerId, CustomerType customerType, DateTime onDate);
    }
}
