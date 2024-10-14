using DevSkill.Inventory.Domain.Entities;
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
        public string ProductTypeId { get; set; } = string.Empty;
        public string CategoryId { get; set; } = string.Empty;
        public string BrandId { get; set; } = string.Empty;
        public string UnitId { get; set; } = string.Empty;
        public string BusinessLocationId {  get; set; } = string.Empty; 
        public string SellingPriceTaxId {  get; set; } = string.Empty;
        public string ApplicableTaxId {  get; set; } = string.Empty;
        public ProductStatus Status { get; set; }
    }
}
