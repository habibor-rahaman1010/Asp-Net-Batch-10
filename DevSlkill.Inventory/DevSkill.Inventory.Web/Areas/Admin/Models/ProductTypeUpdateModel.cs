namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    public class ProductTypeUpdateModel
    {
        public Guid Id { get; set; }
        public string ProductTypeName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ProductTypeCode { get; set; } = string.Empty;
    }
}
