using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevSkill.Inventory.Infrastructure.Configurations
{
    /// <summary>
    /// How <see cref="DiscountRule"/> is mapped. Lifted out of the context so the
    /// context says what tables there are and each of these says what one table
    /// looks like.
    /// </summary>
    public class DiscountRuleConfiguration : IEntityTypeConfiguration<DiscountRule>
    {
        public void Configure(EntityTypeBuilder<DiscountRule> builder)
        {

            //DiscountRule
            builder
                .Property(x => x.RuleName)
                .HasMaxLength(150)
                .IsRequired();


            builder
                .Property(x => x.DiscountValue)
                .HasColumnType("decimal(18, 2)");


            builder
                .Property(x => x.MinQuantity)
                .HasColumnType("decimal(18, 2)");


            builder
                .Property(x => x.MinOrderAmount)
                .HasColumnType("decimal(18, 2)");


            builder
                .Property(x => x.MaxDiscountAmount)
                .HasColumnType("decimal(18, 2)");


            builder
                .HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);


            builder
                .HasOne(x => x.Customer)
                .WithMany()
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);


            builder
                .HasIndex(x => new { x.Scope, x.IsActive, x.EffectiveFrom });
        }
    }
}
