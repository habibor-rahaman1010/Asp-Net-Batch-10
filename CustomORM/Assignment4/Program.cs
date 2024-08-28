using Assignment4.DbContextUtility;
using Assignment4.DatabaseSchemaClass;


namespace Assignment4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string connection = ConnectionInfo.ConnectionString;

            AdoNetUtility context = new AdoNetUtility(connection);

            var orm = new MyORM<Guid, Course>(connection);
            IList<Course> courses = orm.GetAll();
        
        }
    }
}
