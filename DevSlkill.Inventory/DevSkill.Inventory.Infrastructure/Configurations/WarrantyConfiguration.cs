using DevSkill.Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevSkill.Inventory.Infrastructure.Configurations
{
    /// <summary>
    /// How <see cref="Warranty"/> is mapped. Lifted out of the context so the
    /// context says what tables there are and each of these says what one table
    /// looks like.
    /// </summary>
    public class WarrantyConfiguration : IEntityTypeConfiguration<Warranty>
    {
        public void Configure(EntityTypeBuilder<Warranty> builder)
        {

            builder.HasData(
                new Warranty { Id = new Guid("9548761d-5d98-4a6e-9d57-9e36979f7008"), WarrantyDuration = "1 Year" },
                new Warranty { Id = new Guid("40c94f14-5f6d-4d45-ba1f-82e669d85531"), WarrantyDuration = "2 Years" },
                new Warranty { Id = new Guid("369db434-c03b-4459-aa6d-990f9a8f2968"), WarrantyDuration = "3 Years" }
            );
        }
    }
}
