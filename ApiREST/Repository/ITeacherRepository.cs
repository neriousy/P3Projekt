using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiREST
{
    public interface ITeacherRepository<T>
    {
        public Task<T> GetTeacherAsync(string email, string password);

    }
}
