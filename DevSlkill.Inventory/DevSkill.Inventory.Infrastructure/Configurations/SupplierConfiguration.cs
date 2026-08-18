using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevSkill.Inventory.Infrastructure.Configurations
{
    /// <summary>
    /// How <see cref="Supplier"/> is mapped. Lifted out of the context so the
    /// context says what tables there are and each of these says what one table
    /// looks like.
    /// </summary>
    public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            //Supplier
            builder
                .Property(x => x.SupplierName)
                .HasMaxLength(200)
                .IsRequired();


            builder
                .Property(x => x.SupplierCode)
                .HasMaxLength(50)
                .IsRequired();


            builder
                .Property(x => x.ContactPerson)
                .HasMaxLength(200);


            builder
                .Property(x => x.Phone)
                .HasMaxLength(30);


            builder
                .Property(x => x.Email)
                .HasMaxLength(150);


            builder
                .Property(x => x.Address)
                .HasMaxLength(500);


            builder
                .Property(x => x.TaxNumber)
                .HasMaxLength(50);


            builder
                .Property(x => x.CreditLimit)
                .HasColumnType("decimal(18, 2)");


            builder
                .Property(x => x.OpeningBalance)
                .HasColumnType("decimal(18, 2)");


            builder
                .Property(x => x.CurrentOutstanding)
                .HasColumnType("decimal(18, 2)");


            builder
                .HasIndex(x => x.SupplierCode)
                .IsUnique();


            builder
                .HasIndex(x => x.SupplierName);


            builder
                .HasOne(x => x.PaymentTerm)
                .WithMany()
                .HasForeignKey(x => x.PaymentTermId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
