using Assignment4.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Assignment4.Program;

namespace Assignment4.DatabaseSchemaClass
{
    public class Topic : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Session> Sessions { get; set; }
    }
}
