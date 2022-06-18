using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ApiREST
{
    public class ParentsRepository : IParentsRepository<Parents>
    {
        readonly MyContext _myContext;

        public ParentsRepository(MyContext context)
        {
            _myContext = context;
        }
        public Task<Parents> GetParentAsync(string email, string password)
        {
            return _myContext.Parents.Include(p => p.Parents_students).FirstOrDefaultAsync(s => (s.Email == email && s.Passwd == password));
        }
    }
}
