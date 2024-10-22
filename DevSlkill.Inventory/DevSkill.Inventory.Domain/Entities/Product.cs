using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevSkill.Inventory.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace DevSkill.Inventory.Domain.Entities
{
    public class Product : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public double Price { get; set; }
        public string SKU { get; set; } = string.Empty;
        public double Ratings { get; set; }
        public int CurrentStock { get; set; }
        public BarcodeType BarcodeType { get; set; }
        public Unit Unit { get; set; }
        public Brand Brand { get; set; }
        public Category Category { get; set; }
        public Subcategory Subcategory { get; set; }
        public BusinessLocation BusinessLocation { get; set; }
        public Warranty Warranty { get; set; }
        public double Weight { get; set; }
        public int AlertQuantity { get; set; }
        public string Description { get; set; } = string.Empty ;
        public ProductStatus Status { get; set; }
        public ProductType ProductType { get; set; }

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
        public string ProductImage { get; set; } = string.Empty;

        public Product() 
        {
            BarcodeType = new BarcodeType();
            Unit = new Unit();
            Brand = new Brand();
            Category = new Category();
            Subcategory = new Subcategory();
            BusinessLocation = new BusinessLocation();
            Warranty = new Warranty();
            ProductType = new ProductType();
            ApplicableTax = new ApplicableTax();
            SellingPriceTax = new SellingPriceTax();
        }
    }
}
