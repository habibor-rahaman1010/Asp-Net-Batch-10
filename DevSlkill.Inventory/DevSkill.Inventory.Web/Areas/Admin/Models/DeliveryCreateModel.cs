using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Infrastructure.RazorUtility;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    public class DeliveryCreateModel
    {
        [Display(Name = "Delivery No")]
        public string DeliveryNo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Proforma invoice is required.")]
        [Display(Name = "Proforma Invoice")]
        public Guid ProformaInvoiceId { get; set; }
        public IList<SelectListItem> ProformaInvoices { get; set; }

        [Display(Name = "Delivery Date")]
        public DateTime DeliveryDate { get; set; }

        [Display(Name = "Received By")]
        [StringLength(200)]
        public string ReceivedBy { get; set; } = string.Empty;

        [StringLength(1000)]
        public string Notes { get; set; } = string.Empty;

        /// <summary>
        /// Ticking this hands the goods over straight away, which is the moment stock
        /// leaves the warehouse. Left unticked the delivery is only planned.
        /// </summary>
        [Display(Name = "Confirm and release the stock now")]
        public bool ConfirmImmediately { get; set; }

        public List<DeliveryItemModel> DeliveryItems { get; set; }

        public DeliveryCreateModel()
        {
            ProformaInvoices = new List<SelectListItem>();
            DeliveryItems = new List<DeliveryItemModel>();
            DeliveryDate = DateTime.Today;
        }

        public void SetProformaInvoiceValues(IList<ProformaInvoice> proformaInvoices)
        {
            ProformaInvoices = Utility.ConvertProformaInvoices(proformaInvoices);
        }
    }
}
