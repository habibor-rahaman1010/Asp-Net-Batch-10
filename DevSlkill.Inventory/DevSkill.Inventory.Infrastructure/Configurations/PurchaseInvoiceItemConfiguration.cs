using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevSkill.Inventory.Infrastructure.Configurations
{
    /// <summary>
    /// How <see cref="PurchaseInvoiceItem"/> is mapped. Lifted out of the context so the
    /// context says what tables there are and each of these says what one table
    /// looks like.
    /// </summary>
    public class PurchaseInvoiceItemConfiguration : IEntityTypeConfiguration<PurchaseInvoiceItem>
    {
        public void Configure(EntityTypeBuilder<PurchaseInvoiceItem> builder)
        {

            //PurchaseInvoiceItem
            foreach (var money in new[] { "Quantity", "UnitPrice", "DiscountAmount", "TaxRate",
                                          "TaxAmount", "LineTotal" })
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
                .HasIndex(x => new { x.PurchaseInvoiceId, x.ProductId })
                .IsUnique();
        }
    }
}
