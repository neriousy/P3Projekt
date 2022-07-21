using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ApiREST
{
    public class LessonPlanRepository : ILessonPlanRepository<Lesson_plan>
    {
        readonly MyContext _myContext;

        public LessonPlanRepository(MyContext context)
        {
            _myContext = context;
        }

        public Task<List<Lesson_plan>> GetLessonPlanByClassId(Guid uuid)
        {
            return _myContext.Lesson_plan.Where(l => l.Class_id == uuid).ToListAsync();
        }

        public Task<List<Guid>> GetUniqueSubjectsByClassId(Guid uuid)
        {
            return _myContext.Lesson_plan.Where(l => l.Class_id == uuid).Select(l => l.Subject_id).Distinct().ToListAsync();
        }
    }
}
