using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevSkill.Inventory.Infrastructure.Configurations
{
    /// <summary>
    /// How <see cref="SalesReturn"/> is mapped. Lifted out of the context so the
    /// context says what tables there are and each of these says what one table
    /// looks like.
    /// </summary>
    public class SalesReturnConfiguration : IEntityTypeConfiguration<SalesReturn>
    {
        public void Configure(EntityTypeBuilder<SalesReturn> builder)
        {

            //SalesReturn
            builder
                .Property(x => x.ReturnNo)
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


            builder
                .Property(x => x.TotalAmount)
                .HasColumnType("decimal(18, 2)");


            builder
                .HasIndex(x => x.ReturnNo)
                .IsUnique();


            // A return is almost always read one invoice at a time, because that is
            // what caps the quantity that may come back.
            builder
                .HasIndex(x => x.SalesInvoiceId);


            builder
                .HasIndex(x => new { x.CustomerId, x.ReturnDate });


            builder
                .HasOne(x => x.SalesInvoice)
                .WithMany()
                .HasForeignKey(x => x.SalesInvoiceId)
                .OnDelete(DeleteBehavior.Restrict);


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
                .HasMany(x => x.SalesReturnItems)
                .WithOne(x => x.SalesReturn)
                .HasForeignKey(x => x.SalesReturnId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
