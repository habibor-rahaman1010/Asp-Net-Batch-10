using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Infrastructure.RazorUtility;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    public class CustomerCreateModel
    {
        [Required(ErrorMessage = "Customer Name is required."), StringLength(200)]
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Customer Code is required."), StringLength(50)]
        [Display(Name = "Customer Code")]
        public string CustomerCode { get; set; } = string.Empty;

        [StringLength(30), Phone(ErrorMessage = "Please enter a valid phone number.")]
        public string Phone { get; set; } = string.Empty;

        [StringLength(150), EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; } = string.Empty;

        [StringLength(500)]
        public string Address { get; set; } = string.Empty;

        [Display(Name = "Customer Type")]
        public CustomerType CustomerType { get; set; }
        public IList<SelectListItem> CustomerTypes { get; set; }

        [Display(Name = "Credit Limit")]
        [Range(0, 999999999999.99, ErrorMessage = "Credit Limit cannot be negative.")]
        public decimal CreditLimit { get; set; }

        [Display(Name = "Opening Balance")]
        public decimal OpeningBalance { get; set; }

        public CustomerStatus Status { get; set; }
        public IList<SelectListItem> Statuses { get; set; }

        public CustomerCreateModel()
        {
            CustomerTypes = Utility.ConvertEnumToSelectList<CustomerType>();
            Statuses = Utility.ConvertEnumToSelectList<CustomerStatus>();
        }
    }
}
