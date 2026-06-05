using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevSkill.Inventory.Domain.Entities.StockAdjustmentEntites
{
    public class AdjustmentType : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string AdjustmentTypeName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
