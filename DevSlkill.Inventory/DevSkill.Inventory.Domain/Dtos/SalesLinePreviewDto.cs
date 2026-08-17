namespace DevSkill.Inventory.Domain.Dtos
{
    /// <summary>
    /// What one sales line is worth once the server has applied the product's tax
    /// setup to the entered price. The browser only displays these numbers.
    /// </summary>
    public class SalesLinePreviewDto
    {
        public string UnitName { get; set; } = string.Empty;

        /// <summary>Physical stock on hand right now.</summary>
        public int CurrentStock { get; set; }

        /// <summary>Physical stock that is not already held for another sale.</summary>
        public int AvailableStock { get; set; }

        /// <summary>The product's own selling price, offered as a starting point.</summary>
        public decimal SellingPrice { get; set; }

        public decimal LineAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TaxRate { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal LineTotal { get; set; }
    }
}
