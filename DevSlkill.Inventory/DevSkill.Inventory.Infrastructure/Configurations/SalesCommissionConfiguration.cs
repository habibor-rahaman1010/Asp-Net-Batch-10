using DevSkill.Inventory.Domain.Entities.SalesEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevSkill.Inventory.Infrastructure.Configurations
{
    /// <summary>
    /// How <see cref="SalesCommission"/> is mapped. Lifted out of the context so the
    /// context says what tables there are and each of these says what one table
    /// looks like.
    /// </summary>
    public class SalesCommissionConfiguration : IEntityTypeConfiguration<SalesCommission>
    {
        public void Configure(EntityTypeBuilder<SalesCommission> builder)
        {

            //SalesCommission
            foreach (var money in new[] { "BasisAmount", "CommissionRate", "CommissionAmount" })
            {
                builder
                    .Property(money)
                    .HasColumnType("decimal(18, 2)");
            }


            builder
                .Property(x => x.Notes)
                .HasMaxLength(500);


            // An invoice earns commission once. Posting it again after a cancellation
            // rewrites that one row rather than adding a second.
            builder
                .HasIndex(x => x.SalesInvoiceId)
                .IsUnique();


            builder
                .HasIndex(x => new { x.SalespersonId, x.EarnedDate });


            builder
                .HasOne(x => x.Salesperson)
                .WithMany()
                .HasForeignKey(x => x.SalespersonId)
                .OnDelete(DeleteBehavior.Restrict);


            builder
                .HasOne(x => x.SalesInvoice)
                .WithMany()
                .HasForeignKey(x => x.SalesInvoiceId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
