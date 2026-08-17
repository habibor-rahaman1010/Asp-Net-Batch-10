using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Infrastructure.RazorUtility;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    /// <summary>
    /// Only a draft collection is editable, and even then it stays with the customer
    /// it came from. Once the money is in, moving it around would silently re-settle
    /// invoices, so it is cancelled and taken again instead.
    /// </summary>
    public class CustomerPaymentUpdateModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Collection No")]
        public string PaymentNo { get; set; } = string.Empty;

        public Guid CustomerId { get; set; }

        [Display(Name = "Customer")]
        public string CustomerName { get; set; } = string.Empty;

        [Display(Name = "Collection Date")]
        public DateTime PaymentDate { get; set; }

        [Display(Name = "Payment Method")]
        public PaymentMethod PaymentMethod { get; set; }
        public IList<SelectListItem> PaymentMethods { get; set; }

        [Display(Name = "Reference No")]
        [StringLength(100)]
        public string ReferenceNo { get; set; } = string.Empty;

        [Display(Name = "Credit Note")]
        public Guid? CreditNoteId { get; set; }
        public IList<SelectListItem> CreditNotes { get; set; }

        [Display(Name = "Amount Received")]
        [Range(0.01, 999999999999.99, ErrorMessage = "A collection has to be for more than zero.")]
        public decimal Amount { get; set; }

        [StringLength(1000)]
        public string Notes { get; set; } = string.Empty;

        public List<CustomerPaymentAllocationModel> CustomerPaymentAllocations { get; set; }

        public CustomerPaymentUpdateModel()
        {
            CreditNotes = new List<SelectListItem>();
            PaymentMethods = Utility.ConvertEnumToSelectList<PaymentMethod>();
            CustomerPaymentAllocations = new List<CustomerPaymentAllocationModel>();
            PaymentDate = DateTime.Today;
        }

        public void SetCreditNoteValues(IList<CreditNote> creditNotes)
        {
            CreditNotes = Utility.ConvertCreditNotes(creditNotes);
        }
    }
}
