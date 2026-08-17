namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    /// <summary>
    /// One open invoice on the collection screen, with what may still be put on it.
    /// Everything but the allocated amount is read only, because it all comes off the
    /// invoice; the server works the same figures out again when the money is taken.
    /// </summary>
    public class CustomerPaymentAllocationModel
    {
        public Guid SalesInvoiceId { get; set; }
        public string InvoiceNo { get; set; } = string.Empty;

        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }

        /// <summary>Days past the due date, so the oldest debt stands out.</summary>
        public int OverdueDays { get; set; }

        public decimal GrandTotal { get; set; }
        public decimal PaidAmount { get; set; }

        /// <summary>Already pencilled in by other draft collections.</summary>
        public decimal PendingAmount { get; set; }

        /// <summary>The most this collection may put on the invoice.</summary>
        public decimal RemainingAmount { get; set; }

        /// <summary>What is going against it now. This is the only field the user types.</summary>
        public decimal AllocatedAmount { get; set; }

        /// <summary>Filled by the server so the stored allocation records what was owed.</summary>
        public decimal DueAmount { get; set; }

        public string Remarks { get; set; } = string.Empty;
    }
}
