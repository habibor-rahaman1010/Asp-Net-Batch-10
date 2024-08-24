using Assignment4.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment4.DatabaseSchemaClass
{
    public class Phone : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public string Extension { get; set; }
        public string CountryCode { get; set; }
    }
}
