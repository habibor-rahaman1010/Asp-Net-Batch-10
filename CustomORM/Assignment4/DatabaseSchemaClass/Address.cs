using Assignment4.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment4.DatabaseSchemaClass
{
    public class Address : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
