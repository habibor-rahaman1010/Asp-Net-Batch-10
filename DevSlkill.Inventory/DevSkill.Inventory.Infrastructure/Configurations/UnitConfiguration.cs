using DevSkill.Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevSkill.Inventory.Infrastructure.Configurations
{
    /// <summary>
    /// How <see cref="Unit"/> is mapped. Lifted out of the context so the
    /// context says what tables there are and each of these says what one table
    /// looks like.
    /// </summary>
    public class UnitConfiguration : IEntityTypeConfiguration<Unit>
    {
        public void Configure(EntityTypeBuilder<Unit> builder)
        {

            builder.HasData(
                new Unit { Id = new Guid("7f2c29e4-0db7-4654-a3fc-32a7208dcbce"), UnitName = "Kilogram" },
                new Unit { Id = new Guid("864f70fc-7390-44a3-b48b-66512fe1a126"), UnitName = "Liter" },
                new Unit { Id = new Guid("f1f55cd0-b58f-4d56-8c31-99fa87b37604"), UnitName = "Piece" }
            );
        }
    }
}
