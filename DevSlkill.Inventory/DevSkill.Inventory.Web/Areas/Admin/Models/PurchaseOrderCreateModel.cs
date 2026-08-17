using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Infrastructure.RazorUtility;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    public class PurchaseOrderCreateModel
    {
        [Display(Name = "Purchase Order No")]
        public string PurchaseOrderNo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Supplier is required.")]
        public Guid SupplierId { get; set; }
        public IList<SelectListItem> Suppliers { get; set; }

        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }

        [Display(Name = "Expected Delivery")]
        public DateTime ExpectedDeliveryDate { get; set; }

        [Required(ErrorMessage = "Warehouse is required.")]
        [Display(Name = "Warehouse")]
        public Guid BusinessLocationId { get; set; }
        public IList<SelectListItem> BusinessLocations { get; set; }

        [Display(Name = "Payment Terms")]
        public Guid? PaymentTermId { get; set; }
        public IList<SelectListItem> PaymentTerms { get; set; }

        [Display(Name = "From Requisition")]
        public Guid? PurchaseRequisitionId { get; set; }
        public IList<SelectListItem> PurchaseRequisitions { get; set; }

        /// <summary>
        /// Set when the order is being raised on a quotation that won its request,
        /// which is what keeps the price on the order tied to the quote behind it.
        /// </summary>
        [Display(Name = "From Quotation")]
        public Guid? SupplierQuotationId { get; set; }
        public IList<SelectListItem> SupplierQuotations { get; set; }

        [Display(Name = "Other Charges")]
        [Range(0, 999999999999.99, ErrorMessage = "Other charges cannot be negative.")]
        public decimal OtherCharges { get; set; }

        [StringLength(1000)]
        public string Notes { get; set; } = string.Empty;

        public PurchaseOrderStatus Status { get; set; }
        public IList<SelectListItem> Statuses { get; set; }

        public List<PurchaseOrderItemModel> PurchaseOrderItems { get; set; }

        public PurchaseOrderCreateModel()
        {
            Suppliers = new List<SelectListItem>();
            BusinessLocations = new List<SelectListItem>();
            PaymentTerms = new List<SelectListItem>();
            PurchaseRequisitions = new List<SelectListItem>();
            SupplierQuotations = new List<SelectListItem>();

            // A new order is either kept as a draft or sent straight for approval.
            Statuses = Utility.ConvertEnumToSelectList<PurchaseOrderStatus>()
                .Where(x => x.Value == ((int)PurchaseOrderStatus.Draft).ToString()
                         || x.Value == ((int)PurchaseOrderStatus.Submitted).ToString())
                .ToList();

            Status = PurchaseOrderStatus.Draft;
            OrderDate = DateTime.Today;
            ExpectedDeliveryDate = DateTime.Today.AddDays(7);

            PurchaseOrderItems = new List<PurchaseOrderItemModel>
            {
                new PurchaseOrderItemModel()
            };
        }

        public void SetSupplierValues(IList<Supplier> suppliers)
        {
            Suppliers = Utility.ConvertSuppliers(suppliers);
        }

        public void SetBusinessLocationValues(IList<BusinessLocation> businessLocations)
        {
            BusinessLocations = Utility.ConvertBusinessLocations(businessLocations);
        }

        public void SetPaymentTermValues(IList<PaymentTerm> paymentTerms)
        {
            PaymentTerms = Utility.ConvertPaymentTerms(paymentTerms);
        }

        public void SetPurchaseRequisitionValues(IList<PurchaseRequisition> purchaseRequisitions)
        {
            PurchaseRequisitions = Utility.ConvertPurchaseRequisitions(purchaseRequisitions);
        }

        public void SetSupplierQuotationValues(IList<SupplierQuotation> supplierQuotations)
        {
            SupplierQuotations = Utility.ConvertSupplierQuotations(supplierQuotations);
        }
    }
}
