using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Infrastructure.RazorUtility;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    public class ProductCreateModel
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string SKU { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
        public double Ratings { get; set; }
        public IList<SelectListItem> BarcodeTypes { get; private set; }
        public Guid BarcodeTypeId { get; set; }
        public IList<SelectListItem> Units { get; set; }
        public Guid UnitId { get; set; }
        public IList<SelectListItem> Brands { get; set; }
        public Guid BrandId { get; set; }
        public IList<SelectListItem> Subcategories { get; set; }
        public Guid SubcategoryId { get; set; }
        public IList<SelectListItem> BusinessLocations { get; set; }
        public Guid BusinessLocationId { get; set; }
        public IList<SelectListItem> Warranties { get; set; }
        public Guid WarrantyId { get; set; }    
        public double Weight { get; set; }
        public int AlertQuantity { get; set; }
        public ProductStatus Status { get; set; }
        public Guid CategoryId { get; set; }
        public IList<SelectListItem> Categories { get; private set; }
        public Guid ProductypeId { get; set; }
        public IList<SelectListItem> ProductTypes { get; private set; }
        


        public string Rack { get; set; } = string.Empty;
        public string Row { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public string IMEI { get; set; } = string.Empty;

        [Display(Name = "Store Time")]
        public DateTime StoreTime { get; set; }

        public IList<SelectListItem> ApplicableTaxes { get; set; }
        public Guid ApplicableTaxId { get; set; }
        public IList<SelectListItem> SellingPriceTaxes { get; set; }
        public Guid SellingPriceTaxId { get; set; }
        public double ExciseTax { get; set; }
        public double InclusiveTax { get; set; }
        public double MarginTax { get; set; }
        public double SellingPrice { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }


        public ProductCreateModel()
        {
            Categories = new List<SelectListItem>();
            ProductTypes = new List<SelectListItem>();
            BarcodeTypes = new List<SelectListItem>();
            Units = new List<SelectListItem>();
            Brands = new List<SelectListItem>();
            Subcategories = new List<SelectListItem>();
            BusinessLocations = new List<SelectListItem>();
            Warranties = new List<SelectListItem>();
            ApplicableTaxes = new List<SelectListItem>();
            SellingPriceTaxes = new List<SelectListItem>();
        }

        public void SetCategoriesValues(IList<Category> categories)
        {
            Categories = Utility.ConvertCategories(categories);
        }

        public void SetProductTypeValues(IList<ProductType> productTypes)
        {
            ProductTypes = Utility.ConvertProductTypes(productTypes);
        }

        public void SetBarcodeTypeValues(IList<BarcodeType> barcodeTypes)
        {
            BarcodeTypes = Utility.ConvertBarcodeTypes(barcodeTypes);
        }

        public void SetUnitValues(IList<Unit> units)
        {
            Units = Utility.ConvertUnits(units);
        }

        public void SetBrandValues(IList<Brand> brands)
        {
            Brands = Utility.ConvertBrands(brands);
        }

        public void SetSubcategoryValues(IList<Subcategory> subCategories)
        {
            Subcategories = Utility.ConvertSubCatrgory(subCategories);
        }

        public void SetBusinessLocationValues(IList<BusinessLocation> businessLocations)
        {
            BusinessLocations = Utility.ConvertBusinessLocations(businessLocations);
        }

        public void SetWarrantyValues(IList<Warranty> warranties)
        {
            Warranties = Utility.ConvertWarranties(warranties);
        }

        public void SetApplicableTaxValues(IList<ApplicableTax> applicableTaxes)
        {
            ApplicableTaxes = Utility.ConvertApplicableTaxes(applicableTaxes);
        }

        public void SetSellingPriceTaxValues(IList<SellingPriceTax> sellingPriceTaxes)
        {
            SellingPriceTaxes = Utility.ConvertSellingPriceTaxes(sellingPriceTaxes);
        }
    }
}
