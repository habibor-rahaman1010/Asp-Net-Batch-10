namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    public class SubCategoryCreateModel
    {
        public Guid Id { get; set; }
        public string SubCategoryName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string CategoryCode { get; set; } = string.Empty;
    }
}
