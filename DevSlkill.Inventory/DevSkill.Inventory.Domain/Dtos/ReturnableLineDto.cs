namespace DevSkill.Inventory.Domain.Dtos
{
    /// <summary>
    /// One goods receipt line seen from the return side: what came in, what has
    /// already gone back and what may still be sent back right now.
    /// </summary>
    public class ReturnableLineDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string UnitName { get; set; } = string.Empty;

        /// <summary>What the receipt actually brought into stock.</summary>
        public decimal ReceivedQuantity { get; set; }

        /// <summary>Sum of every confirmed return of this product on the receipt.</summary>
        public decimal ReturnedQuantity { get; set; }

        /// <summary>
        /// Sum of the draft returns of this product. They have not moved stock yet,
        /// but the quantity is already spoken for, so it cannot be sent back twice.
        /// </summary>
        public decimal PendingQuantity { get; set; }

        /// <summary>Highest quantity the current return is allowed to carry.</summary>
        public decimal RemainingQuantity { get; set; }

        public decimal UnitPrice { get; set; }

        /// <summary>Physical stock on hand right now, shown for context only.</summary>
        public int CurrentStock { get; set; }
    }
}
