using DevSkill.Inventory.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevSkill.Inventory.Infrastructure
{
    public class ApplicationTime : IApplicationTime
    {
        public DateTime GetCurrentDateTime()
        {
            return DateTime.Now;
        }
    }
}
