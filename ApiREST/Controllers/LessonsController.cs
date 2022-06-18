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

        [HttpGet]
        [Route("GetLesson")]
        public async Task<IActionResult> GetLesson([FromForm] Guid uuid)
        {
            ICollection<Lessons> lessons = await _lessonsRepository.GetLesson(uuid);

            return lessons != null ? Ok(lessons) : NotFound(lessons);
        }

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
