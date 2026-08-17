namespace DevSkill.Inventory.Domain.Dtos
{
    /// <summary>
    /// One line of an awarded quotation, shaped the way the purchase order screen
    /// wants it so a won quote can be turned into an order without retyping.
    /// </summary>
    public class QuotationLineDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string UnitName { get; set; } = string.Empty;

        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal DiscountAmount { get; set; }
    }
}
