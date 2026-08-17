namespace DevSkill.Inventory.Domain.Dtos
{
    /// <summary>
    /// One posted invoice seen from the collection side: what it came to, what has
    /// already been received and what may still be allocated right now.
    /// </summary>
    public class ReceivableInvoiceDto
    {
        public Guid SalesInvoiceId { get; set; }
        public string InvoiceNo { get; set; } = string.Empty;

        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }

        /// <summary>Days past the due date, negative while the invoice is still current.</summary>
        public int OverdueDays { get; set; }

        public decimal GrandTotal { get; set; }
        public decimal PaidAmount { get; set; }

        /// <summary>Sum of the draft collections already pointing at this invoice.</summary>
        public decimal PendingAmount { get; set; }

        /// <summary>Highest amount the current collection is allowed to put on this invoice.</summary>
        public decimal RemainingAmount { get; set; }

        public string Status { get; set; } = string.Empty;
    }
}
