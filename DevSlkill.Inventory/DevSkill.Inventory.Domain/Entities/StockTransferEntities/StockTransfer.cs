using DevSkill.Inventory.Domain.Enums;

namespace DevSkill.Inventory.Domain.Entities.StockTransferEntities
{
    public class StockTransfer : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string? TransferNo { get; set; }
        public DateTime TransferDate { get; set; }
        public Guid FromWarehouseId { get; set; }
        public Guid ToWarehouseId { get; set; }
        public StockTransferStatus Status { get; set; }
        public string? Remarks { get; set; }
        public virtual BusinessLocation? FromWarehouse { get; set; }
        public virtual BusinessLocation? ToWarehouse { get; set; }
        public virtual ICollection<StockTransferItem>? StockTransferItems { get; set; }
    }
}