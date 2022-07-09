using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ApiREST
{
    public interface IGradesRepository<T>
    {
        public Task<List<T>> GetGradesStudent(Guid uuid);
        public Task<List<T>> GetGradesSubject(Guid uuid);
        public Task<Guid> InsertGrade(Grades grade);
        public Task<int> EditGrade(Guid uuid, String Grade, int Weight, String desc, Guid Teacher_id);
        public Task<int> DeleteGrade(Guid uuid);


    }
}
