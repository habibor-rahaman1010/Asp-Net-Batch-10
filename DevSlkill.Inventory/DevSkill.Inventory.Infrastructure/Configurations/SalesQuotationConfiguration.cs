using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevSkill.Inventory.Infrastructure.Configurations
{
    /// <summary>
    /// How <see cref="SalesQuotation"/> is mapped. Lifted out of the context so the
    /// context says what tables there are and each of these says what one table
    /// looks like.
    /// </summary>
    public class SalesQuotationConfiguration : IEntityTypeConfiguration<SalesQuotation>
    {
        public void Configure(EntityTypeBuilder<SalesQuotation> builder)
        {

            //SalesQuotation
            builder
                .Property(x => x.QuotationNo)
                .HasMaxLength(50)
                .IsRequired();


            builder
                .Property(x => x.CustomerReference)
                .HasMaxLength(100);


            builder
                .Property(x => x.Notes)
                .HasMaxLength(1000);


            builder
                .Property(x => x.CreatedBy)
                .HasMaxLength(200);


            foreach (var money in new[] { "SubTotal", "ItemDiscountTotal", "TotalTax", "OtherCharges", "GrandTotal" })
            {
                builder
                    .Property(money)
                    .HasColumnType("decimal(18, 2)");
            }


            builder
                .HasIndex(x => x.QuotationNo)
                .IsUnique();


            builder
                .HasIndex(x => new { x.CustomerId, x.QuotationDate });


            builder
                .HasIndex(x => new { x.Status, x.QuotationDate });


            builder
                .HasOne(x => x.Customer)
                .WithMany()
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);


            builder
                .HasOne(x => x.BusinessLocation)
                .WithMany()
                .HasForeignKey(x => x.BusinessLocationId)
                .OnDelete(DeleteBehavior.Restrict);


            builder
                .HasOne(x => x.PaymentTerm)
                .WithMany()
                .HasForeignKey(x => x.PaymentTermId)
                .OnDelete(DeleteBehavior.Restrict);


            builder
                .HasOne(x => x.Salesperson)
                .WithMany()
                .HasForeignKey(x => x.SalespersonId)
                .OnDelete(DeleteBehavior.Restrict);


            builder
                .HasMany(x => x.SalesQuotationItems)
                .WithOne(x => x.SalesQuotation)
                .HasForeignKey(x => x.SalesQuotationId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
