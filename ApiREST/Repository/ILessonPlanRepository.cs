using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ApiREST
{
    public interface ILessonPlanRepository<T>
    {
        public Task<List<T>> GetLessonPlanByClassId(Guid uuid);
        public Task<List<T>> GetUniqueSubjectsByClassId(Guid uuid);
    }
}
