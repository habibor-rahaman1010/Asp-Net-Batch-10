namespace DevSkill.Inventory.Domain.Enums
{
    /// <summary>
    /// A draft invoice claims nothing from the customer. Posting it is what puts the
    /// amount on the customer ledger, and the paid states are written by the
    /// collection flow alone.
    /// </summary>
    public enum SalesInvoiceStatus
    {
        Draft = 0,
        Posted = 1,
        PartiallyPaid = 2,
        Paid = 3,
        Cancelled = 4
    }
}
