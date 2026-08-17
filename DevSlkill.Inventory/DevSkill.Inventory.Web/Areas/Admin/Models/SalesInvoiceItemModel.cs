namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    /// <summary>
    /// One sales order line seen from the invoicing side. Only the billed quantity is
    /// typed; the price, the discount and the tax all come off the order, so the
    /// invoice can never charge a price that was never agreed.
    /// </summary>
    public class SalesInvoiceItemModel
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string UnitName { get; set; } = string.Empty;

        public decimal OrderedQuantity { get; set; }
        public decimal DeliveredQuantity { get; set; }

        /// <summary>Already billed on other invoices of the same order.</summary>
        public decimal AlreadyInvoicedQuantity { get; set; }

        /// <summary>The most this invoice may bill on the line.</summary>
        public decimal RemainingQuantity { get; set; }

        public decimal UnitPrice { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TaxRate { get; set; }

        /// <summary>What is being billed now. This is the only field the user types.</summary>
        public decimal Quantity { get; set; }
    }
}
