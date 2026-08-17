using DevSkill.Inventory.Domain.Enums;

namespace DevSkill.Inventory.Domain.Entities.SalesEntities
{
    /// <summary>
    /// Credit owed back to a customer, either because goods came back or because an
    /// invoice was overcharged. Issuing it takes the amount off what the customer
    /// owes; spending it is done by a collection of method Adjustment that names it.
    /// </summary>
    public class CreditNote : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string CreditNoteNo { get; set; } = string.Empty;

        public Guid CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }

        /// <summary>
        /// Set when the credit came out of returned goods. A return raises exactly one
        /// credit note, which is what keeps the two in step.
        /// </summary>
        public Guid? SalesReturnId { get; set; }
        public virtual SalesReturn? SalesReturn { get; set; }

        /// <summary>The invoice being corrected, when the credit is a price adjustment.</summary>
        public Guid? SalesInvoiceId { get; set; }
        public virtual SalesInvoice? SalesInvoice { get; set; }

        public DateTime CreditNoteDate { get; set; }

        public string Reason { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;

        public CreditNoteStatus Status { get; set; }

        public decimal SubTotal { get; set; }
        public decimal TotalTax { get; set; }

        /// <summary>What the credit note is worth in total, tax included.</summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// How much of the credit has already been spent against invoices. The note is
        /// fully applied once this reaches the total.
        /// </summary>
        public decimal AppliedAmount { get; set; }

        public string CreatedBy { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public virtual ICollection<CreditNoteItem>? CreditNoteItems { get; set; }
    }
}
