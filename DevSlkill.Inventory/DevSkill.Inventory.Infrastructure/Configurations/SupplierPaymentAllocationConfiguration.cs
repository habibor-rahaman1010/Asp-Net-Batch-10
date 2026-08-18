using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevSkill.Inventory.Infrastructure.Configurations
{
    /// <summary>
    /// How <see cref="SupplierPaymentAllocation"/> is mapped. Lifted out of the context so the
    /// context says what tables there are and each of these says what one table
    /// looks like.
    /// </summary>
    public class SupplierPaymentAllocationConfiguration : IEntityTypeConfiguration<SupplierPaymentAllocation>
    {
        public void Configure(EntityTypeBuilder<SupplierPaymentAllocation> builder)
        {

            //SupplierPaymentAllocation
            foreach (var money in new[] { "DueAmount", "AllocatedAmount" })
            {
                builder
                    .Property(money)
                    .HasColumnType("decimal(18, 2)");
            }


            builder
                .Property(x => x.Remarks)
                .HasMaxLength(500);


            builder
                .HasOne(x => x.PurchaseInvoice)
                .WithMany()
                .HasForeignKey(x => x.PurchaseInvoiceId)
                .OnDelete(DeleteBehavior.Restrict);


            // One payment settles a given invoice once; paying it again is a second
            // payment, which is what makes instalments readable.
            builder
                .HasIndex(x => new { x.SupplierPaymentId, x.PurchaseInvoiceId })
                .IsUnique();


            builder
                .HasIndex(x => x.PurchaseInvoiceId);
        }
    }
}
