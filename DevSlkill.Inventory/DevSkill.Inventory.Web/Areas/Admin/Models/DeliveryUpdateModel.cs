using System.ComponentModel.DataAnnotations;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    /// <summary>
    /// Only a draft delivery is editable, and even then the source document, the
    /// customer and the warehouse it was raised for stay as they are.
    /// </summary>
    public class DeliveryUpdateModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Delivery No")]
        public string DeliveryNo { get; set; } = string.Empty;

        /// <summary>"Proforma Invoice" or "Sales Order", so the screen says which it is.</summary>
        [Display(Name = "Raised Against")]
        public string SourceLabel { get; set; } = string.Empty;

        [Display(Name = "Source Document")]
        public string SourceNo { get; set; } = string.Empty;

        [Display(Name = "Customer")]
        public string CustomerName { get; set; } = string.Empty;

        [Display(Name = "Warehouse")]
        public string WarehouseName { get; set; } = string.Empty;

        [Display(Name = "Delivery Date")]
        public DateTime DeliveryDate { get; set; }

        [Display(Name = "Received By")]
        [StringLength(200)]
        public string ReceivedBy { get; set; } = string.Empty;

        [StringLength(1000)]
        public string Notes { get; set; } = string.Empty;

        public List<DeliveryItemModel> DeliveryItems { get; set; }

        public DeliveryUpdateModel()
        {
            DeliveryItems = new List<DeliveryItemModel>();
            DeliveryDate = DateTime.Today;
        }
    }
}
