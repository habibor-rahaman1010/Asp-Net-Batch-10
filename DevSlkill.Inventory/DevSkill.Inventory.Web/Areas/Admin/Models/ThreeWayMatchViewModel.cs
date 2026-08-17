using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using DevSkill.Inventory.Infrastructure.RazorUtility;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    /// <summary>
    /// Backs the screen that lays a purchase order, its goods receipts and its
    /// purchase invoices side by side.
    /// </summary>
    public class ThreeWayMatchViewModel
    {
        [Display(Name = "Purchase Order")]
        public Guid PurchaseOrderId { get; set; }
        public IList<SelectListItem> PurchaseOrders { get; set; }

        /// <summary>Null until an order is chosen.</summary>
        public ThreeWayMatchDto? Match { get; set; }

        public ThreeWayMatchViewModel()
        {
            PurchaseOrders = new List<SelectListItem>();
        }

        public void SetPurchaseOrderValues(IList<PurchaseOrder> purchaseOrders)
        {
            PurchaseOrders = Utility.ConvertPurchaseOrders(purchaseOrders);
        }
    }
}
