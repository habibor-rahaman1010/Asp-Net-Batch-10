namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    public class ApplicableTaxUpdateModel
    {
        public Guid Id { get; set; }
        public string ApplicableTaxName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal TaxRate { get; set; }
    }
}
