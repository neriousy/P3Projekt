using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ApiREST
{
    public class SubjectsRepository : ISubjectsRepository<Subjects>
    {
        readonly MyContext _myContext;

        public SubjectsRepository(MyContext context)
        {
            _myContext = context;
        }

        public Task<Subjects> GetSubjectAsync(Guid uuid)
        {
            return _myContext.Subjects.FirstOrDefaultAsync(s => s.Subject_id == uuid);
        }
    }
}
