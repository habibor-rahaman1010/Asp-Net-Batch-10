namespace DevSkill.Inventory.Domain.Enums
{
    /// <summary>
    /// The sales book read from a different angle each time. They all run off the
    /// same documents, so any two of them have to agree.
    /// </summary>
    public enum SalesReportType
    {
        SalesSummary = 0,
        SalesDetail = 1,
        CustomerWiseSales = 2,
        ProductWiseSales = 3,
        MonthlySales = 4,
        WarehouseWiseSales = 5,
        SalesReturn = 6,
        CustomerOutstanding = 7,
        SalesInvoice = 8,
        SalespersonPerformance = 9
    }
}
