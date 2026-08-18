using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevSkill.Inventory.Infrastructure.Configurations
{
    /// <summary>
    /// How <see cref="PurchaseReturn"/> is mapped. Lifted out of the context so the
    /// context says what tables there are and each of these says what one table
    /// looks like.
    /// </summary>
    public class PurchaseReturnConfiguration : IEntityTypeConfiguration<PurchaseReturn>
    {
        public void Configure(EntityTypeBuilder<PurchaseReturn> builder)
        {

            //PurchaseReturn
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


            // A return is almost always read one goods receipt at a time, because that
            // is what caps the quantity that may go back.
            builder
                .HasIndex(x => x.GoodsReceiptId);


            // The supplier ledger reads the returns of one supplier over a period.
            builder
                .HasIndex(x => new { x.SupplierId, x.ReturnDate });


            builder
                .HasOne(x => x.GoodsReceipt)
                .WithMany()
                .HasForeignKey(x => x.GoodsReceiptId)
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
                .HasMany(x => x.PurchaseReturnItems)
                .WithOne(x => x.PurchaseReturn)
                .HasForeignKey(x => x.PurchaseReturnId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
