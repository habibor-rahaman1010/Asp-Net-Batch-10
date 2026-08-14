using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.Entities.StockAdjustmentEntites;
using DevSkill.Inventory.Domain.Entities.StockTransferEntities;
using Microsoft.EntityFrameworkCore;

namespace DevSkill.Inventory.Infrastructure.Data
{
    public class InventoryDbContext : DbContext
    {
        private readonly string _connectionString;
        private readonly string _migrationAssembly;

        public InventoryDbContext(string connectionString, string migrationAssembly)
        {
            _connectionString = connectionString;
            _migrationAssembly = migrationAssembly;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString,
                    x => x.MigrationsAssembly(_migrationAssembly));
            }

            base.OnConfiguring(optionsBuilder);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //seeding data of my all some classes...
            modelBuilder.Entity<ApplicableTax>().HasData(
                new ApplicableTax { Id = Guid.NewGuid(), ApplicableTaxName = "Food" },
                new ApplicableTax { Id = Guid.NewGuid(), ApplicableTaxName = "Sales Tax" },
                new ApplicableTax { Id = Guid.NewGuid(), ApplicableTaxName = "Fruits" }
            );

            modelBuilder.Entity<BarcodeType>().HasData(
                new BarcodeType { Id = Guid.NewGuid(), BarcodeTypeName = "QR Code" },
                new BarcodeType { Id = Guid.NewGuid(), BarcodeTypeName = "UPC" },
                new BarcodeType { Id = Guid.NewGuid(), BarcodeTypeName = "NFC" }
            );

            modelBuilder.Entity<Brand>().HasData(
                new Brand { Id = Guid.NewGuid(), BrandName = "Apple" },
                new Brand { Id = Guid.NewGuid(), BrandName = "Samsung" },
                new Brand { Id = Guid.NewGuid(), BrandName = "Sony" }
            );

