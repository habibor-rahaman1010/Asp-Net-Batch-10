namespace DevSkill.Inventory.Domain.Entities.StockAdjustmentEntites
{
    public class StockAdjustment : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public DateTime AdjustmentDate { get; set; }

        public string ReferenceNo { get; set; } = string.Empty;

        public decimal TotalAmount { get; set; }

        public decimal TotalAmountRecover { get; set; }

        public string Reason { get; set; } = string.Empty;

        public string AddedBy { get; set; } = string.Empty;

        public Guid BusinessLocationId { get; set; }
        public BusinessLocation? BusinessLocation { get; set; }

        public Guid AdjustmentTypeId { get; set; }
        public AdjustmentType? AdjustmentType { get; set; }

        public virtual ICollection<StockAdjustmentItem>? StockAdjustmentItems { get; set; }
    }
}