namespace DevSkill.Inventory.Domain.Enums
{
    /// <summary>
    /// A draft receipt is only a record of what the warehouse expects; it holds no
    /// stock. Confirming it is what actually brings the goods in, and cancelling a
    /// confirmed receipt takes them straight back out.
    /// </summary>
    public enum GoodsReceiptStatus
    {
        Draft = 0,
        Received = 1,
        Cancelled = 2
    }
}
