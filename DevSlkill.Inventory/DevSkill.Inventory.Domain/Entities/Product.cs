using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevSkill.Inventory.Domain.Entities
{
    public class Product : IEntity<Guid>
    {
        public Guid Entity { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty ;
        public decimal Price { get; set; }
        public decimal Ratings { get; set; }
        public Category ProductCategory { get; set; }

        public Product() 
        {
            ProductCategory = new Category();
        }
    }
}
