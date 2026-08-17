namespace DevSkill.Inventory.Domain.Dtos
{
    /// <summary>
    /// One movement on the supplier account. Credit is what the supplier is owed
    /// more of, debit is what takes it away again, and the balance is the running
    /// payable after this row.
    /// </summary>
    public class SupplierLedgerEntryDto
    {
        public DateTime Date { get; set; }

        /// <summary>Invoice, Return or Payment.</summary>
        public string DocumentType { get; set; } = string.Empty;
        public string DocumentNo { get; set; } = string.Empty;

        public string Particulars { get; set; } = string.Empty;

        /// <summary>Takes the payable down: a payment made or goods sent back.</summary>
        public decimal Debit { get; set; }

        /// <summary>Puts the payable up: an invoice posted against us.</summary>
        public decimal Credit { get; set; }

        public decimal Balance { get; set; }
    }
}
