namespace DevSkill.Inventory.Domain.Entities.SalesEntities
{
    public class DeliveryItem : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public Guid DeliveryId { get; set; }
        public virtual Delivery? Delivery { get; set; }

        public Guid ProductId { get; set; }
        public virtual Product? Product { get; set; }

        /// <summary>
        /// Quantity the matching proforma invoice line asked for, copied when the
        /// delivery is written so the printed document keeps telling the same story
        /// even if the proforma is corrected later.
        /// </summary>
        public decimal OrderedQuantity { get; set; }

        /// <summary>
        /// Quantity handed over in this shipment only. What is still owed to the
        /// customer is ordered minus the sum of this column over every delivery.
        /// </summary>
        public decimal DeliveredQuantity { get; set; }
    }
}
