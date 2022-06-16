using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiREST
{
    public interface IParentsRepository<T>
    {
        public ICollection<T> GetAllParents();
        public T GetParent(string email, string password);
    }
}
