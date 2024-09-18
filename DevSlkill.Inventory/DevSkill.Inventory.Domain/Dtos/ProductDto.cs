using DevSkill.Inventory.Domain.Enums;
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
        public string LocationName { get; set; } = string.Empty ;
        public string Description { get; set; } = string.Empty;
        public int Price { get; set; } 
        public string SKU {  get; set; } = string.Empty;    
        public int Ratings { get; set; }
        public string CategoryName { get; set; } = string.Empty;  
        public ProductType ProductType { get; set; }
        public string BrandName { get; set; } = string.Empty;
        public Tax Tax { get; set; }
    }
}
