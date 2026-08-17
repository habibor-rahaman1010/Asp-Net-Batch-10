namespace DevSkill.Inventory.Domain.Dtos
{
    /// <summary>
    /// One approved requisition line seen from the purchase order side: what was
    /// asked for, what has already been ordered and what may still be ordered now.
    /// </summary>
    public class ConvertibleRequisitionLineDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string UnitName { get; set; } = string.Empty;

        public decimal RequestedQuantity { get; set; }
        public decimal OrderedQuantity { get; set; }

        /// <summary>Highest quantity a new purchase order may take from this line.</summary>
        public decimal RemainingQuantity { get; set; }

        /// <summary>What the requester expected it to cost, offered as a starting price.</summary>
        public decimal EstimatedUnitPrice { get; set; }
    }
}
