using DevSkill.Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevSkill.Inventory.Infrastructure.Configurations
{
    /// <summary>
    /// How <see cref="Brand"/> is mapped. Lifted out of the context so the
    /// context says what tables there are and each of these says what one table
    /// looks like.
    /// </summary>
    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {

            builder.HasData(
                new Brand { Id = new Guid("a0851b20-20af-427d-9d5d-a40f451f2ef2"), BrandName = "Apple" },
                new Brand { Id = new Guid("4aa25c30-fb48-47b0-a525-b241a55df9ee"), BrandName = "Samsung" },
                new Brand { Id = new Guid("18c2bea8-b25e-433d-a08e-b6f7a00786ee"), BrandName = "Sony" }
            );
        }
    }
}
