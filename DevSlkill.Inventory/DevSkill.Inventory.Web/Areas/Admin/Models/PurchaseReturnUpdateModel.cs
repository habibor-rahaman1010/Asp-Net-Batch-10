using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Infrastructure.RazorUtility;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    public class PurchaseReturnUpdateModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Return No")]
        public string ReturnNo { get; set; } = string.Empty;

        public Guid GoodsReceiptId { get; set; }

        /// <summary>Shown read only; a return never moves to another receipt.</summary>
        [Display(Name = "Goods Receipt")]
        public string GoodsReceiptNo { get; set; } = string.Empty;

        [Display(Name = "Return Date")]
        public DateTime ReturnDate { get; set; }

        [Required(ErrorMessage = "A reason is required, because the supplier is credited for this return.")]
        [StringLength(500)]
        public string Reason { get; set; } = string.Empty;

        [StringLength(1000)]
        public string Notes { get; set; } = string.Empty;

        public PurchaseReturnStatus Status { get; set; }
        public IList<SelectListItem> Statuses { get; set; }

        [Display(Name = "Supplier")]
        public string SupplierName { get; set; } = string.Empty;

        [Display(Name = "Warehouse")]
        public string WarehouseName { get; set; } = string.Empty;

        public List<PurchaseReturnItemModel> PurchaseReturnItems { get; set; }

        public PurchaseReturnUpdateModel()
        {
            Statuses = Utility.ConvertEnumToSelectList<PurchaseReturnStatus>()
                .Where(x => x.Value != ((int)PurchaseReturnStatus.Cancelled).ToString())
                .ToList();

            PurchaseReturnItems = new List<PurchaseReturnItemModel>();
        }
    }
}
