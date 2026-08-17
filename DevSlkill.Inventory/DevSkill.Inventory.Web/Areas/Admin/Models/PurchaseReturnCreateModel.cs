using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Infrastructure.RazorUtility;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    public class PurchaseReturnCreateModel
    {
        [Display(Name = "Return No")]
        public string ReturnNo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Goods receipt is required.")]
        [Display(Name = "Goods Receipt")]
        public Guid GoodsReceiptId { get; set; }
        public IList<SelectListItem> GoodsReceipts { get; set; }

        [Display(Name = "Return Date")]
        public DateTime ReturnDate { get; set; }

        [Required(ErrorMessage = "A reason is required, because the supplier is credited for this return.")]
        [StringLength(500)]
        public string Reason { get; set; } = string.Empty;

        [StringLength(1000)]
        public string Notes { get; set; } = string.Empty;

        public PurchaseReturnStatus Status { get; set; }
        public IList<SelectListItem> Statuses { get; set; }

        // Filled once a goods receipt is chosen, so the header can be read without
        // going back to the receipt.
        [Display(Name = "Supplier")]
        public string SupplierName { get; set; } = string.Empty;

        [Display(Name = "Warehouse")]
        public string WarehouseName { get; set; } = string.Empty;

        public List<PurchaseReturnItemModel> PurchaseReturnItems { get; set; }

        public PurchaseReturnCreateModel()
        {
            GoodsReceipts = new List<SelectListItem>();

            // A return is either parked as a draft or sent back straight away.
            // Cancelled belongs to the cancellation flow.
            Statuses = Utility.ConvertEnumToSelectList<PurchaseReturnStatus>()
                .Where(x => x.Value != ((int)PurchaseReturnStatus.Cancelled).ToString())
                .ToList();

            Status = PurchaseReturnStatus.Draft;
            ReturnDate = DateTime.Today;

            PurchaseReturnItems = new List<PurchaseReturnItemModel>();
        }

        public void SetGoodsReceiptValues(IList<GoodsReceipt> goodsReceipts)
        {
            GoodsReceipts = Utility.ConvertGoodsReceipts(goodsReceipts);
        }
    }
}
