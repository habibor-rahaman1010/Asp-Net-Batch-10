namespace DevSkill.Inventory.Domain.Enums
{
    /// <summary>
    /// Determines which part of a sale a <see cref="Entities.SalesEntities.DiscountRule"/> applies to.
    /// </summary>
    public enum DiscountScope
    {
        Product = 1,
        Order = 2,
        Customer = 3,
        Quantity = 4
    }
}
