using DevSkill.Inventory.Domain.Entities.SalesEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevSkill.Inventory.Infrastructure.Configurations
{
    /// <summary>
    /// How <see cref="CreditNote"/> is mapped. Lifted out of the context so the
    /// context says what tables there are and each of these says what one table
    /// looks like.
    /// </summary>
    public class CreditNoteConfiguration : IEntityTypeConfiguration<CreditNote>
    {
        public void Configure(EntityTypeBuilder<CreditNote> builder)
        {

            //CreditNote
            builder
                .Property(x => x.CreditNoteNo)
                .HasMaxLength(50)
                .IsRequired();


            builder
                .Property(x => x.Reason)
                .HasMaxLength(500);


            builder
                .Property(x => x.Notes)
                .HasMaxLength(1000);


            builder
                .Property(x => x.CreatedBy)
                .HasMaxLength(200);


            foreach (var money in new[] { "SubTotal", "TotalTax", "TotalAmount", "AppliedAmount" })
            {
                builder
                    .Property(money)
                    .HasColumnType("decimal(18, 2)");
            }


            builder
                .HasIndex(x => x.CreditNoteNo)
                .IsUnique();


            builder
                .HasIndex(x => new { x.CustomerId, x.CreditNoteDate });


            builder
                .HasIndex(x => new { x.Status, x.CreditNoteDate });


            // A return raises exactly one credit note, which is what keeps the goods
            // that came back and the credit that was given for them in step.
            builder
                .HasIndex(x => x.SalesReturnId)
                .IsUnique()
                .HasFilter("[SalesReturnId] IS NOT NULL");


            builder
                .HasOne(x => x.Customer)
                .WithMany()
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);


            builder
                .HasOne(x => x.SalesReturn)
                .WithMany()
                .HasForeignKey(x => x.SalesReturnId)
                .OnDelete(DeleteBehavior.Restrict);


            builder
                .HasOne(x => x.SalesInvoice)
                .WithMany()
                .HasForeignKey(x => x.SalesInvoiceId)
                .OnDelete(DeleteBehavior.Restrict);


            builder
                .HasMany(x => x.CreditNoteItems)
                .WithOne(x => x.CreditNote)
                .HasForeignKey(x => x.CreditNoteId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
