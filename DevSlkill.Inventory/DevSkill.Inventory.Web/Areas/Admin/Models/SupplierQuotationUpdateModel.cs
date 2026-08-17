using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Infrastructure.RazorUtility;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    public class SupplierQuotationUpdateModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Quotation No")]
        public string QuotationNo { get; set; } = string.Empty;

        [StringLength(100)]
        [Display(Name = "Supplier's Reference")]
        public string SupplierQuotationNo { get; set; } = string.Empty;

        public Guid RequestForQuotationId { get; set; }

        /// <summary>Shown read only; a quotation never moves to another request.</summary>
        [Display(Name = "Request For Quotation")]
        public string RfqNo { get; set; } = string.Empty;

        public Guid SupplierId { get; set; }

        /// <summary>Shown read only; a quotation never moves to another supplier.</summary>
        [Display(Name = "Supplier")]
        public string SupplierName { get; set; } = string.Empty;

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

        [Display(Name = "Warehouse")]
        public string WarehouseName { get; set; } = string.Empty;

        public List<SupplierQuotationItemModel> SupplierQuotationItems { get; set; }

        public SupplierQuotationUpdateModel()
        {
            PaymentTerms = new List<SelectListItem>();
            SupplierQuotationItems = new List<SupplierQuotationItemModel>();
        }

        public void SetPaymentTermValues(IList<PaymentTerm> paymentTerms)
        {
            PaymentTerms = Utility.ConvertPaymentTerms(paymentTerms);
        }
    }
}
