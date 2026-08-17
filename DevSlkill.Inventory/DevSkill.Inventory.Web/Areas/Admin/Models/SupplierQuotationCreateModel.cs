using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Infrastructure.RazorUtility;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    public class SupplierQuotationCreateModel
    {
        [Display(Name = "Quotation No")]
        public string QuotationNo { get; set; } = string.Empty;

        [StringLength(100)]
        [Display(Name = "Supplier's Reference")]
        public string SupplierQuotationNo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Request for quotation is required.")]
        [Display(Name = "Request For Quotation")]
        public Guid RequestForQuotationId { get; set; }
        public IList<SelectListItem> RequestForQuotations { get; set; }

        [Required(ErrorMessage = "Supplier is required.")]
        [Display(Name = "Supplier")]
        public Guid SupplierId { get; set; }
        public IList<SelectListItem> Suppliers { get; set; }

        [Display(Name = "Quotation Date")]
        public DateTime QuotationDate { get; set; }

        [Display(Name = "Valid Until")]
        public DateTime ValidUntil { get; set; }

        [Range(0, 3650, ErrorMessage = "Delivery days have to be between 0 and 3650.")]
        [Display(Name = "Delivery Days")]
        public int DeliveryDays { get; set; }

        [Display(Name = "Payment Terms")]
        public Guid? PaymentTermId { get; set; }
        public IList<SelectListItem> PaymentTerms { get; set; }

        [Range(0, 999999999999.99, ErrorMessage = "Other charges cannot be negative.")]
        [Display(Name = "Other Charges")]
        public decimal OtherCharges { get; set; }

        [StringLength(1000)]
        public string Notes { get; set; } = string.Empty;

        /// <summary>Filled once a request is chosen, so the header reads on its own.</summary>
        [Display(Name = "Warehouse")]
        public string WarehouseName { get; set; } = string.Empty;

        public List<SupplierQuotationItemModel> SupplierQuotationItems { get; set; }

        public SupplierQuotationCreateModel()
        {
            RequestForQuotations = new List<SelectListItem>();
            Suppliers = new List<SelectListItem>();
            PaymentTerms = new List<SelectListItem>();

            QuotationDate = DateTime.Today;
            ValidUntil = DateTime.Today.AddDays(30);
            DeliveryDays = 7;

            SupplierQuotationItems = new List<SupplierQuotationItemModel>();
        }

        public void SetRequestForQuotationValues(IList<RequestForQuotation> requests)
        {
            RequestForQuotations = Utility.ConvertRequestForQuotations(requests);
        }

        public void SetSupplierValues(IList<Supplier> suppliers)
        {
            Suppliers = Utility.ConvertSuppliers(suppliers);
        }

        public void SetPaymentTermValues(IList<PaymentTerm> paymentTerms)
        {
            PaymentTerms = Utility.ConvertPaymentTerms(paymentTerms);
        }
    }
}
