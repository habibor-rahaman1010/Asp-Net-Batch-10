using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevSkill.Inventory.Domain.Entities
{
    public class BarcodeType : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string BarcodeTypeName { get; set; } = string.Empty;
        public string BarcodeTypeCode {  get; set; } = string.Empty;
        public string BarcodeDescription { get; set; } = string.Empty;
    }
}
