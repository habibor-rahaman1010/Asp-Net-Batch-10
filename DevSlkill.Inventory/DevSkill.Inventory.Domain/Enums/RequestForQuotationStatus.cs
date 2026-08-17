namespace DevSkill.Inventory.Domain.Enums
{
    /// <summary>
    /// A draft RFQ has not gone out yet. Sending it is what opens it for supplier
    /// quotations, and awarding it is what picks the winner and closes the bidding.
    /// </summary>
    public enum RequestForQuotationStatus
    {
        Draft = 0,
        Sent = 1,
        Awarded = 2,
        Closed = 3,
        Cancelled = 4
    }
}
