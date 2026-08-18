using DevSkill.Inventory.Domain.Entities.StockAdjustmentEntites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevSkill.Inventory.Infrastructure.Configurations
{
    /// <summary>
    /// How <see cref="AdjustmentType"/> is mapped. Lifted out of the context so the
    /// context says what tables there are and each of these says what one table
    /// looks like.
    /// </summary>
    public class AdjustmentTypeConfiguration : IEntityTypeConfiguration<AdjustmentType>
    {
        public void Configure(EntityTypeBuilder<AdjustmentType> builder)
        {
            builder.HasData(
                new AdjustmentType { Id = new Guid("43c8d526-3f8a-43d8-92c4-3ae94c6aade4"), AdjustmentTypeName = "Normal"},
                new AdjustmentType { Id = new Guid("43283b35-3f96-4a3e-b35a-f395ea7de383"), AdjustmentTypeName = "Abnormal" }
            );
        }
    }
}
