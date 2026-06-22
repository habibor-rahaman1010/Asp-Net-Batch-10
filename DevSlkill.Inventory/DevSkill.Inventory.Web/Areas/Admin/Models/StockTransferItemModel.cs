namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    public class StockTransferItemModel
    {
        public Guid ProductId { get; set; }

        public decimal Quantity { get; set; }
    }
}