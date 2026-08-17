using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Infrastructure.RazorUtility;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    public class StockReservationCreateModel
    {
        [Display(Name = "Reservation No")]
        public string ReservationNo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Sales order is required.")]
        [Display(Name = "Sales Order")]
        public Guid SalesOrderId { get; set; }
        public IList<SelectListItem> SalesOrders { get; set; }

        /// <summary>Read from the order once it is chosen, never typed.</summary>
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

        /// <summary>
        /// Ticking this holds the stock straight away, which is the point of raising a
        /// reservation at all. Left unticked it is only a plan.
        /// </summary>
        [Display(Name = "Hold the stock now")]
        public bool ReserveImmediately { get; set; } = true;

        public List<StockReservationItemModel> StockReservationItems { get; set; }

        public StockReservationCreateModel()
        {
            SalesOrders = new List<SelectListItem>();
            StockReservationItems = new List<StockReservationItemModel>();
            ReservationDate = DateTime.Today;
            ExpiryDate = DateTime.Today.AddDays(7);
        }

        public void SetSalesOrderValues(IList<SalesOrder> salesOrders)
        {
            SalesOrders = Utility.ConvertSalesOrders(salesOrders);
        }
    }
}
