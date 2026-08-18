using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevSkill.Inventory.Infrastructure.Configurations
{
    /// <summary>
    /// How <see cref="PurchaseInvoice"/> is mapped. Lifted out of the context so the
    /// context says what tables there are and each of these says what one table
    /// looks like.
    /// </summary>
    public class PurchaseInvoiceConfiguration : IEntityTypeConfiguration<PurchaseInvoice>
    {
        public void Configure(EntityTypeBuilder<PurchaseInvoice> builder)
        {

            //PurchaseInvoice
            builder
                .Property(x => x.InvoiceNo)
                .HasMaxLength(50)
                .IsRequired();


            builder
                .Property(x => x.SupplierInvoiceNo)
                .HasMaxLength(100);


            builder
                .Property(x => x.Notes)
                .HasMaxLength(1000);


            builder
                .Property(x => x.CreatedBy)
                .HasMaxLength(200);


            foreach (var money in new[] { "SubTotal", "ItemDiscountTotal", "TotalTax", "OtherCharges",
                                          "GrandTotal", "PaidAmount", "DueAmount" })
            {
                builder
                    .Property(money)
                    .HasColumnType("decimal(18, 2)");
            }


            builder
                .HasIndex(x => x.InvoiceNo)
                .IsUnique();


            builder
                .HasIndex(x => x.PurchaseOrderId);


            // The ageing report reads outstanding invoices one supplier at a time.
            builder
                .HasIndex(x => new { x.SupplierId, x.Status, x.DueDate });


            builder
                .HasOne(x => x.Supplier)
                .WithMany()
                .HasForeignKey(x => x.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);


            builder
                .HasOne(x => x.PurchaseOrder)
                .WithMany()
                .HasForeignKey(x => x.PurchaseOrderId)
                .OnDelete(DeleteBehavior.Restrict);


            builder
                .HasOne(x => x.GoodsReceipt)
                .WithMany()
                .HasForeignKey(x => x.GoodsReceiptId)
                .OnDelete(DeleteBehavior.Restrict);


            builder
                .HasOne(x => x.PaymentTerm)
                .WithMany()
                .HasForeignKey(x => x.PaymentTermId)
                .OnDelete(DeleteBehavior.Restrict);


            builder
                .HasMany(x => x.PurchaseInvoiceItems)
                .WithOne(x => x.PurchaseInvoice)
                .HasForeignKey(x => x.PurchaseInvoiceId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
