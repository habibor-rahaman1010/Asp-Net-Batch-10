using DevSkill.Inventory.Domain.Enums;

namespace DevSkill.Inventory.Domain.Entities.SalesEntities
{
    /// <summary>
    /// A pre-sale document issued to a customer before an order is confirmed.
    /// It never touches stock and never posts to the customer ledger; it only
    /// states what the sale would cost if the customer accepts it.
    /// </summary>
    public class ProformaInvoice : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string ProformaNo { get; set; } = string.Empty;

        public Guid CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }

        public DateTime ProformaDate { get; set; }
        public DateTime ValidUntil { get; set; }

        public Guid BusinessLocationId { get; set; }
        public virtual BusinessLocation? BusinessLocation { get; set; }

        /// <summary>
        /// The salesperson the offer is credited to, the same one every other sales
        /// document names. <see cref="SalespersonName"/> keeps what they were called
        /// when the document was raised, so a later rename cannot rewrite history.
        /// </summary>
        public Guid? SalespersonId { get; set; }
        public virtual Salesperson? Salesperson { get; set; }
        public string SalespersonName { get; set; } = string.Empty;

        /// <summary>
        /// Chosen from the PaymentTerms lookup table, so every document states
        /// the terms in exactly the same words.
        /// </summary>
        public Guid? PaymentTermId { get; set; }
        public virtual PaymentTerm? PaymentTerm { get; set; }

        public string Notes { get; set; } = string.Empty;

        public ProformaInvoiceStatus Status { get; set; }

        // Every amount below is calculated on the server from the price lists,
        // discount rules and product tax setup. Browser values are never trusted.
        public decimal SubTotal { get; set; }
        public decimal ItemDiscountTotal { get; set; }
        public decimal OrderDiscount { get; set; }
        public decimal TotalTax { get; set; }
        public decimal GrandTotal { get; set; }

        public string CreatedBy { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public virtual ICollection<ProformaInvoiceItem>? ProformaInvoiceItems { get; set; }
    }
}
