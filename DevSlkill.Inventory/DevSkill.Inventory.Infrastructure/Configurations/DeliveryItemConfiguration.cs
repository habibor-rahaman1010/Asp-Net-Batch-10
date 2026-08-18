using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevSkill.Inventory.Infrastructure.Configurations
{
    /// <summary>
    /// How <see cref="DeliveryItem"/> is mapped. Lifted out of the context so the
    /// context says what tables there are and each of these says what one table
    /// looks like.
    /// </summary>
    public class DeliveryItemConfiguration : IEntityTypeConfiguration<DeliveryItem>
    {
        public void Configure(EntityTypeBuilder<DeliveryItem> builder)
        {

            //DeliveryItem
            builder
                .Property(x => x.OrderedQuantity)
                .HasColumnType("decimal(18, 2)");


            builder
                .Property(x => x.DeliveredQuantity)
                .HasColumnType("decimal(18, 2)");


            builder
                .HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);


            // One product may appear only once inside a single shipment, so the
            // delivered quantity of a line is never split over two rows.
            builder
                .HasIndex(x => new { x.DeliveryId, x.ProductId })
                .IsUnique();
        }
    }
}
