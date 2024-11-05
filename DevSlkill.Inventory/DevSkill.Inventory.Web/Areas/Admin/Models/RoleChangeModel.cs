using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    public class RoleChangeModel
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        public SelectList Users { get; set; }
        public SelectList Roles { get; set; }

        public RoleChangeModel()
        {
            Users = new SelectList(Enumerable.Empty<SelectListItem>());
            Roles = new SelectList(Enumerable.Empty<SelectListItem>());
        }
    }
}
