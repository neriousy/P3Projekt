using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace ApiREST
{
    public class LessonsRepository : ILessonsRepository<Lessons>
    {
        readonly MyContext _myContext;

        public LessonsRepository(MyContext context)
        {
            _myContext = context;
        }
        /// <summary>
        /// Pobierz lekcje za pomoca jej uuid
        /// </summary>
        /// <param name="uuid">Uuid lekcji ktora chcemy pobraac</param>
        /// <returns>Przy poprawnym uuid zwraca lekcje. W przeciwym wypadku zwraca null</returns>
        public Task<List<Lessons>> GetLesson(Guid uuid)
        {
            return _myContext.Lessons.Where(l => l.Lesson_id == uuid).ToListAsync();
        }
        /// <summary>
        /// Wstaw lekcje
        /// </summary>
        /// <param name="lesson">Obiekt Lessons ktory chcemy wstawic</param>
        /// <returns>Przy poprawnym wstawieniu zwraca uuid lekcji</returns>
        public async Task<Guid> InsertLesson(Lessons lesson)
        {
            await _myContext.AddAsync(lesson);
            await _myContext.SaveChangesAsync();

            return lesson.Lesson_id;
        }
        /// <summary>
        /// Edytuj lekcje
        /// </summary>
        /// <param name="uuid">Uuid lekcji ktora chcemy zmienic</param>
        /// <param name="Subject_id">Id przedmiotu na ktory chcemy ta lekcje zmienic</param>
        /// <param name="Class_id">Id klasy na ktora chcemy zmienic</param>
        /// <param name="Teacher_id">Uuid nauczyciela ktory wstawil ta lekcje</param>
        /// <param name="Topic">Temat na ktory chcemy zmienic</param>
        /// <returns>Zwraca 0 gdy dana lekcja nie istnieje. Zwraca -1 gdy ta osoba nie ma praw do tej lekcji. Zwraca 1 gdy lekcja zostala zmieniona</returns>
        public async Task<int> EditLesson(Guid uuid, Guid Subject_id, Guid Class_id, Guid Teacher_id, string Topic)
        {
            Lessons lesson = await _myContext.Lessons.FindAsync(uuid);

            if (lesson == null)
            {
                return 0;
            }
            else if (lesson.Teacher_id != Teacher_id)
            {
                return -1;
            }

            lesson.Subject_id = Subject_id;
            lesson.Class_id = Class_id;
            lesson.Teacher_id = Teacher_id;
            lesson.Topic = Topic;

            await _myContext.SaveChangesAsync();

            return 1;

        }
        /// <summary>
        /// Usun lekcje
        /// </summary>
        /// <param name="uuid">Uuid lekcji ktora chcemy usunac</param>
        /// <returns>Zwraca 0 gdy dana lekcja nie istnieje. Zwraca 1 przy pomyslnym usunieciu</returns>
        public async Task<int> DeleteLesson(Guid uuid) 
        {
            Lessons lesson = await _myContext.Lessons.FindAsync(uuid);
            if(lesson == null)
            {
                return 0;
            }

            _myContext.Remove(lesson);
            await _myContext.SaveChangesAsync();

            return 1;
        }

    }
}
