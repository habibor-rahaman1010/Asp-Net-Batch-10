using DevSkill.Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevSkill.Inventory.Infrastructure.Configurations
{
    /// <summary>
    /// How <see cref="BusinessLocation"/> is mapped. Lifted out of the context so the
    /// context says what tables there are and each of these says what one table
    /// looks like.
    /// </summary>
    public class BusinessLocationConfiguration : IEntityTypeConfiguration<BusinessLocation>
    {
        public void Configure(EntityTypeBuilder<BusinessLocation> builder)
        {

            builder.HasData(
                new BusinessLocation { Id = new Guid("648fb752-70fa-47e4-9b3f-aed1f6c33027"), LocationName = "Warehouse A" },
                new BusinessLocation { Id = new Guid("cffe7a51-7178-4d25-938f-20402c5331b4"), LocationName = "Warehouse B" },
                new BusinessLocation { Id = new Guid("845099d1-ee98-4be5-8551-7102645b0a81"), LocationName = "Downtown Store" }
            );

            // Existing warehouse gets an activity flag. The store default keeps every
            // already existing row active, ValueGeneratedNever keeps EF sending false
            // when a user really deactivates a location.
            builder
                .Property(x => x.IsActive)
                .HasDefaultValue(true)
                .ValueGeneratedNever();
        }
    }
}
