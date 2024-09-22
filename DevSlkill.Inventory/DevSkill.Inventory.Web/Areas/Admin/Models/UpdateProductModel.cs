namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    public class UpdateProductModel
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
        public double Ratings { get; set; }
    }
}
