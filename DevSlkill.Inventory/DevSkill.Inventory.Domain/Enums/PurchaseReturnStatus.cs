namespace DevSkill.Inventory.Domain.Enums
{
    /// <summary>
    /// A draft return holds no stock. Confirming it is what sends the goods back and
    /// credits the supplier; cancelling a confirmed return brings them back in.
    /// </summary>
    public enum PurchaseReturnStatus
    {
        Draft = 0,
        Returned = 1,
        Cancelled = 2
    }
}
