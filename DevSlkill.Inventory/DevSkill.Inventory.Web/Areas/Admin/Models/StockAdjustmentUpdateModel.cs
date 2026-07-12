using DevSkill.Inventory.Domain.Entities.StockAdjustmentEntites;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Infrastructure.RazorUtility;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    public class StockAdjustmentUpdateModel
    {
        public Guid Id { get; set; }
        public DateTime AdjustmentDate { get; set; }
        public string ReferenceNo { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public decimal TotalAmountRecover { get; set; }
        public string Reason { get; set; } = string.Empty;
        public string AddedBy { get; set; } = string.Empty;
        public decimal AdjustmentQuantity { get; set; }
        public decimal UnitPrice { get; set; }
        public Guid BusinessLocationId { get; set; }
        public IList<SelectListItem> BusinessLocations { get; set; }

        public Guid AdjustmentTypeId { get; set; }
        public IList<SelectListItem> AdjustmentTypes { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public StockAdjustmentUpdateModel()
        {
            BusinessLocations = new List<SelectListItem>();
            AdjustmentTypes = new List<SelectListItem>();
            Product = new Product();
        }

        public void SetBusinessLocationValues(IList<BusinessLocation> businessLocations)
        {
            BusinessLocations = Utility.ConvertBusinessLocations(businessLocations);
        }

        public void SetAdjustmentTypeValues(IList<AdjustmentType> adjustmentTypes)
        {
            AdjustmentTypes = Utility.ConvertjustmentTypes(adjustmentTypes);
        }
    }
}
