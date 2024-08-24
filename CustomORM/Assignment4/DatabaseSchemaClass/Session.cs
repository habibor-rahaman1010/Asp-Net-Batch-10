using Assignment4.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment4.DatabaseSchemaClass
{
    public class Session : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public int DurationInHour { get; set; }
        public string LearningObjective { get; set; }
    }
}
