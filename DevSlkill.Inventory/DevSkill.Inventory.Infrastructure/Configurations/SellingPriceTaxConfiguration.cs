using DevSkill.Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevSkill.Inventory.Infrastructure.Configurations
{
    /// <summary>
    /// How <see cref="SellingPriceTax"/> is mapped. Lifted out of the context so the
    /// context says what tables there are and each of these says what one table
    /// looks like.
    /// </summary>
    public class SellingPriceTaxConfiguration : IEntityTypeConfiguration<SellingPriceTax>
    {
        public void Configure(EntityTypeBuilder<SellingPriceTax> builder)
        {

            builder
                .Property(x => x.TaxRate)
                .HasColumnType("decimal(18, 2)");


            builder.HasData(
                new SellingPriceTax { Id = new Guid("2b97a364-d5b7-4a47-8407-f03f83cc7ae7"), SellingPriceTaxName = "Exclusive" },
                new SellingPriceTax { Id = new Guid("4c9b1237-e114-4908-b4e7-a0a8f62eda91"), SellingPriceTaxName = "Inclusive" },
                new SellingPriceTax { Id = new Guid("bca33a0a-7290-4284-adfc-4d5b25413e15"), SellingPriceTaxName = "Zero Rate" }
            );
        }
    }
}
