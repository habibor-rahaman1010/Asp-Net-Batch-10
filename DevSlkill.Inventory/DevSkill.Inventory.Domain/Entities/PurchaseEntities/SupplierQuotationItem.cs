namespace DevSkill.Inventory.Domain.Entities.PurchaseEntities
{
    public class SupplierQuotationItem : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public Guid SupplierQuotationId { get; set; }
        public virtual SupplierQuotation? SupplierQuotation { get; set; }

        public Guid ProductId { get; set; }
        public virtual Product? Product { get; set; }

        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal LineTotal { get; set; }

        public string Remarks { get; set; } = string.Empty;
    }
}
