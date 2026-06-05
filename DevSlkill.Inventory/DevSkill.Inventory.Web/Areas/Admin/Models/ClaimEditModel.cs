namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    public class ClaimEditModel
    {
        public Guid UserId { get; set; }
        public string OriginalClaimType { get; set; } = string.Empty;
        public string OriginalClaimValue { get; set; } = string.Empty;
        public string NewClaimType { get; set; } = string.Empty;
        public string NewClaimValue { get; set; } = string.Empty;
    }
}
