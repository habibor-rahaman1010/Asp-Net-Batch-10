using DevSkill.Inventory.Domain.Entities.NotificationEntities;
using DevSkill.Inventory.Domain.Enums;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    /// <summary>
    /// Who is assigned to what. This is the only thing that decides who hears about
    /// an operation, so an empty table means nobody is notified about anything.
    /// </summary>
    public interface INotificationSubscriptionRepository : IRepositoryBase<NotificationSubscription, Guid>
    {
        /// <summary>
        /// Everybody assigned to this event. The list the raising side writes copies
        /// for; empty means the operation happens quietly.
        /// </summary>
        Task<IList<Guid>> GetSubscriberUserIdsAsync(NotificationEvent notificationEvent);

        /// <summary>Every event this person is assigned to.</summary>
        Task<IList<NotificationEvent>> GetEventsForUserAsync(Guid userId);

        /// <summary>
        /// How many people are on each event, so an event nobody covers can be seen
        /// as the gap it is.
        /// </summary>
        Task<IDictionary<NotificationEvent, int>> GetSubscriberCountsAsync();

        /// <summary>
        /// Replaces this person's whole assignment list in one go. Written as a
        /// replace rather than an add and a remove, so a half-saved screen cannot
        /// leave somebody assigned to something they were just taken off.
        /// </summary>
        Task ReplaceForUserAsync(Guid userId, IEnumerable<NotificationEvent> events, string assignedBy);
    }
}
