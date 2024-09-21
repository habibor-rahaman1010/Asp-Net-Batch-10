using DevSkill.Inventory.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevSkill.Inventory.Domain.Dtos
{
    public class ProductSearchDto
    {
        public string ProductName { get; set; } = string.Empty;
        public ProductType ProductType { get; set; }
        public string CategoryId { get; set; } = string.Empty;
    }
}
