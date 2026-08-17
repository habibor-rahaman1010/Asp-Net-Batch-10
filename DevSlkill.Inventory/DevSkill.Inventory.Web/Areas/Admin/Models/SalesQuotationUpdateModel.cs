using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Infrastructure.RazorUtility;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    public class SalesQuotationUpdateModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Quotation No")]
        public string QuotationNo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Customer is required.")]
        public Guid CustomerId { get; set; }
        public IList<SelectListItem> Customers { get; set; }

        [Display(Name = "Quotation Date")]
        public DateTime QuotationDate { get; set; }

        [Display(Name = "Valid Until")]
        public DateTime ValidUntil { get; set; }

        [Required(ErrorMessage = "Warehouse is required.")]
        [Display(Name = "Warehouse")]
        public Guid BusinessLocationId { get; set; }
        public IList<SelectListItem> BusinessLocations { get; set; }

        public Guid? SalespersonId { get; set; }
        public IList<SelectListItem> Salespersons { get; set; }

        [Display(Name = "Payment Terms")]
        public Guid? PaymentTermId { get; set; }
        public IList<SelectListItem> PaymentTerms { get; set; }

        [StringLength(100)]
        [Display(Name = "Customer Reference")]
        public string CustomerReference { get; set; } = string.Empty;

        [Display(Name = "Other Charges")]
        [Range(0, 999999999999.99, ErrorMessage = "Other charges cannot be negative.")]
        public decimal OtherCharges { get; set; }

        [StringLength(1000)]
        public string Notes { get; set; } = string.Empty;

        public SalesQuotationStatus Status { get; set; }
        public IList<SelectListItem> Statuses { get; set; }

        public List<SalesQuotationItemModel> SalesQuotationItems { get; set; }

        public SalesQuotationUpdateModel()
        {
            Customers = new List<SelectListItem>();
            BusinessLocations = new List<SelectListItem>();
            Salespersons = new List<SelectListItem>();
            PaymentTerms = new List<SelectListItem>();

            // Accepting, rejecting and expiring are written by their own flows and
            // never by an edit.
            Statuses = Utility.ConvertEnumToSelectList<SalesQuotationStatus>()
                .Where(x => x.Value == ((int)SalesQuotationStatus.Draft).ToString()
                         || x.Value == ((int)SalesQuotationStatus.Sent).ToString())
                .ToList();

            SalesQuotationItems = new List<SalesQuotationItemModel>();
        }

        public void SetCustomerValues(IList<Customer> customers)
        {
            Customers = Utility.ConvertCustomers(customers);
        }

        public void SetBusinessLocationValues(IList<BusinessLocation> businessLocations)
        {
            BusinessLocations = Utility.ConvertBusinessLocations(businessLocations);
        }

        public void SetSalespersonValues(IList<Salesperson> salespersons)
        {
            Salespersons = Utility.ConvertSalespersons(salespersons);
        }

        public void SetPaymentTermValues(IList<PaymentTerm> paymentTerms)
        {
            PaymentTerms = Utility.ConvertPaymentTerms(paymentTerms);
        }
    }
}
