using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using DevSkill.Inventory.Infrastructure.RazorUtility;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    public class SupplierLedgerModel
    {
        [Display(Name = "Supplier")]
        public Guid? SupplierId { get; set; }
        public IList<SelectListItem> Suppliers { get; set; }

        [Display(Name = "From")]
        public DateTime FromDate { get; set; }

        [Display(Name = "To")]
        public DateTime ToDate { get; set; }

        /// <summary>Null until a supplier has actually been chosen.</summary>
        public SupplierLedgerDto? Ledger { get; set; }

        public SupplierLedgerModel()
        {
            Suppliers = new List<SelectListItem>();

            // A year back, because a statement is usually read over a long stretch.
            FromDate = DateTime.Today.AddYears(-1);
            ToDate = DateTime.Today;
        }

        public void SetSupplierValues(IList<Supplier> suppliers)
        {
            Suppliers = Utility.ConvertSuppliers(suppliers);
        }
    }
}
