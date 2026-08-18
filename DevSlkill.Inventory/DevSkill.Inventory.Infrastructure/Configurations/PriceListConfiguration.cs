using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevSkill.Inventory.Infrastructure.Configurations
{
    /// <summary>
    /// How <see cref="PriceList"/> is mapped. Lifted out of the context so the
    /// context says what tables there are and each of these says what one table
    /// looks like.
    /// </summary>
    public class PriceListConfiguration : IEntityTypeConfiguration<PriceList>
    {
        public void Configure(EntityTypeBuilder<PriceList> builder)
        {

            //PriceList
            builder
                .Property(x => x.PriceListName)
                .HasMaxLength(150)
                .IsRequired();


            builder
                .HasIndex(x => x.PriceListName)
                .IsUnique();


            builder
                .HasIndex(x => new { x.CustomerType, x.IsActive, x.EffectiveFrom });


            builder
                .HasMany(x => x.PriceListItems)
                .WithOne(x => x.PriceList)
                .HasForeignKey(x => x.PriceListId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
