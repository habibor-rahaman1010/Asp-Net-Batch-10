namespace DevSkill.Inventory.Domain.Dtos
{
    /// <summary>
    /// One accepted quotation line seen from the sales order side: what was offered,
    /// what has already been ordered and what may still be turned into an order.
    /// </summary>
    public class ConvertibleQuotationLineDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string UnitName { get; set; } = string.Empty;

        public decimal QuotedQuantity { get; set; }
        public decimal OrderedQuantity { get; set; }

        /// <summary>Highest quantity a new sales order is allowed to take from this line.</summary>
        public decimal RemainingQuantity { get; set; }

        public decimal UnitPrice { get; set; }
        public decimal DiscountAmount { get; set; }
    }
}
