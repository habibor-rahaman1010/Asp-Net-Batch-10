using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Infrastructure.RazorUtility;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    /// <summary>
    /// A requisition line. The estimated price is a guide typed by the requester,
    /// the firm price is agreed later on the purchase order.
    /// </summary>
    public class PurchaseRequisitionItemModel
    {
        public Guid ProductId { get; set; }
        public IList<SelectListItem> Products { get; set; }

        public decimal Quantity { get; set; }

        [Display(Name = "Estimated Unit Price")]
        public decimal EstimatedUnitPrice { get; set; }

        /// <summary>
        /// Filled by the server when an existing requisition is shown, so the edit
        /// screen can tell what has already gone onto a purchase order.
        /// </summary>
        public decimal OrderedQuantity { get; set; }

        [StringLength(500)]
        public string Remarks { get; set; } = string.Empty;

        public PurchaseRequisitionItemModel()
        {
            Products = new List<SelectListItem>();
            Quantity = 1;
        }

        public void SetProductValues(IList<Product> products)
        {
            Products = Utility.ConvertSelectItemToProduct(products);
        }
    }
}
