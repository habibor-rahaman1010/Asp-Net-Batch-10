using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevSkill.Inventory.Infrastructure.Configurations
{
    /// <summary>
    /// How <see cref="PurchaseOrder"/> is mapped. Lifted out of the context so the
    /// context says what tables there are and each of these says what one table
    /// looks like.
    /// </summary>
    public class PurchaseOrderConfiguration : IEntityTypeConfiguration<PurchaseOrder>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrder> builder)
        {

            //PurchaseOrder
            builder
                .Property(x => x.PurchaseOrderNo)
                .HasMaxLength(50)
                .IsRequired();


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
                .Property(x => x.SubTotal)
                .HasColumnType("decimal(18, 2)");


            builder
                .Property(x => x.ItemDiscountTotal)
                .HasColumnType("decimal(18, 2)");


            builder
                .Property(x => x.TotalTax)
                .HasColumnType("decimal(18, 2)");


            builder
                .Property(x => x.OtherCharges)
                .HasColumnType("decimal(18, 2)");


            builder
                .Property(x => x.GrandTotal)
                .HasColumnType("decimal(18, 2)");


            builder
                .HasIndex(x => x.PurchaseOrderNo)
                .IsUnique();


            builder
                .HasIndex(x => new { x.SupplierId, x.OrderDate });


            builder
                .HasIndex(x => new { x.Status, x.OrderDate });


            builder
                .HasOne(x => x.Supplier)
                .WithMany()
                .HasForeignKey(x => x.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);


            builder
                .HasOne(x => x.BusinessLocation)
                .WithMany()
                .HasForeignKey(x => x.BusinessLocationId)
                .OnDelete(DeleteBehavior.Restrict);


            builder
                .HasOne(x => x.PaymentTerm)
                .WithMany()
                .HasForeignKey(x => x.PaymentTermId)
                .OnDelete(DeleteBehavior.Restrict);


            builder
                .HasOne(x => x.PurchaseRequisition)
                .WithMany()
                .HasForeignKey(x => x.PurchaseRequisitionId)
                .OnDelete(DeleteBehavior.Restrict);


            builder
                .HasMany(x => x.PurchaseOrderItems)
                .WithOne(x => x.PurchaseOrder)
                .HasForeignKey(x => x.PurchaseOrderId)
                .OnDelete(DeleteBehavior.Restrict);


            // The order keeps a pointer back to the quotation it was won on.
            builder
                .HasOne(x => x.SupplierQuotation)
                .WithMany()
                .HasForeignKey(x => x.SupplierQuotationId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
