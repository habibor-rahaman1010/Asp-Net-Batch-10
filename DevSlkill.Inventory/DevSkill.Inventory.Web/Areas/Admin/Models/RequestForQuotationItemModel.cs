using System.ComponentModel.DataAnnotations;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    /// <summary>
    /// One product we want quoted. There is no price here on purpose: the price is
    /// exactly what the supplier is being asked to state.
    /// </summary>
    public class RequestForQuotationItemModel
    {
        [Display(Name = "Product")]
        public Guid ProductId { get; set; }

        public string ProductName { get; set; } = string.Empty;

        [Display(Name = "Unit")]
        public string UnitName { get; set; } = string.Empty;

        [Display(Name = "Quantity")]
        public decimal Quantity { get; set; }

        [StringLength(500)]
        public string Remarks { get; set; } = string.Empty;
    }
}
