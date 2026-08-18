using DevSkill.Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevSkill.Inventory.Infrastructure.Configurations
{
    /// <summary>
    /// How <see cref="Category"/> is mapped. Lifted out of the context so the
    /// context says what tables there are and each of these says what one table
    /// looks like.
    /// </summary>
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {

            builder.HasData(
                new Category { Id = new Guid("beac8874-432a-4028-a535-5d43e9287e5f"), CategoryName = "Electronics" },
                new Category { Id = new Guid("113fc42f-3eb3-46d5-ba13-e9a257983ba9"), CategoryName = "Home Appliances" },
                new Category { Id = new Guid("6501b4d4-54e2-4276-ab33-b773e1071649"), CategoryName = "Clothing" }
            );
        }
    }
}
