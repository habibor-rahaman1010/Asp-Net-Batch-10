namespace DevSkill.Inventory.Domain.Enums
{
    /// <summary>
    /// Every operation that is worth telling somebody about. Nobody hears about an
    /// operation unless they have been assigned to it, so this list is the whole
    /// vocabulary the assignment screen offers.
    ///
    /// The numbers are written out and blocked by module. They are what ends up in
    /// the database, so renaming a member is safe but renumbering one would silently
    /// re-point every subscription already saved against it.
    /// </summary>
    public enum NotificationEvent
    {
        // Sales, the order-to-cash run
        SalesQuotationCreated = 101,
        SalesOrderCreated = 102,
        SalesOrderConfirmed = 103,
        DeliveryCompleted = 104,
        SalesInvoicePosted = 105,
        CustomerPaymentReceived = 106,
        SalesReturnCreated = 107,

        // Purchase, the procure-to-pay run
        PurchaseRequisitionCreated = 201,
        PurchaseOrderCreated = 202,
        GoodsReceived = 203,
        PurchaseInvoicePosted = 204,
        SupplierPaymentRecorded = 205,
        PurchaseReturnCreated = 206,

        // Inventory, what happens to the goods themselves
        StockAdjustmentPosted = 301,
        StockTransferCompleted = 302,
        LowStockReached = 303,
        OutOfStock = 304
    }
}
