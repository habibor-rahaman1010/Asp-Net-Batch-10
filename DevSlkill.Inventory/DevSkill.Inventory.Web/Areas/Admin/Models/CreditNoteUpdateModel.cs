using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Infrastructure.RazorUtility;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    /// <summary>
    /// Only a draft credit note raised by hand is editable. One that came out of a
    /// sales return belongs to that return, and an issued one is already sitting on
    /// the customer account.
    /// </summary>
    public class CreditNoteUpdateModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Credit Note No")]
        public string CreditNoteNo { get; set; } = string.Empty;

        public Guid CustomerId { get; set; }

        [Display(Name = "Customer")]
        public string CustomerName { get; set; } = string.Empty;

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

        public List<CreditNoteItemModel> CreditNoteItems { get; set; }

        public CreditNoteUpdateModel()
        {
            SalesInvoices = new List<SelectListItem>();
            CreditNoteItems = new List<CreditNoteItemModel>();
            CreditNoteDate = DateTime.Today;
        }

        public void SetSalesInvoiceValues(IList<SalesInvoice> salesInvoices)
        {
            SalesInvoices = Utility.ConvertSalesInvoices(salesInvoices);
        }
    }
}
