using Assignment4.DbContextUtility;
using Assignment4.Interface;
using System.Security.Principal;
using Assignment4.DatabaseSchemaClass;

namespace Assignment4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string connection = ConnectionInfo.ConnectionString;

            AdoNetUtility context = new AdoNetUtility(connection);

            Address presentAddress = new Address
            {
                Id = Guid.NewGuid(),
                Street = "123 Main St",
                City = "New York",
                Country = "USA"
            };

            Address permanentAddress = new Address
            {
                Id = Guid.NewGuid(),
                Street = "456 Elm St",
                City = "Boston",
                Country = "USA"
            };

            List<Phone> phoneNumbers = new List<Phone>
            {
                new Phone { Id = Guid.NewGuid(), Number = "1234567890", Extension = "101", CountryCode = "+1" },
                new Phone { Id = Guid.NewGuid(), Number = "0987654321", Extension = "102", CountryCode = "+1" }
            };


            Instructor teacher = new Instructor
            {
                Id = Guid.NewGuid(),
                Name = "John Doe",
                Email = "johndoe@example.com",
                PresentAddress = presentAddress,
                PermanentAddress = permanentAddress,
                PhoneNumbers = phoneNumbers
            };

            
            List<Session> sessions = new List<Session>
            {
                new Session { Id = Guid.NewGuid(), DurationInHour = 2, LearningObjective = "Introduction to C#" },
                new Session { Id = Guid.NewGuid(), DurationInHour = 3, LearningObjective = "Advanced C# Techniques" }
            };

   
            Topic topic1 = new Topic
            {
                Id = Guid.NewGuid(),
                Title = "C# Basics",
                Description = "An introduction to C# programming language.",
                Sessions = sessions
            };
 
            List<AdmissionTest> tests = new List<AdmissionTest>
            {
                new AdmissionTest
                {
                    Id = Guid.NewGuid(),
                    StartDateTime = new DateTime(2024, 9, 1, 9, 0, 0),
                    EndDateTime = new DateTime(2024, 9, 1, 11, 0, 0),
                    TestFees = 50.0
                },
                new AdmissionTest
                {
                    Id = Guid.NewGuid(),
                    StartDateTime = new DateTime(2024, 10, 1, 9, 0, 0),
                    EndDateTime = new DateTime(2024, 10, 1, 11, 0, 0),
                    TestFees = 50.0
                }
            };

            Course myCourse = new Course
            {
                Id = Guid.NewGuid(),
                Title = "C# Programming",
                Teacher = teacher,
                Topics = new List<Topic> { topic1 },
                Fees = 300.0,
                Tests = tests
            };


            /*var myOrm = new MyORM<Guid, Course>(connection);
            List<Course> courses = myOrm.GetAll();

            foreach (var course in courses)
            {
                Console.WriteLine($"Course Title: {course.Title}");

                if (course.Teacher != null)
                {
                    Console.WriteLine($"Teacher Name: {course.Teacher.Name}");

                    if (course.Teacher.PhoneNumbers != null)
                    {
                        foreach (var phone in course.Teacher.PhoneNumbers)
                        {
                            Console.WriteLine($"Phone: {phone.Number}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No phone numbers available for this teacher.");
                    }
                }
                else
                {
                    Console.WriteLine("Teacher information not available for this course.");
                }
            }*/


            var myOrm = new MyORM<Guid, Course>(connection);
            myOrm.Delete(new Guid("9106fa9b-bb19-4158-83b0-720c6b42b8c1"));

        }
    }
}
