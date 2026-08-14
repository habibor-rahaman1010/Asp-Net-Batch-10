using DevSkill.Inventory.Domain.Enums;

namespace DevSkill.Inventory.Domain.Entities.SalesEntities
{
    public class Customer : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerCode { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        public CustomerType CustomerType { get; set; }

        public decimal CreditLimit { get; set; }
        public decimal OpeningBalance { get; set; }

        /// <summary>
        /// Running due of the customer. Only sales/payment/return operations
        /// are allowed to change this value, always inside a transaction.
        /// </summary>
        public decimal CurrentOutstanding { get; set; }

        public CustomerStatus Status { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
