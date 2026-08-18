using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevSkill.Inventory.Infrastructure.Configurations
{
    /// <summary>
    /// How <see cref="PurchaseRequisitionItem"/> is mapped. Lifted out of the context so the
    /// context says what tables there are and each of these says what one table
    /// looks like.
    /// </summary>
    public class PurchaseRequisitionItemConfiguration : IEntityTypeConfiguration<PurchaseRequisitionItem>
    {
        public void Configure(EntityTypeBuilder<PurchaseRequisitionItem> builder)
        {

            //PurchaseRequisitionItem
            builder
                .Property(x => x.Quantity)
                .HasColumnType("decimal(18, 2)");


            builder
                .Property(x => x.EstimatedUnitPrice)
                .HasColumnType("decimal(18, 2)");


            builder
                .Property(x => x.OrderedQuantity)
                .HasColumnType("decimal(18, 2)");


            builder
                .Property(x => x.Remarks)
                .HasMaxLength(500);


            builder
                .HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);


            // One product may appear only once on a requisition, so its quantity is
            // never split over two rows.
            builder
                .HasIndex(x => new { x.PurchaseRequisitionId, x.ProductId })
                .IsUnique();
        }
    }
}
