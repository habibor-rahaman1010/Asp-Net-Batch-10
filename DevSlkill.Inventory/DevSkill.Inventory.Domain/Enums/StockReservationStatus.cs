namespace DevSkill.Inventory.Domain.Enums
{
    /// <summary>
    /// A draft reservation holds nothing. Confirming it is what puts the quantity
    /// into <see cref="Entities.Product.ReservedStock"/>; releasing or cancelling it
    /// gives that quantity back to whoever asks for it next.
    /// </summary>
    public enum StockReservationStatus
    {
        Draft = 0,
        Reserved = 1,

        /// <summary>Given back on purpose, because the sale is no longer expected.</summary>
        Released = 2,

        /// <summary>The goods have left on a delivery, so the hold is over.</summary>
        Consumed = 3,

        Cancelled = 4
    }
}
