using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.Enums;

namespace DevSkill.Inventory.Domain.Entities.PurchaseEntities
{
    /// <summary>
    /// The vendor side counterpart of <see cref="Customer"/>. Every purchase
    /// document is raised against one of these.
    /// </summary>
    public class Supplier : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string SupplierName { get; set; } = string.Empty;
        public string SupplierCode { get; set; } = string.Empty;

        public string ContactPerson { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// Tax registration number, printed on the purchase invoice.
        /// </summary>
        public string TaxNumber { get; set; } = string.Empty;

        /// <summary>
        /// Default terms a purchase document starts with. Reuses the same lookup
        /// the sales side uses, so both modules state terms in one wording.
        /// </summary>
        public Guid? PaymentTermId { get; set; }
        public virtual PaymentTerm? PaymentTerm { get; set; }

        public decimal CreditLimit { get; set; }
        public decimal OpeningBalance { get; set; }

        /// <summary>
        /// Running payable to the supplier. Only purchase invoice, return and
        /// payment operations change this, always inside a transaction.
        /// </summary>
        public decimal CurrentOutstanding { get; set; }

        public SupplierStatus Status { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
