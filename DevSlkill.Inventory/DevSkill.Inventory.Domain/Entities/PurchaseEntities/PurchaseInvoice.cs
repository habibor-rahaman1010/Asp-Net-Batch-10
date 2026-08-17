using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.Enums;

namespace DevSkill.Inventory.Domain.Entities.PurchaseEntities
{
    /// <summary>
    /// What the supplier is billing for goods already received. Posting it is the
    /// only thing that raises the supplier's outstanding balance; it never touches
    /// stock, because the goods receipt has already done that.
    /// </summary>
    public class PurchaseInvoice : IEntity<Guid>
    {
        public Guid Id { get; set; }

        /// <summary>Our own reference, issued in sequence.</summary>
        public string InvoiceNo { get; set; } = string.Empty;

        /// <summary>The number printed on the supplier's own invoice.</summary>
        public string SupplierInvoiceNo { get; set; } = string.Empty;

        public Guid SupplierId { get; set; }
        public virtual Supplier? Supplier { get; set; }

        public Guid PurchaseOrderId { get; set; }
        public virtual PurchaseOrder? PurchaseOrder { get; set; }

        /// <summary>
        /// Set when the invoice covers one specific delivery. Leaving it empty bills
        /// everything received on the order so far.
        /// </summary>
        public Guid? GoodsReceiptId { get; set; }
        public virtual GoodsReceipt? GoodsReceipt { get; set; }

        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }

        public Guid? PaymentTermId { get; set; }
        public virtual PaymentTerm? PaymentTerm { get; set; }

        public string Notes { get; set; } = string.Empty;

        public PurchaseInvoiceStatus Status { get; set; }

        // Every amount is calculated on the server from the received quantities and
        // the order's agreed prices. Browser values are never trusted.
        public decimal SubTotal { get; set; }
        public decimal ItemDiscountTotal { get; set; }
        public decimal TotalTax { get; set; }
        public decimal OtherCharges { get; set; }
        public decimal GrandTotal { get; set; }

        /// <summary>Sum of the payments allocated to this invoice.</summary>
        public decimal PaidAmount { get; set; }

        /// <summary>Grand total less what has been paid. Kept for fast ageing reports.</summary>
        public decimal DueAmount { get; set; }

        public string CreatedBy { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public virtual ICollection<PurchaseInvoiceItem>? PurchaseInvoiceItems { get; set; }
    }
}
