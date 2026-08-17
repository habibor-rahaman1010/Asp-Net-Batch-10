namespace DevSkill.Inventory.Domain.Entities.PurchaseEntities
{
    public class PurchaseRequisitionItem : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public Guid PurchaseRequisitionId { get; set; }
        public virtual PurchaseRequisition? PurchaseRequisition { get; set; }

        public Guid ProductId { get; set; }
        public virtual Product? Product { get; set; }

        public decimal Quantity { get; set; }

        /// <summary>
        /// What the requester expects the item to cost. It is a guide for the buyer,
        /// never the price that ends up on the purchase order.
        /// </summary>
        public decimal EstimatedUnitPrice { get; set; }

        /// <summary>
        /// How much of this line has already gone onto purchase orders. It is what
        /// makes a requisition convertible in more than one go.
        /// </summary>
        public decimal OrderedQuantity { get; set; }

        public string Remarks { get; set; } = string.Empty;
    }
}
