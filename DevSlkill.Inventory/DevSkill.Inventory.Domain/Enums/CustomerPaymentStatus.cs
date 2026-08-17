namespace DevSkill.Inventory.Domain.Enums
{
    /// <summary>
    /// A draft collection settles nothing. Confirming it is what clears the invoices
    /// it was allocated to; cancelling a received collection puts the due back.
    /// </summary>
    public enum CustomerPaymentStatus
    {
        Draft = 0,
        Received = 1,
        Cancelled = 2
    }
}
