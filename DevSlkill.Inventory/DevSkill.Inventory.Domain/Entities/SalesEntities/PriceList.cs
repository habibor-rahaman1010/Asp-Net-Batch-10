using DevSkill.Inventory.Domain.Enums;

namespace DevSkill.Inventory.Domain.Entities.SalesEntities
{
    /// <summary>
    /// Product -> Price List -> Customer Type -> Selling Price.
    /// One price list serves one customer type for a given effective period.
    /// </summary>
    public class PriceList : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string PriceListName { get; set; } = string.Empty;

        public CustomerType CustomerType { get; set; }

        public DateTime EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }

        public bool IsActive { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public virtual ICollection<PriceListItem>? PriceListItems { get; set; }
    }
}
