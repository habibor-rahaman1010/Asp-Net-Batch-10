using Assignment4.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment4.DatabaseSchemaClass
{
    public class AdmissionTest : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public double TestFees { get; set; }
    }
}
