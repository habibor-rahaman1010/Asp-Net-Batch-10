namespace DevSkill.Inventory.Domain.Enums
{
    /// <summary>
    /// Draft -> Sent -> Accepted/Rejected. Converted is written by the sales order
    /// flow alone and never by an edit, because it means an order already exists.
    /// </summary>
    public enum SalesQuotationStatus
    {
        Draft = 0,
        Sent = 1,
        Accepted = 2,
        Rejected = 3,
        Expired = 4,
        Converted = 5,
        Cancelled = 6
    }
}
