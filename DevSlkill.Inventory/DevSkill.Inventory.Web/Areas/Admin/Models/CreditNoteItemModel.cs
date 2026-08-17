using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Infrastructure.RazorUtility;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    /// <summary>
    /// One line of a hand raised credit note. The product is optional: a pure price
    /// adjustment credits money rather than goods, and carries only a description.
    /// </summary>
    public class CreditNoteItemModel
    {
        public Guid? ProductId { get; set; }
        public IList<SelectListItem> Products { get; set; }

        public string Description { get; set; } = string.Empty;

        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TaxRate { get; set; }

        public CreditNoteItemModel()
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
