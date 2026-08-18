using DevSkill.Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevSkill.Inventory.Infrastructure.Configurations
{
    /// <summary>
    /// How <see cref="ApplicableTax"/> is mapped. Lifted out of the context so the
    /// context says what tables there are and each of these says what one table
    /// looks like.
    /// </summary>
    public class ApplicableTaxConfiguration : IEntityTypeConfiguration<ApplicableTax>
    {
        public void Configure(EntityTypeBuilder<ApplicableTax> builder)
        {

            builder
                .Property(x => x.TaxRate)
                .HasColumnType("decimal(18, 2)");


            // Seeding data of my all some classes...
            // The ids are written out rather than generated: a fresh Guid on every
            // model build makes EF think the seed rows changed, so each new migration
            // would drop and re-insert master data that products already point at.
            builder.HasData(
                new ApplicableTax { Id = new Guid("55fc0b11-4bd7-4499-8cf0-dfbde9eafb7f"), ApplicableTaxName = "Food" },
                new ApplicableTax { Id = new Guid("958bfc74-5814-4da3-bf68-ac65e2866103"), ApplicableTaxName = "Sales Tax" },
                new ApplicableTax { Id = new Guid("ac3c3079-e076-4092-8e54-016ea8063308"), ApplicableTaxName = "Fruits" }
            );
        }
    }
}
