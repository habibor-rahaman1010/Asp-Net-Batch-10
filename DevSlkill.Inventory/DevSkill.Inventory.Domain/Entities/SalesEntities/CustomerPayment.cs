using DevSkill.Inventory.Domain.Enums;

namespace DevSkill.Inventory.Domain.Entities.SalesEntities
{
    /// <summary>
    /// Money collected from a customer. One collection can settle several invoices at
    /// once and may settle any of them only in part, which is what the allocations
    /// record.
    /// </summary>
    public class CustomerPayment : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string PaymentNo { get; set; } = string.Empty;

        public Guid CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }

        public DateTime PaymentDate { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        /// <summary>Cheque number, transaction id or whatever the method leaves behind.</summary>
        public string ReferenceNo { get; set; } = string.Empty;

        /// <summary>
        /// Set when the collection is funded by a credit note rather than by money.
        /// It is what spends that credit, so the same credit is never used twice.
        /// </summary>
        public Guid? CreditNoteId { get; set; }
        public virtual CreditNote? CreditNote { get; set; }

        /// <summary>
        /// What actually came in. The allocations may add up to less than this, and
        /// the remainder simply sits against the customer as an advance.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>Sum of the allocations, kept so the list can be read at a glance.</summary>
        public decimal AllocatedAmount { get; set; }

        public string Notes { get; set; } = string.Empty;

        public CustomerPaymentStatus Status { get; set; }

        public string CreatedBy { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public virtual ICollection<CustomerPaymentAllocation>? CustomerPaymentAllocations { get; set; }
    }
}
