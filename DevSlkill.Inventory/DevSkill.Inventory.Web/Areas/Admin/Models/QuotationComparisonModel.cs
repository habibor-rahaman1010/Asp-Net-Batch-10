using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using DevSkill.Inventory.Infrastructure.RazorUtility;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    public class QuotationComparisonModel
    {
        [Display(Name = "Request For Quotation")]
        public Guid? RequestForQuotationId { get; set; }
        public IList<SelectListItem> RequestForQuotations { get; set; }

        /// <summary>Null until a request has actually been chosen.</summary>
        public QuotationComparisonDto? Comparison { get; set; }

        public QuotationComparisonModel()
        {
            RequestForQuotations = new List<SelectListItem>();
        }

        public void SetRequestForQuotationValues(IList<RequestForQuotation> requests)
        {
            RequestForQuotations = Utility.ConvertRequestForQuotations(requests);
        }
    }
}
