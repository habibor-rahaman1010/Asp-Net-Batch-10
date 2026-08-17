using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Infrastructure.RazorUtility;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    /// <summary>
    /// Only a draft invoice is editable. A posted one is a claim already made on the
    /// customer, so it is corrected with a credit note rather than a quiet edit.
    /// </summary>
    public class SalesInvoiceUpdateModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Invoice No")]
        public string InvoiceNo { get; set; } = string.Empty;

        public Guid SalesOrderId { get; set; }

        [Display(Name = "Sales Order")]
        public string SalesOrderNo { get; set; } = string.Empty;

        public Guid? DeliveryId { get; set; }

        [Display(Name = "Delivery")]
        public string DeliveryNo { get; set; } = string.Empty;

        [Display(Name = "Customer")]
        public string CustomerName { get; set; } = string.Empty;

        [Display(Name = "Warehouse")]
        public string WarehouseName { get; set; } = string.Empty;

        [Display(Name = "Invoice Date")]
        public DateTime InvoiceDate { get; set; }

        [Display(Name = "Due Date")]
        public DateTime DueDate { get; set; }

        [Display(Name = "Payment Terms")]
        public Guid? PaymentTermId { get; set; }
        public IList<SelectListItem> PaymentTerms { get; set; }

        [Display(Name = "Other Charges")]
        [Range(0, 999999999999.99, ErrorMessage = "Other charges cannot be negative.")]
        public decimal OtherCharges { get; set; }

        [StringLength(1000)]
        public string Notes { get; set; } = string.Empty;

        public List<SalesInvoiceItemModel> SalesInvoiceItems { get; set; }

        public SalesInvoiceUpdateModel()
        {
            PaymentTerms = new List<SelectListItem>();
            SalesInvoiceItems = new List<SalesInvoiceItemModel>();
            InvoiceDate = DateTime.Today;
            DueDate = DateTime.Today;
        }

        public void SetPaymentTermValues(IList<PaymentTerm> paymentTerms)
        {
            PaymentTerms = Utility.ConvertPaymentTerms(paymentTerms);
        }
    }
}
