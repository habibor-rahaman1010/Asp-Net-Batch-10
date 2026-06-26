namespace DevSkill.Inventory.Domain.Entities.StockTransferEntities
{
    public class StockTransferItem : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public Guid StockTransferId { get; set; }
        public Guid ProductId { get; set; }
        public decimal Quantity { get; set; }
        public virtual StockTransfer? StockTransfer { get; set; }
        public virtual Product? Product { get; set; }
    }
}