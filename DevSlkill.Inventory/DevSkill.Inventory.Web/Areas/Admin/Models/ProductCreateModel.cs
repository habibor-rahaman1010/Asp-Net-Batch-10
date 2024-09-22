using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Infrastructure.RazorUtility;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    public class ProductCreateModel
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string SKU { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Price { get; set; }
        public int Ratings { get; set; }
        public IList<SelectListItem> BarcodeTypes { get; private set; }
        public Guid BarcodeTypeId { get; set; }
        public Unit Unit { get; set; }
        public Brand Brand { get; set; }
        public Subcategory Subcategory { get; set; }
        public BusinessLocation BusinessLocation { get; set; }
        public Warranty Warranty { get; set; }
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
        public DateTime StoreTime { get; set; }

        public ApplicableTax ApplicableTax { get; set; }
        public SellingPriceTax SellingPriceTax { get; set; }
        public double ExciseTax { get; set; }
        public double InclusiveTax { get; set; }
        public double MarginTax { get; set; }
        public double SellingPrice { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }



        public ProductCreateModel()
        {
            this.Categories = new List<SelectListItem>();
            this.ProductTypes = new List<SelectListItem>();
            this.BarcodeTypes = new List<SelectListItem>();
            this.Unit = new Unit();
            this.Brand = new Brand();
            this.Subcategory = new Subcategory();
            this.BusinessLocation = new BusinessLocation();
            this.Warranty = new Warranty();
            this.ApplicableTax = new ApplicableTax();
            this.SellingPriceTax = new SellingPriceTax();
        }

        public void SetCategoriesValues(IList<Category> categories)
        {
            this.Categories = Utility.ConvertCategories(categories);
        }

        public void SetProductTypeValues(IList<ProductType> productTypes)
        {
            this.ProductTypes = Utility.ConvertProductTypes(productTypes);
        }

        public void SetBarcodeTypeValues(IList<BarcodeType> barcodeTypes)
        {
            this.BarcodeTypes = Utility.ConvertBarcodeTypes(barcodeTypes);
        }
    }
}
