using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevSkill.Inventory.Infrastructure.Configurations
{
    /// <summary>
    /// How <see cref="RequestForQuotationItem"/> is mapped. Lifted out of the context so the
    /// context says what tables there are and each of these says what one table
    /// looks like.
    /// </summary>
    public class RequestForQuotationItemConfiguration : IEntityTypeConfiguration<RequestForQuotationItem>
    {
        public void Configure(EntityTypeBuilder<RequestForQuotationItem> builder)
        {

            //RequestForQuotationItem
            builder
                .Property(x => x.Quantity)
                .HasColumnType("decimal(18, 2)");


            builder
                .Property(x => x.Remarks)
                .HasMaxLength(500);


            builder
                .HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);


            builder
                .HasIndex(x => new { x.RequestForQuotationId, x.ProductId })
                .IsUnique();
        }
    }
}
