namespace DevSkill.Inventory.Domain.Entities.SalesEntities
{
    public class ProformaInvoiceItem : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public Guid ProformaInvoiceId { get; set; }
        public virtual ProformaInvoice? ProformaInvoice { get; set; }

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
