using DevSkill.Inventory.Domain.Entities.SalesEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevSkill.Inventory.Infrastructure.Configurations
{
    /// <summary>
    /// How <see cref="Customer"/> is mapped. Lifted out of the context so the
    /// context says what tables there are and each of these says what one table
    /// looks like.
    /// </summary>
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {

            //Customer
            builder
                .Property(x => x.CustomerName)
                .HasMaxLength(200)
                .IsRequired();


            builder
                .Property(x => x.CustomerCode)
                .HasMaxLength(50)
                .IsRequired();


            builder
                .Property(x => x.Phone)
                .HasMaxLength(30);


            builder
                .Property(x => x.Email)
                .HasMaxLength(150);


            builder
                .Property(x => x.Address)
                .HasMaxLength(500);


            builder
                .Property(x => x.CreditLimit)
                .HasColumnType("decimal(18, 2)");


            builder
                .Property(x => x.OpeningBalance)
                .HasColumnType("decimal(18, 2)");


            builder
                .Property(x => x.CurrentOutstanding)
                .HasColumnType("decimal(18, 2)");


            builder
                .HasIndex(x => x.CustomerCode)
                .IsUnique();


            builder
                .HasIndex(x => x.CustomerName);
        }
    }
}
