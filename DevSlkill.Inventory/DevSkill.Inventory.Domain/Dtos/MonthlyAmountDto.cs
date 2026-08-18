namespace DevSkill.Inventory.Domain.Dtos
{
    /// <summary>
    /// One calendar month's worth of billed value. The month is kept as year plus
    /// month rather than a date so the grouping survives the trip out of the
    /// database without a timezone deciding which month a total lands in.
    /// </summary>
    public class MonthlyAmountDto
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal Total { get; set; }
    }
}
