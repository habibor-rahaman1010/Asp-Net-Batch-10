namespace DevSkill.Inventory.Domain.Dtos
{
    /// <summary>
    /// The state of the goods actually held in house: how many products are safe to
    /// sell from, how many are about to run out, and how much of what is on the
    /// shelf is already promised to someone.
    /// </summary>
    public class StockHealthDto
    {
        /// <summary>Products holding more than their alert quantity.</summary>
        public int HealthyProducts { get; set; }

        /// <summary>Still sellable, but down to the alert quantity or below.</summary>
        public int LowStockProducts { get; set; }

        /// <summary>Nothing on the shelf at all.</summary>
        public int OutOfStockProducts { get; set; }

        public int TotalProducts => HealthyProducts + LowStockProducts + OutOfStockProducts;

        /// <summary>Physical units on the shelf, reserved ones included.</summary>
        public int UnitsOnHand { get; set; }

        /// <summary>Units held against confirmed orders that have not shipped yet.</summary>
        public int UnitsReserved { get; set; }

        /// <summary>What is left to sell once the reservations are honoured.</summary>
        public int UnitsAvailable => UnitsOnHand - UnitsReserved;
    }
}
