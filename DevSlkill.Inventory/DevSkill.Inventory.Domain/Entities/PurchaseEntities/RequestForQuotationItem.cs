namespace DevSkill.Inventory.Domain.Entities.PurchaseEntities
{
    /// <summary>
    /// One product we want quoted. There is no price here on purpose: the price is
    /// exactly what the supplier is being asked to state.
    /// </summary>
    public class RequestForQuotationItem : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public Guid RequestForQuotationId { get; set; }
        public virtual RequestForQuotation? RequestForQuotation { get; set; }

        public Guid ProductId { get; set; }
        public virtual Product? Product { get; set; }

        public decimal Quantity { get; set; }

        public string Remarks { get; set; } = string.Empty;
    }
}
