namespace DevSkill.Inventory.Domain.Dtos
{
    /// <summary>
    /// One purchase order line seen from the invoicing side. Only goods that have
    /// actually been received may be billed, so the received quantity, not the
    /// ordered one, is the ceiling.
    /// </summary>
    public class BillableLineDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string UnitName { get; set; } = string.Empty;

        public decimal OrderedQuantity { get; set; }
        public decimal ReceivedQuantity { get; set; }
        public decimal InvoicedQuantity { get; set; }

        /// <summary>Highest quantity the current invoice is allowed to bill.</summary>
        public decimal RemainingQuantity { get; set; }

        public decimal UnitPrice { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TaxRate { get; set; }
    }
}
