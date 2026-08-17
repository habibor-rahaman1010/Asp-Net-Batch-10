using DevSkill.Inventory.Domain.Enums;

namespace DevSkill.Inventory.Domain.Dtos
{
    /// <summary>
    /// What to report on. The optional ids narrow the same period down further; a
    /// report simply ignores a filter that does not apply to it.
    /// </summary>
    public class SalesReportFilterDto
    {
        public SalesReportType ReportType { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public Guid? CustomerId { get; set; }
        public Guid? ProductId { get; set; }
        public Guid? BusinessLocationId { get; set; }
        public Guid? SalespersonId { get; set; }
    }
}
