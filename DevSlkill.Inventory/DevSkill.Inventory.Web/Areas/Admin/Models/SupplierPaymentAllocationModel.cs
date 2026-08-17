using System.ComponentModel.DataAnnotations;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    /// <summary>
    /// One invoice on the payment screen. Everything but the allocated amount comes
    /// from the invoice itself and is shown read only.
    /// </summary>
    public class SupplierPaymentAllocationModel
    {
        public Guid PurchaseInvoiceId { get; set; }

        [Display(Name = "Invoice No")]
        public string InvoiceNo { get; set; } = string.Empty;

        [Display(Name = "Supplier Invoice No")]
        public string SupplierInvoiceNo { get; set; } = string.Empty;

        [Display(Name = "Invoice Date")]
        public DateTime InvoiceDate { get; set; }

        [Display(Name = "Due Date")]
        public DateTime DueDate { get; set; }

        /// <summary>Days past the due date, negative while the invoice is still current.</summary>
        [Display(Name = "Overdue")]
        public int OverdueDays { get; set; }

        [Display(Name = "Invoice Total")]
        public decimal GrandTotal { get; set; }

        [Display(Name = "Already Paid")]
        public decimal PaidAmount { get; set; }

        /// <summary>Highest amount this payment may put on the invoice.</summary>
        [Display(Name = "Outstanding")]
        public decimal RemainingAmount { get; set; }

        [Display(Name = "Paying Now")]
        public decimal AllocatedAmount { get; set; }

        [StringLength(500)]
        public string Remarks { get; set; } = string.Empty;
    }
}
