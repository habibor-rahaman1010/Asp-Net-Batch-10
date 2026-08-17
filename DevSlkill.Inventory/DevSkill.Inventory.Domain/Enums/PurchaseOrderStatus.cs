namespace DevSkill.Inventory.Domain.Enums
{
    /// <summary>
    /// Draft -> Submitted -> Approved/Rejected. Receiving moves an approved order
    /// through PartiallyReceived to Received; those two are written by the goods
    /// receipt flow alone and never by an edit.
    /// </summary>
    public enum PurchaseOrderStatus
    {
        Draft = 0,
        Submitted = 1,
        Approved = 2,
        Rejected = 3,
        PartiallyReceived = 4,
        Received = 5,
        Cancelled = 6,

        /// <summary>
        /// Deliberately finished short of the ordered quantity. Nothing more may be
        /// received against it.
        /// </summary>
        Closed = 7
    }
}
