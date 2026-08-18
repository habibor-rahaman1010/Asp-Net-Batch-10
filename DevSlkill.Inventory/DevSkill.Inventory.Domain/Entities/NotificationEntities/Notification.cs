using DevSkill.Inventory.Domain.Enums;

namespace DevSkill.Inventory.Domain.Entities.NotificationEntities
{
    /// <summary>
    /// One thing that happened, written once however many people are told about it.
    /// Who hears about it lives in <see cref="NotificationRecipient"/>, so ten
    /// subscribers do not mean ten copies of the same sentence.
    /// </summary>
    public class Notification : IEntity<Guid>
    {
        public Guid Id { get; set; }

        /// <summary>Which operation this was. Decides who was told.</summary>
        public NotificationEvent Event { get; set; }

        /// <summary>One line, ready to read in the bell without opening anything.</summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>The detail underneath: the document, the party, the amount.</summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Where clicking it goes. Empty when the operation has no page worth
        /// landing on, and the notification is then read-only.
        /// </summary>
        public string Url { get; set; } = string.Empty;

        /// <summary>
        /// The document this is about, kept so the row can be traced back after the
        /// wording has been changed. Not a foreign key: it points into whichever
        /// table the event belongs to.
        /// </summary>
        public Guid? ReferenceId { get; set; }

        /// <summary>Who did the operation, as a name rather than an id.</summary>
        public string RaisedBy { get; set; } = string.Empty;

        public DateTime Created { get; set; }

        public virtual ICollection<NotificationRecipient>? Recipients { get; set; }
    }
}
