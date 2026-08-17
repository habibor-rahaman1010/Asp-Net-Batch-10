using DevSkill.Inventory.Domain.Enums;

namespace DevSkill.Inventory.Domain.Entities.PurchaseEntities
{
    /// <summary>
    /// Goods going back to the supplier. Only what was actually received may be
    /// returned, and confirming the return is what takes the stock back out.
    /// </summary>
    public class PurchaseReturn : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string ReturnNo { get; set; } = string.Empty;

        /// <summary>
        /// The receipt the goods came in on. Returning against a specific delivery is
        /// what makes the returned quantity provable.
        /// </summary>
        public Guid GoodsReceiptId { get; set; }
        public virtual GoodsReceipt? GoodsReceipt { get; set; }

        public Guid SupplierId { get; set; }
        public virtual Supplier? Supplier { get; set; }

        public Guid BusinessLocationId { get; set; }
        public virtual BusinessLocation? BusinessLocation { get; set; }

        public DateTime ReturnDate { get; set; }

        public string Reason { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;

        public PurchaseReturnStatus Status { get; set; }

        /// <summary>
        /// Value of the returned goods. Confirming the return takes this off what is
        /// owed to the supplier.
        /// </summary>
        public decimal TotalAmount { get; set; }

        public string CreatedBy { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public virtual ICollection<PurchaseReturnItem>? PurchaseReturnItems { get; set; }
    }
}
