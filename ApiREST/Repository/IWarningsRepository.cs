using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiREST
{
    public interface IWarningsRepository<T>
    {
        public Task<List<T>> GetStudentWarningsAsync(Guid uuid);
    }
}
