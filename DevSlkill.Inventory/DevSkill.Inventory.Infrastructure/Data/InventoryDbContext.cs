using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
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
            // Seeding data of my all some classes...
            // The ids are written out rather than generated: a fresh Guid on every
            // model build makes EF think the seed rows changed, so each new migration
            // would drop and re-insert master data that products already point at.
            modelBuilder.Entity<ApplicableTax>().HasData(
                new ApplicableTax { Id = new Guid("55fc0b11-4bd7-4499-8cf0-dfbde9eafb7f"), ApplicableTaxName = "Food" },
                new ApplicableTax { Id = new Guid("958bfc74-5814-4da3-bf68-ac65e2866103"), ApplicableTaxName = "Sales Tax" },
                new ApplicableTax { Id = new Guid("ac3c3079-e076-4092-8e54-016ea8063308"), ApplicableTaxName = "Fruits" }
            );

            modelBuilder.Entity<BarcodeType>().HasData(
                new BarcodeType { Id = new Guid("03d28362-1525-4dfb-b46b-4656bd76dd1f"), BarcodeTypeName = "QR Code" },
                new BarcodeType { Id = new Guid("c43bf678-8caf-46a8-900c-64e58c67b1fb"), BarcodeTypeName = "UPC" },
                new BarcodeType { Id = new Guid("7da178ba-0a11-4836-a55d-93ee28bfcdb1"), BarcodeTypeName = "NFC" }
            );

            modelBuilder.Entity<Brand>().HasData(
                new Brand { Id = new Guid("a0851b20-20af-427d-9d5d-a40f451f2ef2"), BrandName = "Apple" },
                new Brand { Id = new Guid("4aa25c30-fb48-47b0-a525-b241a55df9ee"), BrandName = "Samsung" },
                new Brand { Id = new Guid("18c2bea8-b25e-433d-a08e-b6f7a00786ee"), BrandName = "Sony" }
            );

            modelBuilder.Entity<BusinessLocation>().HasData(
                new BusinessLocation { Id = new Guid("648fb752-70fa-47e4-9b3f-aed1f6c33027"), LocationName = "Warehouse A" },
                new BusinessLocation { Id = new Guid("cffe7a51-7178-4d25-938f-20402c5331b4"), LocationName = "Warehouse B" },
                new BusinessLocation { Id = new Guid("845099d1-ee98-4be5-8551-7102645b0a81"), LocationName = "Downtown Store" }
            );

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = new Guid("beac8874-432a-4028-a535-5d43e9287e5f"), CategoryName = "Electronics" },
                new Category { Id = new Guid("113fc42f-3eb3-46d5-ba13-e9a257983ba9"), CategoryName = "Home Appliances" },
                new Category { Id = new Guid("6501b4d4-54e2-4276-ab33-b773e1071649"), CategoryName = "Clothing" }
            );

            modelBuilder.Entity<ProductType>().HasData(
                new ProductType { Id = new Guid("ef208b24-e8e2-470e-8b16-08aee482a125"), ProductTypeName = "Electronics" },
                new ProductType { Id = new Guid("54f5ee90-adaf-4b0c-a656-57ecde14ef79"), ProductTypeName = "Clothing" },
                new ProductType { Id = new Guid("d99f73c5-4045-49c5-b3cb-511e714b2039"), ProductTypeName = "Food" },
                new ProductType { Id = new Guid("b180d99d-12ff-42d9-b099-1ec17e8ec58c"), ProductTypeName = "Furniture" },
                new ProductType { Id = new Guid("af92f651-47ca-4dbd-bd27-c1f2b4f23ea4"), ProductTypeName = "Toys" }
            );

            modelBuilder.Entity<SellingPriceTax>().HasData(
                new SellingPriceTax { Id = new Guid("2b97a364-d5b7-4a47-8407-f03f83cc7ae7"), SellingPriceTaxName = "Exclusive" },
                new SellingPriceTax { Id = new Guid("4c9b1237-e114-4908-b4e7-a0a8f62eda91"), SellingPriceTaxName = "Inclusive" },
                new SellingPriceTax { Id = new Guid("bca33a0a-7290-4284-adfc-4d5b25413e15"), SellingPriceTaxName = "Zero Rate" }
            );

            modelBuilder.Entity<SubCategory>().HasData(
                new SubCategory { Id = new Guid("fba544f7-3086-4287-a6d0-55d39da2ede0"), SubCategoryName = "Smartphones" },
                new SubCategory { Id = new Guid("17b15e1c-5063-4edc-8567-8f1e581eb494"), SubCategoryName = "Laptops" },
                new SubCategory { Id = new Guid("73f7b37d-e831-45a5-89e1-766b4e2b9ad1"), SubCategoryName = "Televisions" }
            );

            modelBuilder.Entity<Unit>().HasData(
                new Unit { Id = new Guid("7f2c29e4-0db7-4654-a3fc-32a7208dcbce"), UnitName = "Kilogram" },
                new Unit { Id = new Guid("864f70fc-7390-44a3-b48b-66512fe1a126"), UnitName = "Liter" },
                new Unit { Id = new Guid("f1f55cd0-b58f-4d56-8c31-99fa87b37604"), UnitName = "Piece" }
            );

            modelBuilder.Entity<Warranty>().HasData(
                new Warranty { Id = new Guid("9548761d-5d98-4a6e-9d57-9e36979f7008"), WarrantyDuration = "1 Year" },
                new Warranty { Id = new Guid("40c94f14-5f6d-4d45-ba1f-82e669d85531"), WarrantyDuration = "2 Years" },
                new Warranty { Id = new Guid("369db434-c03b-4459-aa6d-990f9a8f2968"), WarrantyDuration = "3 Years" }
            );
            modelBuilder.Entity<AdjustmentType>().HasData(
                new AdjustmentType { Id = new Guid("43c8d526-3f8a-43d8-92c4-3ae94c6aade4"), AdjustmentTypeName = "Normal"},
                new AdjustmentType { Id = new Guid("43283b35-3f96-4a3e-b35a-f395ea7de383"), AdjustmentTypeName = "Abnormal" }
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
            ConfigureSalesOperationsModule(modelBuilder);
            ConfigurePurchaseModule(modelBuilder);

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

            // Restrict, like every other document that names one: a salesperson with
            // work to their name cannot be deleted out from under it.
            modelBuilder.Entity<ProformaInvoice>()
                .HasOne(x => x.Salesperson)
                .WithMany()
                .HasForeignKey(x => x.SalespersonId)
                .OnDelete(DeleteBehavior.Restrict);

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

            // A shipment raised off a sales order is read one order at a time, exactly
            // the way the proforma side is.
            modelBuilder.Entity<Delivery>()
                .HasIndex(x => x.SalesOrderId);

            modelBuilder.Entity<Delivery>()
                .HasOne(x => x.ProformaInvoice)
                .WithMany()
                .HasForeignKey(x => x.ProformaInvoiceId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Delivery>()
                .HasOne(x => x.SalesOrder)
                .WithMany()
                .HasForeignKey(x => x.SalesOrderId)
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

        //The Sales Management order-to-cash chain lives here: quotation -> order ->
        //reservation -> invoice -> collection -> return -> credit note, plus the
        //salesperson and the commission earned along the way.
        private static void ConfigureSalesOperationsModule(ModelBuilder modelBuilder)
        {
            //Salesperson
            modelBuilder.Entity<Salesperson>()
                .Property(x => x.SalespersonName)
                .HasMaxLength(200)
                .IsRequired();

            modelBuilder.Entity<Salesperson>()
                .Property(x => x.SalespersonCode)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Salesperson>()
                .Property(x => x.Phone)
                .HasMaxLength(30);

            modelBuilder.Entity<Salesperson>()
                .Property(x => x.Email)
                .HasMaxLength(150);

            foreach (var money in new[] { "CommissionRate", "MonthlyTarget" })
            {
                modelBuilder.Entity<Salesperson>()
                    .Property(money)
                    .HasColumnType("decimal(18, 2)");
            }

            modelBuilder.Entity<Salesperson>()
                .HasIndex(x => x.SalespersonCode)
                .IsUnique();

            modelBuilder.Entity<Salesperson>()
                .HasIndex(x => x.SalespersonName);

            //SalesQuotation
            modelBuilder.Entity<SalesQuotation>()
                .Property(x => x.QuotationNo)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<SalesQuotation>()
                .Property(x => x.CustomerReference)
                .HasMaxLength(100);

            modelBuilder.Entity<SalesQuotation>()
                .Property(x => x.Notes)
                .HasMaxLength(1000);

            modelBuilder.Entity<SalesQuotation>()
                .Property(x => x.CreatedBy)
                .HasMaxLength(200);

            foreach (var money in new[] { "SubTotal", "ItemDiscountTotal", "TotalTax", "OtherCharges", "GrandTotal" })
            {
                modelBuilder.Entity<SalesQuotation>()
                    .Property(money)
                    .HasColumnType("decimal(18, 2)");
            }

            modelBuilder.Entity<SalesQuotation>()
                .HasIndex(x => x.QuotationNo)
                .IsUnique();

            modelBuilder.Entity<SalesQuotation>()
                .HasIndex(x => new { x.CustomerId, x.QuotationDate });

            modelBuilder.Entity<SalesQuotation>()
                .HasIndex(x => new { x.Status, x.QuotationDate });

            modelBuilder.Entity<SalesQuotation>()
                .HasOne(x => x.Customer)
                .WithMany()
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SalesQuotation>()
                .HasOne(x => x.BusinessLocation)
                .WithMany()
                .HasForeignKey(x => x.BusinessLocationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SalesQuotation>()
                .HasOne(x => x.PaymentTerm)
                .WithMany()
                .HasForeignKey(x => x.PaymentTermId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SalesQuotation>()
                .HasOne(x => x.Salesperson)
                .WithMany()
                .HasForeignKey(x => x.SalespersonId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SalesQuotation>()
                .HasMany(x => x.SalesQuotationItems)
                .WithOne(x => x.SalesQuotation)
                .HasForeignKey(x => x.SalesQuotationId)
                .OnDelete(DeleteBehavior.Restrict);

            //SalesQuotationItem
            foreach (var money in new[] { "Quantity", "UnitPrice", "DiscountAmount", "TaxRate",
                                          "TaxAmount", "LineTotal", "OrderedQuantity" })
            {
                modelBuilder.Entity<SalesQuotationItem>()
                    .Property(money)
                    .HasColumnType("decimal(18, 2)");
            }

            modelBuilder.Entity<SalesQuotationItem>()
                .Property(x => x.Remarks)
                .HasMaxLength(500);

            modelBuilder.Entity<SalesQuotationItem>()
                .HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // One product may appear only once on a quotation, so the ordered quantity
            // of a line is never split over two rows.
            modelBuilder.Entity<SalesQuotationItem>()
                .HasIndex(x => new { x.SalesQuotationId, x.ProductId })
                .IsUnique();

            //SalesOrder
            modelBuilder.Entity<SalesOrder>()
                .Property(x => x.SalesOrderNo)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<SalesOrder>()
                .Property(x => x.CustomerReference)
                .HasMaxLength(100);

            modelBuilder.Entity<SalesOrder>()
                .Property(x => x.Notes)
                .HasMaxLength(1000);

            modelBuilder.Entity<SalesOrder>()
                .Property(x => x.CreatedBy)
                .HasMaxLength(200);

            foreach (var money in new[] { "SubTotal", "ItemDiscountTotal", "TotalTax", "OtherCharges", "GrandTotal" })
            {
                modelBuilder.Entity<SalesOrder>()
                    .Property(money)
                    .HasColumnType("decimal(18, 2)");
            }

            modelBuilder.Entity<SalesOrder>()
                .HasIndex(x => x.SalesOrderNo)
                .IsUnique();

            modelBuilder.Entity<SalesOrder>()
                .HasIndex(x => new { x.CustomerId, x.OrderDate });

            modelBuilder.Entity<SalesOrder>()
                .HasIndex(x => new { x.Status, x.OrderDate });

            modelBuilder.Entity<SalesOrder>()
                .HasOne(x => x.Customer)
                .WithMany()
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SalesOrder>()
                .HasOne(x => x.BusinessLocation)
                .WithMany()
                .HasForeignKey(x => x.BusinessLocationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SalesOrder>()
                .HasOne(x => x.PaymentTerm)
                .WithMany()
                .HasForeignKey(x => x.PaymentTermId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SalesOrder>()
                .HasOne(x => x.Salesperson)
                .WithMany()
                .HasForeignKey(x => x.SalespersonId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SalesOrder>()
                .HasOne(x => x.SalesQuotation)
                .WithMany()
                .HasForeignKey(x => x.SalesQuotationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SalesOrder>()
                .HasMany(x => x.SalesOrderItems)
                .WithOne(x => x.SalesOrder)
                .HasForeignKey(x => x.SalesOrderId)
                .OnDelete(DeleteBehavior.Restrict);

            //SalesOrderItem
            foreach (var money in new[] { "Quantity", "UnitPrice", "DiscountAmount", "TaxRate",
                                          "TaxAmount", "LineTotal", "DeliveredQuantity", "InvoicedQuantity" })
            {
                modelBuilder.Entity<SalesOrderItem>()
                    .Property(money)
                    .HasColumnType("decimal(18, 2)");
            }

            modelBuilder.Entity<SalesOrderItem>()
                .HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // One product may appear only once on an order, so the delivered and
            // invoiced quantities of a line are never split over two rows.
            modelBuilder.Entity<SalesOrderItem>()
                .HasIndex(x => new { x.SalesOrderId, x.ProductId })
                .IsUnique();

            //StockReservation
            modelBuilder.Entity<StockReservation>()
                .Property(x => x.ReservationNo)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<StockReservation>()
                .Property(x => x.Notes)
                .HasMaxLength(1000);

            modelBuilder.Entity<StockReservation>()
                .Property(x => x.CreatedBy)
                .HasMaxLength(200);

            modelBuilder.Entity<StockReservation>()
                .HasIndex(x => x.ReservationNo)
                .IsUnique();

            // The reservation list is almost always read one sales order at a time.
            modelBuilder.Entity<StockReservation>()
                .HasIndex(x => x.SalesOrderId);

            // The expiry sweep reads active holds by the date they run out on.
            modelBuilder.Entity<StockReservation>()
                .HasIndex(x => new { x.Status, x.ExpiryDate });

            modelBuilder.Entity<StockReservation>()
                .HasOne(x => x.SalesOrder)
                .WithMany()
                .HasForeignKey(x => x.SalesOrderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StockReservation>()
                .HasOne(x => x.Customer)
                .WithMany()
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StockReservation>()
                .HasOne(x => x.BusinessLocation)
                .WithMany()
                .HasForeignKey(x => x.BusinessLocationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StockReservation>()
                .HasMany(x => x.StockReservationItems)
                .WithOne(x => x.StockReservation)
                .HasForeignKey(x => x.StockReservationId)
                .OnDelete(DeleteBehavior.Restrict);

            //StockReservationItem
            foreach (var money in new[] { "OrderedQuantity", "ReservedQuantity", "ConsumedQuantity" })
            {
                modelBuilder.Entity<StockReservationItem>()
                    .Property(money)
                    .HasColumnType("decimal(18, 2)");
            }

            modelBuilder.Entity<StockReservationItem>()
                .Property(x => x.Remarks)
                .HasMaxLength(500);

            modelBuilder.Entity<StockReservationItem>()
                .HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StockReservationItem>()
                .HasIndex(x => new { x.StockReservationId, x.ProductId })
                .IsUnique();

            //SalesInvoice
            modelBuilder.Entity<SalesInvoice>()
                .Property(x => x.InvoiceNo)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<SalesInvoice>()
                .Property(x => x.Notes)
                .HasMaxLength(1000);

            modelBuilder.Entity<SalesInvoice>()
                .Property(x => x.CreatedBy)
                .HasMaxLength(200);

            foreach (var money in new[] { "SubTotal", "ItemDiscountTotal", "TotalTax", "OtherCharges",
                                          "GrandTotal", "PaidAmount", "DueAmount" })
            {
                modelBuilder.Entity<SalesInvoice>()
                    .Property(money)
                    .HasColumnType("decimal(18, 2)");
            }

            modelBuilder.Entity<SalesInvoice>()
                .HasIndex(x => x.InvoiceNo)
                .IsUnique();

            modelBuilder.Entity<SalesInvoice>()
                .HasIndex(x => x.SalesOrderId);

            // The ageing report reads outstanding invoices one customer at a time.
            modelBuilder.Entity<SalesInvoice>()
                .HasIndex(x => new { x.CustomerId, x.Status, x.DueDate });

            modelBuilder.Entity<SalesInvoice>()
                .HasIndex(x => new { x.SalespersonId, x.InvoiceDate });

            modelBuilder.Entity<SalesInvoice>()
                .HasOne(x => x.Customer)
                .WithMany()
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SalesInvoice>()
                .HasOne(x => x.SalesOrder)
                .WithMany()
                .HasForeignKey(x => x.SalesOrderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SalesInvoice>()
                .HasOne(x => x.Delivery)
                .WithMany()
                .HasForeignKey(x => x.DeliveryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SalesInvoice>()
                .HasOne(x => x.BusinessLocation)
                .WithMany()
                .HasForeignKey(x => x.BusinessLocationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SalesInvoice>()
                .HasOne(x => x.Salesperson)
                .WithMany()
                .HasForeignKey(x => x.SalespersonId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SalesInvoice>()
                .HasOne(x => x.PaymentTerm)
                .WithMany()
                .HasForeignKey(x => x.PaymentTermId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SalesInvoice>()
                .HasMany(x => x.SalesInvoiceItems)
                .WithOne(x => x.SalesInvoice)
                .HasForeignKey(x => x.SalesInvoiceId)
                .OnDelete(DeleteBehavior.Restrict);

            //SalesInvoiceItem
            foreach (var money in new[] { "Quantity", "UnitPrice", "DiscountAmount", "TaxRate",
                                          "TaxAmount", "LineTotal", "ReturnedQuantity" })
            {
                modelBuilder.Entity<SalesInvoiceItem>()
                    .Property(money)
                    .HasColumnType("decimal(18, 2)");
            }

            modelBuilder.Entity<SalesInvoiceItem>()
                .HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SalesInvoiceItem>()
                .HasIndex(x => new { x.SalesInvoiceId, x.ProductId })
                .IsUnique();

            //CustomerPayment
            modelBuilder.Entity<CustomerPayment>()
                .Property(x => x.PaymentNo)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<CustomerPayment>()
                .Property(x => x.ReferenceNo)
                .HasMaxLength(100);

            modelBuilder.Entity<CustomerPayment>()
                .Property(x => x.Notes)
                .HasMaxLength(1000);

            modelBuilder.Entity<CustomerPayment>()
                .Property(x => x.CreatedBy)
                .HasMaxLength(200);

            foreach (var money in new[] { "Amount", "AllocatedAmount" })
            {
                modelBuilder.Entity<CustomerPayment>()
                    .Property(money)
                    .HasColumnType("decimal(18, 2)");
            }

            modelBuilder.Entity<CustomerPayment>()
                .HasIndex(x => x.PaymentNo)
                .IsUnique();

            // The customer ledger reads the collections of one customer over a period.
            modelBuilder.Entity<CustomerPayment>()
                .HasIndex(x => new { x.CustomerId, x.PaymentDate });

            modelBuilder.Entity<CustomerPayment>()
                .HasIndex(x => x.CreditNoteId);

            modelBuilder.Entity<CustomerPayment>()
                .HasOne(x => x.Customer)
                .WithMany()
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CustomerPayment>()
                .HasOne(x => x.CreditNote)
                .WithMany()
                .HasForeignKey(x => x.CreditNoteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CustomerPayment>()
                .HasMany(x => x.CustomerPaymentAllocations)
                .WithOne(x => x.CustomerPayment)
                .HasForeignKey(x => x.CustomerPaymentId)
                .OnDelete(DeleteBehavior.Restrict);

            //CustomerPaymentAllocation
            foreach (var money in new[] { "DueAmount", "AllocatedAmount" })
            {
                modelBuilder.Entity<CustomerPaymentAllocation>()
                    .Property(money)
                    .HasColumnType("decimal(18, 2)");
            }

            modelBuilder.Entity<CustomerPaymentAllocation>()
                .Property(x => x.Remarks)
                .HasMaxLength(500);

            modelBuilder.Entity<CustomerPaymentAllocation>()
                .HasOne(x => x.SalesInvoice)
                .WithMany()
                .HasForeignKey(x => x.SalesInvoiceId)
                .OnDelete(DeleteBehavior.Restrict);

            // One collection settles a given invoice once; collecting again is a second
            // collection, which is what makes instalments readable.
            modelBuilder.Entity<CustomerPaymentAllocation>()
                .HasIndex(x => new { x.CustomerPaymentId, x.SalesInvoiceId })
                .IsUnique();

            modelBuilder.Entity<CustomerPaymentAllocation>()
                .HasIndex(x => x.SalesInvoiceId);

            //SalesReturn
            modelBuilder.Entity<SalesReturn>()
                .Property(x => x.ReturnNo)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<SalesReturn>()
                .Property(x => x.Reason)
                .HasMaxLength(500);

            modelBuilder.Entity<SalesReturn>()
                .Property(x => x.Notes)
                .HasMaxLength(1000);

            modelBuilder.Entity<SalesReturn>()
                .Property(x => x.CreatedBy)
                .HasMaxLength(200);

            modelBuilder.Entity<SalesReturn>()
                .Property(x => x.TotalAmount)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<SalesReturn>()
                .HasIndex(x => x.ReturnNo)
                .IsUnique();

            // A return is almost always read one invoice at a time, because that is
            // what caps the quantity that may come back.
            modelBuilder.Entity<SalesReturn>()
                .HasIndex(x => x.SalesInvoiceId);

            modelBuilder.Entity<SalesReturn>()
                .HasIndex(x => new { x.CustomerId, x.ReturnDate });

            modelBuilder.Entity<SalesReturn>()
                .HasOne(x => x.SalesInvoice)
                .WithMany()
                .HasForeignKey(x => x.SalesInvoiceId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SalesReturn>()
                .HasOne(x => x.Customer)
                .WithMany()
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SalesReturn>()
                .HasOne(x => x.BusinessLocation)
                .WithMany()
                .HasForeignKey(x => x.BusinessLocationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SalesReturn>()
                .HasMany(x => x.SalesReturnItems)
                .WithOne(x => x.SalesReturn)
                .HasForeignKey(x => x.SalesReturnId)
                .OnDelete(DeleteBehavior.Restrict);

            //SalesReturnItem
            foreach (var money in new[] { "InvoicedQuantity", "ReturnQuantity", "UnitPrice",
                                          "TaxRate", "TaxAmount", "LineTotal" })
            {
                modelBuilder.Entity<SalesReturnItem>()
                    .Property(money)
                    .HasColumnType("decimal(18, 2)");
            }

            modelBuilder.Entity<SalesReturnItem>()
                .Property(x => x.Remarks)
                .HasMaxLength(500);

            modelBuilder.Entity<SalesReturnItem>()
                .HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SalesReturnItem>()
                .HasIndex(x => new { x.SalesReturnId, x.ProductId })
                .IsUnique();

            //CreditNote
            modelBuilder.Entity<CreditNote>()
                .Property(x => x.CreditNoteNo)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<CreditNote>()
                .Property(x => x.Reason)
                .HasMaxLength(500);

            modelBuilder.Entity<CreditNote>()
                .Property(x => x.Notes)
                .HasMaxLength(1000);

            modelBuilder.Entity<CreditNote>()
                .Property(x => x.CreatedBy)
                .HasMaxLength(200);

            foreach (var money in new[] { "SubTotal", "TotalTax", "TotalAmount", "AppliedAmount" })
            {
                modelBuilder.Entity<CreditNote>()
                    .Property(money)
                    .HasColumnType("decimal(18, 2)");
            }

            modelBuilder.Entity<CreditNote>()
                .HasIndex(x => x.CreditNoteNo)
                .IsUnique();

            modelBuilder.Entity<CreditNote>()
                .HasIndex(x => new { x.CustomerId, x.CreditNoteDate });

            modelBuilder.Entity<CreditNote>()
                .HasIndex(x => new { x.Status, x.CreditNoteDate });

            // A return raises exactly one credit note, which is what keeps the goods
            // that came back and the credit that was given for them in step.
            modelBuilder.Entity<CreditNote>()
                .HasIndex(x => x.SalesReturnId)
                .IsUnique()
                .HasFilter("[SalesReturnId] IS NOT NULL");

            modelBuilder.Entity<CreditNote>()
                .HasOne(x => x.Customer)
                .WithMany()
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CreditNote>()
                .HasOne(x => x.SalesReturn)
                .WithMany()
                .HasForeignKey(x => x.SalesReturnId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CreditNote>()
                .HasOne(x => x.SalesInvoice)
                .WithMany()
                .HasForeignKey(x => x.SalesInvoiceId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CreditNote>()
                .HasMany(x => x.CreditNoteItems)
                .WithOne(x => x.CreditNote)
                .HasForeignKey(x => x.CreditNoteId)
                .OnDelete(DeleteBehavior.Restrict);

            //CreditNoteItem
            foreach (var money in new[] { "Quantity", "UnitPrice", "TaxRate", "TaxAmount", "LineTotal" })
            {
                modelBuilder.Entity<CreditNoteItem>()
                    .Property(money)
                    .HasColumnType("decimal(18, 2)");
            }

            modelBuilder.Entity<CreditNoteItem>()
                .Property(x => x.Description)
                .HasMaxLength(500);

            modelBuilder.Entity<CreditNoteItem>()
                .HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CreditNoteItem>()
                .HasIndex(x => x.CreditNoteId);

            //SalesCommission
            foreach (var money in new[] { "BasisAmount", "CommissionRate", "CommissionAmount" })
            {
                modelBuilder.Entity<SalesCommission>()
                    .Property(money)
                    .HasColumnType("decimal(18, 2)");
            }

            modelBuilder.Entity<SalesCommission>()
                .Property(x => x.Notes)
                .HasMaxLength(500);

            // An invoice earns commission once. Posting it again after a cancellation
            // rewrites that one row rather than adding a second.
            modelBuilder.Entity<SalesCommission>()
                .HasIndex(x => x.SalesInvoiceId)
                .IsUnique();

            modelBuilder.Entity<SalesCommission>()
                .HasIndex(x => new { x.SalespersonId, x.EarnedDate });

            modelBuilder.Entity<SalesCommission>()
                .HasOne(x => x.Salesperson)
                .WithMany()
                .HasForeignKey(x => x.SalespersonId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SalesCommission>()
                .HasOne(x => x.SalesInvoice)
                .WithMany()
                .HasForeignKey(x => x.SalesInvoiceId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        //All Purchase Management module configuration lives here...
        private static void ConfigurePurchaseModule(ModelBuilder modelBuilder)
        {
            //Supplier
            modelBuilder.Entity<Supplier>()
                .Property(x => x.SupplierName)
                .HasMaxLength(200)
                .IsRequired();

            modelBuilder.Entity<Supplier>()
                .Property(x => x.SupplierCode)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Supplier>()
                .Property(x => x.ContactPerson)
                .HasMaxLength(200);

            modelBuilder.Entity<Supplier>()
                .Property(x => x.Phone)
                .HasMaxLength(30);

            modelBuilder.Entity<Supplier>()
                .Property(x => x.Email)
                .HasMaxLength(150);

            modelBuilder.Entity<Supplier>()
                .Property(x => x.Address)
                .HasMaxLength(500);

            modelBuilder.Entity<Supplier>()
                .Property(x => x.TaxNumber)
                .HasMaxLength(50);

            modelBuilder.Entity<Supplier>()
                .Property(x => x.CreditLimit)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Supplier>()
                .Property(x => x.OpeningBalance)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Supplier>()
                .Property(x => x.CurrentOutstanding)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Supplier>()
                .HasIndex(x => x.SupplierCode)
                .IsUnique();

            modelBuilder.Entity<Supplier>()
                .HasIndex(x => x.SupplierName);

            modelBuilder.Entity<Supplier>()
                .HasOne(x => x.PaymentTerm)
                .WithMany()
                .HasForeignKey(x => x.PaymentTermId)
                .OnDelete(DeleteBehavior.Restrict);

            //PurchaseRequisition
            modelBuilder.Entity<PurchaseRequisition>()
                .Property(x => x.RequisitionNo)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<PurchaseRequisition>()
                .Property(x => x.RequestedByName)
                .HasMaxLength(200);

            modelBuilder.Entity<PurchaseRequisition>()
                .Property(x => x.ApprovedByName)
                .HasMaxLength(200);

            modelBuilder.Entity<PurchaseRequisition>()
                .Property(x => x.RejectionReason)
                .HasMaxLength(500);

            modelBuilder.Entity<PurchaseRequisition>()
                .Property(x => x.Notes)
                .HasMaxLength(1000);

            modelBuilder.Entity<PurchaseRequisition>()
                .Property(x => x.CreatedBy)
                .HasMaxLength(200);

            modelBuilder.Entity<PurchaseRequisition>()
                .Property(x => x.EstimatedTotal)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<PurchaseRequisition>()
                .HasIndex(x => x.RequisitionNo)
                .IsUnique();

            modelBuilder.Entity<PurchaseRequisition>()
                .HasIndex(x => new { x.Status, x.RequisitionDate });

            modelBuilder.Entity<PurchaseRequisition>()
                .HasOne(x => x.BusinessLocation)
                .WithMany()
                .HasForeignKey(x => x.BusinessLocationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PurchaseRequisition>()
                .HasMany(x => x.PurchaseRequisitionItems)
                .WithOne(x => x.PurchaseRequisition)
                .HasForeignKey(x => x.PurchaseRequisitionId)
                .OnDelete(DeleteBehavior.Restrict);

            //PurchaseRequisitionItem
            modelBuilder.Entity<PurchaseRequisitionItem>()
                .Property(x => x.Quantity)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<PurchaseRequisitionItem>()
                .Property(x => x.EstimatedUnitPrice)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<PurchaseRequisitionItem>()
                .Property(x => x.OrderedQuantity)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<PurchaseRequisitionItem>()
                .Property(x => x.Remarks)
                .HasMaxLength(500);

            modelBuilder.Entity<PurchaseRequisitionItem>()
                .HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // One product may appear only once on a requisition, so its quantity is
            // never split over two rows.
            modelBuilder.Entity<PurchaseRequisitionItem>()
                .HasIndex(x => new { x.PurchaseRequisitionId, x.ProductId })
                .IsUnique();

            //PurchaseOrder
            modelBuilder.Entity<PurchaseOrder>()
                .Property(x => x.PurchaseOrderNo)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<PurchaseOrder>()
                .Property(x => x.ApprovedByName)
                .HasMaxLength(200);

            modelBuilder.Entity<PurchaseOrder>()
                .Property(x => x.RejectionReason)
                .HasMaxLength(500);

            modelBuilder.Entity<PurchaseOrder>()
                .Property(x => x.Notes)
                .HasMaxLength(1000);

            modelBuilder.Entity<PurchaseOrder>()
                .Property(x => x.CreatedBy)
                .HasMaxLength(200);

            modelBuilder.Entity<PurchaseOrder>()
                .Property(x => x.SubTotal)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<PurchaseOrder>()
                .Property(x => x.ItemDiscountTotal)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<PurchaseOrder>()
                .Property(x => x.TotalTax)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<PurchaseOrder>()
                .Property(x => x.OtherCharges)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<PurchaseOrder>()
                .Property(x => x.GrandTotal)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<PurchaseOrder>()
                .HasIndex(x => x.PurchaseOrderNo)
                .IsUnique();

            modelBuilder.Entity<PurchaseOrder>()
                .HasIndex(x => new { x.SupplierId, x.OrderDate });

            modelBuilder.Entity<PurchaseOrder>()
                .HasIndex(x => new { x.Status, x.OrderDate });

            modelBuilder.Entity<PurchaseOrder>()
                .HasOne(x => x.Supplier)
                .WithMany()
                .HasForeignKey(x => x.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PurchaseOrder>()
                .HasOne(x => x.BusinessLocation)
                .WithMany()
                .HasForeignKey(x => x.BusinessLocationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PurchaseOrder>()
                .HasOne(x => x.PaymentTerm)
                .WithMany()
                .HasForeignKey(x => x.PaymentTermId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PurchaseOrder>()
                .HasOne(x => x.PurchaseRequisition)
                .WithMany()
                .HasForeignKey(x => x.PurchaseRequisitionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PurchaseOrder>()
                .HasMany(x => x.PurchaseOrderItems)
                .WithOne(x => x.PurchaseOrder)
                .HasForeignKey(x => x.PurchaseOrderId)
                .OnDelete(DeleteBehavior.Restrict);

            //PurchaseOrderItem
            foreach (var money in new[] { "Quantity", "UnitPrice", "DiscountAmount", "TaxRate",
                                          "TaxAmount", "LineTotal", "ReceivedQuantity", "InvoicedQuantity" })
            {
                modelBuilder.Entity<PurchaseOrderItem>()
                    .Property(money)
                    .HasColumnType("decimal(18, 2)");
            }

            modelBuilder.Entity<PurchaseOrderItem>()
                .HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // One product may appear only once on an order, so the received and
            // invoiced quantities of a line are never split over two rows.
            modelBuilder.Entity<PurchaseOrderItem>()
                .HasIndex(x => new { x.PurchaseOrderId, x.ProductId })
                .IsUnique();

            //GoodsReceipt
            modelBuilder.Entity<GoodsReceipt>()
                .Property(x => x.GoodsReceiptNo)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<GoodsReceipt>()
                .Property(x => x.SupplierChallanNo)
                .HasMaxLength(100);

            modelBuilder.Entity<GoodsReceipt>()
                .Property(x => x.ReceivedBy)
                .HasMaxLength(200);

            modelBuilder.Entity<GoodsReceipt>()
                .Property(x => x.Notes)
                .HasMaxLength(1000);

            modelBuilder.Entity<GoodsReceipt>()
                .Property(x => x.CreatedBy)
                .HasMaxLength(200);

            modelBuilder.Entity<GoodsReceipt>()
                .HasIndex(x => x.GoodsReceiptNo)
                .IsUnique();

            // The receipt list is almost always read one purchase order at a time.
            modelBuilder.Entity<GoodsReceipt>()
                .HasIndex(x => x.PurchaseOrderId);

            modelBuilder.Entity<GoodsReceipt>()
                .HasIndex(x => new { x.SupplierId, x.ReceiptDate });

            modelBuilder.Entity<GoodsReceipt>()
                .HasOne(x => x.PurchaseOrder)
                .WithMany()
                .HasForeignKey(x => x.PurchaseOrderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<GoodsReceipt>()
                .HasOne(x => x.Supplier)
                .WithMany()
                .HasForeignKey(x => x.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<GoodsReceipt>()
                .HasOne(x => x.BusinessLocation)
                .WithMany()
                .HasForeignKey(x => x.BusinessLocationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<GoodsReceipt>()
                .HasMany(x => x.GoodsReceiptItems)
                .WithOne(x => x.GoodsReceipt)
                .HasForeignKey(x => x.GoodsReceiptId)
                .OnDelete(DeleteBehavior.Restrict);

            //GoodsReceiptItem
            foreach (var money in new[] { "OrderedQuantity", "ReceivedQuantity", "RejectedQuantity",
                                          "ReturnedQuantity", "UnitPrice" })
            {
                modelBuilder.Entity<GoodsReceiptItem>()
                    .Property(money)
                    .HasColumnType("decimal(18, 2)");
            }

            modelBuilder.Entity<GoodsReceiptItem>()
                .Property(x => x.Remarks)
                .HasMaxLength(500);

            modelBuilder.Entity<GoodsReceiptItem>()
                .HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<GoodsReceiptItem>()
                .HasIndex(x => new { x.GoodsReceiptId, x.ProductId })
                .IsUnique();

            //PurchaseInvoice
            modelBuilder.Entity<PurchaseInvoice>()
                .Property(x => x.InvoiceNo)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<PurchaseInvoice>()
                .Property(x => x.SupplierInvoiceNo)
                .HasMaxLength(100);

            modelBuilder.Entity<PurchaseInvoice>()
                .Property(x => x.Notes)
                .HasMaxLength(1000);

            modelBuilder.Entity<PurchaseInvoice>()
                .Property(x => x.CreatedBy)
                .HasMaxLength(200);

            foreach (var money in new[] { "SubTotal", "ItemDiscountTotal", "TotalTax", "OtherCharges",
                                          "GrandTotal", "PaidAmount", "DueAmount" })
            {
                modelBuilder.Entity<PurchaseInvoice>()
                    .Property(money)
                    .HasColumnType("decimal(18, 2)");
            }

            modelBuilder.Entity<PurchaseInvoice>()
                .HasIndex(x => x.InvoiceNo)
                .IsUnique();

            modelBuilder.Entity<PurchaseInvoice>()
                .HasIndex(x => x.PurchaseOrderId);

            // The ageing report reads outstanding invoices one supplier at a time.
            modelBuilder.Entity<PurchaseInvoice>()
                .HasIndex(x => new { x.SupplierId, x.Status, x.DueDate });

            modelBuilder.Entity<PurchaseInvoice>()
                .HasOne(x => x.Supplier)
                .WithMany()
                .HasForeignKey(x => x.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PurchaseInvoice>()
                .HasOne(x => x.PurchaseOrder)
                .WithMany()
                .HasForeignKey(x => x.PurchaseOrderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PurchaseInvoice>()
                .HasOne(x => x.GoodsReceipt)
                .WithMany()
                .HasForeignKey(x => x.GoodsReceiptId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PurchaseInvoice>()
                .HasOne(x => x.PaymentTerm)
                .WithMany()
                .HasForeignKey(x => x.PaymentTermId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PurchaseInvoice>()
                .HasMany(x => x.PurchaseInvoiceItems)
                .WithOne(x => x.PurchaseInvoice)
                .HasForeignKey(x => x.PurchaseInvoiceId)
                .OnDelete(DeleteBehavior.Restrict);

            //PurchaseInvoiceItem
            foreach (var money in new[] { "Quantity", "UnitPrice", "DiscountAmount", "TaxRate",
                                          "TaxAmount", "LineTotal" })
            {
                modelBuilder.Entity<PurchaseInvoiceItem>()
                    .Property(money)
                    .HasColumnType("decimal(18, 2)");
            }

            modelBuilder.Entity<PurchaseInvoiceItem>()
                .HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PurchaseInvoiceItem>()
                .HasIndex(x => new { x.PurchaseInvoiceId, x.ProductId })
                .IsUnique();

            //PurchaseReturn
            modelBuilder.Entity<PurchaseReturn>()
                .Property(x => x.ReturnNo)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<PurchaseReturn>()
                .Property(x => x.Reason)
                .HasMaxLength(500);

            modelBuilder.Entity<PurchaseReturn>()
                .Property(x => x.Notes)
                .HasMaxLength(1000);

            modelBuilder.Entity<PurchaseReturn>()
                .Property(x => x.CreatedBy)
                .HasMaxLength(200);

            modelBuilder.Entity<PurchaseReturn>()
                .Property(x => x.TotalAmount)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<PurchaseReturn>()
                .HasIndex(x => x.ReturnNo)
                .IsUnique();

            // A return is almost always read one goods receipt at a time, because that
            // is what caps the quantity that may go back.
            modelBuilder.Entity<PurchaseReturn>()
                .HasIndex(x => x.GoodsReceiptId);

            // The supplier ledger reads the returns of one supplier over a period.
            modelBuilder.Entity<PurchaseReturn>()
                .HasIndex(x => new { x.SupplierId, x.ReturnDate });

            modelBuilder.Entity<PurchaseReturn>()
                .HasOne(x => x.GoodsReceipt)
                .WithMany()
                .HasForeignKey(x => x.GoodsReceiptId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PurchaseReturn>()
                .HasOne(x => x.Supplier)
                .WithMany()
                .HasForeignKey(x => x.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PurchaseReturn>()
                .HasOne(x => x.BusinessLocation)
                .WithMany()
                .HasForeignKey(x => x.BusinessLocationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PurchaseReturn>()
                .HasMany(x => x.PurchaseReturnItems)
                .WithOne(x => x.PurchaseReturn)
                .HasForeignKey(x => x.PurchaseReturnId)
                .OnDelete(DeleteBehavior.Restrict);

            //PurchaseReturnItem
            foreach (var money in new[] { "ReceivedQuantity", "ReturnQuantity", "UnitPrice", "LineTotal" })
            {
                modelBuilder.Entity<PurchaseReturnItem>()
                    .Property(money)
                    .HasColumnType("decimal(18, 2)");
            }

            modelBuilder.Entity<PurchaseReturnItem>()
                .Property(x => x.Remarks)
                .HasMaxLength(500);

            modelBuilder.Entity<PurchaseReturnItem>()
                .HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PurchaseReturnItem>()
                .HasIndex(x => new { x.PurchaseReturnId, x.ProductId })
                .IsUnique();

            //SupplierPayment
            modelBuilder.Entity<SupplierPayment>()
                .Property(x => x.PaymentNo)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<SupplierPayment>()
                .Property(x => x.ReferenceNo)
                .HasMaxLength(100);

            modelBuilder.Entity<SupplierPayment>()
                .Property(x => x.Notes)
                .HasMaxLength(1000);

            modelBuilder.Entity<SupplierPayment>()
                .Property(x => x.CreatedBy)
                .HasMaxLength(200);

            foreach (var money in new[] { "Amount", "AllocatedAmount" })
            {
                modelBuilder.Entity<SupplierPayment>()
                    .Property(money)
                    .HasColumnType("decimal(18, 2)");
            }

            modelBuilder.Entity<SupplierPayment>()
                .HasIndex(x => x.PaymentNo)
                .IsUnique();

            // The supplier ledger reads the payments of one supplier over a period.
            modelBuilder.Entity<SupplierPayment>()
                .HasIndex(x => new { x.SupplierId, x.PaymentDate });

            modelBuilder.Entity<SupplierPayment>()
                .HasOne(x => x.Supplier)
                .WithMany()
                .HasForeignKey(x => x.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SupplierPayment>()
                .HasMany(x => x.SupplierPaymentAllocations)
                .WithOne(x => x.SupplierPayment)
                .HasForeignKey(x => x.SupplierPaymentId)
                .OnDelete(DeleteBehavior.Restrict);

            //SupplierPaymentAllocation
            foreach (var money in new[] { "DueAmount", "AllocatedAmount" })
            {
                modelBuilder.Entity<SupplierPaymentAllocation>()
                    .Property(money)
                    .HasColumnType("decimal(18, 2)");
            }

            modelBuilder.Entity<SupplierPaymentAllocation>()
                .Property(x => x.Remarks)
                .HasMaxLength(500);

            modelBuilder.Entity<SupplierPaymentAllocation>()
                .HasOne(x => x.PurchaseInvoice)
                .WithMany()
                .HasForeignKey(x => x.PurchaseInvoiceId)
                .OnDelete(DeleteBehavior.Restrict);

            // One payment settles a given invoice once; paying it again is a second
            // payment, which is what makes instalments readable.
            modelBuilder.Entity<SupplierPaymentAllocation>()
                .HasIndex(x => new { x.SupplierPaymentId, x.PurchaseInvoiceId })
                .IsUnique();

            modelBuilder.Entity<SupplierPaymentAllocation>()
                .HasIndex(x => x.PurchaseInvoiceId);

            //RequestForQuotation
            modelBuilder.Entity<RequestForQuotation>()
                .Property(x => x.RfqNo)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<RequestForQuotation>()
                .Property(x => x.Notes)
                .HasMaxLength(1000);

            modelBuilder.Entity<RequestForQuotation>()
                .Property(x => x.CreatedBy)
                .HasMaxLength(200);

            modelBuilder.Entity<RequestForQuotation>()
                .HasIndex(x => x.RfqNo)
                .IsUnique();

            // The open requests are read one status at a time, because only a sent
            // request can be quoted against.
            modelBuilder.Entity<RequestForQuotation>()
                .HasIndex(x => new { x.Status, x.RfqDate });

            modelBuilder.Entity<RequestForQuotation>()
                .HasOne(x => x.PurchaseRequisition)
                .WithMany()
                .HasForeignKey(x => x.PurchaseRequisitionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RequestForQuotation>()
                .HasOne(x => x.BusinessLocation)
                .WithMany()
                .HasForeignKey(x => x.BusinessLocationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RequestForQuotation>()
                .HasMany(x => x.RequestForQuotationItems)
                .WithOne(x => x.RequestForQuotation)
                .HasForeignKey(x => x.RequestForQuotationId)
                .OnDelete(DeleteBehavior.Restrict);

            //RequestForQuotationItem
            modelBuilder.Entity<RequestForQuotationItem>()
                .Property(x => x.Quantity)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<RequestForQuotationItem>()
                .Property(x => x.Remarks)
                .HasMaxLength(500);

            modelBuilder.Entity<RequestForQuotationItem>()
                .HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RequestForQuotationItem>()
                .HasIndex(x => new { x.RequestForQuotationId, x.ProductId })
                .IsUnique();

            //SupplierQuotation
            modelBuilder.Entity<SupplierQuotation>()
                .Property(x => x.QuotationNo)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<SupplierQuotation>()
                .Property(x => x.SupplierQuotationNo)
                .HasMaxLength(100);

            modelBuilder.Entity<SupplierQuotation>()
                .Property(x => x.Notes)
                .HasMaxLength(1000);

            modelBuilder.Entity<SupplierQuotation>()
                .Property(x => x.CreatedBy)
                .HasMaxLength(200);

            foreach (var money in new[] { "SubTotal", "DiscountTotal", "OtherCharges", "GrandTotal" })
            {
                modelBuilder.Entity<SupplierQuotation>()
                    .Property(money)
                    .HasColumnType("decimal(18, 2)");
            }

            modelBuilder.Entity<SupplierQuotation>()
                .HasIndex(x => x.QuotationNo)
                .IsUnique();

            // The comparison reads every quotation of one request at a time.
            modelBuilder.Entity<SupplierQuotation>()
                .HasIndex(x => x.RequestForQuotationId);

            // A supplier answers a given request once; a revised price edits that
            // same quotation rather than adding a second one.
            modelBuilder.Entity<SupplierQuotation>()
                .HasIndex(x => new { x.RequestForQuotationId, x.SupplierId })
                .IsUnique();

            modelBuilder.Entity<SupplierQuotation>()
                .HasOne(x => x.RequestForQuotation)
                .WithMany()
                .HasForeignKey(x => x.RequestForQuotationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SupplierQuotation>()
                .HasOne(x => x.Supplier)
                .WithMany()
                .HasForeignKey(x => x.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SupplierQuotation>()
                .HasOne(x => x.PaymentTerm)
                .WithMany()
                .HasForeignKey(x => x.PaymentTermId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SupplierQuotation>()
                .HasMany(x => x.SupplierQuotationItems)
                .WithOne(x => x.SupplierQuotation)
                .HasForeignKey(x => x.SupplierQuotationId)
                .OnDelete(DeleteBehavior.Restrict);

            //SupplierQuotationItem
            foreach (var money in new[] { "Quantity", "UnitPrice", "DiscountAmount", "LineTotal" })
            {
                modelBuilder.Entity<SupplierQuotationItem>()
                    .Property(money)
                    .HasColumnType("decimal(18, 2)");
            }

            modelBuilder.Entity<SupplierQuotationItem>()
                .Property(x => x.Remarks)
                .HasMaxLength(500);

            modelBuilder.Entity<SupplierQuotationItem>()
                .HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SupplierQuotationItem>()
                .HasIndex(x => new { x.SupplierQuotationId, x.ProductId })
                .IsUnique();

            // The order keeps a pointer back to the quotation it was won on.
            modelBuilder.Entity<PurchaseOrder>()
                .HasOne(x => x.SupplierQuotation)
                .WithMany()
                .HasForeignKey(x => x.SupplierQuotationId)
                .OnDelete(DeleteBehavior.Restrict);
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
        public DbSet<Salesperson> Salespersons => Set<Salesperson>();
        public DbSet<SalesQuotation> SalesQuotations => Set<SalesQuotation>();
        public DbSet<SalesQuotationItem> SalesQuotationItems => Set<SalesQuotationItem>();
        public DbSet<SalesOrder> SalesOrders => Set<SalesOrder>();
        public DbSet<SalesOrderItem> SalesOrderItems => Set<SalesOrderItem>();
        public DbSet<StockReservation> StockReservations => Set<StockReservation>();
        public DbSet<StockReservationItem> StockReservationItems => Set<StockReservationItem>();
        public DbSet<SalesInvoice> SalesInvoices => Set<SalesInvoice>();
        public DbSet<SalesInvoiceItem> SalesInvoiceItems => Set<SalesInvoiceItem>();
        public DbSet<CustomerPayment> CustomerPayments => Set<CustomerPayment>();
        public DbSet<CustomerPaymentAllocation> CustomerPaymentAllocations => Set<CustomerPaymentAllocation>();
        public DbSet<SalesReturn> SalesReturns => Set<SalesReturn>();
        public DbSet<SalesReturnItem> SalesReturnItems => Set<SalesReturnItem>();
        public DbSet<CreditNote> CreditNotes => Set<CreditNote>();
        public DbSet<CreditNoteItem> CreditNoteItems => Set<CreditNoteItem>();
        public DbSet<SalesCommission> SalesCommissions => Set<SalesCommission>();

        //Purchase Management module DbSet<T>
        public DbSet<Supplier> Suppliers => Set<Supplier>();
        public DbSet<PurchaseRequisition> PurchaseRequisitions => Set<PurchaseRequisition>();
        public DbSet<PurchaseRequisitionItem> PurchaseRequisitionItems => Set<PurchaseRequisitionItem>();
        public DbSet<PurchaseOrder> PurchaseOrders => Set<PurchaseOrder>();
        public DbSet<PurchaseOrderItem> PurchaseOrderItems => Set<PurchaseOrderItem>();
        public DbSet<GoodsReceipt> GoodsReceipts => Set<GoodsReceipt>();
        public DbSet<GoodsReceiptItem> GoodsReceiptItems => Set<GoodsReceiptItem>();
        public DbSet<PurchaseInvoice> PurchaseInvoices => Set<PurchaseInvoice>();
        public DbSet<PurchaseInvoiceItem> PurchaseInvoiceItems => Set<PurchaseInvoiceItem>();
        public DbSet<PurchaseReturn> PurchaseReturns => Set<PurchaseReturn>();
        public DbSet<PurchaseReturnItem> PurchaseReturnItems => Set<PurchaseReturnItem>();
        public DbSet<SupplierPayment> SupplierPayments => Set<SupplierPayment>();
        public DbSet<SupplierPaymentAllocation> SupplierPaymentAllocations => Set<SupplierPaymentAllocation>();
        public DbSet<RequestForQuotation> RequestForQuotations => Set<RequestForQuotation>();
        public DbSet<RequestForQuotationItem> RequestForQuotationItems => Set<RequestForQuotationItem>();
        public DbSet<SupplierQuotation> SupplierQuotations => Set<SupplierQuotation>();
        public DbSet<SupplierQuotationItem> SupplierQuotationItems => Set<SupplierQuotationItem>();
    }
}