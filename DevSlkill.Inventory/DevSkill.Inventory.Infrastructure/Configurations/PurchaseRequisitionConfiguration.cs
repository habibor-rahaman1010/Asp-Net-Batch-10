using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevSkill.Inventory.Infrastructure.Configurations
{
    /// <summary>
    /// How <see cref="PurchaseRequisition"/> is mapped. Lifted out of the context so the
    /// context says what tables there are and each of these says what one table
    /// looks like.
    /// </summary>
    public class PurchaseRequisitionConfiguration : IEntityTypeConfiguration<PurchaseRequisition>
    {
        public void Configure(EntityTypeBuilder<PurchaseRequisition> builder)
        {

            //PurchaseRequisition
            builder
                .Property(x => x.RequisitionNo)
                .HasMaxLength(50)
                .IsRequired();


            builder
                .Property(x => x.RequestedByName)
                .HasMaxLength(200);


            builder
                .Property(x => x.ApprovedByName)
                .HasMaxLength(200);


            builder
                .Property(x => x.RejectionReason)
                .HasMaxLength(500);


            builder
                .Property(x => x.Notes)
                .HasMaxLength(1000);


            builder
                .Property(x => x.CreatedBy)
                .HasMaxLength(200);


            builder
                .Property(x => x.EstimatedTotal)
                .HasColumnType("decimal(18, 2)");


            builder
                .HasIndex(x => x.RequisitionNo)
                .IsUnique();


            builder
                .HasIndex(x => new { x.Status, x.RequisitionDate });


            builder
                .HasOne(x => x.BusinessLocation)
                .WithMany()
                .HasForeignKey(x => x.BusinessLocationId)
                .OnDelete(DeleteBehavior.Restrict);


            builder
                .HasMany(x => x.PurchaseRequisitionItems)
                .WithOne(x => x.PurchaseRequisition)
                .HasForeignKey(x => x.PurchaseRequisitionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
