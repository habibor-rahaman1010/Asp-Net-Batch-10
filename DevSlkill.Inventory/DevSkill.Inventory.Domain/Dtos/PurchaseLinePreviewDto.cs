namespace DevSkill.Inventory.Domain.Dtos
{
    /// <summary>
    /// What one purchase line is worth once the server has applied the product's tax
    /// setup to the negotiated price. The browser only displays these numbers.
    /// </summary>
    public class PurchaseLinePreviewDto
    {
        public string UnitName { get; set; } = string.Empty;
        public int CurrentStock { get; set; }

        public decimal LineAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TaxRate { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal LineTotal { get; set; }
    }
}
