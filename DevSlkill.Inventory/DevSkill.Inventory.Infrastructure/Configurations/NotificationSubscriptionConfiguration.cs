using DevSkill.Inventory.Domain.Entities.NotificationEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevSkill.Inventory.Infrastructure.Configurations
{
    /// <summary>
    /// How <see cref="NotificationSubscription"/> is mapped. Lifted out of the context so the
    /// context says what tables there are and each of these says what one table
    /// looks like.
    /// </summary>
    public class NotificationSubscriptionConfiguration : IEntityTypeConfiguration<NotificationSubscription>
    {
        public void Configure(EntityTypeBuilder<NotificationSubscription> builder)
        {

            builder
                .Property(x => x.AssignedBy)
                .HasMaxLength(256);


            // One assignment per person per event. Assigning twice would otherwise
            // send two copies of the same notification.
            builder
                .HasIndex(x => new { x.UserId, x.Event })
                .IsUnique();


            // Raising an event reads this the other way round: everybody on it.
            builder
                .HasIndex(x => x.Event);
        }
    }
}
