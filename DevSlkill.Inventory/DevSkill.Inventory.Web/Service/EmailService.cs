using System.Text;

namespace DevSkill.Inventory.Web.Service
{
    public class EmailService : IEmailService
    {
        public string SendEmail(string email, string subject, string body)
        {
            StringBuilder response = new StringBuilder();
            response.Append("Email send properly to -> ");
            response.Append($"{email}");
            return response.ToString();
        }
    }
}
