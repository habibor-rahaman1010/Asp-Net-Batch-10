using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Infrastructure.RazorUtility;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    public class DiscountRuleCreateModel
    {
        [Required(ErrorMessage = "Rule Name is required."), StringLength(150)]
        [Display(Name = "Rule Name")]
        public string RuleName { get; set; } = string.Empty;

        public DiscountScope Scope { get; set; }
        public IList<SelectListItem> Scopes { get; set; }

        [Display(Name = "Discount Type")]
        public DiscountType DiscountType { get; set; }
        public IList<SelectListItem> DiscountTypes { get; set; }

        [Display(Name = "Discount Value")]
        [Range(0, 999999999999.99, ErrorMessage = "Discount Value cannot be negative.")]
        public decimal DiscountValue { get; set; }

        public Guid? ProductId { get; set; }
        public IList<SelectListItem> Products { get; set; }

        public Guid? CustomerId { get; set; }
        public IList<SelectListItem> Customers { get; set; }

        [Display(Name = "Customer Type")]
        public CustomerType? CustomerType { get; set; }
        public IList<SelectListItem> CustomerTypes { get; set; }

        [Display(Name = "Minimum Quantity")]
        [Range(0, 999999999999.99, ErrorMessage = "Minimum Quantity cannot be negative.")]
        public decimal MinQuantity { get; set; }

        [Display(Name = "Minimum Order Amount")]
        [Range(0, 999999999999.99, ErrorMessage = "Minimum Order Amount cannot be negative.")]
        public decimal MinOrderAmount { get; set; }

        [Display(Name = "Maximum Discount Amount")]
        [Range(0, 999999999999.99, ErrorMessage = "Maximum Discount Amount cannot be negative.")]
        public decimal MaxDiscountAmount { get; set; }

        [Display(Name = "Effective From")]
        public DateTime EffectiveFrom { get; set; }

        [Display(Name = "Effective To")]
        public DateTime? EffectiveTo { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        public int Priority { get; set; }

        public DiscountRuleCreateModel()
        {
            Scopes = Utility.ConvertEnumToSelectList<DiscountScope>();
            DiscountTypes = Utility.ConvertEnumToSelectList<DiscountType>();
            CustomerTypes = Utility.ConvertEnumToSelectList<CustomerType>();
            Products = new List<SelectListItem>();
            Customers = new List<SelectListItem>();
            EffectiveFrom = DateTime.Today;
            IsActive = true;
            Priority = 1;
        }

        public void SetProductValues(IList<Product> products)
        {
            Products = Utility.ConvertSelectItemToProduct(products);
        }

        public void SetCustomerValues(IList<Customer> customers)
        {
            Customers = Utility.ConvertCustomers(customers);
        }
    }
}
