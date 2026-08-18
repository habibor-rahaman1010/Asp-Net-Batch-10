using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevSkill.Inventory.Infrastructure.Configurations
{
    /// <summary>
    /// How <see cref="GoodsReceipt"/> is mapped. Lifted out of the context so the
    /// context says what tables there are and each of these says what one table
    /// looks like.
    /// </summary>
    public class GoodsReceiptConfiguration : IEntityTypeConfiguration<GoodsReceipt>
    {
        public void Configure(EntityTypeBuilder<GoodsReceipt> builder)
        {

            //GoodsReceipt
            builder
                .Property(x => x.GoodsReceiptNo)
                .HasMaxLength(50)
                .IsRequired();


            builder
                .Property(x => x.SupplierChallanNo)
                .HasMaxLength(100);


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
                .HasIndex(x => x.GoodsReceiptNo)
                .IsUnique();


            // The receipt list is almost always read one purchase order at a time.
            builder
                .HasIndex(x => x.PurchaseOrderId);


            builder
                .HasIndex(x => new { x.SupplierId, x.ReceiptDate });


            builder
                .HasOne(x => x.PurchaseOrder)
                .WithMany()
                .HasForeignKey(x => x.PurchaseOrderId)
                .OnDelete(DeleteBehavior.Restrict);


            builder
                .HasOne(x => x.Supplier)
                .WithMany()
                .HasForeignKey(x => x.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);


            builder
                .HasOne(x => x.BusinessLocation)
                .WithMany()
                .HasForeignKey(x => x.BusinessLocationId)
                .OnDelete(DeleteBehavior.Restrict);


            builder
                .HasMany(x => x.GoodsReceiptItems)
                .WithOne(x => x.GoodsReceipt)
                .HasForeignKey(x => x.GoodsReceiptId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
