namespace DevSkill.Inventory.Domain.Enums
{
    public enum SalespersonStatus
    {
        Active = 0,

        /// <summary>
        /// Kept on record so past commission stays readable, but no new sales
        /// document may name them.
        /// </summary>
        Inactive = 1
    }
}
