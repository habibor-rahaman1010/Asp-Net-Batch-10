namespace DevSkill.Inventory.Domain.Dtos
{
    /// <summary>
    /// One proforma invoice line seen from the delivery side: what was sold, what has
    /// already left the warehouse and what may still be shipped right now.
    /// </summary>
    public class DeliverableLineDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string UnitName { get; set; } = string.Empty;

        public decimal OrderedQuantity { get; set; }

        /// <summary>Sum of every confirmed shipment of this product.</summary>
        public decimal DeliveredQuantity { get; set; }

        /// <summary>
        /// Sum of the draft shipments of this product. They have not moved stock yet,
        /// but the quantity is already spoken for, so it cannot be promised twice.
        /// </summary>
        public decimal PendingQuantity { get; set; }

        /// <summary>Highest quantity the current delivery is allowed to carry.</summary>
        public decimal RemainingQuantity { get; set; }

        /// <summary>Physical stock that is not reserved for anything else.</summary>
        public int AvailableStock { get; set; }
    }
}
