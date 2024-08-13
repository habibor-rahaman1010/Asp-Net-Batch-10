using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment4.DbContextUtility
{
    public class ApplicationDbContext
    {
        private readonly string _connectionString;

        public ApplicationDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }
        public ApplicationDbContext()
        {
            _connectionString = ConnectionInfo.ConnectionString;
        }

       
    }
}
