using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevSkill.Inventory.Domain.Entities.StockAdjustmentEntites
{
    public class StockAdjustment : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public DateTime AdjustmentDate { get; set; }
        public string ReferenceNo { get; set; } = string.Empty;
        public int AdjustmentQuantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalAmountRecover { get; set; }
        public string Reason { get; set; } = string.Empty;
        public string AddedBy { get; set; } = string.Empty;

        // Foreign Key for BusinessLocation
        public Guid BusinessLocationId { get; set; }
        public BusinessLocation BusinessLocation { get; set; }

        // Foreign Key for AdjustmentType
        public Guid AdjustmentTypeId { get; set; }
        public AdjustmentType AdjustmentType { get; set; }

        // Foreign Key for Product
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public StockAdjustment()
        {
            BusinessLocation = new BusinessLocation();
            AdjustmentType = new AdjustmentType();
            Product = new Product();
        }
    }
}
