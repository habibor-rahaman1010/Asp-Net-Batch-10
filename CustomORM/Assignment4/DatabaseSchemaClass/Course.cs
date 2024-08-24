using Assignment4.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Assignment4.Program;

namespace Assignment4.DatabaseSchemaClass
{
    public class Course : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Instructor Teacher { get; set; }
        public List<Topic> Topics { get; set; }
        public double Fees { get; set; }
        public List<AdmissionTest> Tests { get; set; }
    }
}
