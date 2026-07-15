using Microsoft.AspNetCore.Identity;

namespace DevSkill.Inventory.Infrastructure.InventoryIdentity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;       
    }
}