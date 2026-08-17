using System.ComponentModel.DataAnnotations;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    /// <summary>
    /// One invoice line. The product, unit price and tax rate all come from the
    /// purchase order and are read only; only the billed quantity and the discount
    /// are typed.
    /// </summary>
    public class PurchaseInvoiceItemModel
    {
        public Guid ProductId { get; set; }

        [Display(Name = "Product")]
        public string ProductName { get; set; } = string.Empty;

        [Display(Name = "Unit")]
        public string UnitName { get; set; } = string.Empty;

        [Display(Name = "Received")]
        public decimal ReceivedQuantity { get; set; }

        [Display(Name = "Already Billed")]
        public decimal InvoicedQuantity { get; set; }

        /// <summary>Highest quantity this invoice may bill for the product.</summary>
        [Display(Name = "Billable")]
        public decimal RemainingQuantity { get; set; }

        [Display(Name = "Billing Now")]
        public decimal Quantity { get; set; }

        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; }

        [Display(Name = "Discount")]
        public decimal DiscountAmount { get; set; }

        [Display(Name = "Tax Rate")]
        public decimal TaxRate { get; set; }
    }
}
