using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace ApiREST
{
    public class GradesRepository : IGradesRepository<Grades>
    {
        readonly MyContext _myContext;

        public GradesRepository(MyContext context)
        {
            _myContext = context;
        }

        public Task<List<Grades>> GetGrades(Guid uuid)
        {
            return _myContext.Grades.Where(g => g.Student_id == uuid).ToListAsync();
        }


        public async Task<Guid> InsertGrade(Grades grade)
        {
            grade.Date = DateTime.Now;
            await _myContext.AddAsync(grade);
            await _myContext.SaveChangesAsync();

            return grade.Grade_id;
        }

        public async Task<int> EditGrade(Guid uuid, String Grade, int Weight, String desc)
        {
            Grades grade = await _myContext.Grades.FindAsync(uuid);
            if(grade == null)
            {
                return 0;
            }
            grade.Grade = Grade;
            grade.Weight = Weight;
            grade.Desc = desc;

            await _myContext.SaveChangesAsync();

            return 1;
        }

        public async Task<int> DeleteGrade(Guid uuid)
        {
            Grades grade = await _myContext.Grades.FindAsync(uuid);
            if(grade == null)
            {
                return 0;
            }

            _myContext.Remove(grade);
            await _myContext.SaveChangesAsync();

            return 1;
        }
    }
}
