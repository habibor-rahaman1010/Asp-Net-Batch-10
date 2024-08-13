using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment4.Interface
{
    public interface IEntity<G>
    {
        G Id { get; set; }
    }
}
