using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Infrastructure.RazorUtility;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    public class GoodsReceiptCreateModel
    {
        [Display(Name = "Goods Receipt No")]
        public string GoodsReceiptNo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Purchase order is required.")]
        [Display(Name = "Purchase Order")]
        public Guid PurchaseOrderId { get; set; }
        public IList<SelectListItem> PurchaseOrders { get; set; }

        [Display(Name = "Receipt Date")]
        public DateTime ReceiptDate { get; set; }

        [StringLength(100)]
        [Display(Name = "Supplier Challan No")]
        public string SupplierChallanNo { get; set; } = string.Empty;

        [StringLength(200)]
        [Display(Name = "Received By")]
        public string ReceivedBy { get; set; } = string.Empty;

        [StringLength(1000)]
        public string Notes { get; set; } = string.Empty;

        public GoodsReceiptStatus Status { get; set; }
        public IList<SelectListItem> Statuses { get; set; }

        // Filled once a purchase order is chosen, so the header can be read without
        // going back to the order.
        [Display(Name = "Supplier")]
        public string SupplierName { get; set; } = string.Empty;

        [Display(Name = "Warehouse")]
        public string WarehouseName { get; set; } = string.Empty;

        public List<GoodsReceiptItemModel> GoodsReceiptItems { get; set; }

        public GoodsReceiptCreateModel()
        {
            PurchaseOrders = new List<SelectListItem>();

            // A receipt is either parked as a draft or booked straight in. Cancelled
            // belongs to the cancellation flow.
            Statuses = Utility.ConvertEnumToSelectList<GoodsReceiptStatus>()
                .Where(x => x.Value != ((int)GoodsReceiptStatus.Cancelled).ToString())
                .ToList();

            Status = GoodsReceiptStatus.Draft;
            ReceiptDate = DateTime.Today;

            GoodsReceiptItems = new List<GoodsReceiptItemModel>();
        }

        public void SetPurchaseOrderValues(IList<PurchaseOrder> purchaseOrders)
        {
            PurchaseOrders = Utility.ConvertPurchaseOrders(purchaseOrders);
        }
    }
}
