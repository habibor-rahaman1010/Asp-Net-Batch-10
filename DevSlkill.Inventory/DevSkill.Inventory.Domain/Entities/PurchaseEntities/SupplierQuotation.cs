using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.Enums;

namespace DevSkill.Inventory.Domain.Entities.PurchaseEntities
{
    /// <summary>
    /// One supplier's answer to a request for quotation. Several of these are
    /// compared side by side and exactly one of them is awarded.
    /// </summary>
    public class SupplierQuotation : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string QuotationNo { get; set; } = string.Empty;

        /// <summary>The supplier's own reference on the quotation they sent.</summary>
        public string SupplierQuotationNo { get; set; } = string.Empty;

        public Guid RequestForQuotationId { get; set; }
        public virtual RequestForQuotation? RequestForQuotation { get; set; }

        public Guid SupplierId { get; set; }
        public virtual Supplier? Supplier { get; set; }

        public DateTime QuotationDate { get; set; }

        /// <summary>After this date the prices quoted are no longer being held.</summary>
        public DateTime ValidUntil { get; set; }

        /// <summary>
        /// How long the supplier says delivery takes. It is one of the three things
        /// the comparison weighs, alongside price and terms.
        /// </summary>
        public int DeliveryDays { get; set; }

        public Guid? PaymentTermId { get; set; }
        public virtual PaymentTerm? PaymentTerm { get; set; }

        public string Notes { get; set; } = string.Empty;

        public SupplierQuotationStatus Status { get; set; }

        // Calculated on the server from the lines and the charges below; browser
        // totals are never trusted.
        public decimal SubTotal { get; set; }
        public decimal DiscountTotal { get; set; }
        public decimal OtherCharges { get; set; }
        public decimal GrandTotal { get; set; }

        public string CreatedBy { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public virtual ICollection<SupplierQuotationItem>? SupplierQuotationItems { get; set; }
    }
}
