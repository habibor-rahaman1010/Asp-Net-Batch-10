using DevSkill.Inventory.Domain.Entities;
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
            //seed data
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

            modelBuilder.Entity<Subcategory>().HasData(
                new Subcategory { Id = Guid.NewGuid(), SubcategoryName = "Smartphones" },
                new Subcategory { Id = Guid.NewGuid(), SubcategoryName = "Laptops" },
                new Subcategory { Id = Guid.NewGuid(), SubcategoryName = "Televisions" }
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

            // Define entity relationships
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.HasOne(p => p.Category)
                      .WithMany()
                      .HasForeignKey("CategoryId");

                entity.HasOne(p => p.Subcategory)
                      .WithMany()
                      .HasForeignKey("SubcategoryId");

                entity.HasOne(p => p.Brand)
                      .WithMany()
                      .HasForeignKey("BrandId");

                entity.HasOne(p => p.Unit)
                      .WithMany()
                      .HasForeignKey("UnitId");

                entity.HasOne(p => p.Warranty)
                      .WithMany()
                      .HasForeignKey("WarrantyId");

                entity.HasOne(p => p.BarcodeType)
                      .WithMany()
                      .HasForeignKey("BarcodeTypeId");

                entity.HasOne(p => p.BusinessLocation)
                      .WithMany()
                      .HasForeignKey("BusinessLocationId");

                entity.HasOne(p => p.SellingPriceTax)
                      .WithMany()
                      .HasForeignKey("SellingPriceTaxId");

                entity.HasOne(p => p.ProductType)
                      .WithMany()
                      .HasForeignKey("ProductTypeId");

                entity.HasOne(p => p.ApplicableTax)
                      .WithMany()
                      .HasForeignKey("ApplicableTaxId");
            });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; } 
        public DbSet<Brand> Brands { get; set; }          
        public DbSet<Unit> Units { get; set; }         
        public DbSet<BarcodeType> BarcodeTypes { get; set; } 
        public DbSet<BusinessLocation> BusinessLocations { get; set; } 
        public DbSet<Warranty> Warranties { get; set; }
        public DbSet<SellingPriceTax> SellingPriceTaxes { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ApplicableTax> ApplicableTaxs { get; set; }
    }
}
