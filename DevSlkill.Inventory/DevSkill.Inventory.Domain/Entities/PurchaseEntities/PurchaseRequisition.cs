using DevSkill.Inventory.Domain.Enums;

namespace DevSkill.Inventory.Domain.Entities.PurchaseEntities
{
    /// <summary>
    /// An internal request to buy something. It names no supplier and no firm price,
    /// and it never touches stock. Once approved it can be turned into one or more
    /// purchase orders.
    /// </summary>
    public class PurchaseRequisition : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string RequisitionNo { get; set; } = string.Empty;

        public DateTime RequisitionDate { get; set; }

        /// <summary>
        /// When the goods are needed in the warehouse.
        /// </summary>
        public DateTime RequiredDate { get; set; }

        /// <summary>
        /// Warehouse the goods are requested for. It also decides which products
        /// the lines may hold.
        /// </summary>
        public Guid BusinessLocationId { get; set; }
        public virtual BusinessLocation? BusinessLocation { get; set; }

        /// <summary>
        /// Identity user who raised the request. Identity lives in a different
        /// DbContext, so this is stored without a foreign key.
        /// </summary>
        public Guid? RequestedById { get; set; }
        public string RequestedByName { get; set; } = string.Empty;

        public string Notes { get; set; } = string.Empty;

        public PurchaseRequisitionStatus Status { get; set; }

        /// <summary>
        /// Filled by the approval flow only. A rejection always carries a reason so
        /// the requester knows what to change.
        /// </summary>
        public Guid? ApprovedById { get; set; }
        public string ApprovedByName { get; set; } = string.Empty;
        public DateTime? ApprovedDate { get; set; }
        public string RejectionReason { get; set; } = string.Empty;

        /// <summary>
        /// Indicative value only, summed from the estimated line prices. The real
        /// money is decided on the purchase order.
        /// </summary>
        public decimal EstimatedTotal { get; set; }

        public string CreatedBy { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public virtual ICollection<PurchaseRequisitionItem>? PurchaseRequisitionItems { get; set; }
    }
}
