using Assignment4.DbContextUtility;
using Assignment4.Interface;
using System.Security.Principal;
using Assignment4.DatabaseSchemaClass;
using static System.Net.Mime.MediaTypeNames;

namespace Assignment4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string connection = ConnectionInfo.ConnectionString;

            AdoNetUtility context = new AdoNetUtility(connection);

            var ORM = new MyORM<Guid, Course>(connection); 

        }
    }
}
