using DevSkill.Inventory.Domain.Entities.SalesEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevSkill.Inventory.Infrastructure.Configurations
{
    /// <summary>
    /// How <see cref="PaymentTerm"/> is mapped. Lifted out of the context so the
    /// context says what tables there are and each of these says what one table
    /// looks like.
    /// </summary>
    public class PaymentTermConfiguration : IEntityTypeConfiguration<PaymentTerm>
    {
        public void Configure(EntityTypeBuilder<PaymentTerm> builder)
        {

            //PaymentTerm
            builder
                .Property(x => x.TermName)
                .HasMaxLength(100)
                .IsRequired();


            builder
                .Property(x => x.Description)
                .HasMaxLength(250);


            builder
                .Property(x => x.IsActive)
                .HasDefaultValue(true)
                .ValueGeneratedNever();


            builder
                .HasIndex(x => x.TermName)
                .IsUnique();


            // Fixed ids keep the seed stable, so a later migration never tries to
            // delete and re-insert these rows.
            builder.HasData(
                new PaymentTerm
                {
                    Id = Guid.Parse("6a1f0b2c-0001-4a1e-9f01-2b7d5c9e1001"),
                    TermName = "Cash on Delivery",
                    Description = "Full payment is collected when the goods are handed over.",
                    DueDays = 0,
                    IsActive = true
                },
                new PaymentTerm
                {
                    Id = Guid.Parse("6a1f0b2c-0002-4a1e-9f01-2b7d5c9e1002"),
                    TermName = "Advance Payment",
                    Description = "Full payment is received before the goods are released.",
                    DueDays = 0,
                    IsActive = true
                },
                new PaymentTerm
                {
                    Id = Guid.Parse("6a1f0b2c-0003-4a1e-9f01-2b7d5c9e1003"),
                    TermName = "50% Advance, 50% on Delivery",
                    Description = "Half of the amount is paid up front and the rest on delivery.",
                    DueDays = 0,
                    IsActive = true
                },
                new PaymentTerm
                {
                    Id = Guid.Parse("6a1f0b2c-0004-4a1e-9f01-2b7d5c9e1004"),
                    TermName = "Net 7",
                    Description = "Payment is due within 7 days of the invoice date.",
                    DueDays = 7,
                    IsActive = true
                },
                new PaymentTerm
                {
                    Id = Guid.Parse("6a1f0b2c-0005-4a1e-9f01-2b7d5c9e1005"),
                    TermName = "Net 15",
                    Description = "Payment is due within 15 days of the invoice date.",
                    DueDays = 15,
                    IsActive = true
                },
                new PaymentTerm
                {
                    Id = Guid.Parse("6a1f0b2c-0006-4a1e-9f01-2b7d5c9e1006"),
                    TermName = "Net 30",
                    Description = "Payment is due within 30 days of the invoice date.",
                    DueDays = 30,
                    IsActive = true
                },
                new PaymentTerm
                {
                    Id = Guid.Parse("6a1f0b2c-0007-4a1e-9f01-2b7d5c9e1007"),
                    TermName = "Net 60",
                    Description = "Payment is due within 60 days of the invoice date.",
                    DueDays = 60,
                    IsActive = true
                }
            );
        }
    }
}
