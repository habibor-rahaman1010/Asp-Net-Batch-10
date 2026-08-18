using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Enums;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    /// <summary>
    /// What the notification table asks for. The unread switch is carried here
    /// rather than as a query string, so a filtered table keeps its filter across
    /// paging without the URL having to say so.
    /// </summary>
    public class NotificationListModel : DataTables
    {
        public bool UnreadOnly { get; set; }
    }

    /// <summary>One person on the assignment screen's user picker.</summary>
    public class NotificationUserModel
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        /// <summary>How many events this person is currently assigned to.</summary>
        public int AssignedCount { get; set; }
    }

    /// <summary>
    /// What the assignment screen draws: everybody who could be assigned, and the
    /// events of the person currently being edited.
    /// </summary>
    public class NotificationAssignmentViewModel
    {
        public IList<NotificationUserModel> Users { get; set; } = new List<NotificationUserModel>();

        /// <summary>Who the event list below belongs to. Empty until somebody is picked.</summary>
        public Guid? SelectedUserId { get; set; }

        public IList<NotificationAssignmentDto> Assignments { get; set; }
            = new List<NotificationAssignmentDto>();
    }

    /// <summary>
    /// One save of the assignment screen. The event list replaces whatever the
    /// person had, so an empty list means take them off everything.
    /// </summary>
    public class NotificationAssignmentSaveModel
    {
        public Guid UserId { get; set; }

        public IList<NotificationEvent> Events { get; set; } = new List<NotificationEvent>();
    }
}
