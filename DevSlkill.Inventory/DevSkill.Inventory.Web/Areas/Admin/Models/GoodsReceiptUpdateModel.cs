using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Infrastructure.RazorUtility;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    public class GoodsReceiptUpdateModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Goods Receipt No")]
        public string GoodsReceiptNo { get; set; } = string.Empty;

        public Guid PurchaseOrderId { get; set; }

        /// <summary>Shown read only; a receipt never moves to another order.</summary>
        [Display(Name = "Purchase Order")]
        public string PurchaseOrderNo { get; set; } = string.Empty;

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

        [Display(Name = "Supplier")]
        public string SupplierName { get; set; } = string.Empty;

        [Display(Name = "Warehouse")]
        public string WarehouseName { get; set; } = string.Empty;

        public List<GoodsReceiptItemModel> GoodsReceiptItems { get; set; }

        public GoodsReceiptUpdateModel()
        {
            Statuses = Utility.ConvertEnumToSelectList<GoodsReceiptStatus>()
                .Where(x => x.Value != ((int)GoodsReceiptStatus.Cancelled).ToString())
                .ToList();

            GoodsReceiptItems = new List<GoodsReceiptItemModel>();
        }
    }
}
