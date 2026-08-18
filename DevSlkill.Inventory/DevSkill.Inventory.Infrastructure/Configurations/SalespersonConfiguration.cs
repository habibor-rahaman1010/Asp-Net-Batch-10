using DevSkill.Inventory.Domain.Entities.SalesEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevSkill.Inventory.Infrastructure.Configurations
{
    /// <summary>
    /// How <see cref="Salesperson"/> is mapped. Lifted out of the context so the
    /// context says what tables there are and each of these says what one table
    /// looks like.
    /// </summary>
    public class SalespersonConfiguration : IEntityTypeConfiguration<Salesperson>
    {
        public void Configure(EntityTypeBuilder<Salesperson> builder)
        {
            //Salesperson
            builder
                .Property(x => x.SalespersonName)
                .HasMaxLength(200)
                .IsRequired();


            builder
                .Property(x => x.SalespersonCode)
                .HasMaxLength(50)
                .IsRequired();


            builder
                .Property(x => x.Phone)
                .HasMaxLength(30);


            builder
                .Property(x => x.Email)
                .HasMaxLength(150);


            foreach (var money in new[] { "CommissionRate", "MonthlyTarget" })
            {
                builder
                    .Property(money)
                    .HasColumnType("decimal(18, 2)");
            }


            builder
                .HasIndex(x => x.SalespersonCode)
                .IsUnique();


            builder
                .HasIndex(x => x.SalespersonName);
        }
    }
}
