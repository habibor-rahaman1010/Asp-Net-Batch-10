namespace DevSkill.Inventory.Domain.Enums
{
    /// <summary>
    /// What the salesperson's rate is measured against. Both are read off the posted
    /// invoice, so the two can never drift apart.
    /// </summary>
    public enum CommissionBasis
    {
        /// <summary>A percentage of the invoice value.</summary>
        Percentage = 0,

        /// <summary>A flat amount for every invoice, whatever it came to.</summary>
        FixedPerInvoice = 1
    }
}
