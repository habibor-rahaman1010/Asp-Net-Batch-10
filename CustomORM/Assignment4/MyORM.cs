using System.Data;
using System.Reflection;
using System.Text;

using Assignment4.Interface;
using Microsoft.Data.SqlClient;

namespace Assignment4
{
    public class MyORM<G, T> : IMyORM<G, T> where T : class, IEntity<G>, new()
    {
        private readonly string _connectionString;

        public MyORM(string connectionString)
        {
            _connectionString = connectionString;
        }


        // I'm try but i did not complete the Insert mehtod so i pass this..
        public void Insert(T item)
        {
            throw new NotImplementedException();
        }


        // I'm try but i did not complete the GetAll mehtod so i pass this..
        public IList<T> GetAll()
        {
            throw new NotImplementedException();
        }


        // I'm try but i did not complete the GetById mehtod so i pass this..
        public T GetById(G id)
        {

            throw new NotImplementedException();
        }


        // I'm try but i did not complete the Update mehtod so i pass this..
        public void Update(T item)
        {
            throw new NotImplementedException();
        }


        // I'm try but i did not complete the delete mehtod so i pass this..
        public void Delete(T item)
        {
            throw new NotImplementedException();
        }


        // I'm try but i did not complete the delete mehtod so i pass this..
        public void Delete(G id)
        {
            throw new NotImplementedException ();
        }
    }
}