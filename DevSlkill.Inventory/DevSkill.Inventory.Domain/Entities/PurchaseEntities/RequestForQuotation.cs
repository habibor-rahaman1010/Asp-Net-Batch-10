using DevSkill.Inventory.Domain.Enums;

namespace DevSkill.Inventory.Domain.Entities.PurchaseEntities
{
    /// <summary>
    /// What we are asking suppliers to quote for. It commits us to nothing; the
    /// quotations come back against it and one of them is then awarded.
    /// </summary>
    public class RequestForQuotation : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string RfqNo { get; set; } = string.Empty;

        /// <summary>
        /// Set when the request came out of an approved requisition, so the chain
        /// PR -> RFQ -> Quotation -> PO stays traceable.
        /// </summary>
        public Guid? PurchaseRequisitionId { get; set; }
        public virtual PurchaseRequisition? PurchaseRequisition { get; set; }

        /// <summary>Warehouse the goods would be delivered into.</summary>
        public Guid BusinessLocationId { get; set; }
        public virtual BusinessLocation? BusinessLocation { get; set; }

        public DateTime RfqDate { get; set; }

        /// <summary>Last day a supplier quotation is accepted against this request.</summary>
        public DateTime ResponseDeadline { get; set; }

        public string Notes { get; set; } = string.Empty;

        public RequestForQuotationStatus Status { get; set; }

        /// <summary>
        /// The quotation that won. Set by awarding the request and cleared if the
        /// award is undone.
        /// </summary>
        public Guid? SelectedSupplierQuotationId { get; set; }

        public string CreatedBy { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public virtual ICollection<RequestForQuotationItem>? RequestForQuotationItems { get; set; }
    }
}
