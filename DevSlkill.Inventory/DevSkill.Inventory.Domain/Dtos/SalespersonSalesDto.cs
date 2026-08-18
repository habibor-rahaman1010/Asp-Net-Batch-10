namespace DevSkill.Inventory.Domain.Dtos
{
    /// <summary>
    /// One salesperson's bar: what they billed over the year being looked at. The
    /// figure is invoiced value, not collected cash, so it says what was sold rather
    /// than what has been paid for.
    /// </summary>
    public class SalespersonSalesPointDto
    {
        public string SalespersonName { get; set; } = string.Empty;

        /// <summary>Empty for invoices nobody was credited with.</summary>
        public string SalespersonCode { get; set; } = string.Empty;

        public decimal InvoicedAmount { get; set; }

        /// <summary>How many invoices that value came off, for the tooltip.</summary>
        public int InvoiceCount { get; set; }
    }

    /// <summary>
    /// Who sold what over one calendar year, biggest seller first. Invoices raised
    /// without a salesperson are carried as their own bar rather than dropped, so
    /// the bars still add up to the year's sales.
    /// </summary>
    public class SalespersonSalesDto
    {
        public int Year { get; set; }

        /// <summary>Every salesperson who billed something that year, biggest first.</summary>
        public IList<SalespersonSalesPointDto> Points { get; set; } = new List<SalespersonSalesPointDto>();

        /// <summary>Everything billed that year, the unassigned invoices included.</summary>
        public decimal TotalSales { get; set; }

        public int TotalInvoices { get; set; }

        /// <summary>How many bars the chart draws.</summary>
        public int SellerCount => Points.Count;

        /// <summary>The biggest seller of the year, or empty when nothing was billed.</summary>
        public string TopSellerName => Points.Count > 0 ? Points[0].SalespersonName : string.Empty;

        public decimal TopSellerAmount => Points.Count > 0 ? Points[0].InvoicedAmount : 0;

        /// <summary>
        /// True while the year is still running, so a part-finished year is never read
        /// as a bad one.
        /// </summary>
        public bool IsRunningYear => Year == DateTime.Today.Year;
    }
}
