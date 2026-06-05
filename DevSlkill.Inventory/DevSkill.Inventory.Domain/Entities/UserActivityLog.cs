namespace DevSkill.Inventory.Domain.Entities
{
    public class UserActivityLog : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string? ControllerName { get; set; }
        public string? ActionName { get; set; }
        public string? Url { get; set; }
        public string? HttpMethod { get; set; }
        public DateTime VisitTime { get; set; }
        public string? IpAddress { get; set; }
        public string? Browser { get; set; }
    }
}