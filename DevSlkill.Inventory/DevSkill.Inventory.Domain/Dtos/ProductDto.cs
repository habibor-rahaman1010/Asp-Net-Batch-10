using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevSkill.Inventory.Domain.Dtos
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string SKU {  get; set; } = string.Empty;    
        public int Price { get; set; }
        public int Ratings { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }
}
