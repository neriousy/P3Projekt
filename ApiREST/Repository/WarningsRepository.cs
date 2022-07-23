using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ApiREST
{
    public class WarningsRepository : IWarningsRepository<Warnings>
    {
        readonly MyContext _myContext;

        public WarningsRepository(MyContext context)
        {
            _myContext = context;
        }
        
        /// <summary>
        /// Pobierz uwagi studenta przy pomocy jego uuid
        /// </summary>
        /// <param name="uuid">Uuid studenta</param>
        /// <returns>Zwraca liste uwag studenta lub null</returns>
        public Task<List<Warnings>> GetStudentWarningsAsync(Guid uuid)
        {
            return _myContext.Warnings.Where(w => w.Student_id == uuid).ToListAsync();
        }
    }
}
