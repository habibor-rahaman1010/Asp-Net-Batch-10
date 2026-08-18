using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevSkill.Inventory.Infrastructure.Configurations
{
    /// <summary>
    /// How <see cref="PriceListItem"/> is mapped. Lifted out of the context so the
    /// context says what tables there are and each of these says what one table
    /// looks like.
    /// </summary>
    public class PriceListItemConfiguration : IEntityTypeConfiguration<PriceListItem>
    {
        public void Configure(EntityTypeBuilder<PriceListItem> builder)
        {

            //PriceListItem
            builder
                .Property(x => x.UnitPrice)
                .HasColumnType("decimal(18, 2)");


            builder
                .Property(x => x.MinQuantity)
                .HasColumnType("decimal(18, 2)");


            builder
                .HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);


            builder
                .HasIndex(x => new { x.PriceListId, x.ProductId, x.MinQuantity })
                .IsUnique();
        }
    }
}
