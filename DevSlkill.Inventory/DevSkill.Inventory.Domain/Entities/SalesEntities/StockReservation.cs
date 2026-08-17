using DevSkill.Inventory.Domain.Enums;

namespace DevSkill.Inventory.Domain.Entities.SalesEntities
{
    /// <summary>
    /// Stock held back for one confirmed sales order. Physical stock never moves;
    /// only <see cref="Product.ReservedStock"/> does, which is what keeps the goods
    /// from being promised to a second customer.
    /// </summary>
    public class StockReservation : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string ReservationNo { get; set; } = string.Empty;

        public Guid SalesOrderId { get; set; }
        public virtual SalesOrder? SalesOrder { get; set; }

        /// <summary>Copied from the sales order so the list reads without loading it.</summary>
        public Guid CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }

        /// <summary>
        /// Warehouse the stock is held in. It follows the sales order, so nothing can
        /// be held in a location the sale was not raised from.
        /// </summary>
        public Guid BusinessLocationId { get; set; }
        public virtual BusinessLocation? BusinessLocation { get; set; }

        public DateTime ReservationDate { get; set; }

        /// <summary>
        /// After this date the hold is no longer meant to stand. Expiring it is a
        /// deliberate release, never something that happens quietly.
        /// </summary>
        public DateTime ExpiryDate { get; set; }

        public StockReservationStatus Status { get; set; }

        public string Notes { get; set; } = string.Empty;

        public string CreatedBy { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public virtual ICollection<StockReservationItem>? StockReservationItems { get; set; }
    }
}
