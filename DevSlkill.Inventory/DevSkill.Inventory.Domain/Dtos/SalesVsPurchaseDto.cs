using DevSkill.Inventory.Domain.Enums;

namespace DevSkill.Inventory.Domain.Dtos
{
    /// <summary>
    /// One bar pair on the sales-against-purchase chart. Both sides are read off
    /// posted invoices, so the two figures are comparable: each is what was actually
    /// billed for goods that had already moved.
    /// </summary>
    public class SalesVsPurchasePointDto
    {
        /// <summary>Short label, ready to sit under an axis tick.</summary>
        public string Label { get; set; } = string.Empty;

        public decimal SalesAmount { get; set; }
        public decimal PurchaseAmount { get; set; }
    }

    /// <summary>
    /// A run of consecutive periods with nothing missing. Periods without a single
    /// invoice are carried as zero rather than dropped, so the axis keeps an even
    /// step and a quiet month reads as quiet instead of vanishing.
    /// </summary>
    public class SalesVsPurchaseDto
    {
        public int FromYear { get; set; }
        public int ToYear { get; set; }

        /// <summary>Whether a bar covers one month or one whole year.</summary>
        public SalesVsPurchaseGranularity Granularity { get; set; }

        /// <summary>
        /// The same thing as a word. The browser reads this rather than the enum, so it
        /// never has to know how the values happen to be numbered.
        /// </summary>
        public string GranularityName => Granularity.ToString();

        public IList<SalesVsPurchasePointDto> Points { get; set; } = new List<SalesVsPurchasePointDto>();

        public decimal TotalSales { get; set; }
        public decimal TotalPurchase { get; set; }

        /// <summary>Sales less purchases over the whole window.</summary>
        public decimal Gap => TotalSales - TotalPurchase;

        /// <summary>
        /// True once the window runs past the current month, which it does whenever the
        /// range ends on the running year. The chart says so rather than letting a
        /// part-finished period look like a downturn.
        /// </summary>
        public bool EndsOnRunningYear => ToYear == DateTime.Today.Year;
    }
}
