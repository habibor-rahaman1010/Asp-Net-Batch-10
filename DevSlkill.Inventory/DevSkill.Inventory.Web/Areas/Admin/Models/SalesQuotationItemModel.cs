using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Infrastructure.RazorUtility;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    /// <summary>
    /// A quotation line. Quantity is always typed; a unit price or discount left at
    /// zero is filled in by the customer's price list and the discount rules, and
    /// anything typed in wins. Tax and every total come back from the server.
    /// </summary>
    public class SalesQuotationItemModel
    {
        /// <summary>
        /// Nullable on purpose. A row whose product has not been picked yet posts an
        /// empty value, and a non nullable Guid would turn that into a binding error
        /// that silently refuses the whole save. An empty row is simply dropped.
        /// </summary>
        public Guid? ProductId { get; set; }
        public IList<SelectListItem> Products { get; set; }

        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal DiscountAmount { get; set; }

        /// <summary>
        /// Filled by the server when an existing quotation is shown, so the edit
        /// screen can tell how much of the line has already been ordered.
        /// </summary>
        public decimal OrderedQuantity { get; set; }

        public string Remarks { get; set; } = string.Empty;

        public SalesQuotationItemModel()
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
