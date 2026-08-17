namespace DevSkill.Inventory.Domain.Dtos
{
    /// <summary>
    /// One supplier's offer as it appears in the comparison: the three things worth
    /// weighing, plus the flags that say where it wins.
    /// </summary>
    public class QuotationOfferDto
    {
        public Guid SupplierQuotationId { get; set; }
        public string QuotationNo { get; set; } = string.Empty;
        public string SupplierQuotationNo { get; set; } = string.Empty;
        public string SupplierName { get; set; } = string.Empty;

        public DateTime QuotationDate { get; set; }
        public DateTime ValidUntil { get; set; }
        public int DeliveryDays { get; set; }
        public string PaymentTermName { get; set; } = string.Empty;

        public decimal SubTotal { get; set; }
        public decimal DiscountTotal { get; set; }
        public decimal OtherCharges { get; set; }
        public decimal GrandTotal { get; set; }

        public string Status { get; set; } = string.Empty;

        /// <summary>Cheapest of the offers on this request.</summary>
        public bool IsLowestPrice { get; set; }

        /// <summary>Quickest of the offers on this request.</summary>
        public bool IsFastestDelivery { get; set; }

        /// <summary>Already past its validity date, so the price is no longer held.</summary>
        public bool IsExpired { get; set; }

        /// <summary>
        /// A supplier may quote only part of what was asked for. Saying so stops a
        /// short quote from looking cheap next to a complete one.
        /// </summary>
        public bool IsComplete { get; set; }

        public IList<QuotationOfferLineDto> Lines { get; set; } = new List<QuotationOfferLineDto>();
    }

    /// <summary>One product on one offer, as priced by that supplier.</summary>
    public class QuotationOfferLineDto
    {
        public Guid ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }

        /// <summary>Cheapest unit price for this product across every offer.</summary>
        public bool IsLowestUnitPrice { get; set; }
    }

    /// <summary>
    /// Every offer against one request for quotation, lined up so price, terms and
    /// delivery can be read across in one go.
    /// </summary>
    public class QuotationComparisonDto
    {
        public Guid RequestForQuotationId { get; set; }
        public string RfqNo { get; set; } = string.Empty;
        public string WarehouseName { get; set; } = string.Empty;
        public DateTime RfqDate { get; set; }
        public DateTime ResponseDeadline { get; set; }
        public string Status { get; set; } = string.Empty;
        public Guid? SelectedSupplierQuotationId { get; set; }

        /// <summary>The products asked for, which is what the offers are read against.</summary>
        public IList<QuotationRequestedLineDto> RequestedLines { get; set; }
            = new List<QuotationRequestedLineDto>();

        public IList<QuotationOfferDto> Offers { get; set; } = new List<QuotationOfferDto>();
    }

    /// <summary>One product the request asked to be quoted for.</summary>
    public class QuotationRequestedLineDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string UnitName { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
    }
}
