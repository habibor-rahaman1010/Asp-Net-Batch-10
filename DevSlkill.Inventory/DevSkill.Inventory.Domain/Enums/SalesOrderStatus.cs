namespace DevSkill.Inventory.Domain.Enums
{
    /// <summary>
    /// A draft order promises the customer nothing. Confirming it is what lets stock
    /// be reserved and goods be delivered against it. The two delivered states are
    /// written by the delivery flow alone and never by an edit.
    /// </summary>
    public enum SalesOrderStatus
    {
        Draft = 0,
        Confirmed = 1,
        PartiallyDelivered = 2,
        Delivered = 3,

        /// <summary>
        /// Deliberately finished short of the ordered quantity. Nothing more may be
        /// delivered against it.
        /// </summary>
        Closed = 4,

        Cancelled = 5
    }
}
