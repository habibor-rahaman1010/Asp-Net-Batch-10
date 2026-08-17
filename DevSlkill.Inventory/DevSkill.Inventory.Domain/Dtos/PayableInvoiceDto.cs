namespace DevSkill.Inventory.Domain.Dtos
{
    /// <summary>
    /// One posted invoice seen from the payment side: what it came to, what has
    /// already been settled and what may still be allocated right now.
    /// </summary>
    public class PayableInvoiceDto
    {
        public Guid PurchaseInvoiceId { get; set; }
        public string InvoiceNo { get; set; } = string.Empty;
        public string SupplierInvoiceNo { get; set; } = string.Empty;

        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }

        /// <summary>Days past the due date, negative while the invoice is still current.</summary>
        public int OverdueDays { get; set; }

        public decimal GrandTotal { get; set; }
        public decimal PaidAmount { get; set; }

        /// <summary>Sum of the draft payments already pointing at this invoice.</summary>
        public decimal PendingAmount { get; set; }

        /// <summary>Highest amount the current payment is allowed to put on this invoice.</summary>
        public decimal RemainingAmount { get; set; }

        public string Status { get; set; } = string.Empty;
    }
}
