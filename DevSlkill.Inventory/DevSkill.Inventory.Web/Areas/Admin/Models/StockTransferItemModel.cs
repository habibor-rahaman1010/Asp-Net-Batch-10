using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Infrastructure.RazorUtility;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    public class StockTransferItemModel
    {
        public Guid ProductId { get; set; }
        public IList<SelectListItem> Products { get; set; }
        public Guid UnitId { get; set; }
        public IList<SelectListItem> Units { get; set; }
        public decimal Quantity { get; set; }

        public StockTransferItemModel()
        {
            Products = new List<SelectListItem>();
            Units = new List<SelectListItem>();
        }

        public void SetUnitValues(IList<Unit> units)
        {
            Units = Utility.ConvertUnits(units);
        }

        public void SetProductValues(IList<Product> products)
        {
            Products = Utility.ConvertSelectItemToProduct(products);
        }
    }
}