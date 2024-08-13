using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment4.Interface
{
    public interface IMyORM<G, T> where T : class, IEntity<G>, new()
    {
        void Insert(T item);
        void Update(T item);
        void Delete(T item);
        void Delete(G id);
        T GetById(G id);
        IEnumerable<T> GetAll();
    }
}

