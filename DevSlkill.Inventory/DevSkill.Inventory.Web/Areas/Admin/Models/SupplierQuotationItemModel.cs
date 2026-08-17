using System.ComponentModel.DataAnnotations;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    /// <summary>
    /// One quoted line. The product and the quantity asked for come from the
    /// request; the price and the discount are what the supplier is stating.
    /// </summary>
    public class SupplierQuotationItemModel
    {
        public Guid ProductId { get; set; }

        [Display(Name = "Product")]
        public string ProductName { get; set; } = string.Empty;

        [Display(Name = "Unit")]
        public string UnitName { get; set; } = string.Empty;

        [Display(Name = "Asked For")]
        public decimal RequestedQuantity { get; set; }

        [Display(Name = "Quoted Qty")]
        public decimal Quantity { get; set; }

        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; }

        [Display(Name = "Discount")]
        public decimal DiscountAmount { get; set; }

        [StringLength(500)]
        public string Remarks { get; set; } = string.Empty;
    }
}
