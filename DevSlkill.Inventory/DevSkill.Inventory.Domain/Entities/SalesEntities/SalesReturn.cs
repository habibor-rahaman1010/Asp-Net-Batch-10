using DevSkill.Inventory.Domain.Enums;

namespace DevSkill.Inventory.Domain.Entities.SalesEntities
{
    /// <summary>
    /// Goods coming back from the customer. Only what was actually invoiced may be
    /// returned, and confirming the return is what brings the stock back in.
    /// </summary>
    public class SalesReturn : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string ReturnNo { get; set; } = string.Empty;

        /// <summary>
        /// The invoice the goods were billed on. Returning against a specific invoice
        /// is what makes the returned quantity provable and the credit valuable.
        /// </summary>
        public Guid SalesInvoiceId { get; set; }
        public virtual SalesInvoice? SalesInvoice { get; set; }

        public Guid CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }

        /// <summary>Warehouse the goods come back into. It follows the invoice.</summary>
        public Guid BusinessLocationId { get; set; }
        public virtual BusinessLocation? BusinessLocation { get; set; }

        public DateTime ReturnDate { get; set; }

        public string Reason { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;

        public SalesReturnStatus Status { get; set; }

        /// <summary>
        /// Value of the returned goods, tax included. Confirming the return raises a
        /// credit note for exactly this amount.
        /// </summary>
        public decimal TotalAmount { get; set; }

        public string CreatedBy { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public virtual ICollection<SalesReturnItem>? SalesReturnItems { get; set; }
    }
}
