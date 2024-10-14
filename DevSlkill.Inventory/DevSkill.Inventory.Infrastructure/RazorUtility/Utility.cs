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

        public static IList<SelectListItem> ConvertUnits(IList<Unit> units)
        {
            var Items = (from c in units
                         select new SelectListItem(c.UnitName, c.Id.ToString()))
                         .ToList();

            Items.Insert(0, new SelectListItem("Select", string.Empty));

            return Items;
        }

        public static IList<SelectListItem> ConvertBrands(IList<Brand> brands)
        {
            var Items = (from c in brands
                         select new SelectListItem(c.BrandName, c.Id.ToString()))
                         .ToList();

            Items.Insert(0, new SelectListItem("Select", string.Empty));

            return Items;
        }
        

        public static IList<SelectListItem> ConvertBusinessLocations(IList<BusinessLocation> businessLocations)
        {
            var Items = (from c in businessLocations
                         select new SelectListItem(c.LocationName, c.Id.ToString()))
                         .ToList();

            Items.Insert(0, new SelectListItem("Select", string.Empty));

            return Items;
        }

        public static IList<SelectListItem> ConvertWarranties(IList<Warranty> warranties)
        {
            var Items = (from c in warranties
                         select new SelectListItem(c.WarrantyDuration, c.Id.ToString()))
                         .ToList();

            Items.Insert(0, new SelectListItem("Select", string.Empty));

            return Items;
        }

        public static IList<SelectListItem> ConvertApplicableTaxes(IList<ApplicableTax> applicableTaxes)
        {
            var Items = (from c in applicableTaxes
                         select new SelectListItem(c.ApplicableTaxName, c.Id.ToString()))
                         .ToList();

            Items.Insert(0, new SelectListItem("Select", string.Empty));

            return Items;
        }

        public static IList<SelectListItem> ConvertSellingPriceTaxes(IList<SellingPriceTax> sellingPriceTaxes)
        {
            var Items = (from c in sellingPriceTaxes
                         select new SelectListItem(c.SellingPriceTaxName, c.Id.ToString()))
                         .ToList();

            Items.Insert(0, new SelectListItem("Select", string.Empty));

            return Items;
        }

        public static IList<SelectListItem> ConvertEnumToSelectList<TEnum>() where TEnum : Enum
        {
            var items = Enum.GetValues(typeof(TEnum))
                .Cast<TEnum>()
                .Select(e => new SelectListItem
                {
                    Value = Convert.ToInt32(e).ToString(),
                    Text = e.ToString()
                })
                .ToList();

            return items;
        }

    }
}
