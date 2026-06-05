namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    public class UpdateCategoryModel
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string CategoryCode { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
