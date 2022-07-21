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

        [HttpPost]
        [Route("GetLessonPlanByClassId")]
        public async Task<IActionResult> GetLessonPlanByClassId(Guid uuid)
        {
            IEnumerable<Lesson_plan> resp = await _lessonPlanRepository.GetLessonPlanByClassId(uuid);

            if(resp == null)
            {
                return NotFound(resp);
            }

            return Ok(resp);
        }

        [HttpPost]
        [Route("GetUniqueSubjectsByClassId")]
        public async Task<IActionResult> GetUniqueSubjectsByClassId(Guid uuid)
        {
            ICollection<Guid> resp = await _lessonPlanRepository.GetUniqueSubjectsByClassId(uuid);

            if(resp == null)
            {
                return NotFound(resp);
            }

            return Ok(resp);
        }
    }
}
