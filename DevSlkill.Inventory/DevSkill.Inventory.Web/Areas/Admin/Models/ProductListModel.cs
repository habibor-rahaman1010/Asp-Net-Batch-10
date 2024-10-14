using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Infrastructure.RazorUtility;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    public class ProductListModel : DataTables
    {
        public ProductSearchDto SearchItem { get; set; }
        public IList<SelectListItem> Categories { get; private set; }
        public IList<SelectListItem> ProductTypes { get; private set; }
        public IList<SelectListItem> Brands { get; private set; }
        public IList<SelectListItem> Units { get; private set; }
        public IList<SelectListItem> BusinessLocations { get; private set; }
        public IList<SelectListItem> SellingPriceTaxes { get; private set; }
        public IList<SelectListItem> ApplicableTaxes { get; private set; }
        public IList<SelectListItem> Statuses{ get; private set; }

        public ProductListModel() 
        { 
            SearchItem = new ProductSearchDto();
            Categories = new List<SelectListItem>();
            ProductTypes = new List<SelectListItem>();
            Brands = new List<SelectListItem>();
            Units = new List<SelectListItem>();
            BusinessLocations = new List<SelectListItem>();
            SellingPriceTaxes = new List<SelectListItem>();
            ApplicableTaxes = new List<SelectListItem>();
            Statuses = Utility.ConvertEnumToSelectList<ProductStatus>();
        }

        public void SetCategoryValues(IList<Category> categories)
        {
            Categories = Utility.ConvertCategories(categories);
        }

        public void SetProductTypeValues(IList<ProductType> productTypes)
        {
            ProductTypes = Utility.ConvertProductTypes(productTypes);
        }

        public void SetBrandValues(IList<Brand> brands)
        {
            Brands = Utility.ConvertBrands(brands);
        }

        public void SetUnitValues(IList<Unit> units)
        {
            Units = Utility.ConvertUnits(units);
        }
        public void SetBusinessLocationValues(IList<BusinessLocation> businessLocations)
        {
            BusinessLocations = Utility.ConvertBusinessLocations(businessLocations);
        }

        public void SetSellingPriceTaxValues(IList<SellingPriceTax> sellingPriceTaxes)
        {
            SellingPriceTaxes = Utility.ConvertSellingPriceTaxes(sellingPriceTaxes);
        }

        public void SetApplicableTaxValues(IList<ApplicableTax> applicableTaxes)
        {
            ApplicableTaxes = Utility.ConvertApplicableTaxes(applicableTaxes);
        }
     
    }
}