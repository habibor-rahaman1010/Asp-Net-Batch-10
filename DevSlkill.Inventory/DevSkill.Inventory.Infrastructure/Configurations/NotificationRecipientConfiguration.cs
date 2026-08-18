using DevSkill.Inventory.Domain.Entities.NotificationEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevSkill.Inventory.Infrastructure.Configurations
{
    /// <summary>
    /// How <see cref="NotificationRecipient"/> is mapped. Lifted out of the context so the
    /// context says what tables there are and each of these says what one table
    /// looks like.
    /// </summary>
    public class NotificationRecipientConfiguration : IEntityTypeConfiguration<NotificationRecipient>
    {
        public void Configure(EntityTypeBuilder<NotificationRecipient> builder)
        {

            // Deleting an event takes its copies with it: a copy of nothing has no
            // meaning and would leave the inbox showing a blank row.
            builder
                .HasOne(x => x.Notification)
                .WithMany(x => x.Recipients)
                .HasForeignKey(x => x.NotificationId)
                .OnDelete(DeleteBehavior.Cascade);


            // Nobody is told the same thing twice, whatever the raising side does.
            builder
                .HasIndex(x => new { x.NotificationId, x.UserId })
                .IsUnique();


            // The badge counts unread rows for one user on every page load, so that
            // pair is what the index is built on.
            builder
                .HasIndex(x => new { x.UserId, x.IsRead });
        }
    }
}
