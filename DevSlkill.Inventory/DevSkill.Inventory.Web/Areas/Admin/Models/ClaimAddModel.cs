using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    public class ClaimAddModel
    {
        public Guid UserId { get; set; }
        public SelectList Users { get; set; }
        public string ClaimName { get; set; } = string.Empty;
        public string ClaimValue { get; set; } = string.Empty;

        public ClaimAddModel()
        {
            Users = new SelectList(Enumerable.Empty<SelectListItem>());
        }
    }
}
