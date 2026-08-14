using System.ComponentModel.DataAnnotations;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    /// <summary>
    /// Only a draft delivery is editable, and even then the proforma invoice, the
    /// customer and the warehouse it was raised for stay as they are.
    /// </summary>
    public class DeliveryUpdateModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Delivery No")]
        public string DeliveryNo { get; set; } = string.Empty;

        [Display(Name = "Proforma Invoice")]
        public string ProformaNo { get; set; } = string.Empty;

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
