using System.Security.Claims;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    public class UserClaimsViewModel
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public IList<Claim> Claims { get; set; }

        public UserClaimsViewModel()
        {
            Claims = new List<Claim>();
        }
    }
}
