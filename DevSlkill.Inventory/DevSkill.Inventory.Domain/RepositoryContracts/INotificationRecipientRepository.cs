using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities.NotificationEntities;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    /// <summary>
    /// Everything read side of notifications. Every method here is scoped to one
    /// user on purpose: there is no way to ask this repository for somebody else's
    /// notifications, so a mistyped id cannot leak another person's inbox.
    /// </summary>
    public interface INotificationRecipientRepository : IRepositoryBase<NotificationRecipient, Guid>
    {
        /// <summary>How many this person has not read yet. What the bell badge shows.</summary>
        Task<int> GetUnreadCountAsync(Guid userId);

        /// <summary>The newest few for this person, read or not, for the bell dropdown.</summary>
        Task<IList<NotificationDto>> GetRecentAsync(Guid userId, int take);

        /// <summary>
        /// This person's notifications a page at a time, newest first. Unread only
        /// when asked for, so the full history stays reachable.
        /// </summary>
        Task<(IList<NotificationDto> data, int total, int totalDisplay)> GetPagedForUserAsync(Guid userId,
            int pageIndex, int pageSize, string? searchValue, bool unreadOnly);

        /// <summary>
        /// Marks one of this person's notifications read. False when the row is not
        /// theirs, so somebody else's cannot be touched by guessing an id.
        /// </summary>
        Task<bool> MarkAsReadAsync(Guid recipientId, Guid userId);

        /// <summary>Marks everything this person has waiting as read. Returns how many.</summary>
        Task<int> MarkAllAsReadAsync(Guid userId);
    }
}
