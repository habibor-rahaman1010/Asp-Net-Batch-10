using DevSkill.Inventory.Domain.Enums;

namespace DevSkill.Inventory.Domain.Entities.SalesEntities
{
    /// <summary>
    /// Someone who sells, and the rate their commission is worked out at. The rate
    /// is copied onto every commission line when it is earned, so changing it later
    /// never rewrites what has already been earned.
    /// </summary>
    public class Salesperson : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string SalespersonCode { get; set; } = string.Empty;
        public string SalespersonName { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Identity user this salesperson signs in as. Identity lives in a different
        /// DbContext, so this is stored without a foreign key.
        /// </summary>
        public Guid? UserId { get; set; }

        public CommissionBasis CommissionBasis { get; set; }

        /// <summary>
        /// A percentage when the basis is percentage, a flat amount per invoice when
        /// it is not.
        /// </summary>
        public decimal CommissionRate { get; set; }

        /// <summary>What the salesperson is expected to bill in a month.</summary>
        public decimal MonthlyTarget { get; set; }

        public SalespersonStatus Status { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
