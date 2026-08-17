namespace DevSkill.Inventory.Domain.Entities.SalesEntities
{
    public class SalesInvoiceItem : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public Guid SalesInvoiceId { get; set; }
        public virtual SalesInvoice? SalesInvoice { get; set; }

        public Guid ProductId { get; set; }
        public virtual Product? Product { get; set; }

        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TaxRate { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal LineTotal { get; set; }

        /// <summary>
        /// How much of this billed quantity has already gone back on a sales return.
        /// It is what stops the same goods being credited twice.
        /// </summary>
        public decimal ReturnedQuantity { get; set; }
    }
}
