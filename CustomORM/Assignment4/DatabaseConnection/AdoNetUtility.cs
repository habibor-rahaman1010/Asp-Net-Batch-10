using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment4.DbContextUtility
{
    public class AdoNetUtility
    {
        private readonly string _connectionString;

        public AdoNetUtility()
        {
            _connectionString = ConnectionInfo.ConnectionString;
        }

        public AdoNetUtility(string connectionString)
        {
            _connectionString = connectionString;
        }
    }
}
