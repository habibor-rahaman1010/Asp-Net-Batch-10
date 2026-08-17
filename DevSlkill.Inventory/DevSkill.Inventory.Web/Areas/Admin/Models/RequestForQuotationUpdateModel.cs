using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Infrastructure.RazorUtility;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    public class RequestForQuotationUpdateModel
    {
        public Guid Id { get; set; }

        [Display(Name = "RFQ No")]
        public string RfqNo { get; set; } = string.Empty;

        [Display(Name = "From Requisition")]
        public Guid? PurchaseRequisitionId { get; set; }
        public IList<SelectListItem> PurchaseRequisitions { get; set; }

        [Required(ErrorMessage = "Warehouse is required.")]
        [Display(Name = "Warehouse")]
        public Guid BusinessLocationId { get; set; }
        public IList<SelectListItem> BusinessLocations { get; set; }

        [Display(Name = "RFQ Date")]
        public DateTime RfqDate { get; set; }

        [Display(Name = "Response Deadline")]
        public DateTime ResponseDeadline { get; set; }

        [StringLength(1000)]
        public string Notes { get; set; } = string.Empty;

        public RequestForQuotationStatus Status { get; set; }
        public IList<SelectListItem> Statuses { get; set; }

        public IList<SelectListItem> Products { get; set; }

        public List<RequestForQuotationItemModel> RequestForQuotationItems { get; set; }

        public RequestForQuotationUpdateModel()
        {
            PurchaseRequisitions = new List<SelectListItem>();
            BusinessLocations = new List<SelectListItem>();
            Products = new List<SelectListItem>();

            Statuses = Utility.ConvertEnumToSelectList<RequestForQuotationStatus>()
                .Where(x => x.Value == ((int)RequestForQuotationStatus.Draft).ToString()
                         || x.Value == ((int)RequestForQuotationStatus.Sent).ToString())
                .ToList();

            RequestForQuotationItems = new List<RequestForQuotationItemModel>();
        }

        public void SetBusinessLocationValues(IList<BusinessLocation> businessLocations)
        {
            BusinessLocations = Utility.ConvertBusinessLocations(businessLocations);
        }

        public void SetPurchaseRequisitionValues(IList<PurchaseRequisition> purchaseRequisitions)
        {
            PurchaseRequisitions = Utility.ConvertPurchaseRequisitions(purchaseRequisitions);
        }

        public void SetProductValues(IList<Product> products)
        {
            Products = Utility.ConvertSelectItemToProduct(products);
        }
    }
}
