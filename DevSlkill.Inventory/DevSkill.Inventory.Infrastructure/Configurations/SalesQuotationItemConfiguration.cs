using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevSkill.Inventory.Infrastructure.Configurations
{
    /// <summary>
    /// How <see cref="SalesQuotationItem"/> is mapped. Lifted out of the context so the
    /// context says what tables there are and each of these says what one table
    /// looks like.
    /// </summary>
    public class SalesQuotationItemConfiguration : IEntityTypeConfiguration<SalesQuotationItem>
    {
        public void Configure(EntityTypeBuilder<SalesQuotationItem> builder)
        {

            //SalesQuotationItem
            foreach (var money in new[] { "Quantity", "UnitPrice", "DiscountAmount", "TaxRate",
                                          "TaxAmount", "LineTotal", "OrderedQuantity" })
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


            // One product may appear only once on a quotation, so the ordered quantity
            // of a line is never split over two rows.
            builder
                .HasIndex(x => new { x.SalesQuotationId, x.ProductId })
                .IsUnique();
        }
    }
}
