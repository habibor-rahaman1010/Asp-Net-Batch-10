namespace DevSkill.Inventory.Domain.Dtos
{
    /// <summary>
    /// What the year filter offers and where it starts. The years on offer and the
    /// years picked to begin with are deliberately two different things: the list has
    /// to reach back far enough to be worth using even on a database whose invoices
    /// are all from this year, while the opening selection should land on the years
    /// that actually have something in them.
    /// </summary>
    public class SalesVsPurchaseYearOptionsDto
    {
        /// <summary>Oldest year the two pickers list.</summary>
        public int FirstOfferedYear { get; set; }

        /// <summary>Newest year the two pickers list, which is always the running year.</summary>
        public int LastOfferedYear { get; set; }

        public int DefaultFromYear { get; set; }
        public int DefaultToYear { get; set; }

        /// <summary>Every year on offer, newest first.</summary>
        public IList<int> OfferedYears =>
            Enumerable.Range(FirstOfferedYear, LastOfferedYear - FirstOfferedYear + 1)
                .Reverse()
                .ToList();
    }
}
