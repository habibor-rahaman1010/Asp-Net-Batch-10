namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    public class SellingPriceTaxUpdateModel
    {
        public Guid Id { get; set; }
        public string SellingPriceTaxName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal TaxRate { get; set; }
    }
}
