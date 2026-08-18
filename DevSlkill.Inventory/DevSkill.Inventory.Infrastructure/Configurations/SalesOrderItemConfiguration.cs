using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevSkill.Inventory.Infrastructure.Configurations
{
    /// <summary>
    /// How <see cref="SalesOrderItem"/> is mapped. Lifted out of the context so the
    /// context says what tables there are and each of these says what one table
    /// looks like.
    /// </summary>
    public class SalesOrderItemConfiguration : IEntityTypeConfiguration<SalesOrderItem>
    {
        public void Configure(EntityTypeBuilder<SalesOrderItem> builder)
        {

            //SalesOrderItem
            foreach (var money in new[] { "Quantity", "UnitPrice", "DiscountAmount", "TaxRate",
                                          "TaxAmount", "LineTotal", "DeliveredQuantity", "InvoicedQuantity" })
            {
                builder
                    .Property(money)
                    .HasColumnType("decimal(18, 2)");
            }


            builder
                .HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);


            // One product may appear only once on an order, so the delivered and
            // invoiced quantities of a line are never split over two rows.
            builder
                .HasIndex(x => new { x.SalesOrderId, x.ProductId })
                .IsUnique();
        }
    }
}
