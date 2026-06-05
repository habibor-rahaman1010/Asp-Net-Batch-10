using DevSkill.Inventory.Domain.Enums;

namespace DevSkill.Inventory.Domain.Entities
{
    public class Unit : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string UnitName { get; set; } = string.Empty;
        public string ShortName { get; set; } = string.Empty;
        public AllowDecimal AllowDecimal { get; set; }
    }
}
