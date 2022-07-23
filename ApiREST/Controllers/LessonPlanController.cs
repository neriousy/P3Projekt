using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonPlanController : ControllerBase
    {
        private readonly ILessonPlanRepository<Lesson_plan> _lessonPlanRepository;

        public LessonPlanController(ILessonPlanRepository<Lesson_plan> lessonPlanRepository)
        {
            _lessonPlanRepository = lessonPlanRepository;
        }
        /// <summary>
        /// Pobierz plan lekcji klasy
        /// </summary>
        /// <param name="uuid">Uuid klasy</param>
        /// <returns>Zwraca plan lekcji klasy  lub kod 404 gdy dane uuid jest niepoprawne</returns>
        [HttpPost]
        [Route("GetLessonPlanByClassId")]
        public async Task<IActionResult> GetLessonPlanByClassId([FromForm] Guid uuid)
        {
            ICollection<Lesson_plan> resp = await _lessonPlanRepository.GetLessonPlanByClassId(uuid);

            if(resp == null)
            {
                return NotFound(resp);
            }

            return Ok(resp);
        }
        /// <summary>
        /// Pobierz przedmioty na ktore uczeszcza dana klasaa
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns>Zwraca przedmioty na ktore uczeszcza klasa lub kod 404 gdy dane uuid jest niepoprawne</returns>
        [HttpPost]
        [Route("GetUniqueSubjectsByClassId")]
        public async Task<IActionResult> GetUniqueSubjectsByClassId([FromForm] Guid uuid)
        {
            ICollection<Guid> resp = await _lessonPlanRepository.GetUniqueSubjectsByClassId(uuid);

            if(resp == null)
            {
                return NotFound(resp);
            }

            return Ok(resp);
        }
        /// <summary>
        /// Pobierz plan lekcji danego dnia posortowane wg. godziny 
        /// </summary>
        /// <param name="uuid">Uuid klasy</param>
        /// <param name="day">Dzien (0 - 4) </param>
        /// <returns>Zwraca plan lekcji danego dnia posortowane wg godziny(kod 200) lub kod 404 przy niepoprawnych danych</returns>
        [HttpPost]
        [Route("GetLessonPlanForTheDay")]
        public async Task<IActionResult> GetLessonPlanForTheDay([FromForm] Guid uuid, [FromForm] int day)
        {
            ICollection<Lesson_plan> resp = await _lessonPlanRepository.GetLessonPlanForTheDay(uuid, day);

            if (resp == null)
            {
                return NotFound(resp);
            }

            return Ok(resp);
        }
    }
}
