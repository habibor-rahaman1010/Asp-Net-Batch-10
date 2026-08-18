using DevSkill.Inventory.Domain.Dtos;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    public class DashboardViewModel
    {
        public int TotalCategoris { get; set; }
        public int TotalProducts { get; set; }
        public int TotalUsers { get; set; }
        public int BounceRate { get; set; }
        public int EngagementRate { get; set; }
        public int ActiveUsers { get; set; }
        public int UniqueVisitors { get; set; }
        public int TotalWarehouse { get; set; }
        public int TotalBrands { get; set; }

        /// <summary>What the in-house stock chart draws.</summary>
        public StockHealthDto StockHealth { get; set; } = new();

        /// <summary>What the warehouse spread chart draws.</summary>
        public WarehouseStockDto WarehouseStock { get; set; } = new();

        /// <summary>What the sales-against-purchase chart draws.</summary>
        public SalesVsPurchaseDto SalesVsPurchase { get; set; } = new();

        /// <summary>What the who-sold-what chart draws, for the year it opens on.</summary>
        public SalespersonSalesDto SalespersonSales { get; set; } = new();

        /// <summary>What the where-we-buy chart draws, for the year it opens on.</summary>
        public SupplierPurchaseDto SupplierPurchase { get; set; } = new();

        /// <summary>
        /// The years the two year pickers list, newest first so the years somebody is
        /// most likely to want are at the top. This reaches back further than the
        /// invoices do, so a range can be picked before the data covers it.
        /// </summary>
        public IList<int> AvailableYears { get; set; } = new List<int>();
    }
}
