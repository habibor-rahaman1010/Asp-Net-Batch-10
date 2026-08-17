namespace DevSkill.Inventory.Domain.Entities.PurchaseEntities
{
    public class PurchaseReturnItem : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public Guid PurchaseReturnId { get; set; }
        public virtual PurchaseReturn? PurchaseReturn { get; set; }

        public Guid ProductId { get; set; }
        public virtual Product? Product { get; set; }

        /// <summary>What the goods receipt line brought in, copied for context.</summary>
        public decimal ReceivedQuantity { get; set; }

        public decimal ReturnQuantity { get; set; }

        /// <summary>Unit price copied from the receipt, so the credit can be valued.</summary>
        public decimal UnitPrice { get; set; }

        public decimal LineTotal { get; set; }

        public string Remarks { get; set; } = string.Empty;
    }
}
