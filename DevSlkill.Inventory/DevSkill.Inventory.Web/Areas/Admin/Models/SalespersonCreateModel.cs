using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Infrastructure.RazorUtility;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    public class SalespersonCreateModel
    {
        [Display(Name = "Salesperson Code")]
        public string SalespersonCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(150)]
        [Display(Name = "Salesperson Name")]
        public string SalespersonName { get; set; } = string.Empty;

        [StringLength(30)]
        public string Phone { get; set; } = string.Empty;

        [StringLength(150)]
        [EmailAddress(ErrorMessage = "That does not look like an email address.")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Commission Basis")]
        public CommissionBasis CommissionBasis { get; set; }
        public IList<SelectListItem> CommissionBases { get; set; }

        /// <summary>
        /// A percentage when the basis is percentage, a flat amount per invoice when
        /// it is not. The screen says which, because the number means two things.
        /// </summary>
        [Display(Name = "Commission Rate")]
        [Range(0, 999999999999.99, ErrorMessage = "The commission rate cannot be negative.")]
        public decimal CommissionRate { get; set; }

        [Display(Name = "Monthly Target")]
        [Range(0, 999999999999.99, ErrorMessage = "The monthly target cannot be negative.")]
        public decimal MonthlyTarget { get; set; }

        public SalespersonStatus Status { get; set; }
        public IList<SelectListItem> Statuses { get; set; }

        public SalespersonCreateModel()
        {
            CommissionBases = Utility.ConvertEnumToSelectList<CommissionBasis>();
            Statuses = Utility.ConvertEnumToSelectList<SalespersonStatus>();

            CommissionBasis = CommissionBasis.Percentage;
            Status = SalespersonStatus.Active;
        }
    }
}
