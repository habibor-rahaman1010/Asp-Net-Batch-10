namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    public class WarrantyUpdateModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string WarrantyDuration { get; set; } = string.Empty;
    }
}
