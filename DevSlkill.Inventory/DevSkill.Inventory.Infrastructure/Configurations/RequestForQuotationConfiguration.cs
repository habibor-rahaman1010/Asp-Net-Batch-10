using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevSkill.Inventory.Infrastructure.Configurations
{
    /// <summary>
    /// How <see cref="RequestForQuotation"/> is mapped. Lifted out of the context so the
    /// context says what tables there are and each of these says what one table
    /// looks like.
    /// </summary>
    public class RequestForQuotationConfiguration : IEntityTypeConfiguration<RequestForQuotation>
    {
        public void Configure(EntityTypeBuilder<RequestForQuotation> builder)
        {

            //RequestForQuotation
            builder
                .Property(x => x.RfqNo)
                .HasMaxLength(50)
                .IsRequired();


            builder
                .Property(x => x.Notes)
                .HasMaxLength(1000);


            builder
                .Property(x => x.CreatedBy)
                .HasMaxLength(200);


            builder
                .HasIndex(x => x.RfqNo)
                .IsUnique();


            // The open requests are read one status at a time, because only a sent
            // request can be quoted against.
            builder
                .HasIndex(x => new { x.Status, x.RfqDate });


            builder
                .HasOne(x => x.PurchaseRequisition)
                .WithMany()
                .HasForeignKey(x => x.PurchaseRequisitionId)
                .OnDelete(DeleteBehavior.Restrict);


            builder
                .HasOne(x => x.BusinessLocation)
                .WithMany()
                .HasForeignKey(x => x.BusinessLocationId)
                .OnDelete(DeleteBehavior.Restrict);


            builder
                .HasMany(x => x.RequestForQuotationItems)
                .WithOne(x => x.RequestForQuotation)
                .HasForeignKey(x => x.RequestForQuotationId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
