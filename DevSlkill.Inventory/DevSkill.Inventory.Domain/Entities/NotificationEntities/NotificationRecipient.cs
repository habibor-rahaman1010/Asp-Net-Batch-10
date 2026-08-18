namespace DevSkill.Inventory.Domain.Entities.NotificationEntities
{
    /// <summary>
    /// One person's copy of a notification, and whether they have read it. Written
    /// only for people who were assigned the event, which is what keeps the whole
    /// system from turning into a broadcast.
    /// </summary>
    public class NotificationRecipient : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public Guid NotificationId { get; set; }
        public virtual Notification? Notification { get; set; }

        /// <summary>
        /// The identity user this copy belongs to. Identity lives in a different
        /// DbContext, so this is stored without a foreign key, the same way
        /// Salesperson.UserId is.
        /// </summary>
        public Guid UserId { get; set; }

        public bool IsRead { get; set; }

        /// <summary>Null until it is read. Kept so "read" is a fact with a time on it.</summary>
        public DateTime? ReadAt { get; set; }
    }
}
