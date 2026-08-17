using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Infrastructure.RazorUtility;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    public class SalesInvoiceCreateModel
    {
        [Display(Name = "Invoice No")]
        public string InvoiceNo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Sales order is required.")]
        [Display(Name = "Sales Order")]
        public Guid SalesOrderId { get; set; }
        public IList<SelectListItem> SalesOrders { get; set; }

        /// <summary>
        /// Set when the invoice covers one specific shipment. Left empty it bills
        /// everything delivered on the order so far.
        /// </summary>
        [Display(Name = "Delivery")]
        public Guid? DeliveryId { get; set; }
        public IList<SelectListItem> Deliveries { get; set; }

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

        /// <summary>
        /// Ticking this claims the money straight away: the customer's balance goes up
        /// and the salesperson earns their commission. Left unticked it is a draft.
        /// </summary>
        [Display(Name = "Post the invoice now")]
        public bool PostImmediately { get; set; } = true;

        public List<SalesInvoiceItemModel> SalesInvoiceItems { get; set; }

        public SalesInvoiceCreateModel()
        {
            SalesOrders = new List<SelectListItem>();
            Deliveries = new List<SelectListItem>();
            PaymentTerms = new List<SelectListItem>();
            SalesInvoiceItems = new List<SalesInvoiceItemModel>();
            InvoiceDate = DateTime.Today;
            DueDate = DateTime.Today;
        }

        public void SetSalesOrderValues(IList<SalesOrder> salesOrders)
        {
            SalesOrders = Utility.ConvertSalesOrders(salesOrders);
        }

        public void SetDeliveryValues(IList<Delivery> deliveries)
        {
            Deliveries = Utility.ConvertDeliveries(deliveries);
        }

        public void SetPaymentTermValues(IList<PaymentTerm> paymentTerms)
        {
            PaymentTerms = Utility.ConvertPaymentTerms(paymentTerms);
        }
    }
}
