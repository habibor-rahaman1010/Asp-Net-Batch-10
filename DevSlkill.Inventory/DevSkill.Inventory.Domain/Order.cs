using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// This class use for users table column sort. Define the Order class to match the incoming sorting parameters.
namespace DevSkill.Inventory.Domain
{
    public class Order
    {
        public int Column { get; set; }
        public string Direction { get; set; } = string.Empty;
    }
}
