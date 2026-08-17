namespace DevSkill.Inventory.Domain.Enums
{
    /// <summary>
    /// Every quotation starts as simply received. Awarding the RFQ marks one of them
    /// selected and the rest rejected, and only a selected one can become an order.
    /// </summary>
    public enum SupplierQuotationStatus
    {
        Received = 0,
        Selected = 1,
        Rejected = 2,
        Cancelled = 3
    }
}
