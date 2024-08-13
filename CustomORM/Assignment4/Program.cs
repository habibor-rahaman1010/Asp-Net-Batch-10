using Assignment4.DbContextUtility;

namespace Assignment4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string connection = ConnectionInfo.ConnectionString;

            ApplicationDbContext context = new ApplicationDbContext(connection);
        }
    }
}
