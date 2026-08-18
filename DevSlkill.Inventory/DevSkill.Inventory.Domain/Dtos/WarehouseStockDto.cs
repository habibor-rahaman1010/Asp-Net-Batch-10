namespace DevSkill.Inventory.Domain.Dtos
{
    /// <summary>
    /// One warehouse on the spread chart: how much of the catalogue is kept there.
    /// A warehouse holding nothing is still carried, because an empty shelf is
    /// something worth seeing rather than a row that quietly disappears.
    /// </summary>
    public class WarehouseStockPointDto
    {
        public string WarehouseName { get; set; } = string.Empty;

        /// <summary>Distinct products stored at this warehouse.</summary>
        public int ProductCount { get; set; }

        /// <summary>Physical units those products add up to, reserved ones included.</summary>
        public int UnitsOnHand { get; set; }
    }

    /// <summary>
    /// Where the goods sit. The counts are of products, not of units, so a
    /// warehouse keeping one item of a thousand lines still reads as a busy one.
    /// </summary>
    public class WarehouseStockDto
    {
        /// <summary>Every warehouse, busiest first.</summary>
        public IList<WarehouseStockPointDto> Warehouses { get; set; } = new List<WarehouseStockPointDto>();

        /// <summary>
        /// Products that were never given a warehouse. They are kept out of the bars,
        /// which are about places, but counted here so the totals still add up.
        /// </summary>
        public int UnassignedProducts { get; set; }

        public int TotalWarehouses => Warehouses.Count;

        /// <summary>Warehouses that actually hold something.</summary>
        public int WarehousesInUse => Warehouses.Count(x => x.ProductCount > 0);

        /// <summary>Products that have been placed somewhere.</summary>
        public int PlacedProducts => Warehouses.Sum(x => x.ProductCount);
    }
}
