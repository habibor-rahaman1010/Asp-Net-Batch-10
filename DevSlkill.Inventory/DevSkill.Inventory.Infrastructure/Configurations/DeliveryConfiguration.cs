using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevSkill.Inventory.Infrastructure.Configurations
{
    /// <summary>
    /// How <see cref="Delivery"/> is mapped. Lifted out of the context so the
    /// context says what tables there are and each of these says what one table
    /// looks like.
    /// </summary>
    public class DeliveryConfiguration : IEntityTypeConfiguration<Delivery>
    {
        public void Configure(EntityTypeBuilder<Delivery> builder)
        {

            //Delivery
            builder
                .Property(x => x.DeliveryNo)
                .HasMaxLength(50)
                .IsRequired();


            builder
                .Property(x => x.ReceivedBy)
                .HasMaxLength(200);


            builder
                .Property(x => x.Notes)
                .HasMaxLength(1000);


            builder
                .Property(x => x.CreatedBy)
                .HasMaxLength(200);


            builder
                .HasIndex(x => x.DeliveryNo)
                .IsUnique();


            // The delivery list is almost always read one proforma invoice at a time.
            builder
                .HasIndex(x => x.ProformaInvoiceId);


            builder
                .HasIndex(x => new { x.CustomerId, x.DeliveryDate });


            // A shipment raised off a sales order is read one order at a time, exactly
            // the way the proforma side is.
            builder
                .HasIndex(x => x.SalesOrderId);


            builder
                .HasOne(x => x.ProformaInvoice)
                .WithMany()
                .HasForeignKey(x => x.ProformaInvoiceId)
                .OnDelete(DeleteBehavior.Restrict);


            builder
                .HasOne(x => x.SalesOrder)
                .WithMany()
                .HasForeignKey(x => x.SalesOrderId)
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
                .HasMany(x => x.DeliveryItems)
                .WithOne(x => x.Delivery)
                .HasForeignKey(x => x.DeliveryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
