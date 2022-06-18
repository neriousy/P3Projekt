using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ApiREST
{
    public interface ILessonsRepository<T>
    {
        public Task<List<T>> GetLesson (Guid uuid);
        public Task<Guid> InsertLesson(Lessons lesson);
        public Task<int> EditLesson(Guid uuid, Guid Subject_id, Guid Class_id, Guid Teacher_id, string Topic);
        public Task<int> DeleteLesson(Guid uuid);
    }
}
