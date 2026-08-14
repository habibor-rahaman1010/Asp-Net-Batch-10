namespace DevSkill.Inventory.Domain.Entities.SalesEntities
{
    public class PriceListItem : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public Guid PriceListId { get; set; }
        public virtual PriceList? PriceList { get; set; }

        public Guid ProductId { get; set; }
        public virtual Product? Product { get; set; }

        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Smallest quantity this price applies from, so one product can carry
        /// slab pricing inside the same price list.
        /// </summary>
        public decimal MinQuantity { get; set; }
    }
}
