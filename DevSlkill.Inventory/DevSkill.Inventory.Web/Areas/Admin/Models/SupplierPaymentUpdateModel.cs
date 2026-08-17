using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Infrastructure.RazorUtility;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    public class SupplierPaymentUpdateModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Payment No")]
        public string PaymentNo { get; set; } = string.Empty;

        public Guid SupplierId { get; set; }

        /// <summary>Shown read only; a payment never moves to another supplier.</summary>
        [Display(Name = "Supplier")]
        public string SupplierName { get; set; } = string.Empty;

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

        [Display(Name = "Current Outstanding")]
        public decimal CurrentOutstanding { get; set; }

        public List<SupplierPaymentAllocationModel> SupplierPaymentAllocations { get; set; }

        public SupplierPaymentUpdateModel()
        {
            PaymentMethods = Utility.ConvertEnumToSelectList<PaymentMethod>();

            Statuses = Utility.ConvertEnumToSelectList<SupplierPaymentStatus>()
                .Where(x => x.Value != ((int)SupplierPaymentStatus.Cancelled).ToString())
                .ToList();

            SupplierPaymentAllocations = new List<SupplierPaymentAllocationModel>();
        }
    }
}
