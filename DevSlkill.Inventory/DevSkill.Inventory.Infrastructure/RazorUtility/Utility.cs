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

            Items.Insert(0, new SelectListItem("Select", string.Empty));

            return Items;
        }

        public static IList<SelectListItem> ConvertProductTypes(IList<ProductType> productTypes)
        {
            var Items = (from c in productTypes
                         select new SelectListItem(c.ProductTypeName, c.Id.ToString()))
                         .ToList();

            Items.Insert(0, new SelectListItem("Select", string.Empty));

            return Items;
        }

        public static IList<SelectListItem> ConvertSubCatrgory(IList<Subcategory> subcategories)
        {
            var Items = (from c in subcategories
                         select new SelectListItem(c.SubcategoryName, c.Id.ToString()))
                         .ToList();

            Items.Insert(0, new SelectListItem("Select", string.Empty));

            return Items;
        }

        public static IList<SelectListItem> ConvertBarcodeTypes(IList<BarcodeType> productTypes)
        {
            var Items = (from c in productTypes
                         select new SelectListItem(c.BarcodeTypeName, c.Id.ToString()))
                         .ToList();

            Items.Insert(0, new SelectListItem("Select", string.Empty));

            return Items;
        }
    }
}
