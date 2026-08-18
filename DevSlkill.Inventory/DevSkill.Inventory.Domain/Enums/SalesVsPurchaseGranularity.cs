namespace DevSkill.Inventory.Domain.Enums
{
    /// <summary>
    /// How wide one bar on the sales-against-purchase chart is. A long year range
    /// drawn month by month would put too many bars on the axis to read, so the
    /// chart steps up to whole years instead of shrinking the bars.
    /// </summary>
    public enum SalesVsPurchaseGranularity
    {
        Month = 0,
        Year = 1
    }
}
