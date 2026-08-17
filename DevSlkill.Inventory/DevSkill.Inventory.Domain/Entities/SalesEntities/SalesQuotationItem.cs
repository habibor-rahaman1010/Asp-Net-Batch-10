namespace DevSkill.Inventory.Domain.Entities.SalesEntities
{
    public class SalesQuotationItem : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public Guid SalesQuotationId { get; set; }
        public virtual SalesQuotation? SalesQuotation { get; set; }

        public Guid ProductId { get; set; }
        public virtual Product? Product { get; set; }

        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TaxRate { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal LineTotal { get; set; }

        /// <summary>
        /// How much has already been turned into a sales order. It is what stops the
        /// same offer being ordered twice.
        /// </summary>
        public decimal OrderedQuantity { get; set; }

        public string Remarks { get; set; } = string.Empty;
    }
}
