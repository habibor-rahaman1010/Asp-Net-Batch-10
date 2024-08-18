using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    public class ProductModel : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Price { get; set; }
        public int Ratings { get; set; }
    }
}
