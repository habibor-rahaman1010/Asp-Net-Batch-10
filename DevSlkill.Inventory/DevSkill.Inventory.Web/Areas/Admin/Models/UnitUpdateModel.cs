using DevSkill.Inventory.Domain.Enums;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    public class UnitUpdateModel
    {
        public Guid Id { get; set; }
        public string UnitName { get; set; } = string.Empty;
        public string ShortName { get; set; } = string.Empty;
        public AllowDecimal AllowDecimal { get; set; }
    }
}
