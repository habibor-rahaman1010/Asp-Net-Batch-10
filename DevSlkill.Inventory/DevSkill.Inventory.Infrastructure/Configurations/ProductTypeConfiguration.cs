using DevSkill.Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevSkill.Inventory.Infrastructure.Configurations
{
    /// <summary>
    /// How <see cref="ProductType"/> is mapped. Lifted out of the context so the
    /// context says what tables there are and each of these says what one table
    /// looks like.
    /// </summary>
    public class ProductTypeConfiguration : IEntityTypeConfiguration<ProductType>
    {
        public void Configure(EntityTypeBuilder<ProductType> builder)
        {

            builder.HasData(
                new ProductType { Id = new Guid("ef208b24-e8e2-470e-8b16-08aee482a125"), ProductTypeName = "Electronics" },
                new ProductType { Id = new Guid("54f5ee90-adaf-4b0c-a656-57ecde14ef79"), ProductTypeName = "Clothing" },
                new ProductType { Id = new Guid("d99f73c5-4045-49c5-b3cb-511e714b2039"), ProductTypeName = "Food" },
                new ProductType { Id = new Guid("b180d99d-12ff-42d9-b099-1ec17e8ec58c"), ProductTypeName = "Furniture" },
                new ProductType { Id = new Guid("af92f651-47ca-4dbd-bd27-c1f2b4f23ea4"), ProductTypeName = "Toys" }
            );
        }
    }
}
