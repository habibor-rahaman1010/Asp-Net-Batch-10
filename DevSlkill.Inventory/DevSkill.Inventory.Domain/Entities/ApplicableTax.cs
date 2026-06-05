using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevSkill.Inventory.Domain.Entities
{
    public class ApplicableTax : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string ApplicableTaxName { get; set; } = string.Empty; 
        public string Description { get; set; } = string.Empty;
        public decimal TaxRate { get; set; }
    }
}
