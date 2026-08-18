using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevSkill.Inventory.Infrastructure.Configurations
{
    /// <summary>
    /// How <see cref="StockReservation"/> is mapped. Lifted out of the context so the
    /// context says what tables there are and each of these says what one table
    /// looks like.
    /// </summary>
    public class StockReservationConfiguration : IEntityTypeConfiguration<StockReservation>
    {
        public void Configure(EntityTypeBuilder<StockReservation> builder)
        {

            //StockReservation
            builder
                .Property(x => x.ReservationNo)
                .HasMaxLength(50)
                .IsRequired();


            builder
                .Property(x => x.Notes)
                .HasMaxLength(1000);


            builder
                .Property(x => x.CreatedBy)
                .HasMaxLength(200);


            builder
                .HasIndex(x => x.ReservationNo)
                .IsUnique();


            // The reservation list is almost always read one sales order at a time.
            builder
                .HasIndex(x => x.SalesOrderId);


            // The expiry sweep reads active holds by the date they run out on.
            builder
                .HasIndex(x => new { x.Status, x.ExpiryDate });


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
                .HasMany(x => x.StockReservationItems)
                .WithOne(x => x.StockReservation)
                .HasForeignKey(x => x.StockReservationId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
