using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevSkill.Inventory.Infrastructure.Configurations
{
    /// <summary>
    /// How <see cref="GoodsReceiptItem"/> is mapped. Lifted out of the context so the
    /// context says what tables there are and each of these says what one table
    /// looks like.
    /// </summary>
    public class GoodsReceiptItemConfiguration : IEntityTypeConfiguration<GoodsReceiptItem>
    {
        public void Configure(EntityTypeBuilder<GoodsReceiptItem> builder)
        {

            //GoodsReceiptItem
            foreach (var money in new[] { "OrderedQuantity", "ReceivedQuantity", "RejectedQuantity",
                                          "ReturnedQuantity", "UnitPrice" })
            {
                builder
                    .Property(money)
                    .HasColumnType("decimal(18, 2)");
            }


            builder
                .Property(x => x.Remarks)
                .HasMaxLength(500);


            builder
                .HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);


            builder
                .HasIndex(x => new { x.GoodsReceiptId, x.ProductId })
                .IsUnique();
        }
    }
}
