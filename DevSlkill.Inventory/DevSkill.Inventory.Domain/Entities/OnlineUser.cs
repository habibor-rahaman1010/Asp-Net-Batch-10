
namespace DevSkill.Inventory.Domain.Entities
{
    public class OnlineUser : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public DateTime LastActivity { get; set; }
    }
}
