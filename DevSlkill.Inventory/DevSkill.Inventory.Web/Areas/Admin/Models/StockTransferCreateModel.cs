namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    public class StockTransferCreateModel
    {
        public Guid FromWarehouseId { get; set; }

        public Guid ToWarehouseId { get; set; }

        public string? Remarks { get; set; }

        public List<StockTransferItemModel> Items { get; set; }
    }
}