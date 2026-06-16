namespace DevSkill.Inventory.Domain.Entities.StockAdjustmentEntites
{
    public class AdjustmentType : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string AdjustmentTypeName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Sign { get; set; }
    }
}