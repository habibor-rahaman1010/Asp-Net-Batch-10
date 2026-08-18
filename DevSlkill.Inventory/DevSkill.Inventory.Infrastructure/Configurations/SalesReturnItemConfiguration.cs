using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevSkill.Inventory.Infrastructure.Configurations
{
    /// <summary>
    /// How <see cref="SalesReturnItem"/> is mapped. Lifted out of the context so the
    /// context says what tables there are and each of these says what one table
    /// looks like.
    /// </summary>
    public class SalesReturnItemConfiguration : IEntityTypeConfiguration<SalesReturnItem>
    {
        public void Configure(EntityTypeBuilder<SalesReturnItem> builder)
        {

            //SalesReturnItem
            foreach (var money in new[] { "InvoicedQuantity", "ReturnQuantity", "UnitPrice",
                                          "TaxRate", "TaxAmount", "LineTotal" })
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


            builder
                .HasIndex(x => new { x.SalesReturnId, x.ProductId })
                .IsUnique();
        }
    }
}
