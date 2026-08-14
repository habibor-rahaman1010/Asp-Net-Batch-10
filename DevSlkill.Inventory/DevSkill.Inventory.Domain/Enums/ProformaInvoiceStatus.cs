namespace DevSkill.Inventory.Domain.Enums
{
    public enum ProformaInvoiceStatus
    {
        Draft = 1,
        Sent = 2,
        Accepted = 3,
        Rejected = 4,
        Expired = 5,
        Converted = 6,
        Cancelled = 7,

        // Both states below are written by the delivery flow alone. A user never
        // picks them by hand, they only report how much has already shipped.
        PartiallyDelivered = 8,
        Delivered = 9
    }
}
