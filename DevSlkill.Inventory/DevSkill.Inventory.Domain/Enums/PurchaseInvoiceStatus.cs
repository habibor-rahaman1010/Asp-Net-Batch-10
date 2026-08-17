namespace DevSkill.Inventory.Domain.Enums
{
    /// <summary>
    /// A draft invoice is only a working copy; it owes the supplier nothing. Posting
    /// it is what puts the amount on the supplier ledger, and the paid states are
    /// written by the payment flow alone.
    /// </summary>
    public enum PurchaseInvoiceStatus
    {
        Draft = 0,
        Posted = 1,
        PartiallyPaid = 2,
        Paid = 3,
        Cancelled = 4
    }
}
