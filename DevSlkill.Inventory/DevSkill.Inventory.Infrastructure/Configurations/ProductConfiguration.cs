using DevSkill.Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevSkill.Inventory.Infrastructure.Configurations
{
    /// <summary>
    /// How <see cref="Product"/> is mapped. Lifted out of the context so the
    /// context says what tables there are and each of these says what one table
    /// looks like.
    /// </summary>
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {


            //Here relation product to other tables...
            builder
                .HasOne(p => p.BarcodeType)
                .WithMany()
                .HasForeignKey(p => p.BarcodeTypeId)
                .OnDelete(DeleteBehavior.Cascade);


            builder
                .HasOne(p => p.Unit)
                .WithMany()
                .HasForeignKey(p => p.UnitId)
                .OnDelete(DeleteBehavior.Cascade);


            builder
                .HasOne(p => p.Brand)
                .WithMany()
                .HasForeignKey(p => p.BrandId)
                .OnDelete(DeleteBehavior.Cascade);


            builder
                .HasOne(p => p.Category)
                .WithMany()
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);


            builder
                .HasOne(p => p.Subcategory)
                .WithMany()
                .HasForeignKey(p => p.SubcategoryId)
                .OnDelete(DeleteBehavior.Cascade);


            builder
                .HasOne(p => p.BusinessLocation)
                .WithMany()
                .HasForeignKey(p => p.BusinessLocationId)
                .OnDelete(DeleteBehavior.Cascade);


            builder
                .HasOne(p => p.Warranty)
                .WithMany()
                .HasForeignKey(p => p.WarrantyId)
                .OnDelete(DeleteBehavior.Cascade);


            builder
                .HasOne(p => p.ProductType)
                .WithMany()
                .HasForeignKey(p => p.ProductTypeId)
                .OnDelete(DeleteBehavior.Cascade);


            builder
                .HasOne(p => p.ApplicableTax)
                .WithMany()
                .HasForeignKey(p => p.ApplicableTaxId)
                .OnDelete(DeleteBehavior.Cascade);


            builder
                .HasOne(p => p.SellingPriceTax)
                .WithMany()
                .HasForeignKey(p => p.SellingPriceTaxId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
