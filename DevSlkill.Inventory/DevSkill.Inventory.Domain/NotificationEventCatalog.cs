using DevSkill.Inventory.Domain.Enums;

namespace DevSkill.Inventory.Domain
{
    /// <summary>
    /// What one event is called and how it should look. Kept beside the enum rather
    /// than in the views, so the assignment screen, the bell and the notification
    /// list all read the same wording and the same icon for an event.
    /// </summary>
    public sealed class NotificationEventInfo
    {
        public NotificationEvent Event { get; init; }

        /// <summary>Which module it belongs to. The assignment screen groups by this.</summary>
        public string Group { get; init; } = string.Empty;

        public string Name { get; init; } = string.Empty;

        /// <summary>When it fires, in one sentence, so an assignment is made knowingly.</summary>
        public string Description { get; init; } = string.Empty;

        /// <summary>FontAwesome class, without the leading "fas".</summary>
        public string Icon { get; init; } = string.Empty;

        /// <summary>Bootstrap colour suffix, used for the icon rather than for meaning.</summary>
        public string Colour { get; init; } = string.Empty;
    }

    public static class NotificationEventCatalog
    {
        public const string SalesGroup = "Sales";
        public const string PurchaseGroup = "Purchase";
        public const string InventoryGroup = "Inventory";

        /// <summary>
        /// Every event there is, in the order the assignment screen lists them: the
        /// order-to-cash run, then procure-to-pay, then the goods themselves.
        /// </summary>
        public static IReadOnlyList<NotificationEventInfo> All { get; } = new List<NotificationEventInfo>
        {
            new() { Event = NotificationEvent.SalesQuotationCreated, Group = SalesGroup,
                    Name = "Sales Quotation Created", Icon = "fa-file-signature", Colour = "info",
                    Description = "A quotation has been raised for a customer." },

            new() { Event = NotificationEvent.SalesOrderCreated, Group = SalesGroup,
                    Name = "Sales Order Created", Icon = "fa-clipboard-list", Colour = "info",
                    Description = "A customer order has been entered." },

            new() { Event = NotificationEvent.SalesOrderConfirmed, Group = SalesGroup,
                    Name = "Sales Order Confirmed", Icon = "fa-check-circle", Colour = "success",
                    Description = "An order has been confirmed and is ready to be worked on." },

            new() { Event = NotificationEvent.DeliveryCompleted, Group = SalesGroup,
                    Name = "Delivery Completed", Icon = "fa-shipping-fast", Colour = "success",
                    Description = "Goods have left the warehouse against an order." },

            new() { Event = NotificationEvent.SalesInvoicePosted, Group = SalesGroup,
                    Name = "Sales Invoice Posted", Icon = "fa-file-invoice-dollar", Colour = "primary",
                    Description = "An invoice has been posted and the customer now owes it." },

            new() { Event = NotificationEvent.CustomerPaymentReceived, Group = SalesGroup,
                    Name = "Customer Payment Received", Icon = "fa-hand-holding-usd", Colour = "success",
                    Description = "Money has been collected from a customer." },

            new() { Event = NotificationEvent.SalesReturnCreated, Group = SalesGroup,
                    Name = "Sales Return Created", Icon = "fa-undo-alt", Colour = "warning",
                    Description = "A customer has sent goods back." },

            new() { Event = NotificationEvent.PurchaseRequisitionCreated, Group = PurchaseGroup,
                    Name = "Purchase Requisition Created", Icon = "fa-clipboard-check", Colour = "info",
                    Description = "Somebody has asked for goods to be bought." },

            new() { Event = NotificationEvent.PurchaseOrderCreated, Group = PurchaseGroup,
                    Name = "Purchase Order Created", Icon = "fa-clipboard-list", Colour = "info",
                    Description = "An order has been placed with a supplier." },

            new() { Event = NotificationEvent.GoodsReceived, Group = PurchaseGroup,
                    Name = "Goods Received", Icon = "fa-dolly", Colour = "success",
                    Description = "Goods have arrived from a supplier and gone into stock." },

            new() { Event = NotificationEvent.PurchaseInvoicePosted, Group = PurchaseGroup,
                    Name = "Purchase Invoice Posted", Icon = "fa-file-invoice-dollar", Colour = "primary",
                    Description = "A supplier invoice has been posted and is now payable." },

            new() { Event = NotificationEvent.SupplierPaymentRecorded, Group = PurchaseGroup,
                    Name = "Supplier Payment Recorded", Icon = "fa-money-check-alt", Colour = "success",
                    Description = "Money has been paid out to a supplier." },

            new() { Event = NotificationEvent.PurchaseReturnCreated, Group = PurchaseGroup,
                    Name = "Purchase Return Created", Icon = "fa-undo-alt", Colour = "warning",
                    Description = "Goods have been sent back to a supplier." },

            new() { Event = NotificationEvent.StockAdjustmentPosted, Group = InventoryGroup,
                    Name = "Stock Adjustment Posted", Icon = "fa-sliders-h", Colour = "warning",
                    Description = "Stock on hand has been corrected by hand." },

            new() { Event = NotificationEvent.StockTransferCompleted, Group = InventoryGroup,
                    Name = "Stock Transfer Completed", Icon = "fa-exchange-alt", Colour = "info",
                    Description = "Goods have moved from one warehouse to another." },

            new() { Event = NotificationEvent.LowStockReached, Group = InventoryGroup,
                    Name = "Low Stock Reached", Icon = "fa-exclamation-triangle", Colour = "warning",
                    Description = "A product has fallen to or below its alert quantity." },

            new() { Event = NotificationEvent.OutOfStock, Group = InventoryGroup,
                    Name = "Out Of Stock", Icon = "fa-times-circle", Colour = "danger",
                    Description = "A product has run out altogether." }
        };

        private static readonly Dictionary<NotificationEvent, NotificationEventInfo> ByEvent =
            All.ToDictionary(x => x.Event);

        /// <summary>
        /// What this event is called. An event with no entry still comes back with
        /// something readable, so a half-finished addition never breaks a screen.
        /// </summary>
        public static NotificationEventInfo Describe(NotificationEvent notificationEvent)
        {
            return ByEvent.TryGetValue(notificationEvent, out var info)
                ? info
                : new NotificationEventInfo
                {
                    Event = notificationEvent,
                    Group = "Other",
                    Name = notificationEvent.ToString(),
                    Description = string.Empty,
                    Icon = "fa-bell",
                    Colour = "secondary"
                };
        }

        /// <summary>The groups in listing order, with their events.</summary>
        public static IEnumerable<IGrouping<string, NotificationEventInfo>> Grouped()
        {
            return All.GroupBy(x => x.Group);
        }
    }
}
