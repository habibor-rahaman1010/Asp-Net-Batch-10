using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Infrastructure.RazorUtility;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    public class PriceListUpdateModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Price List Name is required."), StringLength(150)]
        [Display(Name = "Price List Name")]
        public string PriceListName { get; set; } = string.Empty;

        [Display(Name = "Customer Type")]
        public CustomerType CustomerType { get; set; }
        public IList<SelectListItem> CustomerTypes { get; set; }

        [Display(Name = "Effective From")]
        public DateTime EffectiveFrom { get; set; }

        [Display(Name = "Effective To")]
        public DateTime? EffectiveTo { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        public List<PriceListItemModel> PriceListItems { get; set; }

        public PriceListUpdateModel()
        {
            CustomerTypes = Utility.ConvertEnumToSelectList<CustomerType>();
            PriceListItems = new List<PriceListItemModel>();
        }
    }
}
