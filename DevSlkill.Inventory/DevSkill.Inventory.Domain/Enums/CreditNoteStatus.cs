namespace DevSkill.Inventory.Domain.Enums
{
    /// <summary>
    /// A draft credit note owes the customer nothing. Issuing it is what puts the
    /// credit on the customer ledger; applying it is what spends that credit against
    /// an invoice.
    /// </summary>
    public enum CreditNoteStatus
    {
        Draft = 0,
        Issued = 1,

        /// <summary>Fully spent against invoices, so nothing is left to apply.</summary>
        Applied = 2,

        Cancelled = 3
    }
}
