using DevSkill.Inventory.Domain;

namespace DevSkill.Inventory.Infrastructure
{
    public class ApplicationTime : IApplicationTime
    {
        public DateTime GetCurrentDateTime()
        {
            return DateTime.Now;
        }
    }
}
