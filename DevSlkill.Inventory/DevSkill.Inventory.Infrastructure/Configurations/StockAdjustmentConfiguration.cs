using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Entities.StockAdjustmentEntites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevSkill.Inventory.Infrastructure.Configurations
{
    /// <summary>
    /// How <see cref="StockAdjustment"/> is mapped. Lifted out of the context so the
    /// context says what tables there are and each of these says what one table
    /// looks like.
    /// </summary>
    public class StockAdjustmentConfiguration : IEntityTypeConfiguration<StockAdjustment>
    {
        public void Configure(EntityTypeBuilder<StockAdjustment> builder)
        {


            builder
                .Property(sa => sa.TotalAmountRecover)
                .HasColumnType("decimal(18, 2)");


            builder
                .Property(sa => sa.TotalAmount)
                .HasColumnType("decimal(18, 2)");


            // StockAdjustment and BusinessLocation relationship
            builder
                .HasOne(sa => sa.BusinessLocation)
                .WithMany()
                .HasForeignKey(sa => sa.BusinessLocationId)
                .OnDelete(DeleteBehavior.Restrict); 


            // StockAdjustment and AdjustmentType relationship
            builder
                .HasOne(sa => sa.AdjustmentType)
                .WithMany()
                .HasForeignKey(sa => sa.AdjustmentTypeId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete


            builder
                .HasMany(x => x.StockAdjustmentItems)
                .WithOne(x => x.StockAdjustment)
                .HasForeignKey(x => x.StockAdjustmentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
