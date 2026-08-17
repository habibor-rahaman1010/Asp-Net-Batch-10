using DevSkill.Inventory.Domain.Enums;

namespace DevSkill.Inventory.Domain.Entities.PurchaseEntities
{
    /// <summary>
    /// Money paid to a supplier. One payment can settle several invoices at once and
    /// may settle any of them only in part, which is what the allocations record.
    /// </summary>
    public class SupplierPayment : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string PaymentNo { get; set; } = string.Empty;

        public Guid SupplierId { get; set; }
        public virtual Supplier? Supplier { get; set; }

        public DateTime PaymentDate { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        /// <summary>Cheque number, transaction id or whatever the method leaves behind.</summary>
        public string ReferenceNo { get; set; } = string.Empty;

        /// <summary>
        /// What actually left the account. The allocations may add up to less than
        /// this, and the remainder simply sits against the supplier as an advance.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>Sum of the allocations, kept so the list can be read at a glance.</summary>
        public decimal AllocatedAmount { get; set; }

        public string Notes { get; set; } = string.Empty;

        public SupplierPaymentStatus Status { get; set; }

        public string CreatedBy { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public virtual ICollection<SupplierPaymentAllocation>? SupplierPaymentAllocations { get; set; }
    }
}
