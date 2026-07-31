using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Infrastructure.RazorUtility;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    public class StockTransferUpdateModel
    {
        public Guid Id { get; set; }

        public Guid FromWarehouseId { get; set; }
        public IList<SelectListItem> FromWarehouses { get; set; }

        public Guid ToWarehouseId { get; set; }
        public IList<SelectListItem> ToWarehouses { get; set; }

        public string? TransferNo { get; set; }

        public DateTime TransferDate { get; set; }

        public StockTransferStatus StockTransferStatus { get; set; }
        public IList<SelectListItem> StockTransferStatuses { get; set; }

        public string? Remarks { get; set; }

        public List<StockTransferItemModel> StockTransferItems { get; set; }

        public StockTransferUpdateModel()
        {
            FromWarehouses = new List<SelectListItem>();
            ToWarehouses = new List<SelectListItem>();
            StockTransferStatuses = Utility.ConvertEnumToSelectList<StockTransferStatus>();
            StockTransferItems = new List<StockTransferItemModel>
            {
                new StockTransferItemModel()
            };
        }

        public void SetFromWarehouseValues(IList<BusinessLocation> warehouses)
        {
            FromWarehouses = Utility.ConvertBusinessLocations(warehouses);
        }

        public void SetToWarehouseValues(IList<BusinessLocation> warehouses)
        {
            ToWarehouses = Utility.ConvertBusinessLocations(warehouses);
        }
    }
}