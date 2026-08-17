using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.Enums;

namespace DevSkill.Inventory.Domain.Entities.PurchaseEntities
{
    /// <summary>
    /// A firm commitment to buy from one supplier, delivered into one warehouse.
    /// Raising it never changes stock; only the goods receipt does that.
    /// </summary>
    public class PurchaseOrder : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string PurchaseOrderNo { get; set; } = string.Empty;

        public Guid SupplierId { get; set; }
        public virtual Supplier? Supplier { get; set; }

        public DateTime OrderDate { get; set; }
        public DateTime ExpectedDeliveryDate { get; set; }

        /// <summary>
        /// Warehouse the goods are delivered into. It also decides which products
        /// the lines may hold.
        /// </summary>
        public Guid BusinessLocationId { get; set; }
        public virtual BusinessLocation? BusinessLocation { get; set; }

        public Guid? PaymentTermId { get; set; }
        public virtual PaymentTerm? PaymentTerm { get; set; }

        /// <summary>
        /// Set when the order came out of an approved requisition, so the chain
        /// PR -> PO -> GRN -> Invoice stays traceable.
        /// </summary>
        public Guid? PurchaseRequisitionId { get; set; }
        public virtual PurchaseRequisition? PurchaseRequisition { get; set; }

        /// <summary>
        /// Set when the order came out of an awarded supplier quotation, which is what
        /// keeps the price on the order tied back to the quote it was won on.
        /// </summary>
        public Guid? SupplierQuotationId { get; set; }
        public virtual SupplierQuotation? SupplierQuotation { get; set; }

        public string Notes { get; set; } = string.Empty;

        public PurchaseOrderStatus Status { get; set; }

        public Guid? ApprovedById { get; set; }
        public string ApprovedByName { get; set; } = string.Empty;
        public DateTime? ApprovedDate { get; set; }
        public string RejectionReason { get; set; } = string.Empty;

        // The unit price is negotiated with the supplier, so it is entered by the
        // buyer. Every total below is still calculated on the server from those
        // entries and the product's tax setup; browser totals are never trusted.
        public decimal SubTotal { get; set; }
        public decimal ItemDiscountTotal { get; set; }
        public decimal TotalTax { get; set; }

        /// <summary>
        /// Freight, handling and the like. It is part of what the supplier bills but
        /// belongs to no single line.
        /// </summary>
        public decimal OtherCharges { get; set; }

        public decimal GrandTotal { get; set; }

        public string CreatedBy { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public virtual ICollection<PurchaseOrderItem>? PurchaseOrderItems { get; set; }
    }
}
