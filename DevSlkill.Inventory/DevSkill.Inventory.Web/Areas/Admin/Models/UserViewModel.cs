namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public bool EmailConfirmed { get; set; }
    }
}
