namespace DevSkill.Inventory.Domain.Enums
{
    /// <summary>
    /// Commission is earned when the invoice is posted, but it is only owed once it
    /// has been approved, and only settled once it has been paid.
    /// </summary>
    public enum CommissionStatus
    {
        Pending = 0,
        Approved = 1,
        Paid = 2,
        Cancelled = 3
    }
}
