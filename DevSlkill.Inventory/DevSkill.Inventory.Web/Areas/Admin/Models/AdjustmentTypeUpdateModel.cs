namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    public class AdjustmentTypeUpdateModel
    {
        public Guid Id { get; set; }
        public string AdjustmentTypeName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Sign {  get; set; }
    }
}
