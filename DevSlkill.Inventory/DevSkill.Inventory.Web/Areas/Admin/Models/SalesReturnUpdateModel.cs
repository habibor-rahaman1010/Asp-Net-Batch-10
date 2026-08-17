using System.ComponentModel.DataAnnotations;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    /// <summary>
    /// Only a draft return is editable. Once the goods are back on the shelf and
    /// credited, an edit would move stock behind the user's back, so it is cancelled
    /// and raised again instead.
    /// </summary>
    public class SalesReturnUpdateModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Return No")]
        public string ReturnNo { get; set; } = string.Empty;

        public Guid SalesInvoiceId { get; set; }

        [Display(Name = "Sales Invoice")]
        public string InvoiceNo { get; set; } = string.Empty;

        [Display(Name = "Customer")]
        public string CustomerName { get; set; } = string.Empty;

        [Display(Name = "Warehouse")]
        public string WarehouseName { get; set; } = string.Empty;

        [Display(Name = "Return Date")]
        public DateTime ReturnDate { get; set; }

        [Required(ErrorMessage = "A reason is needed, so the return can be explained later.")]
        [StringLength(500)]
        public string Reason { get; set; } = string.Empty;

        [StringLength(1000)]
        public string Notes { get; set; } = string.Empty;

        public List<SalesReturnItemModel> SalesReturnItems { get; set; }

        public SalesReturnUpdateModel()
        {
            SalesReturnItems = new List<SalesReturnItemModel>();
            ReturnDate = DateTime.Today;
        }
    }
}
