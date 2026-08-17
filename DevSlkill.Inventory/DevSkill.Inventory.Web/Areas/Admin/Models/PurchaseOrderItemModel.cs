using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Infrastructure.RazorUtility;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    /// <summary>
    /// A purchase order line. Quantity, unit price and discount are negotiated with
    /// the supplier and so are entered here; the tax and every total are calculated
    /// again on the server when the order is saved.
    /// </summary>
    public class PurchaseOrderItemModel
    {
        public Guid ProductId { get; set; }
        public IList<SelectListItem> Products { get; set; }

        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal DiscountAmount { get; set; }

        /// <summary>
        /// Filled by the server when an existing order is shown, so the edit screen
        /// can tell what has already arrived or been billed.
        /// </summary>
        public decimal ReceivedQuantity { get; set; }
        public decimal InvoicedQuantity { get; set; }

        public PurchaseOrderItemModel()
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
