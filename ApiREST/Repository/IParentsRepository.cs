using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiREST
{
    public interface IParentsRepository<T>
    {
        public Task<T> GetParentAsync(string email, string password);
    }
}
