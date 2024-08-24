using Assignment4.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Assignment4.Program;

namespace Assignment4.DatabaseSchemaClass
{
    public class Instructor : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Address PresentAddress { get; set; }
        public Address PermanentAddress { get; set; }
        public List<Phone> PhoneNumbers { get; set; }
    }
}
