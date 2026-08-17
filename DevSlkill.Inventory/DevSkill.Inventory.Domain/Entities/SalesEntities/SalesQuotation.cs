using DevSkill.Inventory.Domain.Enums;

namespace DevSkill.Inventory.Domain.Entities.SalesEntities
{
    /// <summary>
    /// A priced offer made to a customer. It never touches stock and never posts to
    /// the customer ledger; it only states what the sale would cost while the offer
    /// is still being held.
    /// </summary>
    public class SalesQuotation : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string QuotationNo { get; set; } = string.Empty;

        public Guid CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }

        public DateTime QuotationDate { get; set; }

        /// <summary>After this date the prices quoted are no longer being held.</summary>
        public DateTime ValidUntil { get; set; }

        /// <summary>
        /// Warehouse the sale would be served from. It also decides which products
        /// the lines may hold.
        /// </summary>
        public Guid BusinessLocationId { get; set; }
        public virtual BusinessLocation? BusinessLocation { get; set; }

        public Guid? SalespersonId { get; set; }
        public virtual Salesperson? Salesperson { get; set; }

        public Guid? PaymentTermId { get; set; }
        public virtual PaymentTerm? PaymentTerm { get; set; }

        /// <summary>The customer's own enquiry or tender reference, for their filing.</summary>
        public string CustomerReference { get; set; } = string.Empty;

        public string Notes { get; set; } = string.Empty;

        public SalesQuotationStatus Status { get; set; }

        // Every amount below is calculated on the server from the entered prices and
        // the product's tax setup. Browser totals are never trusted.
        public decimal SubTotal { get; set; }
        public decimal ItemDiscountTotal { get; set; }
        public decimal TotalTax { get; set; }

        /// <summary>Freight, handling and the like, belonging to no single line.</summary>
        public decimal OtherCharges { get; set; }

        public decimal GrandTotal { get; set; }

        public string CreatedBy { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public virtual ICollection<SalesQuotationItem>? SalesQuotationItems { get; set; }
    }
}
