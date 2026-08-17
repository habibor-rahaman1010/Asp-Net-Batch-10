using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Infrastructure.RazorUtility;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    /// <summary>
    /// A sales order line. As on the quotation, a unit price or discount left at zero
    /// falls back to the price list and the discount rules, and anything typed in
    /// wins. Tax and every total come back from the server.
    /// </summary>
    public class SalesOrderItemModel
    {
        public Guid ProductId { get; set; }
        public IList<SelectListItem> Products { get; set; }

        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal DiscountAmount { get; set; }

        /// <summary>
        /// Filled by the server when an existing order is shown, so the edit screen
        /// can tell what has already shipped or been billed.
        /// </summary>
        public decimal DeliveredQuantity { get; set; }
        public decimal InvoicedQuantity { get; set; }

        public SalesOrderItemModel()
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
