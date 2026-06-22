using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Infrastructure.RazorUtility;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    public class StockTransferCreateModel
    {
        public Guid FromWarehouseId { get; set; }
        public IList<SelectListItem> FromWarehouses { get; set; }

        public Guid ToWarehouseId { get; set; }
        public IList<SelectListItem> ToWarehouses { get; set; }

        public StockTransferStatus StockTransferStatus { get; set; }
        public IList<SelectListItem> StockTransferStatuses {  get; set; }
        public string? Remarks { get; set; }
        public string? TransferNo { get; set; }
        public DateTime TransferDate { get; set; }
        public List<StockTransferItemModel> StockTransferItems { get; set; }

        public StockTransferCreateModel()
        {
            FromWarehouses = new List<SelectListItem>();
            ToWarehouses = new List<SelectListItem>();
            StockTransferItems = new List<StockTransferItemModel>();
            StockTransferStatuses = Utility.ConvertEnumToSelectList<StockTransferStatus>();
        }

        public void SetFromWarehouseValues(IList<BusinessLocation> businessLocations)
        {
            FromWarehouses = Utility.ConvertBusinessLocations(businessLocations);
        }

        public void SetToWarehouseValues(IList<BusinessLocation> businessLocations)
        {
            ToWarehouses = Utility.ConvertBusinessLocations(businessLocations);
        }
    }
}