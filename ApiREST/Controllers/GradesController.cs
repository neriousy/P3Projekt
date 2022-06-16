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
    public class GradesController : ControllerBase
    {
        private readonly IGradesRepository<Grades> _gradesRepository;
        public GradesController(IGradesRepository<Grades> gradesRepository)
        {
            _gradesRepository = gradesRepository;
        }

        [HttpGet]
        [Route("GetGrades")]
        public IActionResult GetGrades(Guid uuid)
        {
            ICollection<Grades> oceny = _gradesRepository.GetGrades(uuid);
            return oceny != null ? Ok(oceny) : NotFound(oceny);
        }

        [HttpGet]
        [Route("GetAllGrades")]
        public IActionResult GetAllGrades()
        {
            ICollection<Grades> oceny = _gradesRepository.GetAllGrades();
            return oceny != null ? Ok(oceny) : NotFound(oceny);

        }
    }
}
