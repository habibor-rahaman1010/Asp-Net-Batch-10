using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Infrastructure.RazorUtility;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    /// <summary>
    /// A credit note raised by hand, which is always a price adjustment. A note that
    /// pays for returned goods is written by the return itself and never from here.
    /// </summary>
    public class CreditNoteCreateModel
    {
        [Display(Name = "Credit Note No")]
        public string CreditNoteNo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Customer is required.")]
        public Guid CustomerId { get; set; }
        public IList<SelectListItem> Customers { get; set; }

        /// <summary>The invoice being corrected. The credit can never come to more than it did.</summary>
        [Display(Name = "Against Invoice")]
        public Guid? SalesInvoiceId { get; set; }
        public IList<SelectListItem> SalesInvoices { get; set; }

        [Display(Name = "Credit Note Date")]
        public DateTime CreditNoteDate { get; set; }

        [Required(ErrorMessage = "A reason is needed, so the credit can be explained later.")]
        [StringLength(500)]
        public string Reason { get; set; } = string.Empty;

        [StringLength(1000)]
        public string Notes { get; set; } = string.Empty;

        /// <summary>
        /// Ticking this puts the credit on the customer account straight away, so it
        /// can be spent. Left unticked it is only a draft.
        /// </summary>
        [Display(Name = "Issue the credit now")]
        public bool IssueImmediately { get; set; } = true;

        public List<CreditNoteItemModel> CreditNoteItems { get; set; }

        public CreditNoteCreateModel()
        {
            Customers = new List<SelectListItem>();
            SalesInvoices = new List<SelectListItem>();
            CreditNoteDate = DateTime.Today;

            CreditNoteItems = new List<CreditNoteItemModel>
            {
                new CreditNoteItemModel()
            };
        }

        public void SetCustomerValues(IList<Customer> customers)
        {
            Customers = Utility.ConvertCustomers(customers);
        }

        public void SetSalesInvoiceValues(IList<SalesInvoice> salesInvoices)
        {
            SalesInvoices = Utility.ConvertSalesInvoices(salesInvoices);
        }
    }
}
