namespace DevSkill.Inventory.Domain.Dtos
{
    /// <summary>
    /// One supplier's bar: what they billed us over the year being looked at. The
    /// figure is invoiced value, not cash paid out, so it says what was bought
    /// rather than what has been settled.
    /// </summary>
    public class SupplierPurchasePointDto
    {
        public string SupplierName { get; set; } = string.Empty;

        public string SupplierCode { get; set; } = string.Empty;

        public decimal PurchasedAmount { get; set; }

        /// <summary>How many invoices that value came off, for the tooltip.</summary>
        public int InvoiceCount { get; set; }
    }

    /// <summary>
    /// Where the goods were bought over one calendar year, biggest supplier first.
    /// Every purchase invoice belongs to a supplier, so unlike the salesperson chart
    /// there is nothing to carry as unassigned.
    /// </summary>
    public class SupplierPurchaseDto
    {
        public int Year { get; set; }

        /// <summary>Every supplier who billed something that year, biggest first.</summary>
        public IList<SupplierPurchasePointDto> Points { get; set; } = new List<SupplierPurchasePointDto>();

        /// <summary>Everything billed to us that year.</summary>
        public decimal TotalPurchase { get; set; }

        public int TotalInvoices { get; set; }

        /// <summary>How many bars the chart draws.</summary>
        public int SupplierCount => Points.Count;

        /// <summary>The biggest supplier of the year, or empty when nothing was bought.</summary>
        public string TopSupplierName => Points.Count > 0 ? Points[0].SupplierName : string.Empty;

        public decimal TopSupplierAmount => Points.Count > 0 ? Points[0].PurchasedAmount : 0;

        /// <summary>
        /// How much of the year's spend went to the biggest supplier. Worth seeing on
        /// its own, because everything coming from one place is a risk rather than a
        /// good deal.
        /// </summary>
        public decimal TopSupplierShare => TotalPurchase > 0
            ? Math.Round(TopSupplierAmount / TotalPurchase * 100, 1)
            : 0;

        /// <summary>
        /// True while the year is still running, so a part-finished year is never read
        /// as a quiet one.
        /// </summary>
        public bool IsRunningYear => Year == DateTime.Today.Year;
    }
}
