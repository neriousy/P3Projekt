using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ApiREST
{
    public class LessonPlanRepository : ILessonPlanRepository<Lesson_plan>
    {
        readonly MyContext _myContext;

        public LessonPlanRepository(MyContext context)
        {
            _myContext = context;
        }
        /// <summary>
        /// Pobierz plan lekcji danej klasy
        /// </summary>
        /// <param name="uuid">Uuid klasy ktorej plan lekcji chcemy pobrac</param>
        /// <returns>Przy poprawnym uuid zwraca plan lekcji klasy. W przeciwym wypadku zwraca null</returns>
        public Task<List<Lesson_plan>> GetLessonPlanByClassId(Guid uuid)
        {
            return _myContext.Lesson_plan.Where(l => l.Class_id == uuid).ToListAsync();
        }
        /// <summary>
        /// Pobiera unikalne przedmioty z planu lekcji danej klasy
        /// </summary>
        /// <param name="uuid">Uuid klasy ktorej chcemy pobrac unikalne przedmioty</param>
        /// <returns>Przy poprawnym uuid zwraca plan lekcji klasy. W przeciwym wypadku zwraca null</returns>
        public Task<List<Guid>> GetUniqueSubjectsByClassId(Guid uuid)
        {
            return _myContext.Lesson_plan.Where(l => l.Class_id == uuid).Select(l => l.Subject_id).Distinct().ToListAsync();
        }
        /// <summary>
        /// Pobierz plan lekcji z danego dnia
        /// </summary>
        /// <param name="uuid">Uuid klasy</param>
        /// <param name="day">Dzien (0 -4)</param>
        /// <returns>Zwraca plan lekcji z danego dnia posortowany wg godziny</returns>
        public Task<List<Lesson_plan>> GetLessonPlanForTheDay(Guid uuid, int day)
        {
            return _myContext.Lesson_plan.Where(l => (l.Class_id == uuid && l.Day == day)).OrderBy(l => l.Start_time).ToListAsync();
        }
    }
}
