using DevSkill.Inventory.Domain.Entities;
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

            Items.Insert(0, new SelectListItem("Select A category", string.Empty));

            return Items;
        }
    }
}
