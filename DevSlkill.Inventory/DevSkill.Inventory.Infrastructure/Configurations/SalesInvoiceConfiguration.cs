using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevSkill.Inventory.Infrastructure.Configurations
{
    /// <summary>
    /// How <see cref="SalesInvoice"/> is mapped. Lifted out of the context so the
    /// context says what tables there are and each of these says what one table
    /// looks like.
    /// </summary>
    public class SalesInvoiceConfiguration : IEntityTypeConfiguration<SalesInvoice>
    {
        public void Configure(EntityTypeBuilder<SalesInvoice> builder)
        {

            //SalesInvoice
            builder
                .Property(x => x.InvoiceNo)
                .HasMaxLength(50)
                .IsRequired();


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
                .HasIndex(x => x.SalesOrderId);


            // The ageing report reads outstanding invoices one customer at a time.
            builder
                .HasIndex(x => new { x.CustomerId, x.Status, x.DueDate });


            builder
                .HasIndex(x => new { x.SalespersonId, x.InvoiceDate });


            builder
                .HasOne(x => x.Customer)
                .WithMany()
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);


            builder
                .HasOne(x => x.SalesOrder)
                .WithMany()
                .HasForeignKey(x => x.SalesOrderId)
                .OnDelete(DeleteBehavior.Restrict);


            builder
                .HasOne(x => x.Delivery)
                .WithMany()
                .HasForeignKey(x => x.DeliveryId)
                .OnDelete(DeleteBehavior.Restrict);


            builder
                .HasOne(x => x.BusinessLocation)
                .WithMany()
                .HasForeignKey(x => x.BusinessLocationId)
                .OnDelete(DeleteBehavior.Restrict);


            builder
                .HasOne(x => x.Salesperson)
                .WithMany()
                .HasForeignKey(x => x.SalespersonId)
                .OnDelete(DeleteBehavior.Restrict);


            builder
                .HasOne(x => x.PaymentTerm)
                .WithMany()
                .HasForeignKey(x => x.PaymentTermId)
                .OnDelete(DeleteBehavior.Restrict);


            builder
                .HasMany(x => x.SalesInvoiceItems)
                .WithOne(x => x.SalesInvoice)
                .HasForeignKey(x => x.SalesInvoiceId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
