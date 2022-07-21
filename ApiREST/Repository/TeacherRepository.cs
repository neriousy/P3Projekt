using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ApiREST
{
    public class TeacherRepository : ITeacherRepository<Teachers>
    {
        readonly MyContext _myContext;

        public TeacherRepository(MyContext context)
        {
            _myContext = context;
        }

        public Task<Teachers> GetTeacherAsync(string email, string password)
        {
            return _myContext.Teachers.FirstOrDefaultAsync(t => (t.Email == email && t.Passwd == password));
        }

        public Task<Teachers> GetTeacherNameByUuidAsync(Guid uuid)
        {
            return _myContext.Teachers.FirstOrDefaultAsync(t => t.Teacher_id == uuid);
        }
    }
}
