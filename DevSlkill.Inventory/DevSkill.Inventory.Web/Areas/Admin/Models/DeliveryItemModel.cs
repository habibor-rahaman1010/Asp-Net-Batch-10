namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    /// <summary>
    /// One line of the delivery screen. Only <see cref="ProductId"/> and
    /// <see cref="DeliveredQuantity"/> are posted back; everything else is shown so the
    /// user can see what was sold and what is left, and is read again on the server.
    /// </summary>
    public class DeliveryItemModel
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string UnitName { get; set; } = string.Empty;

        public decimal OrderedQuantity { get; set; }
        public decimal AlreadyDeliveredQuantity { get; set; }
        public decimal RemainingQuantity { get; set; }
        public int AvailableStock { get; set; }

        public decimal DeliveredQuantity { get; set; }
    }
}
