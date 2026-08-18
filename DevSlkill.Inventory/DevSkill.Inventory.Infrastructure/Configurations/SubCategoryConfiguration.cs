using DevSkill.Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevSkill.Inventory.Infrastructure.Configurations
{
    /// <summary>
    /// How <see cref="SubCategory"/> is mapped. Lifted out of the context so the
    /// context says what tables there are and each of these says what one table
    /// looks like.
    /// </summary>
    public class SubCategoryConfiguration : IEntityTypeConfiguration<SubCategory>
    {
        public void Configure(EntityTypeBuilder<SubCategory> builder)
        {

            builder.HasData(
                new SubCategory { Id = new Guid("fba544f7-3086-4287-a6d0-55d39da2ede0"), SubCategoryName = "Smartphones" },
                new SubCategory { Id = new Guid("17b15e1c-5063-4edc-8567-8f1e581eb494"), SubCategoryName = "Laptops" },
                new SubCategory { Id = new Guid("73f7b37d-e831-45a5-89e1-766b4e2b9ad1"), SubCategoryName = "Televisions" }
            );
        }
    }
}
