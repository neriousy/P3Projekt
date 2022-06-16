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

        public Students GetStudent(string email, string password)
        {
            return _myContext.Studenci.Include(s => s.Parents_students).FirstOrDefault(s => (s.email == email && s.passwd == password));
        }

        public ICollection<Students> GetAllStudents()
        {
            return _myContext.Studenci.ToList();
        }

    }
}
