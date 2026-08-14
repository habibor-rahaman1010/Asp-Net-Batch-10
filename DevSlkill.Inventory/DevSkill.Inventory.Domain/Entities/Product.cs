using DevSkill.Inventory.Domain.Enums;


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

        /// <summary>
        /// Quantity held for confirmed sales orders that are not delivered yet.
        /// Physical stock stays in <see cref="CurrentStock"/>; sellable stock is
        /// CurrentStock - ReservedStock.
        /// </summary>
        public int ReservedStock { get; set; }

        public Guid BarcodeTypeId { get; set; }
        public BarcodeType? BarcodeType { get; set; }

        public Guid UnitId { get; set; }
        public Unit? Unit { get; set; }

        public Guid BrandId { get; set; }
        public Brand? Brand { get; set; }

        public Guid CategoryId { get; set; }
        public Category? Category { get; set; }

        public Guid? SubcategoryId { get; set; }
        public SubCategory? Subcategory { get; set; }

        public Guid? BusinessLocationId { get; set; }
        public BusinessLocation? BusinessLocation { get; set; }

        public Guid WarrantyId { get; set; }
        public Warranty? Warranty { get; set; }

        public double Weight { get; set; }
        public int AlertQuantity { get; set; }
        public string Description { get; set; } = string.Empty;
        public ProductStatus Status { get; set; }

        public Guid ProductTypeId { get; set; }
        public ProductType? ProductType { get; set; }

        public string Rack { get; set; } = string.Empty;
        public string Row { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public string IMEI { get; set; } = string.Empty;
        public DateTime StoreTime { get; set; }

        public Guid? ApplicableTaxId { get; set; }
        public ApplicableTax? ApplicableTax { get; set; }

        public Guid SellingPriceTaxId { get; set; }
        public SellingPriceTax? SellingPriceTax { get; set; }

        public double ExciseTax { get; set; }
        public double InclusiveTax { get; set; }
        public double MarginTax { get; set; }
        public double SellingPrice { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string ProductImage { get; set; } = string.Empty;    
    }
}
