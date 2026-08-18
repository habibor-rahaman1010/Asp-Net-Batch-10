using DevSkill.Inventory.Domain.Entities.NotificationEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevSkill.Inventory.Infrastructure.Configurations
{
    /// <summary>
    /// How <see cref="Notification"/> is mapped. Lifted out of the context so the
    /// context says what tables there are and each of these says what one table
    /// looks like.
    /// </summary>
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {

            //Notification module. Nothing here is seeded: an empty subscription table
            //means nobody is told about anything, which is the right starting point.
            builder
                .Property(x => x.Title)
                .HasMaxLength(200)
                .IsRequired();


            builder
                .Property(x => x.Message)
                .HasMaxLength(1000);


            builder
                .Property(x => x.Url)
                .HasMaxLength(400);


            builder
                .Property(x => x.RaisedBy)
                .HasMaxLength(256);


            // The bell asks for the newest first every time it opens.
            builder
                .HasIndex(x => x.Created);
        }
    }
}
