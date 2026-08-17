using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Infrastructure.RazorUtility;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    public class PurchaseRequisitionUpdateModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Requisition No")]
        public string RequisitionNo { get; set; } = string.Empty;

        [Display(Name = "Requisition Date")]
        public DateTime RequisitionDate { get; set; }

        [Display(Name = "Required Date")]
        public DateTime RequiredDate { get; set; }

        [Required(ErrorMessage = "Warehouse is required.")]
        [Display(Name = "Warehouse")]
        public Guid BusinessLocationId { get; set; }
        public IList<SelectListItem> BusinessLocations { get; set; }

        [Display(Name = "Requested By")]
        public Guid? RequestedById { get; set; }
        public IList<SelectListItem> Requesters { get; set; }

        [StringLength(1000)]
        public string Notes { get; set; } = string.Empty;

        public PurchaseRequisitionStatus Status { get; set; }
        public IList<SelectListItem> Statuses { get; set; }

        public List<PurchaseRequisitionItemModel> PurchaseRequisitionItems { get; set; }

        public PurchaseRequisitionUpdateModel()
        {
            BusinessLocations = new List<SelectListItem>();
            Requesters = new List<SelectListItem>();

            // Approval, rejection and conversion are written by their own flows and
            // never by an edit.
            Statuses = Utility.ConvertEnumToSelectList<PurchaseRequisitionStatus>()
                .Where(x => x.Value == ((int)PurchaseRequisitionStatus.Draft).ToString()
                         || x.Value == ((int)PurchaseRequisitionStatus.Submitted).ToString())
                .ToList();

            PurchaseRequisitionItems = new List<PurchaseRequisitionItemModel>();
        }

        public void SetBusinessLocationValues(IList<BusinessLocation> businessLocations)
        {
            BusinessLocations = Utility.ConvertBusinessLocations(businessLocations);
        }

        public void SetRequesterValues(IList<SelectListItem> requesters)
        {
            Requesters = requesters;
        }
    }
}
