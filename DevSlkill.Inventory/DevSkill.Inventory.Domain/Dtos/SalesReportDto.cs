namespace DevSkill.Inventory.Domain.Dtos
{
    /// <summary>
    /// One column of a sales report. <see cref="Numeric"/> is what the view uses to
    /// right align a figure, so every report can share one table.
    /// </summary>
    public class SalesReportColumnDto
    {
        public string Title { get; set; } = string.Empty;
        public bool Numeric { get; set; }

        public SalesReportColumnDto()
        {
        }

        public SalesReportColumnDto(string title, bool numeric = false)
        {
            Title = title;
            Numeric = numeric;
        }
    }

    /// <summary>
    /// A finished sales report: the columns it has, the rows it found and the footer
    /// that adds them up.
    /// </summary>
    /// <remarks>
    /// The cells arrive already formatted. Ten reports with ten different shapes
    /// would otherwise need ten view models and ten views to say the same thing.
    /// </remarks>
    public class SalesReportDto
    {
        public string Title { get; set; } = string.Empty;

        /// <summary>What the report counts, said in one line above the table.</summary>
        public string Description { get; set; } = string.Empty;

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public IList<SalesReportColumnDto> Columns { get; set; } = new List<SalesReportColumnDto>();
        public IList<IList<string>> Rows { get; set; } = new List<IList<string>>();

        /// <summary>
        /// The totals row, cell for cell against <see cref="Columns"/>. Empty when a
        /// report has nothing worth adding up.
        /// </summary>
        public IList<string> Totals { get; set; } = new List<string>();
    }
}
