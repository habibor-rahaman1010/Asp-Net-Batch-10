using DevSkill.Inventory.Domain.Entities.NotificationEntities;
using DevSkill.Inventory.Domain.Enums;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    /// <summary>
    /// The events themselves. Reading is done through
    /// <see cref="INotificationRecipientRepository"/>, because nothing is read
    /// except as somebody's copy.
    /// </summary>
    public interface INotificationRepository : IRepositoryBase<Notification, Guid>
    {
        /// <summary>
        /// Deletes read notifications older than the given date and any event left
        /// with nobody holding a copy. Housekeeping only: an unread notification is
        /// never removed, however old it is.
        /// </summary>
        Task<int> DeleteReadOlderThanAsync(DateTime cutoff);

        /// <summary>
        /// Whether this event has already been raised about this document since the
        /// given time. Stock alerts read this before firing, so a product sitting
        /// below its alert line is reported once rather than on every movement.
        /// </summary>
        Task<bool> HasRecentAsync(NotificationEvent notificationEvent, Guid referenceId, DateTime since);
    }
}
