namespace DevSkill.Inventory.Domain.Enums
{
    /// <summary>
    /// A draft return brings nothing back. Confirming it is what puts the goods into
    /// stock and credits the customer; cancelling a confirmed return takes them out
    /// again.
    /// </summary>
    public enum SalesReturnStatus
    {
        Draft = 0,
        Returned = 1,
        Cancelled = 2
    }
}
