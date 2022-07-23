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
        /// <summary>
        /// Pobierz liste ocen studenta
        /// </summary>
        /// <param name="uuid">Uuid studenta ktorego oceny chcemy pobrac</param>
        /// <returns>Zwraca liste ocen danego studenta lub null</returns>
        public Task<List<Grades>> GetGradesStudent(Guid uuid)
        {
            return _myContext.Grades.Where(g => g.Student_id == uuid).ToListAsync();
        }
        /// <summary>
        /// Pobierz liste ocen z danego przedmiotu
        /// </summary>
        /// <param name="uuid">Uuid przedmiotu z ktorego oceny chcemy pobrac</param>
        /// <returns>Zwraca liste ocen z danego przedmiotu lub null</returns>
        public Task<List<Grades>> GetGradesSubject(Guid uuid)
        {
            return _myContext.Grades.Where(g => g.Subject_id == uuid).ToListAsync();
        }

        /// <summary>
        /// Wstaw ocene
        /// </summary>
        /// <param name="grade">Parametrem jest obiekt grade</param>
        /// <returns>Przy pomyslnym wstawieniu zwraca uuid oceny. Gdy sie nie uda zwraca null</returns>
        public async Task<Guid> InsertGrade(Grades grade)
        {
            grade.Date = DateTime.Now;
            await _myContext.AddAsync(grade);
            await _myContext.SaveChangesAsync();

            return grade.Grade_id;
        }
        /// <summary>
        /// Edytuj ocene
        /// </summary>
        /// <param name="uuid">Uuid oceny ktora chcemy zmienic</param>
        /// <param name="Grade">Ocena na ktora chcemy zmienic</param>
        /// <param name="Weight">Waga na ktora chcemy zmienic</param>
        /// <param name="desc">Opis na ktory chcemy zmienic</param>
        /// <param name="Teacher_id">Uuid nauczyciela ktory wystawil ta ocene</param>
        /// <returns>Zwraca 0 gdy dany uuid oceny nie istnieje. Zwraca -1 gdy ta osoba nie ma praw do oceny (uuid osoby wystawiajacej != zmieniajacej ocene. Zwraca 1 gdy ocena zostala zmieniona</returns>
        public async Task<int> EditGrade(Guid uuid, String Grade, int Weight, String desc, Guid Teacher_id)
        {
            Grades grade = await _myContext.Grades.FindAsync(uuid);
            if(grade == null)
            {
                return 0;
            }
            if(grade.Teacher_id != Teacher_id)
            {
                return -1;
            }
            grade.Grade = Grade;
            grade.Weight = Weight;
            grade.Desc = desc;

            await _myContext.SaveChangesAsync();

            return 1;
        }
        /// <summary>
        /// Usun ocene
        /// </summary>
        /// <param name="uuid">Uuid oceny ktora chcemy usunac</param>
        /// <returns>Zwraca 0 gdy dana ocena nie istniała. Zwraca 1 przy pomyslnym usunieciu</returns>
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
