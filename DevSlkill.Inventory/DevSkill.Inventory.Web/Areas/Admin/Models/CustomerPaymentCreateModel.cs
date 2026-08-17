using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Infrastructure.RazorUtility;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    public class CustomerPaymentCreateModel
    {
        [Display(Name = "Collection No")]
        public string PaymentNo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Customer is required.")]
        public Guid CustomerId { get; set; }
        public IList<SelectListItem> Customers { get; set; }

        [Display(Name = "Collection Date")]
        public DateTime PaymentDate { get; set; }

        [Display(Name = "Payment Method")]
        public PaymentMethod PaymentMethod { get; set; }
        public IList<SelectListItem> PaymentMethods { get; set; }

        [Display(Name = "Reference No")]
        [StringLength(100)]
        public string ReferenceNo { get; set; } = string.Empty;

        /// <summary>
        /// Set when the collection is funded by a credit note rather than by money.
        /// Issuing that note already credited the account, so this only settles the
        /// invoices it points at.
        /// </summary>
        [Display(Name = "Credit Note")]
        public Guid? CreditNoteId { get; set; }
        public IList<SelectListItem> CreditNotes { get; set; }

        [Display(Name = "Amount Received")]
        [Range(0.01, 999999999999.99, ErrorMessage = "A collection has to be for more than zero.")]
        public decimal Amount { get; set; }

        [StringLength(1000)]
        public string Notes { get; set; } = string.Empty;

        /// <summary>
        /// Ticking this takes the money in straight away, which settles the invoices it
        /// was put against. Left unticked it is only a draft.
        /// </summary>
        [Display(Name = "Receive the money now")]
        public bool ReceiveImmediately { get; set; } = true;

        public List<CustomerPaymentAllocationModel> CustomerPaymentAllocations { get; set; }

        public CustomerPaymentCreateModel()
        {
            Customers = new List<SelectListItem>();
            CreditNotes = new List<SelectListItem>();
            PaymentMethods = Utility.ConvertEnumToSelectList<PaymentMethod>();
            CustomerPaymentAllocations = new List<CustomerPaymentAllocationModel>();
            PaymentDate = DateTime.Today;
        }

        public void SetCustomerValues(IList<Customer> customers)
        {
            Customers = Utility.ConvertCustomers(customers);
        }

        public void SetCreditNoteValues(IList<CreditNote> creditNotes)
        {
            CreditNotes = Utility.ConvertCreditNotes(creditNotes);
        }
    }
}
