using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Domain.UnitOfWorkContracts;

namespace DevSkill.Inventory.Application.Services
{
    public class DiscountRuleManagementService : IDiscountRuleManagementService
    {
        private readonly IInventoryUnitOfWork _discountUnitOfWork;

        public DiscountRuleManagementService(IInventoryUnitOfWork unitOfWork)
        {
            _discountUnitOfWork = unitOfWork;
        }

        public async Task CreateDiscountRuleAsync(DiscountRule discountRule)
        {
            ValidateRule(discountRule);

            discountRule.Created = DateTime.Now;
            discountRule.Updated = DateTime.Now;

            await _discountUnitOfWork.DiscountRuleRepository.AddAsync(discountRule);
            await _discountUnitOfWork.SaveAsync();
        }

        public async Task UpdateDiscountRuleAsync(DiscountRule discountRule)
        {
            ValidateRule(discountRule);

            var existingRule = await _discountUnitOfWork.DiscountRuleRepository.GetByIdAsync(discountRule.Id)
                ?? throw new InvalidOperationException("Discount rule not found.");

            existingRule.RuleName = discountRule.RuleName;
            existingRule.Scope = discountRule.Scope;
            existingRule.DiscountType = discountRule.DiscountType;
            existingRule.DiscountValue = discountRule.DiscountValue;
            existingRule.ProductId = discountRule.ProductId;
            existingRule.CustomerId = discountRule.CustomerId;
            existingRule.CustomerType = discountRule.CustomerType;
            existingRule.MinQuantity = discountRule.MinQuantity;
            existingRule.MinOrderAmount = discountRule.MinOrderAmount;
            existingRule.MaxDiscountAmount = discountRule.MaxDiscountAmount;
            existingRule.EffectiveFrom = discountRule.EffectiveFrom;
            existingRule.EffectiveTo = discountRule.EffectiveTo;
            existingRule.IsActive = discountRule.IsActive;
            existingRule.Priority = discountRule.Priority;
            existingRule.Updated = DateTime.Now;

            await _discountUnitOfWork.DiscountRuleRepository.EditAsync(existingRule);
            await _discountUnitOfWork.SaveAsync();
        }

        public async Task DeleteDiscountRuleAsync(Guid id)
        {
            await _discountUnitOfWork.DiscountRuleRepository.RemoveAsync(id);
            await _discountUnitOfWork.SaveAsync();
        }

        public async Task<DiscountRule?> GetDiscountRuleByIdAsync(Guid id)
        {
            return await _discountUnitOfWork.DiscountRuleRepository.GetDiscountRuleByIdAsync(id);
        }

        public async Task<(IList<DiscountRule> data, int total, int totalDisplay)> GetDiscountRulesAsync(int pageIndex,
            int pageSize, DataTablesSearch search, string? order)
        {
            return await _discountUnitOfWork.DiscountRuleRepository.GetPagedDiscountRulesAsync(pageIndex, pageSize, search, order);
        }

        public async Task<decimal> CalculateLineDiscountAsync(Guid productId, Guid? customerId,
            CustomerType customerType, decimal quantity, decimal lineAmount, DateTime onDate)
        {
            if (lineAmount <= 0 || quantity <= 0)
            {
                return 0m;
            }

            // Product level and quantity level rules both act on a single line.
            var candidates = new List<DiscountRule>();

            candidates.AddRange(await _discountUnitOfWork.DiscountRuleRepository
                .GetApplicableRulesAsync(DiscountScope.Product, productId, customerId, customerType, onDate));

            candidates.AddRange(await _discountUnitOfWork.DiscountRuleRepository
                .GetApplicableRulesAsync(DiscountScope.Quantity, productId, customerId, customerType, onDate));

            var rule = candidates
                .Where(x => quantity >= x.MinQuantity && lineAmount >= x.MinOrderAmount)
                .OrderBy(x => x.Priority)
                .ThenByDescending(x => x.MinQuantity)
                .FirstOrDefault();

            return ResolveAmount(rule, lineAmount);
        }

        public async Task<decimal> CalculateOrderDiscountAsync(Guid? customerId, CustomerType customerType,
            decimal orderAmount, DateTime onDate)
        {
            if (orderAmount <= 0)
            {
                return 0m;
            }

            var candidates = new List<DiscountRule>();

            candidates.AddRange(await _discountUnitOfWork.DiscountRuleRepository
                .GetApplicableRulesAsync(DiscountScope.Order, null, customerId, customerType, onDate));

            candidates.AddRange(await _discountUnitOfWork.DiscountRuleRepository
                .GetApplicableRulesAsync(DiscountScope.Customer, null, customerId, customerType, onDate));

            var rule = candidates
                .Where(x => orderAmount >= x.MinOrderAmount)
                .OrderBy(x => x.Priority)
                .ThenByDescending(x => x.MinOrderAmount)
                .FirstOrDefault();

            return ResolveAmount(rule, orderAmount);
        }

        private static decimal ResolveAmount(DiscountRule? rule, decimal baseAmount)
        {
            if (rule == null)
            {
                return 0m;
            }

            var discount = rule.DiscountType == DiscountType.Percentage
                ? Math.Round(baseAmount * rule.DiscountValue / 100m, 2, MidpointRounding.AwayFromZero)
                : rule.DiscountValue;

            if (rule.MaxDiscountAmount > 0 && discount > rule.MaxDiscountAmount)
            {
                discount = rule.MaxDiscountAmount;
            }

            // A discount can never exceed what is being discounted.
            return Math.Clamp(discount, 0m, baseAmount);
        }

        private static void ValidateRule(DiscountRule discountRule)
        {
            if (discountRule.DiscountValue < 0)
            {
                throw new InvalidOperationException("Discount value cannot be negative.");
            }

            if (discountRule.DiscountType == DiscountType.Percentage && discountRule.DiscountValue > 100)
            {
                throw new InvalidOperationException("Percentage discount cannot be more than 100.");
            }

            if (discountRule.EffectiveTo.HasValue && discountRule.EffectiveTo < discountRule.EffectiveFrom)
            {
                throw new InvalidOperationException("Effective To cannot be earlier than Effective From.");
            }

            if (discountRule.Scope == DiscountScope.Product && discountRule.ProductId == null)
            {
                throw new InvalidOperationException("A product level discount needs a product.");
            }

            if (discountRule.Scope == DiscountScope.Customer && discountRule.CustomerId == null)
            {
                throw new InvalidOperationException("A customer specific discount needs a customer.");
            }
        }
    }
}
