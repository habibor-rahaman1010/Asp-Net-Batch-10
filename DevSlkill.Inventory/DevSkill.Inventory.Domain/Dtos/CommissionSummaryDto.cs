namespace DevSkill.Inventory.Domain.Dtos
{
    /// <summary>
    /// One salesperson's standing over a period: what they billed, what that earned
    /// them and how much of it has actually been settled.
    /// </summary>
    public class CommissionSummaryDto
    {
        public Guid SalespersonId { get; set; }
        public string SalespersonCode { get; set; } = string.Empty;
        public string SalespersonName { get; set; } = string.Empty;

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public int InvoiceCount { get; set; }

        /// <summary>Total value of the posted invoices the commission was earned on.</summary>
        public decimal SalesAmount { get; set; }

        /// <summary>What the salesperson was expected to bill over the same period.</summary>
        public decimal TargetAmount { get; set; }

        public decimal PendingCommission { get; set; }
        public decimal ApprovedCommission { get; set; }
        public decimal PaidCommission { get; set; }

        /// <summary>Pending plus approved plus paid: everything earned in the period.</summary>
        public decimal TotalCommission { get; set; }
    }
}
