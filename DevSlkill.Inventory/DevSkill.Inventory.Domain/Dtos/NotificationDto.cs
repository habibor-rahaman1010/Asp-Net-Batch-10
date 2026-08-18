using DevSkill.Inventory.Domain.Enums;

namespace DevSkill.Inventory.Domain.Dtos
{
    /// <summary>
    /// One notification as one person sees it: the event's own wording plus whether
    /// that person has read it. Two people looking at the same event see the same
    /// words and can still differ on the read flag.
    /// </summary>
    public class NotificationDto
    {
        /// <summary>The recipient row, not the notification. Marking read works on this.</summary>
        public Guid Id { get; set; }

        public NotificationEvent Event { get; set; }

        /// <summary>The event as a word, so the browser never reads the numbering.</summary>
        public string EventName => Event.ToString();

        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string RaisedBy { get; set; } = string.Empty;

        public bool IsRead { get; set; }
        public DateTime Created { get; set; }

        /// <summary>FontAwesome class for the event, filled in from the catalogue.</summary>
        public string Icon { get; set; } = string.Empty;

        /// <summary>Bootstrap colour suffix for the icon, from the catalogue.</summary>
        public string Colour { get; set; } = string.Empty;

        /// <summary>What the event is called on screen, from the catalogue.</summary>
        public string EventLabel { get; set; } = string.Empty;
    }

    /// <summary>
    /// What the bell needs in one read: how many are waiting, and the newest few to
    /// show without opening the full list.
    /// </summary>
    public class NotificationSummaryDto
    {
        public int UnreadCount { get; set; }

        public IList<NotificationDto> Recent { get; set; } = new List<NotificationDto>();
    }

    /// <summary>
    /// One row of the assignment screen: an event, and whether this person is on it.
    /// </summary>
    public class NotificationAssignmentDto
    {
        public NotificationEvent Event { get; set; }
        public string EventName => Event.ToString();

        public string Group { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public string Colour { get; set; } = string.Empty;

        /// <summary>Whether the person being edited is assigned to this event.</summary>
        public bool IsAssigned { get; set; }

        /// <summary>
        /// How many people in total are on this event. Shown so an event nobody is
        /// assigned to is visible as a gap rather than looking the same as any other.
        /// </summary>
        public int SubscriberCount { get; set; }
    }
}
