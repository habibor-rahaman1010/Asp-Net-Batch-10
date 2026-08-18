using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevSkill.Inventory.Infrastructure.Configurations
{
    /// <summary>
    /// How <see cref="ProformaInvoice"/> is mapped. Lifted out of the context so the
    /// context says what tables there are and each of these says what one table
    /// looks like.
    /// </summary>
    public class ProformaInvoiceConfiguration : IEntityTypeConfiguration<ProformaInvoice>
    {
        public void Configure(EntityTypeBuilder<ProformaInvoice> builder)
        {

            //ProformaInvoice
            builder
                .Property(x => x.ProformaNo)
                .HasMaxLength(50)
                .IsRequired();


            builder
                .Property(x => x.SalespersonName)
                .HasMaxLength(200);


            // Restrict, like every other document that names one: a salesperson with
            // work to their name cannot be deleted out from under it.
            builder
                .HasOne(x => x.Salesperson)
                .WithMany()
                .HasForeignKey(x => x.SalespersonId)
                .OnDelete(DeleteBehavior.Restrict);


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
                .Property(x => x.OrderDiscount)
                .HasColumnType("decimal(18, 2)");


            builder
                .Property(x => x.TotalTax)
                .HasColumnType("decimal(18, 2)");


            builder
                .Property(x => x.GrandTotal)
                .HasColumnType("decimal(18, 2)");


            builder
                .HasIndex(x => x.ProformaNo)
                .IsUnique();


            builder
                .HasIndex(x => new { x.CustomerId, x.ProformaDate });


            builder
                .HasOne(x => x.Customer)
                .WithMany()
                .HasForeignKey(x => x.CustomerId)
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
                .HasMany(x => x.ProformaInvoiceItems)
                .WithOne(x => x.ProformaInvoice)
                .HasForeignKey(x => x.ProformaInvoiceId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
