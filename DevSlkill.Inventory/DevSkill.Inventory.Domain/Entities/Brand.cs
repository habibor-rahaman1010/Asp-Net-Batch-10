using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevSkill.Inventory.Domain.Entities
{
    public class Brand : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string BrandName { get; set; } = string.Empty;
        public string BandOrigin { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
