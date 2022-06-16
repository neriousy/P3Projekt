using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiREST
{
    public interface IGradesRepository<T>
    {
        public ICollection<T> GetGrades(Guid uuid);
        public ICollection<T> GetAllGrades();
    }
}
