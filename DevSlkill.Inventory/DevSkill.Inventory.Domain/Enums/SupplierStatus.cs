namespace DevSkill.Inventory.Domain.Enums
{
    public enum SupplierStatus
    {
        Active = 0,
        Inactive = 1,

        /// <summary>
        /// Kept on record for history, but no new purchase document may be raised
        /// against it. Existing documents stay untouched.
        /// </summary>
        Blacklisted = 2
    }
}
