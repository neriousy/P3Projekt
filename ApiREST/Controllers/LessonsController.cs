using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ApiREST;

namespace ApiREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonsController : ControllerBase
    {
        private readonly ILessonsRepository<Lessons> _lessonsRepository;

        public LessonsController(ILessonsRepository<Lessons> lessonsRepository)
        {
            _lessonsRepository = lessonsRepository;
        }
        /// <summary>
        /// Popbierz lekcje za pomoca uuid
        /// </summary>
        /// <param name="uuid">Uuid lekcji</param>
        /// <returns>Zwraca obiekt lekcji lub 404 przy niepoprawnym uuid</returns>
        [HttpPost]
        [Route("GetLesson")]
        public async Task<IActionResult> GetLesson([FromForm] Guid uuid)
        {
            ICollection<Lessons> lessons = await _lessonsRepository.GetLesson(uuid);

            return lessons != null ? Ok(lessons) : NotFound(lessons);
        }
        /// <summary>
        /// Wstaw lekcje
        /// </summary>
        /// <param name="Subject_id">Uuid przedmiotu</param>
        /// <param name="Class_id">Uuid klasy</param>
        /// <param name="Teacher_id">Uuid nauczyciela</param>
        /// <param name="Topic">Temat</param>
        /// <returns>Zwraca uuid lekcji lub lub 400 przy niepoprawnych danych</returns>
        [HttpPost]
        [Route("InsertLesson")]
        public async Task<IActionResult> InsertLesson([FromForm] Guid Subject_id, [FromForm] Guid Class_id, [FromForm] Guid Teacher_id, [FromForm] string Topic)
        {
            Lessons lesson = new Lessons(Subject_id, Class_id,Teacher_id, Topic);
            Guid uuid = await _lessonsRepository.InsertLesson(lesson);

            if(uuid != Guid.Empty)
            {
                return CreatedAtAction(nameof(lesson), new { id = lesson.Lesson_id });
            }

            return BadRequest();
        }
        /// <summary>
        /// Edytuj lekcje
        /// </summary>
        /// <param name="uuid">Uuid lekcji ktora chcemy edytowac</param>
        /// <param name="Subject_id">Uuid przedmiotu</param>
        /// <param name="Class_id">Uuid klasy</param>
        /// <param name="Teacher_id">Uuid nauczyciela ktory stworzyl lekcje</param>
        /// <param name="Topic">Temaat</param>
        /// <returns>Zwraca kod 204 przy poprawnej zmianie. Kod 404 przy niepoprawnym uuid. Kod 401 przy niepoprawnym uuid nauczyciela</returns>
        [HttpPut]
        [Route("EditLesson")]
        public async Task<IActionResult> EditLesson([FromForm] Guid uuid, [FromForm] Guid Subject_id, [FromForm] Guid Class_id, [FromForm] Guid Teacher_id, [FromForm] string Topic)
        {
            int result = await _lessonsRepository.EditLesson(uuid, Subject_id, Class_id, Teacher_id, Topic);
            
            if(result == 0)
            {
                return NotFound();
            }else if(result == -1)
            {
                return Unauthorized();
            }

            return NoContent();
        }
        /// <summary>
        /// Usun lekcje
        /// </summary>
        /// <param name="uuid">Uuid lekcji</param>
        /// <returns>Zwraca kod 200 przy poprawnym usunieciu. Kod 404 przy niepoprawnym uuid</returns>
        [HttpDelete]
        [Route("DeleteLesson")]
        public async Task<IActionResult> DeleteLesson([FromForm] Guid uuid)
        {
            int result = await _lessonsRepository.DeleteLesson(uuid);

            if(result == 0)
            {
                return NotFound();
            }

            return Ok(uuid);
        }
    }
}
