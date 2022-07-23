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
        /// <summary>
        /// Logowanie do nauczyciela
        /// </summary>
        /// <param name="email">Email nauczyciela</param>
        /// <param name="password">Haslo nauczyciela</param>
        /// <returns>Zwraca obiekt nauczyciela albo null</returns>
        public Task<Teachers> GetTeacherAsync(string email, string password)
        {
            return _myContext.Teachers.FirstOrDefaultAsync(t => (t.Email == email && t.Passwd == password));
        }
        /// <summary>
        /// Zwraca obiekt nauczyciela po jego uuid
        /// </summary>
        /// <param name="uuid">Uuid nauczyciela</param>
        /// <returns>Zwraca obiekt nauczyciela albo null</returns>
        public Task<Teachers> GetTeacherNameByUuidAsync(Guid uuid)
        {
            return _myContext.Teachers.FirstOrDefaultAsync(t => t.Teacher_id == uuid);
        }
    }
}
