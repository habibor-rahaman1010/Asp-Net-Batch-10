using DevSkill.Inventory.Domain.Enums;

namespace DevSkill.Inventory.Domain.Entities.SalesEntities
{
    /// <summary>
    /// Commission earned on one posted invoice. It is written when the invoice is
    /// posted and reversed when that invoice is cancelled, so the two can never
    /// disagree about what was actually sold.
    /// </summary>
    public class SalesCommission : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public Guid SalespersonId { get; set; }
        public virtual Salesperson? Salesperson { get; set; }

        public Guid SalesInvoiceId { get; set; }
        public virtual SalesInvoice? SalesInvoice { get; set; }

        public DateTime EarnedDate { get; set; }

        /// <summary>
        /// The invoice value the rate was applied to, copied so the line can be
        /// re-checked without reading the invoice again.
        /// </summary>
        public decimal BasisAmount { get; set; }

        public CommissionBasis CommissionBasis { get; set; }

        /// <summary>The rate as it stood when the commission was earned.</summary>
        public decimal CommissionRate { get; set; }

        public decimal CommissionAmount { get; set; }

        public CommissionStatus Status { get; set; }

        public DateTime? PaidDate { get; set; }

        public string Notes { get; set; } = string.Empty;

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
