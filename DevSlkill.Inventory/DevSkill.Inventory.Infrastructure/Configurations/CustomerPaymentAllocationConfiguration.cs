using DevSkill.Inventory.Domain.Entities.SalesEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevSkill.Inventory.Infrastructure.Configurations
{
    /// <summary>
    /// How <see cref="CustomerPaymentAllocation"/> is mapped. Lifted out of the context so the
    /// context says what tables there are and each of these says what one table
    /// looks like.
    /// </summary>
    public class CustomerPaymentAllocationConfiguration : IEntityTypeConfiguration<CustomerPaymentAllocation>
    {
        public void Configure(EntityTypeBuilder<CustomerPaymentAllocation> builder)
        {

            //CustomerPaymentAllocation
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
                .HasOne(x => x.SalesInvoice)
                .WithMany()
                .HasForeignKey(x => x.SalesInvoiceId)
                .OnDelete(DeleteBehavior.Restrict);


            // One collection settles a given invoice once; collecting again is a second
            // collection, which is what makes instalments readable.
            builder
                .HasIndex(x => new { x.CustomerPaymentId, x.SalesInvoiceId })
                .IsUnique();


            builder
                .HasIndex(x => x.SalesInvoiceId);
        }
    }
}
