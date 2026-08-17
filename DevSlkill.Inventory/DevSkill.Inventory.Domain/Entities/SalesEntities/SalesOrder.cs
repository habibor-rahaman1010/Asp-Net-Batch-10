using DevSkill.Inventory.Domain.Enums;

namespace DevSkill.Inventory.Domain.Entities.SalesEntities
{
    /// <summary>
    /// A firm commitment to sell to one customer out of one warehouse. Confirming it
    /// never changes stock; only a reservation holds stock and only a delivery takes
    /// it out.
    /// </summary>
    public class SalesOrder : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string SalesOrderNo { get; set; } = string.Empty;

        public Guid CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }

        public DateTime OrderDate { get; set; }
        public DateTime ExpectedDeliveryDate { get; set; }

        /// <summary>
        /// Warehouse the goods are served from. It also decides which products the
        /// lines may hold.
        /// </summary>
        public Guid BusinessLocationId { get; set; }
        public virtual BusinessLocation? BusinessLocation { get; set; }

        public Guid? SalespersonId { get; set; }
        public virtual Salesperson? Salesperson { get; set; }

        public Guid? PaymentTermId { get; set; }
        public virtual PaymentTerm? PaymentTerm { get; set; }

        /// <summary>
        /// Set when the order came out of an accepted quotation, so the chain
        /// Quotation -> Order -> Delivery -> Invoice stays traceable.
        /// </summary>
        public Guid? SalesQuotationId { get; set; }
        public virtual SalesQuotation? SalesQuotation { get; set; }

        /// <summary>The customer's own purchase order number, for their filing.</summary>
        public string CustomerReference { get; set; } = string.Empty;

        public string Notes { get; set; } = string.Empty;

        public SalesOrderStatus Status { get; set; }

        // Every total is calculated on the server from the entered prices and the
        // product's tax setup. Browser totals are never trusted.
        public decimal SubTotal { get; set; }
        public decimal ItemDiscountTotal { get; set; }
        public decimal TotalTax { get; set; }
        public decimal OtherCharges { get; set; }
        public decimal GrandTotal { get; set; }

        public string CreatedBy { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public virtual ICollection<SalesOrderItem>? SalesOrderItems { get; set; }
    }
}
