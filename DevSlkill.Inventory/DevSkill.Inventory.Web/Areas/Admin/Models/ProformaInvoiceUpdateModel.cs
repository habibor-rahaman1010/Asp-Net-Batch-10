using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Infrastructure.RazorUtility;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    public class ProformaInvoiceUpdateModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Proforma No")]
        public string ProformaNo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Customer is required.")]
        public Guid CustomerId { get; set; }
        public IList<SelectListItem> Customers { get; set; }

        [Display(Name = "Proforma Date")]
        public DateTime ProformaDate { get; set; }

        [Display(Name = "Valid Until")]
        public DateTime ValidUntil { get; set; }

        [Required(ErrorMessage = "Warehouse is required.")]
        [Display(Name = "Warehouse")]
        public Guid BusinessLocationId { get; set; }
        public IList<SelectListItem> BusinessLocations { get; set; }

        [Display(Name = "Salesperson")]
        public Guid? SalespersonId { get; set; }
        public IList<SelectListItem> Salespersons { get; set; }

        [Display(Name = "Payment Terms")]
        public Guid? PaymentTermId { get; set; }
        public IList<SelectListItem> PaymentTerms { get; set; }

        [StringLength(1000)]
        public string Notes { get; set; } = string.Empty;

        public ProformaInvoiceStatus Status { get; set; }
        public IList<SelectListItem> Statuses { get; set; }

        public List<ProformaInvoiceItemModel> ProformaInvoiceItems { get; set; }

        public ProformaInvoiceUpdateModel()
        {
            Customers = new List<SelectListItem>();
            BusinessLocations = new List<SelectListItem>();
            Salespersons = new List<SelectListItem>();
            PaymentTerms = new List<SelectListItem>();

            // Converted is reached through the conversion flow and the two delivered
            // states are written by the delivery flow, never through an edit.
            var flowDrivenStatuses = new[]
            {
                ((int)ProformaInvoiceStatus.Converted).ToString(),
                ((int)ProformaInvoiceStatus.PartiallyDelivered).ToString(),
                ((int)ProformaInvoiceStatus.Delivered).ToString()
            };

            Statuses = Utility.ConvertEnumToSelectList<ProformaInvoiceStatus>()
                .Where(x => !flowDrivenStatuses.Contains(x.Value))
                .ToList();

            ProformaInvoiceItems = new List<ProformaInvoiceItemModel>();
        }

        public void SetCustomerValues(IList<Customer> customers)
        {
            Customers = Utility.ConvertCustomers(customers);
        }

        public void SetBusinessLocationValues(IList<BusinessLocation> businessLocations)
        {
            BusinessLocations = Utility.ConvertBusinessLocations(businessLocations);
        }

        public void SetSalespersonValues(IList<SelectListItem> salespersons)
        {
            Salespersons = salespersons;
        }

        public void SetPaymentTermValues(IList<PaymentTerm> paymentTerms)
        {
            PaymentTerms = Utility.ConvertPaymentTerms(paymentTerms);
        }
    }
}
