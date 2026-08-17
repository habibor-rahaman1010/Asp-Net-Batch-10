namespace DevSkill.Inventory.Domain.Entities.SalesEntities
{
    /// <summary>
    /// How much of one collection went against one invoice. Splitting a collection
    /// this way is what lets an invoice be settled in instalments.
    /// </summary>
    public class CustomerPaymentAllocation : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public Guid CustomerPaymentId { get; set; }
        public virtual CustomerPayment? CustomerPayment { get; set; }

        public Guid SalesInvoiceId { get; set; }
        public virtual SalesInvoice? SalesInvoice { get; set; }

        /// <summary>What the invoice still owed when the allocation was made.</summary>
        public decimal DueAmount { get; set; }

        public decimal AllocatedAmount { get; set; }

        public string Remarks { get; set; } = string.Empty;
    }
}
