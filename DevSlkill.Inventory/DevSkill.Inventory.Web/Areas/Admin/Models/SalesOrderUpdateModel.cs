using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Infrastructure.RazorUtility;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    public class SalesOrderUpdateModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Sales Order No")]
        public string SalesOrderNo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Customer is required.")]
        public Guid CustomerId { get; set; }
        public IList<SelectListItem> Customers { get; set; }

        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }

        [Display(Name = "Expected Delivery")]
        public DateTime ExpectedDeliveryDate { get; set; }

        [Required(ErrorMessage = "Warehouse is required.")]
        [Display(Name = "Warehouse")]
        public Guid BusinessLocationId { get; set; }
        public IList<SelectListItem> BusinessLocations { get; set; }

        public Guid? SalespersonId { get; set; }
        public IList<SelectListItem> Salespersons { get; set; }

        [Display(Name = "Payment Terms")]
        public Guid? PaymentTermId { get; set; }
        public IList<SelectListItem> PaymentTerms { get; set; }

        public Guid? SalesQuotationId { get; set; }

        /// <summary>Shown read only, the source quotation is fixed once the order exists.</summary>
        [Display(Name = "From Quotation")]
        public string SalesQuotationNo { get; set; } = string.Empty;

        [StringLength(100)]
        [Display(Name = "Customer Reference")]
        public string CustomerReference { get; set; } = string.Empty;

        [Display(Name = "Other Charges")]
        [Range(0, 999999999999.99, ErrorMessage = "Other charges cannot be negative.")]
        public decimal OtherCharges { get; set; }

        [StringLength(1000)]
        public string Notes { get; set; } = string.Empty;

        public SalesOrderStatus Status { get; set; }
        public IList<SelectListItem> Statuses { get; set; }

        public List<SalesOrderItemModel> SalesOrderItems { get; set; }

        public SalesOrderUpdateModel()
        {
            Customers = new List<SelectListItem>();
            BusinessLocations = new List<SelectListItem>();
            Salespersons = new List<SelectListItem>();
            PaymentTerms = new List<SelectListItem>();

            // Only a draft is editable at all, and confirming, delivering and closing
            // are written by their own flows.
            Statuses = Utility.ConvertEnumToSelectList<SalesOrderStatus>()
                .Where(x => x.Value == ((int)SalesOrderStatus.Draft).ToString())
                .ToList();

            SalesOrderItems = new List<SalesOrderItemModel>();
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
