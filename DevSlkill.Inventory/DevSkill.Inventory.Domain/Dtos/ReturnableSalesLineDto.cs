namespace DevSkill.Inventory.Domain.Dtos
{
    /// <summary>
    /// One sales invoice line seen from the return side: what was billed, what has
    /// already come back and what may still be sent back right now.
    /// </summary>
    public class ReturnableSalesLineDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string UnitName { get; set; } = string.Empty;

        /// <summary>What the invoice actually billed the customer for.</summary>
        public decimal InvoicedQuantity { get; set; }

        /// <summary>Sum of every confirmed return of this product on the invoice.</summary>
        public decimal ReturnedQuantity { get; set; }

        /// <summary>
        /// Sum of the draft returns of this product. They have not moved stock yet,
        /// but the quantity is already spoken for, so it cannot come back twice.
        /// </summary>
        public decimal PendingQuantity { get; set; }

        /// <summary>Highest quantity the current return is allowed to carry.</summary>
        public decimal RemainingQuantity { get; set; }

        public decimal UnitPrice { get; set; }
        public decimal TaxRate { get; set; }
    }
}