            modelBuilder.Entity<BusinessLocation>().HasData(
                new BusinessLocation { Id = Guid.NewGuid(), LocationName = "Warehouse A" },
                new BusinessLocation { Id = Guid.NewGuid(), LocationName = "Warehouse B" },
                new BusinessLocation { Id = Guid.NewGuid(), LocationName = "Downtown Store" }
            );

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = Guid.NewGuid(), CategoryName = "Electronics" },
                new Category { Id = Guid.NewGuid(), CategoryName = "Home Appliances" },
                new Category { Id = Guid.NewGuid(), CategoryName = "Clothing" }
            );

            modelBuilder.Entity<ProductType>().HasData(
                new ProductType { Id = Guid.NewGuid(), ProductTypeName = "Electronics" },
                new ProductType { Id = Guid.NewGuid(), ProductTypeName = "Clothing" },
                new ProductType { Id = Guid.NewGuid(), ProductTypeName = "Food" },
                new ProductType { Id = Guid.NewGuid(), ProductTypeName = "Furniture" },
                new ProductType { Id = Guid.NewGuid(), ProductTypeName = "Toys" }
            );

            modelBuilder.Entity<SellingPriceTax>().HasData(
                new SellingPriceTax { Id = Guid.NewGuid(), SellingPriceTaxName = "Exclusive" },
                new SellingPriceTax { Id = Guid.NewGuid(), SellingPriceTaxName = "Inclusive" },
                new SellingPriceTax { Id = Guid.NewGuid(), SellingPriceTaxName = "Zero Rate" }
            );

            modelBuilder.Entity<SubCategory>().HasData(
                new SubCategory { Id = Guid.NewGuid(), SubCategoryName = "Smartphones" },
                new SubCategory { Id = Guid.NewGuid(), SubCategoryName = "Laptops" },
                new SubCategory { Id = Guid.NewGuid(), SubCategoryName = "Televisions" }
            );

            modelBuilder.Entity<Unit>().HasData(
                new Unit { Id = Guid.NewGuid(), UnitName = "Kilogram" },
                new Unit { Id = Guid.NewGuid(), UnitName = "Liter" },
                new Unit { Id = Guid.NewGuid(), UnitName = "Piece" }
            );

            modelBuilder.Entity<Warranty>().HasData(
                new Warranty { Id = Guid.NewGuid(), WarrantyDuration = "1 Year" },
                new Warranty { Id = Guid.NewGuid(), WarrantyDuration = "2 Years" },
                new Warranty { Id = Guid.NewGuid(), WarrantyDuration = "3 Years" }
            );
            modelBuilder.Entity<AdjustmentType>().HasData(
                new AdjustmentType { Id = Guid.NewGuid(), AdjustmentTypeName = "Normal"},
                new AdjustmentType { Id = Guid.NewGuid(), AdjustmentTypeName = "Abnormal" }
            );


            //Here relation product to other tables...
            modelBuilder.Entity<Product>()
                .HasOne(p => p.BarcodeType)
                .WithMany()
                .HasForeignKey(p => p.BarcodeTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Unit)
                .WithMany()
                .HasForeignKey(p => p.UnitId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Brand)
                .WithMany()
                .HasForeignKey(p => p.BrandId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany()
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Subcategory)
                .WithMany()
                .HasForeignKey(p => p.SubcategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.BusinessLocation)
                .WithMany()
                .HasForeignKey(p => p.BusinessLocationId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Warranty)
                .WithMany()
                .HasForeignKey(p => p.WarrantyId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.ProductType)
                .WithMany()
                .HasForeignKey(p => p.ProductTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.ApplicableTax)
                .WithMany()
                .HasForeignKey(p => p.ApplicableTaxId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.SellingPriceTax)
                .WithMany()
                .HasForeignKey(p => p.SellingPriceTaxId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<StockAdjustment>()
                .Property(sa => sa.TotalAmountRecover)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<StockAdjustment>()
                .Property(sa => sa.TotalAmount)
                .HasColumnType("decimal(18, 2)");

            // StockAdjustment and BusinessLocation relationship
            modelBuilder.Entity<StockAdjustment>()
                .HasOne(sa => sa.BusinessLocation)
                .WithMany()
                .HasForeignKey(sa => sa.BusinessLocationId)
                .OnDelete(DeleteBehavior.Restrict); 

            // StockAdjustment and AdjustmentType relationship
            modelBuilder.Entity<StockAdjustment>()
                .HasOne(sa => sa.AdjustmentType)
                .WithMany()
                .HasForeignKey(sa => sa.AdjustmentTypeId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

            // StockTransfer and warehouse relationship
            modelBuilder.Entity<StockTransfer>()
                .HasOne(x => x.FromWarehouse)
                .WithMany()
                .HasForeignKey(x => x.FromWarehouseId)
                .OnDelete(DeleteBehavior.Restrict);

            // StockTransfer and warehouse relationship
            modelBuilder.Entity<StockTransfer>()
                .HasOne(x => x.ToWarehouse)
                .WithMany()
                .HasForeignKey(x => x.ToWarehouseId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StockTransfer>()
                .HasMany(x => x.StockTransferItems)
                .WithOne(x => x.StockTransfer)
                .HasForeignKey(x => x.StockTransferId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StockAdjustment>()
                .HasMany(x => x.StockAdjustmentItems)
                .WithOne(x => x.StockAdjustment)
                .HasForeignKey(x => x.StockAdjustmentId)
                .OnDelete(DeleteBehavior.Restrict);

            ConfigureSalesModule(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        //All Sales Management module configuration lives here...
        private static void ConfigureSalesModule(ModelBuilder modelBuilder)
        {
            // Existing warehouse gets an activity flag. The store default keeps every
            // already existing row active, ValueGeneratedNever keeps EF sending false
            // when a user really deactivates a location.
            modelBuilder.Entity<BusinessLocation>()
                .Property(x => x.IsActive)
                .HasDefaultValue(true)
                .ValueGeneratedNever();

            //Customer
            modelBuilder.Entity<Customer>()
                .Property(x => x.CustomerName)
                .HasMaxLength(200)
                .IsRequired();

            modelBuilder.Entity<Customer>()
                .Property(x => x.CustomerCode)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Customer>()
                .Property(x => x.Phone)
                .HasMaxLength(30);

            modelBuilder.Entity<Customer>()
                .Property(x => x.Email)
                .HasMaxLength(150);

            modelBuilder.Entity<Customer>()
                .Property(x => x.Address)
                .HasMaxLength(500);

            modelBuilder.Entity<Customer>()
                .Property(x => x.CreditLimit)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Customer>()
                .Property(x => x.OpeningBalance)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Customer>()
                .Property(x => x.CurrentOutstanding)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Customer>()
                .HasIndex(x => x.CustomerCode)
                .IsUnique();

            modelBuilder.Entity<Customer>()
                .HasIndex(x => x.CustomerName);

            //PaymentTerm
            modelBuilder.Entity<PaymentTerm>()
                .Property(x => x.TermName)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<PaymentTerm>()
                .Property(x => x.Description)
                .HasMaxLength(250);

            modelBuilder.Entity<PaymentTerm>()
                .Property(x => x.IsActive)
                .HasDefaultValue(true)
                .ValueGeneratedNever();

            modelBuilder.Entity<PaymentTerm>()
                .HasIndex(x => x.TermName)
                .IsUnique();

            // Fixed ids keep the seed stable, so a later migration never tries to
            // delete and re-insert these rows.
            modelBuilder.Entity<PaymentTerm>().HasData(
                new PaymentTerm
                {
                    Id = Guid.Parse("6a1f0b2c-0001-4a1e-9f01-2b7d5c9e1001"),
                    TermName = "Cash on Delivery",
                    Description = "Full payment is collected when the goods are handed over.",
                    DueDays = 0,
                    IsActive = true
                },
                new PaymentTerm
                {
                    Id = Guid.Parse("6a1f0b2c-0002-4a1e-9f01-2b7d5c9e1002"),
                    TermName = "Advance Payment",
                    Description = "Full payment is received before the goods are released.",
                    DueDays = 0,
                    IsActive = true
                },
                new PaymentTerm
                {
                    Id = Guid.Parse("6a1f0b2c-0003-4a1e-9f01-2b7d5c9e1003"),
                    TermName = "50% Advance, 50% on Delivery",
                    Description = "Half of the amount is paid up front and the rest on delivery.",
                    DueDays = 0,
                    IsActive = true
                },
                new PaymentTerm
                {
                    Id = Guid.Parse("6a1f0b2c-0004-4a1e-9f01-2b7d5c9e1004"),
                    TermName = "Net 7",
                    Description = "Payment is due within 7 days of the invoice date.",
                    DueDays = 7,
                    IsActive = true
                },
                new PaymentTerm
                {
                    Id = Guid.Parse("6a1f0b2c-0005-4a1e-9f01-2b7d5c9e1005"),
                    TermName = "Net 15",
                    Description = "Payment is due within 15 days of the invoice date.",
                    DueDays = 15,
                    IsActive = true
                },
                new PaymentTerm
                {
                    Id = Guid.Parse("6a1f0b2c-0006-4a1e-9f01-2b7d5c9e1006"),
                    TermName = "Net 30",
                    Description = "Payment is due within 30 days of the invoice date.",
                    DueDays = 30,
                    IsActive = true
                },
                new PaymentTerm
                {
                    Id = Guid.Parse("6a1f0b2c-0007-4a1e-9f01-2b7d5c9e1007"),
                    TermName = "Net 60",
                    Description = "Payment is due within 60 days of the invoice date.",
                    DueDays = 60,
                    IsActive = true
                }
            );

            //PriceList
            modelBuilder.Entity<PriceList>()
                .Property(x => x.PriceListName)
                .HasMaxLength(150)
                .IsRequired();

            modelBuilder.Entity<PriceList>()
                .HasIndex(x => x.PriceListName)
                .IsUnique();

            modelBuilder.Entity<PriceList>()
                .HasIndex(x => new { x.CustomerType, x.IsActive, x.EffectiveFrom });

            modelBuilder.Entity<PriceList>()
                .HasMany(x => x.PriceListItems)
                .WithOne(x => x.PriceList)
                .HasForeignKey(x => x.PriceListId)
                .OnDelete(DeleteBehavior.Restrict);

            //PriceListItem
            modelBuilder.Entity<PriceListItem>()
                .Property(x => x.UnitPrice)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<PriceListItem>()
                .Property(x => x.MinQuantity)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<PriceListItem>()
                .HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PriceListItem>()
                .HasIndex(x => new { x.PriceListId, x.ProductId, x.MinQuantity })
                .IsUnique();

            //DiscountRule
            modelBuilder.Entity<DiscountRule>()
                .Property(x => x.RuleName)
                .HasMaxLength(150)
                .IsRequired();

            modelBuilder.Entity<DiscountRule>()
                .Property(x => x.DiscountValue)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<DiscountRule>()
                .Property(x => x.MinQuantity)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<DiscountRule>()
                .Property(x => x.MinOrderAmount)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<DiscountRule>()
                .Property(x => x.MaxDiscountAmount)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<DiscountRule>()
                .HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DiscountRule>()
                .HasOne(x => x.Customer)
                .WithMany()
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DiscountRule>()
                .HasIndex(x => new { x.Scope, x.IsActive, x.EffectiveFrom });

            //ProformaInvoice
            modelBuilder.Entity<ProformaInvoice>()
                .Property(x => x.ProformaNo)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<ProformaInvoice>()
                .Property(x => x.SalespersonName)
                .HasMaxLength(200);

            modelBuilder.Entity<ProformaInvoice>()
                .Property(x => x.Notes)
                .HasMaxLength(1000);

            modelBuilder.Entity<ProformaInvoice>()
                .Property(x => x.CreatedBy)
                .HasMaxLength(200);

            modelBuilder.Entity<ProformaInvoice>()
                .Property(x => x.SubTotal)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<ProformaInvoice>()
                .Property(x => x.ItemDiscountTotal)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<ProformaInvoice>()
                .Property(x => x.OrderDiscount)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<ProformaInvoice>()
                .Property(x => x.TotalTax)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<ProformaInvoice>()
                .Property(x => x.GrandTotal)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<ProformaInvoice>()
                .HasIndex(x => x.ProformaNo)
                .IsUnique();

            modelBuilder.Entity<ProformaInvoice>()
                .HasIndex(x => new { x.CustomerId, x.ProformaDate });

            modelBuilder.Entity<ProformaInvoice>()
                .HasOne(x => x.Customer)
                .WithMany()
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProformaInvoice>()
                .HasOne(x => x.BusinessLocation)
                .WithMany()
                .HasForeignKey(x => x.BusinessLocationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProformaInvoice>()
                .HasOne(x => x.PaymentTerm)
                .WithMany()
                .HasForeignKey(x => x.PaymentTermId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProformaInvoice>()
                .HasMany(x => x.ProformaInvoiceItems)
                .WithOne(x => x.ProformaInvoice)
                .HasForeignKey(x => x.ProformaInvoiceId)
                .OnDelete(DeleteBehavior.Restrict);

            //ProformaInvoiceItem
            modelBuilder.Entity<ProformaInvoiceItem>()
                .Property(x => x.Quantity)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<ProformaInvoiceItem>()
                .Property(x => x.UnitPrice)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<ProformaInvoiceItem>()
                .Property(x => x.DiscountAmount)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<ProformaInvoiceItem>()
                .Property(x => x.TaxRate)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<ProformaInvoiceItem>()
                .Property(x => x.TaxAmount)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<ProformaInvoiceItem>()
                .Property(x => x.LineTotal)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<ProformaInvoiceItem>()
                .HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            //Delivery
            modelBuilder.Entity<Delivery>()
                .Property(x => x.DeliveryNo)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Delivery>()
                .Property(x => x.ReceivedBy)
                .HasMaxLength(200);

            modelBuilder.Entity<Delivery>()
                .Property(x => x.Notes)
                .HasMaxLength(1000);

            modelBuilder.Entity<Delivery>()
                .Property(x => x.CreatedBy)
                .HasMaxLength(200);

            modelBuilder.Entity<Delivery>()
                .HasIndex(x => x.DeliveryNo)
                .IsUnique();

            // The delivery list is almost always read one proforma invoice at a time.
            modelBuilder.Entity<Delivery>()
                .HasIndex(x => x.ProformaInvoiceId);

            modelBuilder.Entity<Delivery>()
                .HasIndex(x => new { x.CustomerId, x.DeliveryDate });

            modelBuilder.Entity<Delivery>()
                .HasOne(x => x.ProformaInvoice)
                .WithMany()
                .HasForeignKey(x => x.ProformaInvoiceId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Delivery>()
                .HasOne(x => x.Customer)
                .WithMany()
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Delivery>()
                .HasOne(x => x.BusinessLocation)
                .WithMany()
                .HasForeignKey(x => x.BusinessLocationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Delivery>()
                .HasMany(x => x.DeliveryItems)
                .WithOne(x => x.Delivery)
                .HasForeignKey(x => x.DeliveryId)
                .OnDelete(DeleteBehavior.Restrict);

            //DeliveryItem
            modelBuilder.Entity<DeliveryItem>()
                .Property(x => x.OrderedQuantity)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<DeliveryItem>()
                .Property(x => x.DeliveredQuantity)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<DeliveryItem>()
                .HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // One product may appear only once inside a single shipment, so the
            // delivered quantity of a line is never split over two rows.
            modelBuilder.Entity<DeliveryItem>()
                .HasIndex(x => new { x.DeliveryId, x.ProductId })
                .IsUnique();
        }

        //Here is my all applcation DbSet<T>
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<SubCategory> SubCategories => Set<SubCategory>();
        public DbSet<Brand> Brands => Set<Brand>();
        public DbSet<Unit> Units => Set<Unit>();
        public DbSet<BarcodeType> BarcodeTypes => Set<BarcodeType>();
        public DbSet<BusinessLocation> BusinessLocations => Set<BusinessLocation>();
        public DbSet<Warranty> Warranties => Set<Warranty>();
        public DbSet<SellingPriceTax> SellingPriceTaxes => Set<SellingPriceTax>();
        public DbSet<ProductType> ProductTypes => Set<ProductType>();
        public DbSet<ApplicableTax> ApplicableTaxs => Set<ApplicableTax>();
        public DbSet<AdjustmentType> AdjustmentTypes => Set<AdjustmentType>();
        public DbSet<StockAdjustment> StockAdjustments => Set<StockAdjustment>();
        public DbSet<StockAdjustmentItem> StockAdjustmentItems => Set<StockAdjustmentItem>();
        public DbSet<ApplicationLog> ApplicationLogs => Set<ApplicationLog>();
        public DbSet<UserActivity> UserActivities => Set<UserActivity>();
        public DbSet<UserActivityLog> UserActivityLogs => Set<UserActivityLog>();
        public DbSet<StockTransfer> StockTransfers => Set<StockTransfer>();
        public DbSet<StockTransferItem> StockTransferItems => Set<StockTransferItem>();

        //Sales Management module DbSet<T>
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<PaymentTerm> PaymentTerms => Set<PaymentTerm>();
        public DbSet<PriceList> PriceLists => Set<PriceList>();
        public DbSet<PriceListItem> PriceListItems => Set<PriceListItem>();
        public DbSet<DiscountRule> DiscountRules => Set<DiscountRule>();
        public DbSet<ProformaInvoice> ProformaInvoices => Set<ProformaInvoice>();
        public DbSet<ProformaInvoiceItem> ProformaInvoiceItems => Set<ProformaInvoiceItem>();
        public DbSet<Delivery> Deliveries => Set<Delivery>();
        public DbSet<DeliveryItem> DeliveryItems => Set<DeliveryItem>();
    }
}