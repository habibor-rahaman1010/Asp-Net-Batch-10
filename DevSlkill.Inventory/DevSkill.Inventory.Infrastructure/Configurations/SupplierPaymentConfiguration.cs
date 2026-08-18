using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevSkill.Inventory.Infrastructure.Configurations
{
    /// <summary>
    /// How <see cref="SupplierPayment"/> is mapped. Lifted out of the context so the
    /// context says what tables there are and each of these says what one table
    /// looks like.
    /// </summary>
    public class SupplierPaymentConfiguration : IEntityTypeConfiguration<SupplierPayment>
    {
        public void Configure(EntityTypeBuilder<SupplierPayment> builder)
        {

            //SupplierPayment
            builder
                .Property(x => x.PaymentNo)
                .HasMaxLength(50)
                .IsRequired();


            builder
                .Property(x => x.ReferenceNo)
                .HasMaxLength(100);


            builder
                .Property(x => x.Notes)
                .HasMaxLength(1000);


            builder
                .Property(x => x.CreatedBy)
                .HasMaxLength(200);


            foreach (var money in new[] { "Amount", "AllocatedAmount" })
            {
                builder
                    .Property(money)
                    .HasColumnType("decimal(18, 2)");
            }


            builder
                .HasIndex(x => x.PaymentNo)
                .IsUnique();


            // The supplier ledger reads the payments of one supplier over a period.
            builder
                .HasIndex(x => new { x.SupplierId, x.PaymentDate });


            builder
                .HasOne(x => x.Supplier)
                .WithMany()
                .HasForeignKey(x => x.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);


            builder
                .HasMany(x => x.SupplierPaymentAllocations)
                .WithOne(x => x.SupplierPayment)
                .HasForeignKey(x => x.SupplierPaymentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
