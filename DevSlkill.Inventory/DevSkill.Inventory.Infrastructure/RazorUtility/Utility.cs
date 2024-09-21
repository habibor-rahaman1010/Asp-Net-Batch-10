using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevSkill.Inventory.Infrastructure.RazorUtility
{
    public class Utility
    {
        public static IList<SelectListItem> ConvertCategories(IList<Category> categories)
        {
            var Items = (from c in categories
                         select new SelectListItem(c.CategoryName, c.Id.ToString()))
                          .ToList();

            Items.Insert(0, new SelectListItem("Select A Category", string.Empty));

            return Items;
        }

        public static IList<SelectListItem> ConvertProductTypes()
        {
            var items = Enum.GetValues(typeof(ProductType))
                            .Cast<ProductType>()
                            .Select(pt => new SelectListItem(pt.ToString(), ((int)pt).ToString()))
                            .ToList();

            items.Insert(0, new SelectListItem("Select A Product Type", string.Empty));
            return items;
        }
    }
}
