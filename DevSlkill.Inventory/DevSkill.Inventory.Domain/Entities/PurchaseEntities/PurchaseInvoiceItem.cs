namespace DevSkill.Inventory.Domain.Entities.PurchaseEntities
{
    public class PurchaseInvoiceItem : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public Guid PurchaseInvoiceId { get; set; }
        public virtual PurchaseInvoice? PurchaseInvoice { get; set; }

        public Guid ProductId { get; set; }
        public virtual Product? Product { get; set; }

        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TaxRate { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal LineTotal { get; set; }
    }
}
