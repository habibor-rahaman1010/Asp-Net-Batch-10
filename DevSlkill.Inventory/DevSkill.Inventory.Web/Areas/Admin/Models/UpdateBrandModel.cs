namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    public class UpdateBrandModel
    {
        public Guid Id { get; set; }
        public string BrandName { get; set; } = string.Empty;
        public string BandOrigin { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
