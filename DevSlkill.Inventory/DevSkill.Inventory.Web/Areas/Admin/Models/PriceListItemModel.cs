using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Infrastructure.RazorUtility;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    public class PriceListItemModel
    {
        public Guid ProductId { get; set; }
        public IList<SelectListItem> Products { get; set; }

        public decimal UnitPrice { get; set; }
        public decimal MinQuantity { get; set; }

        public PriceListItemModel()
        {
            Products = new List<SelectListItem>();
            MinQuantity = 1;
        }

        public void SetProductValues(IList<Product> products)
        {
            Products = Utility.ConvertSelectItemToProduct(products);
        }
    }
}
