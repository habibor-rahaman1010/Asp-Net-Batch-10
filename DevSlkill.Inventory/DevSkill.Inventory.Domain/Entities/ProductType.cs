using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevSkill.Inventory.Domain.Entities
{
    public class ProductType : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string ProductTypeName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ProductTypeCode { get; set; } = string.Empty;
    }
}
