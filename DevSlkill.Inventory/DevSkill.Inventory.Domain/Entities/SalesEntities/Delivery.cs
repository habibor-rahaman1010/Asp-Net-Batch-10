using DevSkill.Inventory.Domain.Enums;

namespace DevSkill.Inventory.Domain.Entities.SalesEntities
{
    /// <summary>
    /// One shipment made against a proforma invoice. A proforma may need several of
    /// them, because the customer can be served partially, so the delivered quantity
    /// always lives here and never on the proforma itself.
    /// </summary>
    public class Delivery : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string DeliveryNo { get; set; } = string.Empty;

        public Guid ProformaInvoiceId { get; set; }
        public virtual ProformaInvoice? ProformaInvoice { get; set; }

        /// <summary>
        /// Copied from the proforma invoice so the delivery can be listed and searched
        /// without loading the whole document.
        /// </summary>
        public Guid CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }

        /// <summary>
        /// Warehouse the goods leave from. It follows the proforma invoice, so stock
        /// can never be taken out of a location the sale was not quoted from.
        /// </summary>
        public Guid BusinessLocationId { get; set; }
        public virtual BusinessLocation? BusinessLocation { get; set; }

        public DateTime DeliveryDate { get; set; }

        public DeliveryStatus Status { get; set; }

        /// <summary>
        /// Person who signed for the goods at the customer end.
        /// </summary>
        public string ReceivedBy { get; set; } = string.Empty;

        public string Notes { get; set; } = string.Empty;

        public string CreatedBy { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public virtual ICollection<DeliveryItem>? DeliveryItems { get; set; }
    }
}
