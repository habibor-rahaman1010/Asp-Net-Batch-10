namespace DevSkill.Inventory.Domain.Enums
{
    /// <summary>
    /// A delivery is only a plan while it is a draft. Confirming it is the moment
    /// goods physically leave the warehouse, so that is the only state that moves stock.
    /// </summary>
    public enum DeliveryStatus
    {
        Draft = 1,
        Confirmed = 2,
        Cancelled = 3
    }
}
