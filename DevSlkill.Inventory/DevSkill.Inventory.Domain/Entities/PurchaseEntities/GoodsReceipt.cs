using DevSkill.Inventory.Domain.Enums;

namespace DevSkill.Inventory.Domain.Entities.PurchaseEntities
{
    /// <summary>
    /// Goods actually arriving against a purchase order. This is the only purchase
    /// document that increases stock, and it does so when it is confirmed.
    /// </summary>
    public class GoodsReceipt : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string GoodsReceiptNo { get; set; } = string.Empty;

        public Guid PurchaseOrderId { get; set; }
        public virtual PurchaseOrder? PurchaseOrder { get; set; }

        public Guid SupplierId { get; set; }
        public virtual Supplier? Supplier { get; set; }

        /// <summary>
        /// Copied from the purchase order. A receipt always lands in the warehouse
        /// the order was raised for.
        /// </summary>
        public Guid BusinessLocationId { get; set; }
        public virtual BusinessLocation? BusinessLocation { get; set; }

        public DateTime ReceiptDate { get; set; }

        /// <summary>
        /// The supplier's own delivery note reference, so a physical document can be
        /// matched to this record.
        /// </summary>
        public string SupplierChallanNo { get; set; } = string.Empty;

        public string ReceivedBy { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;

        public GoodsReceiptStatus Status { get; set; }

        public string CreatedBy { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public virtual ICollection<GoodsReceiptItem>? GoodsReceiptItems { get; set; }
    }
}
