namespace DevSkill.Inventory.Domain.Enums
{
    /// <summary>
    /// A draft payment settles nothing. Confirming it is what clears the invoices it
    /// was allocated to; cancelling a confirmed payment puts the due back.
    /// </summary>
    public enum SupplierPaymentStatus
    {
        Draft = 0,
        Paid = 1,
        Cancelled = 2
    }
}
