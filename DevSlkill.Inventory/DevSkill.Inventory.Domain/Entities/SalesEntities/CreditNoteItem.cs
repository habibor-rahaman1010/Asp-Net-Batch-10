namespace DevSkill.Inventory.Domain.Entities.SalesEntities
{
    /// <summary>
    /// What the credit is made of. A credit note raised from a return copies the
    /// returned lines; a price adjustment carries a single line of its own.
    /// </summary>
    public class CreditNoteItem : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public Guid CreditNoteId { get; set; }
        public virtual CreditNote? CreditNote { get; set; }

        /// <summary>Empty on a pure price adjustment, which credits no product.</summary>
        public Guid? ProductId { get; set; }
        public virtual Product? Product { get; set; }

        public string Description { get; set; } = string.Empty;

        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TaxRate { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal LineTotal { get; set; }
    }
}
