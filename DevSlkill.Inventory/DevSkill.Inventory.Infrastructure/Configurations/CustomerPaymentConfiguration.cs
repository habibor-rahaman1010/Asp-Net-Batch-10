using DevSkill.Inventory.Domain.Entities.SalesEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevSkill.Inventory.Infrastructure.Configurations
{
    /// <summary>
    /// How <see cref="CustomerPayment"/> is mapped. Lifted out of the context so the
    /// context says what tables there are and each of these says what one table
    /// looks like.
    /// </summary>
    public class CustomerPaymentConfiguration : IEntityTypeConfiguration<CustomerPayment>
    {
        public void Configure(EntityTypeBuilder<CustomerPayment> builder)
        {

            //CustomerPayment
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


            // The customer ledger reads the collections of one customer over a period.
            builder
                .HasIndex(x => new { x.CustomerId, x.PaymentDate });


            builder
                .HasIndex(x => x.CreditNoteId);


            builder
                .HasOne(x => x.Customer)
                .WithMany()
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);


            builder
                .HasOne(x => x.CreditNote)
                .WithMany()
                .HasForeignKey(x => x.CreditNoteId)
                .OnDelete(DeleteBehavior.Restrict);


            builder
                .HasMany(x => x.CustomerPaymentAllocations)
                .WithOne(x => x.CustomerPayment)
                .HasForeignKey(x => x.CustomerPaymentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
