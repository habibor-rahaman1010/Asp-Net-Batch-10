using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Enums;

namespace DevSkill.Inventory.Application.ServicesContract
{
    public interface INotificationService
    {
        /// <summary>
        /// Records that an operation happened and gives a copy to everybody assigned
        /// to it. Nobody assigned means nothing is written at all, so an unassigned
        /// event costs one small read and no rows.
        ///
        /// Never throws. A notification is a side effect of the operation, not part
        /// of it, so a failure here is logged and swallowed rather than rolling back
        /// an invoice that was posted perfectly well.
        /// </summary>
        Task RaiseAsync(NotificationEvent notificationEvent, string title, string message,
            string? url = null, Guid? referenceId = null, string? raisedBy = null);

        /// <summary>
        /// Looks over the products and tells whoever is assigned about the ones that
        /// have run down or run out. Meant to be called after any operation that
        /// moves stock.
        ///
        /// A product below its line stays below it, so this would fire on every
        /// movement. It does not: a product already reported within the quiet window
        /// is passed over, and only reported again once that window has gone by.
        ///
        /// Never throws, for the same reason RaiseAsync does not.
        /// </summary>
        Task RaiseStockAlertsAsync();

        /// <summary>What the bell shows: the unread count and the newest few.</summary>
        Task<NotificationSummaryDto> GetSummaryAsync(Guid userId, int take = 6);

        /// <summary>This person's notifications a page at a time, newest first.</summary>
        Task<(IList<NotificationDto> data, int total, int totalDisplay)> GetPagedAsync(Guid userId,
            int pageIndex, int pageSize, string? searchValue, bool unreadOnly);

        /// <summary>
        /// Marks one of this person's notifications read. False when the row is not
        /// theirs.
        /// </summary>
        Task<bool> MarkAsReadAsync(Guid recipientId, Guid userId);

        /// <summary>Marks everything this person has waiting as read. Returns how many.</summary>
        Task<int> MarkAllAsReadAsync(Guid userId);

        /// <summary>
        /// Every event there is, flagged with whether this person is assigned to it
        /// and how many people are on it in total. This is what the assignment screen
        /// draws.
        /// </summary>
        Task<IList<NotificationAssignmentDto>> GetAssignmentsAsync(Guid userId);

        /// <summary>
        /// Replaces this person's whole assignment list. Passing an empty list takes
        /// them off everything, which is a real choice rather than a no-op.
        /// </summary>
        Task SaveAssignmentsAsync(Guid userId, IEnumerable<NotificationEvent> events, string assignedBy);
    }
}
