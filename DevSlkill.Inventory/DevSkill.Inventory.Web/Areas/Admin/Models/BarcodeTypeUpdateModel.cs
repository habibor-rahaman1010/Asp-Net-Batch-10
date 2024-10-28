namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    public class BarcodeTypeUpdateModel
    {
        public Guid Id { get; set; }
        public string BarcodeTypeName { get; set; } = string.Empty;
        public string BarcodeTypeCode { get; set; } = string.Empty;
        public string BarcodeDescription { get; set; } = string.Empty;
    }
}
