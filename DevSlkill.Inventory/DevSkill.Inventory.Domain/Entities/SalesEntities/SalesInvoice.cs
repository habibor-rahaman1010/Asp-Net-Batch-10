using DevSkill.Inventory.Domain.Enums;

namespace DevSkill.Inventory.Domain.Entities.SalesEntities
{
    /// <summary>
    /// What the customer is being billed for goods already delivered. Posting it is
    /// the only thing that raises the customer's outstanding balance; it never
    /// touches stock, because the delivery has already done that.
    /// </summary>
    public class SalesInvoice : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string InvoiceNo { get; set; } = string.Empty;

        public Guid CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }

        public Guid SalesOrderId { get; set; }
        public virtual SalesOrder? SalesOrder { get; set; }

        /// <summary>
        /// Set when the invoice covers one specific shipment. Leaving it empty bills
        /// everything delivered on the order so far.
        /// </summary>
        public Guid? DeliveryId { get; set; }
        public virtual Delivery? Delivery { get; set; }

        /// <summary>Copied from the order so the reports can group without a join.</summary>
        public Guid BusinessLocationId { get; set; }
        public virtual BusinessLocation? BusinessLocation { get; set; }

        public Guid? SalespersonId { get; set; }
        public virtual Salesperson? Salesperson { get; set; }

        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }

        public Guid? PaymentTermId { get; set; }
        public virtual PaymentTerm? PaymentTerm { get; set; }

        public string Notes { get; set; } = string.Empty;

        public SalesInvoiceStatus Status { get; set; }

        // Every amount is calculated on the server from the delivered quantities and
        // the order's agreed prices. Browser values are never trusted.
        public decimal SubTotal { get; set; }
        public decimal ItemDiscountTotal { get; set; }
        public decimal TotalTax { get; set; }
        public decimal OtherCharges { get; set; }
        public decimal GrandTotal { get; set; }

        /// <summary>Sum of the collections allocated to this invoice.</summary>
        public decimal PaidAmount { get; set; }

        /// <summary>Grand total less what has been collected. Kept for fast ageing reports.</summary>
        public decimal DueAmount { get; set; }

        public string CreatedBy { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public virtual ICollection<SalesInvoiceItem>? SalesInvoiceItems { get; set; }
    }
}
