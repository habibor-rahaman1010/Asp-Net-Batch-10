using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Infrastructure.RazorUtility;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    public class SalesReturnCreateModel
    {
        [Display(Name = "Return No")]
        public string ReturnNo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Sales invoice is required.")]
        [Display(Name = "Sales Invoice")]
        public Guid SalesInvoiceId { get; set; }
        public IList<SelectListItem> SalesInvoices { get; set; }

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

        /// <summary>
        /// Ticking this takes the goods back straight away: stock goes up and a credit
        /// note is raised for what the return is worth. Left unticked it is a draft.
        /// </summary>
        [Display(Name = "Take the goods back now")]
        public bool ConfirmImmediately { get; set; } = true;

        public List<SalesReturnItemModel> SalesReturnItems { get; set; }

        public SalesReturnCreateModel()
        {
            SalesInvoices = new List<SelectListItem>();
            SalesReturnItems = new List<SalesReturnItemModel>();
            ReturnDate = DateTime.Today;
        }

        public void SetSalesInvoiceValues(IList<SalesInvoice> salesInvoices)
        {
            SalesInvoices = Utility.ConvertSalesInvoices(salesInvoices);
        }
    }
}
