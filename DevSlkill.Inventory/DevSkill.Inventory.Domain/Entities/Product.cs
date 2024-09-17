using DevSkill.Inventory.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevSkill.Inventory.Domain.Entities
{
    public class Product : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Price { get; set; }
        public string SKU { get; set; } = string.Empty;
        public int Ratings { get; set; }
        public BarcodeType BarcodeType { get; set; }
        public Unit Unit { get; set; }
        public Brand Brand { get; set; }
        public Category Category { get; set; }
        public Subcategory Subcategory { get; set; }
        public BusinessLocation BusinessLocation { get; set; }
        public Warranty Warranty { get; set; }
        public float Weight { get; set; }
        public int AlertQuantity { get; set; }
        public string IMEI { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty ;
        public ProductStatus Status { get; set; }
        public ProductType ProductType { get; set; }
        public Tax Tax { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

       /* public string ProductImage { get; set; } = string.Empty;
        public string ProductBrochure { get; set; } = string.Empty;*/

        public Product() 
        {
            this.BarcodeType = new BarcodeType();
            this.Unit = new Unit();
            this.Brand = new Brand();
            this.Category = new Category();
            this.Subcategory = new Subcategory();
            this.BusinessLocation = new BusinessLocation();
            this.Warranty = new Warranty();
        }
    }
}
