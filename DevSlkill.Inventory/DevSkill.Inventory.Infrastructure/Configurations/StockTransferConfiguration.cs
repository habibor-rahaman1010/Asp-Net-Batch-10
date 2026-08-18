using DevSkill.Inventory.Domain.Entities.StockTransferEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevSkill.Inventory.Infrastructure.Configurations
{
    /// <summary>
    /// How <see cref="StockTransfer"/> is mapped. Lifted out of the context so the
    /// context says what tables there are and each of these says what one table
    /// looks like.
    /// </summary>
    public class StockTransferConfiguration : IEntityTypeConfiguration<StockTransfer>
    {
        public void Configure(EntityTypeBuilder<StockTransfer> builder)
        {

            // StockTransfer and warehouse relationship
            builder
                .HasOne(x => x.FromWarehouse)
                .WithMany()
                .HasForeignKey(x => x.FromWarehouseId)
                .OnDelete(DeleteBehavior.Restrict);


            // StockTransfer and warehouse relationship
            builder
                .HasOne(x => x.ToWarehouse)
                .WithMany()
                .HasForeignKey(x => x.ToWarehouseId)
                .OnDelete(DeleteBehavior.Restrict);


            builder
                .HasMany(x => x.StockTransferItems)
                .WithOne(x => x.StockTransfer)
                .HasForeignKey(x => x.StockTransferId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
