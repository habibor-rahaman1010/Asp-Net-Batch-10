using System.ComponentModel.DataAnnotations;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    /// <summary>
    /// One return line. The product, the received quantity and the unit price all
    /// come from the goods receipt and are shown read only; only what is going back
    /// is typed.
    /// </summary>
    public class PurchaseReturnItemModel
    {
        public Guid ProductId { get; set; }

        [Display(Name = "Product")]
        public string ProductName { get; set; } = string.Empty;

        [Display(Name = "Unit")]
        public string UnitName { get; set; } = string.Empty;

        [Display(Name = "Received")]
        public decimal ReceivedQuantity { get; set; }

        [Display(Name = "Already Returned")]
        public decimal AlreadyReturnedQuantity { get; set; }

        /// <summary>Highest quantity this return may carry for the product.</summary>
        [Display(Name = "Returnable")]
        public decimal RemainingQuantity { get; set; }

        [Display(Name = "Returning Now")]
        public decimal ReturnQuantity { get; set; }

        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; }

        [Display(Name = "In Stock")]
        public int CurrentStock { get; set; }

        [StringLength(500)]
        public string Remarks { get; set; } = string.Empty;
    }
}
