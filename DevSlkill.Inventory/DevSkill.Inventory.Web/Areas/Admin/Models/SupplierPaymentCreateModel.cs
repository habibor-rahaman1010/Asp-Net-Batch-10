using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Infrastructure.RazorUtility;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    public class SupplierPaymentCreateModel
    {
        [Display(Name = "Payment No")]
        public string PaymentNo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Supplier is required.")]
        [Display(Name = "Supplier")]
        public Guid SupplierId { get; set; }
        public IList<SelectListItem> Suppliers { get; set; }

        [Display(Name = "Payment Date")]
        public DateTime PaymentDate { get; set; }

        [Display(Name = "Payment Method")]
        public PaymentMethod PaymentMethod { get; set; }
        public IList<SelectListItem> PaymentMethods { get; set; }

        [StringLength(100)]
        [Display(Name = "Reference No")]
        public string ReferenceNo { get; set; } = string.Empty;

        [Range(0.01, 9999999999, ErrorMessage = "The payment amount has to be greater than zero.")]
        [Display(Name = "Payment Amount")]
        public decimal Amount { get; set; }

        [StringLength(1000)]
        public string Notes { get; set; } = string.Empty;

        public SupplierPaymentStatus Status { get; set; }
        public IList<SelectListItem> Statuses { get; set; }

        /// <summary>Filled once a supplier is chosen, so the payable side can be read here.</summary>
        [Display(Name = "Current Outstanding")]
        public decimal CurrentOutstanding { get; set; }

        public List<SupplierPaymentAllocationModel> SupplierPaymentAllocations { get; set; }

        public SupplierPaymentCreateModel()
        {
            Suppliers = new List<SelectListItem>();
            PaymentMethods = Utility.ConvertEnumToSelectList<PaymentMethod>();

            // A payment is either parked as a draft or paid straight away. Cancelled
            // belongs to the cancellation flow.
            Statuses = Utility.ConvertEnumToSelectList<SupplierPaymentStatus>()
                .Where(x => x.Value != ((int)SupplierPaymentStatus.Cancelled).ToString())
                .ToList();

            Status = SupplierPaymentStatus.Draft;
            PaymentDate = DateTime.Today;

            SupplierPaymentAllocations = new List<SupplierPaymentAllocationModel>();
        }

        public void SetSupplierValues(IList<Supplier> suppliers)
        {
            Suppliers = Utility.ConvertSuppliers(suppliers);
        }
    }
}
