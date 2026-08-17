namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    /// <summary>
    /// One invoice line seen from the return side. Everything but the quantity coming
    /// back is read only, because it all comes off the invoice; that is what makes
    /// the credit worth exactly what was billed.
    /// </summary>
    public class SalesReturnItemModel
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string UnitName { get; set; } = string.Empty;

        public decimal InvoicedQuantity { get; set; }

        /// <summary>Already returned on other confirmed returns of the same invoice.</summary>
        public decimal AlreadyReturnedQuantity { get; set; }

        /// <summary>Claimed by draft returns, spoken for but not yet back on the shelf.</summary>
        public decimal PendingQuantity { get; set; }

        /// <summary>The most this return may carry on the line.</summary>
        public decimal RemainingQuantity { get; set; }

        public decimal UnitPrice { get; set; }
        public decimal TaxRate { get; set; }

        /// <summary>What is coming back now. This is the only field the user types.</summary>
        public decimal ReturnQuantity { get; set; }

        public string Remarks { get; set; } = string.Empty;
    }
}
