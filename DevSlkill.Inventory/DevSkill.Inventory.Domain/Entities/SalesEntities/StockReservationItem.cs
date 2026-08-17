namespace DevSkill.Inventory.Domain.Entities.SalesEntities
{
    public class StockReservationItem : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public Guid StockReservationId { get; set; }
        public virtual StockReservation? StockReservation { get; set; }

        public Guid ProductId { get; set; }
        public virtual Product? Product { get; set; }

        /// <summary>
        /// What the matching sales order line asked for, copied when the reservation
        /// is written so the document keeps telling the same story later.
        /// </summary>
        public decimal OrderedQuantity { get; set; }

        /// <summary>Quantity held back for this order only.</summary>
        public decimal ReservedQuantity { get; set; }

        /// <summary>
        /// How much of the hold has already gone out on a delivery. The hold is over
        /// once this reaches the reserved quantity.
        /// </summary>
        public decimal ConsumedQuantity { get; set; }

        public string Remarks { get; set; } = string.Empty;
    }
}
