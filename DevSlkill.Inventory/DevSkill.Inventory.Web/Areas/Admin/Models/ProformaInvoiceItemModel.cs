using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Infrastructure.RazorUtility;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    /// <summary>
    /// Only <see cref="ProductId"/> and <see cref="Quantity"/> are posted back to
    /// the server. The price and total fields exist purely so the user can preview
    /// the line while typing; the server recalculates all of them.
    /// </summary>
    public class ProformaInvoiceItemModel
    {
        public Guid ProductId { get; set; }
        public IList<SelectListItem> Products { get; set; }

        public decimal Quantity { get; set; }

        public ProformaInvoiceItemModel()
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
