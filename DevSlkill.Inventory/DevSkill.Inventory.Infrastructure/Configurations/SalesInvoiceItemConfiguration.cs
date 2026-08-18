using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevSkill.Inventory.Infrastructure.Configurations
{
    /// <summary>
    /// How <see cref="SalesInvoiceItem"/> is mapped. Lifted out of the context so the
    /// context says what tables there are and each of these says what one table
    /// looks like.
    /// </summary>
    public class SalesInvoiceItemConfiguration : IEntityTypeConfiguration<SalesInvoiceItem>
    {
        public void Configure(EntityTypeBuilder<SalesInvoiceItem> builder)
        {

            //SalesInvoiceItem
            foreach (var money in new[] { "Quantity", "UnitPrice", "DiscountAmount", "TaxRate",
                                          "TaxAmount", "LineTotal", "ReturnedQuantity" })
            {
                builder
                    .Property(money)
                    .HasColumnType("decimal(18, 2)");
            }


            builder
                .HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);


            builder
                .HasIndex(x => new { x.SalesInvoiceId, x.ProductId })
                .IsUnique();
        }
    }
}
