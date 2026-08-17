using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Infrastructure.RazorUtility;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    public class PurchaseInvoiceCreateModel
    {
        [Display(Name = "Invoice No")]
        public string InvoiceNo { get; set; } = string.Empty;

        [StringLength(100)]
        [Display(Name = "Supplier Invoice No")]
        public string SupplierInvoiceNo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Purchase order is required.")]
        [Display(Name = "Purchase Order")]
        public Guid PurchaseOrderId { get; set; }
        public IList<SelectListItem> PurchaseOrders { get; set; }

        [Display(Name = "Against Goods Receipt")]
        public Guid? GoodsReceiptId { get; set; }
        public IList<SelectListItem> GoodsReceipts { get; set; }

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

        public PurchaseInvoiceStatus Status { get; set; }
        public IList<SelectListItem> Statuses { get; set; }

        [Display(Name = "Supplier")]
        public string SupplierName { get; set; } = string.Empty;

        public List<PurchaseInvoiceItemModel> PurchaseInvoiceItems { get; set; }

        public PurchaseInvoiceCreateModel()
        {
            PurchaseOrders = new List<SelectListItem>();
            GoodsReceipts = new List<SelectListItem> { new SelectListItem("-- Whole order --", string.Empty) };
            PaymentTerms = new List<SelectListItem>();

            // An invoice is either parked as a draft or posted straight to the ledger.
            // The paid states belong to the payment flow.
            Statuses = Utility.ConvertEnumToSelectList<PurchaseInvoiceStatus>()
                .Where(x => x.Value == ((int)PurchaseInvoiceStatus.Draft).ToString()
                         || x.Value == ((int)PurchaseInvoiceStatus.Posted).ToString())
                .ToList();

            Status = PurchaseInvoiceStatus.Draft;
            InvoiceDate = DateTime.Today;
            DueDate = DateTime.Today;

            PurchaseInvoiceItems = new List<PurchaseInvoiceItemModel>();
        }

        public void SetPurchaseOrderValues(IList<PurchaseOrder> purchaseOrders)
        {
            PurchaseOrders = Utility.ConvertPurchaseOrders(purchaseOrders);
        }

        public void SetPaymentTermValues(IList<PaymentTerm> paymentTerms)
        {
            PaymentTerms = Utility.ConvertPaymentTerms(paymentTerms);
        }
    }
}
