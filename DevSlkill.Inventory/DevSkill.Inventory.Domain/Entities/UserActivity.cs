namespace DevSkill.Inventory.Domain.Entities
{
    public class UserActivity : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime VisitDate { get; set; }
        public int PageVisited { get; set; }
        public string? ControllerName { get; set; }
        public string? ActionName { get; set; }
        public string? IpAddress { get; set; }
        public string? Browser { get; set; }
        public DateTime LastActivityTime { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime? LogoutTime { get; set; }
        public int ActionCount { get; set; }
    }
}