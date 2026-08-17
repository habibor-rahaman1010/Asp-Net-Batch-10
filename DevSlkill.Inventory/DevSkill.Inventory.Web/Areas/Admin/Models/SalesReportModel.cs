using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Infrastructure.RazorUtility;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    public class SalesReportModel
    {
        [Display(Name = "Report")]
        public SalesReportType ReportType { get; set; }
        public IList<SelectListItem> ReportTypes { get; set; }

        [Display(Name = "From")]
        public DateTime FromDate { get; set; }

        [Display(Name = "To")]
        public DateTime ToDate { get; set; }

        [Display(Name = "Customer")]
        public Guid? CustomerId { get; set; }
        public IList<SelectListItem> Customers { get; set; }

        [Display(Name = "Product")]
        public Guid? ProductId { get; set; }
        public IList<SelectListItem> Products { get; set; }

        [Display(Name = "Warehouse")]
        public Guid? BusinessLocationId { get; set; }
        public IList<SelectListItem> BusinessLocations { get; set; }

        [Display(Name = "Salesperson")]
        public Guid? SalespersonId { get; set; }
        public IList<SelectListItem> Salespersons { get; set; }

        /// <summary>Null until the filter has actually been run.</summary>
        public SalesReportDto? Report { get; set; }

        public SalesReportModel()
        {
            ReportTypes = Utility.ConvertEnumToSelectList<SalesReportType>();
            Customers = new List<SelectListItem>();
            Products = new List<SelectListItem>();
            BusinessLocations = new List<SelectListItem>();
            Salespersons = new List<SelectListItem>();

            // A month back is the range a seller opens this page for most often.
            FromDate = DateTime.Today.AddMonths(-1);
            ToDate = DateTime.Today;
        }

        public void SetLookupValues(IList<Customer> customers, IList<Product> products,
            IList<BusinessLocation> businessLocations, IList<Salesperson> salespersons)
        {
            Customers = Utility.ConvertCustomers(customers);
            Products = Utility.ConvertSelectItemToProduct(products);
            BusinessLocations = Utility.ConvertBusinessLocations(businessLocations);
            Salespersons = Utility.ConvertSalespersons(salespersons);
        }

        public SalesReportFilterDto ToFilter()
        {
            return new SalesReportFilterDto
            {
                ReportType = ReportType,
                FromDate = FromDate,
                ToDate = ToDate,
                CustomerId = CustomerId == Guid.Empty ? null : CustomerId,
                ProductId = ProductId == Guid.Empty ? null : ProductId,
                BusinessLocationId = BusinessLocationId == Guid.Empty ? null : BusinessLocationId,
                SalespersonId = SalespersonId == Guid.Empty ? null : SalespersonId
            };
        }
    }
}
