using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ApiREST
{
    public class StudentsRepository : IStudentsRepository<Students>
    {
        readonly MyContext _myContext;

        public StudentsRepository(MyContext context)
        {
            _myContext = context;
        }

        public Task<Students> GetStudentAsync(string email, string password)
        {
            return _myContext.Students.Include(s => s.Parents_students).FirstOrDefaultAsync(s => (s.Email == email && s.Passwd == password));
        }

        public ICollection<Students> GetAllStudents()
        {
            return _myContext.Students.ToList();
        }

    }
}
