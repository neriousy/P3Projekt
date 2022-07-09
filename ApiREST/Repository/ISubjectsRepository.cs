using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiREST
{
    public interface ISubjectsRepository<T>
    {
        public Task<T> GetSubjectAsync(Guid uuid);
    }
}
