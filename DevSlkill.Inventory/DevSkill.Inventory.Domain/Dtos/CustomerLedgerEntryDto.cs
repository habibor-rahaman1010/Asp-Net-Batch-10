namespace DevSkill.Inventory.Domain.Dtos
{
    /// <summary>
    /// One movement on the customer account. Debit is what the customer owes more
    /// of, credit is what takes it away again, and the balance is the running
    /// receivable after this row.
    /// </summary>
    public class CustomerLedgerEntryDto
    {
        public DateTime Date { get; set; }

        /// <summary>Invoice, Return, Credit Note or Collection.</summary>
        public string DocumentType { get; set; } = string.Empty;
        public string DocumentNo { get; set; } = string.Empty;

        public string Particulars { get; set; } = string.Empty;

        /// <summary>Puts the receivable up: an invoice posted against the customer.</summary>
        public decimal Debit { get; set; }

        /// <summary>Takes the receivable down: money collected or credit given back.</summary>
        public decimal Credit { get; set; }

        public decimal Balance { get; set; }
    }
}
