namespace DevSkill.Inventory.Domain.Entities.PurchaseEntities
{
    /// <summary>
    /// How much of one payment went against one invoice. Splitting a payment this
    /// way is what lets an invoice be settled in instalments.
    /// </summary>
    public class SupplierPaymentAllocation : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public Guid SupplierPaymentId { get; set; }
        public virtual SupplierPayment? SupplierPayment { get; set; }

        public Guid PurchaseInvoiceId { get; set; }
        public virtual PurchaseInvoice? PurchaseInvoice { get; set; }

        /// <summary>What the invoice still owed when the allocation was made.</summary>
        public decimal DueAmount { get; set; }

        public decimal AllocatedAmount { get; set; }

        public string Remarks { get; set; } = string.Empty;
    }
}
