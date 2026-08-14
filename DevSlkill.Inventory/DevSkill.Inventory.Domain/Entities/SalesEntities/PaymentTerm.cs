namespace DevSkill.Inventory.Domain.Entities.SalesEntities
{
    /// <summary>
    /// Lookup table for the payment terms a sales document can be issued on.
    /// Keeping them in a table instead of free text means every proforma invoice
    /// carries the exact same wording and the same number of credit days.
    /// </summary>
    public class PaymentTerm : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string TermName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Credit days allowed by this term. Zero means payment is due immediately.
        /// </summary>
        public int DueDays { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
