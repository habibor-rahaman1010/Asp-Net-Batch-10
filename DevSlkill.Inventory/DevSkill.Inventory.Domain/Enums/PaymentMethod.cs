namespace DevSkill.Inventory.Domain.Enums
{
    public enum PaymentMethod
    {
        Cash = 0,
        BankTransfer = 1,
        Cheque = 2,
        MobileBanking = 3,
        Card = 4,

        /// <summary>
        /// Settled against a purchase return credit rather than with money.
        /// </summary>
        Adjustment = 5
    }
}
