namespace DevSkill.Inventory.Domain.Dtos
{
    /// <summary>
    /// One purchase order line seen from the receiving side: what was ordered, what
    /// has already arrived and what may still be received right now.
    /// </summary>
    public class ReceivableLineDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string UnitName { get; set; } = string.Empty;

        public decimal OrderedQuantity { get; set; }

        /// <summary>Sum of every confirmed receipt of this product on the order.</summary>
        public decimal ReceivedQuantity { get; set; }

        /// <summary>
        /// Sum of the draft receipts of this product. They have not moved stock yet,
        /// but the quantity is already spoken for, so it cannot be booked twice.
        /// </summary>
        public decimal PendingQuantity { get; set; }

        /// <summary>Highest quantity the current receipt is allowed to carry.</summary>
        public decimal RemainingQuantity { get; set; }

        public decimal UnitPrice { get; set; }

        /// <summary>Physical stock on hand right now, shown for context only.</summary>
        public int CurrentStock { get; set; }
    }
}
