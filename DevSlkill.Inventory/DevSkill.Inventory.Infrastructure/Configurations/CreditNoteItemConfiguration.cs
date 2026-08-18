using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevSkill.Inventory.Infrastructure.Configurations
{
    /// <summary>
    /// How <see cref="CreditNoteItem"/> is mapped. Lifted out of the context so the
    /// context says what tables there are and each of these says what one table
    /// looks like.
    /// </summary>
    public class CreditNoteItemConfiguration : IEntityTypeConfiguration<CreditNoteItem>
    {
        public void Configure(EntityTypeBuilder<CreditNoteItem> builder)
        {

            //CreditNoteItem
            foreach (var money in new[] { "Quantity", "UnitPrice", "TaxRate", "TaxAmount", "LineTotal" })
            {
                builder
                    .Property(money)
                    .HasColumnType("decimal(18, 2)");
            }


            builder
                .Property(x => x.Description)
                .HasMaxLength(500);


            builder
                .HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);


            builder
                .HasIndex(x => x.CreditNoteId);
        }
    }
}
