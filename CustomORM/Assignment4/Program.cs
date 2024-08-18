using Assignment4.DbContextUtility;
using Assignment4.Interface;

namespace Assignment4
{
    public class Program
    {
        public class Phone : IEntity<Guid>
        {
            public Guid Id { get; set; }
            public string Number { get; set; }
            public string Type { get; set; }
        }

        public static void Main(string[] args)
        {
            string connection = ConnectionInfo.ConnectionString;

            AdoNetUtility context = new AdoNetUtility(connection);


            // Create instances of MyORM for each entity type
            var instructorOrm = new MyORM<Guid, Phone>(connection);



            var newPhones = new Phone { Id = Guid.NewGuid(), Number = "555-1234", Type = "Mobile" };



            //instructorOrm.Insert(newPhones);
            var data = instructorOrm.GetAll();
            Console.WriteLine(data[0].Id);

        }
    }
}
