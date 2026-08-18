using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevSkill.Inventory.Infrastructure.Configurations
{
    /// <summary>
    /// How <see cref="ProformaInvoiceItem"/> is mapped. Lifted out of the context so the
    /// context says what tables there are and each of these says what one table
    /// looks like.
    /// </summary>
    public class ProformaInvoiceItemConfiguration : IEntityTypeConfiguration<ProformaInvoiceItem>
    {
        public void Configure(EntityTypeBuilder<ProformaInvoiceItem> builder)
        {

            //ProformaInvoiceItem
            builder
                .Property(x => x.Quantity)
                .HasColumnType("decimal(18, 2)");


            builder
                .Property(x => x.UnitPrice)
                .HasColumnType("decimal(18, 2)");


            builder
                .Property(x => x.DiscountAmount)
                .HasColumnType("decimal(18, 2)");


            builder
                .Property(x => x.TaxRate)
                .HasColumnType("decimal(18, 2)");


            builder
                .Property(x => x.TaxAmount)
                .HasColumnType("decimal(18, 2)");


            builder
                .Property(x => x.LineTotal)
                .HasColumnType("decimal(18, 2)");


            builder
                .HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
