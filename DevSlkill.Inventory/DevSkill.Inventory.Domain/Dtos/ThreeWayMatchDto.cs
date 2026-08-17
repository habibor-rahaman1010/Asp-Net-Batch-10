namespace DevSkill.Inventory.Domain.Dtos
{
    /// <summary>
    /// The purchase order, the goods receipts and the purchase invoices of one order
    /// laid side by side, so a mismatch between what was ordered, what arrived and
    /// what is being billed is visible at a glance.
    /// </summary>
    public class ThreeWayMatchDto
    {
        public Guid PurchaseOrderId { get; set; }
        public string PurchaseOrderNo { get; set; } = string.Empty;
        public string SupplierName { get; set; } = string.Empty;
        public string WarehouseName { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = string.Empty;

        public decimal OrderedAmount { get; set; }
        public decimal ReceivedAmount { get; set; }
        public decimal InvoicedAmount { get; set; }

        /// <summary>True when every line matches on both quantity and amount.</summary>
        public bool IsMatched { get; set; }

        public IList<ThreeWayMatchLineDto> Lines { get; set; } = new List<ThreeWayMatchLineDto>();

        /// <summary>Plain sentences naming what does not line up.</summary>
        public IList<string> Mismatches { get; set; } = new List<string>();
    }

    public class ThreeWayMatchLineDto
    {
        public string ProductName { get; set; } = string.Empty;
        public string UnitName { get; set; } = string.Empty;

        public decimal OrderedQuantity { get; set; }
        public decimal ReceivedQuantity { get; set; }
        public decimal InvoicedQuantity { get; set; }

        public decimal OrderedAmount { get; set; }
        public decimal ReceivedAmount { get; set; }
        public decimal InvoicedAmount { get; set; }

        /// <summary>Received less invoiced. Positive means goods are not billed yet.</summary>
        public decimal QuantityVariance { get; set; }

        /// <summary>Value of what arrived less what is billed for it.</summary>
        public decimal AmountVariance { get; set; }

        public bool IsMatched { get; set; }
    }
}
