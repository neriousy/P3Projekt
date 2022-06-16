using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiREST
{
    public class GradesRepository : IGradesRepository<Grades>
    {
        readonly MyContext _myContext;

        public GradesRepository(MyContext context)
        {
            _myContext = context;
        }

        public ICollection<Grades> GetGrades(Guid uuid)
        {
            Students student = _myContext.Studenci.FirstOrDefault(s => s.student_id == uuid);
            return student?.Grades.ToList();
        }
            

        public ICollection<Grades> GetAllGrades()
        {
            return _myContext.Grades.ToList();
        }
    }
}
