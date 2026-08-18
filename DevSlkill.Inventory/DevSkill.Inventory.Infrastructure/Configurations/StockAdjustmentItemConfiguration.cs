using DevSkill.Inventory.Domain.Entities.StockAdjustmentEntites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevSkill.Inventory.Infrastructure.Configurations
{
    /// <summary>
    /// How <see cref="StockAdjustmentItem"/> is mapped. Lifted out of the context so the
    /// context says what tables there are and each of these says what one table
    /// looks like.
    /// </summary>
    public class StockAdjustmentItemConfiguration : IEntityTypeConfiguration<StockAdjustmentItem>
    {
        public void Configure(EntityTypeBuilder<StockAdjustmentItem> builder)
        {

            //StockAdjustmentItem
            builder
                .Property(x => x.UnitPrice)
                .HasColumnType("decimal(18, 2)");


            builder
                .Property(x => x.TotalAmount)
                .HasColumnType("decimal(18, 2)");
        }
    }
}
