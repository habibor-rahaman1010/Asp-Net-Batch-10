using DevSkill.Inventory.Domain.Enums;

namespace DevSkill.Inventory.Domain.Entities.SalesEntities
{
    /// <summary>
    /// Covers product level, order level, customer specific and quantity based
    /// discounts through a single rule table. The scope decides which of the
    /// optional filters below are used while resolving a discount.
    /// </summary>
    public class DiscountRule : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string RuleName { get; set; } = string.Empty;

        public DiscountScope Scope { get; set; }
        public DiscountType DiscountType { get; set; }
        public decimal DiscountValue { get; set; }

        public Guid? ProductId { get; set; }
        public virtual Product? Product { get; set; }

        public Guid? CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }

        public CustomerType? CustomerType { get; set; }

        public decimal MinQuantity { get; set; }
        public decimal MinOrderAmount { get; set; }

        /// <summary>
        /// Upper cap of the money that this rule may take off. Zero means no cap.
        /// </summary>
        public decimal MaxDiscountAmount { get; set; }

        public DateTime EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }

        public bool IsActive { get; set; }

        /// <summary>
        /// Lower number wins when more than one rule matches.
        /// </summary>
        public int Priority { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
