using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevSkill.Inventory.Infrastructure.Configurations
{
    /// <summary>
    /// How <see cref="SupplierQuotation"/> is mapped. Lifted out of the context so the
    /// context says what tables there are and each of these says what one table
    /// looks like.
    /// </summary>
    public class SupplierQuotationConfiguration : IEntityTypeConfiguration<SupplierQuotation>
    {
        public void Configure(EntityTypeBuilder<SupplierQuotation> builder)
        {

            //SupplierQuotation
            builder
                .Property(x => x.QuotationNo)
                .HasMaxLength(50)
                .IsRequired();


            builder
                .Property(x => x.SupplierQuotationNo)
                .HasMaxLength(100);


            builder
                .Property(x => x.Notes)
                .HasMaxLength(1000);


            builder
                .Property(x => x.CreatedBy)
                .HasMaxLength(200);


            foreach (var money in new[] { "SubTotal", "DiscountTotal", "OtherCharges", "GrandTotal" })
            {
                builder
                    .Property(money)
                    .HasColumnType("decimal(18, 2)");
            }


            builder
                .HasIndex(x => x.QuotationNo)
                .IsUnique();


            // The comparison reads every quotation of one request at a time.
            builder
                .HasIndex(x => x.RequestForQuotationId);


            // A supplier answers a given request once; a revised price edits that
            // same quotation rather than adding a second one.
            builder
                .HasIndex(x => new { x.RequestForQuotationId, x.SupplierId })
                .IsUnique();


            builder
                .HasOne(x => x.RequestForQuotation)
                .WithMany()
                .HasForeignKey(x => x.RequestForQuotationId)
                .OnDelete(DeleteBehavior.Restrict);


            builder
                .HasOne(x => x.Supplier)
                .WithMany()
                .HasForeignKey(x => x.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);


            builder
                .HasOne(x => x.PaymentTerm)
                .WithMany()
                .HasForeignKey(x => x.PaymentTermId)
                .OnDelete(DeleteBehavior.Restrict);


            builder
                .HasMany(x => x.SupplierQuotationItems)
                .WithOne(x => x.SupplierQuotation)
                .HasForeignKey(x => x.SupplierQuotationId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
