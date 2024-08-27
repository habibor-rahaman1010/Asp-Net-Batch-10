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

            Course updatedCourse = new Course
            {
                Title = "Updated Course Title",
            };

            var myOrm = new MyORM<Guid, Course>(connection);
            updatedCourse.Title = "Demo";
            myOrm.Update(updatedCourse);

            


        }
    }
}
