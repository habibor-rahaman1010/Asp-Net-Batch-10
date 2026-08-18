using DevSkill.Inventory.Domain.Entities.StockTransferEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevSkill.Inventory.Infrastructure.Configurations
{
    /// <summary>
    /// How <see cref="StockTransferItem"/> is mapped. Lifted out of the context so the
    /// context says what tables there are and each of these says what one table
    /// looks like.
    /// </summary>
    public class StockTransferItemConfiguration : IEntityTypeConfiguration<StockTransferItem>
    {
        public void Configure(EntityTypeBuilder<StockTransferItem> builder)
        {

            //StockTransferItem
            builder
                .Property(x => x.Quantity)
                .HasColumnType("decimal(18, 2)");
        }
    }
}
