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
        /// <summary>
        /// Zaloguj sie do rodzica 
        /// </summary>
        /// <param name="email">Email rodzica</param>
        /// <param name="password">Haslo rodzica</param>
        /// <returns>Zwraca obiekt rodzica przy poprawnych danych. Zwraca null przy niepoprawnych danych</returns>
        public Task<Parents> GetParentAsync(string email, string password)
        {
            return _myContext.Parents.Include(p => p.Parents_students).FirstOrDefaultAsync(s => (s.Email == email && s.Passwd == password));
        }
    }
}
