namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    /// <summary>
    /// One sales order line seen from the reservation side. Everything but the
    /// quantity being held is read only, because it all comes off the order and the
    /// product; the server works the same figures out again when the hold is saved.
    /// </summary>
    public class StockReservationItemModel
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string UnitName { get; set; } = string.Empty;

        public decimal OrderedQuantity { get; set; }

        /// <summary>Already held for this order by other reservations.</summary>
        public decimal AlreadyReservedQuantity { get; set; }

        /// <summary>Pencilled in by draft reservations, spoken for but not yet held.</summary>
        public decimal PendingQuantity { get; set; }

        public decimal DeliveredQuantity { get; set; }

        /// <summary>The most this reservation may hold on the line.</summary>
        public decimal RemainingQuantity { get; set; }

        public int CurrentStock { get; set; }

        /// <summary>Physical stock that is not held for anything else.</summary>
        public int AvailableStock { get; set; }

        /// <summary>What is being held now. This is the only field the user types.</summary>
        public decimal ReservedQuantity { get; set; }

        public string Remarks { get; set; } = string.Empty;
    }
}
