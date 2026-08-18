using DevSkill.Inventory.Domain.Enums;

namespace DevSkill.Inventory.Domain.Entities.NotificationEntities
{
    /// <summary>
    /// One assignment: this person is to be told when this operation happens. No
    /// row, no notification. Nothing here is implied by a role or a claim, so
    /// somebody being an admin does not on its own put them on any list.
    /// </summary>
    public class NotificationSubscription : IEntity<Guid>
    {
        public Guid Id { get; set; }

        /// <summary>
        /// The identity user assigned to the event. Stored without a foreign key
        /// because identity lives in a different DbContext.
        /// </summary>
        public Guid UserId { get; set; }

        public NotificationEvent Event { get; set; }

        public DateTime Created { get; set; }

        /// <summary>Who made the assignment, so a surprise subscription can be traced.</summary>
        public string AssignedBy { get; set; } = string.Empty;
    }
}
