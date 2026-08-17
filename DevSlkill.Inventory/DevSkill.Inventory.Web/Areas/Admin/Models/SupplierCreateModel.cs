using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Infrastructure.RazorUtility;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    public class SupplierCreateModel
    {
        [Required(ErrorMessage = "Supplier Name is required."), StringLength(200)]
        [Display(Name = "Supplier Name")]
        public string SupplierName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Supplier Code is required."), StringLength(50)]
        [Display(Name = "Supplier Code")]
        public string SupplierCode { get; set; } = string.Empty;

        [StringLength(200)]
        [Display(Name = "Contact Person")]
        public string ContactPerson { get; set; } = string.Empty;

        [StringLength(30), Phone(ErrorMessage = "Please enter a valid phone number.")]
        public string Phone { get; set; } = string.Empty;

        [StringLength(150), EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; } = string.Empty;

        [StringLength(500)]
        public string Address { get; set; } = string.Empty;

        [StringLength(50)]
        [Display(Name = "Tax / BIN Number")]
        public string TaxNumber { get; set; } = string.Empty;

        [Display(Name = "Payment Terms")]
        public Guid? PaymentTermId { get; set; }
        public IList<SelectListItem> PaymentTerms { get; set; }

        [Display(Name = "Credit Limit")]
        [Range(0, 999999999999.99, ErrorMessage = "Credit Limit cannot be negative.")]
        public decimal CreditLimit { get; set; }

        [Display(Name = "Opening Balance")]
        public decimal OpeningBalance { get; set; }

        public SupplierStatus Status { get; set; }
        public IList<SelectListItem> Statuses { get; set; }

        public SupplierCreateModel()
        {
            PaymentTerms = new List<SelectListItem>();
            Statuses = Utility.ConvertEnumToSelectList<SupplierStatus>();
            Status = SupplierStatus.Active;
        }

        public void SetPaymentTermValues(IList<PaymentTerm> paymentTerms)
        {
            PaymentTerms = Utility.ConvertPaymentTerms(paymentTerms);
        }
    }
}
