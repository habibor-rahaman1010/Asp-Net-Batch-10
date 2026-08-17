namespace DevSkill.Inventory.Domain.Entities.SalesEntities
{
    public class SalesOrderItem : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public Guid SalesOrderId { get; set; }
        public virtual SalesOrder? SalesOrder { get; set; }

        public Guid ProductId { get; set; }
        public virtual Product? Product { get; set; }

        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TaxRate { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal LineTotal { get; set; }

        /// <summary>
        /// How much has actually left the warehouse. Written by the delivery flow and
        /// read by delivering, invoicing and the reports.
        /// </summary>
        public decimal DeliveredQuantity { get; set; }

        /// <summary>
        /// How much has already been billed to the customer. It is what stops the
        /// same goods being invoiced twice.
        /// </summary>
        public decimal InvoicedQuantity { get; set; }
    }
}
