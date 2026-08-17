using System.ComponentModel.DataAnnotations;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    /// <summary>
    /// Only a draft reservation is editable. Once the stock is really held, changing
    /// the quantities would move the reserved column behind the user's back, so it is
    /// released and raised again instead.
    /// </summary>
    public class StockReservationUpdateModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Reservation No")]
        public string ReservationNo { get; set; } = string.Empty;

        public Guid SalesOrderId { get; set; }

        [Display(Name = "Sales Order")]
        public string SalesOrderNo { get; set; } = string.Empty;

        [Display(Name = "Customer")]
        public string CustomerName { get; set; } = string.Empty;

        [Display(Name = "Warehouse")]
        public string WarehouseName { get; set; } = string.Empty;

        [Display(Name = "Reservation Date")]
        public DateTime ReservationDate { get; set; }

        [Display(Name = "Expiry Date")]
        public DateTime ExpiryDate { get; set; }

        [StringLength(1000)]
        public string Notes { get; set; } = string.Empty;

        public List<StockReservationItemModel> StockReservationItems { get; set; }

        public StockReservationUpdateModel()
        {
            StockReservationItems = new List<StockReservationItemModel>();
            ReservationDate = DateTime.Today;
            ExpiryDate = DateTime.Today.AddDays(7);
        }
    }
}
