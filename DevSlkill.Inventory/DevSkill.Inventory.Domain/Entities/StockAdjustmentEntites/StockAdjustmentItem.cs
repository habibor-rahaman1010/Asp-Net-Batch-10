namespace DevSkill.Inventory.Domain.Entities.StockAdjustmentEntites
{
    public class StockAdjustmentItem
    {
        public Guid Id { get; set; }

        public Guid StockAdjustmentId { get; set; }
        public StockAdjustment? StockAdjustment { get; set; }

        public Guid ProductId { get; set; }
        public Product? Product { get; set; }

        public int AdjustmentQuantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal TotalAmount { get; set; }
    }
}