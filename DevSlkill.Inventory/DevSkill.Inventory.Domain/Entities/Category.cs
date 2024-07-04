using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevSkill.Inventory.Domain.Entities
{
    public class Category : IEntity<Guid>
    {
        public Guid Entity { get; set; }
        public string CategoryName{ get; set; } = string.Empty;
    }
}
