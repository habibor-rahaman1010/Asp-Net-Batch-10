using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Infrastructure.RazorUtility;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    public class PurchaseReportModel
    {
        [Display(Name = "Report")]
        public PurchaseReportType ReportType { get; set; }
        public IList<SelectListItem> ReportTypes { get; set; }

        [Display(Name = "From")]
        public DateTime FromDate { get; set; }

        [Display(Name = "To")]
        public DateTime ToDate { get; set; }

        [Display(Name = "Supplier")]
        public Guid? SupplierId { get; set; }
        public IList<SelectListItem> Suppliers { get; set; }

        [Display(Name = "Product")]
        public Guid? ProductId { get; set; }
        public IList<SelectListItem> Products { get; set; }

        [Display(Name = "Warehouse")]
        public Guid? BusinessLocationId { get; set; }
        public IList<SelectListItem> BusinessLocations { get; set; }

        /// <summary>Null until the filter has actually been run.</summary>
        public PurchaseReportDto? Report { get; set; }

        public PurchaseReportModel()
        {
            ReportTypes = Utility.ConvertEnumToSelectList<PurchaseReportType>();
            Suppliers = new List<SelectListItem>();
            Products = new List<SelectListItem>();
            BusinessLocations = new List<SelectListItem>();

            // A month back is the range a buyer opens this page for most often.
            FromDate = DateTime.Today.AddMonths(-1);
            ToDate = DateTime.Today;
        }

        public void SetLookupValues(IList<Supplier> suppliers, IList<Product> products,
            IList<BusinessLocation> businessLocations)
        {
            Suppliers = Utility.ConvertSuppliers(suppliers);
            Products = Utility.ConvertSelectItemToProduct(products);
            BusinessLocations = Utility.ConvertBusinessLocations(businessLocations);
        }

        public PurchaseReportFilterDto ToFilter()
        {
            return new PurchaseReportFilterDto
            {
                ReportType = ReportType,
                FromDate = FromDate,
                ToDate = ToDate,
                SupplierId = SupplierId == Guid.Empty ? null : SupplierId,
                ProductId = ProductId == Guid.Empty ? null : ProductId,
                BusinessLocationId = BusinessLocationId == Guid.Empty ? null : BusinessLocationId
            };
        }
    }
}
