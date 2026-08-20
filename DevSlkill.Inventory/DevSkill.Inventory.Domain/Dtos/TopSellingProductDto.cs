namespace DevSkill.Inventory.Domain.Dtos
{
    /// <summary>
    /// One slice of the pie: what a product was billed for over the year being looked
    /// at. The figure is invoiced value, not collected cash, so it says what was sold
    /// rather than what has been paid for.
    /// </summary>
    public class TopSellingProductPointDto
    {
        public string ProductName { get; set; } = string.Empty;

        /// <summary>Empty on the lumped slice, which stands for no single product.</summary>
        public string Sku { get; set; } = string.Empty;

        /// <summary>How much was billed for it, before any invoice-level charge.</summary>
        public decimal InvoicedAmount { get; set; }

        /// <summary>How many units went out on those lines, for the tooltip.</summary>
        public decimal QuantitySold { get; set; }

        /// <summary>How many invoices it was billed on, for the tooltip.</summary>
        public int InvoiceCount { get; set; }

        /// <summary>The slice's share of the year, worked out once so the chart and the
        /// tooltip can never disagree about it.</summary>
        public decimal Share { get; set; }

        /// <summary>
        /// True on the one slice that stands for everything outside the leading few,
        /// so the chart can grey it out rather than colour it like a product.
        /// </summary>
        public bool IsOthers { get; set; }
    }

    /// <summary>
    /// What sold best over one calendar year, biggest earner first. Only the leading
    /// few products get a slice of their own; the rest are carried as a single lumped
    /// slice rather than dropped, so the slices still add up to the year's billed
    /// lines.
    /// </summary>
    public class TopSellingProductDto
    {
        public int Year { get; set; }

        /// <summary>The leading products, biggest first, with the lumped slice last.</summary>
        public IList<TopSellingProductPointDto> Points { get; set; } = new List<TopSellingProductPointDto>();

        /// <summary>
        /// Everything billed on invoice lines that year. This is the sum of the lines
        /// rather than of the invoices, so it sits a little under the sales figure the
        /// other charts show, which also carries freight and the like.
        /// </summary>
        public decimal TotalSales { get; set; }

        public decimal TotalQuantity { get; set; }

        /// <summary>How many different products sold that year, lumping aside.</summary>
        public int ProductCount { get; set; }

        /// <summary>How many slices the chart draws.</summary>
        public int SliceCount => Points.Count;

        /// <summary>The best seller of the year, or empty when nothing was billed.</summary>
        public string TopProductName => Points.Count > 0 ? Points[0].ProductName : string.Empty;

        public decimal TopProductAmount => Points.Count > 0 ? Points[0].InvoicedAmount : 0;

        /// <summary>
        /// How much of the year's selling one product carried. Worth seeing on its own,
        /// because a catalogue resting on a single line is a risk rather than a win.
        /// </summary>
        public decimal TopProductShare => Points.Count > 0 ? Points[0].Share : 0;

        /// <summary>
        /// True while the year is still running, so a part-finished year is never read
        /// as a quiet one.
        /// </summary>
        public bool IsRunningYear => Year == DateTime.Today.Year;
    }
}
