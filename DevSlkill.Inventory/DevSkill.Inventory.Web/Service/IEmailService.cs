namespace DevSkill.Inventory.Web.Service
{
    public interface IEmailService
    {
        public string SendEmail(string email, string subject, string body);
    }
}
