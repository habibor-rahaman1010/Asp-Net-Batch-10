namespace DevSkill.Inventory.Domain.Dtos
{
    /// <summary>
    /// A customer account over one period: where the balance stood when the period
    /// began, everything that moved it, and where it stands at the end.
    /// </summary>
    public class CustomerLedgerDto
    {
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerCode { get; set; } = string.Empty;

        /// <summary>The ceiling the closing balance is measured against.</summary>
        public decimal CreditLimit { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        /// <summary>
        /// The customer's own opening balance plus everything that moved before the
        /// period started, so the first row of the period reads correctly.
        /// </summary>
        public decimal OpeningBalance { get; set; }

        public decimal TotalDebit { get; set; }
        public decimal TotalCredit { get; set; }
        public decimal ClosingBalance { get; set; }

        public IList<CustomerLedgerEntryDto> Entries { get; set; } = new List<CustomerLedgerEntryDto>();
    }
}
