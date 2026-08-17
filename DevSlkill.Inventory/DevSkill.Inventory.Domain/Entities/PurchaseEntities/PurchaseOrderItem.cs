namespace DevSkill.Inventory.Domain.Entities.PurchaseEntities
{
    public class PurchaseOrderItem : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public Guid PurchaseOrderId { get; set; }
        public virtual PurchaseOrder? PurchaseOrder { get; set; }

        public Guid ProductId { get; set; }
        public virtual Product? Product { get; set; }

        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TaxRate { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal LineTotal { get; set; }

        /// <summary>
        /// How much has actually arrived. Written by the goods receipt flow and read
        /// by receiving, invoicing and the three way match.
        /// </summary>
        public decimal ReceivedQuantity { get; set; }

        /// <summary>
        /// How much has already been billed by the supplier. It is what stops the
        /// same goods being invoiced twice.
        /// </summary>
        public decimal InvoicedQuantity { get; set; }
    }
}
