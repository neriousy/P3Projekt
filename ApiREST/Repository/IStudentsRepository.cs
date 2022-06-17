using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiREST
{
    public interface IStudentsRepository<T>
    {
        public Task<T> GetStudentAsync(string email, string password);
        public ICollection<T> GetAllStudents();

        
    }
}
