namespace DevSkill.Inventory.Domain.Dtos
{
    /// <summary>
    /// One sales order line seen from the reservation side: what was sold, what is
    /// already being held for it and how much free stock is left to hold.
    /// </summary>
    public class ReservableLineDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string UnitName { get; set; } = string.Empty;

        public decimal OrderedQuantity { get; set; }

        /// <summary>Sum of every active reservation of this product on the order.</summary>
        public decimal ReservedQuantity { get; set; }

        /// <summary>Sum of the draft reservations, already spoken for but not yet held.</summary>
        public decimal PendingQuantity { get; set; }

        /// <summary>What has already shipped, and so no longer needs holding.</summary>
        public decimal DeliveredQuantity { get; set; }

        /// <summary>Highest quantity the current reservation is allowed to hold.</summary>
        public decimal RemainingQuantity { get; set; }

        /// <summary>Physical stock on hand right now.</summary>
        public int CurrentStock { get; set; }

        /// <summary>Physical stock that is not held for anything else.</summary>
        public int AvailableStock { get; set; }
    }
}
