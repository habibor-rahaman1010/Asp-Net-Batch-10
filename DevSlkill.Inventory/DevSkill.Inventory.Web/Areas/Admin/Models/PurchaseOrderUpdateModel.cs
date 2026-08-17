using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Infrastructure.RazorUtility;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    public class PurchaseOrderUpdateModel
    {
        public Guid Id { get; set; }

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

        /// <summary>Shown read only, the source requisition is fixed once the order exists.</summary>
        [Display(Name = "From Requisition")]
        public string PurchaseRequisitionNo { get; set; } = string.Empty;

        [Display(Name = "Other Charges")]
        [Range(0, 999999999999.99, ErrorMessage = "Other charges cannot be negative.")]
        public decimal OtherCharges { get; set; }

        [StringLength(1000)]
        public string Notes { get; set; } = string.Empty;

        public PurchaseOrderStatus Status { get; set; }
        public IList<SelectListItem> Statuses { get; set; }

        public List<PurchaseOrderItemModel> PurchaseOrderItems { get; set; }

        public PurchaseOrderUpdateModel()
        {
            Suppliers = new List<SelectListItem>();
            BusinessLocations = new List<SelectListItem>();
            PaymentTerms = new List<SelectListItem>();

            // Approval, receiving and closing are written by their own flows and
            // never by an edit.
            Statuses = Utility.ConvertEnumToSelectList<PurchaseOrderStatus>()
                .Where(x => x.Value == ((int)PurchaseOrderStatus.Draft).ToString()
                         || x.Value == ((int)PurchaseOrderStatus.Submitted).ToString())
                .ToList();

            PurchaseOrderItems = new List<PurchaseOrderItemModel>();
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
    }
}
