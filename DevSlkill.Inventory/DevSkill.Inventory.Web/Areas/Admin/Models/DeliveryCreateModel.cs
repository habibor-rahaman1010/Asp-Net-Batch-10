using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Infrastructure.RazorUtility;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    /// <summary>
    /// A shipment is raised against exactly one source document, which is either a
    /// proforma invoice or a confirmed sales order. The screen makes that a choice
    /// rather than two half filled fields, and the service checks it again.
    /// </summary>
    public class DeliveryCreateModel
    {
        [Display(Name = "Delivery No")]
        public string DeliveryNo { get; set; } = string.Empty;

        /// <summary>"Proforma" or "SalesOrder". Which of the two ids below is used.</summary>
        [Display(Name = "Raised Against")]
        public string SourceType { get; set; } = SalesOrderSource;

        public const string ProformaSource = "Proforma";
        public const string SalesOrderSource = "SalesOrder";

        [Display(Name = "Proforma Invoice")]
        public Guid? ProformaInvoiceId { get; set; }
        public IList<SelectListItem> ProformaInvoices { get; set; }

        [Display(Name = "Sales Order")]
        public Guid? SalesOrderId { get; set; }
        public IList<SelectListItem> SalesOrders { get; set; }

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
            SalesOrders = new List<SelectListItem>();
            DeliveryItems = new List<DeliveryItemModel>();
            DeliveryDate = DateTime.Today;
        }

        /// <summary>The id the chosen source actually points at, empty when nothing is picked.</summary>
        public Guid? SelectedSourceId => SourceType == ProformaSource ? ProformaInvoiceId : SalesOrderId;

        public void SetProformaInvoiceValues(IList<ProformaInvoice> proformaInvoices)
        {
            ProformaInvoices = Utility.ConvertProformaInvoices(proformaInvoices);
        }

        public void SetSalesOrderValues(IList<SalesOrder> salesOrders)
        {
            SalesOrders = Utility.ConvertSalesOrders(salesOrders);
        }
    }
}
