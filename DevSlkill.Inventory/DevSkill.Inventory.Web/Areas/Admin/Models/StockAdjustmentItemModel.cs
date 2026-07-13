using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Infrastructure.RazorUtility;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    public class StockAdjustmentItemModel
    {
        public Guid ProductId { get; set; }

        public decimal AdjustmentQuantity { get; set; }

        public decimal UnitPrice { get; set; }

        public Guid UnitId { get; set; }

        public IList<SelectListItem> Products { get; set; } = new List<SelectListItem>();

        public IList<SelectListItem> Units { get; set; } = new List<SelectListItem>();

        public StockAdjustmentItemModel()
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