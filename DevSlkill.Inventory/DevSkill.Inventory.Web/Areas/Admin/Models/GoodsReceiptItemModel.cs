using System.ComponentModel.DataAnnotations;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    /// <summary>
    /// One receiving line. The product and the ordered quantity come from the
    /// purchase order and are shown read only; only what actually arrived is typed.
    /// </summary>
    public class GoodsReceiptItemModel
    {
        public Guid ProductId { get; set; }

        [Display(Name = "Product")]
        public string ProductName { get; set; } = string.Empty;

        [Display(Name = "Unit")]
        public string UnitName { get; set; } = string.Empty;

        [Display(Name = "Ordered")]
        public decimal OrderedQuantity { get; set; }

        [Display(Name = "Already Received")]
        public decimal AlreadyReceivedQuantity { get; set; }

        /// <summary>Highest quantity this receipt may carry for the product.</summary>
        [Display(Name = "Remaining")]
        public decimal RemainingQuantity { get; set; }

        [Display(Name = "Receiving Now")]
        public decimal ReceivedQuantity { get; set; }

        [Display(Name = "Rejected")]
        public decimal RejectedQuantity { get; set; }

        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; }

        [Display(Name = "In Stock")]
        public int CurrentStock { get; set; }

        [StringLength(500)]
        public string Remarks { get; set; } = string.Empty;
    }
}
