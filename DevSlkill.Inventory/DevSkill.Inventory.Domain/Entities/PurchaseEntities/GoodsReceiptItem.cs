namespace DevSkill.Inventory.Domain.Entities.PurchaseEntities
{
    public class GoodsReceiptItem : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public Guid GoodsReceiptId { get; set; }
        public virtual GoodsReceipt? GoodsReceipt { get; set; }

        public Guid ProductId { get; set; }
        public virtual Product? Product { get; set; }

        /// <summary>
        /// What the purchase order line asked for, copied here so the receipt still
        /// reads correctly after the order is closed.
        /// </summary>
        public decimal OrderedQuantity { get; set; }

        /// <summary>
        /// What actually arrived and was accepted. Only this quantity moves stock.
        /// </summary>
        public decimal ReceivedQuantity { get; set; }

        /// <summary>
        /// Arrived but refused, damaged or failing inspection. It is recorded for the
        /// supplier's benefit and never enters stock.
        /// </summary>
        public decimal RejectedQuantity { get; set; }

        /// <summary>
        /// How much of this receipt has already been returned to the supplier. It is
        /// what caps a purchase return.
        /// </summary>
        public decimal ReturnedQuantity { get; set; }

        /// <summary>Unit price copied from the order line, so the receipt can be valued.</summary>
        public decimal UnitPrice { get; set; }

        public string Remarks { get; set; } = string.Empty;
    }
}
