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
        /// <summary>
        /// Zaloguj sie do studenta
        /// </summary>
        /// <param name="email">Email studenta</param>
        /// <param name="password">Haslo studenta</param>
        /// <returns>Zwraca obiekt studenta przy poprawnych danych. Zwraca null przy niepoprawnych danych</returns>
        public Task<Students> GetStudentAsync(string email, string password)
        {
            return _myContext.Students.Include(s => s.Parents_students).FirstOrDefaultAsync(s => (s.Email == email && s.Passwd == password));
        }
        /// <summary>
        /// Pobierz liste uczniow danej klasy
        /// </summary>
        /// <param name="uuid">Uuid klasy z ktorej chcemy pobrac liste uczniow</param>
        /// <returns>Zwraca liste uczniow danej klasy lub null przy niepoprawnych uuid</returns>
        public Task<List<Students>> GetStudentsByClassAsync(Guid uuid)
        {
            return _myContext.Students.Where(s => s.Class_id == uuid).ToListAsync();
        }
        /// <summary>
        /// Pobierz studenta za pomoca jego uuid
        /// </summary>
        /// <param name="uuid">Uuid studenta ktorego chcemy pobrac</param>
        /// <returns>Zwraca obiekt studenta lub null przy niepoprawnym uuid</returns>
        public Task<Students> GetStudentByUuidAsync(Guid uuid)
        {
            return _myContext.Students.Include(s => s.Parents_students).FirstOrDefaultAsync(s => s.Student_id == uuid);
        }


    }
}
