namespace DevSkill.Inventory.Domain.Dtos
{
    /// <summary>
    /// What one product sold for in one month, net of anything that has since gone
    /// back on a sales return. The product is carried through rather than added up
    /// away, because what it cost has to be looked up product by product.
    /// </summary>
    public class MonthlyProductSalesDto
    {
        public int Year { get; set; }

        public int Month { get; set; }

        public Guid ProductId { get; set; }

        /// <summary>Units billed less units returned.</summary>
        public decimal Quantity { get; set; }

        /// <summary>Billed value less the value of what came back.</summary>
        public decimal Revenue { get; set; }

        /// <summary>
        /// The product's own price, used to cost a product nothing has ever been bought
        /// against. It is a standing figure rather than a purchase, so a month resting
        /// on it is worth saying out loud.
        /// </summary>
        public double ListUnitCost { get; set; }
    }

    /// <summary>
    /// What one product has been bought for, added up over every purchase invoice that
    /// counts. The average of the two is the weighted average cost, which is the usual
    /// way of costing goods that were bought at different prices.
    /// </summary>
    public class ProductAverageCostDto
    {
        public Guid ProductId { get; set; }

        public decimal PurchasedAmount { get; set; }

        public decimal PurchasedQuantity { get; set; }

        /// <summary>Zero when nothing was ever bought, which the caller has to check.</summary>
        public decimal AverageUnitCost => PurchasedQuantity > 0
            ? PurchasedAmount / PurchasedQuantity
            : 0;
    }

    /// <summary>
    /// One month of the profit and loss: what came in, what the goods behind it cost,
    /// and what was left.
    /// </summary>
    public class ProfitCostPointDto
    {
        public string Label { get; set; } = string.Empty;

        /// <summary>Net of sales returns.</summary>
        public decimal Revenue { get; set; }

        /// <summary>The cost of the goods that revenue was earned on, nothing else.</summary>
        public decimal Cost { get; set; }

        public decimal GrossProfit => Revenue - Cost;

        /// <summary>
        /// Gross profit as a share of revenue. Held as its own figure rather than worked
        /// out in the browser, so the line and the tooltip can never disagree.
        /// </summary>
        public decimal GrossMargin { get; set; }
    }

    /// <summary>
    /// Revenue against the cost of what was sold, month by month over one calendar
    /// year, with the margin those two leave.
    /// </summary>
    /// <remarks>
    /// Only the goods are costed. There is no wages, rent or freight in the system, so
    /// this is a gross profit and never a bottom line, and the chart says so rather
    /// than letting it be read as one.
    /// </remarks>
    public class ProfitCostDto
    {
        public int Year { get; set; }

        /// <summary>
        /// Every month of the year in order, a quiet one included as a zero. A running
        /// year stops at the month we are in rather than trailing into months that have
        /// not happened.
        /// </summary>
        public IList<ProfitCostPointDto> Points { get; set; } = new List<ProfitCostPointDto>();

        public decimal TotalRevenue { get; set; }

        public decimal TotalCost { get; set; }

        public decimal GrossProfit => TotalRevenue - TotalCost;

        /// <summary>Gross profit over revenue for the whole year, to one decimal.</summary>
        public decimal GrossMargin => TotalRevenue > 0
            ? Math.Round(GrossProfit / TotalRevenue * 100, 1)
            : 0;

        /// <summary>
        /// The revenue whose cost had to be taken from the product's standing price
        /// because nothing was ever bought against it.
        /// </summary>
        public decimal ListPricedRevenue { get; set; }

        /// <summary>
        /// How much of the year rests on that standing price. Said on the chart,
        /// because a margin costed off a price nobody paid is a guess, and a reader is
        /// owed the difference between a guess and a purchase.
        /// </summary>
        public decimal ListPricedShare => TotalRevenue > 0
            ? Math.Round(ListPricedRevenue / TotalRevenue * 100, 1)
            : 0;

        /// <summary>The best month of the year by gross profit, for the footer.</summary>
        public string BestMonthLabel { get; set; } = string.Empty;

        public decimal BestMonthProfit { get; set; }

        /// <summary>
        /// True while the year is still running, so a part-finished year is never read
        /// as a bad one.
        /// </summary>
        public bool IsRunningYear => Year == DateTime.Today.Year;
    }
}
