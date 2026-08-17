namespace DevSkill.Inventory.Domain.Enums
{
    /// <summary>
    /// The purchase book read from a different angle each time. They all run off the
    /// same documents, so any two of them have to agree.
    /// </summary>
    public enum PurchaseReportType
    {
        PurchaseSummary = 0,
        PurchaseDetail = 1,
        SupplierWisePurchase = 2,
        ProductWisePurchase = 3,
        MonthlyPurchase = 4,
        WarehouseWisePurchase = 5,
        PurchaseReturn = 6,
        SupplierOutstanding = 7,
        PurchaseInvoice = 8,
        PurchasePriceHistory = 9
    }
}
