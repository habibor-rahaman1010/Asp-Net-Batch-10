namespace DevSkill.Inventory.Domain.Dtos
{
    /// <summary>
    /// A supplier account over one period: where the balance stood when the period
    /// began, everything that moved it, and where it stands at the end.
    /// </summary>
    public class SupplierLedgerDto
    {
        public Guid SupplierId { get; set; }
        public string SupplierName { get; set; } = string.Empty;
        public string SupplierCode { get; set; } = string.Empty;

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        /// <summary>
        /// The supplier's own opening balance plus everything that moved before the
        /// period started, so the first row of the period reads correctly.
        /// </summary>
        public decimal OpeningBalance { get; set; }

        public decimal TotalDebit { get; set; }
        public decimal TotalCredit { get; set; }
        public decimal ClosingBalance { get; set; }

        public IList<SupplierLedgerEntryDto> Entries { get; set; } = new List<SupplierLedgerEntryDto>();
    }
}
