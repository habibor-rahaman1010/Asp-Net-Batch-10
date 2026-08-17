namespace DevSkill.Inventory.Domain.Entities.SalesEntities
{
    public class SalesReturnItem : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public Guid SalesReturnId { get; set; }
        public virtual SalesReturn? SalesReturn { get; set; }

        public Guid ProductId { get; set; }
        public virtual Product? Product { get; set; }

        /// <summary>What the invoice line billed, copied for context.</summary>
        public decimal InvoicedQuantity { get; set; }

        public decimal ReturnQuantity { get; set; }

        /// <summary>Unit price copied from the invoice, so the credit can be valued.</summary>
        public decimal UnitPrice { get; set; }

        public decimal TaxRate { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal LineTotal { get; set; }

        public string Remarks { get; set; } = string.Empty;
    }
}
