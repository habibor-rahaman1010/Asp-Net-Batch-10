using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Infrastructure.RazorUtility;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    public class SalesOrderCreateModel
    {
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

        /// <summary>
        /// Set when the order is being raised on an offer the customer accepted, which
        /// is what keeps the price on the order tied to what was quoted.
        /// </summary>
        [Display(Name = "From Quotation")]
        public Guid? SalesQuotationId { get; set; }
        public IList<SelectListItem> SalesQuotations { get; set; }

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

        public SalesOrderCreateModel()
        {
            Customers = new List<SelectListItem>();
            BusinessLocations = new List<SelectListItem>();
            Salespersons = new List<SelectListItem>();
            PaymentTerms = new List<SelectListItem>();
            SalesQuotations = new List<SelectListItem>();

            // A new order is only ever a draft. Confirming it checks the credit limit,
            // so it is an action of its own rather than a value typed into a dropdown.
            Statuses = Utility.ConvertEnumToSelectList<SalesOrderStatus>()
                .Where(x => x.Value == ((int)SalesOrderStatus.Draft).ToString())
                .ToList();

            Status = SalesOrderStatus.Draft;
            OrderDate = DateTime.Today;
            ExpectedDeliveryDate = DateTime.Today.AddDays(7);

            SalesOrderItems = new List<SalesOrderItemModel>
            {
                new SalesOrderItemModel()
            };
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

        public void SetSalesQuotationValues(IList<SalesQuotation> salesQuotations)
        {
            SalesQuotations = Utility.ConvertSalesQuotations(salesQuotations);
        }
    }
}
