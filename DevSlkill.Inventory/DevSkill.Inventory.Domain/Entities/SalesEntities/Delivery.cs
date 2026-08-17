using DevSkill.Inventory.Domain.Enums;

namespace DevSkill.Inventory.Domain.Entities.SalesEntities
{
    /// <summary>
    /// One shipment made against a source document, which is either a proforma
    /// invoice or a confirmed sales order. That document may need several shipments,
    /// because the customer can be served partially, so the delivered quantity always
    /// lives here and never on the source itself.
    /// </summary>
    /// <remarks>
    /// Exactly one of <see cref="ProformaInvoiceId"/> and <see cref="SalesOrderId"/>
    /// is set. The service enforces that; the database only knows both are optional.
    /// </remarks>
    public class Delivery : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string DeliveryNo { get; set; } = string.Empty;

        /// <summary>Set when the shipment was raised straight off a proforma invoice.</summary>
        public Guid? ProformaInvoiceId { get; set; }
        public virtual ProformaInvoice? ProformaInvoice { get; set; }

        /// <summary>
        /// Set when the shipment was raised off a confirmed sales order, which is the
        /// path that carries on into invoicing and collection.
        /// </summary>
        public Guid? SalesOrderId { get; set; }
        public virtual SalesOrder? SalesOrder { get; set; }

        /// <summary>
        /// Copied from the source document so the delivery can be listed and searched
        /// without loading the whole thing.
        /// </summary>
        public Guid CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }

        /// <summary>
        /// Warehouse the goods leave from. It follows the source document, so stock
        /// can never be taken out of a location the sale was not raised from.
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
