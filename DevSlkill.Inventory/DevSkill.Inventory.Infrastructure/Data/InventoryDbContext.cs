using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Entities.StockAdjustmentEntites;
using DevSkill.Inventory.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Unit)
                .WithMany()
                .HasForeignKey(p => p.UnitId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Brand)
                .WithMany()
                .HasForeignKey(p => p.BrandId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany()
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Subcategory)
                .WithMany()
                .HasForeignKey(p => p.SubcategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.BusinessLocation)
                .WithMany()
                .HasForeignKey(p => p.BusinessLocationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Warranty)
                .WithMany()
                .HasForeignKey(p => p.WarrantyId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.ProductType)
                .WithMany()
                .HasForeignKey(p => p.ProductTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.ApplicableTax)
                .WithMany()
                .HasForeignKey(p => p.ApplicableTaxId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.SellingPriceTax)
                .WithMany()
                .HasForeignKey(p => p.SellingPriceTaxId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<StockAdjustment>()
                .Property(sa => sa.TotalAmountRecover)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<StockAdjustment>()
                .Property(sa => sa.TotalAmount)
                .HasColumnType("decimal(18, 2)");


            // StockAdjustment and Product relationship
            modelBuilder.Entity<StockAdjustment>()
                .HasOne(sa => sa.Product)
                .WithMany()
                .HasForeignKey(sa => sa.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

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

            base.OnModelCreating(modelBuilder);
        }

        //This is my project related DbSet<T>

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; } 
        public DbSet<Brand> Brands { get; set; }          
        public DbSet<Unit> Units { get; set; }         
        public DbSet<BarcodeType> BarcodeTypes { get; set; } 
        public DbSet<BusinessLocation> BusinessLocations { get; set; } 
        public DbSet<Warranty> Warranties { get; set; }
        public DbSet<SellingPriceTax> SellingPriceTaxes { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ApplicableTax> ApplicableTaxs { get; set; }
        public DbSet<AdjustmentType> AdjustmentTypes { get; set; }
        public DbSet<StockAdjustment> StockAdjustments { get; set; }
    }
}
