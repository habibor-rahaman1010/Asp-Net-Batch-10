using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevSkill.Inventory.Infrastructure.Configurations
{
    /// <summary>
    /// How <see cref="PurchaseOrderItem"/> is mapped. Lifted out of the context so the
    /// context says what tables there are and each of these says what one table
    /// looks like.
    /// </summary>
    public class PurchaseOrderItemConfiguration : IEntityTypeConfiguration<PurchaseOrderItem>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrderItem> builder)
        {

            //PurchaseOrderItem
            foreach (var money in new[] { "Quantity", "UnitPrice", "DiscountAmount", "TaxRate",
                                          "TaxAmount", "LineTotal", "ReceivedQuantity", "InvoicedQuantity" })
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


            // One product may appear only once on an order, so the received and
            // invoiced quantities of a line are never split over two rows.
            builder
                .HasIndex(x => new { x.PurchaseOrderId, x.ProductId })
                .IsUnique();
        }
    }
}
