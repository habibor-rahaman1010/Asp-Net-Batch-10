using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.Enums;

namespace DevSkill.Inventory.Application.ServicesContract
{
    public interface IDiscountRuleManagementService
    {
        Task CreateDiscountRuleAsync(DiscountRule discountRule);
        Task UpdateDiscountRuleAsync(DiscountRule discountRule);
        Task DeleteDiscountRuleAsync(Guid id);
        Task<DiscountRule?> GetDiscountRuleByIdAsync(Guid id);
        Task<(IList<DiscountRule> data, int total, int totalDisplay)> GetDiscountRulesAsync(int pageIndex,
            int pageSize, DataTablesSearch search, string? order);

        /// <summary>
        /// Discount amount allowed on a single order line. Always resolved on the
        /// server so a manipulated browser value can never be honoured.
        /// </summary>
        Task<decimal> CalculateLineDiscountAsync(Guid productId, Guid? customerId,
            CustomerType customerType, decimal quantity, decimal lineAmount, DateTime onDate);

        /// <summary>
        /// Discount amount allowed on the order total.
        /// </summary>
        Task<decimal> CalculateOrderDiscountAsync(Guid? customerId, CustomerType customerType,
            decimal orderAmount, DateTime onDate);
    }
}
